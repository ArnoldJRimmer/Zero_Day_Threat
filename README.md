# 2022 3DGED GDApp & GD Engine

## Overview ##
This repository contains code for the game engine


### Add To Table
- [ ] Overload operators - Integer2

### Exercises
- [ ] Add Integer2 to demo operator overloading in C# and add [expression bodied members](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members)

### Demo/Comment
- [x] Demo [git emojis](https://gitmoji.dev/) for begin, feature, bug, fix, refactor, remove
- [x] Relationship between world scale and movement speed
- [x] Demo PerfUtility updates re SpriteBatchInfo
- [ ] Demo EventDispatcher

### Bugs
- [x] Fix FirstPersonController rotation
- [x] Fix GameObject::GetComponent<>() - see FOVCameraController
- [x] PerfUtility is not updating FPS - Fixed - base.Update() was accidentally removed
- [ ] Fix CycledTranslationBehaviour

### Possible Improvements
- [ ] Add InputDevice to separate input from keyboard or gamepad and allow mappings

### To Do - Week 5
- [x] Explain two project setup (one app, one engine)
- [x] Explain Camera, Transform, Component
- [x] Explain abstract classes and methods
- [x] Explain Mesh
- [x] Explain GameObject, Component
- [x] Create QuadMesh
- [x] Explain Material, IEffect, Renderer
- [x] Add RotationBehaviour on Quad
- [x] Add simple (first pass) FirstPersonCameraController for camera
- [x] Added AppData to abstract and centralise hard-coded magic numbers and improve readability
- [x] Added skybox and grass plane
- [x] Tidied Main::Initialize
- [x] Added SamplerState setting to remove artifacts between skybox planes
- [x] Explain how to extend a class (see Extensions)
- [x] Finish CubeMesh

### To Do - Week 6
- [x] Added Scene to store List<GameObject>
- [x] Added GameObjectList to support splitting GameObjects into two lists - static (persists for game duration) and dynamic (add/remove during gameplay)
- [x] Refactored Scene to support opaque and transparent GameObjectLists
- [x] Added ObjectType and RenderType enums to GameObject to prevent passing in two hard-to-understand booleans
- [x] Added Add, Remove, Find, Size, Clear to Scene and GameObjectList
- [x] Added ModelMesh to support loading FBX meshes
- [x] Refactored Mesh, CubeMesh, QuadMesh and ModelMesh to solve constructor problem with model == null
- [x] Added SceneManager to support switching between scenes
- [x] Added Color and Viewport extensions for future use
- [x] Added Perf, Serialization, and String -Utility classes
- [x] Added ContentDictionary
- [x] Added methods in Scene to support transparent game objects
- [x] Added Curve classes for camera controller

### To Do - Week 7
- [x] Added pre-processor directives (i.e. #if...) to set HIRES and DEMO in Main and AppData
- [x] Re-added GameObject::RemoveComponent<T>() to support removal of components by type
- [x] Added Resolutions to clarify and standardize setting screen resolutions
- [x] Added Rail for RailBehaviour
- [x] Added CameraManager::ActiveCameraName for use in PerfUtility
- [x] Added ILoadLevel interface in preparation for moving level specific code out of Main
- [x] Added TestUpdateableOnlyComponent and TestUpdateableDrawableComponent to demo Component concept
- [x] Re-factor SecurityCameraBehaviour to add axis and re-name since it can be applied to any GameObject
- [x] Add CameraFOVController with validation on Camera::FieldOfView setter to ensure its never set <= zero
- [x] Demo SoundManager
- [x] Add camera types
- [x] Added GetPerfStats to Scene and GameObjectList
- [x] Improved PerfUtility to support component based information elements
- [x] Reduced font size on PerfUtility - it was a bit big - do this by opening the spritefont file in MGCB, setting Size in the XML file and re-building in MGCB
- [x] Refactor SecurityCameraBehaviour
- [x] Changed boom sound to play on B and not S key as it clashed with WASD for camera

### To Do - Week 8
- [x] Finish 1st Person Camera
- [ ] Convert SceneManager and CameraManager to inherit from DrawableGameComponent and GameComponent 
- [ ] Add ActionType enum to support turning Update and Draw on/off during gameplay (hint: use when menu is shown) for specific GameObject and Scene
- [ ] Change List to SortedList in GameObjectList to sort by material and reduce rendertime
- [ ] Add support for cloning GameObjects
- [ ] Add Camera::Viewport
- [ ] Add clean/dirty flag to Transform on change
- [ ] Add event notification on Transform change
- [ ] Practice extension of a class (see Extensions)
- [ ] Add Integer2 for use with screen resolution to prevent need to typecast - see InitializeGraphics()
- [ ] Refactor RotationBehaviour 
- [ ] Move level-specific methods to Level class

 
