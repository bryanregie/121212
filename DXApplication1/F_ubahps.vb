Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

Public Class F_ubahps
    Dim WithEvents cb As RepositoryItemComboBox = New RepositoryItemComboBox()
    Dim WithEvents cbDrop As RepositoryItemComboBox = New RepositoryItemComboBox()
    Dim konsenawal As String
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        AddHandler GridView1.CustomDrawRowIndicator, AddressOf Class1.GridNumbering
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Sub lod()
        cb.Items.Clear()
        cb.Items.AddRange({"PS", "WK", "PIL", "TAM"})


        cbDrop.Items.Clear()
        cbDrop.Items.AddRange({"Yes", "No"})


        'Class1.LoadGrid(GridControl1, "Select idtstr1030,idnim as NIM,idprogdi as Progdi,fullname as Name, semester as Semester,idmakul as Kode,makul as MataKuliah,sks,nilgrade as Grade,nilbobot as Bobot,case flagnotused when 0 then 'No' else 'Yes' end as 'Drop',case flagpil when 2 then 'WK' when 3 then 'PIL' else 'PS' end as flagpil,flagpil,flagpil,'' as edited  from tstr1030 where idnim='22110253' and idthnaka='2013/2014'")
        'Class1.LoadGrid(GridControl1, "Select idtstr1030,idnim as NIM,idprogdi as Progdi,fullname as Name, semester as Semester,idmakul as Kode,makul as MataKuliah,sks,nilgrade as Grade,nilbobot as Bobot,case flagnotused when 1 then 'Yes' else 'No' end as 'Drop',case flagpil when 2 then 'WK' when 3 then 'PIL' when 4 then 'TAM' else 'PS' end as flagpil,coalesce( flagpil,1) as flagpil1,coalesce(flagpil,1) as flagpil2,'' as edited,flagnotused ,flagnotused as flagnotused2 from tstr1030 where idnim= '" & Class1.strnim & "' ", True, True)
        Class1.LoadGrid(GridControl1, "Select idtstr1030, semester as Semester,idmakul as Kode,makul as MataKuliah,sks,nilgrade as Grade,nilbobot as Bobot,case flagnotused when 1 then 'Yes' else 'No' end as 'Drop',case flagpil when 2 then 'WK' when 3 then 'PIL' when 4 then 'TAM' else 'PS' end as flagpil,coalesce( flagpil,1) as flagpil1,coalesce(flagpil,1) as flagpil2,'' as edited,flagnotused ,flagnotused as flagnotused2 from tstr1030 where idnim= '" & Class1.strnim & "' ", True, True)

        GridView1.OptionsBehavior.Editable = True
        For Each column As GridColumn In GridView1.Columns
            column.OptionsColumn.AllowEdit = False
        Next

        GridView1.Columns("flagpil").OptionsColumn.AllowEdit = True
        GridView1.Columns("flagpil").ColumnEdit = cb

        GridView1.Columns("Drop").OptionsColumn.AllowEdit = True
        GridView1.Columns("Drop").ColumnEdit = cbDrop

        GridView1.Columns("idtstr1030").Visible = False
        GridView1.Columns("flagpil1").Visible = False
        GridView1.Columns("flagpil2").Visible = False
        GridView1.Columns("edited").Visible = False
        GridView1.Columns("flagnotused").Visible = False
        GridView1.Columns("flagnotused2").Visible = False

        TextEdit1.EditValue = Class1.strnim
        TextEdit2.EditValue = Class1.strname

        Class1.LoadLook(LookUpEdit1, "select idkonsen as 'Kode',mekonsen as 'Konsentrasi' from cmdkonsen where idprogdi ='" & KweryRvalue("select idprogdi from satr9001 where idnim ='" & Class1.strnim & "'", "idprogdi") & "'", 1, 0)

        LookUpEdit1.EditValue = KweryRvalue("select idkonsen from satr9001 where idnim ='" & Class1.strnim & "'", "idkonsen")
        konsenawal = KweryRvalue("select idkonsen from satr9001 where idnim ='" & Class1.strnim & "'", "idkonsen")

    End Sub

    Private Sub cb_EditValueChanged(sender As Object, e As EventArgs) Handles cb.EditValueChanged


        Dim edit As ComboBoxEdit = CType(sender, ComboBoxEdit)
        Dim fp As Integer
        If edit.EditValue = "PIL" Then
            fp = 3
        ElseIf edit.EditValue = "WK" Then
            fp = 2
        ElseIf edit.EditValue = "TAM" Then
            fp = 4
        Else
            fp = 1
        End If

        GridView1.SetFocusedRowCellValue("flagpil", edit.EditValue)

        GridView1.SetFocusedRowCellValue("flagpil2", fp)

        If GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "flagpil1") <> GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "flagpil2") Then
            GridView1.SetFocusedRowCellValue("edited", 1)
        Else
            GridView1.SetFocusedRowCellValue("edited", "")
        End If

        If fp = 4 Then
            GridView1.SetFocusedRowCellValue("flagnotused2", 1)
        End If

        'If GridView1.GetFocusedRowCellValue("flagpil1") <> GridView1.GetFocusedRowCellValue("flagpil2") Then
        '    GridView1.FocusedRowHandle
        'End If
    End Sub

    Private Sub cbDrop_EditValueChanged(sender As Object, e As EventArgs) Handles cbDrop.EditValueChanged
        Dim edit As ComboBoxEdit = CType(sender, ComboBoxEdit)
        Dim fp As Integer
        If edit.EditValue = "Yes" Then
            fp = 1
        Else
            fp = 0
        End If

        GridView1.SetFocusedRowCellValue("Drop", edit.EditValue)
        GridView1.SetFocusedRowCellValue("flagnotused2", fp)

        If GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "flagnotused") <> GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "flagnotused2") Then
            GridView1.SetFocusedRowCellValue("edited", 1)
        Else
            GridView1.SetFocusedRowCellValue("edited", "")
        End If
    End Sub

    Private Sub F_ubahps_Load(sender As Object, e As EventArgs) Handles Me.Load
        lod()
    End Sub



    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        lod()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Dim ubah As Integer
        ubah = 0
        For i = 0 To GridView1.RowCount - 1
            If GridView1.GetRowCellValue(i, "edited") = "1" Then
                ubah = ubah + 1
                Class1.MysqlUIDNew("update tstr1030 set flagnotused= '" & GridView1.GetRowCellValue(i, "flagnotused2") & "',flagpil= '" & GridView1.GetRowCellValue(i, "flagpil2") & "', idactor ='" & Class1.iduser & "', idsource ='" & Class1.idpcname & "', dtchange='" & Now.ToString("yyyy-MM-dd hh:mm:ss") & "' where idtstr1030 ='" & GridView1.GetRowCellValue(i, "idtstr1030") & "'")
            End If
        Next

        If ubah > 0 Then Class1.MysqlUIDNew("sp_updateipk('" & Class1.strnim & "')")

        If konsenawal <> LookUpEdit1.EditValue Then Class1.MysqlUIDNew("update satr9001 set idkonsen= '" & LookUpEdit1.EditValue & "' where idnim ='" & Class1.strnim & "'")



        If ubah = 0 And konsenawal <> LookUpEdit1.EditValue Then
            MsgBox("update Sukses ubah konsentrasi")
        ElseIf ubah > 0 And konsenawal = LookUpEdit1.EditValue Then
            MsgBox("update Sukses " & ubah & " Matakuliah")


        ElseIf ubah > 0 And konsenawal <> LookUpEdit1.EditValue Then
            MsgBox("update Sukses " & ubah & " Matakuliah , dan ubah konsentrasi")

        End If
    End Sub
End Class