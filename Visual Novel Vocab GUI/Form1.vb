Imports System.IO
Imports System.Net

Public Class Form1
    Dim SearchType As String = "Words"
    Public Reverse As Boolean = False

    Public LastScrapeName As String = ""
    Dim ContentList As New List(Of Content) From {}
    Public OwnOpen As Boolean = False

    'Function ContentList = SearchDecks(MediaType, SearchOrdering, SearchReverse, SearchBoxText)  ContentList = SearchDecks(cbbMediaType.Text, cbbSearchOrdering.Text, Reverse, tbxSearchBox.Text)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblResultCount.Hide()
        lblResultCount.Text = ""
        ContentList = SearchDecks("All", "Difficulty", False, tbxSearchBox.Text)
        RefreshResults()
        Try
            lbResults.SelectedIndex = 0
        Catch ex As Exception
            lblContentName.Text = "Name: "
            lblContentType.Text = "Type: "
            lblWordLength.Text = "Word Length: "
            lblUniqueWords.Text = "Unique Words: "
            lblUsedOnce.Text = "Used Once: "
            lblUsedOncePcent.Text = "Used Once (%): "
            lblUniqueKanji.Text = "Unique Kanji: "
            lblDifficulty.Text = "Difficulty: "
        End Try
        tbxSearchBox.Text = ""
        nbPageEnd.Value = 3000
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim SnipIndex As Integer = -1
        Try
            LastScrapeName = ContentList.Item(lbResults.SelectedIndex).Name
        Catch ex As Exception
            Try
                LastScrapeName = tbxSearchBox.Text
                LastScrapeName = LastScrapeName.Replace("https://jpdb.io/visual-novel/", "").Replace("https://jpdb.io/anime/", "")
                SnipIndex = LastScrapeName.IndexOf("/") + 2
                LastScrapeName = Mid(LastScrapeName, SnipIndex)
                LastScrapeName = LastScrapeName.Replace("/vocabulary-list", "")
            Catch ex2 As Exception
                MsgBox(ex2.Message)
                Exit Sub
            End Try
        End Try

        Try
            If nbPageStart.Value > nbPageEnd.Value Then
                nbPageStart.Value = nbPageEnd.Value
            End If
            If nbPageEnd.Value > ContentList(lbResults.SelectedIndex).UniqueWords Then
                nbPageEnd.Value = ContentList(lbResults.SelectedIndex).UniqueWords
            End If
        Catch ex As Exception
        End Try

        'PageStart, PageEnd, FilterType, NovelLink
        If tbxSearchBox.Text.Contains("https://") Then
            Try
                If nbPageEnd.Value > ContentList(lbResults.SelectedIndex).UniqueWords Then
                    nbPageEnd.Value = ContentList(lbResults.SelectedIndex).UniqueWords + 50
                End If
            Catch ex As Exception
                Exit Sub
            End Try
            SaveToTXT(ScrapeDeck(nbPageStart.Value, nbPageEnd.Value, cbbFilterType.Text, tbxSearchBox.Text), "downloads", LastScrapeName)
        Else
            ContentList = SearchDecks(cbbMediaType.Text, cbbSearchOrdering.Text, Reverse, tbxSearchBox.Text)
            RefreshResults()
        End If

    End Sub
    Function LinkGet(ByVal NovelLink)
        Dim Client As New WebClient
        Client.Encoding = System.Text.Encoding.UTF8
        Dim HTML As String = ""
        Dim SnipIndex As Integer = -1

        Try
            HTML = Client.DownloadString(New Uri(NovelLink))
        Catch ex As Exception
            ContentList = SearchDecks(cbbMediaType.Text, cbbSearchOrdering.Text, Reverse, tbxSearchBox.Text)
            RefreshResults()
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
                SaveToTXT(KanjiIds, "downloads", LastScrapeName)
        End Select
    End Sub
    Private Sub lbResults_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbResults.SelectedIndexChanged

        nbPageEnd.Value = ContentList(lbResults.SelectedIndex).UniqueWords

        Try
            lblContentName.Cursor = Cursors.Hand
            tbxSearchBox.Text = ContentList.Item(lbResults.SelectedIndex).DeckLink

            Debug.WriteLine(ContentList.Item(lbResults.SelectedIndex).Name.Length)
            If ContentList.Item(lbResults.SelectedIndex).ContentType <> "Web novel" Then
                If ContentList.Item(lbResults.SelectedIndex).Name.Length <= 30 Then
                    lblContentName.Font = New Font("Microsoft Sans Serif", 21, FontStyle.Regular)
                ElseIf ContentList.Item(lbResults.SelectedIndex).Name.Length <= 53 Then
                    lblContentName.Font = New Font("Microsoft Sans Serif", 18, FontStyle.Regular)
                ElseIf ContentList.Item(lbResults.SelectedIndex).Name.Length <= 62 Then
                    lblContentName.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Regular)
                ElseIf ContentList.Item(lbResults.SelectedIndex).Name.Length <= 70 Then
                    lblContentName.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Regular)
                Else
                    lblContentName.Font = New Font("Microsoft Sans Serif", 11, FontStyle.Regular)
                End If
            Else
                If ContentList.Item(lbResults.SelectedIndex).Name.Length <= 15 Then
                    lblContentName.Font = New Font("Microsoft Sans Serif", 21, FontStyle.Regular)
                ElseIf ContentList.Item(lbResults.SelectedIndex).Name.Length <= 30 Then
                    lblContentName.Font = New Font("Microsoft Sans Serif", 18, FontStyle.Regular)
                ElseIf ContentList.Item(lbResults.SelectedIndex).Name.Length <= 40 Then
                    lblContentName.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Regular)
                ElseIf ContentList.Item(lbResults.SelectedIndex).Name.Length <= 50 Then
                    lblContentName.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Regular)
                ElseIf ContentList.Item(lbResults.SelectedIndex).Name.Length <= 65 Then
                    lblContentName.Font = New Font("Microsoft Sans Serif", 11, FontStyle.Regular)
                ElseIf ContentList.Item(lbResults.SelectedIndex).Name.Length <= 75 Then
                    lblContentName.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                Else
                    lblContentName.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)
                End If
            End If


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
                lblWordLength.ForeColor = Color.FromArgb(0, 179, 0)
            ElseIf CompareValue >= 20000 And CompareValue < 40000 Then 'light green 20,000-40,000
                lblWordLength.ForeColor = Color.FromArgb(153, 255, 102)
            ElseIf CompareValue >= 40000 And CompareValue < 70000 Then 'yellow 40,000-70,000
                lblWordLength.ForeColor = Color.FromArgb(255, 255, 128)
            ElseIf CompareValue >= 70000 And CompareValue < 150000 Then 'orange 70,000-150,000
                lblWordLength.ForeColor = Color.FromArgb(255, 128, 0)
            ElseIf CompareValue >= 150000 And CompareValue < 400000 Then 'red 150,000-400,000
                lblWordLength.ForeColor = Color.FromArgb(255, 128, 128)
            ElseIf CompareValue >= 400000 Then 'dark red 400,000+
                lblWordLength.ForeColor = Color.Red
            Else
                lblWordLength.ForeColor = Color.White
            End If

            CompareValue = ContentList.Item(lbResults.SelectedIndex).UniqueWords
            If CompareValue < 1500 Then 'green 0-1500
                lblUniqueWords.ForeColor = Color.FromArgb(0, 179, 0)
            ElseIf CompareValue >= 1500 And CompareValue < 6500 Then 'light green 1500-6500
                lblUniqueWords.ForeColor = Color.FromArgb(153, 255, 102)
            ElseIf CompareValue >= 6500 And CompareValue < 8000 Then 'yellow 6500-8000
                lblUniqueWords.ForeColor = Color.FromArgb(255, 255, 128)
            ElseIf CompareValue >= 8000 And CompareValue < 10000 Then 'orange 8000-10,000
                lblUniqueWords.ForeColor = Color.FromArgb(255, 128, 0)
            ElseIf CompareValue >= 10000 And CompareValue < 15000 Then 'red 10,000-15,000
                lblUniqueWords.ForeColor = Color.FromArgb(255, 128, 128)
            ElseIf CompareValue >= 15000 Then 'dark red 15,000+
                lblUniqueWords.ForeColor = Color.Red
            Else
                lblUniqueWords.ForeColor = Color.White
            End If

            CompareValue = ContentList.Item(lbResults.SelectedIndex).UniqueWordsOnce
            If CompareValue < 1000 Then 'green 0-800
                lblUsedOnce.ForeColor = Color.FromArgb(0, 179, 0)
            ElseIf CompareValue >= 1000 And CompareValue < 1700 Then 'light green 1000-1700
                lblUsedOnce.ForeColor = Color.FromArgb(153, 255, 102)
            ElseIf CompareValue >= 1700 And CompareValue < 2300 Then 'yellow 1700-2300
                lblUsedOnce.ForeColor = Color.FromArgb(255, 255, 128)
            ElseIf CompareValue >= 2300 And CompareValue < 2700 Then 'orange 2300-2700
                lblUsedOnce.ForeColor = Color.FromArgb(255, 128, 0)
            ElseIf CompareValue >= 2700 And CompareValue < 3200 Then 'red 2700-3200
                lblUsedOnce.ForeColor = Color.FromArgb(255, 128, 128)
            ElseIf CompareValue >= 3200 Then '3200+
                lblUsedOnce.ForeColor = Color.Red
            Else
                lblUsedOnce.ForeColor = Color.White
            End If

            CompareValue = ContentList.Item(lbResults.SelectedIndex).OncePercentage.Replace("%", "")
            If CompareValue = 0 Then
                lblUsedOncePcent.ForeColor = Color.White
            ElseIf CompareValue < 50 Then 'green 0-50
                lblUsedOncePcent.ForeColor = Color.FromArgb(0, 179, 0)
            ElseIf CompareValue >= 50 And CompareValue < 64 Then 'light green 50-64
                lblUsedOncePcent.ForeColor = Color.FromArgb(153, 255, 102)
            ElseIf CompareValue >= 64 And CompareValue < 66 Then 'yellow 64-66
                lblUsedOncePcent.ForeColor = Color.FromArgb(255, 255, 128)
            ElseIf CompareValue >= 66 And CompareValue < 68 Then 'orange 66-68
                lblUsedOncePcent.ForeColor = Color.FromArgb(255, 128, 0)
            ElseIf CompareValue >= 68 And CompareValue < 70 Then 'red 68-70
                lblUsedOncePcent.ForeColor = Color.FromArgb(255, 128, 128)
            ElseIf CompareValue >= 70 Then '70+
                lblUsedOncePcent.ForeColor = Color.Red
            Else
                lblUsedOncePcent.ForeColor = Color.White
            End If

            CompareValue = ContentList.Item(lbResults.SelectedIndex).UniqueKanji
            If CompareValue < 900 Then 'green 0-900
                lblUniqueKanji.ForeColor = Color.FromArgb(0, 179, 0)
            ElseIf CompareValue >= 900 And CompareValue < 1200 Then 'light green 900-1200
                lblUniqueKanji.ForeColor = Color.FromArgb(153, 255, 102)
            ElseIf CompareValue >= 1200 And CompareValue < 1500 Then 'yellow 1300-1500
                lblUniqueKanji.ForeColor = Color.FromArgb(255, 255, 128)
            ElseIf CompareValue >= 1500 And CompareValue < 2200 Then 'orange 1500-2200
                lblUniqueKanji.ForeColor = Color.FromArgb(255, 128, 0)
            ElseIf CompareValue >= 2200 And CompareValue < 2500 Then 'red 2200-2500
                lblUniqueKanji.ForeColor = Color.FromArgb(255, 128, 128)
            ElseIf CompareValue >= 2500 Then 'dark red 2500+
                lblUniqueKanji.ForeColor = Color.Red
            Else
                lblUniqueWords.ForeColor = Color.White
            End If

            CompareValue = ContentList.Item(lbResults.SelectedIndex).Difficulty.Replace("/10", "")
            If CompareValue < 3 Then 'green 1-2
                lblDifficulty.ForeColor = Color.FromArgb(0, 179, 0)
            ElseIf CompareValue >= 3 And CompareValue < 5 Then 'light green 3-4
                lblDifficulty.ForeColor = Color.FromArgb(153, 255, 102)
            ElseIf CompareValue >= 5 And CompareValue < 6 Then 'yellow 5
                lblDifficulty.ForeColor = Color.FromArgb(255, 255, 128)
            ElseIf CompareValue >= 6 And CompareValue < 8 Then 'orange 6-7
                lblDifficulty.ForeColor = Color.FromArgb(255, 128, 0)
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

        Me.Refresh()
        Try
            pbContentImage.Load(ContentList.Item(lbResults.SelectedIndex).ImageURL)
            pbContentImage.Visible = True
        Catch ex As Exception
            pbContentImage.Visible = False
        End Try
        Me.Refresh()
    End Sub
    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Me.Size.Width < 500 Then
            lblResultCount.Hide()
        Else
            lblResultCount.Show()
        End If
        If Me.Height < 362 Then
            pbContentImage.Visible = False
        Else
            pbContentImage.Visible = True
        End If
        If Me.Height < 718 Then
            pbContentImage.Width = pbContentImage.Height / 1.29
        ElseIf Me.Height = 718 Then
            pbContentImage.Width = 286
            pbContentImage.Height = 366
        Else
            pbContentImage.Width = 286
            pbContentImage.Height = 366
        End If
        If Me.Width < 771 Then
            lblResultCount.Hide()
        Else
            lblResultCount.Show()
        End If
    End Sub

    Private Sub cbbMediaType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbMediaType.SelectedIndexChanged, cbbSearchOrdering.SelectedIndexChanged
        If tbxSearchBox.Text.Contains("https://") = True Then
            tbxSearchBox.Text = ""
        End If
        ContentList = SearchDecks(cbbMediaType.Text, cbbSearchOrdering.Text, Reverse, tbxSearchBox.Text)
        RefreshResults()
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
    Private Sub cbbMediaType_TextUpdate(sender As Object, e As EventArgs) Handles cbbMediaType.TextUpdate, cbbSearchOrdering.TextUpdate


        If Strings.Left(cbbMediaType.Text.ToLower, 1) <> "a" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "v" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "l" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "w" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "j" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "t" And Strings.Left(cbbMediaType.Text.ToLower, 1) <> "n" Then
            cbbMediaType.Text = ""
        End If

        If cbbMediaType.Text.ToLower = "n" Then
            cbbMediaType.Text = "Visual Novel"
            Me.lblContentName.Focus()
        ElseIf cbbMediaType.Text.ToLower = "vi" Then
            cbbMediaType.Text = "Visual Novel"
            Me.lblContentName.Focus()
        ElseIf cbbMediaType.Text.ToLower = "an" Then
            cbbMediaType.Text = "Anime"
            Me.lblContentName.Focus()
        ElseIf cbbMediaType.Text.ToLower = "l" Then
            cbbMediaType.Text = "Light Novel"
            Me.lblContentName.Focus()
        ElseIf cbbMediaType.Text.ToLower = "w" Then
            cbbMediaType.Text = "web novel"
            Me.lblContentName.Focus()
        ElseIf cbbMediaType.Text.ToLower = "j" Then
            cbbMediaType.Text = "J-Drama"
            Me.lblContentName.Focus()
        ElseIf cbbMediaType.Text.ToLower = "t" Then
            cbbMediaType.Text = "Textbook"
            Me.lblContentName.Focus()
        ElseIf cbbMediaType.Text.ToLower = "vo" Then
            cbbMediaType.Text = "Vocabulary List"
            Me.lblContentName.Focus()
        ElseIf cbbMediaType.Text.Length = 5 Then
            cbbMediaType.Text = ""
        End If

    End Sub
    Private Sub cbbMediaType_Leave(sender As Object, e As EventArgs) Handles cbbMediaType.Leave, cbbSearchOrdering.Leave
        If cbbMediaType.Text.ToLower <> "anime" And cbbMediaType.Text.ToLower <> "visual novel" And cbbMediaType.Text.ToLower <> "visual novel" And cbbMediaType.Text.ToLower <> "light novel" And cbbMediaType.Text.ToLower <> "web novel" And cbbMediaType.Text.ToLower <> "j-drama" And cbbMediaType.Text.ToLower <> "textbook" And cbbMediaType.Text.ToLower <> "vocabulary list" Then
            cbbMediaType.Text = "All"
        End If
    End Sub
    Private Sub cbbSearchType_TextUpdate(sender As Object, e As EventArgs) Handles cbbSearchType.TextUpdate
        If cbbSearchType.Text.ToLower.Contains("k") Then
            cbbSearchType.Text = "Kanji"
            Me.lblContentName.Focus()
        Else
            cbbSearchType.Text = "Words"
        End If
    End Sub
    Private Sub cbbFilterType_TextUpdate(sender As Object, e As EventArgs) Handles cbbFilterType.TextUpdate
        If cbbFilterType.Text.ToLower.Contains("g") Then
            cbbFilterType.Text = "DeckGlobal"
            Me.lblContentName.Focus()
        ElseIf cbbFilterType.Text.ToLower.Contains("t") Then
            cbbFilterType.Text = "Time"
            Me.lblContentName.Focus()
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
    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        Try
            Clipboard.SetText(ContentList.Item(lbResults.SelectedIndex).Name & vbNewLine & "Unique Words: " & ContentList.Item(lbResults.SelectedIndex).UniqueWords & vbNewLine & "Used Once (%): " & ContentList.Item(lbResults.SelectedIndex).OncePercentage & vbNewLine & "Difficulty: " & ContentList.Item(lbResults.SelectedIndex).Difficulty)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub cbSearchReverse_CheckedChanged(sender As Object, e As EventArgs) Handles cbSearchReverse.CheckedChanged
        If tbxSearchBox.Text.Contains("https://") = True Then
            tbxSearchBox.Text = ""
        End If

        ContentList = SearchDecks(cbbMediaType.Text, cbbSearchOrdering.Text, Reverse, tbxSearchBox.Text)
        RefreshResults()
    End Sub
    Private Sub btnOwnDeck_Click(sender As Object, e As EventArgs) Handles btnOwnDeck.Click
        If OwnOpen = False Then
            Dim ScrapeOwnDeck As New FormOwnDeck
            ScrapeOwnDeck.Show()
            OwnOpen = True
        End If
    End Sub

    Sub RefreshResults()
        lbResults.Items.Clear()
        For Each Cont In ContentList
            lbResults.Items.Add(Cont.Name)
        Next
        lblResultCount.Show()
        lblResultCount.Text = "Results: " & lbResults.Items.Count
    End Sub

    Private Sub nbPageEnd_ValueChanged(sender As Object, e As EventArgs) Handles nbPageEnd.ValueChanged
        Try
            If nbPageEnd.Value > ContentList(lbResults.SelectedIndex).UniqueWords Then
                nbPageEnd.Value = ContentList(lbResults.SelectedIndex).UniqueWords
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class