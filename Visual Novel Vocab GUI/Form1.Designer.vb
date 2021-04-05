<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.tbxSearchBox = New System.Windows.Forms.TextBox()
        Me.cbbSearchType = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lbResults = New System.Windows.Forms.ListBox()
        Me.nbPageStart = New System.Windows.Forms.NumericUpDown()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.nbPageEnd = New System.Windows.Forms.NumericUpDown()
        Me.pbProgress = New System.Windows.Forms.ProgressBar()
        Me.cbbFilterType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbbMediaType = New System.Windows.Forms.ComboBox()
        Me.lblResultCount = New System.Windows.Forms.Label()
        Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel()
        Me.lbOutput = New System.Windows.Forms.ListBox()
        Me.pbContentImage = New System.Windows.Forms.PictureBox()
        Me.lblContentName = New System.Windows.Forms.Label()
        Me.lblWordLength = New System.Windows.Forms.Label()
        CType(Me.nbPageStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nbPageEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbContentImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbxSearchBox
        '
        resources.ApplyResources(Me.tbxSearchBox, "tbxSearchBox")
        Me.tbxSearchBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.tbxSearchBox.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.tbxSearchBox.Name = "tbxSearchBox"
        '
        'cbbSearchType
        '
        Me.cbbSearchType.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        resources.ApplyResources(Me.cbbSearchType, "cbbSearchType")
        Me.cbbSearchType.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.cbbSearchType.FormattingEnabled = True
        Me.cbbSearchType.Items.AddRange(New Object() {resources.GetString("cbbSearchType.Items"), resources.GetString("cbbSearchType.Items1")})
        Me.cbbSearchType.Name = "cbbSearchType"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        resources.ApplyResources(Me.btnSearch, "btnSearch")
        Me.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'lbResults
        '
        resources.ApplyResources(Me.lbResults, "lbResults")
        Me.lbResults.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.lbResults.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbResults.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.lbResults.FormattingEnabled = True
        Me.lbResults.Name = "lbResults"
        '
        'nbPageStart
        '
        resources.ApplyResources(Me.nbPageStart, "nbPageStart")
        Me.nbPageStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.nbPageStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nbPageStart.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.nbPageStart.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nbPageStart.Maximum = New Decimal(New Integer() {40000, 0, 0, 0})
        Me.nbPageStart.Name = "nbPageStart"
        '
        'lblTo
        '
        resources.ApplyResources(Me.lblTo, "lblTo")
        Me.lblTo.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblTo.Name = "lblTo"
        '
        'nbPageEnd
        '
        resources.ApplyResources(Me.nbPageEnd, "nbPageEnd")
        Me.nbPageEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.nbPageEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nbPageEnd.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.nbPageEnd.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nbPageEnd.Maximum = New Decimal(New Integer() {40000, 0, 0, 0})
        Me.nbPageEnd.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.nbPageEnd.Name = "nbPageEnd"
        Me.nbPageEnd.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'pbProgress
        '
        resources.ApplyResources(Me.pbProgress, "pbProgress")
        Me.pbProgress.ForeColor = System.Drawing.SystemColors.Control
        Me.pbProgress.MarqueeAnimationSpeed = 0
        Me.pbProgress.Maximum = 50
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Step = 1
        '
        'cbbFilterType
        '
        Me.cbbFilterType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cbbFilterType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbbFilterType.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        resources.ApplyResources(Me.cbbFilterType, "cbbFilterType")
        Me.cbbFilterType.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.cbbFilterType.FormattingEnabled = True
        Me.cbbFilterType.Items.AddRange(New Object() {resources.GetString("cbbFilterType.Items"), resources.GetString("cbbFilterType.Items1"), resources.GetString("cbbFilterType.Items2")})
        Me.cbbFilterType.Name = "cbbFilterType"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'cbbMediaType
        '
        Me.cbbMediaType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cbbMediaType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbbMediaType.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.cbbMediaType.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.cbbMediaType, "cbbMediaType")
        Me.cbbMediaType.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.cbbMediaType.FormattingEnabled = True
        Me.cbbMediaType.Items.AddRange(New Object() {resources.GetString("cbbMediaType.Items"), resources.GetString("cbbMediaType.Items1"), resources.GetString("cbbMediaType.Items2"), resources.GetString("cbbMediaType.Items3"), resources.GetString("cbbMediaType.Items4"), resources.GetString("cbbMediaType.Items5"), resources.GetString("cbbMediaType.Items6"), resources.GetString("cbbMediaType.Items7")})
        Me.cbbMediaType.Name = "cbbMediaType"
        '
        'lblResultCount
        '
        resources.ApplyResources(Me.lblResultCount, "lblResultCount")
        Me.lblResultCount.Name = "lblResultCount"
        '
        'BottomToolStripPanel
        '
        resources.ApplyResources(Me.BottomToolStripPanel, "BottomToolStripPanel")
        Me.BottomToolStripPanel.Name = "BottomToolStripPanel"
        Me.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.BottomToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        '
        'TopToolStripPanel
        '
        resources.ApplyResources(Me.TopToolStripPanel, "TopToolStripPanel")
        Me.TopToolStripPanel.Name = "TopToolStripPanel"
        Me.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.TopToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        '
        'RightToolStripPanel
        '
        resources.ApplyResources(Me.RightToolStripPanel, "RightToolStripPanel")
        Me.RightToolStripPanel.Name = "RightToolStripPanel"
        Me.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.RightToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        '
        'LeftToolStripPanel
        '
        resources.ApplyResources(Me.LeftToolStripPanel, "LeftToolStripPanel")
        Me.LeftToolStripPanel.Name = "LeftToolStripPanel"
        Me.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.LeftToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        '
        'ContentPanel
        '
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        '
        'lbOutput
        '
        resources.ApplyResources(Me.lbOutput, "lbOutput")
        Me.lbOutput.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.lbOutput.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbOutput.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.lbOutput.FormattingEnabled = True
        Me.lbOutput.Name = "lbOutput"
        '
        'pbContentImage
        '
        resources.ApplyResources(Me.pbContentImage, "pbContentImage")
        Me.pbContentImage.Name = "pbContentImage"
        Me.pbContentImage.TabStop = False
        '
        'lblContentName
        '
        resources.ApplyResources(Me.lblContentName, "lblContentName")
        Me.lblContentName.Name = "lblContentName"
        '
        'lblWordLength
        '
        resources.ApplyResources(Me.lblWordLength, "lblWordLength")
        Me.lblWordLength.Name = "lblWordLength"
        '
        'Form1
        '
        Me.AcceptButton = Me.btnSearch
        Me.AllowDrop = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Controls.Add(Me.lblWordLength)
        Me.Controls.Add(Me.lblContentName)
        Me.Controls.Add(Me.pbContentImage)
        Me.Controls.Add(Me.pbProgress)
        Me.Controls.Add(Me.lbOutput)
        Me.Controls.Add(Me.lblResultCount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbbMediaType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbbFilterType)
        Me.Controls.Add(Me.nbPageEnd)
        Me.Controls.Add(Me.lblTo)
        Me.Controls.Add(Me.nbPageStart)
        Me.Controls.Add(Me.lbResults)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.cbbSearchType)
        Me.Controls.Add(Me.tbxSearchBox)
        Me.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        CType(Me.nbPageStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nbPageEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbContentImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbxSearchBox As TextBox
    Friend WithEvents cbbSearchType As ComboBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents lbResults As ListBox
    Friend WithEvents nbPageStart As NumericUpDown
    Friend WithEvents lblTo As Label
    Friend WithEvents nbPageEnd As NumericUpDown
    Friend WithEvents pbProgress As ProgressBar
    Friend WithEvents cbbFilterType As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cbbMediaType As ComboBox
    Friend WithEvents lblResultCount As Label
    Friend WithEvents BottomToolStripPanel As ToolStripPanel
    Friend WithEvents TopToolStripPanel As ToolStripPanel
    Friend WithEvents RightToolStripPanel As ToolStripPanel
    Friend WithEvents LeftToolStripPanel As ToolStripPanel
    Friend WithEvents ContentPanel As ToolStripContentPanel
    Friend WithEvents lbOutput As ListBox
    Friend WithEvents pbContentImage As PictureBox
    Friend WithEvents lblContentName As Label
    Friend WithEvents lblWordLength As Label
End Class
