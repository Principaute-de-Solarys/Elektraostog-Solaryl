Imports CefSharp
Imports Elektraostog_Solaryl.tab

Public Module General
    Public TabContentMap As New Dictionary(Of TabPage, Control)

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
    Public Class ParamManager
        Public Function getSettings() As Object
            Return New With {
                .darkmode = My.Settings.darkmode,
                .homepage = My.Settings.homepage
            }
        End Function

        Public Sub saveSettings(darkmode As Boolean, homepage As String)
            My.Settings.darkmode = darkmode
            My.Settings.homepage = homepage
            My.Settings.Save()
        End Sub
    End Class

    Public Class SolarysSchemeHandlerFactory
        Implements ISchemeHandlerFactory
        Public Function Create(browser As IBrowser, frame As IFrame, schemeName As String, request As IRequest) As IResourceHandler Implements ISchemeHandlerFactory.Create
            If schemeName = "solarys" Then
                If request.Url.StartsWith("solarys://settings/") Then
                    Return ResourceHandler.FromString(My.Resources.settings)
                Else
                    Return ResourceHandler.FromString(My.Resources.notfound)
                End If
            End If
            Return New ResourceHandler()
        End Function
        Private Function GetMimeType(extension As String) As String
            Select Case extension.ToLower()
                Case ".html", ".htm"
                    Return "text/html"
                Case ".js"
                    Return "application/javascript"
                Case ".css"
                    Return "text/css"
                Case ".png"
                    Return "image/png"
                Case ".jpg", ".jpeg"
                    Return "image/jpeg"
                Case ".gif"
                    Return "image/gif"
                Case Else
                    Return "application/octet-stream"
            End Select
        End Function
    End Class

    Public Class SolarysJsDialogHandler
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

    Public Class SolarysLifeSpanHandler
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

    Public Function GetOSInfo() As String
        Dim os As OperatingSystem = Environment.OSVersion
        Dim osName As String

        Select Case os.Platform
            Case PlatformID.Win32NT
                Select Case os.Version.Major
                    Case 10
                        osName = "Windows NT 10.0; Win64; x64"
                    Case 6
                        Select Case os.Version.Minor
                            Case 3
                                osName = "Windows NT 6.3; Win64; x64" ' Windows 8.1
                            Case 2
                                osName = "Windows NT 6.2; Win64; x64" ' Windows 8
                            Case 1
                                osName = "Windows NT 6.1; Win64; x64" ' Windows 7
                            Case Else
                                osName = "Windows NT 6.x; Win64; x64"
                        End Select
                    Case Else
                        osName = "Windows NT; Win64; x64"
                End Select

            Case PlatformID.Unix
                osName = "X11; Linux x86_64"

            Case PlatformID.MacOSX
                osName = "Macintosh; Intel Mac OS X 10_15_7"

            Case Else
                osName = "Unknown OS"
        End Select

        Return osName
    End Function
End Module
