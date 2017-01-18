Imports System.Text
Imports System.Runtime.InteropServices

Module StartUp

    <DllImport("PVSec32.dll", CharSet:=CharSet.Auto)> _
    Private Sub ConnInfo(<MarshalAs(UnmanagedType.LPStr)> ByVal DSN As StringBuilder, ByVal LenDSN As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal DBUID As StringBuilder, ByVal LenUID As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal DBPW As StringBuilder, ByVal LenPW As Integer, ByRef _DBType As Integer)
    End Sub

    ''' <summary>
    ''' authorize the user before running the main window
    ''' </summary>
    ''' <remarks>
    ''' This also obtains the database connection string from the pvsec com object (if the login was successful) and passes
    ''' that to the window so it can use it.
    ''' </remarks>
    Public Sub Main()
        Dim DbConnString As String = String.Empty
        Dim CommonDataSource As String = String.Empty
        If Not AuthorizeUser(DbConnString, CommonDataSource) Then
            MessageBox.Show("You do not have permissions to Batch Attach Images.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim f As New frmMain2()
            f.DbConnString = DbConnString
            f.CommonDataSource = CommonDataSource
            Application.Run(f)
        End If
    End Sub

    ''' <summary>
    ''' use ProVal's security routines to authorize the user for Batch Attaching Images
    ''' </summary>
    ''' <param name="DbConnString">returns the database connection string if successful</param>
    ''' <returns>true if authenticated and authorized (and it populates the DbConnString arg) otherwise false </returns>
    ''' <remarks>
    ''' Uses the security permission (AppFunction) for ProVal's native batch attach image process.
    ''' This also extracts the database connection information and returns it, if the login is successful.
    ''' </remarks>
    Private Function AuthorizeUser(ByRef DbConnString As String, ByRef CommonDataSource As String) As Boolean
        Const BatchAttachImages As Integer = 5072

        Dim a As New PVAUTOLib.PVPreferencesClass
        Dim pVal As Integer = 0
        Dim Authorized As Boolean = False

        a.Login("Proval Plus", "", "", pVal)
        If pVal <> 0 Then
            Dim View As Integer = 0
            Dim Create As Integer = 0
            Dim Update As Integer = 0
            Dim Delete As Integer = 0
            Dim Execute As Integer = 0
            Dim ErrNo As Integer = 0
            Dim rc As Integer = 0
            Dim ErrDesc As String = ""

            a.ValidateFunction(BatchAttachImages, View, Create, Update, Delete, Execute, ErrNo, ErrDesc, rc)
            Authorized = (Update = 1)

            If Authorized Then
                Dim b As New PVAUTOLib.PVSecurityClass

                Dim dSN As New StringBuilder(40)
                Dim dBUID As New StringBuilder(40)
                Dim dBPW As New StringBuilder(40)
                Dim lenDSN As Integer = &H29
                Dim lenUID As Integer = &H29
                Dim lenPW As Integer = &H29
                Dim num As Integer = 0

                'Me.typeDB = class2.DatabaseType.ToString
                'Me.CommonDSN = class2.CommonDataSource.ToString
                'Me.sDSN = class2.CommonDataSource.ToString

                ConnInfo(dSN, lenDSN, dBUID, lenUID, dBPW, lenPW, num)
                DbConnString = String.Concat(New String() {"DSN=", dSN.ToString.Trim, ";Uid=", dBUID.ToString.Trim, ";Pwd=", dBPW.ToString.Trim})
                CommonDataSource = a.CommonDataSource
            End If
        End If

        Return Authorized
    End Function

End Module
