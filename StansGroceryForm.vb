'Ivan Ochoa
'RCET 0265
'Fall 2020
'StansGrocery_DIO
'https://github.com/ochodieg/Stan-sGroceryForm_DIO
Option Explicit On
Option Strict On
Option Compare Binary
Imports System.Text.RegularExpressions
Public Class StansGroceryForm
    Dim finalArray(255, 2) As String
    Dim sortedaisle(16) As String
    Dim sortedCategory(23) As String
    Dim varSection As String
    'tried to do this another way
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
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        SplashScreenForm.Hide()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AisleRadioButton.CheckedChanged, CategoryRadioButton.CheckedChanged
        If AisleRadioButton.Checked = True Then
            FilterComboBox.Items.Clear()
            FilterComboBox.Items.Add("Show All")
            FilterComboBox.Items.Add("Choose Aisle...")
            FilterComboBox.SelectedItem = "Choose Aisle..."
            For i = 0 To UBound(sortedaisle)
                FilterComboBox.Items.Add(sortedaisle(i))
            Next
        Else
            FilterComboBox.Items.Clear()
            FilterComboBox.Items.Add("Show All")
            FilterComboBox.Items.Add("Choose Category...")
            FilterComboBox.SelectedItem = "Choose Category..."
            For j = 0 To UBound(sortedaisle)
                FilterComboBox.Items.Add(sortedCategory(j))
            Next
        End If
        FilterComboBox.Items.Remove("")
    End Sub
    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim file As String = My.Resources.
        SplashScreenForm.BackgroundImageLayout = ImageLayout.Stretch
        SplashScreenForm.BackgroundImage = My.Resources.stans_grocery_splashtop
        SplashScreenForm.Size = Me.Size
        SplashScreenForm.Show()
        Me.Show()
        Dim matchArray As Match
        Dim secondreplace, stringReplace, string3 As String
        Dim arrayOne() As String
        Dim arrayTwo() As String
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
        Dim arrayThree(integer1 - 1, 2) As String
        integer2 = 0
        For i = 0 To UBound(arrayTwo)
            matchArray = Regex.Match(arrayTwo(i), "ITM")
            If matchArray.Success = True Then
                arrayThree(integer2, 0) = arrayTwo(i)
                integer2 += 1
            End If
        Next
        For i = 0 To UBound(arrayTwo)
            matchArray = Regex.Match(arrayTwo(i), "LOC")
            If matchArray.Success = True Then
                arrayThree(integer3, 1) = arrayTwo(i)
                integer3 += 1
            End If
        Next
        For i = 0 To UBound(arrayTwo)
            matchArray = Regex.Match(arrayTwo(i), "CAT")
            If matchArray.Success = True Then
                arrayThree(integer4, 2) = arrayTwo(i)
                integer4 += 1
            End If
        Next
        For i = 0 To UBound(arrayThree) - 1
            For j = 0 To 2
                arrayThree(i, j) = Regex.Replace(arrayThree(i, j), "λ", "/")
                arrayThree(i, j) = Regex.Replace(arrayThree(i, j), "ITM", "")
                arrayThree(i, j) = Regex.Replace(arrayThree(i, j), "CAT", "")
                arrayThree(i, j) = Regex.Replace(arrayThree(i, j), "LOC", "")
            Next
        Next
        finalArray = arrayThree
        AisleSorter()
        CategorySorter()
        FilterComboBox.SelectedItem = "Show All"
    End Sub
    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Dim variable As Boolean
        Dim search As Match
        variable = False
        DisplayListBox.Items.Clear()
        DisplayLabel.Text = String.Empty
        If SearchTextBox.TextLength = 1 Then
            DisplayLabel.Text = "Please be more specific."
            Exit Sub
        ElseIf SearchTextBox.Text = "zzz" Then
            Me.Close()
        End If
        For i = 0 To UBound(finalArray) - 1
            search = Regex.Match(finalArray(i, 0), "\b" & SearchTextBox.Text, RegexOptions.IgnoreCase)
            If search.Success = True Then
                DisplayListBox.Items.Add(finalArray(i, 0))
                variable = True
            End If
        Next
        If variable = False Then
            DisplayLabel.Text = $"Sorry, no matches for {SearchTextBox.Text}"
        End If
        DisplayListBox.Items.Remove("")
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
    Sub AisleSorter()
        Dim aisle(UBound(finalArray)) As String
        For i = 0 To UBound(finalArray)
            aisle(i) = finalArray(i, 1)
        Next
        Dim preDedupe As String = String.Join(",", aisle)
        Dim dedupe As String = DeDupeinator(preDedupe)
        sortedaisle = Regex.Split(dedupe, ",")
        Array.Sort(sortedaisle)
        Console.Read()
    End Sub
    Function DeDupeinator(ByVal sInput As String, Optional ByVal sDelimiter As String = ",") As String
        Dim varSection, sTemp As String
        For Each varSection In Split(sInput, sDelimiter)
            If InStr(1, sDelimiter & sTemp & sDelimiter, sDelimiter & varSection & sDelimiter, vbTextCompare) = 0 Then
                sTemp = sTemp & sDelimiter & varSection
            End If
        Next varSection
        DeDupeinator = Mid(sTemp, Len(sDelimiter) + 1)
    End Function
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
End Class