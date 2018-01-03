Imports DevExpress.Data
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class f_datamhs
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        AddHandler GridView1.CustomDrawRowIndicator, AddressOf Class1.GridNumbering
        'AddHandler GridView1.PopupMenuShowing, AddressOf Class1.GridRightClick
        ' Add any initialization after the InitializeComponent() call.
        'Class1.LoadGrid(GridControl1, "sp_datamhs('2016')", True, True, True)
        ComboBoxEdit1.Properties.Items.Clear()

        For i = 0 To (Year(Now()) + 2) - 1987
            ComboBoxEdit1.Properties.Items.Add(1987 + i)
        Next

        ComboBoxEdit1.EditValue = Year(Now())

        'ComboBoxEdit1.SelectedIndex = ComboBoxEdit1.Properties.Items.Count - 1
    End Sub

    Private Sub f_datamhs_Load(sender As Object, e As EventArgs) Handles Me.Load


    End Sub

    Private Sub GridView1_CustomSummaryCalculate(sender As Object, e As CustomSummaryEventArgs) Handles GridView1.CustomSummaryCalculate
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize AndAlso (CType(e.Item, GridGroupSummaryItem)).Tag.Equals(1) Then
            e.TotalValue = (TryCast(sender, GridView)).GetGroupRowValue(e.RowHandle)
        End If
    End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged
        'GridControl1.DataSource = Nothing
        'Class1.LoadGrid(GridControl1, "sp_datamhs('" & ComboBoxEdit1.EditValue & "')", True, True)
    End Sub



    Private Sub GridView1_EndGrouping(sender As Object, e As EventArgs) Handles GridView1.EndGrouping
        Dim colm As String
        If (GridView1.IsGroupRow(GridView1.FocusedRowHandle) = True) Then
            colm = GridView1.GroupedColumns(GridView1.GetRowLevel(GridView1.FocusedRowHandle)).FieldName
            Class1.GridSummary(GridView1, colm, DevExpress.Data.SummaryItemType.Sum, "Total:   {0:n2}")
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub SimpleButton1_Click_1(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        GridControl1.DataSource = Nothing
        Class1.LoadGrid(GridControl1, "sp_datamhs('" & ComboBoxEdit1.EditValue & "')", True, True, True)
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress

        Exit Sub

    End Sub
End Class