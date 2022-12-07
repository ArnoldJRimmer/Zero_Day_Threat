#region Pre-compiler directives

#define DEMO
#define SHOW_DEBUG_INFO

#endregion

using BEPUphysics;
using BEPUphysics.Entities.Prefabs;
using GD.Core;
using GD.Engine;
using GD.Engine.Events;
using GD.Engine.Globals;
using GD.Engine.Inputs;
using GD.Engine.Managers;
using GD.Engine.Parameters;
using GD.Engine.Utilities;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Application = GD.Engine.Globals.Application;
using Box = BEPUphysics.Entities.Prefabs.Box;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Cue = GD.Engine.Managers.Cue;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Material = GD.Engine.Material;
using GD.Engine.Collections;
using System.Windows.Forms;
using System.Linq;

namespace GD.App
{
    public class Main : Game
    {
        #region Fields

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BasicEffect unlitEffect;
        private BasicEffect litEffect;

        private CameraManager cameraManager;
        private SceneManager sceneManager;
        private SoundManager soundManager;
        private PhysicsManager physicsManager;
        private RenderManager renderManager;
        private EventDispatcher eventDispatcher;
        private GameObject playerGameObject;
        private StateManager stateManager;

        private Path temp;
        private GameObject tempCube1;
        private GameObject tempCube2;
        private GameObject tempCube3;
        private GameObject tempCube4;

#if DEMO
        private event EventHandler OnChanged;
#endif

        #endregion Fields

        #region Constructors

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #endregion Constructors

        #region Actions - Initialize

#if DEMO

        private void DemoCode()
        {
            //shows how we can create an event, register for it, and raise it in Main::Update() on Keys.E press
            DemoEvent();

            //shows us how to listen to a specific event
            DemoStateManagerEvent();

            Demo3DSoundTree();
        }

        private void Demo3DSoundTree()
        {
            //var camera = Application.CameraManager.ActiveCamera.AudioListener;
            //var audioEmitter = //get tree, get emitterbehaviour, get audio emitter

            //object[] parameters = { "sound name", audioListener, audioEmitter};

            //EventDispatcher.Raise(new EventData(EventCategoryType.Sound,
            //    EventActionType.OnPlay3D, parameters));

            //throw new NotImplementedException();
        }

        private void DemoStateManagerEvent()
        {
            EventDispatcher.Subscribe(EventCategoryType.Player, HandleEvent);
        }

        private void HandleEvent(EventData eventData)
        {
            switch (eventData.EventActionType)
            {
                case EventActionType.OnWin:
                    System.Diagnostics.Debug.WriteLine(eventData.Parameters[0] as string);
                    break;

                case EventActionType.OnLose:
                    System.Diagnostics.Debug.WriteLine(eventData.Parameters[2] as string);
                    break;

                case EventActionType.OnPlay2D:
                    Application.SoundManager.Play2D(eventData.Parameters[0] as string);
                    break;

                case EventActionType.OnPauseSound:
                    Application.SoundManager.Stop(eventData.Parameters[0] as string);
                    break;


                default:
                    break;
            }
        }

      
        private void DemoEvent()
        {
            OnChanged += HandleOnChanged;
        }

        private void HandleOnChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{e} was sent by {sender}");
        }

#endif

        protected override void Initialize()
        {
            //moved spritebatch initialization here because we need it in InitializeDebug() below
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //core engine - common across any game
            InitializeEngine(AppData.APP_RESOLUTION, true, true);

            //game specific content
            InitializeLevel("Zero Day Threat", AppData.SKYBOX_WORLD_SCALE);

        

#if SHOW_DEBUG_INFO
            InitializeDebug();
#endif

#if DEMO
            DemoCode();
#endif

            base.Initialize();
        }

        #endregion Actions - Initialize

        #region Actions - Level Specific

        protected override void LoadContent()
        {
          
        }

        private void InitializeLevel(string title, float worldScale)
        {
            //set game title
            SetTitle(title);

            //initialize curves used by cameras
            InitializeCurves();

            //initialize rails used by cameras
            InitializeRails();

            //add scene manager and starting scenes
            InitializeScenes();

            //add collidable drawn stuff
            InitializeCollidableContent(worldScale);

            //add non-collidable drawn stuff
            InitializeNonCollidableContent(worldScale);

            //load sounds, textures, models etc
            LoadMediaAssets();

            //add drawn stuff
            //InitializeDrawnContent(worldScale);

            InitializePath();
            //add the player
            //InitializePlayer();

            //Raise all the events that I want to happen at the start
            //object[] parameters = { "epic_soundcue" };
            //EventDispatcher.Raise(
            //    new EventData(EventCategoryType.Player,
            //    EventActionType.OnSpawnObject,
            //    parameters));
            EventDispatcher.Subscribe(EventCategoryType.Sound, HandleEvent);
        }

        private void SetTitle(string title)
        {
            Window.Title = title.Trim();
        }

        private void LoadMediaAssets()
        {
            //sounds, models, textures
            LoadSounds();
            LoadTextures();
            LoadModels();
        }

        private void LoadSounds()
        {
            var soundEffect =
                Content.Load<SoundEffect>("Assets/Audio/Diegetic/explode1");

            //add the new sound effect
            soundManager.Add(new Cue(
                "boom1",
                soundEffect,
                SoundCategoryType.Alarm,
                new Vector3(1, 1, 0),
                false));

            var alarmEffect =
               Content.Load<SoundEffect>("Assets/Audio/Diegetic/smokealarm1");

            //add the new sound effect
            soundManager.Add(new Cue(
                "smokeAlarm",
                alarmEffect,
                SoundCategoryType.Alarm,
                new Vector3(1, 1, 0),
                false));
        }

        private void LoadTextures()
        {
            //load and add to dictionary
        }

        private void LoadModels()
        {
            //load and add to dictionary

            InitializeSatiliteModel();
            IntializeRadarModel();
            IntializeConsoleModel();
            IntializeKeypadModel();
            IntializeButtonModel();
            IntializeKeyboardModel();
            IntializePanelModel();
            IntializeVentModel();
            IntializeScreenLeftModel();
            IntializeScreenCentreModel();
            IntializeScreenRightModel();
            IntializeFloppyDiskModel();
            IntializeRadioModel();
            IntializeLampModel();
            
            InitializeCube();

        }

