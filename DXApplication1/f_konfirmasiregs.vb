Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports DevExpress.XtraReports.UI
Public Class f_konfirmasiregs
 ' Dim myConnectionString As String


    'Dim conn As MySql.Data.MySqlClient.MySqlConnection
    Dim dA As MySqlDataAdapter
    Dim dA2, dA3 As MySqlDataAdapter
    Dim typemhs As String = ""

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Konek()
        ''MsgBox(KweryRvalue("select todate from cmdpkk", "todate"))
        'DateEdit1.EditValue = KweryRvalue("select fromdate from cmdpkk where thakad='" & LookUpEdit2.EditValue & "'", "fromdate")
        'DateEdit2.EditValue = KweryRvalue("select todate from cmdpkk where  thakad='" & LookUpEdit2.EditValue & "'", "todate")
        'DateEdit3.EditValue = KweryRvalue("select mulaikul from cmdpkk where  thakad='" & LookUpEdit2.EditValue & "'", "mulaikul")



        'Class1.detform(DateEdit1)
        'Class1.detform(DateEdit2)
        'Class1.detform(DateEdit3)
        'Class1.detform(DateEdit4)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ConnectMysql(nim As String)


        ''myConnectionString = "server=172.16.0.12;port=3306;user id=fingerprint; password=fingerprint2016; database=iap_db;persist security info=true;CharSet=utf8;convert zero datetime=True; "
        conn = New MySql.Data.MySqlClient.MySqlConnection()


        Try
            conn.ConnectionString = Class1.myConnectionString
            conn.Open()
        Catch ex As Exception

            MsgBox(ex.Message)
            Return
        End Try


        dA = New MySqlDataAdapter("call iap_db.sp_transkripheader2018('" & nim & "','%%','%%','%%')", conn)

        Dim DS As New DataSet()

        dA.Fill(DS)
        GridControl1.DataSource = DS.Tables(0)

        TextEdit2.EditValue = ""
        If (DS.Tables(0).Rows.Count) > 0 Then
            TextEdit2.EditValue = StrConv(DS.Tables(0).Rows(0)("fullname").ToString, VbStrConv.ProperCase)
        End If
        'MsgBox("select * from cmdijazah where kode ='ijazah_" & DS.Tables(0).Rows(0)("shprogdi").ToString & "'")
        If GridView1.RowCount <= 0 Then




            'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False
            MsgBox("NIM Tidak terdaftar, Mohon Periksa kembali")
            Exit Sub
        Else
            WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = True
        End If


        '=============
        'dA2 = New MySqlDataAdapter("select * from cmdijazah where kode ='konf_reg2'", conn)

        'Dim DS2 As New DataSet()

        'dA2.Fill(DS2)
        'GridControl2.DataSource = DS2.Tables(0)

        If KweryRvalue("select idtypemhs from satr9001  where idnim='" & TextEdit1.EditValue & "'", "idtypemhs") = "R" Then
            typemhs = "R"
            KweryGrid("select * from cmdijazah where kode ='konf_reg2'", GridControl2)
        Else
            typemhs = "K"
            KweryGrid("select * from cmdijazah where kode ='konf_regk'", GridControl2)
        End If




        '===========================
        dA3 = New MySqlDataAdapter("select *,now() as tgl from cmdnodok where kodedok ='SKRG'", conn)

        Dim DS3 As New DataSet()

        dA3.Fill(DS3)
        Dim a1, a2, a3, a4, a5 As String
        TextEdit3.EditValue = ""
        If (DS3.Tables(0).Rows.Count) > 0 Then
            'a1 = Microsoft.VisualBasic.Right("0000" & DS3.Tables(0).Rows(0)("nourut").ToString(), 4)
            a1 = Microsoft.VisualBasic.Right(GridView1.GetRowCellValue(0, "idnim"), 4)
            a2 = "IBIKKG-BAAK"
            a3 = DS3.Tables(0).Rows(0)("prefix").ToString
            a4 = bln(Month(DS3.Tables(0).Rows(0)("tgl")))
            a5 = Microsoft.VisualBasic.Right(Year(DS3.Tables(0).Rows(0)("tgl")), 2)
            TextEdit3.EditValue = a1 & "/" & a2 & "/" & a3 & "/" & a4 & "/" & a5 'StrConv("SKcB", VbStrConv.ProperCase)
        End If


    End Sub
    Private Sub TextEdit1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextEdit1.KeyUp
        If e.KeyCode = Keys.Enter Then

            'If CInt(KweryRvalue("select count(idtstr1010) from tstr1010 where idnim='" & TextEdit1.EditValue & "'", "count(idtstr1010)")) > 0 Then MsgBox("Mohon Periksa Kembali, NIm tersebut bukan mahasiswa baru") : Exit Sub
            ConnectMysql(TextEdit1.EditValue.ToString.Trim)
            'If CInt(KweryRvalue("select count(idtstr1010) from tstr1010 where idnim='" & TextEdit1.EditValue & "'", "count(idtstr1010)")) > 0 Then
            '    'heryr add aproval from
            '    Exit Sub
            'Else

            '    ConnectMysql(TextEdit1.EditValue.ToString.Trim)
            'End If


        End If

    End Sub

    Sub tglpkk()
        Me.V_pejabatTableAdapter.Fill(Me.Iap_dbDataSet.v_pejabat)
        'DateEdit1.EditValue = Now()
        'DateEdit2.EditValue = Now()
        'DateEdit3.EditValue = Now()
        DateEdit4.EditValue = Now()
        TextEdit2.ReadOnly = True
        LayoutControlItem1.ContentVisible = False
        'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False
        LookUpEdit1.ItemIndex = 0

        'Try
        Class1.LoadLook(LookUpEdit2, "select thakad as `Thn Akademik` from cmdpkk order by active desc,thakad")
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        LookUpEdit2.ItemIndex = 0

        If typemhs = "" Then typemhs = "R"

        'MsgBox("select fromdate from cmdpkk where thakad='" & LookUpEdit2.EditValue & "' and typemhs='" & typemhs & "' and active=1 ")

        DateEdit1.EditValue = KweryRvalue("select fromdate from cmdpkk where thakad='" & LookUpEdit2.EditValue & "' and typemhs='" & typemhs & "' and active=1 ", "fromdate")
        DateEdit2.EditValue = KweryRvalue("select todate from cmdpkk where  thakad='" & LookUpEdit2.EditValue & "' and typemhs='" & typemhs & "' and active=1 ", "todate")
        DateEdit3.EditValue = KweryRvalue("select mulaikul from cmdpkk where  thakad='" & LookUpEdit2.EditValue & "' and typemhs='" & typemhs & "' and active=1 ", "mulaikul")



        Class1.detform(DateEdit1)
        Class1.detform(DateEdit2)
        Class1.detform(DateEdit3)
        Class1.detform(DateEdit4)

    End Sub

    Private Sub WindowsUIButtonPanel1_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles WindowsUIButtonPanel1.ButtonClick

        DateEdit1.DoValidate()
        DateEdit2.DoValidate()
        DateEdit3.DoValidate()
        DateEdit4.DoValidate()
        ConnectMysql(TextEdit1.EditValue.ToString.Trim)

        If GridView1.RowCount <= 0 Then Exit Sub
        'If CInt(KweryRvalue("select count(idtstr1010) from tstr1010 where idnim='" & TextEdit1.EditValue & "'", "count(idtstr1010)")) > 0 Then MsgBox("Mohon Periksa Kembali, NIm tersebut bukan mahasiswa baru") : Exit Sub

        'pejabat
        Dim row As DataRowView = CType(LookUpEdit1.Properties.GetDataSourceRowByKeyValue(LookUpEdit1.EditValue), DataRowView)
        Dim vpejabat As String = row("fullname")
        Dim vjabatan_ind As String = row("org_desc")
        Dim vjabatan_eng As String = row("jabatan")

        Try
            Class1.strname = TextEdit2.EditValue
            Class1.strnim = TextEdit1.EditValue
            Class1.idprogdi = GridView1.GetRowCellValue(0, "idprogdi")
            Class1.idthnaka = GridView1.GetRowCellValue(0, "idthnaka")
            Class1.idevent = GridView1.GetRowCellValue(0, "idevent")
            Class1.idsemest = GridView1.GetRowCellValue(0, "idsemest").ToString
            Class1.nosurat = TextEdit3.EditValue
            Class1.idactor = yser
            Class1.status = ""
            Class1.note = ""
            Class1.stype = "Konfirmasi Registrasi"
            Class1.idprogm = GridView1.GetRowCellValue(0, "idprogm")
            Class1.pejabat = vpejabat
            Class1.ipaddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try




        Select Case e.Button.Properties.Tag


            Case 1

                If GridView1.RowCount <= 0 Then Exit Sub
                tglpkk()
                LookUpEdit2.EditValue = KweryRvalue("select idthnaka from satr9001 where idnim='" & TextEdit1.EditValue & "'", "idthnaka")
                'Call gantithn()

                PanelControl2.Controls.Clear()
                Try
                    Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_konfirmasiregistrasi.repx", True)

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                Dim Tool As ReportPrintTool = New ReportPrintTool(class1.report)
                Dim rtb As New System.Windows.Forms.RichTextBox()

                Dim dbirth As DateTime = GridView1.GetRowCellValue(0, "dtbirth")

                'MsgBox(DateEdit1.EditValue)
                Dim dt1 As DateTime = DateEdit1.EditValue
                Dim dt2 As DateTime = DateEdit2.EditValue
                Dim dt3 As DateTime = DateEdit3.EditValue
                Dim dt4 As DateTime = DateEdit4.EditValue


                Dim pkm As String


                If dt1.Date = dt2.Date Then
                    pkm = (CInt(dt2.ToString("dd", indo)) & "  " & dt2.ToString("MMMM", indo) & "  " & dt2.ToString("yyyy", indo))
                ElseIf dt1.Month = dt2.Month Then

                    pkm = CInt(dt1.ToString("dd", indo)) & " - " & CInt(dt2.ToString("dd", indo)) & "  " & dt2.ToString("MMMM", indo) & "  " & dt2.ToString("yyyy", indo)

                Else

                    pkm = CInt(dt1.ToString("dd", indo)) & "  " & dt1.ToString("MMMM", indo) & " s/d " & CInt(dt2.ToString("dd", indo)) & "  " & dt2.ToString("MMMM", indo) & "  " & dt2.ToString("yyyy", indo)
                End If

                Class1.report.Parameters("par_tanggal1").Value = CInt(dt4.ToString("dd", indo)) & "  " & dt4.ToString("MMMM", indo) & "  " & dt4.ToString("yyyy", indo)


                rtb.Clear()
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                'textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                'textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                'textOut(vbNewLine + vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))


                textOut("Jakarta, " & CInt(dt4.ToString("dd", indo)) & "  " & dt4.ToString("MMMM", indo) & "  " & dt4.ToString("yyyy", indo) + vbNewLine, rtb, New Font("Tahoma", 12))

                textOut(vbNewLine, rtb, New Font("Tahoma", 12))
                textOut("No" + vbTab + ": " + TextEdit3.EditValue + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut("Hal" + vbTab + ": " + GridView2.GetRowCellValue(0, "par1") + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(vbNewLine, rtb, New Font("Tahoma", 12))
                textOut("Kepada Yth " + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(StrConv(GridView1.GetRowCellValue(0, "fullname"), VbStrConv.ProperCase) + vbNewLine, rtb, New Font("Tahoma", 11, FontStyle.Bold))
                textOut(StrConv(GridView1.GetRowCellValue(0, "txadr1"), VbStrConv.ProperCase) + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(StrConv(GridView1.GetRowCellValue(0, "txkota"), VbStrConv.ProperCase) + "  " + GridView1.GetRowCellValue(0, "idzip") + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut("Dengan Hormat, " + vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(GridView2.GetRowCellValue(0, "par2") + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(GridView2.GetRowCellValue(0, "par3") + vbNewLine, rtb, New Font("Tahoma", 12)) 'adapun
                textOut(vbNewLine, rtb, New Font("Tahoma", 12))


                rtb.SelectionHangingIndent = 25




                textOut(vbTab + GridView2.GetRowCellValue(0, "par4") & "  ", rtb, New Font("Tahoma", 12))
                textOut(GridView1.GetRowCellValue(0, "idnim") + vbNewLine, rtb, New Font("Tahoma", 11, FontStyle.Bold))

                'textOut(vbNewLine, rtb, New Font("Tahoma", 10)) 'ddd

                If typemhs = "R" Then

                    textOut(vbTab & GridView2.GetRowCellValue(0, "par5").ToString.Trim & " ", rtb, New Font("Tahoma", 12))
                    textOut(pkm, rtb, New Font("Tahoma", 11, FontStyle.Bold))
                    textOut(",", rtb, New Font("Tahoma", 12))
                    textOut(vbNewLine + vbTab + "     " + GridView2.GetRowCellValue(0, "par6"), rtb, New Font("Tahoma", 12))

                    textOut(GridView2.GetRowCellValue(0, "par7") + " ", rtb, New Font("Tahoma", 12))
                    textOut(" http://www.kwikkiangie.ac.id. " + vbNewLine, rtb, New Font("Tahoma", 11, FontStyle.Bold))
                End If

               

                'disini2
                'textOut(vbNewLine, rtb, New Font("Tahoma", 10)) 'ddd
                textOut(vbTab + GridView2.GetRowCellValue(0, "par8"), rtb, New Font("Tahoma", 12))
                textOut(CInt(dt3.ToString("dd", indo)) & "  " & dt3.ToString("MMMM", indo) & "  " & dt3.ToString("yyyy", indo) + vbNewLine, rtb, New Font("Tahoma", 11, FontStyle.Bold))
                'textOut(vbNewLine, rtb, New Font("Tahoma", 10)) 'ddd 


                textOut(vbTab + GridView2.GetRowCellValue(0, "par9"), rtb, New Font("Tahoma", 12))
                textOut(" http://www.gmail.com ", rtb, New Font("Tahoma", 11, FontStyle.Bold))
                textOut(GridView2.GetRowCellValue(0, "par10"), rtb, New Font("Tahoma", 12))
                textOut("dengan" + vbTab + vbTab + vbTab + "username ", rtb, New Font("Tahoma", 12))
                textOut(GridView1.GetRowCellValue(0, "idnim") & GridView2.GetRowCellValue(0, "par24"), rtb, New Font("Tahoma", 11, FontStyle.Bold))
                textOut(GridView2.GetRowCellValue(0, "par11"), rtb, New Font("Tahoma", 12))
                textOut(GridView2.GetRowCellValue(0, "par25") + vbNewLine, rtb, New Font("Tahoma", 11, FontStyle.Bold))
                'textOut(vbNewLine, rtb, New Font("Tahoma", 10)) 'ddd

                textOut(vbTab + GridView2.GetRowCellValue(0, "par12"), rtb, New Font("Tahoma", 12))
                textOut(vbTab + GridView2.GetRowCellValue(0, "par21") + " ", rtb, New Font("Tahoma", 11, FontStyle.Bold))
                textOut(GridView2.GetRowCellValue(0, "par14"), rtb, New Font("Tahoma", 12))
                textOut(GridView1.GetRowCellValue(0, "idnim"), rtb, New Font("Tahoma", 11, FontStyle.Bold))
                textOut(GridView2.GetRowCellValue(0, "par15"), rtb, New Font("Tahoma", 12))



                textOut(GridView1.GetRowCellValue(0, "idnim") + vbNewLine, rtb, New Font("Tahoma", 11, FontStyle.Bold))

                'textOut(vbNewLine, rtb, New Font("Tahoma", 10)) 'ddd

                If typemhs = "R" Then
                    textOut(vbTab + GridView2.GetRowCellValue(0, "par13"), rtb, New Font("Tahoma", 12))
                    textOut(vbTab + GridView2.GetRowCellValue(0, "par21") + " ", rtb, New Font("Tahoma", 11, FontStyle.Bold))

                    textOut(GridView2.GetRowCellValue(0, "par14"), rtb, New Font("Tahoma", 12))
                    textOut(GridView1.GetRowCellValue(0, "idnim"), rtb, New Font("Tahoma", 11, FontStyle.Bold))
                    textOut(GridView2.GetRowCellValue(0, "par15"), rtb, New Font("Tahoma", 12))
                    textOut("p" + GridView1.GetRowCellValue(0, "idnim") + vbNewLine, rtb, New Font("Tahoma", 11, FontStyle.Bold))
                End If
               

                rtb.SelectionHangingIndent = 0
                textOut(vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(GridView2.GetRowCellValue(0, "par22") + vbNewLine, rtb, New Font("Tahoma", 12))
                'disini
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(GridView2.GetRowCellValue(0, "par23") + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                textOut(vpejabat + vbNewLine, rtb, New Font("Tahoma", 11, FontStyle.Bold Or FontStyle.Underline))
                textOut(vjabatan_ind + vbNewLine, rtb, New Font("Tahoma", 11, FontStyle.Regular Or FontStyle.Italic))


                If typemhs = "R" Then
                    textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                    textOut("MN", rtb, New Font("Tahoma", 12))
                Else
                    'textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12))
                    'textOut("JN", rtb, New Font("Tahoma", 12))
                End If

               

                DirectCast(Class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                'rtb.Clear()
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Regular Or FontStyle.Italic)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "titlepejabating")
                'DirectCast(class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                'rtb.Clear()
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Bold)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "pejabat1")
                'DirectCast(class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                'rtb.Clear()
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Bold)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "titlepejabat2ind")
                'DirectCast(class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                'rtb.Clear()
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Regular Or FontStyle.Italic)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "titlepejabat2ing")
                'DirectCast(class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                'rtb.Clear()
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Bold)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "pejabat2")
                'DirectCast(class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                'rtb.Clear()
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Bold)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "skpend")
                'DirectCast(class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                'rtb.Clear()
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Bold)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "par1") & "  "
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Regular)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "par2") & "  "
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Regular)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "par3") + vbNewLine

                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Bold Or FontStyle.Italic)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "par4") & "  "
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Italic)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "par5") + vbNewLine
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Italic)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "par6") + vbNewLine + vbNewLine

                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Regular)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "par7") & "  "
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Regular)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "par8")
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Bold)
                'rtb.SelectedText = GridView1.GetRowCellValue(0, "gelarind") & IIf(GridView1.GetRowCellValue(0, "aliasind") = "", "" + vbNewLine, " [ " & GridView1.GetRowCellValue(0, "aliasind") & " ]" + vbNewLine)

                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Italic)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "par9") + vbNewLine
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Italic)
                'rtb.SelectedText = GridView2.GetRowCellValue(0, "par10")
                'rtb.SelectionFont = New Font("Tahoma",11, FontStyle.Bold Or FontStyle.Italic)
                'rtb.SelectedText = GridView1.GetRowCellValue(0, "gelaring") & IIf(GridView1.GetRowCellValue(0, "aliasing") = "", "" + vbNewLine, " [ " & GridView1.GetRowCellValue(0, "aliasing") & " ]")
                'DirectCast(class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf



                Tool.PreviewForm.TopLevel = False
                Tool.PreviewForm.Parent = PanelControl2
                Tool.PreviewForm.Dock = DockStyle.Fill
                Tool.PreviewForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                Tool.PrintingSystem.AddCommandHandler(New Class1.printdirect())
                Tool.PrintingSystem.AddCommandHandler(New Class1.print())

                Tool.ShowPreview()

                LayoutControlGroup2.Expanded = False
            Case 2
                MsgBox("close")

        End Select
    End Sub

    Private Sub f_konfirmasiregs_Load(sender As Object, e As EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Iap_dbDataSet.v_pejabat' table. You can move, or remove it, as needed.
        LayoutControlGroup2.Expanded = True
        tglpkk()
        LayoutControlGroup2.Expanded = False
    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged

    End Sub

    Private Sub TextEdit3_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles TextEdit3.ButtonClick

        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim myNum As String

            Try

                myNum = InputBox("Masukan Password Admin untuk merubah nomor surat")

            Catch

                'MsgBox("Password tidak terdaftar, silahkan hubungi kepala Bagian anda")
                Exit Sub
            End Try


            If myNum = "" Then
                Exit Sub
            ElseIf myNum <> "1234" Then


                MsgBox("Password tidak terdaftar, silahkan hubungi kepala Bagian anda")
                Exit Sub

            End If
            e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.OK

            TextEdit3.BackColor = Color.White
            TextEdit3.ReadOnly = False
        Else
            e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus
            TextEdit3.BackColor = Color.LightGray
            TextEdit3.ReadOnly = True
        End If
    End Sub
    Sub gantithn()
        DateEdit1.EditValue = KweryRvalue("select fromdate from cmdpkk where thakad='" & LookUpEdit2.EditValue & "' and typemhs='R' and active=1 ", "fromdate")
        DateEdit2.EditValue = KweryRvalue("select todate from cmdpkk where  thakad='" & LookUpEdit2.EditValue & "' and typemhs='R' and active=1 ", "todate")
        DateEdit3.EditValue = KweryRvalue("select mulaikul from cmdpkk where  thakad='" & LookUpEdit2.EditValue & "' and typemhs='R' and active=1 ", "mulaikul")

    End Sub

    Private Sub LookUpEdit2_EditValueChanged(sender As Object, e As EventArgs) Handles LookUpEdit2.EditValueChanged
        DateEdit1.EditValue = KweryRvalue("select fromdate from cmdpkk where thakad='" & LookUpEdit2.EditValue & "' and typemhs='R' and active=1 ", "fromdate")
        DateEdit2.EditValue = KweryRvalue("select todate from cmdpkk where  thakad='" & LookUpEdit2.EditValue & "' and typemhs='R' and active=1 ", "todate")
        DateEdit3.EditValue = KweryRvalue("select mulaikul from cmdpkk where  thakad='" & LookUpEdit2.EditValue & "' and typemhs='R' and active=1 ", "mulaikul")


    End Sub

    Private Sub WindowsUIButtonPanel1_Click(sender As Object, e As EventArgs) Handles WindowsUIButtonPanel1.Click

    End Sub
End Class