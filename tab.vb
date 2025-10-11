Imports CefSharp
Imports CefSharp.WinForms
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions

Public Class tab
    Public Class TSRenderer
        Inherits ToolStripProfessionalRenderer

        Private topLevelNormalBack As Color = Color.FromArgb(40, 40, 45)
        Private topLevelHoverBack As Color = Color.LightBlue
        Private topLevelNormalText As Color = Color.FromArgb(200, 200, 205)
        Private topLevelHoverText As Color = Color.Black

        Private dropDownNormalBack As Color = Color.FromArgb(40, 40, 45)
        Private dropDownHoverBack As Color = Color.LightBlue
        Private dropDownNormalText As Color = Color.FromArgb(200, 200, 205)
        Private dropDownSelectedText As Color = Color.Black

        Private separatorColor As Color = Color.FromArgb(200, 200, 205)

        Protected Overrides Sub OnRenderMenuItemBackground(ByVal e As ToolStripItemRenderEventArgs)
            Dim rect As New Rectangle(Point.Empty, e.Item.Size)
            Dim backColor As Color

            If TypeOf e.ToolStrip Is MenuStrip Then
                Dim tsi As ToolStripMenuItem = TryCast(e.Item, ToolStripMenuItem)
                If tsi IsNot Nothing AndAlso tsi.DropDown.Visible Then
                    backColor = topLevelNormalBack
                Else
                    backColor = If(e.Item.Selected, topLevelHoverBack, topLevelNormalBack)
                End If
            Else
                backColor = If(e.Item.Selected, dropDownHoverBack, dropDownNormalBack)
            End If

            e.Graphics.FillRectangle(New SolidBrush(backColor), rect)
        End Sub


        Protected Overrides Sub OnRenderItemText(ByVal e As ToolStripItemTextRenderEventArgs)
            If TypeOf e.ToolStrip Is MenuStrip Then
                e.TextColor = If(e.Item.Selected, topLevelHoverText, topLevelNormalText)
            Else
                e.TextColor = If(e.Item.Selected, dropDownSelectedText, dropDownNormalText)
            End If
            MyBase.OnRenderItemText(e)
        End Sub

        Protected Overrides Sub OnRenderToolStripBackground(ByVal e As ToolStripRenderEventArgs)
            If TypeOf e.ToolStrip Is ToolStripDropDownMenu Then
                Dim rect As New Rectangle(Point.Empty, e.ToolStrip.Size)
                e.Graphics.FillRectangle(New SolidBrush(dropDownNormalBack), rect)
            Else
                MyBase.OnRenderToolStripBackground(e)
            End If
        End Sub

        Protected Overrides Sub OnRenderImageMargin(ByVal e As ToolStripRenderEventArgs)
            Dim rect As New Rectangle(Point.Empty, e.ToolStrip.Size)
            e.Graphics.FillRectangle(New SolidBrush(dropDownNormalBack), rect)
        End Sub

        Protected Overrides Sub OnRenderSeparator(ByVal e As ToolStripSeparatorRenderEventArgs)
            Dim g As Graphics = e.Graphics
            g.FillRectangle(New SolidBrush(dropDownNormalBack), e.Item.Bounds)

            Dim rect As Rectangle = e.Item.ContentRectangle
            Using pen As New Pen(separatorColor)
                If rect.Width > rect.Height Then
                    Dim midY As Integer = rect.Top + rect.Height \ 2
                    g.DrawLine(pen, rect.Left + 10, midY, rect.Right - 10, midY)
                Else
                    Dim midX As Integer = rect.Left + rect.Width \ 2
                    g.DrawLine(pen, midX, rect.Top + 5, midX, rect.Bottom - 5)
                End If
            End Using
        End Sub
    End Class

    Dim patt = "^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$"

    Private Sub CheckLink()
        If ToolStripTextBox1.Text = "" Then
            MsgBox("Veuillez mettre une URL")
        ElseIf Regex.IsMatch(ToolStripTextBox1.Text, patt) OrElse ToolStripTextBox1.Text.StartsWith("chrome://") Then
            ChromiumWebBrowser1.Load(ToolStripTextBox1.Text)
        Else
            ChromiumWebBrowser1.Load("https://www.google.com/search?q=" & ToolStripTextBox1.Text)
        End If
        ToolStripTextBox1.Clear()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        CheckLink()
    End Sub

    Private Sub tab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChromiumWebBrowser1.Dock = DockStyle.Fill
        ChromiumWebBrowser1.JsDialogHandler = New CustomJsDialogHandler
        ChromiumWebBrowser1.LifeSpanHandler = New CustomLifeSpanHandler(AddressOf AjouterNouvelOnglet)
        ChromiumWebBrowser1.Load(TargetUrl)
        ToolStrip1.BackColor = Color.FromArgb(40, 40, 45)
        ToolStrip1.ForeColor = Color.FromArgb(200, 200, 205)
        ToolStrip1.Renderer = New TSRenderer()
        Me.BackColor = Color.FromArgb(40, 40, 45)
        Me.ForeColor = Color.FromArgb(200, 200, 205)
        ChromiumWebBrowser1.BackColor = Color.FromArgb(40, 40, 45)
        ChromiumWebBrowser1.ForeColor = Color.FromArgb(200, 200, 205)
        ToolStripTextBox1.BackColor = Color.FromArgb(51, 51, 57)
        ToolStripTextBox1.ForeColor = Color.FromArgb(200, 200, 205)
    End Sub

    Public title
    Public tabIdx = Me.Tag

    Private Sub ChromiumWebBrowser1_TitleChanged(sender As Object, e As TitleChangedEventArgs) Handles ChromiumWebBrowser1.TitleChanged
        title = e.Title
    End Sub

    Private Sub RevenirEnArrièreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevenirEnArrièreToolStripMenuItem.Click
        If ChromiumWebBrowser1.CanGoBack Then
            ChromiumWebBrowser1.Back
        End If
    End Sub

    Private Sub RevenirEnAvantToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevenirEnAvantToolStripMenuItem.Click
        If ChromiumWebBrowser1.CanGoForward Then
            ChromiumWebBrowser1.Forward
        End If
    End Sub

    Dim currtUrl = TargetUrl

    Private Sub RaffraîchirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RafraîchirToolStripMenuItem.Click
        ChromiumWebBrowser1.Load(currtUrl)
    End Sub

    Private Sub ToolStripTextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles ToolStripTextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            CheckLink()
        End If
    End Sub

    Private Sub NouvellePageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NouvellePageToolStripMenuItem.Click
        Dim tabcontroll As TabControl = ObtenirTabControl()
        Dim tab1 As New tab
        With tab1
            .Dock = DockStyle.Fill
            .Tag = tabcontroll.TabPages.Count
            .ParentTabControl = tabcontroll
            .ParentTabContainer = ParentTabContainer
            .TargetUrl = TargetUrl
        End With
        Dim newTab As New TabPage("Nouvel onglet")
        newTab.Controls.Add(tab1)
        ParentTabContainer.Add(newTab, tab1)
        tabcontroll.TabPages.Add(newTab)
        tabcontroll.SelectedTab = newTab
    End Sub

    Private Sub FermerLaPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FermerLaPageToolStripMenuItem.Click
        If ParentTabControl IsNot Nothing Then
            Dim currentTab As TabPage = Nothing
            For Each tabPage As KeyValuePair(Of TabPage, Control) In ParentTabContainer
                If tabPage.Value Is Me Then
                    currentTab = tabPage.Key
                    Exit For
                End If
            Next
            If currentTab Is Nothing Then Exit Sub

            If ParentTabControl.TabPages.Count = 1 Then
                Dim result As DialogResult = MessageBox.Show(
                    "Vous allez fermer le dernier onglet. Voulez-vous fermer Elektraostog Solaryl ?",
                    "Dernier onglet",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning)

                If result = DialogResult.Yes Then
                    Application.Exit()
                ElseIf result = DialogResult.No Then
                    AjouterNouvelOnglet()
                    ParentTabControl.TabPages.Remove(currentTab)
                    ParentTabContainer.Remove(currentTab)
                End If
            Else
                ParentTabControl.TabPages.Remove(currentTab)
                ParentTabContainer.Remove(currentTab)
            End If

        End If
    End Sub

    Public Delegate Sub AjouterNouvelOngletDelegate(targetUrl As String)
    Private Sub AjouterNouvelOnglet(Optional targetUrl As String = "https://google.com")
        If ParentTabControl.InvokeRequired Then
            ParentTabControl.Invoke(New Action(Of String)(AddressOf AjouterNouvelOnglet), targetUrl)
            Return
        End If
        Dim tab1 As New tab
        With tab1
            .Dock = DockStyle.Fill
            .Tag = ParentTabControl.TabPages.Count
            .ParentTabControl = ParentTabControl
            .ParentTabContainer = ParentTabContainer
            .TargetUrl = targetUrl
        End With
        Dim newTab As New TabPage("Nouvel onglet")
        newTab.Controls.Add(tab1)
        ParentTabContainer.Add(newTab, tab1)
        ParentTabControl.TabPages.Add(newTab)
        ParentTabControl.SelectedTab = newTab
    End Sub

    Public Property ParentTabControl As TabControl
    Public Property ParentTabContainer As Dictionary(Of TabPage, Control)
    Public Property TargetUrl As String

    Private Function ObtenirTabControl() As TabControl
        Dim form As Form = Me.ParentForm
        If form IsNot Nothing Then
            For Each pnl As Control In form.Controls
                If TypeOf pnl Is Panel Then
                    For Each ctrl As Control In pnl.Controls
                        If TypeOf ctrl Is TabControl Then
                            Return ctrl
                        End If
                    Next
                End If
            Next
        End If
        Return Nothing
    End Function

    Dim dropped As Boolean = False

    Private Sub PageTSSB_ButtonClick(sender As Object, e As EventArgs) Handles PageTSSB.ButtonClick
        If dropped = True Then
            PageTSSB.HideDropDown()
            dropped = False
        Else
            PageTSSB.ShowDropDown()
            dropped = True
        End If
    End Sub

    Private Sub ChromiumWebBrowser1_AddressChanged(sender As Object, e As AddressChangedEventArgs) Handles ChromiumWebBrowser1.AddressChanged
        currtUrl = e.Address
        ToolStripTextBox1.GetCurrentParent().Invoke(Sub()
                                                        ToolStripTextBox1.Text = currtUrl
                                                    End Sub)
    End Sub

    Private Sub ChromiumWebBrowser1_LoadError(sender As Object, e As LoadErrorEventArgs) Handles ChromiumWebBrowser1.LoadError
        If Not e.ErrorText = "ERR_ABORTED" Then
            MessageBox.Show("Une erreur est survenue lors du chargement de la page : " & e.ErrorText, "Erreur de chargement", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ChromiumWebBrowser1_JavascriptMessageReceived(sender As Object, e As JavascriptMessageReceivedEventArgs) Handles ChromiumWebBrowser1.JavascriptMessageReceived
        MessageBox.Show(e.ToString)
    End Sub

    Public Class CustomJsDialogHandler
        Implements IJsDialogHandler

        Public Function OnJSDialog(chromiumWebBrowser As IWebBrowser, browser As IBrowser, originUrl As String, dialogType As CefJsDialogType, messageText As String, defaultPromptText As String, callback As IJsDialogCallback, ByRef suppressMessage As Boolean) As Boolean Implements IJsDialogHandler.OnJSDialog
            Dim result As DialogResult = DialogResult.None
            Dim winBrowser = TryCast(chromiumWebBrowser, Control)
            If winBrowser IsNot Nothing Then
                winBrowser.Invoke(Sub()
                                      If dialogType = CefJsDialogType.Alert Then
                                          MessageBox.Show(messageText, "Alerte JS", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                          result = DialogResult.OK
                                      ElseIf dialogType = CefJsDialogType.Confirm Then
                                          result = MessageBox.Show(messageText, "Confirmation JS", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                                      Else
                                          result = MessageBox.Show(messageText, "Prompt JS", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                                      End If
                                  End Sub)
            Else
                result = MessageBox.Show(messageText, "JS Dialog", MessageBoxButtons.OKCancel)
            End If

            If result = DialogResult.OK Then
                callback.Continue(True)
            Else
                callback.Continue(False)
            End If

            Return True
        End Function

        Public Function OnBeforeUnloadDialog(chromiumWebBrowser As IWebBrowser, browser As IBrowser, messageText As String, isReload As Boolean, callback As IJsDialogCallback) As Boolean Implements IJsDialogHandler.OnBeforeUnloadDialog
            Dim result As DialogResult = DialogResult.None
            Dim winBrowser = TryCast(chromiumWebBrowser, Control)
            If winBrowser IsNot Nothing Then
                winBrowser.Invoke(Sub()
                                      result = MessageBox.Show(messageText, "Before Unload", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                                  End Sub)
            Else
                result = MessageBox.Show(messageText, "Before Unload", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            End If

            If result = DialogResult.OK Then
                callback.Continue(True)
            Else
                callback.Continue(False)
            End If

            Return True
        End Function

        Public Sub OnDialogClosed(chromiumWebBrowser As IWebBrowser, browser As IBrowser) Implements IJsDialogHandler.OnDialogClosed

        End Sub

        Public Sub OnResetDialogState(chromiumWebBrowser As IWebBrowser, browser As IBrowser) Implements IJsDialogHandler.OnResetDialogState

        End Sub
    End Class

    Public Class CustomLifeSpanHandler
        Implements ILifeSpanHandler

        Private ReadOnly _ajouterNouvelOnglet As AjouterNouvelOngletDelegate

        Public Sub New(ajouterNouvelOngletFunc As AjouterNouvelOngletDelegate)
            _ajouterNouvelOnglet = ajouterNouvelOngletFunc
        End Sub

        Public Function OnBeforePopup(chromiumWebBrowser As IWebBrowser, browser As IBrowser, frame As IFrame, targetUrl As String, targetFrameName As String, targetDisposition As WindowOpenDisposition, userGesture As Boolean, popupFeatures As IPopupFeatures, windowInfo As IWindowInfo, browserSettings As IBrowserSettings, ByRef noJavascriptAccess As Boolean, ByRef newBrowser As IWebBrowser) As Boolean Implements ILifeSpanHandler.OnBeforePopup
            _ajouterNouvelOnglet.Invoke(targetUrl)
            Return True
        End Function

        Public Sub OnAfterCreated(browserControl As IWebBrowser, browser As IBrowser) Implements ILifeSpanHandler.OnAfterCreated
        End Sub

        Public Function DoClose(browserControl As IWebBrowser, browser As IBrowser) As Boolean Implements ILifeSpanHandler.DoClose
            Return False
        End Function

        Public Sub OnBeforeClose(browserControl As IWebBrowser, browser As IBrowser) Implements ILifeSpanHandler.OnBeforeClose
        End Sub
    End Class

    Private Sub ParamètresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParamètresToolStripMenuItem.Click

    End Sub

    Dim verr As String = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
    Private Sub MettreÀJourToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MettreÀJourToolStripMenuItem.Click
        Dim web As HttpWebRequest = HttpWebRequest.Create("https://raw.githubusercontent.com/Timoh5709/Elektraostog-Solaryl/refs/heads/main/ver")
        web.Timeout = 10000
        web.Method = "GET"
        Dim reponse As HttpWebResponse = web.GetResponse()
        Dim reader As New StreamReader(reponse.GetResponseStream())
        Dim page As String = reader.ReadToEnd
        Dim onlinever As String = page.Substring(0, 7)
        Dim link As String = ""
        If Not onlinever = verr Then
            link = "https://raw.githubusercontent.com/Timoh5709/Elektraostog-Solaryl/refs/heads/main/app.zip"
        End If
        If Not onlinever = "" Then
            If Not link = "" Then
                MsgBox("Mise à jour requise")
                Process.Start("chrome", link)
            Else
                MsgBox("Vous êtes à jour")
            End If
        Else
            MsgBox("Erreur Internet !")
        End If
    End Sub

    Private Sub ToolStrip1_Resize(sender As Object, e As EventArgs) Handles ToolStrip1.Resize
        ToolStripTextBox1.Size = New Point(ToolStrip1.Width - 182, ToolStripTextBox1.Height)
    End Sub

    Private Sub ServeurDiscordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ServeurDiscordToolStripMenuItem.Click
        ChromiumWebBrowser1.Load("https://discord.com/channels/1257704920526618766/1257704921650954303")
    End Sub

    Private Sub SiteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SiteToolStripMenuItem.Click
        ChromiumWebBrowser1.Load("https://principaute-de-solarys.gitbook.io/site")
    End Sub
End Class
