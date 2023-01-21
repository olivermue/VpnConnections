# VPN Connections
A little helper tool that shows the current state of the selected VPN connection as tray icon.

![Configuration Dialog](Images/Dialog_english.png "Configuration Dialog")


The tool is customizable and you can configure the following settings:
- The name of the connection that should be tracked.
- When the icon should be shown. This can be never, always or only when connected or disconnected.
- The color of the tray icon in connected or disconnected stated. The color can either be manually configured or take the current accent color.
- If a notification message should be shown either never, when the state changed or when connected or disconnected.
- Decide if the tool should be run at startup of windows or not.
- What should happen if you click or double click with the left or right mouse button on the icon. Should the configuration dialog be opened, explicit connected or disconnected, change the connection state or close this application.

Additionally the tool has two command line arguments:
- `-show` will on startup automatically show the configuration dialog.
- `-culture` let you define the current language of the tool. English will be taken by default or if the desired language is not available. Currently additionally available is German only.

<br/>
<br/>

![Configuration Dialog German](Images/Dialog_deutsch.png "Configuration Dialog German")