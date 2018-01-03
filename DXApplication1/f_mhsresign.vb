Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports DevExpress.XtraReports.UI
Public Class f_mhsresign
 ' Dim myConnectionString As String


    'Dim conn As MySql.Data.MySqlClient.MySqlConnection
    Dim dA As MySqlDataAdapter
    Dim dA2, dA3 As MySqlDataAdapter

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Class1.detform(DateEdit4)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ConnectMysql(nim As String)


        'myConnectionString = "server=172.16.0.12;port=3306;user id=fingerprint; password=fingerprint2016; database=iap_db;persist security info=true;CharSet=utf8;convert zero datetime=True; "
        conn = New MySql.Data.MySqlClient.MySqlConnection()


        Try
            conn.ConnectionString = Class1.myConnectionString
            conn.Open()
        Catch ex As Exception

            MsgBox(ex.Message)
            Return
        End Try


        dA = New MySqlDataAdapter("call iap_db.sp_transkripheader2018('" & nim & "','%01%','%%','%%')", conn)

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
            MsgBox("NIM Tidak terdaftar atau mahasiswa tidak aktif, Mohon Periksa kembali")
            Exit Sub
        Else
            WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = True
        End If

        '===========================
        'dA2 = New MySqlDataAdapter("select * from cmdijazah where kode ='skresign'", conn)

        'Dim DS2 As New DataSet()

        'dA2.Fill(DS2)
        'GridControl2.DataSource = DS2.Tables(0)
        KweryGrid("select * from cmdijazah where kode ='skresign'", GridControl2)

        '// double nosurat
        Dim ns As String = KweryRvalue("select nosurat from satr0020 where idnim = '" & TextEdit1.EditValue & "' and type='SK Mengundurkan Diri' order by nosurat desc ", "nosurat")

        If ns.Trim <> "" Then
            Class1.addnodoc = False
            TextEdit3.EditValue = ns
            Exit Sub
        Else

            Class1.addnodoc = True

        End If

        '===========================
        dA3 = New MySqlDataAdapter("select *,now() as tgl from cmdnodok where kodedok ='SKRS'", conn)

        Dim DS3 As New DataSet()

        dA3.Fill(DS3)
        Dim a1, a2, a3, a4, a5 As String
        TextEdit3.EditValue = ""
        If (DS3.Tables(0).Rows.Count) > 0 Then
            a1 = Microsoft.VisualBasic.Right("0000" & DS3.Tables(0).Rows(0)("nourut").ToString(), 4)
            a2 = "IBIKKG-BAAK"
            a3 = DS3.Tables(0).Rows(0)("prefix").ToString
            a4 = bln(Month(DS3.Tables(0).Rows(0)("tgl")))
            a5 = Microsoft.VisualBasic.Right(Year(DS3.Tables(0).Rows(0)("tgl")), 2)
            TextEdit3.EditValue = a1 & "/" & a2 & "/" & a3 & "/" & a4 & "/" & a5 'StrConv("SKcB", VbStrConv.ProperCase)
        End If
    End Sub
    Private Sub TextEdit1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextEdit1.KeyUp
        If e.KeyCode = Keys.Enter Then
            ConnectMysql(TextEdit1.EditValue.ToString.Trim)
        End If

    End Sub

    Private Sub WindowsUIButtonPanel1_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles WindowsUIButtonPanel1.ButtonClick
        MsgBox("Perhatian, Saat melakukan Print / Cetak," & vbCrLf & "maka akan merubah status mahasiswa menjadi resign")
        'DateEdit1.DoValidate()
        'DateEdit2.DoValidate()
        'DateEdit3.DoValidate()
        DateEdit4.DoValidate()
        ConnectMysql(TextEdit1.EditValue.ToString.Trim)

        If GridView1.RowCount <= 0 Then Exit Sub
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
            Class1.stype = "SK Mengundurkan Diri"
            Class1.idprogm = GridView1.GetRowCellValue(0, "idprogm")
            Class1.pejabat = vpejabat
            Class1.ipaddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Select Case e.Button.Properties.Tag


            Case 1

                If GridView1.RowCount <= 0 Then Exit Sub

                PanelControl2.Controls.Clear()
                Try
                    Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_mhsaktif.repx", True)

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                Dim Tool As ReportPrintTool = New ReportPrintTool(class1.report)
                Dim rtb As New System.Windows.Forms.RichTextBox()

                Dim dbirth As DateTime = GridView1.GetRowCellValue(0, "dtbirth")
                'Dim dt1 As DateTime = DateEdit1.EditValue
                'Dim dt2 As DateTime = DateEdit2.EditValue
                'Dim dt3 As DateTime = DateEdit3.EditValue
                Dim dt4 As DateTime = DateEdit4.EditValue
                'Dim smt() As String = {"", "I (Satu)", "II (Dua)", "III (Tiga)", "IV (Empat)", "V (Lima)", "VI (Enam)", "VII (Tujuh)", "IIX (Delapan)", "IX (Sembilan)", "X (Sepuluh)", "XI (Sebelas)", "XII (Dua Belas)"}
                Dim vsmt As String
                vsmt = smt(CInt(GridView1.GetRowCellValue(0, "idsemest")))

                'Dim pkm As String
                Dim dbrt As String
                dbrt = StrConv(GridView1.GetRowCellValue(0, "plbirth").ToString(), VbStrConv.ProperCase) & ", " & CInt(dbirth.ToString("dd", indo)) & "  " & dbirth.ToString("MMMM", indo) & "  " & dbirth.ToString("yyyy", indo)



                'If dt1.Date = dt2.Date Then
                '    pkm = (dt2.ToString("dd", indo) & "  " & dt2.ToString("MMMM", indo) & "  " & dt2.ToString("yyyy", indo))
                'Else

                '    pkm = dt1.ToString("dd", indo) & "  " & dt1.ToString("MMMM", indo) & " s/d " & dt2.ToString("dd", indo) & "  " & dt2.ToString("MMMM", indo) & "  " & dt2.ToString("yyyy", indo)
                'End If



                rtb.Clear()
                textOut(GridView2.GetRowCellValue(0, "par25"), rtb, New Font("Arial Narrow", 20, FontStyle.Bold Or FontStyle.Underline), HorizontalAlignment.Center)
                DirectCast(class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                textOut(TextEdit3.EditValue, rtb, New Font("Arial Narrow", 11, FontStyle.Regular), HorizontalAlignment.Center)
                DirectCast(class1.report.FindControl("richText2", True), XRRichText).Rtf = rtb.Rtf



                rtb.Clear()
                textOut(GridView2.GetRowCellValue(0, "par10") + vbNewLine, rtb)
                textOut(vbNewLine, rtb)

                textOut(vbTab & "Nama" & vbTab & vbTab & ": " & vpejabat + vbNewLine, rtb)
                textOut(vbNewLine, rtb)
                textOut(vbTab & "Jabatan" & vbTab & vbTab & ": " & vjabatan_ind + vbNewLine, rtb)
                textOut(vbNewLine, rtb)

                textOut(GridView2.GetRowCellValue(0, "par11") + vbNewLine, rtb)
                textOut(vbNewLine, rtb)


                textOut(vbTab & GridView2.GetRowCellValue(0, "par2") & vbTab & vbTab & ": ", rtb)
                textOut(GridView1.GetRowCellValue(0, "fullname") & vbNewLine, rtb)

                textOut(vbNewLine, rtb)

                textOut(vbTab & GridView2.GetRowCellValue(0, "par4") & vbTab & vbTab & ": ", rtb)
                textOut(GridView1.GetRowCellValue(0, "idnim") & vbNewLine, rtb)

                textOut(vbNewLine, rtb)
                textOut(vbTab & GridView2.GetRowCellValue(0, "par3") & vbTab & ": ", rtb)
                textOut(GridView1.GetRowCellValue(0, "meprogdi") & vbNewLine, rtb)
                textOut(vbNewLine, rtb)

                textOut(GridView2.GetRowCellValue(0, "par6") & "  ", rtb)
                'textOut(" (" & GridView1.GetRowCellValue(0, "meprogdi") & ") ", rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                textOut(GridView1.GetRowCellValue(0, "sks") & " SKS, ", rtb, New Font("Arial Narrow", 10, FontStyle.Bold))

                textOut(GridView2.GetRowCellValue(0, "par7") & "  ", rtb)
                textOut(GridView1.GetRowCellValue(0, "nilipk") & ", ", rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                ' textOut(GridView1.GetRowCellValue(0, "mekonsen") & ". ", rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                textOut(GridView2.GetRowCellValue(0, "par8") & "  ", rtb)


                textOut(vbNewLine & vbNewLine, rtb)
                textOut(GridView2.GetRowCellValue(0, "par9") & "  ", rtb)

                'textOut(GridView2.GetRowCellValue(0, "par10") & "  ", rtb)

                'textOut(GridView2.GetRowCellValue(0, "par11") & vbNewLine + vbNewLine, rtb)


                'textOut(GridView2.GetRowCellValue(0, "par12") & "  ", rtb)


                textOut(vbNewLine & vbNewLine, rtb)
                textOut(vbNewLine & vbNewLine, rtb)
                textOut("Jakarta, " & CInt(dt4.ToString("dd", indo)) & "  " & dt4.ToString("MMMM", indo) & "  " & dt4.ToString("yyyy", indo), rtb)

                textOut(vbNewLine & vbNewLine, rtb)
                textOut(vbNewLine & vbNewLine, rtb)
                textOut(vbNewLine & vbNewLine, rtb)
                textOut(vbNewLine & vbNewLine, rtb)

                textOut(vpejabat & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Bold Or FontStyle.Underline))
                textOut(vjabatan_ind & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Italic))
                rtb.SelectionCharOffset = 100

                DirectCast(Class1.report.FindControl("richText3", True), XRRichText).Rtf = rtb.Rtf

                'rtb.Clear()
                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut("Jakarta, " & dt4.ToString("dd", indo) & "  " & dt4.ToString("MMMM", indo) & "  " & dt4.ToString("yyyy", indo) & vbNewLine, rtb)
                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut("No" & vbTab & ": " & TextEdit3.EditValue & vbNewLine, rtb)
                'textOut("Hal" & vbTab & ": " & GridView2.GetRowCellValue(0, "par1") & vbNewLine, rtb)
                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut("Kepada Yth " & vbNewLine, rtb)
                'textOut(StrConv(GridView1.GetRowCellValue(0, "fullname"), VbStrConv.ProperCase) & vbNewLine, rtb, New Font("Arial Narrow", 11, FontStyle.Bold))
                'textOut(StrConv(GridView1.GetRowCellValue(0, "txadr1"), VbStrConv.ProperCase) & vbNewLine, rtb)
                'textOut(StrConv(GridView1.GetRowCellValue(0, "txkota"), VbStrConv.ProperCase) & "  " & GridView1.GetRowCellValue(0, "idzip") & vbNewLine, rtb)
                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut("Dengan Hormat, " & vbNewLine, rtb)
                'textOut(GridView2.GetRowCellValue(0, "par2") & vbNewLine, rtb)
                'textOut(GridView2.GetRowCellValue(0, "par3") & vbNewLine, rtb)
                'textOut(vbNewLine & vbNewLine, rtb)

                'textOut(vbTab & GridView2.GetRowCellValue(0, "par4") & "  ", rtb)
                'textOut(GridView1.GetRowCellValue(0, "idnim") & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                'textOut(vbTab & GridView2.GetRowCellValue(0, "par5"), rtb)
                'textOut(pkm & "  " & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                'textOut(vbTab & "    " & GridView2.GetRowCellValue(0, "par6"), rtb)
                'textOut(GridView2.GetRowCellValue(0, "par7") & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                'textOut(vbTab & GridView2.GetRowCellValue(0, "par8"), rtb)
                'textOut((dt3.ToString("dd", indo) & "  " & dt3.ToString("MMMM", indo) & "  " & dt3.ToString("yyyy", indo)) & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Bold))

                'textOut(vbTab & GridView2.GetRowCellValue(0, "par9"), rtb)
                'textOut(GridView2.GetRowCellValue(0, "par10") & vbNewLine, rtb)
                'textOut(vbTab & "    " & GridView1.GetRowCellValue(0, "idnim") & GridView2.GetRowCellValue(0, "par24"), rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                'textOut(GridView2.GetRowCellValue(0, "par11"), rtb)
                'textOut(GridView2.GetRowCellValue(0, "par25") & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                'textOut(vbTab & GridView2.GetRowCellValue(0, "par12"), rtb)
                'textOut(GridView2.GetRowCellValue(0, "par21") & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                'textOut(vbTab & "    " & GridView2.GetRowCellValue(0, "par14") & "  ", rtb)
                'textOut(GridView1.GetRowCellValue(0, "idnim"), rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                'textOut("  " & GridView2.GetRowCellValue(0, "par15") & "  ", rtb)
                'textOut(GridView1.GetRowCellValue(0, "idnim") & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                'textOut(vbTab & GridView2.GetRowCellValue(0, "par13"), rtb)
                'textOut(GridView2.GetRowCellValue(0, "par21") & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                'textOut(vbTab & "    " & GridView2.GetRowCellValue(0, "par14") & "  ", rtb)
                'textOut(GridView1.GetRowCellValue(0, "idnim"), rtb, New Font("Arial Narrow", 10, FontStyle.Bold))
                'textOut("  " & GridView2.GetRowCellValue(0, "par15") & "  ", rtb)
                'textOut("p" & GridView1.GetRowCellValue(0, "idnim") & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Bold))


                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut(GridView2.GetRowCellValue(0, "par22") & vbNewLine, rtb)
                ''disini
                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut(GridView2.GetRowCellValue(0, "par23") & vbNewLine, rtb)
                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut(vbNewLine & vbNewLine, rtb)
                'textOut(GridView2.GetRowCellValue(0, "pejabat1") & vbNewLine, rtb, New Font("Arial Narrow", 11, FontStyle.Bold))
                'textOut(GridView2.GetRowCellValue(0, "titlepejabatind") & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Italic))



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
        Me.V_pejabatTableAdapter.Fill(Me.Iap_dbDataSet.v_pejabat)
        'DateEdit1.EditValue = Now()
        'DateEdit2.EditValue = Now()
        'DateEdit3.EditValue = Now()
        DateEdit4.EditValue = Now()
        TextEdit2.ReadOnly = True
        LayoutControlItem1.ContentVisible = False
        'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False

        LookUpEdit1.ItemIndex = 0
        LayoutControlGroup2.Expanded = False
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
End Class