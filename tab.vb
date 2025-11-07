Imports CefSharp
Imports CefSharp.WinForms
Imports CefSharp.WinForms.Host
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions

Public Class tab
    Dim patt = "^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$"

    Public Class SolarysContextMenuHandler
        Implements IContextMenuHandler

        Private ReadOnly _showDevTools As ShowDevToolsDelegate
        Private ReadOnly _ajouterNouvelOnglet As AjouterNouvelOngletDelegate

        Public Sub New(ajouterNouvelOngletFunc As AjouterNouvelOngletDelegate, showDevToolsFunc As ShowDevToolsDelegate)
            _ajouterNouvelOnglet = ajouterNouvelOngletFunc
            _showDevTools = showDevToolsFunc
        End Sub

        Private Const IDM_OPEN_FILE_IN_NEW_TAB As Integer = 26501
        Private Const IDM_SAVE_FILE As Integer = 26502
        Private Const IDM_COPY_FILE As Integer = 26503
        Private Const IDM_COPY_FILE_ADDRESS As Integer = 26504
        Private Const IDM_OPEN_DEVTOOLS As Integer = 26505

        Public Sub OnBeforeContextMenu(browserControl As IWebBrowser, browser As IBrowser, frame As IFrame, contextMenuParams As IContextMenuParams, model As IMenuModel) Implements IContextMenuHandler.OnBeforeContextMenu
            model.Clear()
            model.AddItem(CefMenuCommand.Back, "Revenir en arrière")
            model.AddItem(CefMenuCommand.Forward, "Revenir en avant")
            model.AddItem(CefMenuCommand.Reload, "Rafraîchir")
            model.AddSeparator()
            model.AddItem(CefMenuCommand.Print, "Imprimer...")
            model.AddItem(CefMenuCommand.ViewSource, "Voir le code source")

            Dim parentTab = TryCast(browserControl, Control)
            Dim devToolsOuvert As Boolean = False
            If parentTab IsNot Nothing AndAlso TypeOf parentTab.Parent Is tab Then
                devToolsOuvert = DirectCast(parentTab.Parent, tab).devTools
            End If

            Dim DTstring As String = "Ouvrir les outils de développement"

            If devToolsOuvert Then
                DTstring = "Fermer les outils de développement"
            End If

            model.AddItem(DirectCast(IDM_OPEN_DEVTOOLS, CefMenuCommand), DTstring)

            Dim isDevTools As Boolean = False

            If contextMenuParams IsNot Nothing AndAlso contextMenuParams.SourceUrl IsNot Nothing Then
                If contextMenuParams.SourceUrl.StartsWith("devtools://") Then
                    isDevTools = True
                End If
            End If

            If frame IsNot Nothing AndAlso frame.Url IsNot Nothing Then
                If frame.Url.StartsWith("devtools://") Then
                    isDevTools = True
                End If
            End If

            model.SetEnabled(DirectCast(IDM_OPEN_DEVTOOLS, CefMenuCommand), Not isDevTools)
            If contextMenuParams.MediaType = ContextMenuMediaType.Image Then
                model.AddSeparator()
                model.AddItem(DirectCast(IDM_OPEN_FILE_IN_NEW_TAB, CefMenuCommand), "Ouvrir l'image dans un nouvel onglet")
                model.AddItem(DirectCast(IDM_SAVE_FILE, CefMenuCommand), "Enregistrer l'image sous...")
                model.AddItem(DirectCast(IDM_COPY_FILE, CefMenuCommand), "Copier l'image")
                model.AddItem(DirectCast(IDM_COPY_FILE_ADDRESS, CefMenuCommand), "Copier l'adresse de l'image")
            ElseIf contextMenuParams.MediaType = ContextMenuMediaType.Video Then
                model.AddSeparator()
                model.AddItem(DirectCast(IDM_OPEN_FILE_IN_NEW_TAB, CefMenuCommand), "Ouvrir la vidéo dans un nouvel onglet")
                model.AddItem(DirectCast(IDM_SAVE_FILE, CefMenuCommand), "Enregistrer la vidéo sous...")
                model.AddItem(DirectCast(IDM_COPY_FILE_ADDRESS, CefMenuCommand), "Copier l'adresse de la vidéo")
            ElseIf contextMenuParams.MediaType = ContextMenuMediaType.Audio Then
                model.AddSeparator()
                model.AddItem(DirectCast(IDM_OPEN_FILE_IN_NEW_TAB, CefMenuCommand), "Ouvrir l'audio dans un nouvel onglet")
                model.AddItem(DirectCast(IDM_SAVE_FILE, CefMenuCommand), "Enregistrer l'audio sous...")
                model.AddItem(DirectCast(IDM_COPY_FILE_ADDRESS, CefMenuCommand), "Copier l'adresse de l'audio")
            ElseIf contextMenuParams.MediaType = ContextMenuMediaType.File Then
                model.AddSeparator()
                model.AddItem(DirectCast(IDM_OPEN_FILE_IN_NEW_TAB, CefMenuCommand), "Ouvrir le fichier dans un nouvel onglet")
                model.AddItem(DirectCast(IDM_SAVE_FILE, CefMenuCommand), "Enregistrer le fichier sous...")
                model.AddItem(DirectCast(IDM_COPY_FILE_ADDRESS, CefMenuCommand), "Copier l'adresse du fichier")
            ElseIf contextMenuParams.IsEditable Then
                Dim canPaste As Boolean = False
                Try
                    canPaste = Clipboard.ContainsText()
                Catch
                End Try
                model.AddSeparator()
                model.AddItem(CefMenuCommand.Copy, "Copier")
                model.AddItem(CefMenuCommand.Cut, "Couper")
                model.AddItem(CefMenuCommand.Paste, "Coller")
                model.SetEnabled(CefMenuCommand.Paste, canPaste)
                model.AddItem(CefMenuCommand.SelectAll, "Tout sélectionner")
            End If
        End Sub

        Public Function OnContextMenuCommand(browserControl As IWebBrowser, browser As IBrowser, frame As IFrame, contextMenuParams As IContextMenuParams, commandId As CefMenuCommand, eventFlags As CefEventFlags) As Boolean Implements IContextMenuHandler.OnContextMenuCommand
            Select Case commandId
                Case CefMenuCommand.Back
                    If browser.CanGoBack Then
                        browser.GoBack()
                    End If
                    Return True
                Case CefMenuCommand.Forward
                    If browser.CanGoForward Then
                        browser.GoForward()
                    End If
                    Return True
                Case CefMenuCommand.Reload
                    browser.Reload()
                    Return True
                Case CefMenuCommand.Print
                    browser.Print()
                    Return True
                Case CefMenuCommand.ViewSource
                    frame.ViewSource()
                    Return True
                Case CefMenuCommand.Copy
                    browser.FocusedFrame.Copy()
                    Return True
                Case CefMenuCommand.Cut
                    browser.FocusedFrame.Cut()
                    Return True
                Case CefMenuCommand.Paste
                    browser.FocusedFrame.Paste()
                    Return True
                Case CefMenuCommand.SelectAll
                    browser.FocusedFrame.SelectAll()
                    Return True
                Case DirectCast(IDM_OPEN_DEVTOOLS, CefMenuCommand)
                    Try
                        _showDevTools.Invoke()
                    Catch ex As Exception
                        MessageBox.Show("Impossible d'ouvrir les outils de développement : " & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    Return True
                Case DirectCast(IDM_OPEN_FILE_IN_NEW_TAB, CefMenuCommand)
                    _ajouterNouvelOnglet.Invoke(contextMenuParams.SourceUrl)
                    Return True
                Case DirectCast(IDM_SAVE_FILE, CefMenuCommand)
                    browser.GetHost().StartDownload(contextMenuParams.SourceUrl)
                    Return True
                Case DirectCast(IDM_COPY_FILE, CefMenuCommand)
                    Try
                        Dim web As New WebClient()
                        Dim data As Byte() = web.DownloadData(contextMenuParams.SourceUrl)
                        Using ms As New MemoryStream(data)
                            Using img As Image = Image.FromStream(ms, useEmbeddedColorManagement:=False, validateImageData:=True)
                                Clipboard.SetImage(img)
                            End Using
                        End Using
                    Catch ex As Exception
                        MessageBox.Show("Impossible de copier l'image : " & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    Return True
                Case DirectCast(IDM_COPY_FILE_ADDRESS, CefMenuCommand)
                    Clipboard.SetText(contextMenuParams.SourceUrl)
                    Return True
                Case Else
                    Return False
            End Select
        End Function
        Public Sub OnContextMenuDismissed(browserControl As IWebBrowser, browser As IBrowser, frame As IFrame) Implements IContextMenuHandler.OnContextMenuDismissed
        End Sub
        Public Function RunContextMenu(browserControl As IWebBrowser, browser As IBrowser, frame As IFrame, contextMenuParams As IContextMenuParams, model As IMenuModel, callback As IRunContextMenuCallback) As Boolean Implements IContextMenuHandler.RunContextMenu
            Return False
        End Function
    End Class

    Private Sub CheckLink()
        If ToolStripTextBox1.Text = "" Then
            MsgBox("Veuillez mettre une URL")
        ElseIf Regex.IsMatch(ToolStripTextBox1.Text, patt) OrElse ToolStripTextBox1.Text.StartsWith("chrome://") OrElse ToolStripTextBox1.Text.StartsWith("solarys://") Then
            ChromiumWebBrowser1.Load(ToolStripTextBox1.Text)
        Else
            ChromiumWebBrowser1.Load(My.Settings.searchengine & ToolStripTextBox1.Text)
        End If
        ToolStripTextBox1.Clear()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        CheckLink()
    End Sub

    Private Sub tab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PageTSSB.Image = If(My.Settings.darkmode, My.Resources.page_sombre, My.Resources.page_clair)
        ToolStripButton1.Image = If(My.Settings.darkmode, My.Resources.y_aller_sombre, My.Resources.y_aller_clair)
        ChromiumWebBrowser1.Dock = DockStyle.Fill
        Panel1.Dock = DockStyle.Right
        Panel1.Hide()
        ChromiumWebBrowser1.JsDialogHandler = New SolarysJsDialogHandler
        ChromiumWebBrowser1.LifeSpanHandler = New SolarysLifeSpanHandler(AddressOf AjouterNouvelOnglet)
        ChromiumWebBrowser1.MenuHandler = New SolarysContextMenuHandler(AddressOf AjouterNouvelOnglet, AddressOf ShowDevTools)
        ChromiumWebBrowser1.Load(TargetUrl)
        ToolStrip1.BackColor = _BackColor
        ToolStrip1.ForeColor = TextColor
        ToolStrip1.Renderer = New TSRenderer()
        Me.BackColor = _BackColor
        Me.ForeColor = TextColor
        ChromiumWebBrowser1.BackColor = _BackColor
        ChromiumWebBrowser1.ForeColor = TextColor
        ToolStripTextBox1.BackColor = TextBoxBackColor
        ToolStripTextBox1.ForeColor = TextColor
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
        AjouterNouvelOnglet()
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
    Public Sub AjouterNouvelOnglet(Optional targetUrl As String = "Défini pas ici avec My.Settings.homepage")
        If targetUrl = "Défini pas ici avec My.Settings.homepage" Then
            targetUrl = My.Settings.homepage
        End If
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

    Dim isSRisky As Boolean = False
    Dim isURisky As Boolean = False

    Private Sub ChromiumWebBrowser1_AddressChanged(sender As Object, e As AddressChangedEventArgs) Handles ChromiumWebBrowser1.AddressChanged
        Dim currtUrl As String = e.Address
        If currtUrl.StartsWith("solarys://settings/") And Not isSRisky Then
            ChromiumWebBrowser1.JavascriptObjectRepository.Register("solarysSettings", New ParamManager(), isAsync:=True)
            isSRisky = True
        ElseIf isSRisky Then
            ChromiumWebBrowser1.JavascriptObjectRepository.UnRegister("solarysSettings")
            isSRisky = False
        End If
        If currtUrl.StartsWith("solarys://update/") And Not isURisky Then
            ChromiumWebBrowser1.JavascriptObjectRepository.Register("solarysUpdate", New UpdateManager(), isAsync:=True)
            isURisky = True
        ElseIf isSRisky Then
            ChromiumWebBrowser1.JavascriptObjectRepository.UnRegister("solarysUpdate")
            isURisky = False
        End If
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

    Private Sub ParamètresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParamètresToolStripMenuItem.Click
        ChromiumWebBrowser1.LoadUrl("solarys://settings/")
    End Sub

    Private Sub MettreÀJourToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MettreÀJourToolStripMenuItem.Click
        ChromiumWebBrowser1.LoadUrl("solarys://update/")
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

    Dim devTools As Boolean = False
    Dim ChromiumDevTools1 As ChromiumHostControl
    Private Sub AfficherLesDevToolsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AfficherLesDevToolsToolStripMenuItem.Click
        ShowDevTools()
    End Sub

    Public Delegate Sub ShowDevToolsDelegate()
    Private Sub ShowDevTools()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf ShowDevTools))
            Return
        End If
        If devTools Then
            ChromiumWebBrowser1.CloseDevTools()
            Panel1.Controls.Remove(ChromiumDevTools1)
            ChromiumDevTools1.Dispose()
            ChromiumDevTools1 = Nothing
            ChromiumWebBrowser1.Dock = DockStyle.Fill
            Panel1.Hide()
            devTools = False
        Else
            Panel1.Show()
            ChromiumWebBrowser1.Dock = DockStyle.Left
            Panel1.Width = Math.Round(Me.Width * 0.4)
            ChromiumDevTools1 = ChromiumWebBrowser1.ShowDevToolsDocked(Panel1)
            ChromiumDevTools1.Dock = DockStyle.Fill
            Panel1.Controls.Add(ChromiumDevTools1)
            ChromiumDevTools1.Show()
            ChromiumDevTools1.Size = New Point(Panel1.Width, Panel1.Height)
            ChromiumWebBrowser1.Width = Math.Round(Me.Width * 0.6)
            devTools = True
        End If
    End Sub

    Private Sub tab_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If devTools Then
            Panel1.Width = Math.Round(Me.Width * 0.4)
            ChromiumWebBrowser1.Width = Math.Round(Me.Width * 0.6)
            ChromiumDevTools1.Dock = DockStyle.Fill
            ChromiumDevTools1.Size = New Point(Panel1.Width, Panel1.Height)
        End If
    End Sub

    Private Sub ÀProposToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ÀProposToolStripMenuItem.Click
        ChromiumWebBrowser1.LoadUrl("solarys://about/")
    End Sub
End Class