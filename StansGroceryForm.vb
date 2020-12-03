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
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        SplashScreenForm.BackgroundImageLayout = ImageLayout.Stretch
        SplashScreenForm.BackgroundImage = My.Resources.stans_grocery
        SplashScreenForm.Size = Me.Size
        SplashScreenForm.Show()
        Me.Show()
        Dim secondreplace As String = Regex.Replace(My.Resources.Grocery, "/", "λ")
        Dim stringReplace As String = Regex.Replace(secondreplace, "\p{P}|\p{Sc}", String.Empty)
        Dim arrayOne() As String = Regex.Split(stringReplace, vbLf)
        Array.Sort(arrayOne)
        Dim stringJoin As String = String.Join(String.Empty, arrayOne)
        Dim arrayTwo() As String = Regex.Split(stringJoin, "(?=ITM)|(?=LOC)|(?=CAT)")
        Dim match As Match
        Dim poop As Integer
        'Dim integer1, integer2, integer3, integer4 As Integer
        poop = 0
        For i = 0 To UBound(arrayTwo)
            match = Regex.Match(arrayTwo(i), "ITM")
            If match.Success = True Then
                finalArray(poop, 0) = arrayTwo(i)
                poop += 1
            End If
        Next
        poop = 0
        For i = 0 To UBound(arrayTwo)
            match = Regex.Match(arrayTwo(i), "LOC")
            If match.Success = True Then
                finalArray(poop, 1) = arrayTwo(i)
                poop += 1
            End If
        Next
        poop = 0
        For i = 0 To UBound(arrayTwo)
            match = Regex.Match(arrayTwo(i), "CAT")
            If match.Success = True Then
                finalArray(poop, 2) = arrayTwo(i)
                poop += 1

            End If
        Next
        For row = 0 To UBound(finalArray)
            For collum = 0 To 2
                finalArray(row, collum) = Regex.Replace(finalArray(row, collum), "λ", "/")
                finalArray(row, collum) = Regex.Replace(finalArray(row, collum), "ITM", "")
                finalArray(row, collum) = Regex.Replace(finalArray(row, collum), "CAT", "")
                finalArray(row, collum) = Regex.Replace(finalArray(row, collum), "LOC", "")
            Next
        Next
        ' arrayFour = finalArray
        ' aisleSorter()
        ' CatSort()
        ' FilterComboBox.SelectedItem = "Show All"







        For i = 0 To UBound(finalArray)

            For j = 0 To 2


                DisplayListBox.Items.Add(finalArray(i, j))
            Next
        Next


        ' Console.Read()

    End Sub

    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Dim goodData As Boolean
        Dim searchMatch As Match
        goodData = False
        DisplayListBox.Items.Clear()
        DisplayLabel.Text = String.Empty
        If SearchTextBox.TextLength = 1 Then
            DisplayLabel.Text = "Please be more specific."
            Exit Sub
        ElseIf SearchTextBox.Text = "zzz" Then
            Me.Close()
        End If
        For i = 0 To UBound(finalArray) - 1
            searchMatch = Regex.Match(finalArray(i, 0), "\b" & SearchTextBox.Text, RegexOptions.IgnoreCase)
            If searchMatch.Success = True Then
                DisplayListBox.Items.Add(finalArray(i, 0))
                goodData = True
            End If
        Next
        If goodData = False Then
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

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AisleRadioButton.CheckedChanged, CategoryRadioButton.CheckedChanged
        If AisleRadioButton.Checked = True Then
            FilterComboBox.Items.Clear()
            FilterComboBox.Items.Add("Show All")
            FilterComboBox.Items.Add("Choose Aisle...")
            FilterComboBox.SelectedItem = "Choose Aisle..."
            For i = 0 To UBound(sortedAisle)
                FilterComboBox.Items.Add(sortedAisle(i))
            Next
        Else
            FilterComboBox.Items.Clear()
            FilterComboBox.Items.Add("Show All")
            FilterComboBox.Items.Add("Choose Category...")
            FilterComboBox.SelectedItem = "Choose Category..."
            For j = 0 To UBound(sortedAisle)
                FilterComboBox.Items.Add(sortedCategory(j))
            Next
        End If
        FilterComboBox.Items.Remove("")
    End Sub








    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        SplashScreenForm.Hide()
    End Sub

End Class
