<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormOwnDeck
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormOwnDeck))
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.lblSignedIn = New System.Windows.Forms.Label()
        Me.lblBrowserStatus = New System.Windows.Forms.Label()
        Me.btnExportScrapes = New System.Windows.Forms.Button()
        Me.lblScraped = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WebBrowser1.Location = New System.Drawing.Point(16, 79)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.ScriptErrorsSuppressed = True
        Me.WebBrowser1.Size = New System.Drawing.Size(807, 440)
        Me.WebBrowser1.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(12, 39)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(97, 24)
        Me.lblMessage.TabIndex = 1
        Me.lblMessage.Text = "[Message]"
        '
        'lblSignedIn
        '
        Me.lblSignedIn.AutoSize = True
        Me.lblSignedIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSignedIn.Location = New System.Drawing.Point(12, 9)
        Me.lblSignedIn.Name = "lblSignedIn"
        Me.lblSignedIn.Size = New System.Drawing.Size(121, 24)
        Me.lblSignedIn.TabIndex = 1
        Me.lblSignedIn.Text = "Not signed in"
        '
        'lblBrowserStatus
        '
        Me.lblBrowserStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBrowserStatus.AutoSize = True
        Me.lblBrowserStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBrowserStatus.Location = New System.Drawing.Point(3, 522)
        Me.lblBrowserStatus.Name = "lblBrowserStatus"
        Me.lblBrowserStatus.Size = New System.Drawing.Size(22, 13)
        Me.lblBrowserStatus.TabIndex = 1
        Me.lblBrowserStatus.Text = "[-1]"
        '
        'btnExportScrapes
        '
        Me.btnExportScrapes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExportScrapes.BackColor = System.Drawing.Color.Silver
        Me.btnExportScrapes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportScrapes.Location = New System.Drawing.Point(720, 43)
        Me.btnExportScrapes.Name = "btnExportScrapes"
        Me.btnExportScrapes.Size = New System.Drawing.Size(103, 30)
        Me.btnExportScrapes.TabIndex = 2
        Me.btnExportScrapes.Text = "Export Scrapes"
        Me.btnExportScrapes.UseVisualStyleBackColor = False
        '
        'lblScraped
        '
        Me.lblScraped.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblScraped.AutoSize = True
        Me.lblScraped.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScraped.Location = New System.Drawing.Point(732, 15)
        Me.lblScraped.Name = "lblScraped"
        Me.lblScraped.Size = New System.Drawing.Size(91, 18)
        Me.lblScraped.TabIndex = 1
        Me.lblScraped.Text = "Scraped [    ]"
        '
        'FormOwnDeck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(835, 535)
        Me.Controls.Add(Me.btnExportScrapes)
        Me.Controls.Add(Me.lblBrowserStatus)
        Me.Controls.Add(Me.lblSignedIn)
        Me.Controls.Add(Me.lblScraped)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(750, 470)
        Me.Name = "FormOwnDeck"
        Me.Text = "Scrape Own Deck"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents WebBrowser1 As WebBrowser
    Friend WithEvents lblMessage As Label
    Friend WithEvents lblSignedIn As Label
    Friend WithEvents lblBrowserStatus As Label
    Friend WithEvents btnExportScrapes As Button
    Friend WithEvents lblScraped As Label
End Class
