Imports System.Drawing.Imaging
Imports System.Text
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Tile
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
Imports MySql.Data.MySqlClient

Public Class Class1
    Public Shared addnodoc As Boolean = True
    Public Shared report As New XtraReport
    Public Shared myConnectionString, myConnectionString2, myConnectionString3 As String
    Public Shared strnim, strname, idprogdi, iduser, idpcname,
        idthnaka, idevent, idsemest, nosurat, idactor, status, note, stype, idprogm, pejabat, ipaddress As String
    Public Shared Sub GridNumbering(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs)

        If e.Info.IsRowIndicator Then
            If CInt(e.RowHandle.ToString()) >= 0 Then
                e.Info.DisplayText = CStr(CInt(e.RowHandle.ToString()) + 1)

            Else
                e.Info.DisplayText = "*"
            End If

            'e.Appearance.Font = My.Settings.
            'e.Appearance.ForeColor = My.Settings.fontcolor1
            e.Info.HeaderPosition = DevExpress.Utils.Drawing.HeaderPositionKind.Center
            e.Appearance.Font = New Font("Tahoma", 10)
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            e.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        End If
        e.Info.ImageIndex = -1 'row cursor
    End Sub
    Public Shared Sub GridRightClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs)
        On Error Resume Next
        For i = 0 To e.Menu.Items.Count - 1
            If e.Menu.Items(i).Caption = "Remove This Column" Or e.Menu.Items(i).Caption = "Show Group By Box" Or
                e.Menu.Items(i).Caption = "Group By This Column" Or e.Menu.Items(i).Caption = "Filter Editor..." Or
                e.Menu.Items(i).Caption = "Show Find Panel" Or e.Menu.Items(i).Caption = "Best Fit (all columns)" Or
                e.Menu.Items(i).Caption = "Show Find Panel" Or e.Menu.Items(i).Caption = "Best Fit" Or
                e.Menu.Items(i).Caption = "Show Auto Filter Row" Or e.Menu.Items(i).Caption = "Hide This Column" Or e.Menu.Items(i).Caption = "Hide Group By Box" Then e.Menu.Items.Item(i).Visible = False
        Next
    End Sub
    Public Shared Sub LoadSLook(Grid As SearchLookUpEdit, Query As String, Optional colIndex As Integer = 0, Optional footr As Boolean = False, Optional grup As Boolean = False, Optional edit As Boolean = False, Optional vertline As Boolean = True, Optional horzline As Boolean = True)

        Dim ds As DataSet = New DataSet
        Dim mysqlAdapter As MySqlDataAdapter = New MySqlDataAdapter(Query, myConnectionString)

        mysqlAdapter.Fill(ds)



        Dim dvManager As DataViewManager = New DataViewManager(ds)
        Dim dv As DataView = dvManager.CreateDataView(ds.Tables(0))
        Grid.Properties.DataSource = dv
        Grid.Properties.PopulateViewColumns()
        Grid.Properties.DisplayMember = Grid.Properties.View.Columns(colIndex).ToString
        Grid.Properties.ValueMember = Grid.Properties.View.Columns(colIndex).ToString
        Grid.Properties.NullText = ""
        Grid.Properties.View.HideCustomization()
        Grid.Properties.View.HideEditForm()
        Grid.Properties.View.IndicatorWidth = 50
        Grid.Properties.View.OptionsBehavior.Editable = False


        'Grid.Properties.View.Appearance.Row.Font = My.MySettings.Default.textfont1
        Grid.Properties.View.Appearance.EvenRow.BackColor = Color.Gainsboro
        Grid.Properties.View.Appearance.OddRow.BackColor = Color.White
        'Grid.Properties.View.Appearance.FocusedCell.BackColor = My.MySettings.Default.focuscel
        'Grid.Properties.View.Appearance.FocusedRow.BackColor = My.MySettings.Default.focusrow
        Grid.Properties.View.OptionsView.EnableAppearanceEvenRow = True
        Grid.Properties.View.OptionsView.EnableAppearanceOddRow = True
        Grid.Refresh()
        Grid.Properties.View.RefreshData()
    End Sub
    Public Shared Sub LoadLook(Grid As LookUpEdit, Query As String, Optional coldis As Integer = 0, Optional colval As Integer = 0)

        Dim ds As DataSet = New DataSet
        Dim mysqlAdapter As MySqlDataAdapter = New MySqlDataAdapter(Query, myConnectionString)

        mysqlAdapter.Fill(ds)

        Dim dvManager As DataViewManager = New DataViewManager(ds)
        Dim dv As DataView = dvManager.CreateDataView(ds.Tables(0))
        Grid.Properties.DataSource = dv
        Grid.Properties.PopulateColumns()
        Grid.Properties.NullText = ""
        Grid.Properties.DisplayMember = Grid.Properties.Columns(coldis).FieldName.ToString
        Grid.Properties.ValueMember = Grid.Properties.Columns(colval).FieldName.ToString
        Grid.Refresh()



    End Sub
    Public Shared Sub GridSummary(gv As GridView, Col As String, typ As DevExpress.Data.SummaryItemType, Display As String)
        gv.Columns(Col).SummaryItem.SummaryType = typ
        gv.Columns(Col).SummaryItem.FieldName = Col
        gv.Columns(Col).SummaryItem.DisplayFormat = Display
    End Sub
    Public Shared Sub LoadGrid(Grid As GridControl, Query As String, Optional footr As Boolean = True, Optional grup As Boolean = True, Optional edit As Boolean = False, Optional vertline As Boolean = True, Optional horzline As Boolean = True)
        Dim gv As GridView = CType(Grid.ViewCollection(0), GridView)
        Dim ds As DataSet = New DataSet
        Dim mysqlAdapter As MySqlDataAdapter = New MySqlDataAdapter(Query, myConnectionString)

        mysqlAdapter.Fill(ds)
        'ds.WriteXml("d:\dd.xml", XmlWriteMode.WriteSchema)

        'Dim dvManager As DataViewManager = New DataViewManager(ds)
        'Dim dv As DataView = dvManager.CreateDataView(ds.Tables(0))

        gv.Columns.Clear()
        'Grid.DataSource = dv
        Grid.DataSource = ds.Tables(0).DefaultView
        Grid.MainView.PopulateColumns()


        gv.OptionsView.ShowFooter = footr
        gv.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways
        gv.OptionsView.ShowGroupPanel = grup
        gv.OptionsBehavior.Editable = edit
        gv.Appearance.HeaderPanel.Font = New Font("Tahoma", 10, FontStyle.Bold)
        gv.Appearance.Row.Font = New Font("Tahoma", 10)
        gv.Appearance.Row.ForeColor = Color.Black
        gv.Appearance.EvenRow.BackColor = Color.Wheat
        gv.Appearance.OddRow.BackColor = Color.White
        'gv.Appearance.FocusedCell.BackColor = My.MySettings.Default.focuscel
        'gv.Appearance.FocusedRow.BackColor = My.MySettings.Default.focusrow
        gv.OptionsView.EnableAppearanceEvenRow = True
        gv.OptionsView.EnableAppearanceOddRow = True
        gv.IndicatorWidth = 50
        gv.OptionsView.ColumnHeaderAutoHeight = True
        gv.OptionsView.RowAutoHeight = True
        gv.OptionsView.ShowVerticalLines = vertline
        gv.OptionsView.ShowHorizontalLines = horzline
        Grid.Refresh()
        gv.RefreshData()
    End Sub
    Public Shared Sub LoadGridT(Grid As GridControl, Query As String, Optional footr As Boolean = False, Optional grup As Boolean = False, Optional edit As Boolean = False, Optional vertline As Boolean = True, Optional horzline As Boolean = True)
        'Dim gv As TileView = CType(Grid.ViewCollection(0), TileView)
        Dim gv As TileView = New TileView
        Dim gc, gd, ge As TileViewColumn '= New TileViewColumn
        Dim item As TileViewItemElement '= New TileViewItemElement

        gv.OptionsTiles.ColumnCount = 4
        gv.OptionsTiles.RowCount = 4
        Grid.MainView = gv


        gc = New TileViewColumn
        item = New TileViewItemElement
        gc.Name = "col1"
        gc.FieldName = "nama"
        gc.Visible = True
        gc.VisibleIndex = 0
        item.Column = gc
        gv.TileTemplate.Add(item)

        gd = New TileViewColumn
        item = New TileViewItemElement
        gd.Name = "col2"
        gd.FieldName = "kelas"
        gd.Visible = True
        gd.VisibleIndex = 1

        item.Column = gd
        gv.TileTemplate.Add(item)

        ge = New TileViewColumn
        item = New TileViewItemElement


        ge.Name = "col3"
        ge.FieldName = "img"
        ge.Visible = True
        ge.VisibleIndex = 2
        item.ImageBorder = TileItemElementImageBorderMode.SingleBorder
        item.ImageScaleMode = TileItemImageScaleMode.Squeeze
        item.ImageAlignment = TileItemContentAlignment.MiddleCenter
        item.Column = ge
        gv.TileTemplate.Add(item)

        Grid.ForceInitialize()

        'GridControl1.LevelTree.Nodes(0).LevelTemplate = tileview1


        Dim ds As DataSet = New DataSet
        Dim mysqlAdapter As MySqlDataAdapter = New MySqlDataAdapter(Query, myConnectionString)

        mysqlAdapter.Fill(ds)
        'ds.WriteXml("d:\dd.xml", XmlWriteMode.WriteSchema)

        'Dim dvManager As DataViewManager = New DataViewManager(ds)
        'Dim dv As DataView = dvManager.CreateDataView(ds.Tables(0))

        gv.Columns.Clear()
        'Grid.DataSource = dv
        Grid.DataSource = ds.Tables(0).DefaultView
        Grid.MainView.PopulateColumns()

        'gv.OptionsView.ShowFooter = footr
        'gv.OptionsView.ShowGroupPanel = grup
        'gv.OptionsBehavior.Editable = edit
        'gv.Appearance.HeaderPanel.Font = My.MySettings.Default.textfont2

        'gv.Appearance.Row.Font = My.MySettings.Default.textfont1
        'gv.Appearance.EvenRow.BackColor = My.MySettings.Default.evencolor
        'gv.Appearance.OddRow.BackColor = My.MySettings.Default.oddcolor
        'gv.Appearance.FocusedCell.BackColor = My.MySettings.Default.focuscel
        'gv.Appearance.FocusedRow.BackColor = My.MySettings.Default.focusrow
        'gv.OptionsView.EnableAppearanceEvenRow = True
        'gv.OptionsView.EnableAppearanceOddRow = True
        'gv.IndicatorWidth = 50
        'gv.OptionsView.ColumnHeaderAutoHeight = True
        'gv.OptionsView.RowAutoHeight = True
        'gv.OptionsView.ShowVerticalLines = vertline
        'gv.OptionsView.ShowHorizontalLines = horzline
        Grid.Refresh()
        gv.RefreshData()
    End Sub
    Public Shared Sub detform(det As DateEdit, Optional mask As String = "dd/MMMM/yyyy")
        det.Properties.Mask.EditMask = mask
        det.Properties.Mask.UseMaskAsDisplayFormat = True
    End Sub

    Public Shared Function MysqlQuery(Query As String, Optional ConnectionStr As String = "") As String
        Dim sCommand As StringBuilder = New StringBuilder(Query)



        'MsgBox(sCommand.ToString)
        Using mConnection As MySqlConnection = New MySqlConnection(IIf(ConnectionStr = "", myConnectionString, ConnectionStr))
            mConnection.Open()
            Using myCmd As MySqlCommand = New MySqlCommand(sCommand.ToString(), mConnection)




                myCmd.CommandType = CommandType.Text
                Try
                    Return myCmd.ExecuteScalar().ToString
                Catch ex As Exception
                    Return ""
                    MsgBox(ex.Message.ToString)
                End Try
            End Using
        End Using
    End Function
    Public Shared Sub MysqlUIDNew(Query As String, Optional lis As List(Of String) = Nothing, Optional PicParam As String = "", Optional MaxPicParam As Double = 0, Optional dic As Dictionary(Of String, Byte()) = Nothing,
                                  Optional dic2 As Dictionary(Of String, Object) = Nothing, Optional ConnectionStr As String = "")
        Dim sCommand As StringBuilder = New StringBuilder(Query)

        If IsNothing(lis) = False Then
            sCommand.Append(String.Join(", ", lis))
            sCommand.Append(";")

            sCommand = sCommand.Replace("'@", "@")
            sCommand = sCommand.Replace("@'", "")

        End If

        'MsgBox(sCommand.ToString)
        Using mConnection As MySqlConnection = New MySqlConnection(IIf(ConnectionStr = "", myConnectionString, ConnectionStr))
            mConnection.Open()
            Using myCmd As MySqlCommand = New MySqlCommand(sCommand.ToString(), mConnection)

                If IsNothing(dic) = False Then
                    For i = 0 To MaxPicParam
                        If dic.ContainsKey(i) Then
                            myCmd.Parameters.AddWithValue("@" & PicParam & i, dic(i))
                        Else

                            myCmd.Parameters.AddWithValue("@" & PicParam & i, Nothing)
                        End If
                    Next
                End If

                If IsNothing(dic2) = False Then
                    For i = 0 To MaxPicParam
                        If dic2.ContainsKey(i) Then
                            myCmd.Parameters.AddWithValue("@" & PicParam & i, dic2(i))
                        Else

                            myCmd.Parameters.AddWithValue("@" & PicParam & i, Nothing)
                        End If
                    Next
                End If



                myCmd.CommandType = CommandType.Text
                Try
                    myCmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                End Try
            End Using
        End Using
    End Sub

    Public Class print
        Implements ICommandHandler
        Public Overridable Sub HandleCommand(ByVal command As PrintingSystemCommand, ByVal args() As Object, ByVal control As IPrintControl, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
            If (Not CanHandleCommand(command, control)) Then
                Return
            End If


            Dim pt As New ReportPrintTool(Class1.report)

            If (pt.PrintDialog.ToString) = True Then

                Class1.MysqlUIDNew("insert into satr0020 (fullname,idnim,idprogdi,idthnaka,idevent,idsemest,nosurat,idactor,status,note,type,idprogm,pejabat,ipaddress) values " &
                "('" & Class1.strname & "','" & Class1.strnim & "','" & Class1.idprogdi & "','" & Class1.idthnaka & "','" & Class1.idevent & "','" & Class1.idsemest & "','" & Class1.nosurat & "','" & Class1.idactor & "','" & Class1.status & "','" & Class1.note & "','" & Class1.stype & "','" & Class1.idprogm & "','" & Class1.pejabat & "','" & Class1.ipaddress & "')")

                'update status mahasiswa
                Select Case Class1.stype

                    Case "SK Cuti"
                        Class1.MysqlUIDNew("update satr9001 set idsubact = '5500' where idnim = '" & Class1.strnim & "' ")
                        If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKCT' ")

                    Case "SK Cuti Extend"
                        Class1.MysqlUIDNew("update satr9001 set idsubact = '5500' where idnim = '" & Class1.strnim & "' ")
                        If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKCT' ")

                    Case "SK Mengundurkan Diri"
                        Class1.MysqlUIDNew("update satr9001 set idsubact = '2404' where idnim = '" & Class1.strnim & "' ")
                        If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKRS' ")

                    Case "SK Lulus Sementara"
                        If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKLS' ")

                    Case "SK Aktif"
                        If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKAC' ")



                    Case "Ijazah"

                    Case "Konfirmasi Registrasi"
                        If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKRG' ")
                    Case "Lap Hasil Studi"
                    Case "Transkrip Akademik"
                    Case "Transkrip Nilai Kumulatif"

                End Select


            End If

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

            report.Print
            Class1.MysqlUIDNew("insert into satr0020 (fullname,idnim,idprogdi,idthnaka,idevent,idsemest,nosurat,idactor,status,note,type,idprogm,pejabat,ipaddress) values " &
                "('" & Class1.strname & "','" & Class1.strnim & "','" & Class1.idprogdi & "','" & Class1.idthnaka & "','" & Class1.idevent & "','" & Class1.idsemest & "','" & Class1.nosurat & "','" & Class1.idactor & "','" & Class1.status & "','" & Class1.note & "','" & Class1.stype & "','" & Class1.idprogm & "','" & Class1.pejabat & "','" & Class1.ipaddress & "')")

            'update status mahasiswa
            Select Case Class1.stype

                Case "SK Cuti"
                    Class1.MysqlUIDNew("update satr9001 set idsubact = '5500' where idnim = '" & Class1.strnim & "' ")
                    If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKCT' ")

                Case "SK Cuti Extend"
                    Class1.MysqlUIDNew("update satr9001 set idsubact = '5500' where idnim = '" & Class1.strnim & "' ")
                    If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKCT' ")

                Case "SK Mengundurkan Diri"
                    Class1.MysqlUIDNew("update satr9001 set idsubact = '2404' where idnim = '" & Class1.strnim & "' ")
                    If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKRS' ")

                Case "SK Lulus Sementara"
                    If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKLS' ")

                Case "SK Aktif"
                    If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKAC' ")



                Case "Ijazah"

                Case "Konfirmasi Registrasi"
                    If addnodoc = True Then Class1.MysqlUIDNew("update cmdnodok set nourut = nourut + 1 where kodedok = 'SKRG' ")
                Case "Lap Hasil Studi"
                Case "Transkrip Akademik"
                Case "Transkrip Nilai Kumulatif"

            End Select

            'MsgBox("sedang cetak")

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
            control.PrintingSystem.ExportToImage("C:\Report.png", ImageFormat.Png)

            ' Set handled to 'true' to prevent the standard exporting procedure from being called.
            handled = True
        End Sub

        Public Overridable Function CanHandleCommand(ByVal command As PrintingSystemCommand, ByVal control As IPrintControl) As Boolean Implements ICommandHandler.CanHandleCommand
            ' This handler is used for the ExportGraphic command.
            Return command = PrintingSystemCommand.ExportGraphic
        End Function
    End Class
End Class
