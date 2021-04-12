<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormScraping
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormScraping))
        Me.pbProgress = New System.Windows.Forms.ProgressBar()
        Me.lblScraping = New System.Windows.Forms.Label()
        Me.lblContextName = New System.Windows.Forms.Label()
        Me.lblExtra = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'pbProgress
        '
        Me.pbProgress.Location = New System.Drawing.Point(12, 172)
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(435, 32)
        Me.pbProgress.TabIndex = 0
        '
        'lblScraping
        '
        Me.lblScraping.AutoSize = True
        Me.lblScraping.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScraping.Location = New System.Drawing.Point(12, 9)
        Me.lblScraping.Name = "lblScraping"
        Me.lblScraping.Size = New System.Drawing.Size(303, 24)
        Me.lblScraping.TabIndex = 1
        Me.lblScraping.Text = "Scraping [type] using the [filter] filter"
        '
        'lblContextName
        '
        Me.lblContextName.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContextName.Location = New System.Drawing.Point(11, 38)
        Me.lblContextName.Name = "lblContextName"
        Me.lblContextName.Size = New System.Drawing.Size(435, 89)
        Me.lblContextName.TabIndex = 2
        Me.lblContextName.Text = "[name of content]"
        Me.lblContextName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblExtra
        '
        Me.lblExtra.AutoSize = True
        Me.lblExtra.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExtra.Location = New System.Drawing.Point(12, 140)
        Me.lblExtra.Name = "lblExtra"
        Me.lblExtra.Size = New System.Drawing.Size(45, 16)
        Me.lblExtra.TabIndex = 1
        Me.lblExtra.Text = "[extra]"
        '
        'FormScraping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(459, 216)
        Me.Controls.Add(Me.lblContextName)
        Me.Controls.Add(Me.lblExtra)
        Me.Controls.Add(Me.lblScraping)
        Me.Controls.Add(Me.pbProgress)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormScraping"
        Me.Text = "FormScraping"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pbProgress As ProgressBar
    Friend WithEvents lblScraping As Label
    Friend WithEvents lblContextName As Label
    Friend WithEvents lblExtra As Label
End Class
