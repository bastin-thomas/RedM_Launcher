# 🤠 RedM_Launcher
 The Redm Server Launcher, will do some automated elementary work before launching RedM (remove temporary files, launching Steam, RockstarLauncher, or EpicGameStore).

## 🙍‍♂️ Usage
The Redm Launcher, is intended to be used as a Client Launcher, that will start all programs necessary to connect yourself to RedM, and after, will launch your RedM Client and directly connect you to the Server.

## 🖥️ For Developers : 
### How To Install
1. [Install Visual Code with .NET desktop development module (.NET 8.0)](https://visualstudio.microsoft.com/fr/free-developer-offers/)
2. Open ```RedM_Launcher.sln```
3. Press on *Play Button* (Execute)

## 🎨 Design : 
![MainPage](./.Documentation/Mockup/MainPage.png)
![SettingPage](./.Documentation/Mockup/SettingPage.png)

## 🔧 Want a Custom Server Launcher ?
Contact me on Discord (Arkios) to get a customized Launcher for your server (Ability to have dynamic Logo/Background based on events, Possibility to add Specific features...) 

## 🐛| Next Fix Update:

☐ Better Popup Thread handling (popup currently not showing because not in UI Thread).

## 🐛| 1.0.1 Hotfix

✅ Fix concurency access to online Background/logo

✅ Add Mutex to have only one App instance at same time



## ♻️| 1.1 Advancement

☐ Launching Pop Bar (Skippable Timer + What's going on)

☐ Create functions to get RP Board Data and displaying them on the mainpage

## ♻️| 1.0 Advancement

✅ ReadMe Creation

✅ Repository Folder Tree Creation

✅ Main Window View Creation

✅ Main Window ViewModel Creation

✅ Main Page View Creation

✅ Main Page ViewModel Creation

✅ Setting Page View Creation
- RedM Folder + install azerty at Launch + Clear Cache at Launch
- RedM IP String + Auto-Connect or not
- Steam Folder
- Epic Folder + IsEnabled
- Rockstar Folder + IsEnabled
- TeamSpeak Folder 
- TeamSpeak String + Auto-Connect or not

✅ Setting Page ViewModel Creation

✅ App Model Creation (Storable) -> Finnaly using .Net Settings

✅ Navigation Logic / Routing Creation

✅ Create functions to Launch RedM using auto-connect to an IP or not

✅ Create functions to Clear the Cache

✅ Create functions to Launch all needed Launcher

✅ Create functions to Launch TeamSpeak on the right Server

✅ Create functions to Manage the First Launch

✅ Dynamic Enable/Disable MainButton + Text / Background representing What's going on

✅ Hide App on RedM Launch, and Show App on RedM Close 

✅ Create functions to get CFX RedM Status and Update UI to relate it

✅ Create functions to get Server Status and Update UI to relate it

✅ Internationalization For EN & FR (more could be added by the Community). Based on OS Language

✅ Custom Popups

✅ Online Backgrounds / Logos

✅ Add "Hide When RedM Running" Settings


## 🎨 Inspiration : 
The launcher take it's inspiration on a project created by Zerator's Community, for their own server.
Another one is the RockStarLauncher UI. 

![SOZ Launcher](./.Documentation/Inspiration/SOZ_Launcher.jpg)
![Rockstar MainPage](./.Documentation/Inspiration/Rockstar1.png)
![Rockstar SettingPage](./.Documentation/Inspiration/Rockstar2.png)
