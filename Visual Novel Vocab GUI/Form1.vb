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
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim PageStart As Integer = nbPageStart.Value
        Dim PageEnd As Integer = nbPageEnd.Value
        Dim FilterType As String = cbbFilterType.Text
        Dim NovelLink As String = tbxSearchBox.Text

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
        Dim NewContent As New Content

        Do Until HTML.Contains("margin-top: 0.5rem;") = False Or ContentList.Count > 49
            Try
                If HTML.IndexOf("lazy" & QUOTE & " src=" & QUOTE & "/") <> -1 Then
                    'snipping image url:
                    SnipIndex = HTML.IndexOf("lazy" & QUOTE & " src=" & QUOTE & "/") + 12
                    HTML = Mid(HTML, SnipIndex)
                    SnipTemp = HTML
                    SnipIndex = SnipTemp.IndexOf(QUOTE)
                    SnipTemp = "https://jpdb.io" & Strings.Left(HTML, SnipIndex)
                    NewContent.ImageURL = Strings.Left(HTML, SnipIndex)
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
                NewContent.WordLength = Strings.Left(HTML, SnipIndex)

                'snipping "Unique words":
                SnipIndex = HTML.IndexOf("Unique words") + 22
                HTML = Mid(HTML, SnipIndex)
                SnipTemp = HTML
                SnipIndex = SnipTemp.IndexOf("<")
                NewContent.UniqueWords = Strings.Left(HTML, SnipIndex)

                'snipping "Unique words (used once)":
                SnipIndex = HTML.IndexOf("(used once)") + 21
                HTML = Mid(HTML, SnipIndex)
                SnipTemp = HTML
                SnipIndex = SnipTemp.IndexOf("<")
                NewContent.UniqueWordsOnce = Strings.Left(HTML, SnipIndex)

                'snipping "Unique kanji":
                SnipIndex = HTML.IndexOf("(used once %)	") + 59
                HTML = Mid(HTML, SnipIndex)
                SnipTemp = HTML
                SnipIndex = SnipTemp.IndexOf("<")
                NewContent.OncePercentage = Strings.Left(HTML, SnipIndex)

                'snipping "Unique kanji":
                SnipIndex = HTML.IndexOf("Unique kanji") + 22
                HTML = Mid(HTML, SnipIndex)
                SnipTemp = HTML
                SnipIndex = SnipTemp.IndexOf("<")
                NewContent.UniqueKanji = Strings.Left(HTML, SnipIndex)

                'snipping "difficulty":
                SnipIndex = HTML.IndexOf("Difficulty</th>") + 20
                HTML = Mid(HTML, SnipIndex)
                SnipTemp = HTML
                SnipIndex = SnipTemp.IndexOf("<")
                NewContent.Difficulty = Strings.Left(HTML, SnipIndex)

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
            tbxSearchBox.Text = ContentList.Item(lbResults.SelectedIndex).DeckLink
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
End Class