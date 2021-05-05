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
        Me.nbPageStart = New System.Windows.Forms.NumericUpDown()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.nbPageEnd = New System.Windows.Forms.NumericUpDown()
        Me.cbbFilterType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbbMediaType = New System.Windows.Forms.ComboBox()
        Me.lblResultCount = New System.Windows.Forms.Label()
        Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel()
        Me.pbContentImage = New System.Windows.Forms.PictureBox()
        Me.lblContentName = New System.Windows.Forms.Label()
        Me.lblWordLength = New System.Windows.Forms.Label()
        Me.lblUniqueWords = New System.Windows.Forms.Label()
        Me.lblUsedOnce = New System.Windows.Forms.Label()
        Me.lblUsedOncePcent = New System.Windows.Forms.Label()
        Me.lblUniqueKanji = New System.Windows.Forms.Label()
        Me.lblDifficulty = New System.Windows.Forms.Label()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.lblContentType = New System.Windows.Forms.Label()
        Me.cbbSearchOrdering = New System.Windows.Forms.ComboBox()
        Me.cbSearchReverse = New System.Windows.Forms.CheckBox()
        Me.btnOwnDeck = New System.Windows.Forms.Button()
        Me.fpResults = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnScrapeDecks = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
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
        'nbPageStart
        '
        Me.nbPageStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.nbPageStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.nbPageStart, "nbPageStart")
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
        Me.nbPageEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.nbPageEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.nbPageEnd, "nbPageEnd")
        Me.nbPageEnd.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.nbPageEnd.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nbPageEnd.Maximum = New Decimal(New Integer() {40000, 0, 0, 0})
        Me.nbPageEnd.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.nbPageEnd.Name = "nbPageEnd"
        Me.nbPageEnd.Value = New Decimal(New Integer() {3000, 0, 0, 0})
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
        'lblUniqueWords
        '
        resources.ApplyResources(Me.lblUniqueWords, "lblUniqueWords")
        Me.lblUniqueWords.Name = "lblUniqueWords"
        '
        'lblUsedOnce
        '
        resources.ApplyResources(Me.lblUsedOnce, "lblUsedOnce")
        Me.lblUsedOnce.Name = "lblUsedOnce"
        '
        'lblUsedOncePcent
        '
        resources.ApplyResources(Me.lblUsedOncePcent, "lblUsedOncePcent")
        Me.lblUsedOncePcent.Name = "lblUsedOncePcent"
        '
        'lblUniqueKanji
        '
        resources.ApplyResources(Me.lblUniqueKanji, "lblUniqueKanji")
        Me.lblUniqueKanji.Name = "lblUniqueKanji"
        '
        'lblDifficulty
        '
        resources.ApplyResources(Me.lblDifficulty, "lblDifficulty")
        Me.lblDifficulty.Name = "lblDifficulty"
        '
        'btnCopy
        '
        resources.ApplyResources(Me.btnCopy, "btnCopy")
        Me.btnCopy.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.UseVisualStyleBackColor = False
        '
        'lblContentType
        '
        resources.ApplyResources(Me.lblContentType, "lblContentType")
        Me.lblContentType.Name = "lblContentType"
        '
        'cbbSearchOrdering
        '
        Me.cbbSearchOrdering.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cbbSearchOrdering.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbbSearchOrdering.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.cbbSearchOrdering.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.cbbSearchOrdering, "cbbSearchOrdering")
        Me.cbbSearchOrdering.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.cbbSearchOrdering.FormattingEnabled = True
        Me.cbbSearchOrdering.Items.AddRange(New Object() {resources.GetString("cbbSearchOrdering.Items"), resources.GetString("cbbSearchOrdering.Items1"), resources.GetString("cbbSearchOrdering.Items2"), resources.GetString("cbbSearchOrdering.Items3"), resources.GetString("cbbSearchOrdering.Items4")})
        Me.cbbSearchOrdering.Name = "cbbSearchOrdering"
        '
        'cbSearchReverse
        '
        resources.ApplyResources(Me.cbSearchReverse, "cbSearchReverse")
        Me.cbSearchReverse.Cursor = System.Windows.Forms.Cursors.Default
        Me.cbSearchReverse.Name = "cbSearchReverse"
        Me.cbSearchReverse.UseVisualStyleBackColor = True
        '
        'btnOwnDeck
        '
        resources.ApplyResources(Me.btnOwnDeck, "btnOwnDeck")
        Me.btnOwnDeck.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.btnOwnDeck.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOwnDeck.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnOwnDeck.Name = "btnOwnDeck"
        Me.btnOwnDeck.UseVisualStyleBackColor = False
        '
        'fpResults
        '
        resources.ApplyResources(Me.fpResults, "fpResults")
        Me.fpResults.Name = "fpResults"
        '
        'btnScrapeDecks
        '
        Me.btnScrapeDecks.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        resources.ApplyResources(Me.btnScrapeDecks, "btnScrapeDecks")
        Me.btnScrapeDecks.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnScrapeDecks.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnScrapeDecks.Name = "btnScrapeDecks"
        Me.btnScrapeDecks.UseVisualStyleBackColor = False
        '
        'btnSelectAll
        '
        Me.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        resources.ApplyResources(Me.btnSelectAll, "btnSelectAll")
        Me.btnSelectAll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSelectAll.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AcceptButton = Me.btnSearch
        Me.AllowDrop = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Controls.Add(Me.btnSelectAll)
        Me.Controls.Add(Me.btnScrapeDecks)
        Me.Controls.Add(Me.fpResults)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.lblDifficulty)
        Me.Controls.Add(Me.lblUniqueKanji)
        Me.Controls.Add(Me.lblUsedOncePcent)
        Me.Controls.Add(Me.lblUsedOnce)
        Me.Controls.Add(Me.lblUniqueWords)
        Me.Controls.Add(Me.lblContentType)
        Me.Controls.Add(Me.lblWordLength)
        Me.Controls.Add(Me.lblContentName)
        Me.Controls.Add(Me.pbContentImage)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbbSearchOrdering)
        Me.Controls.Add(Me.cbbMediaType)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbbFilterType)
        Me.Controls.Add(Me.nbPageEnd)
        Me.Controls.Add(Me.lblTo)
        Me.Controls.Add(Me.nbPageStart)
        Me.Controls.Add(Me.btnOwnDeck)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.cbbSearchType)
        Me.Controls.Add(Me.tbxSearchBox)
        Me.Controls.Add(Me.cbSearchReverse)
        Me.Controls.Add(Me.lblResultCount)
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
    Friend WithEvents nbPageStart As NumericUpDown
    Friend WithEvents lblTo As Label
    Friend WithEvents nbPageEnd As NumericUpDown
    Friend WithEvents cbbFilterType As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cbbMediaType As ComboBox
    Friend WithEvents lblResultCount As Label
    Friend WithEvents BottomToolStripPanel As ToolStripPanel
    Friend WithEvents TopToolStripPanel As ToolStripPanel
    Friend WithEvents RightToolStripPanel As ToolStripPanel
    Friend WithEvents LeftToolStripPanel As ToolStripPanel
    Friend WithEvents ContentPanel As ToolStripContentPanel
    Friend WithEvents pbContentImage As PictureBox
    Friend WithEvents lblContentName As Label
    Friend WithEvents lblWordLength As Label
    Friend WithEvents lblUniqueWords As Label
    Friend WithEvents lblUsedOnce As Label
    Friend WithEvents lblUsedOncePcent As Label
    Friend WithEvents lblUniqueKanji As Label
    Friend WithEvents lblDifficulty As Label
    Friend WithEvents btnCopy As Button
    Friend WithEvents lblContentType As Label
    Friend WithEvents cbbSearchOrdering As ComboBox
    Friend WithEvents cbSearchReverse As CheckBox
    Friend WithEvents btnOwnDeck As Button
    Friend WithEvents fpResults As FlowLayoutPanel
    Friend WithEvents btnScrapeDecks As Button
    Friend WithEvents btnSelectAll As Button
End Class
