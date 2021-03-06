# Codename Chaos
This is a group project that anyone and everyone in the Redmond/Eastside Game Developers Meetup can participate in. If you need help getting set up you can try reaching out to Ben Shutt or Taylor Robbins in the Discord (add your name here if you want to help people get set up).

To get added as a contributor you can contact Ben or Sam and also add your name here https://drive.google.com/drive/folders/1LEx9TKWAAbRbXt6AeKqv11VsZsjzYdRE?usp=sharing

There are 3 projects in this repository, a Unity project, a Godot project, and a Game Maker project. Feel free to use whichever project you are most comfortable with. Within each project there are multiples scenes/rooms which often contain completely different game styles. Feel free to make a new scene/room and work on whatever you want. Ask Taylor Robbins for information about best practices for adding a Scene/Room (making sure it gets a button on the main menu, making sure it's in the correct folder, etc.)

Unity                                                                                                     | Game Maker                                                                                                         | Godot
:--------------------------------------------------------------------------------------------------------:|:------------------------------------------------------------------------------------------------------------------:|:------------------
![Unity Project](https://github.com/Re-GameDev/Codename-Chaos/blob/main/Random/UnityProject.png?raw=true) | ![Game Maker Project](https://github.com/Re-GameDev/Codename-Chaos/blob/main/Random/GameMakerProject.png?raw=true) | ![Godot Project](https://github.com/Re-GameDev/Codename-Chaos/blob/main/Random/GodotProject.png?raw=true)

# Unity Information
We are using the latest Long Term Support (LTS) version of unity which is **version 2020.3.22f1**.

![Unity Hub Screenshot](https://github.com/Re-GameDev/Codename-Chaos/blob/main/Random/UnityHubScreenshotForReadme.png?raw=true)

# Modules
In order to build the game to WebGL, you need to install the Web GL Module after the screen above. Anything else is optional for you to use, however it is important to inform other programmer of platform switch.

![Module Selection](https://github.com/Re-GameDev/Codename-Chaos/blob/main/Random/modules.png?raw=true)

# Game Maker Information
We are using Game Maker Studio 2 v2022.3.0.625 with runtime v2022.3.0.497. Please make sure you've downloaded the correct runtime version and don't convert the project to other versions.

![Game Maker Runtime Release Notes Screenshot](https://github.com/Re-GameDev/Codename-Chaos/blob/main/Random/GameMakerRuntimeReleaseNotesScreenshot.png?raw=true)

# Godot Information
The Godot project is still getting setup. We will update this README when we have more things established...

# Git Information

## Cloning the repository (and authentication/account stuffs)
1. You will need to contact Ben or Sam to get added as a contributor and gain access to this repository. In order to do this you will need a Github account, so if you don't already have one, make one now. Then send your email or username to Ben or Sam in a DM on Discord.
2. Once you have done that you will also need to install Git: https://git-scm.com/book/en/v2/Getting-Started-Installing-Git.
3. Before you can clone (download) the repository you will need set up some authentication stuff. Git now requires you to use an authentication key in order to login when using Git Bash or the Git GUI. Here are some instructions for how to generate an authentication token: https://www.edgoad.com/2021/02/using-personal-access-tokens-with-git-and-github.html. When you get to selecting permissions, just select all the "repo" options. For expiration I just selected "No expiration".
4. Once you have your token, make sure you save it somewhere on your computer so you can easily reference it to copy and paste into the any textboxes or console prompts that are asking for your password. When asked for your username, just use your email that you use for you Github account. Or, ideally, use a password manager to store it safely.
5. NOTE: Most of the information I am going to give here is for the command line interface for git (typing in text commands to make it do stuff) but someone else might come along and give instructions for how to do these things in the Git GUI (the visual user interface version of git)
6. Open Git Bash. Depending on the options you selected (and your platform) you might be able to right click in a File Explorer and select "Git Bash Here." If not, you can try searching for "Git Bash" in the start menu. Once you do this you should see a console window that looks something like this: ![Git Bash](https://github.com/Re-GameDev/Codename-Chaos/blob/main/Random/GitBashScreenshotForReadme.png?raw=true)
7. We need to navigate to a directory where you can put the project. This can be wherever you want but make sure it's somewhere that you can find easily. You can see what folder you are currently in by looking at the prompt that showed up. For example in the image it tells me I am in /c/gamdev/projects (the yellow text). You can list folders in the current directory by typing "dir" (or "ls" for some systems) and hitting enter. To move between folders you can use the command "cd" (stands for Change Directory) followed by a space and then the name of the folder you want to move into. For example "cd CodenameChaos" would move me into the CodenameChaos folder. You can move up a folder by typing "cd .." and hitting enter. Navigate to wherever you want to put the project. If you need a suggestion you can navigate to your Desktop folder by doing "cd %USERPROFILE%\Desktop"
8. Now that you are in a folder, we are going to clone (download) the repository. Type in "clone https://github.com/Re-GameDev/Codename-Chaos CodenameChaos" and hit enter
9. It will ask you for a github username and password. Enter your email for username and the authentication token (the one we generated in step 3) for password. (The password might not show anything as you are typing for security reasons but you are actually typing and whatever you type before hitting enter will be recorded/used)
10. If all goes well this should have created a folder called CodenameChaos in the folder you chose. Go ahead and open the folder in File Explorer and find the Unity project inside.

# Publishing a WebGL Build
- Push a build targetting WebGL into the folder: `<root>/Build/` (or `<root>/GmBuild` for Game Maker)
- Only one build can be hosted at a time, so delete the contents of the folder before copying in a new build.
- The build is autohosted, but it takes a couple minutes to go live.
- The URL of the hosted build is: 
  - Unity: https://re-gamedev.github.io/Codename-Chaos/Build 
  - Game Maker: https://re-gamedev.github.io/Codename-Chaos/GmBuild/
  - Godot: https://re-gamedev.github.io/Codename-Chaos/GodotBuild/

# Discord
This repo is connected to our Discord server, and will post to the #????codename-chaos-commits channel whenever commits are pushed to it.

# Space Otter

![Space Otter](https://github.com/Re-GameDev/Codename-Chaos/blob/main/Random/Space%20Otter%201.jpeg?raw=true)
