<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain2))
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition()
        Me.colSource = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.IaDataset1 = New KPB.ProVal.IADataset()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dpImage = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTools = New DevExpress.XtraBars.Bar()
        Me.barMain = New DevExpress.XtraBars.Bar()
        Me.bbiSettings = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiGetImage = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiGetFolder = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiGetParcel = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiAttachImages = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiClear = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiHelp = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiWorksheetMode2 = New DevExpress.XtraBars.BarCheckItem()
        Me.barStatus = New DevExpress.XtraBars.Bar()
        Me.bsiStatus = New DevExpress.XtraBars.BarStaticItem()
        Me.bsiVersion = New DevExpress.XtraBars.BarStaticItem()
        Me.bsiDatabase = New DevExpress.XtraBars.BarStaticItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.AdvBandedGridView1 = New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView()
        Me.gridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.colImage = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.ripeImage = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.GridBand2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.colSeqNo = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.colParcelId = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.riteParcelId = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.colCard = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.riteCard = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.colDescription = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.colLrsn = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.colPropertyAddress = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.colArea = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.colFilename = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.colOriginalFilename = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.colDate = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.colStatus = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.colId = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.colAction = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.riicbAction = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.cmsRow = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MoveTopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveUpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveDownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveBottomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ResequenceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExpandAllGroupsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollapseAllGroupsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cmsGroupRow = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExpandAllGroupsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollapseAllGroupsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.IaDataset1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dpImage.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AdvBandedGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ripeImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riteParcelId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riteCard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riicbAction, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsRow.SuspendLayout()
        Me.cmsGroupRow.SuspendLayout()
        Me.SuspendLayout()
        '
        'colSource
        '
        Me.colSource.FieldName = "Source"
        Me.colSource.Name = "colSource"
        Me.colSource.OptionsColumn.AllowEdit = False
        Me.colSource.OptionsColumn.AllowFocus = False
        Me.colSource.OptionsColumn.ReadOnly = True
        Me.colSource.OptionsColumn.TabStop = False
        '
        'IaDataset1
        '
        Me.IaDataset1.DataSetName = "IADataset"
        Me.IaDataset1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.dpImage})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'dpImage
        '
        Me.dpImage.Controls.Add(Me.DockPanel1_Container)
        Me.dpImage.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right
        Me.dpImage.ID = New System.Guid("24f79b41-632a-4b5b-900d-9323468f478c")
        Me.dpImage.Location = New System.Drawing.Point(666, 53)
        Me.dpImage.Name = "dpImage"
        Me.dpImage.OriginalSize = New System.Drawing.Size(342, 200)
        Me.dpImage.Size = New System.Drawing.Size(342, 651)
        Me.dpImage.Text = "Image"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.PictureEdit1)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(334, 624)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'PictureEdit1
        '
        Me.PictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureEdit1.Location = New System.Drawing.Point(0, 0)
        Me.PictureEdit1.MenuManager = Me.BarManager1
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.ReadOnly = True
        Me.PictureEdit1.Properties.ShowMenu = False
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.PictureEdit1.Size = New System.Drawing.Size(334, 624)
        Me.PictureEdit1.TabIndex = 0
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.barTools, Me.barMain, Me.barStatus})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.DockManager = Me.DockManager1
        Me.BarManager1.Form = Me
        Me.BarManager1.Images = Me.ImageCollection1
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bsiStatus, Me.bsiVersion, Me.bsiDatabase, Me.bbiSettings, Me.bbiGetImage, Me.bbiGetFolder, Me.bbiAttachImages, Me.bbiClear, Me.bbiGetParcel, Me.bbiWorksheetMode2, Me.bbiHelp})
        Me.BarManager1.MainMenu = Me.barMain
        Me.BarManager1.MaxItemId = 12
        Me.BarManager1.StatusBar = Me.barStatus
        '
        'barTools
        '
        Me.barTools.BarName = "Tools"
        Me.barTools.DockCol = 0
        Me.barTools.DockRow = 1
        Me.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.barTools.OptionsBar.Hidden = True
        Me.barTools.Text = "Tools"
        Me.barTools.Visible = False
        '
        'barMain
        '
        Me.barMain.BarName = "Main menu"
        Me.barMain.DockCol = 0
        Me.barMain.DockRow = 0
        Me.barMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.barMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbiSettings), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiGetImage), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiGetFolder), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiGetParcel), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiAttachImages), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiClear), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.bbiHelp, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(Me.bbiWorksheetMode2, True)})
        Me.barMain.OptionsBar.MultiLine = True
        Me.barMain.OptionsBar.UseWholeRow = True
        Me.barMain.Text = "Main menu"
        '
        'bbiSettings
        '
        Me.bbiSettings.Caption = "Settings"
        Me.bbiSettings.Description = "Settings"
        Me.bbiSettings.Id = 3
        Me.bbiSettings.ImageIndex = 0
        Me.bbiSettings.Name = "bbiSettings"
        Me.bbiSettings.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'bbiGetImage
        '
        Me.bbiGetImage.Caption = "Get Image"
        Me.bbiGetImage.Id = 4
        Me.bbiGetImage.ImageIndex = 3
        Me.bbiGetImage.Name = "bbiGetImage"
        Me.bbiGetImage.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'bbiGetFolder
        '
        Me.bbiGetFolder.Caption = "Get Folder"
        Me.bbiGetFolder.Id = 5
        Me.bbiGetFolder.ImageIndex = 4
        Me.bbiGetFolder.Name = "bbiGetFolder"
        Me.bbiGetFolder.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'bbiGetParcel
        '
        Me.bbiGetParcel.Caption = "Get Parcel"
        Me.bbiGetParcel.Id = 9
        Me.bbiGetParcel.ImageIndex = 9
        Me.bbiGetParcel.Name = "bbiGetParcel"
        Me.bbiGetParcel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'bbiAttachImages
        '
        Me.bbiAttachImages.Caption = "Send to ProVal"
        Me.bbiAttachImages.Id = 6
        Me.bbiAttachImages.ImageIndex = 1
        Me.bbiAttachImages.Name = "bbiAttachImages"
        Me.bbiAttachImages.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'bbiClear
        '
        Me.bbiClear.Caption = "Clear"
        Me.bbiClear.Id = 7
        Me.bbiClear.ImageIndex = 2
        Me.bbiClear.Name = "bbiClear"
        Me.bbiClear.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'bbiHelp
        '
        Me.bbiHelp.Caption = "Help"
        Me.bbiHelp.Id = 11
        Me.bbiHelp.Name = "bbiHelp"
        '
        'bbiWorksheetMode2
        '
        Me.bbiWorksheetMode2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.bbiWorksheetMode2.Caption = "Worksheet Mode"
        Me.bbiWorksheetMode2.Id = 10
        Me.bbiWorksheetMode2.ImageIndex = 8
        Me.bbiWorksheetMode2.Name = "bbiWorksheetMode2"
        Me.bbiWorksheetMode2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barStatus
        '
        Me.barStatus.BarName = "Status bar"
        Me.barStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.barStatus.DockCol = 0
        Me.barStatus.DockRow = 0
        Me.barStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.barStatus.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bsiStatus), New DevExpress.XtraBars.LinkPersistInfo(Me.bsiVersion), New DevExpress.XtraBars.LinkPersistInfo(Me.bsiDatabase)})
        Me.barStatus.OptionsBar.AllowQuickCustomization = False
        Me.barStatus.OptionsBar.DrawDragBorder = False
        Me.barStatus.OptionsBar.DrawSizeGrip = True
        Me.barStatus.OptionsBar.UseWholeRow = True
        Me.barStatus.Text = "Status bar"
        '
        'bsiStatus
        '
        Me.bsiStatus.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring
        Me.bsiStatus.Caption = "Ready"
        Me.bsiStatus.Id = 0
        Me.bsiStatus.Name = "bsiStatus"
        Me.bsiStatus.TextAlignment = System.Drawing.StringAlignment.Near
        Me.bsiStatus.Width = 32
        '
        'bsiVersion
        '
        Me.bsiVersion.Appearance.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.bsiVersion.Appearance.Options.UseForeColor = True
        Me.bsiVersion.Caption = "Version"
        Me.bsiVersion.Id = 1
        Me.bsiVersion.Name = "bsiVersion"
        Me.bsiVersion.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'bsiDatabase
        '
        Me.bsiDatabase.Caption = "Database"
        Me.bsiDatabase.Id = 2
        Me.bsiDatabase.Name = "bsiDatabase"
        Me.bsiDatabase.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1008, 53)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 704)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1008, 26)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 53)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 651)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1008, 53)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 651)
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "configure-3.png")
        Me.ImageCollection1.Images.SetKeyName(1, "document-save-4.png")
        Me.ImageCollection1.Images.SetKeyName(2, "edit-clear-locationbar-rtl.png")
        Me.ImageCollection1.Images.SetKeyName(3, "picture.png")
        Me.ImageCollection1.Images.SetKeyName(4, "folder-new-7.png")
        Me.ImageCollection1.Images.SetKeyName(5, "dialog-more.png")
        Me.ImageCollection1.Images.SetKeyName(6, "cross.png")
        Me.ImageCollection1.Images.SetKeyName(7, "arrow-redo.png")
        Me.ImageCollection1.Images.SetKeyName(8, "enumlist.png")
        Me.ImageCollection1.Images.SetKeyName(9, "document-preview.png")
        '
        'GridControl1
        '
        Me.GridControl1.DataMember = "LogSheet"
        Me.GridControl1.DataSource = Me.IaDataset1
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.Location = New System.Drawing.Point(0, 53)
        Me.GridControl1.MainView = Me.AdvBandedGridView1
        Me.GridControl1.MenuManager = Me.BarManager1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ripeImage, Me.riteParcelId, Me.riteCard, Me.riicbAction})
        Me.GridControl1.Size = New System.Drawing.Size(666, 651)
        Me.GridControl1.TabIndex = 4
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.AdvBandedGridView1})
        '
        'AdvBandedGridView1
        '
        Me.AdvBandedGridView1.Appearance.BandPanel.BackColor = System.Drawing.Color.Gray
        Me.AdvBandedGridView1.Appearance.BandPanel.BorderColor = System.Drawing.Color.Gray
        Me.AdvBandedGridView1.Appearance.BandPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.AdvBandedGridView1.Appearance.BandPanel.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.BandPanel.Options.UseBorderColor = True
        Me.AdvBandedGridView1.Appearance.BandPanel.Options.UseFont = True
        Me.AdvBandedGridView1.Appearance.BandPanelBackground.BackColor = System.Drawing.Color.Black
        Me.AdvBandedGridView1.Appearance.BandPanelBackground.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.DarkGray
        Me.AdvBandedGridView1.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.DarkGray
        Me.AdvBandedGridView1.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.DimGray
        Me.AdvBandedGridView1.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.AdvBandedGridView1.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.AdvBandedGridView1.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.DarkGray
        Me.AdvBandedGridView1.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.DarkGray
        Me.AdvBandedGridView1.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Gainsboro
        Me.AdvBandedGridView1.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.AdvBandedGridView1.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.AdvBandedGridView1.Appearance.Empty.BackColor = System.Drawing.SystemColors.Control
        Me.AdvBandedGridView1.Appearance.Empty.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.AdvBandedGridView1.Appearance.Empty.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.White
        Me.AdvBandedGridView1.Appearance.EvenRow.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.Gray
        Me.AdvBandedGridView1.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.Gray
        Me.AdvBandedGridView1.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.AdvBandedGridView1.Appearance.FilterPanel.BackColor = System.Drawing.Color.Gray
        Me.AdvBandedGridView1.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.AdvBandedGridView1.Appearance.FilterPanel.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.FilterPanel.Options.UseForeColor = True
        Me.AdvBandedGridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver
        Me.AdvBandedGridView1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black
        Me.AdvBandedGridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.AdvBandedGridView1.Appearance.FooterPanel.BackColor = System.Drawing.Color.DarkGray
        Me.AdvBandedGridView1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.DarkGray
        Me.AdvBandedGridView1.Appearance.FooterPanel.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.AdvBandedGridView1.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.AdvBandedGridView1.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.AdvBandedGridView1.Appearance.GroupButton.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.GroupButton.Options.UseBorderColor = True
        Me.AdvBandedGridView1.Appearance.GroupFooter.BackColor = System.Drawing.Color.Silver
        Me.AdvBandedGridView1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.Silver
        Me.AdvBandedGridView1.Appearance.GroupFooter.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.AdvBandedGridView1.Appearance.GroupPanel.BackColor = System.Drawing.Color.DimGray
        Me.AdvBandedGridView1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.AdvBandedGridView1.Appearance.GroupPanel.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.GroupPanel.Options.UseForeColor = True
        Me.AdvBandedGridView1.Appearance.GroupRow.BackColor = System.Drawing.Color.DarkGray
        Me.AdvBandedGridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.AdvBandedGridView1.Appearance.GroupRow.ForeColor = System.Drawing.Color.White
        Me.AdvBandedGridView1.Appearance.GroupRow.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.GroupRow.Options.UseFont = True
        Me.AdvBandedGridView1.Appearance.GroupRow.Options.UseForeColor = True
        Me.AdvBandedGridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.DarkGray
        Me.AdvBandedGridView1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.DarkGray
        Me.AdvBandedGridView1.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.AdvBandedGridView1.Appearance.HeaderPanelBackground.BackColor = System.Drawing.Color.Black
        Me.AdvBandedGridView1.Appearance.HeaderPanelBackground.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.LightSlateGray
        Me.AdvBandedGridView1.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.HorzLine.BackColor = System.Drawing.Color.LightGray
        Me.AdvBandedGridView1.Appearance.HorzLine.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.OddRow.BackColor = System.Drawing.Color.WhiteSmoke
        Me.AdvBandedGridView1.Appearance.OddRow.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.Preview.BackColor = System.Drawing.Color.Gainsboro
        Me.AdvBandedGridView1.Appearance.Preview.ForeColor = System.Drawing.Color.DimGray
        Me.AdvBandedGridView1.Appearance.Preview.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.Preview.Options.UseForeColor = True
        Me.AdvBandedGridView1.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.AdvBandedGridView1.Appearance.Row.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.RowSeparator.BackColor = System.Drawing.Color.DimGray
        Me.AdvBandedGridView1.Appearance.RowSeparator.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.DarkGray
        Me.AdvBandedGridView1.Appearance.SelectedRow.Options.UseBackColor = True
        Me.AdvBandedGridView1.Appearance.VertLine.BackColor = System.Drawing.Color.LightGray
        Me.AdvBandedGridView1.Appearance.VertLine.Options.UseBackColor = True
        Me.AdvBandedGridView1.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gridBand1, Me.GridBand2})
        Me.AdvBandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.colParcelId, Me.colCard, Me.colSeqNo, Me.colPropertyAddress, Me.colFilename, Me.colOriginalFilename, Me.colStatus, Me.colLrsn, Me.colArea, Me.colDate, Me.colDescription, Me.colSource, Me.colImage, Me.colAction, Me.colId})
        StyleFormatCondition1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic)
        StyleFormatCondition1.Appearance.Options.UseFont = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Column = Me.colSource
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = "table"
        Me.AdvBandedGridView1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.AdvBandedGridView1.GridControl = Me.GridControl1
        Me.AdvBandedGridView1.GroupCount = 1
        Me.AdvBandedGridView1.Name = "AdvBandedGridView1"
        Me.AdvBandedGridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.AdvBandedGridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.AdvBandedGridView1.OptionsCustomization.AllowFilter = False
        Me.AdvBandedGridView1.OptionsCustomization.AllowGroup = False
        Me.AdvBandedGridView1.OptionsCustomization.AllowQuickHideColumns = False
        Me.AdvBandedGridView1.OptionsCustomization.AllowSort = False
        Me.AdvBandedGridView1.OptionsMenu.EnableColumnMenu = False
        Me.AdvBandedGridView1.OptionsMenu.EnableFooterMenu = False
        Me.AdvBandedGridView1.OptionsMenu.EnableGroupPanelMenu = False
        Me.AdvBandedGridView1.OptionsSelection.MultiSelect = True
        Me.AdvBandedGridView1.OptionsView.ColumnAutoWidth = True
        Me.AdvBandedGridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.AdvBandedGridView1.OptionsView.EnableAppearanceOddRow = True
        Me.AdvBandedGridView1.OptionsView.ShowBands = False
        Me.AdvBandedGridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.AdvBandedGridView1.OptionsView.ShowGroupPanel = False
        Me.AdvBandedGridView1.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colParcelId, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colSeqNo, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'gridBand1
        '
        Me.gridBand1.Caption = "Image"
        Me.gridBand1.Columns.Add(Me.colImage)
        Me.gridBand1.MinWidth = 20
        Me.gridBand1.Name = "gridBand1"
        Me.gridBand1.Width = 100
        '
        'colImage
        '
        Me.colImage.AppearanceCell.Options.UseTextOptions = True
        Me.colImage.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colImage.AppearanceHeader.Options.UseTextOptions = True
        Me.colImage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colImage.AutoFillDown = True
        Me.colImage.Caption = "Image"
        Me.colImage.ColumnEdit = Me.ripeImage
        Me.colImage.FieldName = "colImage"
        Me.colImage.Name = "colImage"
        Me.colImage.OptionsColumn.AllowEdit = False
        Me.colImage.OptionsColumn.AllowFocus = False
        Me.colImage.OptionsColumn.FixedWidth = True
        Me.colImage.OptionsColumn.ReadOnly = True
        Me.colImage.OptionsColumn.TabStop = False
        Me.colImage.RowCount = 3
        Me.colImage.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        Me.colImage.Visible = True
        Me.colImage.Width = 100
        '
        'ripeImage
        '
        Me.ripeImage.Name = "ripeImage"
        Me.ripeImage.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        '
        'GridBand2
        '
        Me.GridBand2.Caption = "Image Details"
        Me.GridBand2.Columns.Add(Me.colSeqNo)
        Me.GridBand2.Columns.Add(Me.colParcelId)
        Me.GridBand2.Columns.Add(Me.colCard)
        Me.GridBand2.Columns.Add(Me.colDescription)
        Me.GridBand2.Columns.Add(Me.colLrsn)
        Me.GridBand2.Columns.Add(Me.colSource)
        Me.GridBand2.Columns.Add(Me.colPropertyAddress)
        Me.GridBand2.Columns.Add(Me.colArea)
        Me.GridBand2.Columns.Add(Me.colFilename)
        Me.GridBand2.Columns.Add(Me.colOriginalFilename)
        Me.GridBand2.Columns.Add(Me.colDate)
        Me.GridBand2.Columns.Add(Me.colStatus)
        Me.GridBand2.Columns.Add(Me.colId)
        Me.GridBand2.Columns.Add(Me.colAction)
        Me.GridBand2.MinWidth = 20
        Me.GridBand2.Name = "GridBand2"
        Me.GridBand2.Width = 550
        '
        'colSeqNo
        '
        Me.colSeqNo.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.colSeqNo.AppearanceCell.Options.UseFont = True
        Me.colSeqNo.FieldName = "SeqNo"
        Me.colSeqNo.Name = "colSeqNo"
        Me.colSeqNo.OptionsColumn.AllowEdit = False
        Me.colSeqNo.OptionsColumn.AllowFocus = False
        Me.colSeqNo.OptionsColumn.FixedWidth = True
        Me.colSeqNo.OptionsColumn.ReadOnly = True
        Me.colSeqNo.OptionsColumn.TabStop = False
        Me.colSeqNo.Visible = True
        '
        'colParcelId
        '
        Me.colParcelId.ColumnEdit = Me.riteParcelId
        Me.colParcelId.FieldName = "ParcelId"
        Me.colParcelId.Name = "colParcelId"
        Me.colParcelId.OptionsColumn.FixedWidth = True
        Me.colParcelId.Visible = True
        Me.colParcelId.Width = 120
        '
        'riteParcelId
        '
        Me.riteParcelId.AutoHeight = False
        Me.riteParcelId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.riteParcelId.MaxLength = 26
        Me.riteParcelId.Name = "riteParcelId"
        '
        'colCard
        '
        Me.colCard.ColumnEdit = Me.riteCard
        Me.colCard.FieldName = "Card"
        Me.colCard.Name = "colCard"
        Me.colCard.OptionsColumn.FixedWidth = True
        Me.colCard.Visible = True
        '
        'riteCard
        '
        Me.riteCard.AutoHeight = False
        Me.riteCard.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.riteCard.MaxLength = 3
        Me.riteCard.Name = "riteCard"
        '
        'colDescription
        '
        Me.colDescription.FieldName = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.Visible = True
        Me.colDescription.Width = 280
        '
        'colLrsn
        '
        Me.colLrsn.FieldName = "Lrsn"
        Me.colLrsn.Name = "colLrsn"
        Me.colLrsn.OptionsColumn.AllowEdit = False
        Me.colLrsn.OptionsColumn.AllowFocus = False
        Me.colLrsn.OptionsColumn.ReadOnly = True
        Me.colLrsn.OptionsColumn.TabStop = False
        '
        'colPropertyAddress
        '
        Me.colPropertyAddress.FieldName = "PropertyAddress"
        Me.colPropertyAddress.Name = "colPropertyAddress"
        Me.colPropertyAddress.OptionsColumn.AllowEdit = False
        Me.colPropertyAddress.OptionsColumn.AllowFocus = False
        Me.colPropertyAddress.OptionsColumn.ReadOnly = True
        Me.colPropertyAddress.OptionsColumn.TabStop = False
        Me.colPropertyAddress.RowIndex = 1
        Me.colPropertyAddress.Visible = True
        Me.colPropertyAddress.Width = 475
        '
        'colArea
        '
        Me.colArea.FieldName = "Area"
        Me.colArea.Name = "colArea"
        Me.colArea.OptionsColumn.AllowEdit = False
        Me.colArea.OptionsColumn.AllowFocus = False
        Me.colArea.OptionsColumn.FixedWidth = True
        Me.colArea.OptionsColumn.ReadOnly = True
        Me.colArea.OptionsColumn.TabStop = False
        Me.colArea.RowIndex = 1
        Me.colArea.Visible = True
        '
        'colFilename
        '
        Me.colFilename.FieldName = "Filename"
        Me.colFilename.Name = "colFilename"
        Me.colFilename.OptionsColumn.AllowEdit = False
        Me.colFilename.OptionsColumn.AllowFocus = False
        Me.colFilename.OptionsColumn.ReadOnly = True
        Me.colFilename.OptionsColumn.TabStop = False
        Me.colFilename.RowIndex = 2
        Me.colFilename.Visible = True
        Me.colFilename.Width = 228
        '
        'colOriginalFilename
        '
        Me.colOriginalFilename.FieldName = "OriginalFilename"
        Me.colOriginalFilename.Name = "colOriginalFilename"
        Me.colOriginalFilename.OptionsColumn.AllowEdit = False
        Me.colOriginalFilename.OptionsColumn.AllowFocus = False
        Me.colOriginalFilename.OptionsColumn.ReadOnly = True
        Me.colOriginalFilename.OptionsColumn.TabStop = False
        Me.colOriginalFilename.RowIndex = 2
        Me.colOriginalFilename.Visible = True
        Me.colOriginalFilename.Width = 172
        '
        'colDate
        '
        Me.colDate.DisplayFormat.FormatString = "d"
        Me.colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colDate.FieldName = "Date"
        Me.colDate.Name = "colDate"
        Me.colDate.OptionsColumn.AllowEdit = False
        Me.colDate.OptionsColumn.AllowFocus = False
        Me.colDate.OptionsColumn.FixedWidth = True
        Me.colDate.OptionsColumn.ReadOnly = True
        Me.colDate.OptionsColumn.TabStop = False
        Me.colDate.RowIndex = 2
        Me.colDate.Visible = True
        Me.colDate.Width = 150
        '
        'colStatus
        '
        Me.colStatus.FieldName = "Status"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.OptionsColumn.AllowEdit = False
        Me.colStatus.OptionsColumn.AllowFocus = False
        Me.colStatus.OptionsColumn.ReadOnly = True
        Me.colStatus.OptionsColumn.TabStop = False
        Me.colStatus.RowIndex = 3
        Me.colStatus.Visible = True
        Me.colStatus.Width = 400
        '
        'colId
        '
        Me.colId.FieldName = "Id"
        Me.colId.Name = "colId"
        Me.colId.OptionsColumn.AllowEdit = False
        Me.colId.OptionsColumn.AllowFocus = False
        Me.colId.OptionsColumn.FixedWidth = True
        Me.colId.OptionsColumn.ReadOnly = True
        Me.colId.OptionsColumn.TabStop = False
        Me.colId.RowIndex = 3
        '
        'colAction
        '
        Me.colAction.Caption = "Action"
        Me.colAction.ColumnEdit = Me.riicbAction
        Me.colAction.FieldName = "Action"
        Me.colAction.Name = "colAction"
        Me.colAction.OptionsColumn.AllowEdit = False
        Me.colAction.OptionsColumn.AllowFocus = False
        Me.colAction.OptionsColumn.FixedWidth = True
        Me.colAction.OptionsColumn.ReadOnly = True
        Me.colAction.RowIndex = 3
        Me.colAction.Visible = True
        Me.colAction.Width = 150
        '
        'riicbAction
        '
        Me.riicbAction.AutoHeight = False
        Me.riicbAction.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riicbAction.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("None", "None", -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Insert", "Insert", 5), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Update", "Update", 7), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Delete", "Delete", 6)})
        Me.riicbAction.Name = "riicbAction"
        Me.riicbAction.SmallImages = Me.ImageCollection1
        '
        'cmsRow
        '
        Me.cmsRow.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MoveTopToolStripMenuItem, Me.MoveUpToolStripMenuItem, Me.MoveDownToolStripMenuItem, Me.MoveBottomToolStripMenuItem, Me.ToolStripMenuItem1, Me.DeleteToolStripMenuItem, Me.ToolStripMenuItem2, Me.ResequenceToolStripMenuItem, Me.RefreshToolStripMenuItem, Me.ToolStripMenuItem3, Me.ExpandAllGroupsToolStripMenuItem, Me.CollapseAllGroupsToolStripMenuItem})
        Me.cmsRow.Name = "cmsRow"
        Me.cmsRow.Size = New System.Drawing.Size(178, 220)
        '
        'MoveTopToolStripMenuItem
        '
        Me.MoveTopToolStripMenuItem.Enabled = False
        Me.MoveTopToolStripMenuItem.Image = CType(resources.GetObject("MoveTopToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MoveTopToolStripMenuItem.Name = "MoveTopToolStripMenuItem"
        Me.MoveTopToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.MoveTopToolStripMenuItem.Text = "Move Top"
        '
        'MoveUpToolStripMenuItem
        '
        Me.MoveUpToolStripMenuItem.Enabled = False
        Me.MoveUpToolStripMenuItem.Image = CType(resources.GetObject("MoveUpToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MoveUpToolStripMenuItem.Name = "MoveUpToolStripMenuItem"
        Me.MoveUpToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.MoveUpToolStripMenuItem.Text = "Move Up"
        '
        'MoveDownToolStripMenuItem
        '
        Me.MoveDownToolStripMenuItem.Enabled = False
        Me.MoveDownToolStripMenuItem.Image = CType(resources.GetObject("MoveDownToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MoveDownToolStripMenuItem.Name = "MoveDownToolStripMenuItem"
        Me.MoveDownToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.MoveDownToolStripMenuItem.Text = "Move Down"
        '
        'MoveBottomToolStripMenuItem
        '
        Me.MoveBottomToolStripMenuItem.Enabled = False
        Me.MoveBottomToolStripMenuItem.Image = CType(resources.GetObject("MoveBottomToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MoveBottomToolStripMenuItem.Name = "MoveBottomToolStripMenuItem"
        Me.MoveBottomToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.MoveBottomToolStripMenuItem.Text = "Move Bottom"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(174, 6)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = CType(resources.GetObject("DeleteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(174, 6)
        '
        'ResequenceToolStripMenuItem
        '
        Me.ResequenceToolStripMenuItem.Image = CType(resources.GetObject("ResequenceToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ResequenceToolStripMenuItem.Name = "ResequenceToolStripMenuItem"
        Me.ResequenceToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ResequenceToolStripMenuItem.Text = "Resequence"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Image = CType(resources.GetObject("RefreshToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(174, 6)
        '
        'ExpandAllGroupsToolStripMenuItem
        '
        Me.ExpandAllGroupsToolStripMenuItem.Enabled = False
        Me.ExpandAllGroupsToolStripMenuItem.Image = CType(resources.GetObject("ExpandAllGroupsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ExpandAllGroupsToolStripMenuItem.Name = "ExpandAllGroupsToolStripMenuItem"
        Me.ExpandAllGroupsToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ExpandAllGroupsToolStripMenuItem.Text = "Expand All Groups"
        '
        'CollapseAllGroupsToolStripMenuItem
        '
        Me.CollapseAllGroupsToolStripMenuItem.Enabled = False
        Me.CollapseAllGroupsToolStripMenuItem.Image = CType(resources.GetObject("CollapseAllGroupsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CollapseAllGroupsToolStripMenuItem.Name = "CollapseAllGroupsToolStripMenuItem"
        Me.CollapseAllGroupsToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.CollapseAllGroupsToolStripMenuItem.Text = "Collapse All Groups"
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Select Images  to Load"
        Me.FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "jpg"
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "JPEG files|*.jpg|All files|*.*"
        Me.OpenFileDialog1.Title = "Select one or more images to load"
        '
        'Timer1
        '
        Me.Timer1.Interval = 250
        '
        'cmsGroupRow
        '
        Me.cmsGroupRow.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExpandAllGroupsToolStripMenuItem1, Me.CollapseAllGroupsToolStripMenuItem1})
        Me.cmsGroupRow.Name = "cmsGroupRow"
        Me.cmsGroupRow.Size = New System.Drawing.Size(178, 48)
        '
        'ExpandAllGroupsToolStripMenuItem1
        '
        Me.ExpandAllGroupsToolStripMenuItem1.Image = CType(resources.GetObject("ExpandAllGroupsToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.ExpandAllGroupsToolStripMenuItem1.Name = "ExpandAllGroupsToolStripMenuItem1"
        Me.ExpandAllGroupsToolStripMenuItem1.Size = New System.Drawing.Size(177, 22)
        Me.ExpandAllGroupsToolStripMenuItem1.Text = "Expand All Groups"
        '
        'CollapseAllGroupsToolStripMenuItem1
        '
        Me.CollapseAllGroupsToolStripMenuItem1.Image = CType(resources.GetObject("CollapseAllGroupsToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.CollapseAllGroupsToolStripMenuItem1.Name = "CollapseAllGroupsToolStripMenuItem1"
        Me.CollapseAllGroupsToolStripMenuItem1.Size = New System.Drawing.Size(177, 22)
        Me.CollapseAllGroupsToolStripMenuItem1.Text = "Collapse All Groups"
        '
        'frmMain2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 730)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.dpImage)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[Application Name]"
        CType(Me.IaDataset1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dpImage.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AdvBandedGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ripeImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riteParcelId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riteCard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riicbAction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsRow.ResumeLayout(False)
        Me.cmsGroupRow.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IaDataset1 As KPB.ProVal.IADataset
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents AdvBandedGridView1 As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
    Friend WithEvents colImage As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents ripeImage As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents colParcelId As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents riteParcelId As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents colCard As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents riteCard As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents colSeqNo As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents colDescription As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents colLrsn As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents colSource As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents colPropertyAddress As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents colArea As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents colFilename As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents colOriginalFilename As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents colDate As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents colStatus As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barTools As DevExpress.XtraBars.Bar
    Friend WithEvents barMain As DevExpress.XtraBars.Bar
    Friend WithEvents bbiSettings As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiGetImage As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiGetFolder As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiAttachImages As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiClear As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barStatus As DevExpress.XtraBars.Bar
    Friend WithEvents bsiStatus As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents bsiVersion As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents bsiDatabase As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents dpImage As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents gridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents colAction As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents cmsRow As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MoveTopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveUpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveDownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveBottomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents colId As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ResequenceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents bbiGetParcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiWorksheetMode2 As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents ExpandAllGroupsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseAllGroupsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsGroupRow As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExpandAllGroupsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseAllGroupsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents riicbAction As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents bbiHelp As DevExpress.XtraBars.BarButtonItem
End Class