        private void InitializeCurves()
        {
            //load and add to dictionary
        }

        private void InitializeRails()
        {
            //load and add to dictionary
        }

        private void InitializeScenes()
        {
            //initialize a scene
            var scene = new Scene("MissionControl");

            //add scene to the scene manager
            sceneManager.Add(scene.ID, scene);

            //don't forget to set active scene
            sceneManager.SetActiveScene("MissionControl");
        }

        private void InitializeEffects()
        {
            //only for skybox with lighting disabled
            unlitEffect = new BasicEffect(_graphics.GraphicsDevice);
            unlitEffect.TextureEnabled = true;

            //all other drawn objects
            litEffect = new BasicEffect(_graphics.GraphicsDevice);
            litEffect.TextureEnabled = true;
            litEffect.LightingEnabled = true;
            litEffect.EnableDefaultLighting();
        }

        private void InitializeCameras()
        {
            //camera
            GameObject cameraGameObject = null;


            #region First Person

            //camera 1
            cameraGameObject = new GameObject(AppData.FIRST_PERSON_CAMERA_NAME);
            cameraGameObject.Transform = new Transform(null, null, AppData.FIRST_PERSON_DEFAULT_CAMERA_POSITION);
            cameraGameObject.AddComponent(
                new Camera(
                AppData.FIRST_PERSON_HALF_FOV, //MathHelper.PiOver2 / 2,
                (float)_graphics.PreferredBackBufferWidth / _graphics.PreferredBackBufferHeight,
                AppData.FIRST_PERSON_CAMERA_NCP, //0.1f,
                AppData.FIRST_PERSON_CAMERA_FCP,
                new Viewport(0, 0, _graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight))); // 3000

            //added ability for camera to listen to 3D sounds
            cameraGameObject.AddComponent(new AudioListenerBehaviour());

            //NEW
            cameraGameObject.AddComponent(new FirstPersonController(AppData.FIRST_PERSON_MOVE_SPEED, AppData.FIRST_PERSON_STRAFE_SPEED,
                AppData.PLAYER_ROTATE_SPEED_VECTOR2, true));

            cameraManager.Add(cameraGameObject.Name, cameraGameObject);

            #endregion First Person

            cameraManager.SetActiveCamera(AppData.FIRST_PERSON_CAMERA_NAME);
        }

        private void InitializeCollidableContent(float worldScale)
        {
            #region Collidable

            InitializeColliableGround(worldScale);
            InitializeCollidableModel();

            #endregion
        }

        private void InitializeNonCollidableContent(float worldScale)
        {
            //create sky
            InitializeSkyBoxAndGround(worldScale);

        }

        private void InitializeColliableGround(float worldScale)
        {
            var collidableGround = new Box(BEPUutilities.Vector3.Zero, worldScale, 1, worldScale);
            physicsManager.Space.Add(collidableGround);
            physicsManager.Space.Add(new Box(new BEPUutilities.Vector3(0, 4, 0), 1, 1, 1, 1));
            physicsManager.Space.Add(new Box(new BEPUutilities.Vector3(0, 8, 0), 1, 1, 1, 1));
            physicsManager.Space.Add(new Box(new BEPUutilities.Vector3(0, 12, 0), 1, 1, 1, 1));
        }
        
        private void InitializeCollidableModel()
        {
            //game object
            var gameObject = new GameObject("my first collidable box!", ObjectType.Static, RenderType.Opaque);

            gameObject.Transform = new Transform(null, null, new Vector3(0, 4, 0));
            var texture = Content.Load<Texture2D>("Assets/Textures/Props/Crates/crate2");
            var model = Content.Load<Model>("Assets/Models/cube");
            var mesh = new Engine.ModelMesh(_graphics.GraphicsDevice, model);

            gameObject.AddComponent(new Renderer(
                new GDBasicEffect(litEffect),
                new Material(texture, 1f, Color.White),
                mesh));

            gameObject.AddComponent(new BoxCollider(new Vector3(0, 10, 0),
                1, 1, 1, 10));

            sceneManager.ActiveScene.Add(gameObject);
        }
        
