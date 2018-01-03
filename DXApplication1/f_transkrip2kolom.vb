
Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports DevExpress.XtraReports.UI

'Imports DevExpress.XtraReports.UI

Public Class f_transkrip2kolom
 ' Dim myConnectionString As String


    'Dim conn As MySql.Data.MySqlClient.MySqlConnection
    Dim dA As MySqlDataAdapter
    Dim dA2 As MySqlDataAdapter
    Private Sub ConnectMysql(nim As String)


        'myConnectionString = "server=172.16.0.12;port=3306;user id=fingerprint; password=fingerprint2016; database=iap_db;persist security info=true;CharSet=utf8;convert zero datetime=True; "
        conn = New MySql.Data.MySqlClient.MySqlConnection()


        Try
            conn.ConnectionString =Class1.myConnectionString 
            conn.Open()
        Catch ex As Exception

            MsgBox(ex.Message)
            Return
        End Try


        dA = New MySqlDataAdapter("call iap_db.sp_transkripheader2018('" & nim & "','%%','%2100%','%%')", conn)

        Dim DS As New DataSet()

        dA.Fill(DS)
        GridControl1.DataSource = DS.Tables(0)

        TextEdit2.EditValue = ""
        If (DS.Tables(0).Rows.Count) > 0 Then
            TextEdit2.EditValue = StrConv(DS.Tables(0).Rows(0)("fullname").ToString, VbStrConv.ProperCase)
        End If
        'MsgBox("select * from cmdijazah where kode ='ijazah_" & DS.Tables(0).Rows(0)("shprogdi").ToString & "'")

        'dA2 = New MySqlDataAdapter("select * from cmdijazah where kode ='ijazah_" & DS.Tables(0).Rows(0)("shprogdi").ToString & "'", conn)

        'Dim DS2 As New DataSet()

        'dA2.Fill(DS2)
        'GridControl2.DataSource = DS2.Tables(0)

        If GridView1.RowCount <= 0 Then

            'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False
            MsgBox("Mahasiswa belum lulus, Mohon Periksa kembali")
            SimpleButton1.Enabled = False
        Else
            WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = True
            SimpleButton1.Enabled = True
        End If



    End Sub
    Private Sub TextEdit1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextEdit1.KeyUp
        If e.KeyCode = Keys.Enter Then
            ConnectMysql(TextEdit1.EditValue.ToString.Trim)
        End If

    End Sub
    Private Sub WindowsUIButtonPanel1_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles WindowsUIButtonPanel1.ButtonClick


        'pejabat
        'Dim row As DataRowView = CType(LookUpEdit1.Properties.GetDataSourceRowByKeyValue(LookUpEdit1.EditValue), DataRowView)
        'Dim vpejabat As String = row("fullname")
        'Dim vjabatan_ind As String = row("org_desc")
        'Dim vjabatan_eng As String = row("jabatan")

        DateEdit1.DoValidate()
        DateEdit2.DoValidate()
        ConnectMysql(TextEdit1.EditValue.ToString.Trim)

        If GridView1.RowCount <= 0 Then Exit Sub

        Try
            Class1.strname = TextEdit2.EditValue
            Class1.strnim = TextEdit1.EditValue
            Class1.idprogdi = GridView1.GetRowCellValue(0, "idprogdi")
            Class1.idthnaka = GridView1.GetRowCellValue(0, "idthnaka")
            Class1.idevent = GridView1.GetRowCellValue(0, "idevent")
            Class1.idsemest = GridView1.GetRowCellValue(0, "idsemest").ToString
            Class1.nosurat = GridView1.GetRowCellValue(0, "noijazah").ToString
            Class1.idactor = yser
            Class1.status = ""
            Class1.note = ""
            Class1.stype = "Transkrip Akademik"
            Class1.idprogm = GridView1.GetRowCellValue(0, "idprogm")
            Class1.pejabat = GridView1.GetRowCellValue(0, "pejabat1").ToString()
            Class1.ipaddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try




        Select Case e.Button.Properties.Tag


            Case 1

                If GridView1.RowCount <= 0 Then Exit Sub

                PanelControl2.Controls.Clear()


                If ComboBoxEdit1.SelectedIndex = 0 Then
                    Try
                        Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkrip2kolom.repx", True)

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                    Dim Tool As ReportPrintTool = New ReportPrintTool(Class1.report)
                    Dim rtb As New System.Windows.Forms.RichTextBox()

                    Dim dbirth As DateTime = GridView1.GetRowCellValue(0, "dtbirth")
                    Dim dt1 As DateTime = DateEdit1.EditValue
                    Dim dt2 As DateTime = DateEdit2.EditValue

                    Class1.report.Parameters("nim").Value = TextEdit1.EditValue
                    'class1.report.parameters("tanggaly").Value = DateEdit2.EditValue

                    Class1.report.Parameters("nim").Visible = False
                    Class1.report.Parameters("tanggaly").Visible = False



                    rtb.Clear()
                    rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular)
                    rtb.SelectedText = StrConv(GridView1.GetRowCellValue(0, "plbirth").ToString(), VbStrConv.ProperCase) & ", " & CInt(dbirth.ToString("dd", indo)) & "  " & dbirth.ToString("MMMM", indo) & "  " & dbirth.ToString("yyyy", indo)
                    DirectCast(Class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                    rtb.Clear()
                    rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular)
                    rtb.SelectedText = CInt(dt1.ToString("dd", indo)) & "  " & dt1.ToString("MMMM", indo) & "  " & dt1.ToString("yyyy", indo)
                    DirectCast(Class1.report.FindControl("richText2", True), XRRichText).Rtf = rtb.Rtf


                    rtb.Clear()
                    rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular)
                    rtb.SelectedText = "Jakarta, " & CInt(dt2.ToString("dd", indo)) & "  " & dt2.ToString("MMMM", indo) & "  " & dt2.ToString("yyyy", indo)
                    DirectCast(Class1.report.FindControl("richText3", True), XRRichText).Rtf = rtb.Rtf


                    Tool.PreviewForm.TopLevel = False
                    Tool.PreviewForm.Parent = PanelControl2
                    Tool.PreviewForm.Dock = DockStyle.Fill
                    Tool.PreviewForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                    Tool.PrintingSystem.AddCommandHandler(New Class1.printdirect())
                    Tool.PrintingSystem.AddCommandHandler(New Class1.print())

                    Tool.PrintingSystem.AddCommandHandler(New Class1.printdirect())
                    Tool.PrintingSystem.AddCommandHandler(New Class1.print())

                    Tool.ShowPreview()
                Else
                    Try
                        Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkrip2kolom eng.repx", True)

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                    Dim Tool As ReportPrintTool = New ReportPrintTool(Class1.report)
                    Dim rtb As New System.Windows.Forms.RichTextBox()

                    Dim dbirth As DateTime = GridView1.GetRowCellValue(0, "dtbirth")
                    Dim dt1 As DateTime = DateEdit1.EditValue
                    Dim dt2 As DateTime = DateEdit2.EditValue
                    Class1.report.Parameters("nim").Value = TextEdit1.EditValue
                    Class1.report.Parameters("tanggaly").Value = DateEdit1.EditValue

                    Class1.report.Parameters("nim").Visible = False
                    Class1.report.Parameters("tanggaly").Visible = False



                    rtb.Clear()
                    rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular)
                    rtb.SelectedText = StrConv(GridView1.GetRowCellValue(0, "plbirth").ToString(), VbStrConv.ProperCase) & ", " & dbirth.ToString("MMMM", eng) & "  " & ordinal(dbirth.ToString("dd", eng)) & "  " & dbirth.ToString("yyyy", eng)
                    DirectCast(Class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                    rtb.Clear()
                    rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular)
                    rtb.SelectedText = dt1.ToString("MMMM", eng) & "  " & ordinal(dt1.ToString("dd", eng)) & "  " & dt1.ToString("yyyy", eng)
                    DirectCast(Class1.report.FindControl("richText2", True), XRRichText).Rtf = rtb.Rtf


                    rtb.Clear()
                    rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular)
                    rtb.SelectedText = "Jakarta, " & dt2.ToString("MMMM", eng) & "  " & ordinal(dt2.ToString("dd", eng)) & "  " & dt2.ToString("yyyy", eng)
                    DirectCast(Class1.report.FindControl("richText3", True), XRRichText).Rtf = rtb.Rtf

                    Tool.PreviewForm.TopLevel = False
                    Tool.PreviewForm.Parent = PanelControl2
                    Tool.PreviewForm.Dock = DockStyle.Fill
                    Tool.PreviewForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                    Tool.PrintingSystem.AddCommandHandler(New Class1.printdirect())
                    Tool.PrintingSystem.AddCommandHandler(New Class1.print())

                    Tool.ShowPreview()
                End If

                LayoutControlGroup2.Expanded = False

            Case 2
                MsgBox("close")

        End Select

    End Sub

    Private Sub transkrip2kolom_Load(sender As Object, e As EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Iap_dbDataSet.v_pejabat' table. You can move, or remove it, as needed.
        LayoutControlGroup2.Expanded = True
        Me.V_pejabatTableAdapter.Fill(Me.Iap_dbDataSet.v_pejabat)

        TextEdit2.ReadOnly = True
        ComboBoxEdit1.SelectedIndex = 0
        ComboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        LayoutControlItem1.ContentVisible = False
        'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False

        LookUpEdit1.ItemIndex = 0
        LayoutControlGroup2.Expanded = False
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        DateEdit1.EditValue = Now()
        DateEdit2.EditValue = Now()
        Class1.detform(DateEdit1)
        Class1.detform(DateEdit2)


        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub DateEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles DateEdit1.EditValueChanged

    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged

    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim f As Form = f_judulskripsi

        tran_nim = TextEdit1.EditValue
        tran_nama = TextEdit2.EditValue
        tran_sind = GridView1.GetRowCellValue(0, "judul")
        tran_sing = GridView1.GetRowCellValue(0, "judulinggris")
        Try
            Class1.strname = TextEdit2.EditValue
            Class1.strnim = TextEdit1.EditValue
            Class1.idprogdi = GridView1.GetRowCellValue(0, "idprogdi")
            Class1.idthnaka = GridView1.GetRowCellValue(0, "idthnaka")
            Class1.idevent = GridView1.GetRowCellValue(0, "idevent")
            Class1.idsemest = GridView1.GetRowCellValue(0, "idsemest").ToString
            Class1.nosurat = GridView1.GetRowCellValue(0, "noijazah").ToString
            Class1.idactor = yser
            Class1.status = ""
            Class1.note = ""
            Class1.stype = "Transkrip Akademik"
            Class1.idprogm = GridView1.GetRowCellValue(0, "idprogm")
            Class1.pejabat = GridView1.GetRowCellValue(0, "pejabat1").ToString()
            Class1.ipaddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        f.ShowDialog()
    End Sub
End Class