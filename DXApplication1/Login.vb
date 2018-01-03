Public Class Login
    Public Sub New()

        ' This call is required by the designer.
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("id-ID")
        InitializeComponent()
        'MsgBox(Application.StartupPath + " " & Now().ToString("ddMMyy"))
        'If IO.Directory.Exists(Application.StartupPath + "") = True Then
        '    My.Computer.FileSystem.CopyDirectory(Application.StartupPath + "", "rpx", True)

        'End If
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles Me.Load
        'production
        Class1.myConnectionString = "server=172.16.0.12;uid=fingerprint;pwd=fingerprint2016;database=iap_db;persist security info=true;CharSet=utf8;convert zero datetime=True;"

        'MsgBox(Environment.MachineName.ToString)
        'dev
        'Class1.myConnectionString = "server=172.16.19.14;port=3306;user id=iap_user; password=user123; database=iap_db;persist security info=true;CharSet=utf8;convert zero datetime=True;"
    End Sub

    Private Sub WindowsUIButtonPanel1_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles WindowsUIButtonPanel1.ButtonClick

        Select Case e.Button.Properties.Tag

            Case 1
                If (Class1.MysqlQuery("select fullname from patr0002 where idnik = '" & TextEdit1.EditValue & "' and password = md5('" & TextEdit2.EditValue & "')")) <> "" Then
                    Me.Hide()
                    yser = Class1.MysqlQuery("select fullname from patr0002 where idnik = '" & TextEdit1.EditValue & "' and password = md5('" & TextEdit2.EditValue & "')")
                    nik = TextEdit1.EditValue
                    Dim f As Form = MDI
                    f.Show()

                    Class1.iduser = TextEdit1.EditValue
                    Class1.idpcname = Environment.MachineName.ToString
                    TextEdit1.EditValue = ""
                    TextEdit2.EditValue = ""
                    mdilod()

                Else
                    MsgBox("Password tidak Cocok")
                    TextEdit1.Focus()
                    Exit Sub
                End If



            Case 2
                Application.Exit()


        End Select
    End Sub

    Private Sub TextEdit2_KeyUp(sender As Object, e As KeyEventArgs) Handles TextEdit2.KeyUp
        If e.KeyCode = Keys.Enter Then
            If (Class1.MysqlQuery("select fullname from patr0002 where idnik = '" & TextEdit1.EditValue & "' and password = md5('" & TextEdit2.EditValue & "')")) <> "" Then
                Me.Hide()
                yser = Class1.MysqlQuery("select fullname from patr0002 where idnik = '" & TextEdit1.EditValue & "' and password = md5('" & TextEdit2.EditValue & "')")
                nik = TextEdit1.EditValue
                Dim f As Form = MDI
                f.Show()

                Class1.iduser = TextEdit1.EditValue
                Class1.idpcname = Environment.MachineName.ToString
                TextEdit1.EditValue = ""
                TextEdit2.EditValue = ""
                mdilod()


            Else
                MsgBox("Password tidak Cocok")
                TextEdit1.Focus()
                Exit Sub

            End If
        End If
    End Sub

    Private Sub LabelControl1_Click(sender As Object, e As EventArgs) Handles LabelControl1.Click

    End Sub
End Class