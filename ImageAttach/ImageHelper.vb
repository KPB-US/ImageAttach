Imports System.Text

''' <summary>
''' helper for keeping track of image prefs and method for determining path
''' </summary>
''' <remarks></remarks>
Public Class ImageHelper
#Region "properties..."
    Private _ImagePath As String = String.Empty
    Private _Digits As Integer = 2
    Private _ID As IdEnum = IdEnum.ParcelId
    Private _IgnoreDelim As Boolean = True
    Private _NumSections As Integer = 1
    Private _Sections As New List(Of Section)

    Public Property ImagePath As String
        Get
            Return _ImagePath
        End Get
        Set(ByVal value As String)
            If value.EndsWith("\") Then
                _ImagePath = value.Substring(0, value.Length - 1)
            Else
                _ImagePath = value
            End If
        End Set
    End Property

    Public Property Digits As Integer
        Get
            Return _Digits
        End Get
        Set(ByVal value As Integer)
            _Digits = value
        End Set
    End Property

    Public Enum IdEnum As Integer
        ParcelId = 0
        AlternateID = 1
    End Enum

    Public Property ID As IdEnum
        Get
            Return _ID
        End Get
        Set(ByVal value As IdEnum)
            _ID = value
        End Set
    End Property

    Public Property IgnoreDelim As Boolean
        Get
            Return _IgnoreDelim
        End Get
        Set(ByVal value As Boolean)
            _IgnoreDelim = value
        End Set
    End Property

    Public Property NumSections As Integer
        Get
            Return _NumSections
        End Get
        Set(ByVal value As Integer)
            _NumSections = value
        End Set
    End Property

    Public ReadOnly Property Sections As List(Of Section)
        Get
            Return _Sections
        End Get
    End Property

    Public Class Section
        Private _Start As Integer = 0
        Private _Length As Integer = 0

        Public Property Start As Integer
            Get
                Return _Start
            End Get
            Set(ByVal value As Integer)
                _Start = value
            End Set
        End Property

        Public Property Length As Integer
            Get
                Return _Length
            End Get
            Set(ByVal value As Integer)
                _Length = value
            End Set
        End Property

        Public Sub New(ByVal Start As Integer, ByVal Length As Integer)
            Me.Start = Start
            Me.Length = Length
        End Sub
    End Class
#End Region

    Const SingleDigitMap As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"

    ''' <summary>
    ''' load the preferences 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Dim prefs As New PVAUTOLib.PVPreferences
        Me.ImagePath = prefs.ImagePath.ToLower
        Me.Digits = Integer.Parse(prefs.Option("SYSTEM", "Images", "Digits", "2").Trim()) + 1
        Me.ID = IIf(prefs.Option("SYSTEM", "Images", "ID", "0").Trim() = "0", ImageHelper.IdEnum.ParcelId, ImageHelper.IdEnum.AlternateID)
        Me.IgnoreDelim = (prefs.Option("SYSTEM", "Images", "IgnoreDelim", "Y").Trim() = "Y")
        Me.NumSections = Integer.Parse(prefs.Option("SYSTEM", "Images", "NumSections", "1").Trim())

        ' load the sections
        For i = 0 To Me.NumSections - 1
            Me.Sections.Add(New ImageHelper.Section( _
                  Integer.Parse(prefs.Option("SYSTEM", "Images", String.Format("Start({0})", i), "0").Trim()), _
                  Integer.Parse(prefs.Option("SYSTEM", "Images", String.Format("Digits({0})", i), "0").Trim())) _
                 )
        Next
    End Sub

    ''' <summary>
    ''' returns the path for the image
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="Area"></param>
    ''' <param name="Card"></param>
    ''' <param name="SeqNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Path(ByVal OriginalPath As String, ByVal Id As String, ByVal Area As String, ByVal Card As String, ByVal SeqNo As Integer) As String
        Dim ImagePath As String = String.Empty

        ' make sure the required items are present
        If Id.Trim.Length > 0 AndAlso Area.Trim.Length > 0 AndAlso Card.Trim.Length > 0 AndAlso SeqNo > 0 Then
            ImagePath = System.IO.Path.Combine(Me.ImagePath, Area) & "\"

            ' strip spaces and maybe delimiters
            If Me.IgnoreDelim Then
                Id = Id.Replace("-", "")
            End If
            Id = Id.Replace(" ", "")

            If Me.Digits = 1 Then
                Dim num As Integer = Integer.Parse(Card.Substring(1, 2))
                ' if num > SingleDigitMap.Length then this will blow up so caller should catch and handle nicely
                Card = Card.Substring(0, 1) & SingleDigitMap.Substring(num, 1)
            End If

            ' add sections (except last one)
            ' use start -1 since function is zero based
            For j As Integer = 0 To NumSections - 2
                ImagePath &= Id.Substring(Sections(j).Start - 1, Sections(j).Length) & "\"
            Next
            ' add last section as basename
            ImagePath &= Id.Substring(Sections(NumSections - 1).Start - 1, Sections(NumSections - 1).Length)

            ' add card/extension and seqno and extension - save routine saves as jpg format
            ImagePath += Card & SeqNo.ToString() & ".jpg" 'System.IO.Path.GetExtension(OriginalPath)
        End If

        Return ImagePath
    End Function

    ''' <summary>
    ''' returns the settings
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Dim Msg As New StringBuilder

        Msg.AppendLine("Image Path: " & Me.ImagePath)
        Msg.AppendLine("ID: " & IIf(Me.ID = ImageHelper.IdEnum.ParcelId, "ParcelId", "AlternateId"))
        Msg.AppendLine("Ignore Delimiters: " & Me.IgnoreDelim.ToString)
        Msg.AppendLine("# Digits for Card #: " & Me.Digits)
        Msg.AppendLine("# Sections: " & Me.NumSections)
        For i As Integer = 0 To Me.NumSections - 1
            Msg.AppendLine("  Section " & i + 1 & " - " & Me.Sections(i).Length & " digits starting at position " & Me.Sections(i).Start)
        Next

        Return Msg.ToString
    End Function
End Class