<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class JobControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lblContentName = New System.Windows.Forms.Label()
        Me.lblIndex = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblContentName
        '
        Me.lblContentName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContentName.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblContentName.Location = New System.Drawing.Point(2, 10)
        Me.lblContentName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblContentName.Name = "lblContentName"
        Me.lblContentName.Size = New System.Drawing.Size(463, 28)
        Me.lblContentName.TabIndex = 0
        Me.lblContentName.Text = "Label1"
        Me.lblContentName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIndex
        '
        Me.lblIndex.AutoSize = True
        Me.lblIndex.ForeColor = System.Drawing.Color.Snow
        Me.lblIndex.Location = New System.Drawing.Point(470, 35)
        Me.lblIndex.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblIndex.Name = "lblIndex"
        Me.lblIndex.Size = New System.Drawing.Size(16, 13)
        Me.lblIndex.TabIndex = 1
        Me.lblIndex.Text = "-1"
        '
        'JobControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.Controls.Add(Me.lblIndex)
        Me.Controls.Add(Me.lblContentName)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "JobControl"
        Me.Size = New System.Drawing.Size(488, 49)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblContentName As Label
    Friend WithEvents lblIndex As Label
End Class
