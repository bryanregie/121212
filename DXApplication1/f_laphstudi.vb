Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraReports.UI
Imports MySql.Data.MySqlClient

Public Class f_laphstudi
    'Dim conn As MySql.Data.MySqlClient.MySqlConnection

    Dim dA0, dA, dA2, dA3 As MySqlDataAdapter

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Class1.detform(DateEdit4)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ConnectMysql(nim As String)

        Konek()
        KweryGrid("call iap_db.sp_transkripheader2018('" & nim & "','%%','%%','%%')", GridControl1)
        TextEdit2.EditValue = KweryRvalue("call iap_db.sp_transkripheader2018('" & nim & "','%%','%%','%%')", "fullname")
        'TextEdit3.EditValue = ""
        'If NotReg(GridView1, WindowsUIButtonPanel1) = False Then Exit Sub

        If GridView1.RowCount <= 0 Then
            'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False
            MsgBox("NIM Tidak terdaftar, Mohon Periksa kembali")
            Exit Sub
            'Else
            '    WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = True
        End If

        'KweryGrid("select * from cmdijazah where kode ='skcuti1'", GridControl2)

        'Dim a1, a2, a3, a4, a5 As String
        'a1 = Microsoft.VisualBasic.Right("0000" & KweryRvalue("select *,now() as tgl from cmdnodok where kodedok ='OPC'", "nourut"), 4)
        'a2 = KweryRvalue("select *,now() as tgl from cmdnodok where kodedok ='OPC'", "prefix")
        'a3 = "IBIKKG-BAAK"
        'a4 = bln(Month(KweryRvalue("select *,now() as tgl from cmdnodok where kodedok ='OPC'", "tgl")))
        'a5 = Microsoft.VisualBasic.Right(Year(KweryRvalue("select *,now() as tgl from cmdnodok where kodedok ='OPC'", "tgl")), 2)
        'TextEdit3.EditValue = a1 & "/" & a2 & "/" & a3 & "/" & a4 & "/" & a5


    End Sub

    Private Sub LookUpEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles LookUpEdit1.EditValueChanged
        Dim editor As DevExpress.XtraEditors.LookUpEdit = CType(sender, DevExpress.XtraEditors.LookUpEdit)
        'Dim row As DataRowView = CType(editor.Properties.GetDataSourceRowByKeyValue(editor.EditValue), DataRowView)
        'Dim v1 As String = row("fullname")
        'Dim v2 As String = row("org_desc")
        'Dim v3 As String = row("jabatan")
        'MsgBox(v1)

    End Sub

    Private Sub TextEdit1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextEdit1.KeyUp
        If e.KeyCode = Keys.Enter Then
            ConnectMysql(TextEdit1.EditValue.ToString.Trim)
        End If

    End Sub

    Private Sub WindowsUIButtonPanel1_Click(sender As Object, e As EventArgs) Handles WindowsUIButtonPanel1.Click

    End Sub

    Private Sub WindowsUIButtonPanel1_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles WindowsUIButtonPanel1.ButtonClick
        'DateEdit1.DoValidate()
        DateEdit4.DoValidate()
        ComboBoxEdit1.DoValidate()
        ComboBoxEdit2.DoValidate()
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
            Class1.nosurat = ""
            Class1.idactor = yser
            Class1.status = ""
            Class1.note = ""
            Class1.stype = "Lap Hasil Studi"
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
                    Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_lhs.repx", True)

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                Class1.report.Parameters("strnim").Value = TextEdit1.EditValue
                Class1.report.Parameters("thakad").Value = ComboBoxEdit1.EditValue
                Class1.report.Parameters("event").Value = IIf(ComboBoxEdit2.EditValue = "Ganjil", "01", IIf(ComboBoxEdit2.EditValue = "Genap", "02", "03"))
                Class1.report.Parameters("strnim").Visible = False
                Class1.report.Parameters("thakad").Visible = False
                Class1.report.Parameters("event").Visible = False


                Dim Tool As ReportPrintTool = New ReportPrintTool(Class1.report)
                Dim rtb As New System.Windows.Forms.RichTextBox()

                Dim dbirth As DateTime = GridView1.GetRowCellValue(0, "dtbirth")
                'Dim dt1 As DateTime = DateEdit1.EditValue

                Dim dt4 As DateTime = DateEdit4.EditValue
                'Dim smt() As String = {"", "I (Satu)", "II (Dua)", "III (Tiga)", "IV (Empat)", "V (Lima)", "VI (Enam)", "VII (Tujuh)", "IIX (Delapan)", "IX (Sembilan)", "X (Sepuluh)", "XI (Sebelas)", "XII (Dua Belas)"}
                'Dim vsmt As String
                'vsmt = smt(CInt(GridView1.GetRowCellValue(0, "idsemest")))

                'Dim pkm As String
                Dim dbrt As String
                dbrt = StrConv(GridView1.GetRowCellValue(0, "plbirth").ToString(), VbStrConv.ProperCase) & ", " & CInt(dbirth.ToString("dd", indo)) & "  " & dbirth.ToString("MMMM", indo) & "  " & dbirth.ToString("yyyy", indo)

                'textOut("Jakarta, " & dt4.ToString("dd", indo) & "  " & dt4.ToString("MMMM", indo) & "  " & dt4.ToString("yyyy", indo), rtb)
                rtb.Clear()
                textOut(TextEdit2.EditValue, rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                DirectCast(Class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                textOut("Jakarta, " & CInt(dt4.ToString("dd", indo)) & "  " & dt4.ToString("MMMM", indo) & "  " & dt4.ToString("yyyy", indo), rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                DirectCast(Class1.report.FindControl("richText5", True), XRRichText).Rtf = rtb.Rtf


                'rtb.Clear()
                'textOut(GridView2.GetRowCellValue(0, "par24"), rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                'DirectCast(Class1.report.FindControl("richText2", True), XRRichText).Rtf = rtb.Rtf



                'rtb.Clear()
                'textOut(GridView2.GetRowCellValue(0, "par25"), rtb, New Font("Arial Narrow", 20, FontStyle.Underline Or FontStyle.Bold), HorizontalAlignment.Center)
                'DirectCast(Class1.report.FindControl("richText3", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                textOut(vpejabat, rtb, New Font("Arial Narrow", 11, FontStyle.Underline Or FontStyle.Bold))
                DirectCast(Class1.report.FindControl("richText6", True), XRRichText).Rtf = rtb.Rtf


                rtb.Clear()
                textOut(vjabatan_ind, rtb, New Font("Arial Narrow", 11))
                DirectCast(Class1.report.FindControl("richText7", True), XRRichText).Rtf = rtb.Rtf





                'rtb.Clear()


                'textOut(GridView2.GetRowCellValue(0, "par1"), rtb)
                'textOut("  " & GridView2.GetRowCellValue(0, "par2"), rtb)
                ''textOut("  " & (dt1.ToString("dd", indo) & "  " & dt1.ToString("MMMM", indo) & "  " & dt1.ToString("yyyy", indo)), rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                'textOut("  " & GridView2.GetRowCellValue(0, "par3"), rtb)
                'textOut("  " & GridView2.GetRowCellValue(0, "par4"), rtb)
                'textOut("  " & ComboBoxEdit2.EditValue, rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                'textOut("  " & GridView2.GetRowCellValue(0, "par5"), rtb)
                'textOut("  " & ComboBoxEdit1.EditValue, rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                'textOut("  " & GridView2.GetRowCellValue(0, "par6") + vbNewLine + vbNewLine, rtb)
                'textOut(GridView2.GetRowCellValue(0, "par7"), rtb)

                'textOut(vbNewLine + vbNewLine, rtb)
                'rtb.SelectionBullet = True
                'rtb.SelectionHangingIndent = 20
                'textOut(GridView2.GetRowCellValue(0, "par8"), rtb)
                'textOut(vbNewLine + vbNewLine, rtb)
                ''rtb.SelectionBullet = True
                'textOut(GridView2.GetRowCellValue(0, "par9"), rtb)
                ''textOut(GridView1.GetRowCellValue(0, "fullname") & vbNewLine, rtb)
                ''rtb.SelectionBullet = False
                'textOut(vbNewLine + vbNewLine, rtb)

                'rtb.SelectionBullet = False
                'textOut(GridView2.GetRowCellValue(0, "par10"), rtb)


                'textOut(vbNewLine + vbNewLine, rtb)
                'textOut(vbNewLine + vbNewLine, rtb)
                'textOut("Jakarta, " & dt4.ToString("dd", indo) & "  " & dt4.ToString("MMMM", indo) & "  " & dt4.ToString("yyyy", indo), rtb)
                'textOut(vbNewLine + vbNewLine, rtb)
                'textOut(vbNewLine + vbNewLine, rtb)
                'textOut(vbNewLine + vbNewLine, rtb)
                'textOut(vpejabat & vbNewLine, rtb, New Font("Arial Narrow", 12, FontStyle.Bold Or FontStyle.Underline))
                'textOut(vjabatan_ind & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Italic))

                'textOut(vbNewLine + vbNewLine, rtb)
                'textOut(vbNewLine + vbNewLine, rtb)
                'rtb.SelectionHangingIndent = 20
                'textOut("CC :" & vbTab & GridView2.GetRowCellValue(0, "par12") + vbNewLine, rtb)
                'textOut(vbTab & vbTab & GridView2.GetRowCellValue(0, "par13") + vbNewLine, rtb)
                'textOut(vbTab & vbTab & GridView2.GetRowCellValue(0, "par14"), rtb)
                'DirectCast(Class1.report.FindControl("richText4", True), XRRichText).Rtf = rtb.Rtf





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
        LayoutControlGroup2.Expanded = True
        'TODO: This line of code loads data into the 'Iap_dbDataSet.v_pejabat' table. You can move, or remove it, as needed.
        Me.V_pejabatTableAdapter.Fill(Me.Iap_dbDataSet.v_pejabat)
        'DateEdit1.EditValue = Now()
        'DateEdit2.EditValue = Now()
        'DateEdit3.EditValue = Now()
        DateEdit4.EditValue = Now()

        LookUpEdit1.ItemIndex = 0
        LayoutControlGroup2.Expanded = False
        For i = 1996 To Year(Now()) + 1
            ComboBoxEdit1.Properties.Items.Add(i - 1 & "/" & i)
        Next i

        ComboBoxEdit1.SelectedIndex = ComboBoxEdit1.Properties.Items.Count - 1
        ComboBoxEdit2.SelectedIndex = 0
        LayoutControlItem1.ContentVisible = False
        'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False
        TextEdit2.ReadOnly = True

    End Sub

    Private Sub LookUpEdit1_Closed(sender As Object, e As ClosedEventArgs) Handles LookUpEdit1.Closed

    End Sub
End Class