Public Class JobsWindow

    Public SelectedJob As Integer
    Private Sub JobsWindow_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Form1.JobOpen = False
    End Sub
    Private Sub JobsWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshJobs()
    End Sub
    Sub RefreshJobs()
        fpJobs.Controls.Clear()
        Refresh()
        Dim I As Integer = 0
        For Each Job In ScrapeCode.ContentJobs
            Dim JobAdd As New JobControl
            JobAdd.lblContentName.Text = Job.Name
            JobAdd.lblIndex.Text = I
            'JobAdd.lblJobInfo.Text = Job.
            fpJobs.Controls.Add(JobAdd)
            I += 1
        Next
        lblJobCount.Text = fpJobs.Controls.Count & " jobs"
        Refresh()
    End Sub
    Private Sub btnStartJobs_Click(sender As Object, e As EventArgs) Handles btnStartJobs.Click
        If cbbSearchType.Text = "Kanji" Then
            lblWarn.Text = "You cannot scrape kanji just yet" & vbNewLine & "Scrape content on the main window like normal if you want to scrape kanji"

        End If
        Dim JobIndex As Integer = 1
        For Each Job In ContentJobs
            'ScrapeDeck(PageStart, PageEnd, FilterType, NovelLink)
            ScrapeCode.SaveToTXT(ScrapeCode.ScrapeDeck(Job.PageStart, Job.PageEnd, Job.FilterType, Job.DeckLink, True, nbDelay.Value, "(job " & JobIndex & "/ " & ContentJobs.Count & ") " & Job.Name), "downloads", Job.Name)
            JobIndex += 1
        Next
        MsgBox("Finished all jobs!" & vbNewLine & vbNewLine & "Look in your downloads folder :)")
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        RefreshJobs()
    End Sub
    Private Sub btnClearJobs_Click(sender As Object, e As EventArgs) Handles btnClearJobs.Click
        fpJobs.Controls.Clear()
        ContentJobs.Clear()
        lblJobCount.Text = fpJobs.Controls.Count & " jobs"
    End Sub
    Private Sub btnDelJob_Click(sender As Object, e As EventArgs) Handles btnDelJob.Click
        MsgBox("This feature doesn't work yet :(")
        'fpJobs.Controls.RemoveAt(SelectedJob)
        'ContentJobs.RemoveAt(SelectedJob)
        'RefreshJobs()
    End Sub
End Class