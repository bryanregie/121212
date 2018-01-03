
Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports DevExpress.XtraReports.UI
Public Class f_ijazah
 ' Dim myConnectionString As String


    'Dim conn As MySql.Data.MySqlClient.MySqlConnection
    Dim dA As MySqlDataAdapter
    Dim dA2 As MySqlDataAdapter

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Class1.detform(DateEdit1)
        Class1.detform(DateEdit2)

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

        'MsgBox(DateEdit1.DateTime.Year)
        'tgl yudisium merubah title S.TI menjadi S.kom , dan seterusnya
        Dim thn As Integer = DateEdit1.DateTime.Year
        If thn < 2017 Then thn = 0

        dA = New MySqlDataAdapter("call iap_db.sp_transkripheader2018('" & nim & "','%%','%2100%','" & thn & "')", conn)


        'dA = New MySqlDataAdapter("call iap_db.sp_transkripheader2018('" & nim & "','%%','%2100%','%%')", conn)

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
            MsgBox("Mahasiswa Belum Lulus, Mohon Periksa kembali")
            Exit Sub

        Else
            WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = True
        End If
        '=====================

        'dA2 = New MySqlDataAdapter("select * from cmdijazah where idprogdi ='" & DS.Tables(0).Rows(0)("idprogdi").ToString & "'", conn)

        'Dim DS2 As New DataSet()       


        'dA2.Fill(DS2)
        'GridControl2.DataSource = DS2.Tables(0)
       
        KweryGrid("select * from cmdijazah where idprogdi ='" & DS.Tables(0).Rows(0)("idprogdi").ToString & "'", GridControl2)

        ButtonEdit1.EditValue = GridView1.GetRowCellValue(0, "noijazah")

    End Sub
    Private Sub TextEdit1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextEdit1.KeyUp
        If e.KeyCode = Keys.Enter Then
            ConnectMysql(TextEdit1.EditValue.ToString.Trim)
        End If

    End Sub
    Private Sub WindowsUIButtonPanel1_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles WindowsUIButtonPanel1.ButtonClick


        DateEdit1.DoValidate()
        DateEdit2.DoValidate()
        'ComboBoxEdit1.DoValidate()
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
            Class1.stype = "Ijazah"
            Class1.idprogm = GridView1.GetRowCellValue(0, "idprogm")
            Class1.pejabat = GridView2.GetRowCellValue(0, "pejabat1")
            Class1.ipaddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Select Case e.Button.Properties.Tag


            Case 1

                If GridView1.RowCount <= 0 Then Exit Sub

                PanelControl2.Controls.Clear()
                Try
                    ''\\192ff.168.10.238\software\Software ALL ICT\Update (Hery)
                    ''Application.StartupPath
                    Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_ijazah.repx", True)

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                Dim Tool As ReportPrintTool = New ReportPrintTool(class1.report)
                Dim rtb As New System.Windows.Forms.RichTextBox()

                Dim dbirth As DateTime = GridView1.GetRowCellValue(0, "dtbirth")
                Dim dt1 As DateTime = DateEdit1.EditValue
                Dim dt2 As DateTime = DateEdit2.EditValue
                Dim dt3 As DateTime = GridView1.GetRowCellValue(0, "dtbirth")




                Class1.report.Parameters("tanggaly").Value = CInt(dt1.ToString("dd", indo)) & "  " & dt1.ToString("MMMM", indo) & "  " & dt1.ToString("yyyy", indo)
                Class1.report.Parameters("tanggalying").Value = dt1.ToString("MMMM", eng) & "  " & ordinal(dt1.ToString("dd", eng)) & "  " & dt1.ToString("yyyy", eng)
                Class1.report.Parameters("gelarind").Value = GridView1.GetRowCellValue(0, "gelarind")
                Class1.report.Parameters("gelaring").Value = GridView1.GetRowCellValue(0, "gelaring")
                Class1.report.Parameters("aliasind").Value = GridView1.GetRowCellValue(0, "aliasind")
                Class1.report.Parameters("aliasind").Value = GridView1.GetRowCellValue(0, "aliasing")
                Class1.report.Parameters("noseri").Value = GridView1.GetRowCellValue(0, "noijazah")

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "titlepejabatind")
                DirectCast(Class1.report.FindControl("XrRichText10", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Italic)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "titlepejabating")
                DirectCast(Class1.report.FindControl("XrRichText11", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "pejabat1")
                DirectCast(Class1.report.FindControl("XrRichText12", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "titlepejabat2ind")
                DirectCast(Class1.report.FindControl("XrRichText13", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Italic)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "titlepejabat2ing")
                DirectCast(Class1.report.FindControl("XrRichText14", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "pejabat2")
                DirectCast(Class1.report.FindControl("XrRichText15", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "skpend")
                DirectCast(Class1.report.FindControl("XrRichText16", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "par1") & "  "
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Regular)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "par2") & "  "
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Regular)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "par3") + vbNewLine

                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Bold Or FontStyle.Italic)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "par4") & "  "
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Italic)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "par5") + vbNewLine
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Italic)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "par6") + vbNewLine + vbNewLine

                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "par7") & "  "
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "par8")
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Bold)
                rtb.SelectedText = GridView1.GetRowCellValue(0, "gelarind") & IIf(GridView1.GetRowCellValue(0, "aliasind") = "", "" + vbNewLine, " [ " & GridView1.GetRowCellValue(0, "aliasind") & " ]" + vbNewLine)

                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Italic)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "par9") & "  "
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Italic)
                rtb.SelectedText = GridView2.GetRowCellValue(0, "par10")
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Bold Or FontStyle.Italic)
                rtb.SelectedText = GridView1.GetRowCellValue(0, "gelaring") & IIf(GridView1.GetRowCellValue(0, "aliasing") = "", "" + vbNewLine, " [ " & GridView1.GetRowCellValue(0, "aliasing") & " ]")

                DirectCast(Class1.report.FindControl("XrRichText17", True), XRRichText).Rtf = rtb.Rtf

                '//////////////////////////////

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 12, FontStyle.Bold)
                rtb.SelectedText = GridView1.GetRowCellValue(0, "fullname")
                DirectCast(Class1.report.FindControl("XrRichText1", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = GridView1.GetRowCellValue(0, "idnim")
                DirectCast(Class1.report.FindControl("XrRichText2", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = GridView1.GetRowCellValue(0, "idangkat")
                DirectCast(Class1.report.FindControl("XrRichText3", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = StrConv(GridView1.GetRowCellValue(0, "plbirth").ToString, vbProperCase) & ", " & CInt(dt3.ToString("dd", indo)) & "  " & dt3.ToString("MMMM", indo) & "  " & dt3.ToString("yyyy", indo)
                rtb.SelectedText = " / "
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Italic)
                rtb.SelectedText = StrConv(GridView1.GetRowCellValue(0, "plbirth").ToString, vbProperCase) & ", " & dt3.ToString("MMMM", eng) & "  " & ordinal(dt3.ToString("dd", eng)) & ", " & dt3.ToString("yyyy", eng)
                DirectCast(Class1.report.FindControl("XrRichText4", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = StrConv(GridView1.GetRowCellValue(0, "meprogm").ToString(), VbStrConv.ProperCase)
                rtb.SelectedText = " / "
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Italic)
                rtb.SelectedText = StrConv(GridView1.GetRowCellValue(0, "ingnmprog").ToString(), VbStrConv.ProperCase)
                DirectCast(Class1.report.FindControl("XrRichText5", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = StrConv(GridView1.GetRowCellValue(0, "meprogdi").ToString(), VbStrConv.ProperCase)
                rtb.SelectedText = " / "
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Italic)
                rtb.SelectedText = StrConv(GridView1.GetRowCellValue(0, "meprogdiing").ToString(), VbStrConv.ProperCase)
                DirectCast(Class1.report.FindControl("XrRichText6", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Bold)
                rtb.SelectedText = StrConv(GridView1.GetRowCellValue(0, "mekonsen").ToString(), VbStrConv.ProperCase)
                rtb.SelectedText = " / "
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Italic)
                rtb.SelectedText = StrConv(GridView1.GetRowCellValue(0, "mekonsening").ToString(), VbStrConv.ProperCase)
                DirectCast(Class1.report.FindControl("XrRichText7", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 11, FontStyle.Regular)
                rtb.SelectedText = "Jakarta, " & dt2.ToString("dd", indo) & "  " & dt2.ToString("MMMM", indo) & "  " & dt2.ToString("yyyy", indo)
                DirectCast(Class1.report.FindControl("XrRichText8", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Italic)
                rtb.SelectedText = "Jakarta, " & dt2.ToString("MMMM", eng) & "  " & ordinal(dt2.ToString("dd", eng)) & "  " & dt2.ToString("yyyy", eng)
                DirectCast(class1.report.FindControl("XrRichText9", True), XRRichText).Rtf = rtb.Rtf

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

    Private Sub transkrip2kolom_Load(sender As Object, e As EventArgs) Handles Me.Load
        LayoutControlGroup2.Expanded = True
        DateEdit1.EditValue = Now()
        DateEdit2.EditValue = Now()
        TextEdit2.ReadOnly = True
        LayoutControlItem1.ContentVisible = False
        'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False
        ComboBoxEdit1.SelectedIndex = 0
        LayoutControlGroup2.Expanded = False
    End Sub

    Private Sub WindowsUIButtonPanel1_Click(sender As Object, e As EventArgs) Handles WindowsUIButtonPanel1.Click

    End Sub

    Private Sub ButtonEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ButtonEdit1.ButtonClick
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

            ButtonEdit1.BackColor = Color.White
            ButtonEdit1.ReadOnly = False
        Else
            e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus
            ButtonEdit1.BackColor = Color.LightGray
            ButtonEdit1.ReadOnly = True
            Class1.MysqlUIDNew("update satr0020 set nosurat='" & ButtonEdit1.EditValue & "' where idnim='" & TextEdit1.EditValue & "'")
        End If
    End Sub
End Class