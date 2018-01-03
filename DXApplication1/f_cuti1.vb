Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports System.Drawing.Imaging
Imports DevExpress.XtraPrinting.Preview

Public Class f_cuti1
    ' Dim myConnectionString As String


    'Dim conn As MySql.Data.MySqlClient.MySqlConnection

    Dim dA0, dA, dA2, dA3 As MySqlDataAdapter

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Class1.detform(DateEdit1)

        Class1.detform(DateEdit4)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ConnectMysql(nim As String)

        Konek()
        KweryGrid("call iap_db.sp_transkripheader2018('" & nim & "','%01%','%%','%%')", GridControl1)
        TextEdit2.EditValue = KweryRvalue("call iap_db.sp_transkripheader2018('" & nim & "','%01%','%%','%%')", "fullname")
        'TextEdit3.EditValue = ""
        'If NotReg(GridView1, WindowsUIButtonPanel1) = False Then Exit Sub

        If GridView1.RowCount <= 0 Then
            'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False
            MsgBox("NIM Tidak terdaftar atau mahasiswa tidak aktif, Mohon Periksa kembali")
            Exit Sub
            'Else
            '    WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = True
        End If

        KweryGrid("select * from cmdijazah where kode ='skcuti1'", GridControl2)
        '// double nosurat
        Dim ns As String = KweryRvalue("select nosurat from satr0020 where idnim = '" & TextEdit1.EditValue & "' and type='SK Cuti' order by nosurat desc ", "nosurat")

        If ns.Trim <> "" Then
            Class1.addnodoc = False
            TextEdit3.EditValue = ns
            Exit Sub
        Else

            Class1.addnodoc = True

        End If

        Dim a1, a2, a3, a4, a5 As String
        a1 = Microsoft.VisualBasic.Right("0000" & KweryRvalue("select *,now() as tgl from cmdnodok where kodedok ='SKCT'", "nourut"), 4)
        a2 = "IBIKKG-BAAK"
        a3 = KweryRvalue("select *,now() as tgl from cmdnodok where kodedok ='SKCT'", "prefix")
        a4 = bln(Month(KweryRvalue("select *,now() as tgl from cmdnodok where kodedok ='SKCT'", "tgl")))
        a5 = Microsoft.VisualBasic.Right(Year(KweryRvalue("select *,now() as tgl from cmdnodok where kodedok ='SKCT'", "tgl")), 2)
        TextEdit3.EditValue = a1 & "/" & a2 & "/" & a3 & "/" & a4 & "/" & a5


    End Sub
    Private Sub TextEdit1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextEdit1.KeyUp
        If e.KeyCode = Keys.Enter Then
            ConnectMysql(TextEdit1.EditValue.ToString.Trim)
        End If

    End Sub

    Private Sub TextEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit1.EditValueChanged

    End Sub
    'Private Shared Report As XtraReport = Xtraclass1.report.FromFile("rpx\r_cuti1.repx", True)
    Private Sub WindowsUIButtonPanel1_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles WindowsUIButtonPanel1.ButtonClick
        MsgBox("Perhatian, Saat melakukan Print / Cetak," & vbCrLf & "maka akan merubah status mahasiswa menjadi cuti")

        DateEdit1.DoValidate()
        DateEdit4.DoValidate()
        ComboBoxEdit1.DoValidate()
        ComboBoxEdit2.DoValidate()
        ConnectMysql(TextEdit1.EditValue.ToString.Trim)

        If GridView1.RowCount <= 0 Then Exit Sub
        Try
            Class1.report = XtraReport.FromFile(Application.StartupPath + "\rpx\r_cuti1.repx", True)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        'pejabat
        Dim row As DataRowView = CType(LookUpEdit1.Properties.GetDataSourceRowByKeyValue(LookUpEdit1.EditValue), DataRowView)
        Dim vpejabat As String = row("fullname")
        Dim vjabatan_ind As String = row("org_desc")
        Dim vjabatan_eng As String = row("jabatan")

        Class1.strname = TextEdit2.EditValue
        Class1.strnim = TextEdit1.EditValue

        Try
            Class1.idprogdi = GridView1.GetRowCellValue(0, "idprogdi")
            Class1.idthnaka = GridView1.GetRowCellValue(0, "idthnaka")
            Class1.idevent = GridView1.GetRowCellValue(0, "idevent")
            Class1.idsemest = GridView1.GetRowCellValue(0, "idsemest").ToString
            Class1.nosurat = TextEdit3.EditValue
            Class1.idactor = yser
            Class1.status = ""
            Class1.note = ""
            Class1.stype = "SK Cuti"
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


                Dim Tool As ReportPrintTool = New ReportPrintTool(Class1.report)
                Dim rtb As New System.Windows.Forms.RichTextBox()

                Dim dbirth As DateTime = GridView1.GetRowCellValue(0, "dtbirth")
                Dim dt1 As DateTime = DateEdit1.EditValue

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
                textOut(TextEdit3.EditValue, rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                DirectCast(Class1.report.FindControl("richText1", True), XRRichText).Rtf = rtb.Rtf


                rtb.Clear()
                textOut(GridView2.GetRowCellValue(0, "par24"), rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                DirectCast(Class1.report.FindControl("richText2", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                textOut(GridView1.GetRowCellValue(0, "fullname"), rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                DirectCast(Class1.report.FindControl("richText5", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                textOut(GridView1.GetRowCellValue(0, "idnim"), rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                DirectCast(Class1.report.FindControl("richText6", True), XRRichText).Rtf = rtb.Rtf

                rtb.Clear()
                textOut(GridView2.GetRowCellValue(0, "par25"), rtb, New Font("Arial Narrow", 20, FontStyle.Underline Or FontStyle.Bold), HorizontalAlignment.Center)
                DirectCast(Class1.report.FindControl("richText3", True), XRRichText).Rtf = rtb.Rtf



                rtb.Clear()


                textOut(GridView2.GetRowCellValue(0, "par1"), rtb)
                textOut("  " & GridView2.GetRowCellValue(0, "par2"), rtb)
                textOut("  " & CInt((dt1.ToString("dd", indo))) & "  " & dt1.ToString("MMMM", indo) & "  " & dt1.ToString("yyyy", indo), rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                textOut("  " & GridView2.GetRowCellValue(0, "par3"), rtb)
                textOut("  " & GridView2.GetRowCellValue(0, "par4"), rtb)
                textOut("  " & ComboBoxEdit2.EditValue, rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                textOut("  " & GridView2.GetRowCellValue(0, "par5"), rtb)
                textOut("  " & ComboBoxEdit1.EditValue, rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Bold))
                textOut("  " & GridView2.GetRowCellValue(0, "par6") + vbNewLine + vbNewLine, rtb)
                textOut(GridView2.GetRowCellValue(0, "par7"), rtb)

                textOut(vbNewLine + vbNewLine, rtb)
                rtb.SelectionBullet = True
                rtb.SelectionHangingIndent = 20
                textOut(GridView2.GetRowCellValue(0, "par8"), rtb)
                textOut(vbNewLine + vbNewLine, rtb)
                'rtb.SelectionBullet = True
                textOut(GridView2.GetRowCellValue(0, "par9"), rtb)
                'textOut(GridView1.GetRowCellValue(0, "fullname") & vbNewLine, rtb)
                'rtb.SelectionBullet = False
                textOut(vbNewLine + vbNewLine, rtb)

                rtb.SelectionBullet = False
                textOut(GridView2.GetRowCellValue(0, "par10"), rtb)


                textOut(vbNewLine + vbNewLine, rtb)
                textOut(vbNewLine + vbNewLine, rtb)
                textOut("Jakarta, " & CInt(dt4.ToString("dd", indo)) & "  " & dt4.ToString("MMMM", indo) & "  " & dt4.ToString("yyyy", indo), rtb)
                textOut(vbNewLine + vbNewLine, rtb)
                textOut(vbNewLine + vbNewLine, rtb)
                textOut(vbNewLine + vbNewLine, rtb)
                textOut(vpejabat & vbNewLine, rtb, New Font("Arial Narrow", 12, FontStyle.Bold Or FontStyle.Underline))
                textOut(vjabatan_ind & vbNewLine, rtb, New Font("Arial Narrow", 10, FontStyle.Regular Or FontStyle.Italic))

                textOut(vbNewLine + vbNewLine, rtb)
                textOut(vbNewLine + vbNewLine, rtb)
                rtb.SelectionHangingIndent = 20
                textOut("CC :" & vbTab & GridView2.GetRowCellValue(0, "par12") + vbNewLine, rtb)
                textOut(vbTab & vbTab & GridView2.GetRowCellValue(0, "par13") + vbNewLine, rtb)
                textOut(vbTab & vbTab & GridView2.GetRowCellValue(0, "par14"), rtb)
                DirectCast(Class1.report.FindControl("richText4", True), XRRichText).Rtf = rtb.Rtf





                Tool.PreviewForm.TopLevel = False
                Tool.PreviewForm.Parent = PanelControl2
                Tool.PreviewForm.Dock = DockStyle.Fill
                Tool.PreviewForm.FormBorderStyle = Windows.Forms.FormBorderStyle.None


                'Tool.PrintingSystem.AddCommandHandler(New printdirect())
                'Tool.PrintingSystem.AddCommandHandler(New print())

                'Dim rb As PrintPreviewRibbonFormEx = New PrintPreviewRibbonFormEx
                'rb.PrintingSystem = Tool.PrintingSystem
                'rb.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Print, DevExpress.XtraPrinting.CommandVisibility.None)

                Tool.PrintingSystem.AddCommandHandler(New Class1.printdirect())
                Tool.PrintingSystem.AddCommandHandler(New Class1.print())

                Tool.ShowPreview()


                LayoutControlGroup2.Expanded = False
                'Dim ps As PrintingSystemBase = Tool.PrintingSystem

                ' Zoom the print preview, so that it fits the entire page.
                'ps.ExecCommand(PrintingSystemCommand.ViewWholePage)

                '' Invoke the Hand tool.
                'ps.ExecCommand(PrintingSystemCommand.HandTool, New Object() {True})

                ' Hide the Hand tool.
                'ps.ExecCommand(PrintingSystemCommand.Print, New Object() {False})
            Case 2
                MsgBox("close")

        End Select
    End Sub

    Private Sub f_konfirmasiregs_Load(sender As Object, e As EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Iap_dbDataSet.v_pejabat' table. You can move, or remove it, as needed.
        LayoutControlGroup2.Expanded = True
        Me.V_pejabatTableAdapter.Fill(Me.Iap_dbDataSet.v_pejabat)
        DateEdit1.EditValue = Now()
        'DateEdit2.EditValue = Now()
        'DateEdit3.EditValue = Now()
        DateEdit4.EditValue = Now()


        For i = 1996 To Year(Now()) + 1
            ComboBoxEdit1.Properties.Items.Add(i - 1 & " - " & i)
        Next i

        ComboBoxEdit1.SelectedIndex = ComboBoxEdit1.Properties.Items.Count - 1
        ComboBoxEdit2.SelectedIndex = 0
        LayoutControlItem1.ContentVisible = False
        'WindowsUIButtonPanel1.Buttons(0).Properties.Enabled = False
        TextEdit2.ReadOnly = True
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
    Public Class print
        Implements ICommandHandler
        Public Overridable Sub HandleCommand(ByVal command As PrintingSystemCommand, ByVal args() As Object, ByVal control As IPrintControl, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
            If (Not CanHandleCommand(command, control)) Then
                Return
            End If


            'Dim PrintDialog1 As New PrintDialog()
            ''PrintDialog1.Document()
            ''Dim result As DialogResult = PrintDialog1.ShowDialog()

            'If PrintDialog1.ShowDialog = DialogResult.OK Then

            'Dim report As XtraReport = Xtraclass1.report.FromFile("rpx\r_cuti1.repx", True)

            'Specify the printer name. 
            'class1.report.PrinterName = PrintDialog1.PrinterSettings.PrinterName.ToString

            'Create the document. 
            'class1.report.CreateDocument()

            Dim pt As New ReportPrintTool(Class1.report)
            'pt.PrintDialog()

            If (pt.PrintDialog.ToString) = True Then
                MsgBox("print")
            Else
                MsgBox("cancel")
            End If
            'If pt.PrintDialog.Value() Then


            'End If

            'MsgBox("sedang cetaks 3")
            ' Export the document to png.
            'control.PrintingSystem.ExportToImage("C:\class1.report.png", ImageFormat.Png)

            ' Set handled to 'true' to prevent the standard exporting procedure from being called.
            handled = True
        End Sub

        Public Overridable Function CanHandleCommand(ByVal command As PrintingSystemCommand, ByVal control As IPrintControl) As Boolean Implements ICommandHandler.CanHandleCommand
            ' This handler is used for the ExportGraphic command.
            Return command = PrintingSystemCommand.Print
        End Function
    End Class
    Public Class printdirect
        Implements ICommandHandler
        Public Overridable Sub HandleCommand(ByVal command As PrintingSystemCommand, ByVal args() As Object, ByVal control As IPrintControl, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
            If (Not CanHandleCommand(command, control)) Then
                Return
            End If
            MsgBox("sedang cetak")


            ' Export the document to png.
            'control.PrintingSystem.ExportToImage("C:\class1.report.png", ImageFormat.Png)

            ' Set handled to 'true' to prevent the standard exporting procedure from being called.
            handled = True
        End Sub

        Public Overridable Function CanHandleCommand(ByVal command As PrintingSystemCommand, ByVal control As IPrintControl) As Boolean Implements ICommandHandler.CanHandleCommand
            ' This handler is used for the ExportGraphic command.

            Return command = PrintingSystemCommand.PrintDirect
        End Function
    End Class
    Public Class ExportToImageCommandHandler
        Implements ICommandHandler
        Public Overridable Sub HandleCommand(ByVal command As PrintingSystemCommand, ByVal args() As Object, ByVal control As IPrintControl, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
            If (Not CanHandleCommand(command, control)) Then
                Return
            End If

            ' Export the document to png.
            control.PrintingSystem.ExportToImage("C:\class1.report.png", ImageFormat.Png)

            ' Set handled to 'true' to prevent the standard exporting procedure from being called.
            handled = True
        End Sub

        Public Overridable Function CanHandleCommand(ByVal command As PrintingSystemCommand, ByVal control As IPrintControl) As Boolean Implements ICommandHandler.CanHandleCommand
            ' This handler is used for the ExportGraphic command.
            Return command = PrintingSystemCommand.ExportGraphic
        End Function
    End Class
End Class
