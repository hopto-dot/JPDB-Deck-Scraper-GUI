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

        Public PageStart As Integer = 0
        Public PageEnd As Integer = 3000
        Public FilterType As String = "deckfrequency"
    End Class
    Function ScrapeDeck(PageStart, PageEnd, FilterType, NovelLink, IgnoreMsgBox, PageDelay)
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
        End If

        If PageEnd > 32650 Then
            PageEnd = 32650
        End If

        If NovelLink.Contains("https://") = False Then
            SearchDecks("All", "Difficulty", Form1.cbSearchReverse.Checked, "")
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
            Try
                Threading.Thread.Sleep(PageDelay)
            Catch ex As Exception
            End Try

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

        If IgnoreMsgBox = False Then
            Select Case MsgBox("Successfully Scraped " & WordIDs.Count & " words from " & PageStart & "-" & PageInterval & " of deck " & Form1.LastScrapeName & " with the " & FilterType.Replace("?sort_by=by-frequency-local&offset=", "frequency") & " filter" & vbNewLine & vbNewLine & "Would you like to save the results to the downloads folder?", vbQuestion + vbYesNo + vbDefaultButton2, "Successful scraped words")
                Case vbYes
                    'Form1.SaveToTXT(WordIDs, "downloads", Form1.LastScrapeName)
                    Return (WordIDs)
            End Select
        Else
            Return (WordIDs)
        End If

        Exit Function
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths
    Function SearchDecks(MediaType, SearchOrdering, SearchReverse, SearchBoxText)
        Const QUOTE = """"
        Dim Client As New WebClient
        Client.Encoding = System.Text.Encoding.UTF8
        Dim HTML As String = ""
        Dim SnipIndex As Integer = -1

        MediaType = Strings.Left(MediaType, 1) & Strings.Mid(MediaType, 2)

        'ContentList.Clear()
        'lbResults.Items.Clear()

        'If MediaTypw.Text.ToLower <> "anime" And MediaTypw.Text.ToLower <> "visual novel" And MediaTypw.Text.ToLower <> "visual novel" And cbbMediaType.Text.ToLower <> "light novel" And cbbMediaType.Text.ToLower <> "web novel" And cbbMediaType.Text.ToLower <> "j-drama" And cbbMediaType.Text.ToLower <> "textbook" And cbbMediaType.Text.ToLower <> "vocabulary list" Then
        '    MediaType.Text = "All"
        'End If

        MediaType = MediaType.ToLower
        MediaType = MediaType.Replace("j-drama", "drama")
        MediaType = "&show_only=" & MediaType.Replace(" ", "_").ToLower
        If MediaType.Contains("all") Then
            MediaType = ""
        End If

        SearchOrdering = SearchOrdering.Replace(" ", "_")
        If MediaType.ToLower = "textbook" Then
            SearchOrdering = "name"
        End If
        SearchOrdering = "&sort_by=" & SearchOrdering.ToLower
        If SearchReverse = True Then
            SearchOrdering &= "&order=reverse"
        End If

        Dim URL As String = "https://jpdb.io/prebuilt_decks?q=" & SearchBoxText & MediaType & SearchOrdering
        Try
            HTML = Client.DownloadString(New Uri(URL))
            Debug.WriteLine("First client URL")
        Catch ex As Exception
            Try
                URL = "https://jpdb.io/prebuilt_decks?q=" & SearchBoxText & "&sort_by=difficulty"
                HTML = Client.DownloadString(New Uri(URL))
                Debug.WriteLine("Search filter failed")
            Catch ex2 As Exception
                MsgBox(ex.Message & vbNewLine & vbNewLine & "You were either temporarily IP banned from using jpdb.io or aren't connected to the internet")
                Exit Function
            End Try
        End Try

        Dim SnipTemp As String = ""
        Dim ContentList As New List(Of Content) From {}
        Do Until HTML.Contains("margin-top: 0.5rem;") = False Or ContentList.Count > 50
            Dim NewContent As New Content
            Try
                If HTML.IndexOf("lazy" & QUOTE & " src=" & QUOTE & "/") <> -1 Then
                    'snipping image url:
                    SnipIndex = HTML.IndexOf("lazy" & QUOTE & " src=" & QUOTE & "/") + 12
                    HTML = Mid(HTML, SnipIndex)
                    SnipTemp = HTML
                    SnipIndex = SnipTemp.IndexOf(QUOTE)
                    SnipTemp = "https://jpdb.io" & Strings.Left(HTML, SnipIndex)
                    NewContent.ImageURL = SnipTemp
                End If

                'snipping content type:
                SnipIndex = HTML.IndexOf("<div style=" & QUOTE & "opacity: 0.5" & QUOTE & ">") + 27
                HTML = Mid(HTML, SnipIndex)
                SnipTemp = HTML
                SnipIndex = SnipTemp.IndexOf("<")
                NewContent.ContentType = Strings.Left(HTML, SnipIndex)

                'snipping content name:
                SnipIndex = HTML.IndexOf("max-width: 30rem;" & QUOTE & ">") + 20
                HTML = Mid(HTML, SnipIndex)
                SnipTemp = HTML
                SnipIndex = SnipTemp.IndexOf("<")
                SnipTemp = Strings.Left(SnipTemp, SnipIndex)
                SnipTemp = SnipTemp.Replace("&#39;", "'").Replace("&quot;", QUOTE)
                NewContent.Name = SnipTemp

                'snipping "length (in words)":
                SnipIndex = HTML.IndexOf("Length (in words)") + 27
                HTML = Mid(HTML, SnipIndex)
                SnipTemp = HTML
                SnipIndex = SnipTemp.IndexOf("<")
                SnipTemp = Strings.Left(HTML, SnipIndex)
                If IsNumeric(SnipTemp) = True Then
                    NewContent.WordLength = SnipTemp
                End If

                'snipping "Unique words":
                SnipIndex = HTML.IndexOf("Unique words") + 22
                HTML = Mid(HTML, SnipIndex)
                SnipTemp = HTML
                SnipIndex = SnipTemp.IndexOf("<")
                SnipTemp = Strings.Left(HTML, SnipIndex)
                If IsNumeric(SnipTemp) = True Then
                    NewContent.UniqueWords = SnipTemp
                End If

                'snipping "Unique words (used once)":
                If HTML.IndexOf("(used once)") <> -1 Then
                    SnipIndex = HTML.IndexOf("(used once)") + 21
                    HTML = Mid(HTML, SnipIndex)
                    SnipTemp = HTML
                    SnipIndex = SnipTemp.IndexOf("<")
                    SnipTemp = Strings.Left(HTML, SnipIndex)
                    If IsNumeric(SnipTemp) = True Then
                        NewContent.UniqueWordsOnce = SnipTemp
                    End If
                End If

                'snipping "Unique words (used once %)":
                If HTML.IndexOf("(used once %)") <> -1 Then
                    SnipIndex = HTML.IndexOf("(used once %)	") + 58
                    HTML = Mid(HTML, SnipIndex)
                    SnipTemp = HTML
                    SnipIndex = SnipTemp.IndexOf("<")
                    SnipTemp = Strings.Left(HTML, SnipIndex)
                    SnipTemp = SnipTemp.Replace(">", "").Replace("d", "")
                    If IsNumeric(SnipTemp.Replace("%", "")) = True Then
                        If SnipTemp.Replace("%", "") < 10 Then
                            NewContent.OncePercentage = "?%"
                        Else
                            NewContent.OncePercentage = SnipTemp
                        End If
                    End If
                End If

                'snipping "Unique kanji":
                SnipIndex = HTML.IndexOf("Unique kanji") + 22
                HTML = Mid(HTML, SnipIndex)
                SnipTemp = HTML
                SnipIndex = SnipTemp.IndexOf("<")
                NewContent.UniqueKanji = Strings.Left(HTML, SnipIndex)

                'snipping "difficulty":
                If HTML.IndexOf("Difficulty</th>") <> -1 Then
                    SnipIndex = HTML.IndexOf("Difficulty</th>") + 20
                    HTML = Mid(HTML, SnipIndex)
                    SnipTemp = HTML
                    SnipIndex = SnipTemp.IndexOf("<")
                    SnipTemp = Strings.Left(HTML, SnipIndex)
                    If IsNumeric(SnipTemp.Replace("/10", "")) = True Then
                        NewContent.Difficulty = SnipTemp
                    End If
                End If

                'snipping vocab deck link:
                SnipIndex = HTML.IndexOf("top: 0.5rem;") + 25
                HTML = Mid(HTML, SnipIndex)
                SnipTemp = HTML
                SnipIndex = SnipTemp.IndexOf("""")
                SnipTemp = Strings.Left(HTML, SnipIndex)
                NewContent.DeckLink = "https://jpdb.io/" & SnipTemp
            Catch ex As Exception
                MsgBox("Something went wrong with getting information for some content" & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical)
                Exit Function
            End Try

            'If ContentList.Count = 0 Then
            '    lbResults.Items.Clear()
            'End If

            ContentList.Add(NewContent)
        Loop

        Return (ContentList)
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
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
            'MsgBox("No content selected or there is no vocabulary to scrape" & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical)
            TextWriter.Close()
            Exit Sub
        End Try
        TextWriter.Close()
    End Sub

End Module
