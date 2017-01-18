Public Class frmSettings
    Private _ListHasItems As Boolean = False
    Private _AttachToProValSettingChanged As Boolean = False

    ''' <summary>
    ''' caller tells us if there are items in the list
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' This determines if we prompt them when they change the attach proval setting
    ''' </remarks>
    Public WriteOnly Property ListHasItems As Boolean
        Set(ByVal value As Boolean)
            _ListHasItems = value
        End Set
    End Property

    ''' <summary>
    ''' caller tells us what the existing proval image batch setting are
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ProValSettings As String
        Set(ByVal value As String)
            txtProValSettings.Text = value
        End Set
    End Property

    ''' <summary>
    ''' indicate that this setting has changed so the list can be cleared if needed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AttachToProValSettingChanged As Boolean
        Get
            Return _AttachToProValSettingChanged
        End Get
    End Property

    ''' <summary>
    ''' load defaults
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtWidth.Text = My.Settings.ResizeWidth
        txtHeight.Text = My.Settings.ResizeHeight
        chkAttachImages.Checked = My.Settings.AttachToProVal
        chkAllowFileOverwrite.Checked = My.Settings.AllowFileOverwrite
        chkProValVersion9.Checked = My.Settings.ProValVersion9
        _AttachToProValSettingChanged = False
        ' 01/19/12 mjf add this new setting for boone county since they save their photos to L:\Images\YYMM\PIN.jpg
        chkOverrideAreaWith.Checked = (My.Settings.OverrideAreaWith = "YYMM")
    End Sub

    ''' <summary>
    ''' save settings
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If My.Settings.AttachToProVal <> chkAttachImages.Checked Then
            If _ListHasItems Then
                If MessageBox.Show("Changing the Link to ProVal setting will cause the list to be cleared.  Do you want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            _AttachToProValSettingChanged = True
        End If
        Try
            My.Settings.ResizeWidth = Integer.Parse(txtWidth.Text)
            My.Settings.ResizeHeight = Integer.Parse(txtHeight.Text)
            My.Settings.AttachToProVal = chkAttachImages.Checked
            My.Settings.AllowFileOverwrite = chkAllowFileOverwrite.Checked
            My.Settings.ProValVersion9 = chkProValVersion9.Checked
            ' 01/19/12 mjf add this new setting for boone county since they save their photos to L:\Images\YYMM\PIN.jpg
            My.Settings.OverrideAreaWith = IIf(chkOverrideAreaWith.Checked, "YYMM", "")
            My.Settings.Save()
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Some of the settings are not valid.  Please check them.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
End Class