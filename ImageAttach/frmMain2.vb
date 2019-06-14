Option Explicit On
Option Strict On

Imports DevExpress
Imports System.Text
Imports System.IO


' this was written for Ricky (James) Holmes in Norfolk, but was generic enough that anyone can use it
' james.holmes@norfolk.gov 

' basically it takes images from a folder, loads them into a grid and allows the user to type in some
' metadata such as parcel and card/extension that it applies to (these two fields are validated against
' the proval database).  It pulls back the address (for user confirmation) the next seqno for the image
' and the area (township from parcel_base) and the date the picture was taken (from the photo's info or
' the files date and time).  It reads the prefs from proval to determine how to rename the photo and where
' to store it.  It also uses the security from proval when it starts up, and it get the database connection
' info from the proval com object also.  Images are then copied to the appropriate directory and (if 
' specified) then records are inserted into the image_index table in proval.
' the action column is used to determine if we should perform inserts, updates, or deletes in proval
' the source column determines if the record came from the user or from image_index
'
' if the attachtoproval setting is true, then filenames will be guaranteed to be unique when moving them
' over so that no images are inadvertently overwritten
'
' ver 2.4 - 01/19/12
' 1. added new setting for boone county since they save their photos to L:\Images\YYMM\PIN.jpg.  This 
' setting is to allow an override of the area field, so we pass in YYMM instead of the area when
' this setting's value is YYMM (reflected as a checkbox in the frmSettings window).
' ver 2.3 - 07/07/11
' 1. build the list of row ids of what to delete from the grid before actually deleting them in the dataset.
' ver 2.2 - 07/05/11
' 1. Image names were based on the record's image_seq_number, not on the sequence based on the extension/card
' for the parcel.  In this release, the filenames will be generated based on the next available seq number
' for the card, not the parcel, but the image_seq_number will not be affected-- they are still unique per
' lrsn.
' 2. When the image_path is stored in the database it will be uppercased to be consistent with existing data.
' 3. Made database queries case insensitive by using UPPER()
' 4. Forced refresh of grid when rows are moved around so they dont end up being scattered


