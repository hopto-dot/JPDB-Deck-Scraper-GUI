<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobsWindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JobsWindow))
        Me.fpJobs = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblJobCount = New System.Windows.Forms.Label()
        Me.cbbSearchType = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblDelay = New System.Windows.Forms.Label()
        Me.nbDelay = New System.Windows.Forms.NumericUpDown()
        Me.btnStartJobs = New System.Windows.Forms.Button()
        Me.btnClearJobs = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnDelJob = New System.Windows.Forms.Button()
        Me.lblWarn = New System.Windows.Forms.Label()
        CType(Me.nbDelay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fpJobs
        '
        Me.fpJobs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fpJobs.AutoScroll = True
        Me.fpJobs.CausesValidation = False
        Me.fpJobs.Location = New System.Drawing.Point(12, 92)
        Me.fpJobs.Name = "fpJobs"
        Me.fpJobs.Size = New System.Drawing.Size(765, 257)
        Me.fpJobs.TabIndex = 0
        '
        'lblJobCount
        '
        Me.lblJobCount.AutoSize = True
        Me.lblJobCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJobCount.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblJobCount.Location = New System.Drawing.Point(266, 63)
        Me.lblJobCount.Name = "lblJobCount"
        Me.lblJobCount.Size = New System.Drawing.Size(80, 24)
        Me.lblJobCount.TabIndex = 1
        Me.lblJobCount.Text = "___ jobs"
        '
        'cbbSearchType
        '
        Me.cbbSearchType.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.cbbSearchType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbbSearchType.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbbSearchType.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.cbbSearchType.FormattingEnabled = True
        Me.cbbSearchType.Items.AddRange(New Object() {"Words", "Kanji"})
        Me.cbbSearchType.Location = New System.Drawing.Point(104, 29)
        Me.cbbSearchType.Margin = New System.Windows.Forms.Padding(2)
        Me.cbbSearchType.Name = "cbbSearchType"
        Me.cbbSearchType.Size = New System.Drawing.Size(71, 26)
        Me.cbbSearchType.TabIndex = 2
        Me.cbbSearchType.Text = "Words"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label2.Location = New System.Drawing.Point(101, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Scrape:"
        '
        'lblDelay
        '
        Me.lblDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDelay.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblDelay.Location = New System.Drawing.Point(176, 6)
        Me.lblDelay.Name = "lblDelay"
        Me.lblDelay.Size = New System.Drawing.Size(85, 18)
        Me.lblDelay.TabIndex = 1
        Me.lblDelay.Text = "Delay (ms)"
        '
        'nbDelay
        '
        Me.nbDelay.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.nbDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nbDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nbDelay.ForeColor = System.Drawing.SystemColors.Window
        Me.nbDelay.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        Me.nbDelay.Location = New System.Drawing.Point(179, 29)
        Me.nbDelay.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nbDelay.Name = "nbDelay"
        Me.nbDelay.Size = New System.Drawing.Size(82, 24)
        Me.nbDelay.TabIndex = 3
        Me.nbDelay.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'btnStartJobs
        '
        Me.btnStartJobs.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.btnStartJobs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnStartJobs.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStartJobs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStartJobs.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.875!)
        Me.btnStartJobs.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnStartJobs.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnStartJobs.Location = New System.Drawing.Point(12, 11)
        Me.btnStartJobs.Margin = New System.Windows.Forms.Padding(2)
        Me.btnStartJobs.Name = "btnStartJobs"
        Me.btnStartJobs.Size = New System.Drawing.Size(87, 44)
        Me.btnStartJobs.TabIndex = 5
        Me.btnStartJobs.Text = "Start Jobs"
        Me.btnStartJobs.UseVisualStyleBackColor = False
        '
        'btnClearJobs
        '
        Me.btnClearJobs.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.btnClearJobs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnClearJobs.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearJobs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearJobs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearJobs.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnClearJobs.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnClearJobs.Location = New System.Drawing.Point(12, 58)
        Me.btnClearJobs.Margin = New System.Windows.Forms.Padding(2)
        Me.btnClearJobs.Name = "btnClearJobs"
        Me.btnClearJobs.Size = New System.Drawing.Size(87, 29)
        Me.btnClearJobs.TabIndex = 5
        Me.btnClearJobs.Text = "Clear Jobs"
        Me.btnClearJobs.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnRefresh.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRefresh.Location = New System.Drawing.Point(104, 58)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(71, 29)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnDelJob
        '
        Me.btnDelJob.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.btnDelJob.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDelJob.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDelJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelJob.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnDelJob.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDelJob.Location = New System.Drawing.Point(179, 58)
        Me.btnDelJob.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDelJob.Name = "btnDelJob"
        Me.btnDelJob.Size = New System.Drawing.Size(82, 29)
        Me.btnDelJob.TabIndex = 5
        Me.btnDelJob.Text = "Delete Job"
        Me.btnDelJob.UseVisualStyleBackColor = False
        '
        'lblWarn
        '
        Me.lblWarn.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblWarn.Location = New System.Drawing.Point(367, 11)
        Me.lblWarn.Name = "lblWarn"
        Me.lblWarn.Size = New System.Drawing.Size(410, 76)
        Me.lblWarn.TabIndex = 6
        Me.lblWarn.Text = resources.GetString("lblWarn.Text")
        '
        'JobsWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(789, 361)
        Me.Controls.Add(Me.lblWarn)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnDelJob)
        Me.Controls.Add(Me.btnClearJobs)
        Me.Controls.Add(Me.btnStartJobs)
        Me.Controls.Add(Me.nbDelay)
        Me.Controls.Add(Me.cbbSearchType)
        Me.Controls.Add(Me.lblDelay)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblJobCount)
        Me.Controls.Add(Me.fpJobs)
        Me.MinimumSize = New System.Drawing.Size(804, 207)
        Me.Name = "JobsWindow"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "JPDB Deck Scraper Jobs"
        CType(Me.nbDelay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents fpJobs As FlowLayoutPanel
    Friend WithEvents lblJobCount As Label
    Friend WithEvents cbbSearchType As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lblDelay As Label
    Friend WithEvents nbDelay As NumericUpDown
    Friend WithEvents btnStartJobs As Button
    Friend WithEvents btnClearJobs As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnDelJob As Button
    Friend WithEvents lblWarn As Label
End Class
