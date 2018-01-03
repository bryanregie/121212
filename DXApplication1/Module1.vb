Imports System.Globalization
Imports DevExpress.XtraEditors
Imports DevExpress.XtraNavBar
Imports MySql.Data.MySqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.UI
Imports System.Text

Module Module1
    Public lanjut As Boolean
    Public printernem, yser, nik As String
    Public eng As New CultureInfo("en-US")
    Public indo As New CultureInfo("id-ID")
    Public smt() As String = {"", "I (Satu)", "II (Dua)", "III (Tiga)", "IV (Empat)", "V (Lima)", "VI (Enam)", "VII (Tujuh)", "VIII (Delapan)", "IX (Sembilan)", "X (Sepuluh)", "XI (Sebelas)", "XII (Dua Belas)", "XIII (Tiga Belas)", "XIV (Empat Belas)"}
    Public bln() As String = {"", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII"}

    Public tran_nim, tran_nama, tran_sind, tran_sing As String

    Public conn As MySql.Data.MySqlClient.MySqlConnection
    Public Function ordinal(dett As String) As String

        Select Case Right(dett, 1)
            Case "1"
                Return CInt(dett) & "st"
            Case "2"
                Return CInt(dett) & "nd"
            Case "3"
                Return CInt(dett) & "rd"
            Case Else

                Return CInt(dett) & "th"
        End Select


    End Function

    'Public myConnectionString As String = "server=172.16.0.12;port=3306;user id=fingerprint; password=fingerprint2016; database=iap_db;persist security info=true;CharSet=utf8;convert zero datetime=True; "
    Public Sub mdilod()

        MDI.ribbonControl.Minimized = True
        MDI.keskreGroup.SmallImage = My.Resources.buka_16x16
        MDI.statUser.Caption = yser
    End Sub

    Public Sub richIt(text As String, richtext As String, rpt As DevExpress.XtraReports.UI.XtraReport, Optional fon As Font = Nothing)
        If (IsNothing(fon)) = True Then

            fon = New Font("Arial Narrow", 10, FontStyle.Regular)
        End If

        Dim rtb As New System.Windows.Forms.RichTextBox()
        rtb.Clear()
        rtb.SelectionFont = fon
        rtb.SelectedText = text
        Try
            DirectCast(rpt.FindControl(richtext, True), XRRichText).Rtf = rtb.Rtf
        Catch ex As Exception

        End Try

    End Sub

    Public Sub loadDocr(form As Form, panel As PanelControl)
        panel.Controls.Clear()
        form.TopLevel = False
        form.Parent = panel
        form.Dock = DockStyle.Fill
        panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        form.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        form.Show()
    End Sub
    Public Sub textOut(str As String, richtb As System.Windows.Forms.RichTextBox, Optional fon As Font = Nothing, Optional align As System.Windows.Forms.HorizontalAlignment = HorizontalAlignment.Left)
        If (IsNothing(fon)) = True Then

            fon = New Font("Arial Narrow", 10, FontStyle.Regular)
        End If



        richtb.SelectionFont = fon
        richtb.SelectionAlignment = align
        If str <> "" Then
            richtb.SelectedText = Replace(str, "[par]", "  ")
        Else
            richtb.SelectedText = ""
        End If


    End Sub
    Public Sub iconic(navbar As NavBarControl, item As NavBarItem)
        Dim I As Integer
        For I = 0 To navbar.Items.Count - 1
            Dim Itm As NavBarItem = navbar.Items(I)
            ' perform some operations on an item here
            '..
            Itm.SmallImage = Nothing
        Next

        item.SmallImage = My.Resources.Next_16x16
    End Sub

    Public Function Kwery(strQuery As String) As DataTable
        Dim dA As MySqlDataAdapter
        dA = New MySqlDataAdapter(strQuery, conn)
        Dim DS As New DataSet()
        dA.Fill(DS)
        Return DS.Tables(0)
    End Function
    Public Sub KweryGrid(strQuery As String, grid As GridControl)
        Dim dA As MySqlDataAdapter
        dA = New MySqlDataAdapter(strQuery, conn)
        Dim DS As New DataSet()
        dA.Fill(DS)
        grid.DataSource = DS.Tables(0)
    End Sub
    Public Function KweryRvalue(strQuery As String, colm As String) As Object
        Dim dA As MySqlDataAdapter
        dA = New MySqlDataAdapter(strQuery, conn)
        Dim DS As New DataSet()
        dA.Fill(DS)

        If (DS.Tables(0).Rows.Count) > 0 Then
            Return DS.Tables(0).Rows(0)(colm)
        Else
            Return ""

        End If


    End Function
    Public Sub Konek()
        conn = New MySql.Data.MySqlClient.MySqlConnection()

        Try
            conn.ConnectionString = Class1.myConnectionString
            conn.Open()
        Catch ex As Exception

            MsgBox(ex.Message)
            Return
        End Try
    End Sub

    Public Function NotReg(grid As GridView, Optional win8 As DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel = Nothing) As Boolean

        If grid.RowCount <= 0 Then
            If IsNothing(win8) = False Then win8.Buttons(0).Properties.Enabled = False

            MsgBox("NIM Tidak terdaftar, Mohon Periksa kembali")
            Return False
            Exit Function
        Else

            If IsNothing(win8) = False Then win8.Buttons(0).Properties.Enabled = True
            Return True
            Exit Function
        End If
    End Function


End Module