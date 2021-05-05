Public Class JobsWindow
    Dim ContentJobs As New List(Of Content)
    Private Sub JobsWindow_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Form1.JobOpen = False
    End Sub

    Private Sub JobsWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetJobs()
    End Sub
    Public Sub RemoveJob(ToRemoveIndex)
        ContentJobs.Remove(ToRemoveIndex)
        RefreshJobs()
    End Sub
    Sub RefreshJobs()
        fpJobs.Controls.Clear()
        For Each Job In ContentJobs
            Dim JobAdd As New JobControl
            JobAdd.lblContentName.Text = Job.Name
            'JobAdd.lblJobInfo.Text = Job.
            fpJobs.Controls.Add(JobAdd)
        Next
        Refresh()
    End Sub
    Public Sub GetJobs()
        ContentJobs = Form1.Getjobs()

        fpJobs.Controls.Clear()
        Dim I As Integer = 0
        For Each Job In ContentJobs
            Dim JobAdd As New JobControl
            JobAdd.lblContentName.Text = Job.Name
            JobAdd.lblJobInfo.Text = Job.PageStart & " to " & Job.PageEnd & " using filter " & Job.FilterType    '___ to ___ using filter ____________
            JobAdd.lblIndex.Text = I
            fpJobs.Controls.Add(JobAdd)
            I += 1
        Next
        lblJobCount.Text = ContentJobs.Count & " jobs"
        Refresh()

        Debug.WriteLine(fpJobs.Controls.Count)
    End Sub

    Private Sub btnStartJobs_Click(sender As Object, e As EventArgs) Handles btnStartJobs.Click, Button1.Click
        For Each Job In ContentJobs
            'ScrapeDeck(PageStart, PageEnd, FilterType, NovelLink)
            ScrapeCode.SaveToTXT(ScrapeCode.ScrapeDeck(Job.PageStart, Job.PageEnd, Job.FilterType, Job.DeckLink, True, nbDelay.Value), "downloads", Job.Name)
        Next
        MsgBox("Finished all jobs!")
    End Sub
End Class