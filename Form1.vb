Imports System.IO
Imports System.Text
Imports CefSharp
Imports CefSharp.Callback
Imports CefSharp.WinForms

Public Class ElektraostogSolaryl
    Private Sub ElektraostogSolaryl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim settings As New CefSettings
        settings.RemoteDebuggingPort = 8080
        settings.Locale = "fr-FR"
        settings.EnablePrintPreview()
        settings.AcceptLanguageList = "fr-FR,fr,en-US,en"
        Dim osInfo As String = GetOSInfo()
        Dim appVersion As String = Application.ProductVersion
        settings.UserAgent = $"Elektraostog Solaryl/{appVersion} Mozilla/5.0 ({osInfo}) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{Cef.ChromiumVersion} Safari/537.36"
        settings.WindowlessRenderingEnabled = 0
        settings.RegisterScheme(New CefCustomScheme() With {
            .SchemeName = "solarys",
            .SchemeHandlerFactory = New SolarysSchemeHandlerFactory,
            .IsSecure = True,
            .IsStandard = True,
            .IsLocal = True,
            .IsDisplayIsolated = False
        })
        Cef.Initialize(settings)
        AjouterNouvelOnglet()

        For Each tp As TabPage In TabControl1.TabPages
            If tp.Controls.Count > 0 Then
                TabContentMap(tp) = tp.Controls(0)
            End If
        Next

        For Each kvp As KeyValuePair(Of TabPage, Control) In TabContentMap
            kvp.Key.Controls.Remove(kvp.Value)
        Next

        If TabControl1.SelectedTab IsNot Nothing AndAlso TabContentMap.ContainsKey(TabControl1.SelectedTab) Then
            Panel2.Controls.Clear()
            Dim content As Control = TabContentMap(TabControl1.SelectedTab)
            Panel2.Controls.Add(content)
            content.Dock = DockStyle.Fill
        End If
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab IsNot Nothing AndAlso TabContentMap.ContainsKey(TabControl1.SelectedTab) Then
            Panel2.Controls.Clear()
            Dim content As Control = TabContentMap(TabControl1.SelectedTab)
            Panel2.Controls.Add(content)
            content.Dock = DockStyle.Fill
        End If
    End Sub

    Private Sub AjouterNouvelOnglet(Optional targetUrl As String = "Défini pas ici avec My.Settings.homepage")
        If targetUrl = "Défini pas ici avec My.Settings.homepage" Then
            targetUrl = My.Settings.homepage
        End If
        Dim tab1 As New tab
        With tab1
            .Dock = DockStyle.Fill
            .Tag = TabControl1.TabPages.Count
            .ParentTabControl = TabControl1
            .ParentTabContainer = TabContentMap
            .TargetUrl = targetUrl
        End With
        Dim newTab As New TabPage("Nouvel onglet")
        newTab.Controls.Add(tab1)
        TabControl1.TabPages.Add(newTab)
        TabControl1.SelectedTab = newTab
    End Sub

    Private Sub UpdateClock_Tick(sender As Object, e As EventArgs) Handles UpdateClock.Tick
        For Each pagee As KeyValuePair(Of TabPage, Control) In TabContentMap
            If TypeOf pagee.Value Is tab Then
                Dim pageee As tab = pagee.Value
                If Not pageee.title = pagee.Key.Text AndAlso Not pageee.title = "" Then
                    pagee.Key.Text = pageee.title
                End If
            End If
        Next

        If fullScreen = False Then
            Dim COURSOR As Point = Me.PointToClient(Cursor.Position)
            Dim resizeArea As Integer = 10

            If COURSOR.X < resizeArea Then
                If COURSOR.Y < resizeArea Then
                    Panel1.Enabled = False
                ElseIf COURSOR.Y > Me.ClientSize.Height - resizeArea Then
                    Panel2.Enabled = False
                Else
                    Panel1.Enabled = False
                    Panel2.Enabled = False
                End If
            ElseIf COURSOR.X > Me.ClientSize.Width - resizeArea Then
                If COURSOR.Y < resizeArea Then
                    Panel1.Enabled = False
                ElseIf COURSOR.Y > Me.ClientSize.Height - resizeArea Then
                    Panel2.Enabled = False
                Else
                    Panel1.Enabled = False
                    Panel2.Enabled = False
                End If
            ElseIf COURSOR.Y < resizeArea Then
                Panel1.Enabled = False
            ElseIf COURSOR.Y > Me.ClientSize.Height - resizeArea Then
                Panel2.Enabled = False
            ElseIf Panel1.Enabled = False Then
                Panel1.Enabled = True
            ElseIf Panel2.Enabled = False Then
                Panel2.Enabled = True
            End If
        End If
    End Sub

    Private Sub TabControl1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles TabControl1.DrawItem
        Dim g As Graphics = e.Graphics
        Dim tabPage As TabPage = TabControl1.TabPages(e.Index)
        Dim tabRect As Rectangle = TabControl1.GetTabRect(e.Index)
        Dim rightLimit As Integer = TabControl1.Width
        Dim headerRect As New Rectangle(tabRect.Right, 0, TabControl1.Width, TabControl1.ItemSize.Height + 1)


        If tabRect.Right > rightLimit Then
            tabRect.Width = rightLimit - tabRect.Left
        End If

        g.FillRectangle(New SolidBrush(If(e.State.HasFlag(DrawItemState.Selected), Color.LightBlue, Color.FromArgb(40, 40, 45))), tabRect)

        Using pen As New Pen(Color.FromArgb(40, 40, 45))
            g.DrawLine(pen, tabRect.Left, tabRect.Top, tabRect.Right, tabRect.Top)
        End Using

        Dim sf As New StringFormat() With {
        .Alignment = StringAlignment.Center,
        .LineAlignment = StringAlignment.Center
    }
        Dim textColor As Color = If(e.State.HasFlag(DrawItemState.Selected), Color.Black, Color.FromArgb(200, 200, 205))
        g.DrawString(tabPage.Text, TabControl1.Font, New SolidBrush(textColor), tabRect, sf)
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.BackColor = Color.FromArgb(206, 86, 86)
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.BackColor = Color.FromArgb(40, 40, 45)
    End Sub

    Private Sub PictureBox2_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox2.MouseEnter
        PictureBox2.BackColor = Color.DimGray
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.BackColor = Color.FromArgb(40, 40, 45)
    End Sub

    Private Sub PictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox3.MouseEnter
        PictureBox3.BackColor = Color.DimGray
    End Sub

    Private Sub PictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox3.MouseLeave
        PictureBox3.BackColor = Color.FromArgb(40, 40, 45)
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Dim normalSize As Point
    Dim normalPos As Point
    Dim fullScreen As Boolean

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If Not fullScreen Then
            normalSize = Me.Size
            normalPos = Me.Location
            Me.Size = New Point(My.Computer.Screen.WorkingArea.Width, My.Computer.Screen.WorkingArea.Height)
            Me.Location = New Point(0, 0)
            fullScreen = True
            PictureBox2.Image = My.Resources.Min
        Else
            Me.Size = normalSize
            Me.Location = normalPos
            fullScreen = False
            PictureBox2.Image = My.Resources.Max
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Dim isDragging As Boolean = False
    Dim startPoint As Point

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        If Not fullScreen AndAlso e.Button = MouseButtons.Left Then
            If e.Y < 30 AndAlso e.X < Panel1.Width - 90 Then
                isDragging = True
                startPoint = New Point(e.X, e.Y)
            End If
        End If
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        isDragging = False
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If isDragging Then
            Dim p As Point = PointToScreen(e.Location)
            Location = New Point(p.X - startPoint.X, p.Y - startPoint.Y)
        End If
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_NCHITTEST As Integer = &H84
        Const HTCLIENT As Integer = 1
        Const HTLEFT As Integer = 10
        Const HTRIGHT As Integer = 11
        Const HTTOP As Integer = 12
        Const HTTOPLEFT As Integer = 13
        Const HTTOPRIGHT As Integer = 14
        Const HTBOTTOM As Integer = 15
        Const HTBOTTOMLEFT As Integer = 16
        Const HTBOTTOMRIGHT As Integer = 17

        If m.Msg = WM_NCHITTEST Then
            MyBase.WndProc(m)
            Dim COURSOR As Point = Me.PointToClient(Cursor.Position)
            Dim resizeArea As Integer = 10

            If COURSOR.X < resizeArea Then
                If COURSOR.Y < resizeArea Then
                    m.Result = CType(HTTOPLEFT, IntPtr)
                    Return
                ElseIf COURSOR.Y > Me.ClientSize.Height - resizeArea Then
                    m.Result = CType(HTBOTTOMLEFT, IntPtr)
                    Return
                Else
                    m.Result = CType(HTLEFT, IntPtr)
                    Return
                End If
            ElseIf COURSOR.X > Me.ClientSize.Width - resizeArea Then
                If COURSOR.Y < resizeArea Then
                    m.Result = CType(HTTOPRIGHT, IntPtr)
                    Return
                ElseIf COURSOR.Y > Me.ClientSize.Height - resizeArea Then
                    m.Result = CType(HTBOTTOMRIGHT, IntPtr)
                    Return
                Else
                    m.Result = CType(HTRIGHT, IntPtr)
                    Return
                End If
            ElseIf COURSOR.Y < resizeArea Then
                m.Result = CType(HTTOP, IntPtr)
                Return
            ElseIf COURSOR.Y > Me.ClientSize.Height - resizeArea Then
                m.Result = CType(HTBOTTOM, IntPtr)
                Return
            End If

            m.Result = CType(HTCLIENT, IntPtr)
            Return
        End If

        MyBase.WndProc(m)
    End Sub
End Class
