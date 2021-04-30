Imports System.IO
Imports System.Net

Module ScrapeCode
    Public Class Content
        Public Name As String = "?"
        Public ContentType As String = "?"
        Public WordLength As Integer = -1
        Public UniqueWords As Integer = -1
        Public UniqueWordsOnce As Integer = -1
        Public OncePercentage As String = "?"
        Public UniqueKanji As Integer = -1
        Public Difficulty As String = "?"
        Public DeckLink As String = "?"
        Public ImageURL As String = "?"
    End Class
    Function ScrapeDeck(PageStart, PageEnd, FilterType, NovelLink)
        Dim Client As New WebClient
        Client.Encoding = System.Text.Encoding.UTF8
        Dim HTML As String = ""
        Dim SnipIndex As Integer = -1

        Dim OriginalFilter As String = FilterType
        Dim ScrapeKanji As Boolean = False
        'If Strings.Left(cbbSearchType.Text, 1) = "K" Then
        '    ScrapeKanji = True
        'End If

        If CInt(PageStart) > CInt(PageEnd) Then
            PageEnd = PageStart + 50
            Form1.nbPageEnd.Value = PageStart + 50
        End If

        If PageEnd > 32650 Then
            PageEnd = 32650
        End If

        If NovelLink.Contains("https://") = False Then
            Form1.SearchDecks()
            Exit Function
        End If

        If NovelLink.Contains("/vocabulary-list") = False Then
            HTML = Form1.LinkGet(NovelLink)
        Else
            HTML = NovelLink
            HTML = HTML.Replace("?sort_by=by-frequency-local", "").Replace("?sort_by=chronological", "").Replace("?sort_by=by-frequency-global", "").Replace("&show_only=new", "").Replace("&offset=50", "").Replace("&offset=100", "").Replace("#a", "")
        End If

        If HTML.Length > 250 Then
            MsgBox("That is too long to work", MsgBoxStyle.Critical)
        End If

        Dim SnipIndex2 As Integer = -1
        Dim BaseURL As String = HTML

        Dim PageDone As Boolean = False
        Dim PageInterval As Integer = PageStart
        Dim WordTemp As String = ""
        Dim URL As String = ""
        Dim WordIDs As New List(Of String) From {}
        '"?sort_by=by-frequency-local&offset="
        Select Case FilterType.ToLower
            Case "deckfrequency", "deckfreq", "frequency", "freq", "kdeckfrequency", "kdeckfreq", "kfrequency", "kfreq"
                FilterType = "?sort_by=by-frequency-local&offset="

            Case "globalfreq", "globfreq", "globalfrequency", "freqglobal", "freqglob", "kglobalfreq", "kglobfreq", "kglobalfrequency", "kfreqglobal", "kfreqglob"
                FilterType = "?sort_by=by-frequency-global&offset="

            Case "order", "chrono", "chronological", "time", "chronologically", "chron", "korder", "kchrono", "kchronological", "ktime", "kchronologically", "kchron"
                FilterType = "?sort_by=chronological&offset="

            Case Else
                FilterType = "?sort_by=by-frequency-local&offset="

        End Select
        If Form1.cbSearchReverse.Checked = True Then
            Form1.Reverse = True
        Else
            Form1.Reverse = False
        End If
        If Form1.Reverse = True Then
            FilterType = FilterType.Replace("offset=", "order=reverse&offset=")
        End If



        Debug.WriteLine("Scraping {3} from {0}-{1} with filter {2}", PageStart, PageEnd, FilterType, Form1.cbbSearchType.Text)
        Dim LoadingScreen As New FormScraping
        LoadingScreen.lblScraping.Text = "Scraping " & Form1.cbbSearchType.Text & " using the " & Form1.cbbFilterType.Text & " filter"
        LoadingScreen.lblContextName.Text = Form1.LastScrapeName
        LoadingScreen.pbProgress.Maximum = PageEnd
        LoadingScreen.pbProgress.Minimum = PageStart
        LoadingScreen.Show()
        LoadingScreen.Refresh()

        Do Until PageDone = True Or PageInterval >= PageEnd
            Threading.Thread.Sleep(100)

            Try
                LoadingScreen.pbProgress.Value = PageInterval + 40
            Catch ex As Exception
                LoadingScreen.pbProgress.Value = PageEnd
            End Try

            URL = BaseURL & FilterType & PageInterval & "#a"
            LoadingScreen.lblExtra.Text = "https://..." & FilterType & PageInterval & "#a"
            LoadingScreen.Refresh()
            Try
                HTML = Client.DownloadString(New Uri(URL))
            Catch ex As Exception
                PageDone = True
                Continue Do
            End Try
            If HTML.Contains("/vocabulary/") = False Then
                PageDone = True
                Continue Do
            End If

            'debug.WriteLine("-- " & PageInterval & " --")

            SnipIndex = HTML.IndexOf("#a") + 2
            HTML = Mid(HTML, SnipIndex)
            'SnipIndex = HTML.IndexOf("#a") + 2
            'HTML = Mid(HTML, SnipIndex)

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

            PageInterval += 50
        Loop

        LoadingScreen.Refresh()
        LoadingScreen.Hide()

        If System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Contains("Debug") = True Then
            SaveToTXT(WordIDs, "program", Form1.LastScrapeName)
        End If

        Debug.WriteLine("Unique Words: " & WordIDs.Count)
        'If Form1.cbbSearchType.Text = "Kanji" Then
        '    Form1.ExtractKanji(WordIDs)
        '    Return
        'End If

        If WordIDs.Count > 1 Then
            If ScrapeKanji = False Then
                'MsgBox("Successfully Scraped " & WordIDs.Count & " words from " & PageStart & "-" & PageInterval & " of deck " & NovelName & " with the " & FilterType.Replace("?sort_by=by-frequency-local&offset=", "frequency") & " filter")
            End If
        Else
            Debug.WriteLine("Only scraped {3} words from {0}-{1} of {4} with the {2} filter", PageStart, PageInterval, FilterType.Replace("?sort_by=by-frequency-local&offset=", "frequency"), WordIDs.Count, Form1.LastScrapeName)
            MsgBox("Didn't scrape any words")
            Debug.WriteLine(URL)
            Exit Function
        End If

        Select Case MsgBox("Successfully Scraped " & WordIDs.Count & " words from " & PageStart & "-" & PageInterval & " of deck " & Form1.LastScrapeName & " with the " & FilterType.Replace("?sort_by=by-frequency-local&offset=", "frequency") & " filter" & vbNewLine & vbNewLine & "Would you like to save the results to the downloads folder?", vbQuestion + vbYesNo + vbDefaultButton2, "Successful scraped words")
            Case vbYes
                'Form1.SaveToTXT(WordIDs, "downloads", Form1.LastScrapeName)
                Return (WordIDs)
        End Select
        Exit Function
    End Function

    Public Sub SaveToTXT(WordIDs, SaveType, DeckName)
        SaveType = SaveType.trim.tolower
        Randomize()
        Dim RanInt As Integer = Int((1000) * Rnd())
        DeckName = DeckName.replace("?", "").replace("*", "").replace("/", "").replace("|", "").replace("\", "").replace("<", "").replace(">", "").replace(":", "").replace("""", "")

        Dim path As String = ""
        Dim prefix As String = "VocabOutput"

        If SaveType = "downloads" Then
            path = Environ("USERPROFILE") & "\Downloads\" & DeckName & "_Scrape.txt"
        ElseIf SaveType = "program" Then
            path = "Output.txt"
        Else
            path = Environ("USERPROFILE") & "\Downloads\" & DeckName & "_Scrape.txt"
        End If

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

        If SaveType = "program" Then
            TextWriter.WriteLine(DeckName)
            TextWriter.WriteLine(DateTime.Now)
        End If

        Try
            For Each Word In WordIDs
                TextWriter.WriteLine(Word)
            Next
        Catch ex As Exception
            MsgBox("No content selected" & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical)
            TextWriter.Close()
            Exit Sub
        End Try
        TextWriter.Close()
    End Sub

End Module