        #region Zero Day Threat - Models
        private void InitializeSatiliteModel()
        {
            var satiliteGameObject = new GameObject(AppData.SATILITE_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            satiliteGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var satiliteTexture = Content.Load<Texture2D>("Assets/Textures/Satellite/satalite2_Material_BaseColor");
            var satiliteFbxModel = Content.Load<Model>("Assets/Models/satalite2");
            var satiliteMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, satiliteFbxModel);
            satiliteGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(satiliteTexture, 1), satiliteMesh));
            sceneManager.ActiveScene.Add(satiliteGameObject);
        }

        #region Console - Models

        private void IntializeConsoleModel()
        {
            var consoleGameObject = new GameObject(AppData.CONSOLE_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            consoleGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var consoleTexture = Content.Load<Texture2D>("Assets/Textures/console/console_DefaultMaterial_BaseColor");
            var consoleFbxModel = Content.Load<Model>("Assets/Models/console");
            var consoleMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, consoleFbxModel);
            consoleGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(consoleTexture, 1), consoleMesh));
            sceneManager.ActiveScene.Add(consoleGameObject);
        }
        private void IntializeKeypadModel()
        {
            var keypadGameObject = new GameObject(AppData.KEYPAD_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            keypadGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var keypadTexture = Content.Load<Texture2D>("Assets/Textures/console/keypad_DefaultMaterial_BaseColor");
            var keypadFbxModel = Content.Load<Model>("Assets/Models/keypad");
            var keypadMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, keypadFbxModel);
            keypadGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(keypadTexture, 1), keypadMesh));
            sceneManager.ActiveScene.Add(keypadGameObject);
        }

        private void IntializeButtonModel()
        {
            var buttonGameObject = new GameObject(AppData.BUTTON_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            buttonGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var buttonTexture = Content.Load<Texture2D>("Assets/Textures/console/button_DefaultMaterial_Base_color");
            var buttonFbxModel = Content.Load<Model>("Assets/Models/button");
            var buttonMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, buttonFbxModel);
            buttonGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(buttonTexture, 1), buttonMesh));
            sceneManager.ActiveScene.Add(buttonGameObject);
        }

        private void IntializeKeyboardModel()
        {
            var keyboardGameObject = new GameObject(AppData.KEYBOARD_GAMEOJECT_NAME, ObjectType.Static, RenderType.Opaque);
            keyboardGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var keyboardTexture = Content.Load<Texture2D>("Assets/Textures/console/keyboard_Base_color");
            var keyboardFbxModel = Content.Load<Model>("Assets/Models/keyboard");
            var keyboardMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, keyboardFbxModel);
            keyboardGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(keyboardTexture, 1), keyboardMesh));
            sceneManager.ActiveScene.Add(keyboardGameObject);
        }

        private void IntializePanelModel()
        {
            var panelGameObject = new GameObject(AppData.PANEL_GAMEOBECT_NAME, ObjectType.Static, RenderType.Opaque);
            panelGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var panelTexture = Content.Load<Texture2D>("Assets/Textures/console/panel_Base_color");
            var panelFbxModel = Content.Load<Model>("Assets/Models/panel");
            var panelMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, panelFbxModel);
            panelGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            sceneManager.ActiveScene.Add(panelGameObject);
        }

        private void IntializeVentModel()
        {
            var ventGameObject = new GameObject(AppData.VENT_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            ventGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var ventTexture = Content.Load<Texture2D>("Assets/Textures/console/vent_Base_color");
            var ventFbxModel = Content.Load<Model>("Assets/Models/vent");
            var ventMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, ventFbxModel);
            ventGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(ventTexture, 1), ventMesh));
            sceneManager.ActiveScene.Add(ventGameObject);
        }

        private void IntializeScreenRightModel()
        {
            var screenRightGameObject = new GameObject(AppData.SCREEN_RIGHT_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            screenRightGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var screenRightTexture = Content.Load<Texture2D>("Assets/Textures/console/screenright_Base_color");
            var screenRightFbxModel = Content.Load<Model>("Assets/Models/screen-right");
            var screenRightMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, screenRightFbxModel);
            screenRightGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(screenRightTexture, 1), screenRightMesh));
            sceneManager.ActiveScene.Add(screenRightGameObject);
        }

        private void IntializeScreenCentreModel()
        {
            var screenCentreGameObject = new GameObject(AppData.SCREEN_CENTRE_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            screenCentreGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var screenCentreTexture = Content.Load<Texture2D>("Assets/Textures/console/screencentre_Base_color");
            var screenCentreFbxModel = Content.Load<Model>("Assets/Models/screen-centre");
            var screenCentreMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, screenCentreFbxModel);
            screenCentreGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(screenCentreTexture, 1), screenCentreMesh));
            sceneManager.ActiveScene.Add(screenCentreGameObject);
        }

        private void IntializeScreenLeftModel()
        {
            var screenLeftGameObject = new GameObject(AppData.SCREEN_LEFT_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            screenLeftGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var screenLeftTexture = Content.Load<Texture2D>("Assets/Textures/console/screenleft_Base_color");
            var screenLeftFbxModel = Content.Load<Model>("Assets/Models/screen-left");
            var screenLeftMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, screenLeftFbxModel);
            screenLeftGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(screenLeftTexture, 1), screenLeftMesh));
            sceneManager.ActiveScene.Add(screenLeftGameObject);
        }

        private void IntializeRadioModel()
        {
            var radioGameObject = new GameObject(AppData.RADIO_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            radioGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var radioTexture = Content.Load<Texture2D>("Assets/Textures/console/radio_Base_Color");
            var radioFbxModel = Content.Load<Model>("Assets/Models/radio");
            var radioMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, radioFbxModel);
            radioGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(radioTexture, 1), radioMesh));
            sceneManager.ActiveScene.Add(radioGameObject);
        }

        private void IntializeLampModel()
        {
            var lampGameObject = new GameObject(AppData.LAMP_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            lampGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var lampTexture = Content.Load<Texture2D>("Assets/Textures/console/lamp_Base_color");
            var lampFbxModel = Content.Load<Model>("Assets/Models/lamp");
            var lampMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, lampFbxModel);
            lampGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(lampTexture, 1), lampMesh));
            sceneManager.ActiveScene.Add(lampGameObject);
        }

        private void IntializeFloppyDiskModel()
        {
            var floppyDiskGameObject = new GameObject(AppData.FLOPPY_DISk_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            floppyDiskGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var floppyDiskTexture = Content.Load<Texture2D>("Assets/Textures/console/floppydisk_Base_color");
            var floppyDiskFbxModel = Content.Load<Model>("Assets/Models/floppy-disk");
            var floppyDiskMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, floppyDiskFbxModel);
            floppyDiskGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(floppyDiskTexture, 1), floppyDiskMesh));
            sceneManager.ActiveScene.Add(floppyDiskGameObject);
        }

        #endregion

        private void IntializeRadarModel()
        {
            var radarGameObject = new GameObject(AppData.CONSOLE_GAMEOBJECT_NAME, ObjectType.Static, RenderType.Opaque);
            radarGameObject.Transform = new Transform(new Vector3(1.5f, 1.5f, 1.5f), null, null);
            var radarTexture = Content.Load<Texture2D>("Assets/Textures/Radar/radar-display_DefaultMaterial_BaseColor");
            var radarFbxModel = Content.Load<Model>("Assets/Models/radar-display");
            var radarMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, radarFbxModel);
            radarGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(radarTexture, 1), radarMesh));
            sceneManager.ActiveScene.Add(radarGameObject);
        }

        #endregion Zero Day Threat - Models

        #region Zero Day Threat - The Cube
        private void InitializeCube()
        {
            //Make each cube out of 6 different planes. Ideally have a class called Cubey.cs that makes the cube out of these six faces
            //Sample of what i would like to do

            //tempCube1 = new GameObject(AppData.CUBE_NAME, ObjectType.Static, RenderType.Opaque);
            //tempCube1.Transform = new Transform(new Vector3(0.3f, 0.3f, 0.3f), Vector3.Zero, Vector3.One);

            tempCube1 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube1.Transform = new Transform(new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, -MathHelper.PiOver2*2), Vector3.One);

            var panelTexture = Content.Load<Texture2D>("Assets/Textures/cube_DefaultMaterial_BaseColor");
            var panelFbxModel = Content.Load<Model>("Assets/Models/cube");
            var panelMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, panelFbxModel);
            tempCube1.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube1.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad1));

            tempCube2 = new GameObject(AppData.CUBE_NAME, ObjectType.Static, RenderType.Opaque);
            tempCube2.Transform = new Transform(new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 1, 1.7f));
            tempCube2.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube2.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad2));

            tempCube3 = new GameObject(AppData.CUBE_NAME, ObjectType.Static, RenderType.Opaque);
            tempCube3.Transform = new Transform(new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 1, 2.4f));
            tempCube3.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube3.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad3));


            tempCube4 = new GameObject(AppData.CUBE_NAME, ObjectType.Static, RenderType.Opaque);
            tempCube4.Transform = new Transform(new Vector3(0.3f, 0.3f, 0.3f), Vector3.Zero, new Vector3(1, 1, 3.1f));

            tempCube4 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube4.Transform = new Transform(new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, 1, 3.1f));

            tempCube4.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube4.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad4));


            sceneManager.ActiveScene.Add(tempCube1);
            sceneManager.ActiveScene.Add(tempCube2);
            sceneManager.ActiveScene.Add(tempCube3);
            sceneManager.ActiveScene.Add(tempCube4);


            //#region Variables
            //GameObject face_F1 = null;
            //GDBasicEffect gdBasicEffect = new GDBasicEffect(unlitEffect);
            //Mesh quadMesh = new QuadMesh(_graphics.GraphicsDevice);
            //var cubeFbx = Content.Load<Model>("Assets/Models/cube");
            //Engine.ModelMesh cubeMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, cubeFbx);
            //var cubeTxt = Content.Load<Texture2D>("Assets/Textures/cube_DefaultMaterial_BaseColor");
            //Texture2D faceTexture = Content.Load<Texture2D>("Assets/Textures/SkyBox/basicwall");
            //#endregion Variables
            //#region Old Code
            //#region Front Faces
            ////Front Face - Need 9
            //face_F1 = new GameObject("Front", ObjectType.Static, RenderType.Opaque);
            //face_F1.Transform = new Transform(new Vector3(0.3f, 0.3f, 0.3f), null, new Vector3(0, 1, 0));
            //face_F1.AddComponent(new Renderer(gdBasicEffect, new Material(faceTexture, 1, Color.Orange), quadMesh));
            //sceneManager.ActiveScene.Add(face_F1);
            //#endregion Front Faces

            //////Back Face - Need 9

            ////#region Left Faces
            //////Left Face - Need 9
            ////GameObject face_L1 = null;
            ////face_L1 = new GameObject("Left", ObjectType.Static, RenderType.Opaque);
            ////face_L1.Transform = new Transform(new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, MathHelper.ToRadians(-90), 0), new Vector3(-0.15f, 1, -0.15f));
            ////face_L1.AddComponent(new Renderer(gdBasicEffect, new Material(faceTexture, 1, Color.Blue), quadMesh));
            ////sceneManager.ActiveScene.Add(face_L1);
            ////#endregion Left Faces

            ////#region Right Faces
            //////Right Face - Need 9
            ////GameObject face_R1 = null;
            ////face_R1 = new GameObject("Right", ObjectType.Static, RenderType.Opaque);
            ////face_R1.Transform = new Transform(new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, MathHelper.ToRadians(90), 0), new Vector3(0.15f, 1, -0.15f));
            ////face_R1.AddComponent(new Renderer(gdBasicEffect, new Material(faceTexture, 1, Color.Green), quadMesh));
            ////sceneManager.ActiveScene.Add(face_R1);
            ////#endregion

            ////#region Top Faces
            //////Top Face
            ////GameObject face_T1 = null;
            ////face_T1 = new GameObject("Top", ObjectType.Static, RenderType.Opaque);
            ////face_T1.Transform = new Transform(new Vector3(0.3f, 0.3f, 0.3f), new Vector3(MathHelper.ToRadians(-90), 0, 0), new Vector3(0, 1.15f, -0.15f));
            ////face_T1.AddComponent(new Renderer(gdBasicEffect, new Material(faceTexture, 1, Color.Red), quadMesh));
            ////sceneManager.ActiveScene.Add(face_T1);
            ////#endregion Top Faces
            ////Bottom Face
            //#endregion Old Code

            //Cubey myCubey = new Cubey();
            //GameObject face1 = myCubey.CubeyBoi("Front", new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, 0), new Vector3(0, 1, 0),
            //    gdBasicEffect, faceTexture, quadMesh, Color.Orange);

            //GameObject face2 = myCubey.CubeyBoi("Left", new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, MathHelper.ToRadians(-90), 0), new Vector3(-0.15f, 1, -0.15f),
            //    gdBasicEffect, faceTexture, quadMesh, Color.Blue);

            //GameObject face3 = myCubey.CubeyBoi("Right", new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, MathHelper.ToRadians(90), 0), new Vector3(0.15f, 1, -0.15f),
            //    gdBasicEffect, faceTexture, quadMesh, Color.Green);

            //GameObject face4 = myCubey.CubeyBoi("Top", new Vector3(0.3f, 0.3f, 0.3f), new Vector3(MathHelper.ToRadians(-90), 0, 0), new Vector3(0, 1.15f, -0.15f)
            //    , gdBasicEffect, faceTexture, quadMesh, Color.Red);

            //GameObject face5 = myCubey.CubeyBoi("Bottom", new Vector3(0.3f, 0.3f, 0.3f), new Vector3(MathHelper.ToRadians(90), 0, 0), new Vector3(0, 1.15f, -0.15f),
            //    gdBasicEffect, faceTexture, quadMesh, Color.Orange);

            //GameObject face6 = myCubey.CubeyBoi("Back", new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, MathHelper.ToRadians(180), 0), new Vector3(0, 1, -0.3f),
            //    gdBasicEffect, faceTexture, quadMesh, Color.Pink);

            //Transform pathOne = new Transform(Vector3.Zero, new Vector3(MathHelper.ToRadians(90), 0, 0), Vector3.One);

            //#region RotationCheck
            ////Enters if Statement
            //if (pathOne.rotation == face5.Transform.rotation)
            //{
            //    Console.WriteLine("Face 5 is apart of the Path");
            //}
            //else
            //{
            //    Console.WriteLine("Didn't enter if");
            //}
            //#endregion RotationCheck

            //#region Old Code
            //GameObject pathOne = new GameObject("PathOne"); 
            //pathOne.Transform = new Transform(Vector3.Zero, new Vector3(MathHelper.ToRadians(90), 0, 0), Vector3.One);

            //GameObject pathTwo = new GameObject("PathTwo");
            //pathTwo.Transform = new Transform(Vector3.One, new Vector3(0, MathHelper.ToRadians(90), 0), Vector3.Zero);

            //GameObject pathThree = new GameObject("PathThree");
            //pathThree.Transform = new Transform(Vector3.UnitY, new Vector3(0, 0, 0), Vector3.Zero);

            //GameObject pathFour = new GameObject("PathFour");
            //pathFour.Transform = new Transform(new Vector3(2, 4, 3), new Vector3(0, MathHelper.ToRadians(-90), 0), Vector3.One);

            //sceneManager.ActiveScene.Add(pathOne);
            //sceneManager.ActiveScene.Add(pathTwo);
            //sceneManager.ActiveScene.Add(pathThree);
            //sceneManager.ActiveScene.Add(pathFour);
            //Adds it to the scene 


            //sceneManager.ActiveScene.Add(face1);
            //sceneManager.ActiveScene.Add(face2);
            //sceneManager.ActiveScene.Add(face3);
            //sceneManager.ActiveScene.Add(face4);
            //sceneManager.ActiveScene.Add(face5);
            //sceneManager.ActiveScene.Add(face6);


            //#region Basic Proto for cube Rot
            //cube1 = myCubey.CubeyBoi("Cube 1", new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, 0), new Vector3(0, 1, 0),
            // gdBasicEffect, faceTexture, cubeMesh, Color.Red, Keys.NumPad1);
            //cube1.AddComponent(new Renderer(new GDBasicEffect(effect), new Material(cubeTxt, 1), cubeMesh));

            //GameObject cube2 = myCubey.CubeyBoi("Cube 2", new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, 0), new Vector3(0.3f, 1, 0),
            // gdBasicEffect, faceTexture, cubeMesh, Color.Blue, Keys.NumPad2);

            //GameObject cube3 = myCubey.CubeyBoi("Cube 3", new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, 0), new Vector3(0.6f, 1, 0),
            //gdBasicEffect, faceTexture, cubeMesh, Color.Green, Keys.NumPad3);

            //GameObject cube4 = myCubey.CubeyBoi("Cube 4", new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, 0), new Vector3(0.9f, 1, 0),
            //gdBasicEffect, faceTexture, cubeMesh, Color.Yellow, Keys.NumPad4);

            //GameObject cube5 = myCubey.CubeyBoi("Cube 5", new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, 0), new Vector3(1.2f, 1, 0),
            //gdBasicEffect, faceTexture, cubeMesh, Color.Pink, Keys.NumPad5);


            //sceneManager.ActiveScene.Add(cube1);
            //sceneManager.ActiveScene.Add(cube2);
            //sceneManager.ActiveScene.Add(cube3);
            //sceneManager.ActiveScene.Add(cube4);
            //sceneManager.ActiveScene.Add(cube5);
            //#endregion


        }
        #endregion Zero Day Threat - The Cube

        private void InitializePath()
        {
            var gdBasicEffect = new GDBasicEffect(unlitEffect);
            var quadMesh = new QuadMesh(_graphics.GraphicsDevice);

            //Rotation Values shouldn't be changed
            temp = new Path("Temporary");
            Transform one = new Transform(new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, MathHelper.ToRadians(0)), Vector3.One);
            Transform two = new Transform(new Vector3(0.3f, 0.3f, 0.3f), new  Vector3(0, 0, MathHelper.ToRadians(0)), new Vector3(1, 1, 1.7f));
            Transform three = new Transform(new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, MathHelper.ToRadians(0)), new Vector3(1, 1, 2.4f));
            Transform four = new Transform(new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, MathHelper.ToRadians(0)), new Vector3(1, 1, 3.1f));
            temp.AddPiece(one);
            temp.AddPiece(two);
            temp.AddPiece(three);
            temp.AddPiece(four);

            // Since Path has a list of transforms, I went through the list creating GameObjects of the indiviual transforms. 
            // Then the GameObjects created in the for loop are added to the ActiveScene
            // Put in Breakpoint which occurs if the number of times it is hit is 3  ---- hits 3 times
            GameObject temptile = null;
            for (int i = 0; i <= temp.Size - 1; i++)
            {
                if (temp.Pieces[i] != null)
                {
                    Console.WriteLine($"{temp.Pieces[i]}");

                    temptile = new GameObject("TempTile" + i, ObjectType.Static, RenderType.Transparent);
                    temptile.Transform = temp.Pieces[i];
                    var texture = Content.Load<Texture2D>("Assets/Textures/Skybox/basicwall");
                    temptile.AddComponent(new Renderer(gdBasicEffect, new Material(texture, 1), quadMesh));
                    sceneManager.ActiveScene.Add(temptile);

                }
                else
                {
                    Console.WriteLine("NULL VALUE");
                }
            }


            // https://zetcode.com/csharp/predicate/
            //Predicate<GameObject> t = tile => temptile.Name.StartsWith("TempTile");

            //GameObject found = sceneManager.ActiveScene.Find(ObjectType.Static, RenderType.Transparent, t);
            //if (found != null)
            //{
            //    Console.WriteLine($"Found {found.Name}");
            //}

            #region Commented-out Code
            //foreach (Transform piece in temp.Pieces)
            //{
            //    if(piece != null)
            //    {
            //        Console.WriteLine($"{piece}");

            //        GameObject temptile = new GameObject("TempTile" + , ObjectType.Static, RenderType.Transparent);
            //        temptile.Transform = piece;
            //        var texture = Content.Load<Texture2D>("Assets/Textures/Skybox/basicwall");
            //        tempPath.AddComponent(new Renderer(gdBasicEffect, new Material(texture, 1), quadMesh));
            //        sceneManager.ActiveScene.Add(tempPath);
            //    }
            //    else
            //    {
            //        Console.WriteLine("NUll VALUE");
            //    }
            //}

            //GameObject temptile = new GameObject("TempPath", ObjectType.Static, RenderType.Transparent);
            //tempPath.Transform = one;
            //var texture = Content.Load<Texture2D>("Assets/Textures/Skybox/basicwall");
            //tempPath.AddComponent(new Renderer(gdBasicEffect, new Material(texture, 1), quadMesh));
            //sceneManager.ActiveScene.Add(tempPath);
            #endregion Commented-out Code
            #region Old Code
            //This code had errors as there was no components
            //GameObject pathOne = new GameObject("PathOne"); 
            //pathOne.Transform = new Transform(Vector3.Zero, new Vector3(MathHelper.ToRadians(90), 0, 0), Vector3.One);

            //GameObject pathTwo = new GameObject("PathTwo");
            //pathTwo.Transform = new Transform(Vector3.One, new Vector3(0, MathHelper.ToRadians(90), 0), Vector3.Zero);

            //GameObject pathThree = new GameObject("PathThree");
            //pathThree.Transform = new Transform(Vector3.UnitY, new Vector3(0, 0, 0), Vector3.Zero);

            //GameObject pathFour = new GameObject("PathFour");
            //pathFour.Transform = new Transform(new Vector3(2, 4, 3), new Vector3(0, MathHelper.ToRadians(-90), 0), Vector3.One);

            //sceneManager.ActiveScene.Add(pathOne);
            //sceneManager.ActiveScene.Add(pathTwo);
            //sceneManager.ActiveScene.Add(pathThree);
            //sceneManager.ActiveScene.Add(pathFour);
            //Adds it to the scene 
            #endregion Old Code

        
        }

        private void InitializeSkyBoxAndGround(float worldScale)
        {
            float halfWorldScale = worldScale / 2.0f;

            GameObject quad = null;
            var gdBasicEffect = new GDBasicEffect(unlitEffect);
            var quadMesh = new QuadMesh(_graphics.GraphicsDevice);

            //skybox - back face
            quad = new GameObject("skybox back face");
            quad.Transform = new Transform(new Vector3(worldScale, worldScale, 1), null, new Vector3(0, 0, -halfWorldScale));
            var texture = Content.Load<Texture2D>("Assets/Textures/Skybox/basicwall");
            quad.AddComponent(new Renderer(gdBasicEffect, new Material(texture, 1), quadMesh));
            sceneManager.ActiveScene.Add(quad);

            //skybox - left face
            quad = new GameObject("skybox left face");
            quad.Transform = new Transform(new Vector3(worldScale, worldScale, 1), new Vector3(0, MathHelper.ToRadians(90), 0), new Vector3(-halfWorldScale, 0, 0));
            texture = Content.Load<Texture2D>("Assets/Textures/Skybox/basicwall");
            quad.AddComponent(new Renderer(gdBasicEffect, new Material(texture, 1), quadMesh));
            sceneManager.ActiveScene.Add(quad);

            //skybox - right face
            quad = new GameObject("skybox right face");
            quad.Transform = new Transform(new Vector3(worldScale, worldScale, 1), new Vector3(0, MathHelper.ToRadians(-90), 0), new Vector3(halfWorldScale, 0, 0));
            texture = Content.Load<Texture2D>("Assets/Textures/Skybox/basicwall");
            quad.AddComponent(new Renderer(gdBasicEffect, new Material(texture, 1), quadMesh));
            sceneManager.ActiveScene.Add(quad);

            //skybox - top face
            quad = new GameObject("skybox top face");
            quad.Transform = new Transform(new Vector3(worldScale, worldScale, 1), new Vector3(MathHelper.ToRadians(90), MathHelper.ToRadians(-90), 0), new Vector3(0, halfWorldScale, 0));
            texture = Content.Load<Texture2D>("Assets/Textures/Skybox/basicwall");
            quad.AddComponent(new Renderer(gdBasicEffect, new Material(texture, 1), quadMesh));
            sceneManager.ActiveScene.Add(quad);

            //skybox - front face
            quad = new GameObject("skybox front face");
            quad.Transform = new Transform(new Vector3(worldScale, worldScale, 1), new Vector3(0, MathHelper.ToRadians(-180), 0), new Vector3(0, 0, halfWorldScale));
            texture = Content.Load<Texture2D>("Assets/Textures/Skybox/basicwall");
            quad.AddComponent(new Renderer(gdBasicEffect, new Material(texture, 1), quadMesh));
            sceneManager.ActiveScene.Add(quad);

            //ground
            quad = new GameObject("ground");
            quad.Transform = new Transform(new Vector3(worldScale, worldScale, 1), new Vector3(MathHelper.ToRadians(-90), 0, 0), new Vector3(0, 0, 0));
            texture = Content.Load<Texture2D>("Assets/Textures/Skybox/ground");
            quad.AddComponent(new Renderer(gdBasicEffect, new Material(texture, 1), quadMesh));
            sceneManager.ActiveScene.Add(quad);
        }

        #endregion Actions - Level Specific

        #region Actions - Engine Specific

        private void InitializeEngine(Vector2 resolution, bool isMouseVisible, bool isCursorLocked)
        {
            //add support for mouse etc
            InitializeInput();

            //set screen resolution and show/hide mouse
            InitializeGraphics();

            //add game effects
            InitializeEffects();

            //add dictionaries to store and access content
            InitializeDictionaries();

            //add camera, scene manager
            InitializeManagers();

            //share some core references
            InitializeGlobals();

            //set screen properties (incl mouse)
            InitializeScreen(resolution, isMouseVisible, isCursorLocked);

            //add game cameras
            InitializeCameras();
        }

        private void InitializeGlobals()
        {
            //Globally shared commonly accessed variables
            Application.Main = this;
            Application.GraphicsDeviceManager = _graphics;
            Application.GraphicsDevice = _graphics.GraphicsDevice;
            Application.Content = Content;

            //Add access to managers from anywhere in the code
            Application.CameraManager = cameraManager;
            Application.SceneManager = sceneManager;
            Application.SoundManager = soundManager;
            Application.PhysicsManager = physicsManager;
        }

        private void InitializeInput()
        {
            //Globally accessible inputs
            Input.Keys = new KeyboardComponent(this);
            Components.Add(Input.Keys);
            Input.Mouse = new MouseComponent(this);
            Components.Add(Input.Mouse);
            Input.Gamepad = new GamepadComponent(this);
            Components.Add(Input.Gamepad);
        }

        /// <summary>
        /// Sets game window dimensions and shows/hides the mouse
        /// </summary>
        /// <param name="resolution"></param>
        /// <param name="isMouseVisible"></param>
        /// <param name="isCursorLocked"></param>
        private void InitializeScreen(Vector2 resolution, bool isMouseVisible, bool isCursorLocked)
        {
            GD.Core.Screen screen = new GD.Core.Screen();
            // Screen screen = new Screen();

            //set resolution
            screen.Set(resolution, isMouseVisible, isCursorLocked);

            //set global for re-use by other entities
            Application.Screen = screen;

            //set starting mouse position i.e. set mouse in centre at startup
            Input.Mouse.Position = screen.ScreenCentre;

            ////calling set property
            //_graphics.PreferredBackBufferWidth = (int)resolution.X;
            //_graphics.PreferredBackBufferHeight = (int)resolution.Y;
            //IsMouseVisible = isMouseVisible;
            //_graphics.ApplyChanges();
        }

        /// <summary>
        /// Sets the sampler states etc
        /// </summary>
        private void InitializeGraphics()
        {
            //TODO - move later to something like RenderManager
            //sets the sampler states which defines how textures are drawn when surface has UV values outside the range [0,1] - fixes the line issue at boundary between skybox textures
            SamplerState samplerState = new SamplerState();
            samplerState.AddressU = TextureAddressMode.Mirror;
            samplerState.AddressV = TextureAddressMode.Mirror;
            _graphics.GraphicsDevice.SamplerStates[0] = samplerState;
        }

        private void InitializeManagers()
        {
            //add event dispatcher for system events - the most important element!!!!!!
            eventDispatcher = new EventDispatcher(this);
            //add to Components otherwise no Update() called
            Components.Add(eventDispatcher);

            //add support for multiple cameras and camera switching
            cameraManager = new CameraManager(this);
            //add to Components otherwise no Update() called
            Components.Add(cameraManager);

            //big kahuna nr 1! this adds support to store, switch and Update() scene contents
            sceneManager = new SceneManager(this);
            //add to Components otherwise no Update()
            Components.Add(sceneManager);

            //big kahuna nr 2! this renders the ActiveScene from the ActiveCamera perspective
            renderManager = new RenderManager(this, new ForwardSceneRenderer(_graphics.GraphicsDevice));
            Components.Add(renderManager);

            //add support for playing sounds
            soundManager = new SoundManager();
            //why don't we add SoundManager to Components? Because it has no Update()
            //wait...SoundManager has no update? Yes, playing sounds is handled by an internal MonoGame thread - so we're off the hook!

            //add the physics manager update thread
            physicsManager = new PhysicsManager(this);
            Components.Add(physicsManager);

            //add state manager for inventory and countdown
            stateManager = new StateManager(this, AppData.MAX_GAME_TIME_IN_MSECS);
            Components.Add(stateManager);
        }

        private void InitializeDictionaries()
        {
            //TODO - add texture dictionary, soundeffect dictionary, model dictionary
        }

        private void InitializeDebug()
        {
            //intialize the utility component
            var perfUtility = new PerfUtility(this, _spriteBatch,
                new Vector2(10, 10),
                new Vector2(0, 22));

            //set the font to be used
            var spriteFont = Content.Load<SpriteFont>("Assets/Fonts/Perf");

            //add components to the info list to add UI information
            float headingScale = 1f;
            float contentScale = 0.9f;
            perfUtility.infoList.Add(new TextInfo(_spriteBatch, spriteFont, "Performance ------------------------------", Color.Yellow, headingScale * Vector2.One));
            perfUtility.infoList.Add(new FPSInfo(_spriteBatch, spriteFont, "FPS:", Color.White, contentScale * Vector2.One));
            perfUtility.infoList.Add(new TextInfo(_spriteBatch, spriteFont, "Camera -----------------------------------", Color.Yellow, headingScale * Vector2.One));
            perfUtility.infoList.Add(new CameraNameInfo(_spriteBatch, spriteFont, "Name:", Color.White, contentScale * Vector2.One));
            perfUtility.infoList.Add(new CameraPositionInfo(_spriteBatch, spriteFont, "Pos:", Color.White, contentScale * Vector2.One));
            perfUtility.infoList.Add(new CameraRotationInfo(_spriteBatch, spriteFont, "Rot:", Color.White, contentScale * Vector2.One));
            perfUtility.infoList.Add(new TextInfo(_spriteBatch, spriteFont, "Object -----------------------------------", Color.Yellow, headingScale * Vector2.One));
            perfUtility.infoList.Add(new ObjectInfo(_spriteBatch, spriteFont, "Objects:", Color.White, contentScale * Vector2.One));
            perfUtility.infoList.Add(new TextInfo(_spriteBatch, spriteFont, "Hints -----------------------------------", Color.Yellow, headingScale * Vector2.One));
            perfUtility.infoList.Add(new TextInfo(_spriteBatch, spriteFont, "Use mouse scroll wheel to change security camera FOV, F1-F4 for camera switch", Color.White, contentScale * Vector2.One));

            //add to the component list otherwise it wont have its Update or Draw called!
            Components.Add(perfUtility);
        }

        #endregion Actions - Engine Specific

        #region Actions - Update, Draw

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //update all drawn game objects in the active scene
            //sceneManager.Update(gameTime);

            //update active camera
            //cameraManager.Update(gameTime);
            
            //sceneManager.ActiveScene.Find(ObjectType.Dynamic, RenderType.Opaque, )
