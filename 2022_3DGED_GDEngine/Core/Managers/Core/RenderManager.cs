using GD.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GD.Engine.Managers
{
    /// <summary>
    /// Renders the active scene
    /// </summary>
    public class RenderManager : PausableDrawableGameComponent
    {
        #region Fields

        private SceneRenderer sceneRenderer;

        #endregion Fields

        #region Properties

        public SceneRenderer SceneRenderer { get => sceneRenderer; set => sceneRenderer = value; }

        #endregion Properties

        #region Constructors

        public RenderManager(Game game, SceneRenderer sceneRenderer) : base(game)
        {
            this.sceneRenderer = sceneRenderer;
        }

        #endregion Constructors

        public override void Draw(GameTime gameTime)
        {
            if (StatusType != StatusType.Off)
            {
                if (sceneRenderer == null)
                    throw new ArgumentNullException("SceneRenderer is null! Use RenderManager::SceneRenderer to set renderer for the scene in Main::InitializeEngine()");

                sceneRenderer.Draw(Application.GraphicsDevice,
                    Application.CameraManager.ActiveCamera,
                    Application.SceneManager.ActiveScene);
            }
        }
    }

    public class SceneRenderer
    {
        private RasterizerState rasterizerStateOpaque;
        private RasterizerState rasterizerStateTransparent;

        /// <summary>
        /// Call to initialize SceneRenderer - not using this just yet!
        /// </summary>
        public virtual void Initialize(GraphicsDevice graphiceDevice)
        {
            //set the graphics card to repeat the end pixel value for any UV value outside 0-1
            //See http://what-when-how.com/xna-game-studio-4-0-programmingdeveloping-for-windows-phone-7-and-xbox-360/samplerstates-xna-game-studio-4-0-programming/
            SamplerState samplerState = new SamplerState();
            samplerState.AddressU = TextureAddressMode.Mirror;
            samplerState.AddressV = TextureAddressMode.Mirror;
            graphiceDevice.SamplerStates[0] = samplerState;

            //opaque objects
            rasterizerStateOpaque = new RasterizerState();
            rasterizerStateOpaque.CullMode = CullMode.CullCounterClockwiseFace;

            //transparent objects
            rasterizerStateTransparent = new RasterizerState();
            rasterizerStateTransparent.CullMode = CullMode.None;

            //Remember this code from our initial aliasing problems with the Sky box?
            //enable anti-aliasing along the edges of the quad i.e. to remove jagged edges to the primitive
            graphiceDevice.SamplerStates[0] = SamplerState.LinearClamp;
        }

        /// <summary>
        /// Sets GFX states (i.e., CullMode, enable/disable Depth buffer) for transparent and opaque objects
        /// </summary>
        /// <param name="isOpaque"></param>
        public virtual void SetGraphicsStates(bool isOpaque)
        {
            if (isOpaque)
            {
                //set the appropriate state for opaque objects
                Application.GraphicsDevice.RasterizerState = rasterizerStateOpaque;

                //disable to see what happens when we disable depth buffering - look at the boxes
                Application.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            }
            else
            {
                //set the appropriate state for transparent objects
                Application.GraphicsDevice.RasterizerState = rasterizerStateTransparent;

                //enable alpha blending for transparent objects i.e. trees
                Application.GraphicsDevice.BlendState = BlendState.AlphaBlend;

                //disable to see what happens when we disable depth buffering - look at the boxes
                Application.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
            }
        }

        /// <summary>
        /// Renders (i.e., calls a Draw() on each of the drawble GameObjects) in the active scene
        /// </summary>
        /// <param name="graphicsDevice">Handle the GFX card</param>
        /// <param name="camera">Active camera in the game</param>
        /// <param name="scene">Active scene in the game</param>
        public virtual void Draw(GraphicsDevice graphicsDevice, Camera camera, Scene scene)
        {
        }
    }
}