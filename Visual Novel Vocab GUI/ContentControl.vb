Public Class ContentControl
    Private Sub lblContentName_Click(sender As Object, e As EventArgs) Handles lblContentName.Click
        Form1.SelectedContentIndex = lblIndex.Text
        'Form1.lblResultCount.Text = lblIndex.Text
        Form1.RefreshInfo()
        Form1.ChangeColours("select")
    End Sub

    Private Sub ContentControl_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Form1.SelectedContentIndex = lblIndex.Text
        'Form1.lblResultCount.Text = lblIndex.Text
        Form1.RefreshInfo()
        Form1.ChangeColours("select")
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Form1.SelectedContentIndex = lblIndex.Text
        'Form1.lblResultCount.Text = lblIndex.Text

        Form1.RefreshInfo()
        Form1.ChangeColours("add")
        Form1.AddJob(lblIndex.Text)
    End Sub

End Class
