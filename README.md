
# UrbanHealthPathMobile

Urban Health Path is an interdisciplinary project created by Computer Science and Architecture students. The project was created in cooperation with physiotherapists.

Implementation:
- F4talErr0r
- PaulinaCzapla
- matadam667

# The purpose of the project

The aim of this interdisciplinary project was to create a mobile application supporting the physical rehabilitation of the elderly through their physical activation. A specially developed municipal health path is to be used for this purpose. The purpose of the application is to encourage the elderly to leave home and take up physical and intellectual activity through quizzes and interesting facts.

## The main problems that had to be faced during the development of the application:
- Developing the needs, limitations and requirements of people aged 60+. Adapting the application to these needs. This included both the appearance (size of elements, colors), but also functionality and navigation through the application.
- Identify what can motivate the elderly to use the app and what spatial landmarks and app content will be appropriate.
- Analysis of urban space in terms of its suitability for the rehabilitation of the elderly. Development of a map with points and a designated path and translating it into functionality in the application.


# Implementation

The application was created using the Unity Engine.

Features:
- Map with marked points and a path between them. The user's position is marked and the rotation of the phone is taken into account. The map can be zoomed in and out, moved and centered. It is possible to navigate to the first point of the track from any other point. Reaching a designated point is detected automatically.
- The layout of the application is adapted to different types of smartphones (notches, front cameras, different screen sizes).
- The interface is adapted to the needs of an elderly person.
- Can be used without an internet connection as the content is stored locally.
- The application saves the statistics and achievements of the user. You can share statistics thanks to integration with Whatsapp.
- Offers various types of activities after reaching the point on the map - physical exercises presented in the form of videos, quizzes (text and picture) and interesting facts in the form of audio and text. The points on the map are interesting urban elements, often with interesting history and architecture.
- The user can check his physical fitness through questionnaires. This allows you to monitor your progress.
- Basic help system in case the user gets lost in the application interface.


## Usage of library, plugins, etc .:

- Mapbox SDK for Unity - maps were created using Mapbox SDK for Unity. Map styles were created on the studio.mapbox.com website, where the colors were set and the map layers were displayed. In addition, due to the fact that the application uses specific points of interest and the health path itself, Mapbox Studio has been used to create data sets containing characteristic points and paths.
- Device Simulator - this tool allowed us to check the operation and appearance of the application on various device models.
- Cross Platform Native Plugins: Essential Kit (Free Version) - easy integration of the application with WhatsApp.
- Newtonsoft JSON - support for the JSON format, which stores, among others, information about paths.

# Application
### Log in and main views

![image](https://user-images.githubusercontent.com/56382779/170338953-0facb5f9-c87c-4732-8e8d-22d365c9acda.png)
 ![image](https://user-images.githubusercontent.com/56382779/170338856-af5ca115-df56-438e-a4d0-69bd067d1dfa.png)
 
 
### Settings and Help 
![image](https://user-images.githubusercontent.com/56382779/170339049-034ccb2e-0c2f-4a3f-8ea5-aef9b0ce13ac.png)
![image](https://user-images.githubusercontent.com/56382779/170339058-34a2d3c7-94a2-408e-a99e-f9c78818d142.png)

### Path information

![image](https://user-images.githubusercontent.com/56382779/170339152-07f01f58-b67a-42fd-a17e-2d4f6b6b8a87.png) ![image](https://user-images.githubusercontent.com/56382779/170339176-fd837f22-54a9-4a06-a286-e9779ebfe7b6.png) 


### Map view

![image](https://user-images.githubusercontent.com/56382779/170339259-433c2b6d-4349-4d55-bd56-08df105287b5.png)

### Point
![image](https://user-images.githubusercontent.com/56382779/170339368-b547d69b-c922-4c14-a4c8-c01608baffb3.png)

![image](https://user-images.githubusercontent.com/56382779/170339389-9556b71b-0f4f-4b2a-9b28-88e48093b111.png)

### Quizzes and exercises

![image](https://user-images.githubusercontent.com/56382779/170339453-4a157e4a-c7a1-4490-b634-c6c8a644cc71.png)
![image](https://user-images.githubusercontent.com/56382779/170339474-aa41de04-9e2a-4d2f-8e67-168e03b7067f.png)


### Finish path

![image](https://user-images.githubusercontent.com/56382779/170339516-59741ad4-e8c1-4c65-94e2-4aea1e239cb4.png)




The app is still in developing. In the future, the following functionalities are planned:
- Login via google account
- Inviting friends and organizing group events
- More user profile features
- Increasing the possibility of personalizing the application by different styles and sizes of fonts
- Advanced tutorial system
