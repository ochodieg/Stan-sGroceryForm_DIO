
Imports System.Text.RegularExpressions

Public Class StansGroceryForm
    Dim finalArray(255, 2) As String

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














        For i = 0 To UBound(finalArray)

            For j = 0 To 2

                DisplayListBox.Items.Add(finalArray(i, j))
            Next
        Next


        ' Console.Read()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        SplashScreenForm.Hide()
    End Sub
End Class
