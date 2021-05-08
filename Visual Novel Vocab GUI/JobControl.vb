Public Class JobControl
    Sub SelectMe()
        JobsWindow.SelectedJob = Me.lblIndex.Text
    End Sub
    Private Sub JobControl_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        SelectMe()
    End Sub

    Private Sub lblContentName_Click(sender As Object, e As EventArgs) Handles lblContentName.Click
        SelectMe()
    End Sub
End Class
