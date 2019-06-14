<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtProValSettings = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkOverrideAreaWith = New System.Windows.Forms.CheckBox()
        Me.chkAllowFileOverwrite = New System.Windows.Forms.CheckBox()
        Me.chkAttachImages = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtHeight = New System.Windows.Forms.TextBox()
        Me.txtWidth = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblProValVersion = New System.Windows.Forms.Label()
        Me.txtProValVersion = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(162, 403)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(244, 403)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtProValSettings)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(307, 216)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ProVal Image Settings"
        '
        'txtProValSettings
        '
        Me.txtProValSettings.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProValSettings.Location = New System.Drawing.Point(6, 19)
        Me.txtProValSettings.Multiline = True
        Me.txtProValSettings.Name = "txtProValSettings"
        Me.txtProValSettings.ReadOnly = True
        Me.txtProValSettings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtProValSettings.Size = New System.Drawing.Size(295, 191)
        Me.txtProValSettings.TabIndex = 0
        Me.txtProValSettings.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtProValSettings, "These settings must be changed via ProVal's Prefs application.")
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.txtProValVersion)
        Me.GroupBox2.Controls.Add(Me.lblProValVersion)
        Me.GroupBox2.Controls.Add(Me.chkOverrideAreaWith)
        Me.GroupBox2.Controls.Add(Me.chkAllowFileOverwrite)
        Me.GroupBox2.Controls.Add(Me.chkAttachImages)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtHeight)
        Me.GroupBox2.Controls.Add(Me.txtWidth)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 236)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(307, 161)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Image Attach Settings"
        '
        'chkOverrideAreaWith
        '
        Me.chkOverrideAreaWith.AutoSize = True
        Me.chkOverrideAreaWith.Location = New System.Drawing.Point(9, 105)
        Me.chkOverrideAreaWith.Name = "chkOverrideAreaWith"
        Me.chkOverrideAreaWith.Size = New System.Drawing.Size(195, 17)
        Me.chkOverrideAreaWith.TabIndex = 7
        Me.chkOverrideAreaWith.Text = "Override Area with YYMM of picture"
        Me.ToolTip1.SetToolTip(Me.chkOverrideAreaWith, "If checked, this will write out the  year and month of the picture instead of the" &
        " area of the parcel")
        Me.chkOverrideAreaWith.UseVisualStyleBackColor = True
        '
        'chkAllowFileOverwrite
        '
        Me.chkAllowFileOverwrite.AutoSize = True
        Me.chkAllowFileOverwrite.Location = New System.Drawing.Point(9, 81)
        Me.chkAllowFileOverwrite.Name = "chkAllowFileOverwrite"
        Me.chkAllowFileOverwrite.Size = New System.Drawing.Size(172, 17)
        Me.chkAllowFileOverwrite.TabIndex = 6
        Me.chkAllowFileOverwrite.Text = "Allow Images to be Overwritten"
        Me.ToolTip1.SetToolTip(Me.chkAllowFileOverwrite, "If checked, this will not ensure unique filenames and will not warn you when exis" &
        "ting image files are about to be overwritten")
        Me.chkAllowFileOverwrite.UseVisualStyleBackColor = True
        '
        'chkAttachImages
        '
        Me.chkAttachImages.AutoSize = True
        Me.chkAttachImages.Location = New System.Drawing.Point(9, 58)
        Me.chkAttachImages.Name = "chkAttachImages"
        Me.chkAttachImages.Size = New System.Drawing.Size(204, 17)
        Me.chkAttachImages.TabIndex = 5
        Me.chkAttachImages.Text = "Attach (link) Image to Parcel in ProVal"
        Me.ToolTip1.SetToolTip(Me.chkAttachImages, "If this is unchecked, images will only be moved to their ProVal folders, and you " &
        "will still need to run the AddImage.exe from ProVal under Utilities menu.")
        Me.chkAttachImages.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(212, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "h (px)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(101, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "w (px)"
        '
        'txtHeight
        '
        Me.txtHeight.Location = New System.Drawing.Point(146, 32)
        Me.txtHeight.MaxLength = 4
        Me.txtHeight.Name = "txtHeight"
        Me.txtHeight.Size = New System.Drawing.Size(60, 20)
        Me.txtHeight.TabIndex = 2
        '
        'txtWidth
        '
        Me.txtWidth.Location = New System.Drawing.Point(35, 32)
        Me.txtWidth.MaxLength = 4
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.Size = New System.Drawing.Size(60, 20)
        Me.txtWidth.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Resize Images:"
        '
        'lblProValVersion
        '
        Me.lblProValVersion.AutoSize = True
        Me.lblProValVersion.Location = New System.Drawing.Point(9, 129)
        Me.lblProValVersion.Name = "lblProValVersion"
        Me.lblProValVersion.Size = New System.Drawing.Size(123, 13)
        Me.lblProValVersion.TabIndex = 8
        Me.lblProValVersion.Text = "ProVal Version (ie: 9.1.5)"
        '
        'txtProValVersion
        '
        Me.txtProValVersion.Location = New System.Drawing.Point(138, 126)
        Me.txtProValVersion.Name = "txtProValVersion"
        Me.txtProValVersion.Size = New System.Drawing.Size(100, 20)
        Me.txtProValVersion.TabIndex = 9
        '
        'frmSettings
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(331, 435)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmSettings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtProValSettings As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkAttachImages As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtHeight As System.Windows.Forms.TextBox
    Friend WithEvents txtWidth As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkAllowFileOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverrideAreaWith As System.Windows.Forms.CheckBox
    Friend WithEvents txtProValVersion As TextBox
    Friend WithEvents lblProValVersion As Label
End Class