#if DEMO

            if (Input.Keys.WasJustPressed(Keys.B))
            {
                object[] parameters = { "boom1" };
                EventDispatcher.Raise(
                    new EventData(EventCategoryType.Player,
                    EventActionType.OnWin,
                    parameters));

                //    Application.SoundManager.Play2D("boom1");
            }

            #region Demo - Camera switching

            if (Input.Keys.IsPressed(Keys.F1))
                cameraManager.SetActiveCamera(AppData.FIRST_PERSON_CAMERA_NAME);
            else if (Input.Keys.IsPressed(Keys.F2))
                cameraManager.SetActiveCamera(AppData.SECURITY_CAMERA_NAME);
            else if (Input.Keys.IsPressed(Keys.F3))
                cameraManager.SetActiveCamera(AppData.CURVE_CAMERA_NAME);
            else if (Input.Keys.IsPressed(Keys.F4))
                cameraManager.SetActiveCamera(AppData.THIRD_PERSON_CAMERA_NAME);

            #endregion Demo - Camera switching

            #region Demo - Gamepad

            var thumbsL = Input.Gamepad.ThumbSticks(false);
            //   System.Diagnostics.Debug.WriteLine(thumbsL);

            var thumbsR = Input.Gamepad.ThumbSticks(false);
            //     System.Diagnostics.Debug.WriteLine(thumbsR);

            //    System.Diagnostics.Debug.WriteLine($"A: {Input.Gamepad.IsPressed(Buttons.A)}");

            #endregion Demo - Gamepad

            //this works for comparing path one and cube 1
            //Console.WriteLine($"tempCube1 =  {tempCube1.Transform.rotation}");
            
            PathChecker();

            #region Demo - Raising events using GDEvent

            if (Input.Keys.WasJustPressed(Keys.E))
                OnChanged.Invoke(this, null); //passing null for EventArgs but we'll make our own class MyEventArgs::EventArgs later

            #endregion

