Imports System.IO
Imports System.Net

Public Class Form1
    Dim SearchType As String = "Words"
    Dim Reverse As Boolean = False
    Class Content
        Public Name As String = ""
        Public ContentType As String = "Anime"
        Public WordLength As Integer = 0
        Public UniqueWords As Integer = 0
        Public UniqueWordsOnce As Integer = 0
        Public OncePercentage As String = "50%"
        Public UniqueKanji As Integer = 0
        Public Difficulty As String = "/10"
        Public DeckLink As String = ""
        Public ImageURL As String = ""
    End Class

    Dim ContentList As New List(Of Content) From {}
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pbProgress.Hide()
        lblResultCount.Hide()
        lblResultCount.Text = ""

        lblContentName.Text = "Name: "
        lblContentType.Text = "Type: "
        lblWordLength.Text = "Word Length: "
        lblUniqueWords.Text = "Unique Words: "
        lblUsedOnce.Text = "Used Once: "
        lblUsedOncePcent.Text = "Used Once (%): "
        lblUniqueKanji.Text = "Unique Kanji: "
        lblDifficulty.Text = "Difficulty: "
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim PageStart As Integer = nbPageStart.Value
        Dim PageEnd As Integer = nbPageEnd.Value
        Dim FilterType As String = cbbFilterType.Text
        Dim NovelLink As String = tbxSearchBox.Text
        lbOutput.Items.Clear()

        Debug.WriteLine("Scraping {3} from {0}-{1} with filter {2}", PageStart, PageEnd, FilterType, cbbSearchType.Text)
        Dim Client As New WebClient
        Client.Encoding = System.Text.Encoding.UTF8
        Dim HTML As String = ""
        Dim SnipIndex As Integer = -1

        Dim OriginalFilter As String = FilterType
        Dim ScrapeKanji As Boolean = False
        If Strings.Left(FilterType, 1) = "k" Then
            ScrapeKanji = True
        End If

        If CInt(PageStart) > CInt(PageEnd) Then
            PageEnd = PageStart + 50
            nbPageEnd.Value = PageStart + 50
        End If

        If PageEnd > 32650 Then
            PageEnd = 32650
        End If

        If NovelLink.Contains("https://") = False Then
            SearchDecks()
            Return
        End If

        If NovelLink.Contains("/vocabulary-list") = False Then
            HTML = LinkGet(NovelLink)
        Else
            HTML = NovelLink
            HTML = HTML.Replace("?sort_by=by-frequency-local", "").Replace("?sort_by=chronological", "").Replace("?sort_by=by-frequency-global", "").Replace("&show_only=new", "").Replace("&offset=50", "").Replace("&offset=100", "").Replace("#a", "")
        End If

        If HTML.Length > 250 Then
            MsgBox("That is too long to work", MsgBoxStyle.Critical)
        End If

        Dim SnipIndex2 As Integer = -1
        Dim BaseURL As String = HTML

        pbProgress.Visible = True

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
        If Reverse = True Then
            FilterType = FilterType.Replace("offset=", "order=reverse&offset=")
        End If

        Dim NovelName As String = BaseURL
        Try
            NovelName = NovelName.Replace("https://jpdb.io/visual-novel/", "").Replace("https://jpdb.io/anime/", "")
            SnipIndex = NovelName.IndexOf("/") + 2
            NovelName = Mid(NovelName, SnipIndex)
            NovelName = NovelName.Replace("/vocabulary-list", "")
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try

        'lbResults.Items.Clear()
        'lbResults.Items.Add(BaseURL)

        Dim BarInteval As Integer = (PageEnd - PageStart) / 50
        Dim BarProgress As Integer = -1

        Do Until PageDone = True Or PageInterval >= PageEnd
            URL = BaseURL & FilterType & PageInterval & "#a"
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
            BarProgress = PageInterval - PageStart + 50
            'debug.WriteLine(BarProgress / BarInteval)

            Try
                pbProgress.Value = Math.Floor((BarProgress / BarInteval) + 2)
            Catch ex As Exception
                pbProgress.Value = 50
            End Try

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
                        lbOutput.Items.Add(WordTemp)
                    Catch ex As Exception
                        Continue Do
                    End Try
                End If

                SnipIndex = HTML.IndexOf("#a") + 3
                'HTML = Mid(HTML, SnipIndex)
            Loop

            PageInterval += 50
        Loop

        If System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Contains("Debug") = True Then
            SaveToTXT(WordIDs, "program", NovelName)
        End If

        pbProgress.Value = 50

        Debug.WriteLine("Unique Words: " & WordIDs.Count)
        If cbbSearchType.Text = "Kanji" Then
            ExtractKanji(WordIDs)
            pbProgress.Hide()
            Return
        End If

        If WordIDs.Count > 1 Then
            If ScrapeKanji = False Then
                'MsgBox("Successfully Scraped " & WordIDs.Count & " words from " & PageStart & "-" & PageInterval & " of deck " & NovelName & " with the " & FilterType.Replace("?sort_by=by-frequency-local&offset=", "frequency") & " filter")
            End If
        Else
            Debug.WriteLine("Only scraped {3} words from {0}-{1} of {4} with the {2} filter", PageStart, PageInterval, FilterType.Replace("?sort_by=by-frequency-local&offset=", "frequency"), WordIDs.Count, NovelName)
            MsgBox("Didn't scrape any words")
            Debug.WriteLine(URL)
            Return
        End If

        Select Case MsgBox("Successfully Scraped " & WordIDs.Count & " words from " & PageStart & "-" & PageInterval & " of deck " & NovelName & " with the " & FilterType.Replace("?sort_by=by-frequency-local&offset=", "frequency") & " filter" & vbNewLine & vbNewLine & "Would you like to save the results to the downloads folder?", vbQuestion + vbYesNo + vbDefaultButton2, "Successful scraped words")
            Case vbYes
                SaveToTXT(WordIDs, "downloads", NovelName)
        End Select
        pbProgress.Hide()
    End Sub
    Sub SearchDecks()
        pbProgress.Hide()
        Const QUOTE = """"
        Dim Client As New WebClient
        Client.Encoding = System.Text.Encoding.UTF8
        Dim HTML As String = ""
        Dim SnipIndex As Integer = -1



        cbbMediaType.Text = Strings.Left(cbbMediaType.Text, 1) & Strings.Mid(cbbMediaType.Text, 2)

        ContentList.Clear()
        lbResults.Items.Clear()

        If cbbMediaType.Text.ToLower <> "anime" And cbbMediaType.Text.ToLower <> "visual novel" And cbbMediaType.Text.ToLower <> "visual novel" And cbbMediaType.Text.ToLower <> "light novel" And cbbMediaType.Text.ToLower <> "web novel" And cbbMediaType.Text.ToLower <> "j-drama" And cbbMediaType.Text.ToLower <> "textbook" And cbbMediaType.Text.ToLower <> "vocabulary list" Then
            cbbMediaType.Text = "All"
        End If

        Dim MediaType As String = "All"
        MediaType = cbbMediaType.Text.Replace(" ", "-").ToLower.Replace("j-drama", "drama")

        Dim MediaTypeFilter As String = "&show_only=" & cbbMediaType.Text.Replace(" ", "_")
        Dim URL As String = "https://jpdb.io/prebuilt_decks?q=" & tbxSearchBox.Text & MediaTypeFilter.ToLower ' & "#a"
        Try
            HTML = Client.DownloadString(New Uri(URL))
            Debug.WriteLine("First client URL")
        Catch ex As Exception
            Try
                URL = "https://jpdb.io/prebuilt_decks?q=" & tbxSearchBox.Text & "#a"
                HTML = Client.DownloadString(New Uri(URL))
            Catch ex2 As Exception
                MsgBox(ex.Message & vbNewLine & vbNewLine & "Failed to process search URL")
                Exit Sub
            End Try
        End Try

        Dim SnipTemp As String

        Do Until HTML.Contains("margin-top: 0.5rem;") = False Or ContentList.Count > 49
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
                SnipTemp = SnipTemp.Replace("&#39;", "'")
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
                If HTML.IndexOf("(used once %)	") <> -1 Then
                    SnipIndex = HTML.IndexOf("(used once %)	") + 59
                    HTML = Mid(HTML, SnipIndex)
                    SnipTemp = HTML
                    SnipIndex = SnipTemp.IndexOf("<")
                    SnipTemp = Strings.Left(HTML, SnipIndex)
                    If IsNumeric(SnipTemp.Replace("%", "")) = True Then
                        NewContent.OncePercentage = SnipTemp
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
                MsgBox("Something went wrong with getting information for some content, however the program will try to continue as normal" & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical)
                Continue Do
            End Try

            If ContentList.Count = 0 Then
                lbResults.Items.Clear()
            End If

            lbResults.Items.Add(NewContent.Name)
            ContentList.Add(NewContent)
        Loop
        lblResultCount.Show()
        lblResultCount.Text = "Results: " & lbResults.Items.Count
    End Sub
    Function LinkGet(ByVal NovelLink)
        Dim Client As New WebClient
        Client.Encoding = System.Text.Encoding.UTF8
        Dim HTML As String = ""
        Dim SnipIndex As Integer = -1

        Try
            HTML = Client.DownloadString(New Uri(NovelLink))
        Catch ex As Exception
            SearchDecks()
            Return ("")
        End Try

        Try
            SnipIndex = HTML.IndexOf("/vocabulary-list") + 0
            HTML = Strings.Left(HTML, SnipIndex)
            SnipIndex = HTML.LastIndexOf("href=""") + 8
            HTML = Mid(HTML, SnipIndex)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Return ("")
        End Try

        HTML = "https://jpdb.io/" & HTML
        HTML &= "/vocabulary-list" '?sort_by=by-frequency-local&offset="
        Return (HTML)
    End Function
    Sub SaveToTXT(ByVal WordIDs, ByVal SaveType, ByVal DeckName)
        SaveType = SaveType.trim.tolower
        Randomize()
        Dim RanInt As Integer = Int((1000) * Rnd())

        Dim path As String = ""
        Dim prefix As String = "VocabOutput"
        If DeckName = "kanji" Then
            prefix = "KanjiOutput"
        End If

        If SaveType = "downloads" Then
            path = Environ("USERPROFILE") & "\Downloads\" & prefix & RanInt & ".txt"
        ElseIf SaveType = "program" Then
            path = "VocabOutput.txt"
        Else
            path = Environ("USERPROFILE") & "\Downloads\" & prefix & RanInt & ".txt"
        End If

        Try
            File.Create(path).Dispose()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & vbNewLine & "The program needs privilages to create files", MsgBoxStyle.Critical, "Couldn't create file")
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

        For Each Word In WordIDs
            TextWriter.WriteLine(Word)
        Next
        TextWriter.Close()
    End Sub
    Sub ExtractKanji(ByVal WordIDs)
        Dim KanjiString As String = ""
        For Each Word In WordIDs
            KanjiString &= Word
        Next

        Dim Kana As String = "ぉぇぃゃょぁっんあアかカさサたタなナはハまマやヤらラわワいイきキしシちチにニひヒみミりリゐヰうウくクすスつツぬヌふフむムゆユるルえエけケせセてテねネへヘめメ※れレゑヱおオこコそソとトのノほホもモよヨろロをヲぱパばバだダざザか゚カ゚がガぴピびビぢヂじジき゚キ゚ぎギぷプぶブづヅずズく゚ク゚ぐグぺペべベでデぜゼけ゚ケ゚げゲぽポぼボどドぞゾこ゚コ゚ごゴ"
        For Each Character In Kana
            KanjiString = KanjiString.Replace(Character, "")
        Next

        Debug.WriteLine("Kanji Count: " & KanjiString.Length)

        Dim UniqueKanjiString As String = ""
        Dim Found As Boolean = False
        For Each Character In KanjiString
            Found = False
            For Each AlreadyAdded In UniqueKanjiString
                If AlreadyAdded = Character Then
                    Found = True
                    Exit For
                End If
            Next
            If Found = False Then
                UniqueKanjiString &= Character
            End If
        Next
        Debug.WriteLine("Unique Kanji Count: " & UniqueKanjiString.Length)

        Dim KanjiIds As New List(Of String) From {}
        For Each Character In UniqueKanjiString
            KanjiIds.Add(Character)
        Next

        Select Case MsgBox("Successfully Scraped " & WordIDs.Count & " kanji with the " & cbbFilterType.Text & " filter" & vbNewLine & vbNewLine & "Would you like to save the results to the downloads folder?", vbQuestion + vbYesNo + vbDefaultButton2, "Successful scraped kanji")
            Case vbYes
                SaveToTXT(KanjiIds, "downloads", "kanji")
        End Select
    End Sub
    Private Sub lbResults_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbResults.SelectedIndexChanged
        Try
            lblContentName.Cursor = Cursors.Hand
            pbContentImage.Cursor = Cursors.Hand
            tbxSearchBox.Text = ContentList.Item(lbResults.SelectedIndex).DeckLink
            Try
                pbContentImage.Load(ContentList.Item(lbResults.SelectedIndex).ImageURL)
            Catch ex As Exception
            End Try

            lblContentName.Text = ContentList.Item(lbResults.SelectedIndex).Name
            lblContentType.Text = ContentList.Item(lbResults.SelectedIndex).ContentType
            lblWordLength.Text = "Word Length: " & ContentList.Item(lbResults.SelectedIndex).WordLength
            lblUniqueWords.Text = "Unique Words: " & ContentList.Item(lbResults.SelectedIndex).UniqueWords
            lblUsedOnce.Text = "Used Once: " & ContentList.Item(lbResults.SelectedIndex).UniqueWordsOnce
            lblUsedOncePcent.Text = "Used Once (%): " & ContentList.Item(lbResults.SelectedIndex).OncePercentage
            lblUniqueKanji.Text = "Unique Kanji: " & ContentList.Item(lbResults.SelectedIndex).UniqueKanji
            lblDifficulty.Text = "Difficulty: " & ContentList.Item(lbResults.SelectedIndex).Difficulty
            btnCopy.Show()

            Dim CompareValue As Integer = ContentList.Item(lbResults.SelectedIndex).WordLength
            If CompareValue < 20000 Then 'green 0-20,000
                lblWordLength.ForeColor = Color.Lime
            ElseIf CompareValue >= 20000 And CompareValue < 40000 Then 'light green 20,000-40,000
                lblWordLength.ForeColor = Color.FromArgb(128, 255, 128)
            ElseIf CompareValue >= 40000 And CompareValue < 70000 Then 'yellow 40,000-70,000
                lblWordLength.ForeColor = Color.FromArgb(255, 255, 128)
            ElseIf CompareValue >= 70000 And CompareValue < 150000 Then 'orange 70,000-150,000
                lblWordLength.ForeColor = Color.FromArgb(255, 192, 128)
            ElseIf CompareValue >= 150000 And CompareValue < 400000 Then 'red 150,000-400,000
                lblWordLength.ForeColor = Color.FromArgb(255, 128, 128)
            ElseIf CompareValue >= 400000 Then 'dark red 400,000+
                lblWordLength.ForeColor = Color.Red
            Else
                lblWordLength.ForeColor = Color.White
            End If

            CompareValue = ContentList.Item(lbResults.SelectedIndex).UniqueWords
            If CompareValue < 1500 Then 'green 0-1500
                lblUniqueWords.ForeColor = Color.Lime
            ElseIf CompareValue >= 1500 And CompareValue < 6500 Then 'light green 1500-6500
                lblUniqueWords.ForeColor = Color.FromArgb(128, 255, 128)
            ElseIf CompareValue >= 6500 And CompareValue < 8000 Then 'yellow 6500-8000
                lblUniqueWords.ForeColor = Color.FromArgb(255, 255, 128)
            ElseIf CompareValue >= 8000 And CompareValue < 10000 Then 'orange 8000-10,000
                lblUniqueWords.ForeColor = Color.FromArgb(255, 192, 128)
            ElseIf CompareValue >= 10000 And CompareValue < 15000 Then 'red 10,000-15,000
                lblUniqueWords.ForeColor = Color.FromArgb(255, 128, 128)
            ElseIf CompareValue >= 15000 Then 'dark red 15,000+
                lblUniqueWords.ForeColor = Color.Red
            Else
                lblUniqueWords.ForeColor = Color.White
            End If

            CompareValue = ContentList.Item(lbResults.SelectedIndex).OncePercentage.Replace("%", "")
            If CompareValue < 40 Then 'green 0-40
                lblUsedOnce.ForeColor = Color.Lime
                lblUsedOncePcent.ForeColor = Color.Lime
            ElseIf CompareValue >= 40 And CompareValue < 55 Then 'light green 40-55
                lblUsedOnce.ForeColor = Color.FromArgb(128, 255, 128)
                lblUsedOncePcent.ForeColor = Color.FromArgb(128, 255, 128)
            ElseIf CompareValue >= 55 And CompareValue < 58 Then 'yellow 55-58
                lblUsedOnce.ForeColor = Color.FromArgb(255, 255, 128)
                lblUsedOncePcent.ForeColor = Color.FromArgb(255, 255, 128)
            ElseIf CompareValue >= 58 And CompareValue < 63 Then 'orange 58-63
                lblUsedOnce.ForeColor = Color.FromArgb(255, 192, 128)
                lblUsedOncePcent.ForeColor = Color.FromArgb(255, 192, 128)
            ElseIf CompareValue >= 63 And CompareValue < 66 Then 'red 63-66
                lblUsedOnce.ForeColor = Color.FromArgb(255, 128, 128)
                lblUsedOncePcent.ForeColor = Color.FromArgb(255, 128, 128)
            ElseIf CompareValue >= 66 Then '66+
                lblUsedOncePcent.ForeColor = Color.Red
            Else
                lblUsedOncePcent.ForeColor = Color.White
            End If

            CompareValue = ContentList.Item(lbResults.SelectedIndex).UniqueKanji
            If CompareValue < 1000 Then 'green 0-1000
                lblUniqueKanji.ForeColor = Color.Lime
            ElseIf CompareValue >= 1000 And CompareValue < 1500 Then 'light green 1000-1500
                lblUniqueKanji.ForeColor = Color.FromArgb(128, 255, 128)
            ElseIf CompareValue >= 1500 And CompareValue < 1700 Then 'yellow 1500-1700
                lblUniqueKanji.ForeColor = Color.FromArgb(255, 255, 128)
            ElseIf CompareValue >= 1700 And CompareValue < 2400 Then 'orange 1700-2400
                lblUniqueKanji.ForeColor = Color.FromArgb(255, 192, 128)
            ElseIf CompareValue >= 2400 And CompareValue < 2700 Then 'red 2400-2700
                lblUniqueKanji.ForeColor = Color.FromArgb(255, 128, 128)
            ElseIf CompareValue >= 2700 Then 'dark red 2700+
                lblUniqueKanji.ForeColor = Color.Red
            Else
                lblUniqueWords.ForeColor = Color.White
            End If

            CompareValue = ContentList.Item(lbResults.SelectedIndex).Difficulty.Replace("/10", "")
            If CompareValue < 3 Then 'green 1-2
                lblDifficulty.ForeColor = Color.Lime
            ElseIf CompareValue >= 3 And CompareValue < 5 Then 'light green 3-4
                lblDifficulty.ForeColor = Color.FromArgb(128, 255, 128)
            ElseIf CompareValue >= 5 And CompareValue < 6 Then 'yellow 5
                lblDifficulty.ForeColor = Color.FromArgb(255, 255, 128)
            ElseIf CompareValue >= 6 And CompareValue < 8 Then 'orange 6-7
                lblDifficulty.ForeColor = Color.FromArgb(255, 192, 128)
            ElseIf CompareValue >= 8 And CompareValue < 11 Then 'red 8-10
                lblDifficulty.ForeColor = Color.FromArgb(255, 128, 128)
            ElseIf CompareValue >= 11 Then 'dark red 11
                lblDifficulty.ForeColor = Color.Red
            Else
                lblDifficulty.ForeColor = Color.White
            End If

        Catch ex As Exception
            Debug.WriteLine("Selected nothing")
        End Try
    End Sub
    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Me.Size.Width < 500 Then
            lblResultCount.Hide()
        Else
            lblResultCount.Show()
        End If
    End Sub
    Private Sub cbbMediaType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbMediaType.SelectedIndexChanged
        SearchDecks()
    End Sub
    Private Sub tbxSearchBox_TextChanged(sender As Object, e As EventArgs) Handles tbxSearchBox.TextChanged
        If tbxSearchBox.Text.Contains("https://") Then
            btnSearch.Text = "Scrape"
        Else
            btnSearch.Text = "Search"
        End If
    End Sub
    Private Sub cbbSearchType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbSearchType.SelectedIndexChanged
        Select Case cbbSearchType.SelectedItem
            Case "Words"
                SearchType = "Words"
            Case "Kanji"
                SearchType = "Kanji"
        End Select
    End Sub
    Private Sub cbbMediaType_TextUpdate(sender As Object, e As EventArgs) Handles cbbMediaType.TextUpdate
        If Strings.Left(cbbMediaType.Text.ToLower, 1) <> "a" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "v" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "l" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "w" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "j" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "t" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "n" Then
            cbbMediaType.Text = ""
        End If

        If cbbMediaType.Text.ToLower = "n" Then
            cbbMediaType.Text = "Visual Novel"
            Me.lbResults.Focus()
        ElseIf cbbMediaType.Text.ToLower = "vi" Then
            cbbMediaType.Text = "Visual Novel"
            Me.lbResults.Focus()
        ElseIf cbbMediaType.Text.ToLower = "an" Then
            cbbMediaType.Text = "Anime"
            Me.lbResults.Focus()
        ElseIf cbbMediaType.Text.ToLower = "l" Then
            cbbMediaType.Text = "Light Novel"
            Me.lbResults.Focus()
        ElseIf cbbMediaType.Text.ToLower = "w" Then
            cbbMediaType.Text = "web novel"
            Me.lbResults.Focus()
        ElseIf cbbMediaType.Text.ToLower = "j" Then
            cbbMediaType.Text = "J-Drama"
            Me.lbResults.Focus()
        ElseIf cbbMediaType.Text.ToLower = "t" Then
            cbbMediaType.Text = "Textbook"
            Me.lbResults.Focus()
        ElseIf cbbMediaType.Text.ToLower = "vo" Then
            cbbMediaType.Text = "Vocabulary List"
            Me.lbResults.Focus()
        ElseIf cbbMediaType.Text.Length = 5 Then
            cbbMediaType.Text = ""
        End If

    End Sub
    Private Sub cbbMediaType_Leave(sender As Object, e As EventArgs) Handles cbbMediaType.Leave
        If cbbMediaType.Text.ToLower <> "anime" And cbbMediaType.Text.ToLower <> "visual novel" And cbbMediaType.Text.ToLower <> "visual novel" And cbbMediaType.Text.ToLower <> "light novel" And cbbMediaType.Text.ToLower <> "web novel" And cbbMediaType.Text.ToLower <> "j-drama" And cbbMediaType.Text.ToLower <> "textbook" And cbbMediaType.Text.ToLower <> "vocabulary list" Then
            cbbMediaType.Text = "All"
        End If
    End Sub
    Private Sub cbbSearchType_TextUpdate(sender As Object, e As EventArgs) Handles cbbSearchType.TextUpdate
        If cbbSearchType.Text.ToLower.Contains("k") Then
            cbbSearchType.Text = "Kanji"
            Me.lbResults.Focus()
        Else
            cbbSearchType.Text = "Words"
        End If
    End Sub
    Private Sub cbbFilterType_TextUpdate(sender As Object, e As EventArgs) Handles cbbFilterType.TextUpdate
        If cbbFilterType.Text.ToLower.Contains("g") Then
            cbbFilterType.Text = "DeckGlobal"
            Me.lbResults.Focus()
        ElseIf cbbFilterType.Text.ToLower.Contains("t") Then
            cbbFilterType.Text = "Time"
            Me.lbResults.Focus()
        Else
            cbbFilterType.Text = "DeckFreq"
        End If
    End Sub

    Private Sub lblContentName_Click(sender As Object, e As EventArgs) Handles lblContentName.Click
        Try
            Process.Start(ContentList.Item(lbResults.SelectedIndex).DeckLink)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub pbContentImage_Click(sender As Object, e As EventArgs) Handles pbContentImage.Click
        Try
            Process.Start("https://www.google.co.jp/search?q=" & ContentList.Item(lbResults.SelectedIndex).Name)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        Try
            Clipboard.SetText(ContentList.Item(lbResults.SelectedIndex).Name & vbNewLine & "Unique Words: " & ContentList.Item(lbResults.SelectedIndex).UniqueWords & vbNewLine & "Used Once (%): " & ContentList.Item(lbResults.SelectedIndex).OncePercentage & vbNewLine & "Difficulty: " & ContentList.Item(lbResults.SelectedIndex).Difficulty)
        Catch ex As Exception
        End Try

    End Sub
End Class