Public Class Passw
    Private Sub Passw_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextEdit1.Text = nik
    End Sub

    Private Sub WindowsUIButtonPanel1_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles WindowsUIButtonPanel1.ButtonClick
        If TextEdit3.EditValue <> TextEdit4.EditValue Then
            MsgBox("New Password did not match , Please Retype ")
            Exit Sub
        ElseIf TextEdit4.EditValue = "" Then
            MsgBox("Password null is not allowed , Please Retype ")
            Exit Sub
        End If

        If (Class1.MysqlQuery("select fullname from patr0002 where idnik = '" & TextEdit1.EditValue & "' and password = md5('" & TextEdit2.EditValue & "')")) <> "" Then
            Class1.MysqlUIDNew("update patr0002 set password = md5('" & TextEdit4.EditValue & "') where idnik = '" & TextEdit1.EditValue & "'")
            MsgBox("Change Password Success")
        Else

            MsgBox("Old Password did not match, Failed To Change")

        End If

    End Sub
End Class