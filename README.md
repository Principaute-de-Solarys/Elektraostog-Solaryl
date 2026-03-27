# Elektraostog Solaryl

**Elektraostog Solaryl** est un navigateur web développé en **VB.NET** avec **CEFSharp** par **Colog danelektrascral**, pour la **Principauté de Solarys**.
C'est un navigateur solaryen, pensé pour les solaryens. Il est voué à être simple, fluide et personnalisable.

## Présentation

Elektraostog Solaryl est un projet en développement, actuellement en version `0.2`.
Il s'agit d'un navigateur basé sur **Chromium**, avec une interface **WinForms entièrement redesignée**, pensée pour offrir une expérience claire et moderne.

## Fonctionnalités principales

- **Navigation Internet** : bah c'est un navigateur quoi.
- **Système de pages internes** :
  - comme `solarys://about/`, la page d'accueil par défaut, remplaçable dans les paramètres ;
  - 🆕 `solarys://history/` pour accéder à ton historique ;
  - ou bien `solarys://settings/` pour les paramètres ;
  - et `solarys://update/` pour mettre à jour le navigateur.
- **Système d'onglets** : gestion complète des onglets (c'est plus dur que ça en a l'air).
- **Système de favoris** : pour mettre en favori tes pages préférées.
- **Moteurs de recherche supportés** :
  - Par défaut : **DuckDuckGo**
  - En option : **Google, Qwant, Bing, Yahoo**
- **Thèmes** : sombre (par défaut) et clair.
- **Icônes** : Visual Studio like (par défaut) et simple.
- **Interface retravaillée** : c'est basé sur WinForms, mais avec une interface qu'on a essayé de retravailler (au mieux).

## Fonctionnalités prévues

| Fonction | Statut | Remarques |
|--|--|--|
| Gestion des téléchargements | 🟡 Prévu | Ça sera un point travaillé en priorité |
| Bug des raccourcis | 🟡 Prévu | Ça sera un point travaillé en priorité |
| Système d'extensions | 🟠 Possible | Sera ajouté si demandé |
| "Intranet solaryen" | 🟠 Possible | Sera ajouté si demandé |
| Restauration de session | 🟠 Possible | Sera ajouté si demandé |
| Mode privé | ⚫ Lointain | Vraiment pas prioritaire |
| Filtre de contenu / sécurité avancée | ⚫ Lointain | Non prévu pour l'instant |

Si vous avez d'autres suggestions, dites le moi !

## Specs du projet

- **Langage :** VB.NET
- **Framework :** .NET Framework 4.7.2
- **Noyau du navigateur :** [CEFSharp](https://github.com/cefsharp/CefSharp) (Chromium Embedded Framework)
- **Moteur de rendu :** WinForms

## Compatibilité

- **Windows uniquement** pour le moment
- .NET Framework 4.7.2 l'oblige, **Windows 7 SP1+ x86-64** est requis

## Licence

Ce projet est distribué sous la **licence Apache 2.0**.
Tu peux l'utiliser, le modifier et le redistribuer librement, à condition de créditer **Colog danelektrascral**.
