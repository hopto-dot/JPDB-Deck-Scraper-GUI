Imports System.IO
Imports System.Net

Public Class FormOwnDeck

    Dim BrowserStatus As Integer = -1 '-1 = not signed in, 0 = signed in, 1 = picked a deck
    Dim WordIDs As New List(Of String) From {}
    Private Sub FormOwnDeck_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnExportScrapes.Hide()
        lblScraped.Text = ""
        WebBrowser1.Navigate("https://jpdb.io/learn")
        Me.Refresh()
    End Sub
    Sub ScrapeRefresh()
        Try
            lblMessage.Text = WebBrowser1.Url.ToString
        Catch ex As Exception
            Return
        End Try
        lblMessage.Text = "Keep clicking 'Next page', Then 'Export Scrapes'"


        If WebBrowser1.Url.ToString.Contains("jpdb.io/deck?id=") = False Then
            Debug.WriteLine("False scrape start")
            Return
        End If
        Dim HTML As String = WebBrowser1.DocumentText
        Dim SnipIndex As Integer = -1
        Dim WordTemp As String = ""

        Do Until HTML.IndexOf("#a") = -1
            SnipIndex = HTML.IndexOf("#a") - 15
            HTML = Mid(HTML, SnipIndex)

            WordTemp = HTML
            SnipIndex = WordTemp.IndexOf("#a")
            WordTemp = Strings.Left(WordTemp, SnipIndex)
            SnipIndex = WordTemp.LastIndexOf("/") + 2
            WordTemp = Mid(WordTemp, SnipIndex)

            SnipIndex = HTML.IndexOf("#a") + 3
            HTML = Mid(HTML, SnipIndex)

            If WordTemp.Contains(">") = False And WordTemp.Contains("<") = False And WordTemp.Contains("=") = False And WordTemp.Contains("-") = False Then
                Try
                    WordIDs.Add(WordTemp)
                Catch ex As Exception
                    Continue Do
                End Try
            End If

            SnipIndex = HTML.IndexOf("#a") + 3
            'HTML = Mid(HTML, SnipIndex)
        Loop
        lblScraped.Text = WordIDs.Count
        If WordIDs.Count > 1 Then
            btnExportScrapes.Show()
        End If
    End Sub


    Private Sub WebBrowser1_Navigated(sender As Object, e As WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
        lblBrowserStatus.Text = BrowserStatus
        Me.Refresh()
        If BrowserStatus = -1 Then
            Debug.WriteLine(BrowserStatus)
            lblSignedIn.Text = "Not signed in"

            If WebBrowser1.Url.ToString = "https://jpdb.io/learn" Or WebBrowser1.Url.ToString = "https://jpdb.io/" Then
                BrowserStatus = 0
                lblSignedIn.Text = "Signed in"
                lblMessage.Text = "Pick one of your decks to scrape"
                WebBrowser1.Navigate("https://jpdb.io/learn")
                Return
            End If
            If WebBrowser1.Url.ToString.Contains("https://accounts.google.com/") Then
                WebBrowser1.Navigate("https://jpdb.io/learn")
            End If
            lblMessage.Text = "Please login to your jpdb account"

        ElseIf BrowserStatus = 0 Then
            Debug.WriteLine(BrowserStatus)
            lblMessage.Text = "Pick one of your decks to scrape"
            lblSignedIn.Text = "Signed in"
            Me.Refresh()
            If WebBrowser1.Url.ToString <> "https://jpdb.io/learn" And WebBrowser1.Url.ToString.Contains("jpdb.io/deck?id=") = False Then
                WebBrowser1.Navigate("https://jpdb.io/learn")
                BrowserStatus = 0
                Me.Refresh()
            ElseIf WebBrowser1.Url.ToString.Contains("jpdb.io/deck?id=") = True Then
                lblMessage.Text = "Scraping Deck " & WebBrowser1.Url.ToString
                BrowserStatus = 1
                Me.Refresh()
                WebBrowser1.Navigate("https://jpdb.io/deck?id=1&sort_by=by-frequency-global&offset=0#a")
            End If
        ElseIf BrowserStatus = 1 Then
            If WebBrowser1.Url.ToString.Contains("jpdb.io/deck?id=") = False Then
                Me.Close()
            End If
            ScrapeRefresh()
        End If
    End Sub

    Function GetHTML(ByVal URL)
        Try
            WebBrowser1.Navigate(URL)
        Catch ex As Exception
            MsgBox("Couldn't navigate to website")
            Return ("")
        End Try

        Return (WebBrowser1.DocumentText)
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnExportScrapes.Click
        Randomize()
        Dim RanInt As Integer = Int((1000) * Rnd())

        Dim path As String = ""
        Dim prefix As String = "VocabOutput"

        path = Environ("USERPROFILE") & "\Downloads\DeckScrape" & RanInt & ".txt"

        Try
            File.Create(path).Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Couldn't create file")
            Exit Sub
        End Try

        Dim TextWriter As System.IO.StreamWriter
        Try
            TextWriter = New System.IO.StreamWriter(path)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Return
        End Try

        For Each Word In WordIDs
            TextWriter.WriteLine(Word)
        Next
        TextWriter.Close()

        MsgBox("Saved " & WordIDs.Count & " items to 'DeckScrape" & RanInt & ".txt' in the downloads folder!")
    End Sub
End Class