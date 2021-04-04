Imports System.IO
Imports System.Net

Public Class Form1
    Dim SearchType As String = "Words"
    Dim Reverse As Boolean = False
    Private Sub cbbSearchType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbSearchType.SelectedIndexChanged
        Select Case cbbSearchType.SelectedItem
            Case "Words"
                SearchType = "Words"
            Case "Kanji"
                SearchType = "Kanji"
        End Select
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
    End Sub
    Sub SearchDecks()
        pbProgress.Hide()
        Dim Client As New WebClient
        Client.Encoding = System.Text.Encoding.UTF8
        Dim HTML As String = ""
        Dim SnipIndex As Integer = -1

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

        Dim URLList As New List(Of String) From {}
        Dim SnipTemp As String

        Do Until HTML.Contains("margin-top: 0.5rem;") = False Or URLList.Count > 49
            SnipIndex = HTML.IndexOf("top: 0.5rem;") + 25
            HTML = Mid(HTML, SnipIndex)
            SnipTemp = HTML

            SnipIndex = SnipTemp.IndexOf("""")
            SnipTemp = Strings.Left(HTML, SnipIndex)

            If URLList.Count = 0 Then
                lbResults.Items.Clear()
            End If

            If MediaType = "all" Then
                lbResults.Items.Add("https://jpdb.io/" & SnipTemp)
                URLList.Add("https://jpdb.io/" & SnipTemp)
            Else
                If SnipTemp.Contains(MediaType & "/") = True Then
                    lbResults.Items.Add("https://jpdb.io/" & SnipTemp)
                    URLList.Add("https://jpdb.io/" & SnipTemp)
                End If
            End If

        Loop
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

        Select Case MsgBox("Finished scraping words" & vbNewLine & vbNewLine & "Would you like to save the results to the downloads folder?", vbQuestion + vbYesNo + vbDefaultButton2, "Successfully scraped words")
            Case vbYes
                SaveToTXT(KanjiIds, "downloads", "kanji")
        End Select
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pbProgress.Hide()
    End Sub
    Private Sub lbResults_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbResults.SelectedIndexChanged
        Try
            tbxSearchBox.Text = lbResults.Items(lbResults.SelectedIndex)
        Catch ex As Exception
            Debug.WriteLine("Selected nothing")
        End Try
    End Sub
    Private Sub tbxSearchBox_MouseClick(sender As Object, e As MouseEventArgs) Handles tbxSearchBox.MouseClick
        pbProgress.Hide()
    End Sub
    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        pbProgress.Hide()
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

    Private Sub nbPageStart_ValueChanged(sender As Object, e As EventArgs) Handles nbPageStart.ValueChanged

    End Sub

    Private Sub cbbFilterType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbFilterType.SelectedIndexChanged

    End Sub

    Private Sub lblResultCount_Click(sender As Object, e As EventArgs) Handles lblResultCount.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
