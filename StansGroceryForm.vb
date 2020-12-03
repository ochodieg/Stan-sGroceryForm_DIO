'Ivan Ochoa
'RCET 0265
'Fall 2020
'StansGrocery_DIO
Option Explicit On
Option Strict On
Option Compare Binary
Imports System.Text.RegularExpressions
Public Class StansGroceryForm
    Dim finalArray(255, 2) As String
    Dim aisle(16) As String
    Dim sortedCategory(23) As String
    Dim varSection, sTemp As String
    'Dim poop As Integer
    '    'Dim integer1, integer2, integer3, integer4 As Integer
    '    poop = 0
    '    For i = 0 To UBound(arrayTwo)
    '        match = Regex.Match(arrayTwo(i), "ITM")
    '        If match.Success = True Then
    '            finalArray(poop, 0) = arrayTwo(i)
    '            poop += 1
    '        End If
    'Next
    '    poop = 0
    '    For i = 0 To UBound(arrayTwo)
    '        match = Regex.Match(arrayTwo(i), "LOC")
    '        If match.Success = True Then
    '            finalArray(poop, 1) = arrayTwo(i)
    '            poop += 1
    '        End If
    'Next
    '    poop = 0
    '    For i = 0 To UBound(arrayTwo)
    '        match = Regex.Match(arrayTwo(i), "CAT")
    '        If match.Success = True Then
    '            finalArray(poop, 2) = arrayTwo(i)
    '            poop += 1

    '        End If
    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        SplashScreenForm.Hide()
    End Sub
    Private Sub DisplayListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DisplayListBox.SelectedIndexChanged
        For i = 0 To 255
            For j = 0 To 2
                If DisplayListBox.SelectedItem.ToString = finalArray(i, j) Then
                    DisplayLabel.Text = "You will find " & finalArray(i, j) & " on aisle " &
                        finalArray(i, j + 1) & " with the " & finalArray(i, j + 2)
                End If
            Next
        Next
    End Sub
    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Dim variable As Boolean
        Dim searchMatch As Match
        variable = False
        DisplayListBox.Items.Clear()
        DisplayLabel.Text = String.Empty
        If SearchTextBox.TextLength = 1 Then
            DisplayLabel.Text = "Please be more specific."
            Exit Sub
        ElseIf SearchTextBox.Text = "xxx" Then
            Me.Close()
        End If
        For i = 0 To UBound(finalArray) - 1
            searchMatch = Regex.Match(finalArray(i, 0), "\b" & SearchTextBox.Text, RegexOptions.IgnoreCase)
            If searchMatch.Success = True Then
                DisplayListBox.Items.Add(finalArray(i, 0))
                variable = True
            End If
        Next
        If variable = False Then
            DisplayLabel.Text = $"Sorry, no matches for {SearchTextBox.Text}"
        End If
        DisplayListBox.Items.Remove("")
    End Sub
    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim file As String = My.Resources.stans_grocery
        SplashScreenForm.BackgroundImageLayout = ImageLayout.Stretch
        SplashScreenForm.BackgroundImage = My.Resources.stans_grocery
        SplashScreenForm.Size = Me.Size
        SplashScreenForm.Show()
        Me.Show()
        Dim matchArray As Match
        Dim secondreplace, stringReplace, string3 As String
        Dim arrayOne(), arrayTwo() As String
        Dim integer1, integer2, integer3, integer4 As Integer
        secondreplace = Regex.Replace(My.Resources.Grocery, "/", "λ")
        stringReplace = Regex.Replace(secondreplace, "\p{P}|\p{Sc}", "")
        arrayOne = Regex.Split(stringReplace, vbLf)
        Array.Sort(arrayOne)
        string3 = String.Join("", arrayOne)
        arrayTwo = Regex.Split(string3, "(?=ITM)|(?=CAT)|(?=LOC)")
        For i = 0 To UBound(arrayTwo)
            matchArray = Regex.Match(arrayTwo(i), "ITM")
            If matchArray.Success = True Then
                integer1 += 1
            End If
        Next
        Dim array3(integer1 - 1, 2) As String
        integer2 = 0
        For i = 0 To UBound(arrayTwo)
            matchArray = Regex.Match(arrayTwo(i), "ITM")
            If matchArray.Success = True Then
                array3(integer2, 0) = arrayTwo(i)
                integer2 += 1
            End If
        Next
        For i = 0 To UBound(arrayTwo)
            matchArray = Regex.Match(arrayTwo(i), "LOC")
            If matchArray.Success = True Then
                array3(integer3, 1) = arrayTwo(i)
                integer3 += 1
            End If
        Next
        For i = 0 To UBound(arrayTwo)
            matchArray = Regex.Match(arrayTwo(i), "CAT")
            If matchArray.Success = True Then
                array3(integer4, 2) = arrayTwo(i)
                integer4 += 1
            End If
        Next
        For i = 0 To UBound(array3) - 1
            For j = 0 To 2
                array3(i, j) = Regex.Replace(array3(i, j), "λ", "/")
                array3(i, j) = Regex.Replace(array3(i, j), "ITM", "")
                array3(i, j) = Regex.Replace(array3(i, j), "CAT", "")
                array3(i, j) = Regex.Replace(array3(i, j), "LOC", "")
            Next
        Next
        finalArray = array3
        AisleSorter()
        CategorySorter()
        FilterComboBox.SelectedItem = "Show All"
    End Sub
    Sub AisleSorter()
        Dim aisle(UBound(finalArray)) As String
        For i = 0 To UBound(finalArray)
            aisle(i) = finalArray(i, 1)
        Next
        Dim preDedupe As String = String.Join(",", aisle)
        Dim dedupe As String = DeDupeinator(preDedupe)
        aisle = Regex.Split(dedupe, ",")
        Array.Sort(aisle)
        Console.Read()
    End Sub
    Private Sub FilterComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FilterComboBox.SelectedIndexChanged
        FilterComboBox.SelectedItem.ToString()
        DisplayListBox.Items.Clear()
        For i = 0 To 255
            If FilterComboBox.SelectedItem.ToString() = "Show All" Then
                DisplayListBox.Items.Add(finalArray(i, 0))
            End If
            For j = 0 To 2
                If FilterComboBox.SelectedItem.ToString() = finalArray(i, j) Then
                    DisplayListBox.Items.Add(finalArray(i, 0))
                End If
            Next
        Next
        DisplayListBox.Items.Remove("")
    End Sub


    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AisleRadioButton.CheckedChanged, CategoryRadioButton.CheckedChanged
        If AisleRadioButton.Checked = True Then
            FilterComboBox.Items.Clear()
            FilterComboBox.Items.Add("Show All")
            FilterComboBox.Items.Add("Choose Aisle")
            FilterComboBox.SelectedItem = "Choose Aisle"
            For i = 0 To UBound(aisle)
                FilterComboBox.Items.Add(aisle(i))
            Next
        Else
            FilterComboBox.Items.Clear()
            FilterComboBox.Items.Add("Show All")
            FilterComboBox.Items.Add("Select a Category")
            FilterComboBox.SelectedItem = "Select a Category"
            For j = 0 To UBound(aisle)
                FilterComboBox.Items.Add(sortedCategory(j))
            Next
        End If
        FilterComboBox.Items.Remove("")
    End Sub


    Sub CategorySorter()
        Dim category(UBound(finalArray)) As String
        For i = 0 To UBound(finalArray)
            category(i) = finalArray(i, 2)
        Next
        Dim preDedupe As String = String.Join(",", category)
        Dim dedupe As String = DeDupeinator(preDedupe)
        sortedCategory = Regex.Split(dedupe, ",")
        Array.Sort(sortedCategory)
        Console.Read()
    End Sub

    Function DeDupeinator(ByVal sInput As String, Optional ByVal sDelimiter As String = ",") As String

        For Each varSection In Split(sInput, sDelimiter)
            If InStr(1, sDelimiter & sTemp & sDelimiter, sDelimiter & varSection & sDelimiter, vbTextCompare) = 0 Then
                sTemp = sTemp & sDelimiter & varSection
            End If
        Next varSection
        DeDupeinator = Mid(sTemp, Len(sDelimiter) + 1)
    End Function


End Class

