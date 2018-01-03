Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports System.Drawing.Imaging

Public Class f_transkripkumulatif
    'Dim myConnectionString As String
    'Dim conn As MySql.Data.MySqlClient.MySqlConnection
    Dim dA As MySqlDataAdapter
    Dim dA2 As MySqlDataAdapter
    Private Sub ConnectMysql(nim As String)


        'myConnectionString = "server=172.16.0.12;port=3306;user id=fingerprint; password=fingerprint2016; database=iap_db;persist security info=true;CharSet=utf8;convert zero datetime=True; "
        conn = New MySql.Data.MySqlClient.MySqlConnection()

        'MsgBox(myConnectionString)
        Try
            conn.ConnectionString =Class1.myConnectionString 
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

        'dA2 = New MySqlDataAdapter("call iap_db.sp_makultam('" & nim & "'", conn)

        'Dim DS2 As New DataSet()

        'dA2.Fill(DS2)
        'GridControl2.DataSource = DS2.Tables(0)
        Class1.LoadGrid(GridControl2, "call iap_db.sp_makultam('" & nim & "')")
        If GridView1.RowCount <= 0 Then

            'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False
            MsgBox("NIM Tidak terdaftar, Mohon Periksa kembali")
            SimpleButton1.Enabled = False
        Else
            WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = True

        End If



    End Sub
    Private Sub TextEdit1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextEdit1.KeyUp
        If e.KeyCode = Keys.Enter Then
            ConnectMysql(TextEdit1.EditValue.ToString.Trim)
        End If

    End Sub

    Private Sub WindowsUIButtonPanel1_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles WindowsUIButtonPanel1.ButtonClick
        SimpleButton1.Enabled = False
        DateEdit1.DoValidate()
        DateEdit2.DoValidate()
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
            Class1.stype = "Transkrip Nilai Kumulatif"
            Class1.idprogm = GridView1.GetRowCellValue(0, "idprogm")
            Class1.pejabat = vpejabat
            Class1.ipaddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try




        'strHostName = System.Net.Dns.GetHostName()

        'strIPAddress = System.Net.Dns.GetHostByName(strHostName).AddressList(0).ToString()


        Select Case e.Button.Properties.Tag


            Case 1

                If GridView1.RowCount <= 0 Then Exit Sub
                PanelControl2.Controls.Clear()



                If ComboBoxEdit2.EditValue.ToString.ToLower.Trim = "indonesia" Then
                    If GridView2.RowCount = 0 Then
                        Try
                            Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkripkumulatif_2.repx", True)

                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                    Else
                        Try
                            Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkripkumulatif_2tam.repx", True)

                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                    End If

                    Dim Tool As ReportPrintTool = New ReportPrintTool(Class1.report)
                    Dim rtb As New System.Windows.Forms.RichTextBox()

                    Dim dbirth As DateTime = GridView1.GetRowCellValue(0, "dtbirth")
                    Dim dt1 As DateTime = DateEdit1.EditValue
                    Dim dt2 As DateTime = DateEdit2.EditValue

                    Class1.report.Parameters("strnim").Visible = False
                    Class1.report.Parameters("strnim").Value = TextEdit1.EditValue


                    richIt(TextEdit2.EditValue, "richText1", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))
                    richIt(TextEdit1.EditValue, "richText2", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))
                    richIt(GridView1.GetRowCellValue(0, "meprogdi"), "richText8", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))
                    richIt(GridView1.GetRowCellValue(0, "mekonsen"), "richText9", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))
                    richIt("Jakarta, " & CInt(dt2.ToString("dd", indo)) & "  " & dt2.ToString("MMMM", indo) & "  " & dt2.ToString("yyyy", indo), "richText3", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold))
                    richIt(vpejabat, "richText4", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold Or FontStyle.Underline))
                    richIt(vjabatan_ind, "richText5", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold))

                    Tool.PreviewForm.TopLevel = False
                    Tool.PreviewForm.Parent = PanelControl2
                    Tool.PreviewForm.Dock = DockStyle.Fill
                    Tool.PreviewForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                    Tool.PrintingSystem.AddCommandHandler(New Class1.printdirect())
                    Tool.PrintingSystem.AddCommandHandler(New Class1.print())

                    Tool.ShowPreview()
                    SimpleButton1.Enabled = True
                Else

                    If GridView2.RowCount = 0 Then
                        Try
                            Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkripkumulatif_eng.repx", True)

                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                    Else
                        Try
                            Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkripkumulatif_eng_2tam.repx", True)

                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                    End If

                    Dim Tool As ReportPrintTool = New ReportPrintTool(Class1.report)
                    Dim rtb As New System.Windows.Forms.RichTextBox()

                    Dim dbirth As DateTime = GridView1.GetRowCellValue(0, "dtbirth")
                    Dim dt1 As DateTime = DateEdit1.EditValue
                    Dim dt2 As DateTime = DateEdit2.EditValue



                    Class1.report.Parameters("strnim").Value = TextEdit1.EditValue
                    Class1.report.Parameters("strnim").Visible = False

                    richIt(TextEdit2.EditValue, "richText1", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))
                    richIt(TextEdit1.EditValue, "richText2", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))

                    richIt(GridView1.GetRowCellValue(0, "meprogdiing"), "richText8", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))
                    richIt(GridView1.GetRowCellValue(0, "mekonsening"), "richText9", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))

                    richIt("Jakarta, " & dt2.ToString("MMMM", eng) & "  " & ordinal(dt2.ToString("dd", eng)) & "  " & dt2.ToString("yyyy", eng), "richText3", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold))
                    richIt(vpejabat, "richText4", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold Or FontStyle.Underline))
                    richIt(vjabatan_eng, "richText5", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold))

                    Tool.PreviewForm.TopLevel = False
                    Tool.PreviewForm.Parent = PanelControl2
                    Tool.PreviewForm.Dock = DockStyle.Fill
                    Tool.PreviewForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                    Tool.PrintingSystem.AddCommandHandler(New Class1.printdirect())
                    Tool.PrintingSystem.AddCommandHandler(New Class1.print())

                    Tool.ShowPreview()


                    '////////////////

                    'If GridView2.RowCount = 0 Then
                    '    Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkripkumulatif_eng.repx", True)
                    'Else

                    '    Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkripkumulatif_eng_2tam.repx", True)

                    'End If

                    'Class1.report.Parameters("strnim").Visible = False
                    'Class1.report.Parameters("strnim").Value = TextEdit1.EditValue
                    'Tool.PreviewForm.TopLevel = False
                    'Tool.PreviewForm.Parent = PanelControl2
                    'Tool.PreviewForm.Dock = DockStyle.Fill
                    'Tool.PreviewForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                    'Tool.PrintingSystem.AddCommandHandler(New Class1.printdirect())
                    'Tool.PrintingSystem.AddCommandHandler(New Class1.print())

                    'Tool.ShowPreview()
                    SimpleButton1.Enabled = True
                End If
                LayoutControlGroup2.Expanded = False

                'If ComboBoxEdit2.EditValue.ToString.Trim = "Indonesia" Then
                '    If GridView2.RowCount = 0 Then
                '        Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkripkumulatif_2.repx", True)

                '    Else
                '        Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkripkumulatif_2tam.repx", True)

                '    End If



                '    Class1.report.Parameters("strnim").Visible = False
                '    Class1.report.Parameters("strnim").Value = TextEdit1.EditValue


                '    richIt(TextEdit2.EditValue, "richText1", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))
                '    richIt(TextEdit1.EditValue, "richText2", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))

                '    'If ComboBoxEdit2.EditValue = "Indonesia" Then
                '    richIt(GridView1.GetRowCellValue(0, "meprogdi"), "richText8", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))
                '    richIt(GridView1.GetRowCellValue(0, "mekonsen"), "richText9", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))
                '    'Else
                '    '    richIt(GridView1.GetRowCellValue(0, "meprogdiing"), "richText8", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))
                '    '    richIt(GridView1.GetRowCellValue(0, "mekonsening"), "richText9", Class1.report, New Font("Arial Narrow", 10, FontStyle.Regular))
                '    'End If



                '    richIt("Jakarta, " & dt2.ToString("dd", indo) & "  " & dt2.ToString("MMMM", indo) & "  " & dt2.ToString("yyyy", indo), "richText3", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold))
                '    richIt(vpejabat, "richText4", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold Or FontStyle.Underline))

                '    'If ComboBoxEdit2.EditValue = "Indonesia" Then

                '    richIt(vjabatan_ind, "richText5", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold))
                '    'Else

                '    '    richIt(vjabatan_eng, "richText5", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold))
                '    'End If

                '    Tool.PreviewForm.TopLevel = False
                '    Tool.PreviewForm.Parent = PanelControl2
                '    Tool.PreviewForm.Dock = DockStyle.Fill
                '    Tool.PreviewForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                '    Tool.PrintingSystem.AddCommandHandler(New Class1.printdirect())
                '    Tool.PrintingSystem.AddCommandHandler(New Class1.print())

                '    Tool.ShowPreview()

                'Else
                '    If GridView2.RowCount = 0 Then
                '        Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkripkumulatif_eng.repx", True)
                '    Else

                '        Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_transkripkumulatif_eng_2tam.repx", True)

                '    End If







                '    rtb.Clear()
                '    rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular)
                'rtb.SelectedText = StrConv(GridView1.GetRowCellValue(0, "plbirth").ToString(), VbStrConv.ProperCase) & ", " & dbirth.ToString("MMMM", eng) & "  " & ordinal(dbirth.ToString("dd", eng)) & "  " & dbirth.ToString("yyyy", eng)
                'DirectCast(Class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf

                'rtb.Clear()
                'rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular)
                'rtb.SelectedText = dt1.ToString("MMMM", eng) & "  " & ordinal(dt1.ToString("dd", eng)) & "  " & dt1.ToString("yyyy", eng)
                'DirectCast(Class1.report.FindControl("richText2", True), XRRichText).Rtf = rtb.Rtf


                'rtb.Clear()
                'rtb.SelectionFont = New Font("Arial Narrow", 10, FontStyle.Regular)
                'rtb.SelectedText = "Jakarta, " & dt2.ToString("MMMM", eng) & "  " & ordinal(dt2.ToString("dd", eng)) & "  " & dt2.ToString("yyyy", eng)
                'DirectCast(Class1.report.FindControl("richText3", True), XRRichText).Rtf = rtb.Rtf

                'richIt(vpejabat, "richText4", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold Or FontStyle.Underline))
                'richIt(vjabatan_eng, "richText5", Class1.report, New Font("Arial Narrow", 10, FontStyle.Bold))

                'Tool.PreviewForm.TopLevel = False
                'Tool.PreviewForm.Parent = PanelControl2
                'Tool.PreviewForm.Dock = DockStyle.Fill
                'Tool.PreviewForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                'Tool.PrintingSystem.AddCommandHandler(New Class1.printdirect())
                'Tool.PrintingSystem.AddCommandHandler(New Class1.print())

                'Tool.ShowPreview()



                'SimpleButton1.Enabled = True
                'End If

            Case 2
                MsgBox("close")

        End Select

    End Sub



    Private Sub transkrip2kolom_Load(sender As Object, e As EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Iap_dbDataSet.v_pejabat' table. You can move, or remove it, as needed.
        LayoutControlGroup2.Expanded = True
        Me.V_pejabatTableAdapter.Fill(Me.Iap_dbDataSet.v_pejabat)

        TextEdit2.ReadOnly = True
        'ComboBoxEdit1.SelectedIndex = 0
        'ComboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        LayoutControlItem1.ContentVisible = False
        'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False

        LookUpEdit1.ItemIndex = 0
        LayoutControlGroup2.Expanded = False
        ComboBoxEdit2.EditValue = "Indonesia"
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

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim f As Form = F_ubahps
        f.ShowDialog()

    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged

    End Sub
    'Public Class print
    '    Implements ICommandHandler
    '    Public Overridable Sub HandleCommand(ByVal command As PrintingSystemCommand, ByVal args() As Object, ByVal control As IPrintControl, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
    '        If (Not CanHandleCommand(command, control)) Then
    '            Return
    '        End If


    '        Dim pt As New ReportPrintTool(class1.report)

    '        If (pt.PrintDialog.ToString) = True Then
    '            MsgBox("print")
    '        Else
    '            MsgBox("cancel")
    '        End If

    '        handled = True
    '    End Sub

    '    Public Overridable Function CanHandleCommand(ByVal command As PrintingSystemCommand, ByVal control As IPrintControl) As Boolean Implements ICommandHandler.CanHandleCommand
    '        ' This handler is used for the ExportGraphic command.
    '        Return command = PrintingSystemCommand.Print
    '    End Function
    'End Class
    'Public Class printdirect
    '    Implements ICommandHandler
    '    Public Overridable Sub HandleCommand(ByVal command As PrintingSystemCommand, ByVal args() As Object, ByVal control As IPrintControl, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
    '        If (Not CanHandleCommand(command, control)) Then
    '            Return
    '        End If
    '        MsgBox("sedang cetak")


    '        handled = True
    '    End Sub

    '    Public Overridable Function CanHandleCommand(ByVal command As PrintingSystemCommand, ByVal control As IPrintControl) As Boolean Implements ICommandHandler.CanHandleCommand
    '        ' This handler is used for the ExportGraphic command.

    '        Return command = PrintingSystemCommand.PrintDirect
    '    End Function
    'End Class
    'Public Class ExportToImageCommandHandler
    '    Implements ICommandHandler
    '    Public Overridable Sub HandleCommand(ByVal command As PrintingSystemCommand, ByVal args() As Object, ByVal control As IPrintControl, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
    '        If (Not CanHandleCommand(command, control)) Then
    '            Return
    '        End If

    '        ' Export the document to png.
    '        control.PrintingSystem.ExportToImage("C:\Report.png", ImageFormat.Png)

    '        ' Set handled to 'true' to prevent the standard exporting procedure from being called.
    '        handled = True
    '    End Sub

    '    Public Overridable Function CanHandleCommand(ByVal command As PrintingSystemCommand, ByVal control As IPrintControl) As Boolean Implements ICommandHandler.CanHandleCommand
    '        ' This handler is used for the ExportGraphic command.
    '        Return command = PrintingSystemCommand.ExportGraphic
    '    End Function
    'End Class
End Class