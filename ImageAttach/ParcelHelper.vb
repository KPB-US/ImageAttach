Imports System.Data.Odbc

Public Class ParcelHelper
    Private dbConnString As String
    Private dbCon As OdbcConnection

    Private cmdGetInfo As OdbcCommand
    Private cmdGetExtension As OdbcCommand
    Private cmdSaveImage As OdbcCommand
    Private cmdDelete As OdbcCommand
    Private cmdUpdate As OdbcCommand

    ''' <summary>
    ''' set up commands
    ''' </summary>
    ''' <param name="DbConnString"></param>
    ''' <remarks>
    ''' 07/05/11 mjf changed to make case insensitive queries by using UPPER()
    ''' </remarks>
    Public Sub New(ByVal DbConnString As String)
        Me.dbConnString = DbConnString
        dbCon = New OdbcConnection(DbConnString)
        dbCon.Open()

        ' warning: GetParCcelInfo method below references these columns by index number instead of name
        ' 7/25/14 mjf ProVal version 9 uses PropertyStreet instead of prop_street
        If My.Settings.ProValVersion9 Then
            cmdGetInfo = New OdbcCommand("select pb.lrsn, ii.image_seq_number, ii.image_description, ii.image_path, ii.image_date, ii.image_text2, pb.township_number as area, pb.PropertyStreet as address from parcel_base pb left outer join image_index ii on ii.lrsn = pb.lrsn " & _
            " where pb.parcel_id = ?", dbCon)
        Else
            cmdGetInfo = New OdbcCommand("select pb.lrsn, ii.image_seq_number, ii.image_description, ii.image_path, ii.image_date, ii.image_text2, pb.township_number as area, pb.prop_street as address from parcel_base pb left outer join image_index ii on ii.lrsn = pb.lrsn " & _
            " where pb.parcel_id = ?", dbCon)
        End If
        cmdGetInfo.Parameters.Add(New OdbcParameter("@parcelid", Odbc.OdbcType.VarChar))


        cmdDelete = New OdbcCommand("delete from image_index where lrsn = ? and image_seq_number = ? and upper(image_path) = upper(?)", dbCon)
        cmdDelete.Parameters.Add(New OdbcParameter("@lrsn", Odbc.OdbcType.Int))
        cmdDelete.Parameters.Add(New OdbcParameter("@image_seq_number", Odbc.OdbcType.Int))
        cmdDelete.Parameters.Add(New OdbcParameter("@image_path", Odbc.OdbcType.VarChar))

        cmdUpdate = New OdbcCommand("update image_index set image_seq_number = ?, image_description = ? where lrsn = ? and image_seq_number = ? and upper(image_path) = upper(?)", dbCon)
        cmdUpdate.Parameters.Add(New OdbcParameter("@image_seq_number", Odbc.OdbcType.Int))
        cmdUpdate.Parameters.Add(New OdbcParameter("@description", Odbc.OdbcType.VarChar))
        cmdUpdate.Parameters.Add(New OdbcParameter("@lrsn", Odbc.OdbcType.Int))
        cmdUpdate.Parameters.Add(New OdbcParameter("@old_image_seq_number", Odbc.OdbcType.Int))
        cmdUpdate.Parameters.Add(New OdbcParameter("@image_path", Odbc.OdbcType.VarChar))

        cmdGetExtension = New OdbcCommand("select extension from extensions where lrsn = ? and status = 'A' and extension = ?", dbCon)
        cmdGetExtension.Parameters.Add(New OdbcParameter("@lrsn", Odbc.OdbcType.Int))
        cmdGetExtension.Parameters.Add(New OdbcParameter("@extension", Odbc.OdbcType.VarChar))

        cmdSaveImage = New OdbcCommand("insert into image_index (lrsn, image_seq_number, image_description, image_path, image_date, detail_image, history, image_int1, image_int2, image_int3, image_flag1, image_flag2, image_flag3, image_text1, image_text2) " & _
                                       "values (?, ?, ?, ?, ?, 'N', 'N', 0, 0, 0, '', '', '', '', ?)", dbCon)
        cmdSaveImage.Parameters.Add(New OdbcParameter("@lrsn", Odbc.OdbcType.Int))
        cmdSaveImage.Parameters.Add(New OdbcParameter("@image_seq_number", Odbc.OdbcType.Int))
        cmdSaveImage.Parameters.Add(New OdbcParameter("@description", Odbc.OdbcType.VarChar))
        cmdSaveImage.Parameters.Add(New OdbcParameter("@image_path", Odbc.OdbcType.VarChar))
        cmdSaveImage.Parameters.Add(New OdbcParameter("@image_date", Odbc.OdbcType.DateTime))
        cmdSaveImage.Parameters.Add(New OdbcParameter("@image_text2", Odbc.OdbcType.VarChar))
    End Sub

    ''' <summary>
    ''' gets the parcel information
    ''' </summary>
    ''' <param name="ParcelId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetParcelInfo(ByVal ParcelId As String) As ParcelInfo
        Dim pi As ParcelInfo = Nothing

        cmdGetInfo.Parameters(0).Value = ParcelId
        Dim dr As OdbcDataReader = cmdGetInfo.ExecuteReader()
        Dim MaxSeqNo As Integer = 0
        While dr.Read
            ' populate the base parcel data
            If pi Is Nothing Then
                pi = New ParcelInfo(ParcelId, dr.Item("lrsn"), dr.Item("area").ToString.Trim, 0, dr.Item("address").ToString.Trim)
            End If

            If Not dr.IsDBNull(1) Then ' image_seq_number
                ' add the images
                Dim SeqNo As Integer = dr("image_seq_number")
                Dim Description As String = String.Empty
                Dim ImagePath As String = String.Empty
                Dim ImageDate As DateTime = Nothing
                Dim Card As String = String.Empty

                If Not dr.IsDBNull(2) Then
                    Description = dr("image_description").ToString().Trim
                End If
                If Not dr.IsDBNull(3) Then
                    ImagePath = dr("image_path").ToString().Trim.ToLower
                End If
                If Not dr.IsDBNull(4) Then
                    ImageDate = dr("image_date")
                End If
                If Not IsDBNull(5) Then
                    Card = dr("image_text2").ToString().Trim
                End If

                Dim ii As New ImageIndex(SeqNo, Description, ImagePath, ImageDate, Card)
                pi.Images.Add(ii)

                If MaxSeqNo < SeqNo Then
                    MaxSeqNo = SeqNo
                End If
            End If
        End While
        ' update the max seqno for the parcel
        If pi IsNot Nothing Then
            pi.SeqNo = MaxSeqNo
        End If

        dr.Close()

        Return pi
    End Function

    ''' <summary>
    ''' validates the lrsn, card in proval
    ''' </summary>
    ''' <param name="Lrsn"></param>
    ''' <param name="Card"></param>
    ''' <returns>true if lrsn and card exists in proval</returns>
    ''' <remarks></remarks>
    Public Function ValidateCard(ByVal Lrsn As Integer, ByVal Card As String) As Boolean
        Dim Found As Boolean = False

        cmdGetExtension.Parameters(0).Value = Lrsn
        cmdGetExtension.Parameters(1).Value = Card
        Dim dr As OdbcDataReader = cmdGetExtension.ExecuteReader()
        If dr.Read Then
            Found = True
        End If
        dr.Close()

        Return Found
    End Function

    ''' <summary>
    ''' inserts an image_index record in proval
    ''' </summary>
    ''' <param name="Lrsn"></param>
    ''' <param name="Card"></param>
    ''' <param name="SeqNo"></param>
    ''' <param name="Filename">path to image file without pref's imagepath prefix</param>
    ''' <param name="_Date"></param>
    ''' <param name="Description"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' caller must handle errors
    ''' </remarks>
    Public Function AttachToParcel(ByVal Lrsn As Integer, ByVal Card As String, ByVal SeqNo As Integer, ByVal Filename As String, ByVal _Date As DateTime, ByVal Description As String) As Boolean
        Dim Attached As Boolean = False

        cmdSaveImage.Parameters(0).Value = Lrsn
        cmdSaveImage.Parameters(1).Value = SeqNo
        cmdSaveImage.Parameters(2).Value = Description
        cmdSaveImage.Parameters(3).Value = Filename
        cmdSaveImage.Parameters(4).Value = _Date
        cmdSaveImage.Parameters(5).Value = Card

        Attached = cmdSaveImage.ExecuteNonQuery() > 0

        Return Attached
    End Function

    ''' <summary>
    ''' deletes the specified image_index record
    ''' </summary>
    ''' <param name="Lrsn"></param>
    ''' <param name="SeqNo"></param>
    ''' <param name="Filename"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RemoveFromParcel(ByVal Lrsn As Integer, ByVal SeqNo As Integer, ByVal Filename As String) As Boolean
        Dim Deleted As Boolean = False

        cmdDelete.Parameters(0).Value = Lrsn
        cmdDelete.Parameters(1).Value = SeqNo
        cmdDelete.Parameters(2).Value = Filename

        Deleted = cmdDelete.ExecuteNonQuery() > 0
        Return Deleted
    End Function

    ''' <summary>
    ''' update an existing parcel record
    ''' </summary>
    ''' <param name="Lrsn"></param>
    ''' <param name="OldSeqNo"></param>
    ''' <param name="Filename"></param>
    ''' <param name="SeqNo"></param>
    ''' <param name="Description"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateParcel(ByVal Lrsn As Integer, ByVal OldSeqNo As Integer, ByVal Filename As String, ByVal SeqNo As Integer, ByVal Description As Object) As Boolean
        Dim Updated As Boolean = False

        cmdUpdate.Parameters(0).Value = SeqNo
        If IsDBNull(Description) Then
            cmdUpdate.Parameters(1).Value = DBNull.Value
        Else
            cmdUpdate.Parameters(1).Value = CStr(Description)
        End If
        cmdUpdate.Parameters(2).Value = Lrsn
        cmdUpdate.Parameters(3).Value = OldSeqNo
        cmdUpdate.Parameters(4).Value = Filename

        Updated = cmdUpdate.ExecuteNonQuery() > 0
        Return Updated
    End Function

    Public Class ParcelInfo
        Private _parcelid As String = String.Empty
        Private _lrsn As Integer = 0
        Private _area As String = String.Empty
        Private _seqno As Integer = 0
        Private _address As String = String.Empty
        Private _images As New List(Of ImageIndex)

        Public Property ParcelId As String
            Get
                Return _parcelid
            End Get
            Set(ByVal value As String)
                _parcelid = value
            End Set
        End Property

        Public Property Lrsn As Integer
            Get
                Return _lrsn
            End Get
            Set(ByVal value As Integer)
                _lrsn = value
            End Set
        End Property

        Public Property Area As String
            Get
                Return _area
            End Get
            Set(ByVal value As String)
                _area = value
            End Set
        End Property

        Public Property SeqNo As Integer
            Get
                Return _seqno
            End Get
            Set(ByVal value As Integer)
                _seqno = value
            End Set
        End Property

        Public Property Address As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property

        Public ReadOnly Property Images As List(Of ImageIndex)
            Get
                Return _images
            End Get
        End Property

        Public Sub New(ByVal LRSN As Integer, ByVal Area As String, ByVal SeqNo As Integer, ByVal Address As String)
            Me.Lrsn = LRSN
            Me.Area = Area
            Me.SeqNo = SeqNo
            Me.Address = Address
        End Sub

        Public Sub New(ByVal ParcelId As String, ByVal LRSN As Integer, ByVal Area As String, ByVal SeqNo As Integer, ByVal Address As String)
            Me.ParcelId = ParcelId
            Me.Lrsn = LRSN
            Me.Area = Area
            Me.SeqNo = SeqNo
            Me.Address = Address
        End Sub
    End Class

    Public Class ImageIndex
        Private _SeqNo As Integer = 0
        Private _Description As String = String.Empty
        Private _ImagePath As String = String.Empty
        Private _ImageDate As DateTime = Nothing
        Private _Card As String = String.Empty

        Public Property SeqNo As Integer
            Get
                Return _SeqNo
            End Get
            Set(ByVal value As Integer)
                _SeqNo = value
            End Set
        End Property

        Public Property Description As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public Property ImagePath As String
            Get
                Return _ImagePath
            End Get
            Set(ByVal value As String)
                _ImagePath = value
            End Set
        End Property

        Public Property ImageDate As DateTime
            Get
                Return _ImageDate
            End Get
            Set(ByVal value As DateTime)
                _ImageDate = value
            End Set
        End Property

        Public Property Card As String
            Get
                Return _Card
            End Get
            Set(ByVal value As String)
                _Card = value
            End Set
        End Property

        Public Sub New(ByVal SeqNo As Integer, ByVal Description As String, ByVal ImagePath As String, ByVal ImageDate As DateTime, ByVal Card As String)
            Me.SeqNo = SeqNo
            Me.Description = Description
            Me.ImagePath = ImagePath
            Me.ImageDate = ImageDate
            Me.Card = Card
        End Sub
    End Class
End Class
