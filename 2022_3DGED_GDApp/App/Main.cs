#region Pre-compiler directives

#define DEMO
#define SHOW_DEBUG_INFO

#endregion

using GD.Engine;
using GD.Engine.Collections;
using GD.Engine.Events;
using GD.Engine.Globals;
using GD.Engine.Inputs;
using GD.Engine.Managers;
using GD.Engine.Parameters;
using GD.Engine.Utilities;
using JigLibX.Collision;
using JigLibX.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Media;
using Application = GD.Engine.Globals.Application;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Cue = GD.Engine.Managers.Cue;
using Keys = Microsoft.Xna.Framework.Input.Keys;

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
        private SceneManager<Scene> sceneManager;
        private SoundManager soundManager;
        private PhysicsManager physicsManager;
        private RenderManager renderManager;
        private EventDispatcher eventDispatcher;
        private GameObject playerGameObject;
        private StateManager stateManager;
        private GameObject uiTextureGameObject;
        /*
         * private SpriteMaterial textSpriteMaterial; -----> now Renderer2D
         * private UITextureElement uiTextureElement; -----> replaced or redundant 
         */
        private SceneManager<Scene2D> uiManager;
        private Render2DManager uiRenderManager;

        private Path temp;
        private GameObject tempCube1;
        private GameObject tempCube2;
        private GameObject tempCube3;
        private GameObject tempCube4;
        private GameObject tempCube5;
        private GameObject tempCube6;
        private GameObject tempCube7;
        private GameObject tempCube8;
        private GameObject tempCube9;
        private GameObject tempCube10;
        private GameObject tempCube11;
        private GameObject tempCube12;
        private GameObject tempCube13;
        private GameObject tempCube14;
        private GameObject tempCube15;
        private GameObject tempCube16;
        private GameObject tempCube17;
        private GameObject tempCube18;
        private GameObject tempCube19;
        private GameObject tempCube20;
        private GameObject tempCube21;
        private GameObject tempCube22;
        private GameObject tempCube23;
        private GameObject tempCube24;
        private GameObject tempCube25;

        private Transform[,] storeCubes = new Transform[5, 5];


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

            //object[] parameters = {"sound name", audioListener, audioEmitter};

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
            object[] parameters = { "epic_soundcue" };
            EventDispatcher.Raise(
                new EventData(EventCategoryType.Player,
                EventActionType.OnSpawnObject,
                parameters));

            //Application.SoundManager.Play2D("startupline");

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

            #region Console_Dialouge
            SoundEffect startUpLine = Content.Load<SoundEffect>("Assets/Audio/Console_Dialouge/Start_Up");
            soundManager.Add(new Cue(
                "startupline",
                startUpLine,
                SoundCategoryType.Alarm,
                new Vector3(0.1f, 0.1f, 0.1f),
                false));

            //SoundEffect checkingTerminal = Content.Load<SoundEffect>("Assets/Audio/Console_Dialouge/Terminal");
            //soundManager.Add(new Cue(
            //    "checkingterminal",
            //    startUpLine,
            //    SoundCategoryType.Alarm,
            //    new Vector3(0.1f, 0.1f, 0.1f),
            //    false));
            #endregion
            var soundEffect =
                Content.Load<SoundEffect>("Assets/Audio/Diegetic/explode1");

            //add the new sound effect
            soundManager.Add(new Cue(
                "boom1",
                soundEffect,
                SoundCategoryType.Alarm,
                new Vector3(1, 1, 0),
                false));

            var incorrectNumberEffect =
                Content.Load<SoundEffect>("Assets/Audio/Diegetic/IncorrectNumber");

            //add the new sound effect
            soundManager.Add(new Cue(
                "incorrectNumber",
                incorrectNumberEffect,
                SoundCategoryType.Alarm,
                new Vector3(1, 1, 0),
                false));

            var cardDropEffect =
                Content.Load<SoundEffect>("Assets/Audio/Diegetic/CardDrop");

            //add the new sound effect
            soundManager.Add(new Cue(
                "CardDrop",
                cardDropEffect,
                SoundCategoryType.Alarm,
                new Vector3(1, 1, 0),
                false));

            var buttonClickExplosionEffect =
                Content.Load<SoundEffect>("Assets/Audio/Diegetic/Button_click_explosion");

            //add the new sound effect
            soundManager.Add(new Cue(
                "buttonClickExplosion",
                buttonClickExplosionEffect,
                SoundCategoryType.Alarm,
                new Vector3(1, 1, 0),
                false));

            var cardPickupOrSwipeEffect =
                Content.Load<SoundEffect>("Assets/Audio/Diegetic/Card pickup or swipe");

            //add the new sound effect
            soundManager.Add(new Cue(
                "cardPickupOrSwipe",
                cardPickupOrSwipeEffect,
                SoundCategoryType.Alarm,
                new Vector3(1, 1, 0),
                false));

            var dialTwistingEffect =
                Content.Load<SoundEffect>("Assets/Audio/Diegetic/Dial Twisting");

            //add the new sound effect
            soundManager.Add(new Cue(
                "dialTwisting",
                dialTwistingEffect,
                SoundCategoryType.Alarm,
                new Vector3(1, 1, 0),
                false));

            var externalButtonPressEffect =
                Content.Load<SoundEffect>("Assets/Audio/Diegetic/ExternalButtonPress");

            //add the new sound effect
            soundManager.Add(new Cue(
                "externalButtonPress",
                externalButtonPressEffect,
                SoundCategoryType.Alarm,
                new Vector3(1, 1, 0),
                false));

            //var errorEffect =
            //    Content.Load<SoundEffect>("Assets/Audio/Diegetic/Error");

            ////add the new sound effect
            //soundManager.Add(new Cue(
            //    "Error",
            //    errorEffect,
            //    SoundCategoryType.Alarm,
            //    new Vector3(1, 1, 0),
            //    false));

            var keyboardPressingEffect =
                Content.Load<SoundEffect>("Assets/Audio/Diegetic/KeyboardPressing");

            //add the new sound effect
            soundManager.Add(new Cue(
                "keyboardPressing",
                keyboardPressingEffect,
                SoundCategoryType.Alarm,
                new Vector3(1, 1, 0),
                false));

            var numPadEffect =
                Content.Load<SoundEffect>("Assets/Audio/Diegetic/NumPad");

            //add the new sound effect
            soundManager.Add(new Cue(
                "numPad",
                numPadEffect,
                SoundCategoryType.Alarm,
                new Vector3(1, 1, 0),
                false));

            var startUpBeepEffect =
                Content.Load<SoundEffect>("Assets/Audio/Diegetic/StartUpBeep");

            //add the new sound effect
            soundManager.Add(new Cue(
                "startUpBeep",
                startUpBeepEffect,
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
            InitialiseButtonCollider();
            
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
                AppData.PLAYER_ROTATE_SPEED_SINGLE, AppData.FIRST_PERSON_CAMERA_SMOOTH_FACTOR));

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
            var gdBasicEffect = new GDBasicEffect(unlitEffect);
            var quadMesh = new QuadMesh(_graphics.GraphicsDevice);

            //ground
            var ground = new GameObject("ground");
            ground.Transform = new Transform(new Vector3(worldScale, worldScale, 1),
                new Vector3(-90, 0, 0), new Vector3(0, 0, 0));
            var texture = Content.Load<Texture2D>("Assets/Textures/Foliage/Ground/grass1");
            ground.AddComponent(new Renderer(gdBasicEffect, new Material(texture, 1), quadMesh));

            //add Collision Surface(s)
            var collider = new Collider(ground);
            collider.AddPrimitive(new JigLibX.Geometry.Box(
                    ground.Transform.Translation,
                    ground.Transform.Rotation,
                    ground.Transform.Scale),
                new MaterialProperties(0.8f, 0.8f, 0.7f));
            collider.Enable(ground, true, 1);
            ground.AddComponent(collider);

            sceneManager.ActiveScene.Add(ground);
        }
        
        private void InitializeCollidableModel()
        {
            //game object
            // var gameObject = new GameObject("my first collidable box!", ObjectType.Static, RenderType.Opaque);
            //
            // gameObject.Transform = new Transform(null, null, new Vector3(0, 4, 0));
            // var texture = Content.Load<Texture2D>("Assets/Textures/Props/Crates/crate2");
            // var model = Content.Load<Model>("Assets/Models/cube");
            // var mesh = new Engine.ModelMesh(_graphics.GraphicsDevice, model);
            //
            // gameObject.AddComponent(new Renderer(
            //     new GDBasicEffect(litEffect),
            //     new Material(texture, 1f, Color.White),
            //     mesh));
            //
            // gameObject.AddComponent(new BoxCollider(new Vector3(0, 10, 0),
            //     1, 1, 1, 10));
            //
            // sceneManager.ActiveScene.Add(gameObject);
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
            Renderer2D render2D = null;
            buttonGameObject.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(buttonTexture, 1), buttonMesh));
            buttonGameObject.AddComponent(new ButtonController());
            //var buttonCollider = new Collider(buttonGameObject);
            //buttonCollider.AddPrimitive(new Box(
            //    buttonGameObject.Transform.Translation,
            //    buttonGameObject.Transform.Rotation,
            //    buttonGameObject.Transform.Scale),new MaterialProperties(0.8f,0.8f,0.7f));
            //buttonCollider.transform.SetTranslation(new Vector3(-1,-1,-1));
            //buttonGameObject.AddComponent(buttonCollider);
            sceneManager.ActiveScene.Add(buttonGameObject);
        }

        private void InitialiseButtonCollider()
        {
            var gameObject = new GameObject("Button collidable box", ObjectType.Dynamic, RenderType.Opaque);
            gameObject.Transform = new Transform(Vector3.One, Vector3.Zero, Vector3.Zero);

            //var collider = new ButtonCollider2D(gameObject,new Renderer2D(new TextureMaterial2D(Content.Load<Texture2D>("Assets/Textures/console/keyboard_base_colour"))))

            Collider collider = new Collider(gameObject, true);
            collider.AddPrimitive(new Box(gameObject.Transform.Translation, gameObject.Transform.Rotation, new Vector3(.2f,.2f,.2f)), new MaterialProperties(0.08f, 0.08f, 0.07f));

            collider.transform.SetTranslation(new Vector3(0.87f,1.6f,.3f)); //change x axis 
            collider.Enable(gameObject, true, 1);
            gameObject.AddComponent(collider);

            sceneManager.ActiveScene.Add(gameObject);
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
            var panelTexture = Content.Load<Texture2D>("Assets/Textures/cube_DefaultMaterial_BaseColor");
            var panelFbxModel = Content.Load<Model>("Assets/Models/cube");
            var panelMesh = new Engine.ModelMesh(_graphics.GraphicsDevice, panelFbxModel);
            var myRender = new Renderer2D(new TextureMaterial2D(panelTexture,Color.Aqua));


            //string nameOfcube = "";

            ////Row 1
            //for (int i = 0; i < 5; i++)
            //{
            //    float incrementY = 2 + (-0.04f * i);
            //    nameOfcube = "cube" + i.ToString();
            //    for (int j = 0; j < 5; j++)
            //    {
            //        float increment = -0.16f + (0.08f * j);
            //        tempCube1 = new GameObject(nameOfcube + j.ToString(), ObjectType.Dynamic, RenderType.Opaque);
            //        tempCube1.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, incrementY, increment));
            //        tempCube1.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            //        //Our button collision for when a mouse clicks on a cube
            //        var myrectangle = new ButtonCollider2D(tempCube1, myRender);
            //        myrectangle.AddEvent(MouseButton.Left, new EventData(EventCategoryType.Pickup, EventActionType.OnClick));
            //        tempCube1.AddComponent(myrectangle);
            //        ////////////////////////////////////////
            //        //var collisionMesh = new Collider(tempCube1);
            //        //collisionMesh.AddPrimitive(
            //        //    new Box(tempCube1.Transform.Translation,
            //        //            tempCube1.Transform.Rotation,
            //        //            new Vector3(0.03f,0.03f,0.03f)),
            //        //            new MaterialProperties(0.8f, 0.8f, 0.7f));
            //        //collisionMesh.Enable(tempCube1, true, 1);
            //        //tempCube1.AddComponent(collisionMesh);
            //        //Store the cubes
            //        storeCubes[i, j] = tempCube1.Transform;
            //        sceneManager.ActiveScene.Add(tempCube1);
            //    }

            //    tempCube1.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad1 + i));
            //}


            #region The Grid(Look risk to ones eyes)

            tempCube1 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube1.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 2, 0.16f));
            tempCube1.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube1.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.A));

            tempCube2 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube2.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 2, 0.08f));
            tempCube2.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube2.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.S));

            tempCube3 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube3.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, MathHelper.PiOver2), new Vector3(1, 2, 0));
            tempCube3.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube3.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.D));

            tempCube4 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube4.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, 2, -0.08f));
            tempCube4.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube4.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.F));

            tempCube5 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube5.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, 2, -0.16f));
            tempCube5.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube5.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.G));
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////

            tempCube6 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube6.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 2.04f, 0.16f));
            tempCube6.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube6.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.Q));

            tempCube7 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube7.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 2.04f, 0.08f));
            tempCube7.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube7.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.W));

            tempCube8 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube8.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, MathHelper.PiOver2), new Vector3(1, 2.04f, 0));
            tempCube8.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube8.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.E));

            tempCube9 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube9.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, 2.04f, -0.08f));
            tempCube9.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube9.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.R));

            tempCube10 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube10.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, 2.04f, -0.16f));
            tempCube10.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube10.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.T));
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            tempCube11 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube11.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 1.96f, 0.16f));
            tempCube11.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube11.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.Z));

            tempCube12 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube12.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 1.96f, 0.08f));
            tempCube12.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube12.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.X));

            tempCube13 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube13.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, MathHelper.PiOver2), new Vector3(1, 1.96f, 0));
            tempCube13.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube13.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.C));

            tempCube14 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube14.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, 1.96f, -0.08f));
            tempCube14.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube14.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.V));

            tempCube15 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube15.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, 1.96f, -0.16f));
            tempCube15.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube15.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.B));
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            tempCube16 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube16.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 1.92f, 0.16f));
            tempCube16.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube16.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad0));

            tempCube17 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube17.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 1.92f, 0.08f));
            tempCube17.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube17.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad1));

            tempCube18 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube18.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, MathHelper.PiOver2), new Vector3(1, 1.92f, 0));
            tempCube18.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube18.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad2));

            tempCube19 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube19.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, 1.92f, -0.08f));
            tempCube19.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube19.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad3));

            tempCube20 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube20.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, 1.92f, -0.16f));
            tempCube20.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube20.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad4));
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            tempCube21 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube21.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 1.87f, 0.16f));
            tempCube21.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube21.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad5));

            tempCube22 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube22.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2), new Vector3(1, 1.87f, 0.08f));
            tempCube22.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube22.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad6));

            tempCube23 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube23.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, MathHelper.PiOver2), new Vector3(1, 1.87f, 0));
            tempCube23.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube23.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad7));

            tempCube24 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube24.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, 1.87f, -0.08f));
            tempCube24.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube24.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad8));

            tempCube25 = new GameObject(AppData.CUBE_NAME, ObjectType.Dynamic, RenderType.Opaque);
            tempCube25.Transform = new Transform(AppData.CUBE_SCALE, new Vector3(0, 0, -MathHelper.PiOver2 * 2), new Vector3(1, 1.87f, -0.16f));
            tempCube25.AddComponent(new Renderer(new GDBasicEffect(litEffect), new Material(panelTexture, 1), panelMesh));
            tempCube25.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), Keys.NumPad9));

            sceneManager.ActiveScene.Add(tempCube1);
            sceneManager.ActiveScene.Add(tempCube2);
            sceneManager.ActiveScene.Add(tempCube3);
            sceneManager.ActiveScene.Add(tempCube4);
            sceneManager.ActiveScene.Add(tempCube5);
            sceneManager.ActiveScene.Add(tempCube6);
            sceneManager.ActiveScene.Add(tempCube7);
            sceneManager.ActiveScene.Add(tempCube8);
            sceneManager.ActiveScene.Add(tempCube9);
            sceneManager.ActiveScene.Add(tempCube10);
            sceneManager.ActiveScene.Add(tempCube11);
            sceneManager.ActiveScene.Add(tempCube12);
            sceneManager.ActiveScene.Add(tempCube13);
            sceneManager.ActiveScene.Add(tempCube14);
            sceneManager.ActiveScene.Add(tempCube15);
            sceneManager.ActiveScene.Add(tempCube16);
            sceneManager.ActiveScene.Add(tempCube17);
            sceneManager.ActiveScene.Add(tempCube18);
            sceneManager.ActiveScene.Add(tempCube19);
            sceneManager.ActiveScene.Add(tempCube20);
            sceneManager.ActiveScene.Add(tempCube21);
            sceneManager.ActiveScene.Add(tempCube22);
            sceneManager.ActiveScene.Add(tempCube23);
            sceneManager.ActiveScene.Add(tempCube24);
            sceneManager.ActiveScene.Add(tempCube25);
            #endregion

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
            var texture = Content.Load<Texture2D>("Assets/Textures/Skybox/basicwall");

            //skybox - back face
            quad = new GameObject("skybox back face");
            quad.Transform = new Transform(new Vector3(worldScale, worldScale, 1), null, new Vector3(0, 0, -halfWorldScale));
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
            quad.Transform = new Transform(new Vector3(worldScale, worldScale, 1), new Vector3(MathHelper.ToRadians(-90), 0, 0), new Vector3(0, -halfWorldScale, 0));
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
            sceneManager = new SceneManager<Scene>(this);
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
            physicsManager = new PhysicsManager(this,AppData.GRAVITY);
            Components.Add(physicsManager);

            //add state manager for inventory and countdown
            stateManager = new StateManager(this, AppData.MAX_GAME_TIME_IN_MSECS);
            Components.Add(stateManager);
        }

        private void InitializeDictionaries()
        {
            //TODO - add texture dictionary, soundeffect dictionary, model dictionary
        }

        private void InitializeDebug(bool showCollisionSkins = true)
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

            var infoFunction = (Transform transform) =>
            {
                return transform.Translation.GetNewRounded(1).ToString();
            };

            perfUtility.infoList.Add(new TransformInfo(_spriteBatch, spriteFont, "Pos:", Color.White, contentScale * Vector2.One,
                ref Application.CameraManager.ActiveCamera.transform, infoFunction));

            infoFunction = (Transform transform) =>
            {
                return transform.Rotation.GetNewRounded(1).ToString();
            };

            perfUtility.infoList.Add(new TransformInfo(_spriteBatch, spriteFont, "Rot:", Color.White, contentScale * Vector2.One,
                ref Application.CameraManager.ActiveCamera.transform, infoFunction));

            perfUtility.infoList.Add(new TextInfo(_spriteBatch, spriteFont, "Object -----------------------------------", Color.Yellow, headingScale * Vector2.One));
            perfUtility.infoList.Add(new ObjectInfo(_spriteBatch, spriteFont, "Objects:", Color.White, contentScale * Vector2.One));
            perfUtility.infoList.Add(new TextInfo(_spriteBatch, spriteFont, "Hints -----------------------------------", Color.Yellow, headingScale * Vector2.One));
            perfUtility.infoList.Add(new TextInfo(_spriteBatch, spriteFont, "Use mouse scroll wheel to change security camera FOV, F1-F4 for camera switch", Color.White, contentScale * Vector2.One));

            //add to the component list otherwise it wont have its Update or Draw called!
            // perfUtility.StatusType = StatusType.Drawn | StatusType.Updated;
            perfUtility.DrawOrder = 3;
            Components.Add(perfUtility);

            if (showCollisionSkins)
            {
                var physicsDebugDrawer = new PhysicsDebugDrawer(this);
                physicsDebugDrawer.DrawOrder = 4;
                Components.Add(physicsDebugDrawer);
            }
        }

        #endregion Actions - Engine Specific

        #region Actions - Update, Draw

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Delete))
            {
                sceneManager.ActiveScene.Remove(tempCube1.ObjectType,tempCube1.RenderType,o => tempCube1.Name.Equals(AppData.CUBE_NAME) );
                sceneManager.ActiveScene.Remove(tempCube2.ObjectType,tempCube2.RenderType,o => tempCube2.Name.Equals(AppData.CUBE_NAME) );
                sceneManager.ActiveScene.Remove(tempCube3.ObjectType,tempCube3.RenderType,o => tempCube3.Name.Equals(AppData.CUBE_NAME) );
                sceneManager.ActiveScene.Remove(tempCube4.ObjectType,tempCube4.RenderType,o => tempCube4.Name.Equals(AppData.CUBE_NAME) );
                
                InitializeCube();
            }

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

               
                //Application.SoundManager.Play2D("boom1");
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
            if (!temp.States.Contains(false))
            {
                Application.SoundManager.Play2D("alarm");
            }
            else
            {
                Application.SoundManager.Stop("alarm");
            }
            //for (int i = 0; i <= temp.Size-1; i++)
            //{
            //    if (cubes[i].Transform.rotation == temp.Pieces[i].rotation && temp.States[i] == false)
            //    {
            //        Application.SoundManager.Play2D("boom1");
            //        temp.setState(true, i);
            //    }
            //    else if(cubes[i].Transform.rotation != temp.Pieces[i].rotation)
            //    {
            //        temp.setState(false, i);
            //    }
            //}
            if (tempCube1.Transform.Rotation == temp.Pieces[0].Rotation && temp.States[0] == false)
            {
                Application.SoundManager.Play2D("boom1");
                temp.setState(true, 0);
            }
            else if (tempCube1.Transform.Rotation != temp.Pieces[0].Rotation && temp.States[0] == true)
            {
                temp.setState(false, 0);
            }

            else if (tempCube2.Transform.Rotation == temp.Pieces[1].Rotation && temp.States[1] == false)
            {
                Application.SoundManager.Play2D("boom1");
                temp.setState(true, 1);
            }

            else if (tempCube2.Transform.Rotation != temp.Pieces[1].Rotation && temp.States[0] == true)
            {
                temp.setState(false, 1);
            }

            else if (tempCube3.Transform.Rotation == temp.Pieces[2].Rotation && temp.States[2] == false)
            {
                Application.SoundManager.Play2D("boom1");
                temp.setState(true, 2);
            }

            else if (tempCube3.Transform.Rotation != temp.Pieces[2].Rotation && temp.States[0] == true)
            {
                temp.setState(false, 2);
            }

            else if (tempCube4.Transform.Rotation == temp.Pieces[3].Rotation && temp.States[3] == false)
            {
                Application.SoundManager.Play2D("boom1");
                temp.setState(true, 3);
            }

            else if (tempCube4.Transform.Rotation != temp.Pieces[3].Rotation && temp.States[0] == true)
            {
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