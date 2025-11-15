<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class tab
    Inherits System.Windows.Forms.UserControl

    'UserControl remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(tab))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.PageTSSB = New System.Windows.Forms.ToolStripSplitButton()
        Me.RevenirEnArrièreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RevenirEnAvantToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RafraîchirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AjouterAuxFavorisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NouvellePageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FermerLaPageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AfficherLesDevToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RevenirSurSolarysToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ServeurDiscordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChaîneYouTubeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CompteInstagramToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ParamètresToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HistoriqueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MettreÀJourToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ÀProposToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.FavoriteButton = New System.Windows.Forms.ToolStripSplitButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ChromiumWebBrowser1 = New CefSharp.WinForms.ChromiumWebBrowser()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageTSSB, Me.ToolStripSeparator2, Me.ToolStripLabel1, Me.ToolStripTextBox1, Me.ToolStripButton1, Me.ToolStripSeparator3, Me.FavoriteButton})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ToolStrip1.Size = New System.Drawing.Size(941, 31)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'PageTSSB
        '
        Me.PageTSSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageTSSB.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RevenirEnArrièreToolStripMenuItem, Me.RevenirEnAvantToolStripMenuItem, Me.RafraîchirToolStripMenuItem, Me.AjouterAuxFavorisToolStripMenuItem, Me.NouvellePageToolStripMenuItem, Me.FermerLaPageToolStripMenuItem, Me.AfficherLesDevToolsToolStripMenuItem, Me.ToolStripSeparator1, Me.RevenirSurSolarysToolStripMenuItem, Me.ParamètresToolStripMenuItem, Me.HistoriqueToolStripMenuItem, Me.MettreÀJourToolStripMenuItem, Me.ÀProposToolStripMenuItem})
        Me.PageTSSB.Image = CType(resources.GetObject("PageTSSB.Image"), System.Drawing.Image)
        Me.PageTSSB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageTSSB.Name = "PageTSSB"
        Me.PageTSSB.Size = New System.Drawing.Size(40, 28)
        Me.PageTSSB.Text = "Page"
        '
        'RevenirEnArrièreToolStripMenuItem
        '
        Me.RevenirEnArrièreToolStripMenuItem.Name = "RevenirEnArrièreToolStripMenuItem"
        Me.RevenirEnArrièreToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.RevenirEnArrièreToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.RevenirEnArrièreToolStripMenuItem.Text = "Revenir en arrière"
        '
        'RevenirEnAvantToolStripMenuItem
        '
        Me.RevenirEnAvantToolStripMenuItem.Name = "RevenirEnAvantToolStripMenuItem"
        Me.RevenirEnAvantToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.RevenirEnAvantToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.RevenirEnAvantToolStripMenuItem.Text = "Revenir en avant"
        '
        'RafraîchirToolStripMenuItem
        '
        Me.RafraîchirToolStripMenuItem.Name = "RafraîchirToolStripMenuItem"
        Me.RafraîchirToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.RafraîchirToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.RafraîchirToolStripMenuItem.Text = "Rafraîchir"
        '
        'AjouterAuxFavorisToolStripMenuItem
        '
        Me.AjouterAuxFavorisToolStripMenuItem.Name = "AjouterAuxFavorisToolStripMenuItem"
        Me.AjouterAuxFavorisToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.AjouterAuxFavorisToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.AjouterAuxFavorisToolStripMenuItem.Text = "Ajouter aux favoris"
        '
        'NouvellePageToolStripMenuItem
        '
        Me.NouvellePageToolStripMenuItem.Name = "NouvellePageToolStripMenuItem"
        Me.NouvellePageToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NouvellePageToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.NouvellePageToolStripMenuItem.Text = "Nouvelle page"
        '
        'FermerLaPageToolStripMenuItem
        '
        Me.FermerLaPageToolStripMenuItem.Name = "FermerLaPageToolStripMenuItem"
        Me.FermerLaPageToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.FermerLaPageToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.FermerLaPageToolStripMenuItem.Text = "Fermer la page"
        '
        'AfficherLesDevToolsToolStripMenuItem
        '
        Me.AfficherLesDevToolsToolStripMenuItem.Name = "AfficherLesDevToolsToolStripMenuItem"
        Me.AfficherLesDevToolsToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.AfficherLesDevToolsToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.AfficherLesDevToolsToolStripMenuItem.Text = "Afficher les DevTools"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(245, 6)
        '
        'RevenirSurSolarysToolStripMenuItem
        '
        Me.RevenirSurSolarysToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ServeurDiscordToolStripMenuItem, Me.SiteToolStripMenuItem, Me.ChaîneYouTubeToolStripMenuItem, Me.CompteInstagramToolStripMenuItem})
        Me.RevenirSurSolarysToolStripMenuItem.Name = "RevenirSurSolarysToolStripMenuItem"
        Me.RevenirSurSolarysToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.RevenirSurSolarysToolStripMenuItem.Text = "Revenir sur Solarys"
        '
        'ServeurDiscordToolStripMenuItem
        '
        Me.ServeurDiscordToolStripMenuItem.Name = "ServeurDiscordToolStripMenuItem"
        Me.ServeurDiscordToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ServeurDiscordToolStripMenuItem.Text = "Serveur Discord"
        '
        'SiteToolStripMenuItem
        '
        Me.SiteToolStripMenuItem.Name = "SiteToolStripMenuItem"
        Me.SiteToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SiteToolStripMenuItem.Text = "Site"
        '
        'ChaîneYouTubeToolStripMenuItem
        '
        Me.ChaîneYouTubeToolStripMenuItem.Name = "ChaîneYouTubeToolStripMenuItem"
        Me.ChaîneYouTubeToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ChaîneYouTubeToolStripMenuItem.Text = "Chaîne YouTube"
        '
        'CompteInstagramToolStripMenuItem
        '
        Me.CompteInstagramToolStripMenuItem.Name = "CompteInstagramToolStripMenuItem"
        Me.CompteInstagramToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CompteInstagramToolStripMenuItem.Text = "Compte Instagram"
        '
        'ParamètresToolStripMenuItem
        '
        Me.ParamètresToolStripMenuItem.Name = "ParamètresToolStripMenuItem"
        Me.ParamètresToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.ParamètresToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.ParamètresToolStripMenuItem.Text = "Paramètres"
        '
        'HistoriqueToolStripMenuItem
        '
        Me.HistoriqueToolStripMenuItem.Name = "HistoriqueToolStripMenuItem"
        Me.HistoriqueToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.HistoriqueToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.HistoriqueToolStripMenuItem.Text = "Historique"
        '
        'MettreÀJourToolStripMenuItem
        '
        Me.MettreÀJourToolStripMenuItem.Name = "MettreÀJourToolStripMenuItem"
        Me.MettreÀJourToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.MettreÀJourToolStripMenuItem.Text = "Mettre à jour"
        '
        'ÀProposToolStripMenuItem
        '
        Me.ÀProposToolStripMenuItem.Name = "ÀProposToolStripMenuItem"
        Me.ÀProposToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.ÀProposToolStripMenuItem.Text = "À propos"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 31)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(72, 28)
        Me.ToolStripLabel1.Text = "Rechercher :"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(500, 31)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(28, 28)
        Me.ToolStripButton1.Text = "Y aller"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 31)
        '
        'FavoriteButton
        '
        Me.FavoriteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.FavoriteButton.Image = Global.Elektraostog_Solaryl.My.Resources.Resources.Favoris
        Me.FavoriteButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.FavoriteButton.Name = "FavoriteButton"
        Me.FavoriteButton.Size = New System.Drawing.Size(40, 28)
        Me.FavoriteButton.Text = "Favoris"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'ChromiumWebBrowser1
        '
        Me.ChromiumWebBrowser1.ActivateBrowserOnCreation = False
        Me.ChromiumWebBrowser1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ChromiumWebBrowser1.Location = New System.Drawing.Point(0, 31)
        Me.ChromiumWebBrowser1.Name = "ChromiumWebBrowser1"
        Me.ChromiumWebBrowser1.Size = New System.Drawing.Size(487, 534)
        Me.ChromiumWebBrowser1.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(741, 31)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 534)
        Me.Panel1.TabIndex = 2
        '
        'tab
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ChromiumWebBrowser1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "tab"
        Me.Size = New System.Drawing.Size(941, 565)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents PageTSSB As ToolStripSplitButton
    Friend WithEvents RevenirEnArrièreToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RevenirEnAvantToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RafraîchirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents RevenirSurSolarysToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents NouvellePageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FermerLaPageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ParamètresToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MettreÀJourToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ServeurDiscordToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SiteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ChromiumWebBrowser1 As CefSharp.WinForms.ChromiumWebBrowser
    Friend WithEvents AfficherLesDevToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ÀProposToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ChaîneYouTubeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CompteInstagramToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HistoriqueToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents AjouterAuxFavorisToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FavoriteButton As ToolStripSplitButton
End Class
