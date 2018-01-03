Imports DevExpress.XtraReports.UI

Public Class f_va
    'Dim dA As MySqlDataAdapter
    'Dim dA2 As MySqlDataAdapter
    'Private Sub ConnectMysql(nim As String)


    '    'myConnectionString = "server=172.16.0.12;port=3306;user id=fingerprint; password=fingerprint2016; database=iap_db;persist security info=true;CharSet=utf8;convert zero datetime=True; "
    '    conn = New MySql.Data.MySqlClient.MySqlConnection()

    '    'MsgBox(myConnectionString)
    '    Try
    '        conn.ConnectionString = Class1.myConnectionString
    '        conn.Open()
    '    Catch ex As Exception

    '        MsgBox(ex.Message)
    '        Return
    '    End Try


    '    dA = New MySqlDataAdapter("call iap_db.sp_transkripheader2018('" & nim & "','%%','%%','%%')", conn)

    '    Dim DS As New DataSet()

    '    dA.Fill(DS)
    '    GridControl1.DataSource = DS.Tables(0)

    '    TextEdit2.EditValue = ""
    '    If (DS.Tables(0).Rows.Count) > 0 Then
    '        TextEdit2.EditValue = StrConv(DS.Tables(0).Rows(0)("fullname").ToString, VbStrConv.ProperCase)
    '    End If
    '    'MsgBox("select * from cmdijazah where kode ='ijazah_" & DS.Tables(0).Rows(0)("shprogdi").ToString & "'")

    '    'dA2 = New MySqlDataAdapter("select * from cmdijazah where kode ='ijazah_" & DS.Tables(0).Rows(0)("shprogdi").ToString & "'", conn)

    '    'Dim DS2 As New DataSet()

    '    'dA2.Fill(DS2)
    '    'GridControl2.DataSource = DS2.Tables(0)

    '    If GridView1.RowCount <= 0 Then

    '        'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False
    '        MsgBox("NIM Tidak terdaftar, Mohon Periksa kembali")
    '        SimpleButton1.Enabled = False
    '    Else
    '        WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = True

    '    End If



    'End Sub
    'Private Sub TextEdit1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextEdit1.KeyUp
    '    If e.KeyCode = Keys.Enter Then
    '        ConnectMysql(TextEdit1.EditValue.ToString.Trim)
    '    End If

    'End Sub

    Private Sub WindowsUIButtonPanel1_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles WindowsUIButtonPanel1.ButtonClick

        'ConnectMysql(TextEdit1.EditValue.ToString.Trim)

        'If GridView1.RowCount <= 0 Then Exit Sub
        'pejabat
        'Dim row As DataRowView = CType(LookUpEdit1.Properties.GetDataSourceRowByKeyValue(LookUpEdit1.EditValue), DataRowView)
        'Dim vpejabat As String = row("fullname")
        'Dim vjabatan_ind As String = row("org_desc")
        'Dim vjabatan_eng As String = row("jabatan")

        'Class1.strname = TextEdit2.EditValue
        'Class1.strnim = TextEdit1.EditValue
        'Class1.idprogdi = GridView1.GetRowCellValue(0, "idprogdi")
        'Class1.idthnaka = GridView1.GetRowCellValue(0, "idthnaka")
        'Class1.idevent = GridView1.GetRowCellValue(0, "idevent")
        'Class1.idsemest = GridView1.GetRowCellValue(0, "idsemest").ToString
        'Class1.nosurat = ""
        'Class1.idactor = yser
        'Class1.status = ""
        'Class1.note = ""
        'Class1.stype = "Transkrip Nilai Kumulatif"
        'Class1.idprogm = GridView1.GetRowCellValue(0, "idprogm")
        'Class1.pejabat = vpejabat
        'Class1.ipaddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()



        'strHostName = System.Net.Dns.GetHostName()

        'strIPAddress = System.Net.Dns.GetHostByName(strHostName).AddressList(0).ToString()


        Select Case e.Button.Properties.Tag


            Case 1

                'If GridView1.RowCount <= 0 Then Exit Sub

                PanelControl2.Controls.Clear()
                Dim rtb As New System.Windows.Forms.RichTextBox()


                Try
                    Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_va.repx", True)

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try



                Dim Tool As ReportPrintTool = New ReportPrintTool(Class1.report)

                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12, FontStyle.Regular))
                textOut("No Registrasi" + vbTab + vbTab + ":" + vbTab, rtb, New Font("Tahoma", 12, FontStyle.Regular))
                textOut(SearchLookUpEdit1.EditValue + vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12, FontStyle.Regular))

                textOut("Nama" + vbTab + vbTab + vbTab + ": " + vbTab, rtb, New Font("Tahoma", 12, FontStyle.Regular))
                textOut(TextEdit2.EditValue + vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12, FontStyle.Regular))

                textOut("Virtual Account" + vbTab + ":" + vbTab + "00561000", rtb, New Font("Tahoma", 12, FontStyle.Regular))
                textOut(Microsoft.VisualBasic.Right(SearchLookUpEdit1.EditValue, 7) + vbNewLine, rtb, New Font("Tahoma", 12, FontStyle.Regular))
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12, FontStyle.Regular))
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12, FontStyle.Regular))
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12, FontStyle.Regular))
                textOut(vbNewLine + vbNewLine, rtb, New Font("Tahoma", 12, FontStyle.Regular))
                textOut("Printed On " & Now(), rtb, New Font("Tahoma", 8, FontStyle.Regular))

                DirectCast(Class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                'richIt("No Registrasi", "richText1", Class1.report, New Font("Tahoma", 10, FontStyle.Regular))
                'richIt(vbTab & ": ", "richText1", Class1.report, New Font("Tahoma", 10, FontStyle.Regular))
                'richIt(SearchLookUpEdit1.EditValue + vbNewLine, "richText1", Class1.report, New Font("Tahoma", 10, FontStyle.Regular))

                'richIt("Nama", "richText1", Class1.report, New Font("Tahoma", 10, FontStyle.Regular))
                'richIt(vbTab & ": ", "richText1", Class1.report, New Font("Tahoma", 10, FontStyle.Regular))
                'richIt(TextEdit2.EditValue + vbNewLine, "richText1", Class1.report, New Font("Tahoma", 10, FontStyle.Regular))

                'richIt("Virtual Account", "richText1", Class1.report, New Font("Tahoma", 10, FontStyle.Regular))
                'richIt(vbTab & ": 00561000", "richText1", Class1.report, New Font("Tahoma", 10, FontStyle.Regular))
                'richIt(SearchLookUpEdit1.EditValue + vbNewLine, "richText1", Class1.report, New Font("Tahoma", 10, FontStyle.Regular))


                Tool.PreviewForm.TopLevel = False
                    Tool.PreviewForm.Parent = PanelControl2
                    Tool.PreviewForm.Dock = DockStyle.Fill
                    Tool.PreviewForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                    Tool.PrintingSystem.AddCommandHandler(New Class1.printdirect())
                    Tool.PrintingSystem.AddCommandHandler(New Class1.print())

                    Tool.ShowPreview()


                'LayoutControlGroup2.Expanded = False
            Case 2
                MsgBox("close")

        End Select

    End Sub
    Private Shared Sub insertLog()

    End Sub


    Private Sub transkrip2kolom_Load(sender As Object, e As EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Iap_dbDataSet.v_pejabat' table. You can move, or remove it, as needed.


        TextEdit2.ReadOnly = True
        ComboBoxEdit1.SelectedIndex = 0
        ComboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        LayoutControlItem1.ContentVisible = False
        'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False


    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        DateEdit1.EditValue = Now()

        Class1.detform(DateEdit1)



        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub f_va_Load(sender As Object, e As EventArgs) Handles Me.Load
        Class1.LoadSLook(SearchLookUpEdit1, "Select idnoreg as Noregistrasi,firsname as FirstName,lastname as LastName from pmtr0002")
    End Sub

    Private Sub SearchLookUpEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles SearchLookUpEdit1.EditValueChanged

        TextEdit2.EditValue = SearchLookUpEdit1View.GetFocusedRowCellValue("FirstName") & " " & SearchLookUpEdit1View.GetFocusedRowCellValue("LastName")
    End Sub

End Class