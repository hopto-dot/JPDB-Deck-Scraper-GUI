<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lblContentName = New System.Windows.Forms.Label()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.lblIndex = New System.Windows.Forms.Label()
        Me.lblJobInfo = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblContentName
        '
        Me.lblContentName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContentName.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblContentName.Location = New System.Drawing.Point(51, 6)
        Me.lblContentName.Name = "lblContentName"
        Me.lblContentName.Size = New System.Drawing.Size(432, 52)
        Me.lblContentName.TabIndex = 0
        Me.lblContentName.Text = "[Content Name]"
        Me.lblContentName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRemove
        '
        Me.btnRemove.BackgroundImage = Global.Visual_Novel_Vocab_GUI.My.Resources.Resources.remove
        Me.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemove.Location = New System.Drawing.Point(16, 17)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(28, 29)
        Me.btnRemove.TabIndex = 11
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'lblIndex
        '
        Me.lblIndex.AutoSize = True
        Me.lblIndex.ForeColor = System.Drawing.SystemColors.Control
        Me.lblIndex.Location = New System.Drawing.Point(732, 52)
        Me.lblIndex.Name = "lblIndex"
        Me.lblIndex.Size = New System.Drawing.Size(16, 13)
        Me.lblIndex.TabIndex = 12
        Me.lblIndex.Text = "-1"
        '
        'lblJobInfo
        '
        Me.lblJobInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJobInfo.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblJobInfo.Location = New System.Drawing.Point(488, 7)
        Me.lblJobInfo.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblJobInfo.Name = "lblJobInfo"
        Me.lblJobInfo.Size = New System.Drawing.Size(255, 46)
        Me.lblJobInfo.TabIndex = 13
        Me.lblJobInfo.Text = "___ to ___ using filter ____________"
        Me.lblJobInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'JobControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.Controls.Add(Me.lblIndex)
        Me.Controls.Add(Me.lblJobInfo)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.lblContentName)
        Me.Name = "JobControl"
        Me.Size = New System.Drawing.Size(746, 65)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblContentName As Label
    Friend WithEvents btnRemove As Button
    Friend WithEvents lblIndex As Label

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Form1.RemoveJob(lblIndex.Text)
        JobsWindow.GetJobs()
        JobsWindow.Refresh()
    End Sub

    Friend WithEvents lblJobInfo As Label

End Class
