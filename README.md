MCLauncher v2 by tman0
======================
After 1 year in development, the hopefully final version is almost here....

PEOPLE WHO ARE COMING TO MAKE SURE THEIR PASSWORD IS SAFE
---------------------------------------------------------
The files you want to look at are [Minecraft/Launcher.cs](https://github.com/tman0/MCLauncher2/blob/master/Minecraft/Launcher.cs) (specifically ```AuthenticatePlayer(string username, string password)``` and ```LaunchSequence``` and [UI/MainWindow.xaml.cs](https://github.com/tman0/MCLauncher2/blob/master/UI/MainWindow.xaml.cs) specifically ```Login_Click_1```. There, you no longer have any right to complain on Minecraft Forums about your password getting stolen.

Current Features
----------------
* Launches Minecraft

Future Features
---------------
* Server autoconnect management
* Multiple installations
* Mod installation system
* Public mod repository
* Localhosted/managed servers
* Quick login for multiple users

Pull Request Policy
-------------------
I'm very lenient about pull requests.
My only request is that you follow the [C# Coding Conventions](http://msdn.microsoft.com/en-us/library/ff926074.aspx) as outlined on MSDN. If you can fix a bug that I can't seem to get right, don't be shy, clone and send me a pull request and I'll look over it. This is open source, that means everyone can work on it.

License
-------
Fuck licenses. I really couldn't care less what you do with my work as long as you give me credit where credit is due and you don't try to sell it. (But... if you really want to sell it... get in touch. Of course I want a huge cut.) So I guess that makes this LGPL? Whatever.