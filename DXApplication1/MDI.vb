Imports System.ComponentModel
Imports System.Text
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraBars.Helpers
Imports DevExpress.Skins
Imports DevExpress.LookAndFeel
Imports DevExpress.UserSkins
Imports DevExpress.XtraNavBar

Imports System.Deployment.Application
Public Class MDI
    Sub New()
        InitSkins()
        InitializeComponent()
        Me.InitSkinGallery()
        'Me.InitGrid()
        navBarControl.ExplorerBarShowGroupButtons = False
        ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False
        keskreGroup.Expanded = True

    End Sub
    Sub InitSkins()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        DevExpress.UserSkins.BonusSkins.Register()
        'UserLookAndFeel.Default.SetSkinStyle("DevExpress Style")
        UserLookAndFeel.Default.SkinName = My.MySettings.Default("skin").ToString()
    End Sub
    Private Sub InitSkinGallery()
        SkinHelper.InitSkinGallery(rgbiSkins, True)
    End Sub
    Dim gridDataList As New BindingList(Of Person)
    Private Sub InitGrid()
        gridDataList.Add(New Person("John", "Smith"))
        gridDataList.Add(New Person("Gabriel", "Smith"))
        gridDataList.Add(New Person("Ashley", "Smith", "some comment"))
        gridDataList.Add(New Person("Adrian", "Smith", "some comment"))
        gridDataList.Add(New Person("Gabriella", "Smith", "some comment"))
        'Me.gridControl.DataSource = gridDataList
    End Sub

    Private Sub inboxItem_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavBarItem0.LinkClicked
        loadDocr(f_transkrip2kolom, PanelControl1)

        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub NavBarItem1_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavBarItem1.LinkClicked
        loadDocr(f_ijazah, PanelControl1)
        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub NavBarItem2_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavBarItem2.LinkClicked
        loadDocr(f_konfirmasiregs, PanelControl1)
        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub NavBarItem3_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavBarItem3.LinkClicked
        loadDocr(f_sklulussementara, PanelControl1)
        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub NavBarItem4_LinkClicked(sender As Object, e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles NavBarItem4.LinkClicked
        loadDocr(f_skmahasiswaaktif, PanelControl1)
        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub MDI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'If WindowsUIView1.ShowFlyoutDialog(Flyout1) <> System.Windows.Forms.DialogResult.Yes Then
        '    e.Cancel = True
        'Else
        My.MySettings.Default("skin") = UserLookAndFeel.Default.SkinName 'create in setting "applicationskinname"
        My.MySettings.Default.Save()

        'End
        'End If
    End Sub

    Private Sub MDI_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Dim I As Integer
        'For I = 0 To navBarControl.Items.Count - 1
        '    Dim Item As NavBarItem = navBarControl.Items(I)
        '    ' perform some operations on an item here
        '    '..
        '    Item.SmallImage = Nothing
        'Next
        mdilod()
        NavBarGroup2.Visible = False
    End Sub

    Private Sub navBarControl_MouseDown(sender As Object, e As MouseEventArgs) Handles navBarControl.MouseDown
        If e.Button = MouseButtons.Left Then
            Dim navBar As NavBarControl = TryCast(sender, NavBarControl)
            Dim hitInfo As NavBarHitInfo = navBar.CalcHitInfo(New Point(e.X, e.Y))
            If hitInfo.InGroupCaption AndAlso (Not hitInfo.InGroupButton) Then
                hitInfo.Group.Expanded = Not hitInfo.Group.Expanded

                If hitInfo.Group.Expanded = True Then
                    hitInfo.Group.SmallImage = My.Resources.buka_16x16
                Else
                    hitInfo.Group.SmallImage = My.Resources.tutup_16x16

                End If
            End If
        End If
    End Sub

 
    Private Sub NavBarItem5_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem5.LinkClicked
        loadDocr(f_mhsresign, PanelControl1)
        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Dim myNum As String

        Try

            myNum = InputBox("Password")

        Catch

            'MsgBox("Password tidak terdaftar, silahkan hubungi kepala Bagian anda")
            Exit Sub
        End Try


        If myNum = "" Then
            Exit Sub
        ElseIf myNum <> "admin12" Then



            Exit Sub

        End If
        f_reportdesigner.Show()
    End Sub

    Private Sub NavBarItem6_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem6.LinkClicked
        loadDocr(f_cuti1, PanelControl1)
        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub iAbout_ItemClick(sender As Object, e As ItemClickEventArgs) Handles iAbout.ItemClick
        'f_about.Parent = Me
        f_about.StartPosition = FormStartPosition.CenterScreen
        f_about.ShowDialog()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        stat1.Caption = CInt(Now.ToString("dd", indo)) & "  " & Now.ToString("MMMM", indo) & "  " & Now.ToString("yyyy", indo)
        stat2.Caption = Now.ToString("HH", indo) & ":" & Now.ToString("mm", indo) & ":" & Now.ToString("ss", indo)
        'If IO.File.Exists("\\192.168.10.238\software\Software ALL ICT\Update (Hery)\av.txt") = True Then
        '    NavBarItem14.Visible = True

        'End If
    End Sub



    Private Sub NavBarItem7_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem7.LinkClicked
        loadDocr(f_cuti2, PanelControl1)
        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub NavBarItem8_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem8.LinkClicked
        loadDocr(f_transkripkumulatif, PanelControl1)

        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub NavBarItem9_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem9.LinkClicked
        'MsgBox("Under Construction")
        'Exit Sub
        loadDocr(f_laphstudi, PanelControl1)

        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub iHelp_ItemClick(sender As Object, e As ItemClickEventArgs) Handles iHelp.ItemClick
        MsgBox("Under Construction")
    End Sub

    Private Sub NavBarItem12_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem12.LinkClicked
        Application.Exit()
    End Sub

    Private Sub NavBarItem11_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem11.LinkClicked
        Dim f, g, h As Form
        f = Me
        f.Hide()

        h = f_datamhs
        h.Close()

        g = Login
        g.Show()
    End Sub

    Private Sub MDI_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub NavBarItem10_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem10.LinkClicked
        Dim f As Form = Passw
        'f.Parent = Me
        f.ShowDialog()

    End Sub

    Private Sub NavBarItem13_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem13.LinkClicked
        If IO.Directory.Exists(Application.StartupPath + "") = True Then
            My.Computer.FileSystem.CopyDirectory(Application.StartupPath + "", "rpx", True)

        End If
        MsgBox("Semua files sudah terupdate")
    End Sub

    Private Sub NavBarItem14_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem14.LinkClicked
        Try
            'MsgBox(System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString)
            MsgBox(Application.ProductVersion.ToString)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub NavBarItem15_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem15.LinkClicked
        loadDocr(f_datamhs, PanelControl1)

        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub NavBarItem16_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem16.LinkClicked
        loadDocr(f_va, PanelControl1)

        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub

    Private Sub NavBarItem17_LinkClicked(sender As Object, e As NavBarLinkEventArgs) Handles NavBarItem17.LinkClicked
        loadDocr(f_magang, PanelControl1)

        iconic(navBarControl, CType(sender, NavBarItem))
    End Sub
End Class