#endif
            //fixed a bug with components not getting Update called
            base.Update(gameTime);
        }

        private void PathChecker()
        {
            //Set up events for if the path is formed
            object[] buttonClick = { "smokeAlarm", new AudioListener(), new AudioEmitter() };
            EventData pathComplete = new EventData(
                        EventCategoryType.Sound,
                        EventActionType.OnPlay3D
                        , buttonClick);

            EventData pathInComplete = new EventData(
                        EventCategoryType.Sound,
                        EventActionType.OnPauseSound
                        , buttonClick);
            //EventDispatcher.EventHandlerDelegate del = new(pathComplete);

            //EventDispatcher.Subscribe(EventCategoryType.Sound, del);

            object[] boom = { "boom1", new AudioListener(), new AudioEmitter() };
            EventData tileInPosition = new EventData(
                        EventCategoryType.Sound,
                        EventActionType.OnPlay3D
                        , boom);

            //object[] pathCheck1 = { "pathCheck1", new AudioListener(), new AudioEmitter() };
            //EventData path1InPos = new EventData(
            //            EventCategoryType.Sound,
            //            EventActionType.OnPlay3D
            //            , pathCheck1);

            //object[] pathCheck2 = { "pathCheck2", new AudioListener(), new AudioEmitter() };
            //EventData path2InPos = new EventData(
            //            EventCategoryType.Sound,
            //            EventActionType.OnPlay3D
            //            , pathCheck2);

            //object[] patchCheck3 = { "pathCheck3", new AudioListener(), new AudioEmitter() };
            //EventData path3InPos = new EventData(
            //            EventCategoryType.Sound,
            //            EventActionType.OnPlay3D
            //            , patchCheck3);

            //object[] pathCheck4 = { "pathCheck4", new AudioListener(), new AudioEmitter() };
            //EventData path4InPos = new EventData(
            //            EventCategoryType.Sound,
            //            EventActionType.OnPlay3D
            //            , pathCheck4);

            //object[] pathCheck5 = { "pathCheck5", new AudioListener(), new AudioEmitter() };
            //EventData path5InPos = new EventData(
            //            EventCategoryType.Sound,
            //            EventActionType.OnPlay3D
            //            , pathCheck5);

            //object[] patchCheck6 = { "pathCheck6", new AudioListener(), new AudioEmitter() };
            //EventData path6InPos = new EventData(
            //            EventCategoryType.Sound,
            //            EventActionType.OnPlay3D
            //            , patchCheck6);

            EventData tileOutOfPosition = new EventData(
                        EventCategoryType.Sound,
                        EventActionType.OnPause
                        , boom);



            // Check if all paths are formed
            if (!temp.States.Contains(false) && temp.PathFormed == false)
            {
                EventDispatcher.Raise(pathComplete);
                temp.PathFormed = true;
            }
            else if(temp.States.Contains(false) && temp.PathFormed == true)
            {
                EventDispatcher.Raise(pathInComplete);
                temp.PathFormed = false;
            }

            if (tempCube1.Transform.rotation == temp.Pieces[0].rotation && temp.States[0] == false)
            {
                EventDispatcher.Raise(tileInPosition);
                temp.setState(true, 0);
            }

            else if (tempCube2.Transform.rotation == temp.Pieces[1].rotation && temp.States[1] == false)
            {
                EventDispatcher.Raise(tileInPosition);
                temp.setState(true, 1);
            }
            
            else if(tempCube3.Transform.rotation == temp.Pieces[2].rotation && temp.States[2] == false)
            {
                EventDispatcher.Raise(tileInPosition);
                temp.setState(true, 2);
            }
            
            else if(tempCube4.Transform.rotation == temp.Pieces[3].rotation && temp.States[3] == false)
            {
                EventDispatcher.Raise(tileInPosition);
                temp.setState(true, 3);
            }
            else if (tempCube1.Transform.rotation != temp.Pieces[0].rotation)
            {
                EventDispatcher.Raise(tileOutOfPosition);
                temp.setState(false, 0);
            }
            else if (tempCube2.Transform.rotation != temp.Pieces[1].rotation)
            {
                EventDispatcher.Raise(tileOutOfPosition);
                temp.setState(false, 1);
            }
            else if (tempCube3.Transform.rotation != temp.Pieces[2].rotation)
            {
                EventDispatcher.Raise(tileOutOfPosition);
                temp.setState(false, 2);
            }
            else if (tempCube4.Transform.rotation != temp.Pieces[3].rotation)
            {
                EventDispatcher.Raise(tileOutOfPosition);
                temp.setState(false, 3);
            }
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //get active scene, get camera, and call the draw on the active scene
            //sceneManager.ActiveScene.Draw(gameTime, cameraManager.ActiveCamera);

            base.Draw(gameTime);
        }

        #endregion Actions - Update, Draw
    }
}