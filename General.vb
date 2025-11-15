Imports System.IO
Imports System.Environment
Imports System.Net
Imports CefSharp
Imports Elektraostog_Solaryl.tab
Imports Newtonsoft.Json

Public Module General
    Public TabContentMap As New Dictionary(Of TabPage, Control)
    Public TextColor As Color = If(My.Settings.darkmode, Color.FromArgb(200, 200, 205), Color.Black)
    Public TextSelectedColor As Color = Color.Black
    Public _BackColor As Color = If(My.Settings.darkmode, Color.FromArgb(40, 40, 45), Color.FromArgb(240, 240, 240))
    Public BackSelectedColor As Color = Color.FromArgb(179, 215, 243)
    Public ButtonSelectedColor As Color = If(My.Settings.darkmode, Color.DimGray, Color.LightGray)
    Public XButtonSelectedColor As Color = If(My.Settings.darkmode, Color.FromArgb(206, 86, 86), Color.Red)
    Public TextBoxBackColor As Color = If(My.Settings.darkmode, Color.FromArgb(51, 51, 57), Color.White)
    Public TabBackColor As Color = If(My.Settings.darkmode, Color.FromArgb(40, 40, 45), Color.LightGray)

    Public Class TSRenderer
        Inherits ToolStripProfessionalRenderer

        Private topLevelNormalBack As Color = _BackColor
        Private topLevelHoverBack As Color = BackSelectedColor
        Private topLevelNormalText As Color = TextColor
        Private topLevelHoverText As Color = TextSelectedColor

        Private dropDownNormalBack As Color = _BackColor
        Private dropDownHoverBack As Color = BackSelectedColor
        Private dropDownNormalText As Color = TextColor
        Private dropDownSelectedText As Color = TextSelectedColor

        Private separatorColor As Color = TextColor

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

    Public curTab As tab

    Public Class ParamManager
        Public Function getSettings() As Object
            Return New With {
                .darkmode = My.Settings.darkmode,
                .homepage = My.Settings.homepage,
                .searchengine = My.Settings.searchengine,
                .theme = My.Settings.theme
            }
        End Function

        Public Sub saveSettings(darkmode As Boolean, homepage As String, searchengine As String, theme As String)
            My.Settings.darkmode = darkmode
            My.Settings.homepage = homepage
            My.Settings.searchengine = searchengine
            My.Settings.theme = theme
            My.Settings.Save()
        End Sub
    End Class

    Public Class UpdateManager
        Public Function getVer() As Object
            Dim r = CheckUpdate(verr)
            Return New With {
                .ver = verr,
                .onlinever = r.Item2,
                .isNeeded = r.Item1
            }
        End Function

        Public Sub goUpdate()
            curTab.AjouterNouvelOnglet("https://github.com/Principaute-de-Solarys/Elektraostog-Solaryl")
        End Sub
    End Class

    Public Class HistoryManager
        Public Function getJSON(path As String) As Object
            Try
                If path = "history" Then
                    If IO.File.Exists(historyFileLoc) Then
                        Return IO.File.ReadAllText(historyFileLoc)
                    Else
                        Return "[]"
                    End If
                End If
            Catch ex As Exception
                Return "[]"
            End Try

            Return "[]"
        End Function

        Public Sub clearJSON(path As String)
            Try
                If path = "history" Then
                    If IO.File.Exists(historyFileLoc) Then
                        IO.File.Delete(historyFileLoc)
                    End If
                End If
            Catch ex As Exception
            End Try
        End Sub
    End Class

    Public Class SolarysSchemeHandlerFactory
        Implements ISchemeHandlerFactory
        Public Function Create(browser As IBrowser, frame As IFrame, schemeName As String, request As IRequest) As IResourceHandler Implements ISchemeHandlerFactory.Create
            If schemeName = "solarys" Then
                If request.Url.StartsWith("solarys://settings/") Then
                    Return ResourceHandler.FromString(My.Resources.settings)
                ElseIf request.Url.StartsWith("solarys://about/") Then
                    Return ResourceHandler.FromString(My.Resources.about)
                ElseIf request.Url.StartsWith("solarys://update/") Then
                    Return ResourceHandler.FromString(My.Resources.update)
                ElseIf request.Url.StartsWith("solarys://history/") Then
                    Return ResourceHandler.FromString(My.Resources.history)
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

    Public Class HistoryObj
        Public dateLinked As Date
        Public name As String
        Public url As String
    End Class

    Public Class FavoriteObject
        Public name As String
        Public url As String
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

    Dim verr As String = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision

    Public Function CheckUpdate(localVer As String)
        Dim web As HttpWebRequest = HttpWebRequest.Create("https://github.com/Principaute-de-Solarys/Elektraostog-Solaryl/raw/refs/heads/master/ver")
        web.Timeout = 10000
        web.Method = "GET"
        Dim reponse As HttpWebResponse = web.GetResponse()
        Dim reader As New StreamReader(reponse.GetResponseStream())
        Dim page As String = reader.ReadToEnd
        Dim onlinever As String = page.Substring(0, 7)
        Dim isNeeded As Boolean = False
        If Not onlinever = localVer Then
            isNeeded = True
        End If
        Return (isNeeded, onlinever)
    End Function

    Dim ADdirLoc As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Colog danelektrascal\Elektraostog Solaryl"
    Dim historyFileLoc As String
    Dim favoriteFileLoc As String

    Public Sub InitAppData()
        If Not IO.Directory.Exists(ADdirLoc) Then
            IO.Directory.CreateDirectory(GetFolderPath(SpecialFolder.ApplicationData) & "\Colog danelektrascal")
            IO.Directory.CreateDirectory(ADdirLoc)
        End If
        historyFileLoc = ADdirLoc & "\history.json"
        favoriteFileLoc = ADdirLoc & "\favorite.json"
    End Sub

    Public Sub AddToHistory(dateLinked As Date, name As String, url As String)
        Dim historyObj As New HistoryObj With {
            .dateLinked = dateLinked,
            .name = name,
            .url = url
        }

        Dim historyList As New List(Of HistoryObj)

        If IO.File.Exists(historyFileLoc) Then
            Dim existingJson As String = IO.File.ReadAllText(historyFileLoc)

            If Not String.IsNullOrWhiteSpace(existingJson) Then
                Try
                    historyList = JsonConvert.DeserializeObject(Of List(Of HistoryObj))(existingJson)
                Catch ex As Exception
                    historyList = New List(Of HistoryObj)
                End Try
            End If
        End If

        If historyList.Count > 0 Then
            Dim lastEntry = historyList.Last()

            Dim delta As TimeSpan = dateLinked - lastEntry.dateLinked

            If lastEntry.url = url AndAlso Math.Abs(delta.TotalSeconds) < 5 Then
                Exit Sub
            End If
        End If

        historyList.Add(historyObj)

        If historyList.Count > 500 Then
            Dim toRemove As Integer = historyList.Count - 500
            historyList.RemoveRange(0, toRemove)
        End If

        Dim newJson As String = JsonConvert.SerializeObject(historyList, Formatting.Indented)
        IO.File.WriteAllText(historyFileLoc, newJson)
    End Sub

    Public Sub AddFavorite(name As String, url As String)
        Dim favoriteObj As New FavoriteObject With {
            .name = name,
            .url = url
        }

        Dim favoriteList As New List(Of FavoriteObject)

        If IO.File.Exists(favoriteFileLoc) Then
            Dim existingJson As String = IO.File.ReadAllText(favoriteFileLoc)

            If Not String.IsNullOrWhiteSpace(existingJson) Then
                Try
                    favoriteList = JsonConvert.DeserializeObject(Of List(Of FavoriteObject))(existingJson)
                Catch ex As Exception
                    favoriteList = New List(Of FavoriteObject)
                End Try
            End If
        End If

        favoriteList.Add(favoriteObj)

        Dim newJson As String = JsonConvert.SerializeObject(favoriteList, Formatting.Indented)
        IO.File.WriteAllText(favoriteFileLoc, newJson)
    End Sub

    Public Sub RemoveFavorite(url As String)
        If IO.File.Exists(favoriteFileLoc) Then
            Dim json As String = IO.File.ReadAllText(favoriteFileLoc)
            Dim list = JsonConvert.DeserializeObject(Of List(Of FavoriteObject))(json)

            list.RemoveAll(Function(f) f.url.Equals(url))

            Dim newJson = JsonConvert.SerializeObject(list, Formatting.Indented)
            IO.File.WriteAllText(favoriteFileLoc, newJson)
        End If
    End Sub


    Public Function LoadFavorite() As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)

        Try
            If IO.File.Exists(favoriteFileLoc) Then
                Dim json As String = IO.File.ReadAllText(favoriteFileLoc)

                If Not String.IsNullOrWhiteSpace(json) Then
                    Dim favoriteList As List(Of FavoriteObject) = JsonConvert.DeserializeObject(Of List(Of FavoriteObject))(json)

                    If favoriteList IsNot Nothing Then
                        For Each fav In favoriteList
                            If Not result.ContainsKey(fav.name) Then
                                result.Add(fav.name, fav.url)
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
        End Try

        Return result
    End Function
End Module