''' <summary>
''' main window
''' </summary>
''' <remarks>
''' This application was written by marvin frederickson at Kenai Peninsula Borough/IT Dept,
''' with Visual Studio 2010 using the DevExpress XtraGrid component (Win Controls Suite)
''' for the fancy grid.
''' 
'''   mfrederickson@borough.kenai.ak.us
'''   907-714-2105
''' </remarks>
Public Class frmMain2
    Private _dbConString As String = String.Empty
    Private _ImageHelper As ImageHelper
    Private _ParcelHelper As ParcelHelper
    Private _OverrideAreaWith As String = String.Empty
    Private _ProValVersion As Integer = 0

    ''' <summary>
    ''' keep track of items that need to be resequenced
    ''' </summary>
    ''' <remarks></remarks>
    Private _NeedsReseq As New List(Of Integer)

    ''' <summary>
    ''' suppress image display when multiple rows are being added to the gridview
    ''' </summary>
    ''' <remarks></remarks>
    Private _SuspendImageDisplay As Boolean = False

    ''' <summary>
    ''' cache used for displaying thumbnails in the gridview
    ''' </summary>
    ''' <remarks></remarks>
    Private _ImageCache As Hashtable = New Hashtable()

    ''' <summary>
    ''' indicates that the timer event should refresh the data in the gridview
    ''' </summary>
    ''' <remarks></remarks>
    Private _RefreshData As Boolean = False

    ''' <summary>
    ''' connection info to display in status bar so we know if pointing to live or test
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' This is set by StartUp
    ''' </remarks>
    Public WriteOnly Property CommonDataSource As String
        Set(ByVal value As String)
            bsiDatabase.Caption = value.Trim
        End Set
    End Property

    ''' <summary>
    ''' connection information so we can update the database 
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' This is set from StartUp
    ''' </remarks>
    Public WriteOnly Property DbConnString As String
        Set(ByVal value As String)
            ' add AppName='ImageAttach'
            _dbConString = value & ";APP=" & Application.ProductName.Replace(";", "") & ";"
        End Set
    End Property

    ''' <summary>
    ''' set the window title to the application name, updates button text and version number
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = Application.ProductName
        UpdateAttachButton()
        Dim VersionInfo() As String = Application.ProductVersion.Split(CChar("."))
        bsiVersion.Caption = "Ver " & VersionInfo(0) & "." & VersionInfo(1)

        If My.Settings.ProValVersion IsNot Nothing AndAlso My.Settings.ProValVersion.Trim <> "" Then
            ' xx.yy.zz = xx * 10000 + yy * 100 + zz  ' asssumes semantic versioning
            Dim k As Integer = 4
            For Each j As String In My.Settings.ProValVersion.Trim.Split(CType(".", Char()))
                _ProValVersion = CInt(_ProValVersion + CInt(j) * 10 ^ k)
                k = k - 2
                If k < 0 Then
                    ' ignore build version
                    Exit For
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' update the text of the attach button based on settings
    ''' </summary>
    ''' <remarks>
    ''' This is called from the constructor and when the settings are changed
    ''' </remarks>
    Private Sub UpdateAttachButton()
        If My.Settings.AttachToProVal Then
            bbiAttachImages.Caption = "Send to ProVal"
        Else
            bbiAttachImages.Caption = "Move Images"
        End If
    End Sub

    ''' <summary>
    ''' confirm close when changes pending
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If IaDataset1.HasChanges Then

            Select Case MessageBox.Show("There are changes pending - do you want to save them?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)
                Case Windows.Forms.DialogResult.Yes
                    Save()
                    ' if could not save then dont close
                    If IaDataset1.HasChanges Then
                        e.Cancel = True
                    End If
                Case Windows.Forms.DialogResult.No
                    ' close anyway
                    e.Cancel = False
                Case Windows.Forms.DialogResult.Cancel
                    e.Cancel = True
            End Select
        End If
    End Sub

    ''' <summary>
    ''' load info for form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' This calls helper classes to load the proval image prefs and to set up connection and commands for parcel/image lookup
    ''' </remarks>
    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' load the image helper information
            _ImageHelper = New ImageHelper()
        Catch ex As Exception
            MessageBox.Show("Unable to load the image preferences.  Please verify they are set correctly under Prefs (System) Images in ProVal.  [Reason - " & ex.Message & "]", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.Exit()
        End Try

        ' 07/05/11 mjf - currently we only support parcel ids not alternate ids
        If _ImageHelper.ID <> ImageHelper.IdEnum.ParcelId Then
            MessageBox.Show("Currently this application only works with Parcel IDs, not Alternate IDs.  If you need it work with Alternate ID's contact mfrederickson@borough.kenai.ak.us and I can add that functionality.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.Exit()
        End If

        Try
            ' intialize the Parcel helper
            _ParcelHelper = New ParcelHelper(_dbConString, _ProValVersion)
        Catch ex As Exception
            MessageBox.Show("Unable to prepare for parcel validation.  [Reason - " & ex.Message & "]", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.Exit()
        End Try

        _OverrideAreaWith = My.Settings.OverrideAreaWith
    End Sub

    ''' <summary>
    ''' adds a row to the logsheet (from user's interaction - not from image_index table)
    ''' </summary>
    ''' <param name="OriginalFilename">name of the image file</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AppendUserRow(ByVal OriginalFilename As String) As Boolean
        Dim Added As Boolean = False

        OriginalFilename = OriginalFilename.ToLower.Trim
        If IaDataset1.LogSheet.Select("OriginalFilename = '" & OriginalFilename & "'").Length = 0 Then
            Dim r As IADataset.LogSheetRow = IaDataset1.LogSheet.NewLogSheetRow
            r.ParcelId = String.Empty
            r.Card = String.Empty
            r.OriginalFilename = OriginalFilename
            ' indicate that this is a row that the user is loading
            r.Source = "user"
            If My.Settings.AttachToProVal Then
                r.Action = "Insert"
            Else
                r.Action = "None"
            End If

            ' extract date information from image -- if cant then get from file
            Try
                Dim i As Image = Image.FromFile(OriginalFilename)
                Dim a As System.Text.ASCIIEncoding = New System.Text.ASCIIEncoding()
                Dim v As String = a.GetString(i.GetPropertyItem(&H132).Value)
                Dim p() As String = v.Split(CChar(" "))
                Dim dp() As String = p(0).Split(CChar(":"))
                Dim dt As DateTime = DateTime.Parse(dp(1) + "/" + dp(2) + "/" + dp(0) + " " + p(1))
                r._Date = dt
            Catch ex As Exception
                r._Date = File.GetCreationTime(OriginalFilename)
            End Try

            IaDataset1.LogSheet.AddLogSheetRow(r)
            Added = True
        End If

        Return Added
    End Function

    ''' <summary>
    ''' clear the entries from the grid and from the picturebox and from the image cache
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearGrid()
        _ImageCache = New Hashtable()
        IaDataset1.Clear()
        ClearImage()
    End Sub

    ''' <summary>
    ''' clear the image from the display
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearImage()
        PictureEdit1.Image = Nothing
    End Sub

    ''' <summary>
    ''' calls the imagehelper to generate the path
    ''' </summary>
    ''' <param name="OriginalPath"></param>
    ''' <param name="ParcelId"></param>
    ''' <param name="Card"></param>
    ''' <param name="SeqNo"></param>
    ''' <param name="Area"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' caller should handle errors
    ''' </remarks>
    Private Function GenerateFileName(ByVal OriginalPath As String, ByVal ParcelId As String, ByVal Card As String, ByVal SeqNo As Integer, ByVal Area As String) As String
        Dim FileName As String = String.Empty

        ' generate the filename

        FileName = _ImageHelper.Path(OriginalPath, ParcelId, Area, Card, SeqNo).ToLower

        ' since we only generate filenames for new images and if we have a filename and are attaching to proval, make sure it is unique
        If FileName <> String.Empty AndAlso My.Settings.AttachToProVal Then
            Dim DirName As String = System.IO.Path.GetDirectoryName(FileName)
            Dim BaseFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileName)
            Dim Extension As String = System.IO.Path.GetExtension(FileName)
            Dim i As Integer = 1

            ' if it already exists, and we are not allowing overwrites, then add a unique suffix
            While System.IO.File.Exists(FileName) And (Not My.Settings.AllowFileOverwrite)
                FileName = System.IO.Path.Combine(DirName, BaseFileName & "_" & i.ToString() & Extension)
                i = i + 1
            End While
        End If

        Return FileName
    End Function

    ''' <summary>
    ''' calls the routine to generate a filename for a specific data row
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 07/05/11 mjf - changed to calc the sequence number to pass to the generate function based on cards instead
    ''' of the image_seq_number (dr.SeqNo)
    ''' </remarks>
    Private Function GenerateFilenameForRow(ByVal dr As IADataset.LogSheetRow) As String
        Dim Filename As String = String.Empty

        'If Not (dr.IsOriginalFilenameNull Or dr.ParcelId.Trim = "" Or dr.IsCardNull Or dr.SeqNo = 0 Or dr.IsAreaNull) Then
        '    Filename = GenerateFileName(dr.OriginalFilename, dr.ParcelId, dr.Card, dr.SeqNo, dr.Area)
        'End If

        If Not (dr.IsOriginalFilenameNull Or dr.ParcelId.Trim = "" Or dr.IsCardNull Or dr.IsAreaNull) Then
            Dim SeqNo As Integer = 1

            ' based on the records up to our SeqNo, determine what our filename's seqno should be,
            ' based on records matching our card/extension 
            SeqNo = dr.Table.Select("ParcelId = '" & dr.ParcelId & "' and Card = '" & dr.Card & "' and SeqNo < " & dr.SeqNo.ToString & " and Id <> " & dr.Id.ToString).Length + 1

            ' 01/19/12 mjf override area with year and month of photo if desired
            Dim Area As String = dr.Area
            If _OverrideAreaWith = "YYMM" Then
                Area = String.Format("{0:yyMM}", dr._Date)
            End If

            Filename = GenerateFileName(dr.OriginalFilename, dr.ParcelId, dr.Card, SeqNo, Area)
        End If

        Return Filename
    End Function

    ''' <summary>
    ''' displays the image for the specified row
    ''' </summary>
    ''' <param name="RowHandle"></param>
    ''' <remarks></remarks>
    Private Sub DisplayImageForRow(ByVal RowHandle As Integer)
        If Not _SuspendImageDisplay Then
            PictureEdit1.Image = Nothing

            If RowHandle >= 0 AndAlso Not AdvBandedGridView1.IsGroupRow(RowHandle) Then
                Dim dr As IADataset.LogSheetRow = CType(AdvBandedGridView1.GetDataRow(RowHandle), IADataset.LogSheetRow)
                If Not dr.IsOriginalFilenameNull Then
                    Try
                        If Not System.IO.File.Exists(dr.OriginalFilename) Then
                            Status("Cannot display image - file not found - " & dr.OriginalFilename)
                            Exit Sub
                        End If

                        If PictureEdit1.IsLoading Then
                            PictureEdit1.CancelLoadAsync()
                        End If
                        PictureEdit1.LoadAsync(dr.OriginalFilename)
                        Status("Ready")
                    Catch ex As Exception
                        Status("Could not display image for " & dr.OriginalFilename & " - " & ex.Message)
                    End Try
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' returns true if the datagrid has unresolved errors or undetermined filenames
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>checks are nested so user is not inundated with errors</remarks>
    Private Function HasErrors() As Boolean
        If AdvBandedGridView1.IsEditing Then
            AdvBandedGridView1.PostEditor()
            AdvBandedGridView1.UpdateCurrentRow()
        End If
        For Each dr As IADataset.LogSheetRow In IaDataset1.LogSheet.Select("", "", DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
            ' check the parcel id
            If dr.ParcelId.Trim = "" Then
                dr.SetColumnError("ParcelId", "The Parcel Id is required.")
                ' cleared by field validation
            Else
                ' if there is no parcel error then check for a card error
                If dr.IsCardNull OrElse dr.Card = "" Then
                    dr.SetColumnError("Card", "The Card/Extension is required.")
                    ' cleared by field validation
                Else
                    ' if there is no parcel or card error then check for a seqno error
                    If dr.SeqNo = 0 Then
                        dr.SetColumnError("ParcelId", "The sequence number was not generated - please rekey the Parcel Id and Card/Extension.")
                    Else
                        ' cleared by ParcelId field validation
                        ' if there is no seqno error then check for a filename error
                        ' only check the filename column if this is a "user" record that has not yet been copied
                        If dr.Source = "user" And Not dr.Copied Then
                            If dr.Filename = "" Then
                                dr.SetColumnError("Filename", "A filename cannot be generated - please check the Parcel Id, Card, and SeqNo fields.")
                            Else
                                If System.IO.File.Exists(dr.Filename) And (Not My.Settings.AllowFileOverwrite) Then
                                    dr.SetColumnError("Filename", "A file with this name already exists at this location.")
                                Else
                                    dr.SetColumnError("Filename", "")
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Next

        Return IaDataset1.LogSheet.HasErrors()
    End Function

    ''' <summary>
    ''' displays status msg in status bar
    ''' </summary>
    ''' <param name="Msg"></param>
    ''' <remarks></remarks>
    Private Sub Status(ByVal Msg As String)
        bsiStatus.Caption = Msg
        bsiStatus.Refresh()
        barStatus.Invalidate()
    End Sub

    ''' <summary>
    ''' reassign seqno for all rows for specified parcel
    ''' </summary>
    ''' <param name="Lrsn">parcel to reassign sequencing for</param>
    ''' <param name="IgnoreRow">row to ignore when renumbering</param>
    ''' <remarks>This is usually called after an image is flagged/unflagged as deleted or an image's parcelid is changed.
    ''' the IgnoreRow is required to be used when renumbering an lrsn that is currently in the process of having one of
    ''' it's rows reassigned to a new lrsn but has not yet flushed that data to the datarow.
    ''' </remarks>
    Private Sub Resequence(ByVal Lrsn As Integer, Optional ByVal IgnoreRow As IADataset.LogSheetRow = Nothing)
        Dim SeqNo As Integer = 1

        ' sort non deleted rows first
        For Each dr As IADataset.LogSheetRow In IaDataset1.LogSheet.Select("Lrsn = " & Lrsn.ToString() & " and Action <> 'Delete'", "SeqNo")
            If Not dr.Equals(IgnoreRow) Then
                ' only renumber the row if it is currently incorrectly numbered
                If dr.SeqNo <> SeqNo Then
                    dr.SeqNo = SeqNo
                    ' if it is not already flagged to be inserted or updated then flag it for update
                    If dr.Action = "None" And My.Settings.AttachToProVal Then
                        dr.Action = "Update"
                    End If

                    ' 6/10/11 mjf moved inside if changed code block
                    ' regenerate filename for new ("user") records
                    RegenUserFileName(dr)
                End If

                SeqNo += 1
            End If
        Next
    End Sub

    ''' <summary>
    ''' save the photos to their new directory and push to proval
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Save() As Boolean
        Validate()
        AdvBandedGridView1.PostEditor()
        AdvBandedGridView1.UpdateCurrentRow()

        Dim Failed As Boolean = False

        ' make sure there are no errors on the parcelid, card, and filename
        Me.Cursor = Cursors.WaitCursor
        Status("Validating...")
        If HasErrors() Then
            AdvBandedGridView1.ExpandAllGroups()
            Me.Cursor = Cursors.Default
            MessageBox.Show("There are validation errors (or missing data) with the entries in the list.  Please resolve those before continuing.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Status("Ready")
            Return False
        End If

        ' For "user" rows (new rows not stored in image_index yet) that have their
        ' copied column set to false, we are going to copy those files over to their destination and set their copied flag to true
        ' so they are not reprocessed on subsequent saves.

        ' Then, we will delete the "table" records that have an Action of "Delete", removing the photos as we do so, and 
        ' actually removing the row from the dataset so it won't be reprocessed on subsequent saves.

        ' Then, we will update the description and seqno columns for the "table" records that have an Action of "Update", again
        ' acceptingchanges on those rows so they wont be reprocessed.
        ' Lastly, we will insert the "user" records, changing them to "table" records with an action of "None" and a NotModified rowstate.


        ' copy the files to their locations, resizing and renaming

        Status("copying files...")
        For Each dr As IADataset.LogSheetRow In IaDataset1.LogSheet.Rows

            ' only process rows that have changed and that are user rows (didn't come from exising image_index rows)
            If dr.RowState <> DataRowState.Unchanged AndAlso dr.Source = "user" AndAlso Not dr.Copied Then

                Try
                    Dim ImagePath As String = dr.OriginalFilename
                    Dim NewImagePath As String = dr.Filename.ToUpper()

                    ' resize image to preferred dimensions
                    Dim imgOrg As Bitmap = Nothing
                    Dim imgShow As Bitmap = Nothing
                    Dim g As Graphics
                    Dim divideBy, divideByH, divideByW As Double
                    imgOrg = DirectCast(Bitmap.FromFile(ImagePath), Bitmap)

                    divideByW = imgOrg.Width / My.Settings.ResizeWidth
                    divideByH = imgOrg.Height / My.Settings.ResizeHeight
                    If divideByW > 1 Or divideByH > 1 Then
                        If divideByW > divideByH Then
                            divideBy = divideByW
                        Else
                            divideBy = divideByH
                        End If
                    Else
                        ' no scaling needed
                        divideBy = 1.0
                    End If

                    imgShow = New Bitmap(CInt(CDbl(imgOrg.Width) / divideBy), CInt(CDbl(imgOrg.Height) / divideBy))
                    imgShow.SetResolution(imgOrg.HorizontalResolution, imgOrg.VerticalResolution)
                    g = Graphics.FromImage(imgShow)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.DrawImage(imgOrg, New Rectangle(0, 0, CInt(CDbl(imgOrg.Width) / divideBy), CInt(CDbl(imgOrg.Height) / divideBy)), 0, 0, imgOrg.Width, imgOrg.Height, GraphicsUnit.Pixel)
                    g.Dispose()
                    imgOrg.Dispose()

                    ' need to make new path if it does not already exist
                    Try
                        System.IO.Directory.CreateDirectory(NewImagePath.Substring(0, NewImagePath.LastIndexOf("\")))
                    Catch ex As Exception
                        Throw New Exception("could not create directory " & NewImagePath.Substring(0, NewImagePath.LastIndexOf("\")) & " - " & ex.Message)
                    End Try

                    ' might not have rights to save here
                    Try
                        If My.Settings.AllowFileOverwrite AndAlso System.IO.File.Exists(NewImagePath) Then
                            System.IO.File.Delete(NewImagePath)
                        End If
                        imgShow.Save(NewImagePath, Imaging.ImageFormat.Jpeg)
                    Catch ex As Exception
                        Throw New Exception("could not save file " & NewImagePath & " - " & ex.Message)
                    End Try

                    dr.Status = "Copied"
                    dr.Copied = True

                    ' if not attaching to proval, then mark this record as unchanged
                    If Not My.Settings.AttachToProVal Then
                        ' 06/10/11 mjf set action to none since copied file and were finished with it now
                        dr.Action = "None"
                        dr.AcceptChanges()
                    End If
                Catch ex As Exception
                    dr.Status = "could not copy file - " & ex.Message
                    Failed = True
                End Try
            End If
        Next

        Me.Cursor = Cursors.Default
        Status("Ready")

        If Failed Then
            MessageBox.Show("Unable to copy all images to destination folders - please see individual status messages.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        ' update proval 

        If My.Settings.AttachToProVal Then
            Me.Cursor = Cursors.WaitCursor

            Dim OriginalFilename As String = String.Empty
            Status("deleting/moving to history images from ProVal...")
            For Each dr As IADataset.LogSheetRow In IaDataset1.LogSheet.Select("action = 'Delete'")
                If dr.Source = "table" Then
                    Try
                        ' make sure to use original seqno since it may have changed before being deleted
                        ' also use original filename for exact match of existing record
                        OriginalFilename = dr.OriginalFilename
                        If OriginalFilename.StartsWith(_ImageHelper.ImagePath) Then
                            OriginalFilename = OriginalFilename.Substring(_ImageHelper.ImagePath.Length)
                            If OriginalFilename.StartsWith("\") Then
                                OriginalFilename = OriginalFilename.Substring(1)
                            End If
                        End If
                        If _ParcelHelper.RemoveFromParcel(dr.Lrsn, CType(dr.Item("SeqNo", DataRowVersion.Original), Integer), OriginalFilename) Then
                            ' remove the image file
                            ' dont delete if ProVal 9.1.5 or higher -- since they get put in 'H' history status
                            If _ProValVersion < 90105 Then
                                System.IO.File.Delete(dr.OriginalFilename)
                            End If
                        Else
                            Throw New Exception("could not delete image_index record - not found")
                        End If
                        dr.Status = "Deleted"
                        dr.Action = "None"
                        ' all done, so remove the row from the collection
                        dr.Table.Rows.Remove(dr)
                    Catch ex As Exception
                        dr.Status = "could not delete - " & ex.Message
                        Failed = True
                    End Try
                End If
            Next

            Status("updating images in ProVal...")
            For Each dr As IADataset.LogSheetRow In IaDataset1.LogSheet.Select("action = 'Update'")
                If dr.Source = "table" Then
                    Try
                        ' make sure to use original seqno since it may have changed before being deleted
                        ' also use original filename for exact match of existing record
                        OriginalFilename = dr.OriginalFilename
                        If OriginalFilename.StartsWith(_ImageHelper.ImagePath) Then
                            OriginalFilename = OriginalFilename.Substring(_ImageHelper.ImagePath.Length)
                            If OriginalFilename.StartsWith("\") Then
                                OriginalFilename = OriginalFilename.Substring(1)
                            End If
                        End If

                        If Not _ParcelHelper.UpdateParcel(dr.Lrsn, CType(dr.Item("SeqNo", DataRowVersion.Original), Integer), OriginalFilename, dr.SeqNo, dr.Description) Then
                            Throw New Exception("could not update record - not found")
                        End If
                        dr.Status = "Updated"
                        dr.Action = "None"
                        ' all done, so flag the row as unchanged
                        dr.AcceptChanges()
                    Catch ex As Exception
                        dr.Status = "could not update record - " & ex.Message
                        Failed = True
                    End Try
                End If
            Next

            Status("inserting images in ProVal...")
            For Each dr As IADataset.LogSheetRow In IaDataset1.LogSheet.Select("action = 'Insert'")

                ' only process changed "user" rows that have had their files copied
                If dr.RowState <> DataRowState.Unchanged Then
                    If dr.Source = "user" Then
                        If dr.Copied Then
                            ' also only process if the image was copied
                            Dim Filename As String = dr.Filename

                            Try
                                If Filename.StartsWith(_ImageHelper.ImagePath) Then
                                    Filename = Filename.Substring(_ImageHelper.ImagePath.Length)
                                    If Filename.StartsWith("\") Then
                                        Filename = Filename.Substring(1)
                                    End If
                                End If
                                _ParcelHelper.AttachToParcel(dr.Lrsn, dr.Card, dr.SeqNo, Filename.ToUpper, dr._Date, dr.Description)
                                dr.Status = "Inserted"
                                ' prepare the record to look like a "table" record
                                dr.OriginalFilename = dr.Filename
                                dr.Filename = ""
                                ' flag this row as unchanged so it does not get processed again during retries
                                dr.Source = "table"
                                dr.Action = "None"
                                dr.AcceptChanges()
                            Catch ex As Exception
                                Failed = True
                                dr.Status = "could not load into ProVal - " & ex.Message
                            End Try
                        Else
                            ' was not in "copied" state so do not process "user" item to send to proval
                        End If
                    End If
                End If
            Next

            Me.Cursor = Cursors.Default
            Status("Ready")

            If Failed Then
                Dim Msg As String = "Unable to copy all images as requested - please see individual status messages for details."
                If My.Settings.AttachToProVal Then
                    Msg = "Unable to send/update all images to ProVal - please see individual status messages for details."
                End If
                MessageBox.Show(Msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' cause a thumbnail of the image to display in the grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AdvBandedGridView1_CustomUnboundColumnData(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles AdvBandedGridView1.CustomUnboundColumnData
        If e.Column.FieldName = "colImage" AndAlso e.IsGetData Then
            Dim view As XtraGrid.Views.Grid.GridView = TryCast(sender, XtraGrid.Views.Grid.GridView)
            Dim logRow As IADataset.LogSheetRow = Nothing
            If e.Row IsNot Nothing Then
                logRow = CType(CType(e.Row, DataRowView).Row, IADataset.LogSheetRow)
            End If
            If logRow IsNot Nothing AndAlso Not logRow.IsOriginalFilenameNull Then
                Dim Filename As String = logRow.OriginalFilename
                Dim imgShow As Bitmap = Nothing

                ' load and cache thumbnail - scale it here so we cache smaller images not full sized ones
                If (Not _ImageCache.ContainsKey(Filename)) Then
                    If System.IO.File.Exists(Filename) Then
                        Try
                            Dim imgOrg As Bitmap = Nothing
                            Dim g As Graphics
                            Dim divideBy, divideByH, divideByW As Double
                            imgOrg = DirectCast(Bitmap.FromFile(Filename), Bitmap)

                            divideByW = imgOrg.Width / 200 ' thumbnail size
                            divideByH = imgOrg.Height / 150 ' thumbnail size
                            If divideByW > 1 Or divideByH > 1 Then
                                If divideByW > divideByH Then
                                    divideBy = divideByW
                                Else
                                    divideBy = divideByH
                                End If
                            Else
                                ' no scaling needed
                                divideBy = 1.0
                            End If

                            imgShow = New Bitmap(CInt(CDbl(imgOrg.Width) / divideBy), CInt(CDbl(imgOrg.Height) / divideBy))
                            imgShow.SetResolution(imgOrg.HorizontalResolution, imgOrg.VerticalResolution)
                            g = Graphics.FromImage(imgShow)
                            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                            g.DrawImage(imgOrg, New Rectangle(0, 0, CInt(CDbl(imgOrg.Width) / divideBy), CInt(CDbl(imgOrg.Height) / divideBy)), 0, 0, imgOrg.Width, imgOrg.Height, GraphicsUnit.Pixel)
                            g.Dispose()
                            imgOrg.Dispose()

                        Catch ex As Exception
                            ' todo: display error image
                            Debug.WriteLine("could not display custom image thumbnail here - " & ex.Message)
                        End Try
                        _ImageCache.Add(Filename, imgShow)
                    End If
                End If

                e.Value = _ImageCache(Filename)
            End If
        End If
    End Sub

    ''' <summary>
    ''' update the image when the row changes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AdvBandedGridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles AdvBandedGridView1.FocusedRowChanged
        DisplayImageForRow(e.FocusedRowHandle)
    End Sub

    ''' <summary>
    ''' validates the parcel id or the card
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AdvBandedGridView1_ValidatingEditor(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs) Handles AdvBandedGridView1.ValidatingEditor
        Dim frh As Integer = AdvBandedGridView1.FocusedRowHandle
        Dim dr As IADataset.LogSheetRow = CType(AdvBandedGridView1.GetDataRow(frh), IADataset.LogSheetRow)
        dr.Status = ""

        If AdvBandedGridView1.FocusedColumn.FieldName = "ParcelId" Then
            Dim ParcelId As String = e.Value.ToString.Trim
            Dim CellValue As String = AdvBandedGridView1.GetFocusedValue().ToString

            ' only validate if the parcel id changed, otherwise dont waste time (plus it would mess up sequence number)
            'If ParcelId <> dr.ParcelId Then

            Dim pi As ParcelHelper.ParcelInfo = Nothing

            Status("looking up parcel...")
            If ParcelId <> "" Then
                pi = _ParcelHelper.GetParcelInfo(ParcelId)
            End If
            If pi Is Nothing Then
                ' invalid parcel id - clear the card, filename, seqno, lrsn, address
                dr.PropertyAddress = String.Empty
                dr.Card = String.Empty
                dr.Filename = String.Empty
                dr.SeqNo = 0
                dr.Lrsn = 0
                dr.Area = String.Empty

                dr.SetColumnError("Card", "")
                dr.SetColumnError("Filename", "")

                If ParcelId = "" Then
                    dr.SetColumnError("ParcelId", "A Parcel Id is required.")
                Else
                    dr.SetColumnError("ParcelId", "The specified parcel could not be found.")
                    e.ErrorText = "The specified parcel could not be found."
                    e.Valid = False
                End If
            Else

                ' valid parcel so clear any leftover error validation messages
                dr.SetColumnError("Card", "")
                dr.SetColumnError("ParcelId", "")
                dr.SetColumnError("Filename", "")

                ' load all of the image_index records related to this parcel into the list so they can be reorderd if desired
                ' but only if none exist yet - dont want to try to load all images each time we have two or more new ones for the
                ' same parcel
                AdvBandedGridView1.OptionsBehavior.ImmediateUpdateRowPosition = False
                LoadImagesForParcel(pi)

                ' remember the old lrsn so we can renumber it
                Dim OldLrsn As Integer = dr.Lrsn

                ' instead of doing this, lets set the cell values
                If False Then
                    ' load the information for the parcel we just fetched
                    dr.PropertyAddress = pi.Address
                    dr.Area = pi.Area
                    ' clear the card and filename since filename depends on card and card depends on parcel id
                    dr.Card = ""
                    dr.Filename = ""
                    ' update to the new lrsn
                    dr.Lrsn = pi.Lrsn
                    dr.ParcelId = ParcelId

                    ' determine what this row's seqno should be (must happen after other images are loaded)
                    dr.SeqNo = GetNextSeqNo(pi.Lrsn, dr)
                Else
                    AdvBandedGridView1.SetRowCellValue(frh, "PropertyAddress", pi.Address)
                    AdvBandedGridView1.SetRowCellValue(frh, "Area", pi.Area)
                    AdvBandedGridView1.SetRowCellValue(frh, "Card", "")
                    AdvBandedGridView1.SetRowCellValue(frh, "Filename", "")
                    AdvBandedGridView1.SetRowCellValue(frh, "Lrsn", pi.Lrsn)
                    AdvBandedGridView1.SetRowCellValue(frh, "ParcelId", ParcelId)
                    AdvBandedGridView1.SetRowCellValue(frh, "SeqNo", GetNextSeqNo(pi.Lrsn))
                End If

                ' if the parcel changed then we need to renumber the old one
                If pi.Lrsn <> OldLrsn And OldLrsn > 0 Then
                    ' if the resequencing here ends up causing a problem then dont do it
                    ' instead let them do it manually
                    Resequence(OldLrsn, dr)

                    _RefreshData = True
                    Timer1.Enabled = True
                End If

                AdvBandedGridView1.OptionsBehavior.ImmediateUpdateRowPosition = True
            End If
            Status("Ready")

        ElseIf AdvBandedGridView1.FocusedColumn.FieldName = "Card" Then
            ' verify the card
            Dim Card As String = e.Value.ToString.Trim

            ' if we don't have a parcel id then we can't validate the card
            Status("looking up extension...")
            If dr.Lrsn = 0 Then
                dr.Filename = String.Empty
                dr.SetColumnError("Filename", "")

                dr.SetColumnError("Card", "The Card cannot be validated until a valid Parcel Id has been specified.")
            Else
                If Card.Length > 0 Then
                    If _ParcelHelper.ValidateCard(dr.Lrsn, Card) Then
                        ' it is a valid card so clear the error if any and 
                        dr.SetColumnError("Card", "")

                        ' update filename
                        Try
                            ' could have problem with pin and sections defs or card number too big (for single digit), etc.
                            dr.Card = Card ' this must be set before we regenerate teh filename
                            RegenUserFileName(dr)

                            'dr.Filename = GenerateFilenameForRow(dr)
                            'dr.Copied = False ' since we are possibly changing the file name, unmark it as copied
                            'If Not My.Settings.AttachToProVal Then
                            '    ' if this file name is not unique then it is going to overwrite something and we should tell the user
                            '    If System.IO.File.Exists(dr.Filename) Then
                            '        dr.SetColumnError("Filename", "A file with this name already exists at this location.")
                            '    Else
                            '        dr.SetColumnError("Filename", "")
                            '    End If
                            'Else
                            '    dr.SetColumnError("Filename", "")
                            'End If
                        Catch ex As Exception
                            dr.Filename = String.Empty
                            dr.SetColumnError("Filename", "could not generate filename - " & ex.Message)
                        End Try
                    Else
                        ' specified card is invalid
                        dr.Filename = String.Empty
                        dr.SetColumnError("Filename", "")
                        dr.SetColumnError("Card", "The specified card/extension does not exist for this parcel.")
                        e.ErrorText = "The specified card/extension does not exist for this parcel."
                        e.Valid = False
                    End If
                Else
                    dr.Filename = String.Empty
                    dr.SetColumnError("Filename", "")
                    dr.SetColumnError("Card", "The card/extension is required.")
                End If
            End If
            Status("Ready")
        ElseIf AdvBandedGridView1.FocusedColumn.FieldName = "Description" Then
            If dr.Source = "table" Then
                Dim OldDescription As String = String.Empty
                If Not dr.IsNull(dr.Table.Columns("Description"), DataRowVersion.Original) Then
                    OldDescription = CStr(dr.Item("Description", DataRowVersion.Original))
                End If
                If e.Value.ToString.Trim() <> OldDescription And dr.Action = "None" Then
                    dr.Action = "Update"
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' loads images in the specified parcel info data if not already loaded
    ''' </summary>
    ''' <param name="pi"></param>
    ''' <remarks></remarks>
    Private Sub LoadImagesForParcel(ByVal pi As ParcelHelper.ParcelInfo)
        If IaDataset1.LogSheet.Select("lrsn = " & pi.Lrsn).Length = 0 Then
            For Each ii As ParcelHelper.ImageIndex In pi.Images
                ' only add if a row for this lrsn and image is not already loaded
                Dim OriginalFilename As String = System.IO.Path.Combine(_ImageHelper.ImagePath, ii.ImagePath).ToLower
                If IaDataset1.LogSheet.Select("lrsn = " & pi.Lrsn & " and OriginalFilename = '" & OriginalFilename & "'").Length = 0 Then
                    Dim nr As IADataset.LogSheetRow = IaDataset1.LogSheet.NewLogSheetRow
                    nr.ParcelId = pi.ParcelId
                    nr.Lrsn = pi.Lrsn
                    nr.Filename = String.Empty
                    nr.OriginalFilename = OriginalFilename
                    nr.PropertyAddress = pi.Address
                    nr.Area = pi.Area
                    nr.Card = ii.Card
                    nr.Description = ii.Description
                    nr._Date = ii.ImageDate
                    nr.SeqNo = ii.SeqNo
                    ' indicate that these records came from the image_index table so we dont try to insert them later
                    nr.Source = "table"
                    nr.Action = "None"

                    IaDataset1.LogSheet.AddLogSheetRow(nr)
                    ' make sure this row state is unmodified
                    nr.AcceptChanges()
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' shows the settings dialog window
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bbiSettings_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSettings.ItemClick
        Dim f As New frmSettings
        f.ProValSettings = _ImageHelper.ToString()
        f.ListHasItems = IaDataset1.LogSheet.Rows.Count > 0
        If f.ShowDialog(Me) = DialogResult.OK Then
            If f.AttachToProValSettingChanged Then
                ClearGrid()
            End If
            UpdateAttachButton()
            _OverrideAreaWith = My.Settings.OverrideAreaWith
        End If
        Status("Ready")
    End Sub

    ''' <summary>
    ''' loads one or more jpg files
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bbiGetImage_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiGetImage.ItemClick
        OpenFileDialog1.InitialDirectory = My.Settings.LastPath
        OpenFileDialog1.Multiselect = True
        OpenFileDialog1.Title = "Select Images to Load"
        OpenFileDialog1.DefaultExt = "jpg"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Status("loading images...")
            Me.Cursor = Cursors.WaitCursor
            Dim DuplicatesExist As Boolean = False
            Dim Loaded As Boolean = False
            For Each SourceFilename As String In OpenFileDialog1.FileNames
                SourceFilename = SourceFilename.ToLower
                If SourceFilename.EndsWith(".jpg") Then
                    If Not AppendUserRow(SourceFilename) Then
                        DuplicatesExist = True
                    Else
                        Loaded = True
                    End If
                End If
            Next

            ' todo: problem - sometimes new row is not focused, and group is not expanded so cant see row
            If Loaded Then
                ' go to first new item
                AdvBandedGridView1.RefreshData()
                AdvBandedGridView1.FocusedColumn = AdvBandedGridView1.Columns("ParcelId")
                AdvBandedGridView1.FocusedRowHandle = 0
                AdvBandedGridView1.SetRowExpanded(-1, True)
            End If

            If DuplicatesExist Then
                Status("Duplicate filenames were not loaded")
            Else
                Status("Ready")
            End If
            Me.Cursor = Cursors.Default
        End If
    End Sub

    ''' <summary>
    ''' load all *.jpg images (from specified folder) into the grid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' These rows have a source of user and their rowstate is new/modified.
    ''' </remarks>
    Private Sub bbiGetFolder_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiGetFolder.ItemClick
        ' clear the grid first, if needed
        If IaDataset1.HasChanges Then
            If MessageBox.Show("Are you sure you want to clear the list?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        ClearGrid()

        ' default the path to the last one loaded
        If My.Settings.LastPath.Trim <> "" Then
            FolderBrowserDialog1.SelectedPath = My.Settings.LastPath.Trim
        End If

        ' load the folder they selected
        If FolderBrowserDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Me.Cursor = Cursors.WaitCursor

            Status("saving location...")
            My.Settings.LastPath = FolderBrowserDialog1.SelectedPath
            My.Settings.Save()

            Status("loading images...")
            ' suspend picture display on rowchange
            _SuspendImageDisplay = True
            ' get all jpg files
            For Each SourceFileName As String In Directory.GetFiles(FolderBrowserDialog1.SelectedPath, "*.jpg")
                AppendUserRow(SourceFileName)
            Next
            _SuspendImageDisplay = False
            Me.Cursor = Cursors.Default
            Status("Ready")

            ' update the current image to the current row
            DisplayImageForRow(AdvBandedGridView1.FocusedRowHandle)
        End If
    End Sub

    ''' <summary>
    ''' move the photos, resizing and renaming as we go, then go back and load them into proval
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bbiAttachImages_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiAttachImages.ItemClick
        Save()
    End Sub

    ''' <summary>
    ''' clears the list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bbiClear_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiClear.ItemClick
        If IaDataset1.HasChanges Then
            If MessageBox.Show("Are you sure you want to clear the list?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
        End If

        ClearGrid()
    End Sub

    ''' <summary>
    ''' returns the next available sequence number for a given lrsn
    ''' </summary>
    ''' <param name="Lrsn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNextSeqNo(ByVal Lrsn As Integer, Optional ByVal IgnoreRow As IADataset.LogSheetRow = Nothing) As Integer
        Dim NextSeqNo As Integer = 1

        Dim drs() As IADataset.LogSheetRow = CType(IaDataset1.LogSheet.Select("lrsn = " & Lrsn & " and action <> 'Delete'", "SeqNo desc"), IADataset.LogSheetRow())
        If drs.Length > 0 Then
            ' skip over row to ignore, if any
            If IgnoreRow IsNot Nothing Then
                If drs(0).Equals(IgnoreRow) Then
                    ' we're on the row to ignore so skip it
                    If drs.Length > 1 Then
                        NextSeqNo = drs(1).SeqNo + 1
                    End If
                Else
                    ' we're not on the row to ignore, to use it
                    NextSeqNo = drs(0).SeqNo + 1
                End If
            Else
                ' no row to ignore so take value + 1
                NextSeqNo = drs(0).SeqNo + 1
            End If
        End If

        Return NextSeqNo
    End Function

    ''' <summary>
    ''' move the current row to the top
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MoveTopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveTopToolStripMenuItem.Click
        Dim dr As IADataset.LogSheetRow = CType(AdvBandedGridView1.GetDataRow(AdvBandedGridView1.FocusedRowHandle), IADataset.LogSheetRow)
        ' if we are not already the first in the list
        If dr.SeqNo <> 1 Then
            ' set the seqno before the first one, then call the resequence method
            dr.SeqNo = 0
            Resequence(dr.Lrsn)
            Status("Ready")
        End If
        ' 07/05/11 mjf - force refresh so rows dont get scattered when reording a row for a parcel that should be higher in the list
        RefreshGrid()
    End Sub

    ''' <summary>
    ''' Move the current row up one
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MoveUpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveUpToolStripMenuItem.Click
        ' swap with row before
        Dim dr As IADataset.LogSheetRow = CType(AdvBandedGridView1.GetDataRow(AdvBandedGridView1.FocusedRowHandle), IADataset.LogSheetRow)

        ' dont move up if already at top
        If dr.SeqNo <> 1 Then
            ' get the non-deleted row before this one
            Dim drs() As IADataset.LogSheetRow = CType(IaDataset1.LogSheet.Select("lrsn = " & dr.Lrsn & " and Action <> 'Delete' and SeqNo < " & dr.SeqNo.ToString(), "SeqNo desc"), IADataset.LogSheetRow())
            If drs.Length > 0 Then
                Dim SeqNo As Integer = dr.SeqNo
                dr.SeqNo = drs(0).SeqNo
                If dr.Action = "None" Then
                    dr.Action = CStr(IIf(dr.Source = "table", "Update", "Insert"))
                End If
                drs(0).SeqNo = SeqNo
                If drs(0).Action = "None" Then
                    drs(0).Action = CStr(IIf(drs(0).Source = "table", "Update", "Insert"))
                End If

                RegenUserFileName(dr)
                RegenUserFileName(drs(0))

                ' 07/05/11 mjf - force refresh so rows dont get scattered when reording a row for a parcel that should be higher in the list
                RefreshGrid()

                Status("Ready")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Move the current row down one
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MoveDownToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveDownToolStripMenuItem.Click
        ' swap with row after
        Dim dr As IADataset.LogSheetRow = CType(AdvBandedGridView1.GetDataRow(AdvBandedGridView1.FocusedRowHandle), IADataset.LogSheetRow)

        ' dont move down if already at bottom
        If dr.SeqNo < GetNextSeqNo(dr.Lrsn) - 1 Then
            ' get the non-deleted row before this one
            Dim drs() As IADataset.LogSheetRow = CType(IaDataset1.LogSheet.Select("lrsn = " & dr.Lrsn & " and Action <> 'Delete' and SeqNo > " & dr.SeqNo.ToString(), "SeqNo"), IADataset.LogSheetRow())
            If drs.Length > 0 Then
                Dim SeqNo As Integer = dr.SeqNo
                dr.SeqNo = drs(0).SeqNo
                If dr.Action = "None" Then
                    dr.Action = CStr(IIf(dr.Source = "table", "Update", "Insert"))
                End If
                drs(0).SeqNo = SeqNo
                If drs(0).Action = "None" Then
                    drs(0).Action = CStr(IIf(drs(0).Source = "table", "Update", "Insert"))
                End If

                RegenUserFileName(dr)
                RegenUserFileName(drs(0))

                ' 07/05/11 mjf - force refresh so rows dont get scattered when reording a row for a parcel that should be higher in the list
                RefreshGrid()

                Status("Ready")
            End If
        End If
    End Sub

    ''' <summary>
    ''' move the current row to the bottom
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MoveBottomToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveBottomToolStripMenuItem.Click
        Dim dr As IADataset.LogSheetRow = CType(AdvBandedGridView1.GetDataRow(AdvBandedGridView1.FocusedRowHandle), IADataset.LogSheetRow)

        ' set the after the last one, then call the resequence method
        dr.SeqNo = GetNextSeqNo(dr.Lrsn)
        Resequence(dr.Lrsn)

        ' 07/05/11 mjf - force refresh so rows dont get scattered when reording a row for a parcel that should be higher in the list
        RefreshGrid()

        Status("Ready")
    End Sub

    ''' <summary>
    ''' regenerate the filename for user record and check for file existence
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Sub RegenUserFileName(ByVal dr As IADataset.LogSheetRow)
        If dr.Source = "user" Then
            dr.Filename = GenerateFilenameForRow(dr)

            If System.IO.File.Exists(dr.Filename) And (Not My.Settings.AllowFileOverwrite) Then
                dr.SetColumnError("Filename", "A file with this name already exists at this location.")
            Else
                dr.SetColumnError("Filename", "")
            End If

            ' since filename has changed, mark as not copied
            dr.Copied = False
        End If
    End Sub

    ''' <summary>
    ''' deletes or undeletes the row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Deleted rows has a seqno of 9999 so they appear at the bottom (until deleted), new ("user") rows are removed from the table
    ''' immediately, pre-existing "table" rows are merely flagged as deleted by their Action and can be undeleted.  When records are
    ''' undeleted they are given the next available seqno.
    ''' </remarks>
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim dr As IADataset.LogSheetRow = Nothing

        ' remove all of the selected rows
        AdvBandedGridView1.BeginDataUpdate()

        ' build the list from the grid before we start destroying rows
        Dim IdsToDelete As New List(Of Integer)
        For Each i As Integer In AdvBandedGridView1.GetSelectedRows()
            dr = CType(AdvBandedGridView1.GetDataRow(i), IADataset.LogSheetRow)
            IdsToDelete.Add(dr.Id)
        Next

        ' delete the rows in the dataset
        For Each i As Integer In IdsToDelete
            dr = IaDataset1.LogSheet.FindById(i)

            If dr IsNot Nothing Then
                If dr.Source = "table" Then
                    If dr.Action = "Delete" Then
                        dr.SeqNo = GetNextSeqNo(dr.Lrsn)
                        ' update must occur after the next seqno or it throws off the numbering
                        dr.Action = "Update"
                    Else
                        dr.Action = "Delete"
                        dr.SeqNo = 9999
                        Resequence(dr.Lrsn)
                    End If

                    If dr.Action = "Update" Then
                        ' check to see if the seqno or the description have changed because if not then this should be "none"
                        If CType(dr.Item("SeqNo", DataRowVersion.Original), Integer) = dr.SeqNo AndAlso CType(dr.Item("Description", DataRowVersion.Original), String) = dr.Description Then
                            dr.Action = "None"
                            dr.AcceptChanges()
                        End If
                    End If
                ElseIf dr.Source = "user" Then
                    ' it's not in the table yet, so just delete it and renumber its remaining items
                    Dim Lrsn As Integer = dr.Lrsn
                    dr.Delete()
                    If Lrsn > 0 Then
                        Resequence(Lrsn)
                    End If
                End If
            End If
        Next
        AdvBandedGridView1.EndDataUpdate()
        AdvBandedGridView1.ClearSelection()

        Status("Ready")
    End Sub

    ''' <summary>
    ''' disable editing of parcelid and card for existing image_index rows
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AdvBandedGridView1_ShowingEditor(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles AdvBandedGridView1.ShowingEditor
        If AdvBandedGridView1.FocusedColumn.FieldName = "ParcelId" Or AdvBandedGridView1.FocusedColumn.FieldName = "Card" Then
            Dim dr As IADataset.LogSheetRow = CType(AdvBandedGridView1.GetDataRow(AdvBandedGridView1.FocusedRowHandle), IADataset.LogSheetRow)
            If dr.Source = "table" Then
                e.Cancel = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' show the menu for changing the order and for deleting
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AdvBandedGridView1_PopupMenuShowing(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles AdvBandedGridView1.PopupMenuShowing
        Dim HitInfo As XtraGrid.Views.Grid.ViewInfo.GridHitInfo = AdvBandedGridView1.CalcHitInfo(e.Point)
        If HitInfo.InRow AndAlso Not AdvBandedGridView1.IsGroupRow(HitInfo.RowHandle) Then
            MoveTopToolStripMenuItem.Enabled = False
            MoveUpToolStripMenuItem.Enabled = False
            MoveBottomToolStripMenuItem.Enabled = False
            MoveDownToolStripMenuItem.Enabled = False
            DeleteToolStripMenuItem.Text = "Delete"

            Dim dr As IADataset.LogSheetRow = CType(AdvBandedGridView1.GetDataRow(HitInfo.RowHandle), IADataset.LogSheetRow)
            ' only allow moving if there is an lrsn and then only as possible
            If dr.Lrsn <> 0 And dr.Action <> "Delete" Then
                Dim LastSeqNo As Integer = GetNextSeqNo(dr.Lrsn) - 1

                MoveTopToolStripMenuItem.Enabled = dr.SeqNo > 1
                MoveUpToolStripMenuItem.Enabled = dr.SeqNo > 1
                MoveBottomToolStripMenuItem.Enabled = dr.SeqNo < LastSeqNo
                MoveDownToolStripMenuItem.Enabled = dr.SeqNo < LastSeqNo
            End If
            ResequenceToolStripMenuItem.Enabled = dr.Lrsn <> 0

            ' only allow group exp/coll when grouping is enabled
            ExpandAllGroupsToolStripMenuItem.Enabled = Not bbiWorksheetMode2.Checked
            CollapseAllGroupsToolStripMenuItem.Enabled = Not bbiWorksheetMode2.Checked

            If dr.Action = "Delete" Then
                DeleteToolStripMenuItem.Text = "Undelete"
            End If

            AdvBandedGridView1.FocusedRowHandle = HitInfo.RowHandle
            cmsRow.Show(AdvBandedGridView1.GridControl, e.Point)
        ElseIf AdvBandedGridView1.IsGroupRow(HitInfo.RowHandle) Then
            cmsGroupRow.Show(AdvBandedGridView1.GridControl, e.Point)
        End If
    End Sub

    ''' <summary>
    ''' refresh the view (if needed) after the validation processesing has completed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' This is required because if the ImmediateUpdateRowPosition is not turned off during the validation processing then
    ''' incorrect rows end up getting updated by the DevExpress code.  So we turn it off during the validation and back on at
    ''' the end of the validation process.  And if a pin changes, then it wont always move to its new group, so we have to 
    ''' cause a delayed refreshdata to occur.
    ''' </remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If _RefreshData Then
            Timer1.Enabled = False
            AdvBandedGridView1.RefreshData()
            _RefreshData = False
        End If
    End Sub

    ''' <summary>
    ''' resequence the pin's images
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' This is required because when an image is moved from one pin to another, the old pin is not resequenced
    ''' </remarks>
    Private Sub ResequenceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResequenceToolStripMenuItem.Click
        Dim dr As IADataset.LogSheetRow = CType(AdvBandedGridView1.GetDataRow(AdvBandedGridView1.FocusedRowHandle), IADataset.LogSheetRow)
        Resequence(dr.Lrsn)
        Status("Ready")
    End Sub

    ''' <summary>
    ''' manually refresh the datagrid because the rows may not be sorted properly at some point
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        RefreshGrid()
        Status("Ready")
    End Sub

    Private Sub RefreshGrid()
        Dim dr As DataRow = AdvBandedGridView1.GetFocusedDataRow()
        AdvBandedGridView1.RefreshData()
        'If dr IsNot Nothing Then
        'End If
    End Sub


    ''' <summary>
    ''' toggle worksheet mode or image index arrangement mode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bbiWorksheetMode2_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiWorksheetMode2.ItemClick
        Status("switching display mode...")
        If bbiWorksheetMode2.Checked Then
            _SuspendImageDisplay = True
            AdvBandedGridView1.BeginUpdate()
            ' enable worksheet mode
            ' no grouping
            colParcelId.GroupIndex = -1
            ' no sorting (other than by order loaded)
            colParcelId.SortIndex = -1
            colSeqNo.SortIndex = -1
            colId.SortIndex = 0
            colId.SortOrder = DevExpress.Data.ColumnSortOrder.Descending
            ' filter to show only insert rows
            AdvBandedGridView1.ActiveFilterString = "[Source] = 'user'"
            AdvBandedGridView1.EndUpdate()
            ' clear the image display if no rows are shown
            If AdvBandedGridView1.RowCount = 0 Then
                ClearImage()
            End If
            _SuspendImageDisplay = False
            DisplayImageForRow(AdvBandedGridView1.FocusedRowHandle)
        Else
            SwitchToNormalMode()
        End If
        Status("Ready")
    End Sub

    ''' <summary>
    ''' switch back to normal mode - grouping and sorting with no filtering
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SwitchToNormalMode()
        AdvBandedGridView1.BeginUpdate()
        ' group by parcel id
        colParcelId.GroupIndex = 0
        ' sort by parcel id and seqno
        colParcelId.SortIndex = 0
        colSeqNo.SortIndex = 1
        colId.SortIndex = -1
        ' remove the filter
        AdvBandedGridView1.ClearColumnsFilter()
        AdvBandedGridView1.EndUpdate()
    End Sub

    ''' <summary>
    ''' load a particular parcels images
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bbiGetParcel_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiGetParcel.ItemClick
        ' should not be in worksheet mode otherwise you wont see the images because they will be filtered out

        If bbiWorksheetMode2.Checked Then
            Select Case MessageBox.Show("You wont be able to see the images for the parcel because you are currently in Worksheet mode.  Do you want to switch to out of Worksheet mode?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                Case Windows.Forms.DialogResult.Yes
                    bbiWorksheetMode2.Checked = False
                    SwitchToNormalMode()
                Case Windows.Forms.DialogResult.Cancel
                    Exit Sub
            End Select
        End If

        ' prompt for parcel Id and then load images if not already loaded
        Dim f As New frmGetParcel
        f.SetParcelHelper(_ParcelHelper)
        If f.ShowDialog(Me) = DialogResult.OK Then
            LoadImagesForParcel(f.ParcelInfo)
            ' go to first record
            AdvBandedGridView1.FocusedRowHandle = AdvBandedGridView1.LocateByValue("ParcelId", f.ParcelInfo.ParcelId)
        End If
        f.Close()

        Status("Ready")
    End Sub

    ''' <summary>
    ''' expand the groups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExpandAllGroupsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpandAllGroupsToolStripMenuItem.Click
        AdvBandedGridView1.ExpandAllGroups()
    End Sub

    ''' <summary>
    ''' collapse the groups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CollapseAllGroupsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollapseAllGroupsToolStripMenuItem.Click
        AdvBandedGridView1.CollapseAllGroups()
    End Sub

    Private Sub ExpandAllGroupsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpandAllGroupsToolStripMenuItem1.Click
        AdvBandedGridView1.ExpandAllGroups()
    End Sub

    Private Sub CollapseAllGroupsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollapseAllGroupsToolStripMenuItem1.Click
        AdvBandedGridView1.CollapseAllGroups()
    End Sub

    Private Sub bbiHelp_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiHelp.ItemClick
        System.Diagnostics.Process.Start(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "Batch Image Attachment Utility.pdf"))
    End Sub
End Class