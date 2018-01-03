Public Class f_judulskripsi
    Private Sub f_judulskripsi_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextEdit1.EditValue = tran_nim
        TextEdit2.EditValue = tran_nama
        MemoEdit1.EditValue = tran_sind
        MemoEdit2.EditValue = tran_sing
    End Sub

    Private Sub WindowsUIButtonPanel1_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles WindowsUIButtonPanel1.ButtonClick
        Class1.note = "Merubah judul skripsi menjadi : " & MemoEdit1.EditValue
        Class1.MysqlUIDNew("insert into satr0020 (fullname,idnim,idprogdi,idthnaka,idevent,idsemest,nosurat,idactor,status,note,type,idprogm,pejabat,ipaddress) values " &
        "('" & Class1.strname & "','" & Class1.strnim & "','" & Class1.idprogdi & "','" & Class1.idthnaka & "','" & Class1.idevent & "','" & Class1.idsemest & "','" & Class1.nosurat & "','" & Class1.idactor & "','" & Class1.status & "','" & Class1.note & "','" & Class1.stype & "','" & Class1.idprogm & "','" & Class1.pejabat & "','" & Class1.ipaddress & "')")

        If (Class1.MysqlQuery("select * from tstr7000 where idnim='" & TextEdit1.EditValue & "' and (makul like '%skripsi%' or makul like '%business plan%')")) = "" Then
            MsgBox("kosong")
        Else
            MsgBox("Isi")
        End If



        Class1.MysqlUIDNew("update tstr7000 set judul ='" & MemoEdit1.EditValue & "' , judulinggris ='" & MemoEdit2.EditValue & "' where idnim='" & TextEdit1.EditValue & "' and (makul like '%skripsi%' or makul like '%business plan%')")
        Me.Close()
    End Sub

    Private Sub WindowsUIButtonPanel1_Click(sender As Object, e As EventArgs) Handles WindowsUIButtonPanel1.Click

    End Sub
End Class