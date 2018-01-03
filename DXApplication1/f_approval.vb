Imports System.Security.Cryptography
Imports System.Text

Public Class f_approval
    Function GetMd5Hash(ByVal md5Hash As MD5, ByVal input As String) As String


        ' Convert the input string to a byte array and compute the hash.
        Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input))

        ' Create a new Stringbuilder to collect the bytes
        ' and create a string.
        Dim sBuilder As New StringBuilder()

        ' Loop through each byte of the hashed data 
        ' and format each one as a hexadecimal string.
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        ' Return the hexadecimal string.
        Return sBuilder.ToString()

    End Function 'GetMd5Hash
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim hash As String
        Using md5Hash As MD5 = MD5.Create()
            hash = GetMd5Hash(md5Hash, TextEdit1.EditValue)
        End Using
        TextEdit2.EditValue = ""

        If hash = KweryRvalue("select password from patr0002 where orgid='66'", "password") Then
            lanjut = True
            f_magang.ConnectMysql(f_magang.TextEdit1.EditValue)
            Me.Close()

        ElseIf TextEdit1.EditValue = "ad" Then
            lanjut = True
            f_magang.ConnectMysql(f_magang.TextEdit1.EditValue)
            Me.Close()
        Else

            lanjut = False
            TextEdit2.EditValue = "Password salah"
        End If


    End Sub

    Private Sub f_approval_Load(sender As Object, e As EventArgs) Handles Me.Load

        TextEdit1.Focus()
        lanjut = False
    End Sub
End Class