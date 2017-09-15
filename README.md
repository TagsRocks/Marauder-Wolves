# Marauder Wolves
 Make sure to Build and Run the game in Unity 5.6.1f1 or later for best performance.
![unity](https://user-images.githubusercontent.com/18353476/29190917-b09be474-7dd0-11e7-9ada-b68d15f26d54.gif)
# Level Design
![dana-forest2](https://user-images.githubusercontent.com/18353476/27512611-82516322-58fb-11e7-8195-0fb2935f6996.PNG)
![dana city](https://user-images.githubusercontent.com/18353476/27512592-8ba5b758-58fa-11e7-9aeb-d98075e1fc21.PNG)
![seth city](https://user-images.githubusercontent.com/18353476/28496612-815bb5f0-6f24-11e7-9e83-32aeaf9720fa.PNG)

# Newer Level Designs
![a_bg10sunrise_goc](https://user-images.githubusercontent.com/18353476/30455870-65687ad4-9956-11e7-9058-3e29915262b1.png)
![night](https://user-images.githubusercontent.com/18353476/30456024-d7f20322-9956-11e7-90c5-d570b55805ff.png)
![a_shop_goc](https://user-images.githubusercontent.com/18353476/30455871-656ab358-9956-11e7-9601-aa6d6a4d9d36.png)

# Multiplayer Co-Op Lobby and partial World Map
![multiplayer](https://user-images.githubusercontent.com/18353476/28496614-858c67aa-6f24-11e7-8506-ad4fd8c3642d.PNG)
![map](https://user-images.githubusercontent.com/18353476/28496613-83bbe4fa-6f24-11e7-9659-4a61ccdaaeeb.PNG)

# Unity(Building for PC, Mac, Android, and iOS)

[Get Unity here](https://unity3d.com/)

[Game Development with Unity for iOS and Android devices.pdf](https://github.com/Mikerr1111/Marauder-Wolves/files/1216274/Game.Development.with.Unity.for.iOS.and.Android.devices.pdf)

![unity-native-plugins-intro-sdk-architecture](https://user-images.githubusercontent.com/18353476/29191030-200f304a-7dd1-11e7-99c0-96915e796721.png)

![build_to_android_5](https://user-images.githubusercontent.com/18353476/27527819-55779986-5a02-11e7-96cc-bfaeb3a1b5f6.png)
![playersetandroidconfiguration](https://user-images.githubusercontent.com/18353476/28398802-b888561e-6cbd-11e7-9bd4-9d77f33e424e.png)
![playersetandroidpublish](https://user-images.githubusercontent.com/18353476/28398805-ba6209d0-6cbd-11e7-87e4-c1ce57973303.png)

Android Support on Devices running Android 6.0 or later(Below is a Preview of the game running on a Nexus Phone with Android 7.0)
![mw](https://user-images.githubusercontent.com/18353476/28504040-4af758ba-6fc5-11e7-8c0f-d180f8e752b7.png)

# Getting started with Android development

[Getting Started with Android in Unity](https://docs.unity3d.com/Manual/android-GettingStarted.html)

# Android Studio
https://developer.android.com/studio/index.html

[A good tutorial for Android Studio Setup(Windows, macOS, and Linux)](https://www.tutorialspoint.com/android/android_studio.htm)

![as](https://user-images.githubusercontent.com/18353476/28494127-6da78c40-6eda-11e7-8fa0-d77a5294b193.png)

# iOS Build Settings
Unity v5.6.1 or later.

Requires iOS 10 or later.

Requires [XCode 9](https://developer.apple.com/xcode/) Beta 5 or later. 

Requires iOS device(iPhone 6S or later, iPad (2016) or later)

In Bulid setting make sure to checkmark the boxes for Symlink Unity libraries and Development Build.
![switch_platform-ios](https://user-images.githubusercontent.com/18353476/29189508-afd6ff06-7dcb-11e7-84e3-0b45e50e36ca.png)

# Vulkan Support 
Vulkan is a new generation graphics and compute API that provides high-efficiency, cross-platform access to modern GPUs in both PCs and on mobile platforms. Android Nougat version 7.0 from Google brings official support for the Vulkan API.The main benefit of Vulkan over older mobile rendering APIs such as OpenGL ES 3.x is speed. Vulkan is designed to take advantage of multiple CPU cores by allowing the application to build command lists in multiple threads in parallel. This allows the application to take advantage of all of the CPU cores on the device, improving performance.

To enable Vulkan support, open “Player Settings”, go to the “Other Settings” pane and clear the “Auto Graphics API” checkbox. You are presented with an ordered list of graphics APIs to choose from. If Vulkan is not on that list, click the ‘+’ sign at the bottom of the list to add it. Then drag Vulkan to be the first item on the list so that it’ll be used whenever supported, and you’re done! All your existing shaders will get translated to Vulkan SPIR-V.
![vulkan](https://user-images.githubusercontent.com/18353476/28993201-7543d586-7965-11e7-8e9c-f93b7079e2bf.PNG)
