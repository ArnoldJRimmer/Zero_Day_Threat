using GD.Engine.Globals;
using JigLibX.Collision;
using JigLibX.Geometry;
using JigLibX.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using PrimitiveType = Microsoft.Xna.Framework.Graphics.PrimitiveType;

namespace GD.Engine.Managers
{
    /// <summary>
    /// Sums all forces and torques for all collidable bodies in the world
    /// </summary>
    public class PhysicsController : Controller
    {
        #region Properties

        public enum CoordinateSystem
        {
            WorldCoordinates = 0,
            LocalCoordinates = 1
        }

        public struct Force
        {
            public CoordinateSystem coordinateSystem;
            public Vector3 force;
            public Vector3 position;
            public Body body;
        }

        public struct Torque
        {
            public CoordinateSystem coordinateSystem;
            public Vector3 torque;
            public Body body;
        }

        public Queue<Force> forces = new Queue<Force>();

        internal Queue<Force> Forces
        { get { return forces; } }

        public Queue<Torque> torques = new Queue<Torque>();

        internal Queue<Torque> Torques
        { get { return torques; } }

        #endregion Properties

        #region Actions - Update

        public override void UpdateController(float elapsedTime)
        {
            // Apply pending forces
            while (forces.Count > 0)
            {
                Force force = forces.Dequeue();
                switch (force.coordinateSystem)
                {
                    case CoordinateSystem.LocalCoordinates:
                        {
                            force.body.AddBodyForce(force.force, force.position);
                        }
                        break;

                    case CoordinateSystem.WorldCoordinates:
                        {
                            force.body.AddWorldForce(force.force, force.position);
                        }
                        break;
                }
            }

            // Apply pending torques
            while (torques.Count > 0)
            {
                Torque torque = torques.Dequeue();
                switch (torque.coordinateSystem)
                {
                    case CoordinateSystem.LocalCoordinates:
                        {
                            torque.body.AddBodyTorque(torque.torque);
                        }
                        break;

                    case CoordinateSystem.WorldCoordinates:
                        {
                            torque.body.AddWorldTorque(torque.torque);
                        }
                        break;
                }
            }
        }

        #endregion Actions - Update
    }

    public class PhysicsManager : PausableGameComponent
    {
        #region Statics

        private static readonly Vector3 GRAVITY = new Vector3(0, -9.81f, 0);

        #endregion Statics

        #region Fields

        private PhysicsSystem physicSystem;
        private PhysicsController physCont;
        private float timeStep = 0;

        #endregion Fields

        #region Properties

        public PhysicsSystem PhysicsSystem
        {
            get
            {
                return physicSystem;
            }
        }

        public PhysicsController PhysicsController
        {
            get
            {
                return physCont;
            }
        }

        #endregion Properties

        #region Constructors

        //user-defined gravity
        public PhysicsManager(Game game, Vector3 gravity)
            : base(game)
        {
            physicSystem = new PhysicsSystem();

            //add cd/cr system
            physicSystem.CollisionSystem = new CollisionSystemSAP();

            //allows us to define the direction and magnitude of gravity - default is (0, -9.8f, 0)
            physicSystem.Gravity = gravity;

            //prevents bug where objects would show correct CDCR response when velocity == Vector3.Zero
            physicSystem.EnableFreezing = true;

            physicSystem.SolverType = PhysicsSystem.Solver.Normal;
            physicSystem.CollisionSystem.UseSweepTests = true;

            //affect accuracy and the overhead == time required
            physicSystem.NumCollisionIterations = 4;
            physicSystem.NumContactIterations = 8;
            physicSystem.NumPenetrationRelaxationTimesteps = 15;

            #region SETTING_COLLISION_ACCURACY

            //affect accuracy of the collision detection
            physicSystem.AllowedPenetration = 0.00025f;
            physicSystem.CollisionTollerance = 0.0005f;

            #endregion SETTING_COLLISION_ACCURACY

            physCont = new PhysicsController();
            physicSystem.AddController(physCont);
        }

        #endregion Constructors

        #region Actions - Update

        public override void Update(GameTime gameTime)
        {
            if (IsUpdated)
            {
                //TODO - change to Time.Instance
                timeStep = (float)gameTime.ElapsedGameTime.Ticks / System.TimeSpan.TicksPerSecond;

                //if the time between updates indicates a FPS of close to 60 fps or less then update CD/CR engine
                if (timeStep < 1.0f / 60.0f)
                {
                    physicSystem.Integrate(timeStep);
                }
                else
                {
                    //else fix at 60 updates per second
                    physicSystem.Integrate(1.0f / 60.0f);
                }
            }
        }

        #endregion Actions - Update
    }

    public class PhysicsDebugDrawer : PausableDrawableGameComponent
    {
        private Color collisionSkinColor;
        private BasicEffect effect;
        private RasterizerState rasterizerStateOpaque;
        private RasterizerState rasterizerStateTransparent;

        //temps
        private List<VertexPositionColor> vertexData;

        private VertexPositionColor[] wf;

        public PhysicsDebugDrawer(Game game)
            : base(game)
        {
            collisionSkinColor = Color.Red;
            vertexData = new List<VertexPositionColor>();
            effect = new BasicEffect(Application.GraphicsDevice);
            effect.AmbientLightColor = Vector3.One;
            effect.VertexColorEnabled = true;

            Initialize();
        }

        public override void Initialize()
        {
            //set the graphics card to repeat the end pixel value for any UV value outside 0-1
            //See http://what-when-how.com/xna-game-studio-4-0-programmingdeveloping-for-windows-phone-7-and-xbox-360/samplerstates-xna-game-studio-4-0-programming/
            SamplerState samplerState = new SamplerState();
            samplerState.AddressU = TextureAddressMode.Mirror;
            samplerState.AddressV = TextureAddressMode.Mirror;
            Application.GraphicsDevice.SamplerStates[0] = samplerState;

            //opaque objects
            rasterizerStateOpaque = new RasterizerState();
            rasterizerStateOpaque.CullMode = CullMode.CullCounterClockwiseFace;

            //transparent objects
            rasterizerStateTransparent = new RasterizerState();
            rasterizerStateTransparent.CullMode = CullMode.None;

            //Remember this code from our initial aliasing problems with the Sky box?
            //enable anti-aliasing along the edges of the quad i.e. to remove jagged edges to the primitive
            Application.GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
        }

        public override void Draw(GameTime gameTime)
        {
            if (StatusType != StatusType.Off)
            {
                SetGraphicsStates(true);

                //draw static opaque collision surfaces
                var colliders = Application.SceneManager.ActiveScene.OpaqueList.StaticList.Colliders;
                foreach (Collider collider in colliders)
                    AddCollisionSkinVertexData(collider);

                //draw dynamic opaque collision surfaces
                colliders = Application.SceneManager.ActiveScene.OpaqueList.DynamicList.Colliders;
                foreach (Collider collider in colliders)
                    AddCollisionSkinVertexData(collider);

                //draw static transparent collision surfaces
                colliders = Application.SceneManager.ActiveScene.TransparentList.StaticList.Colliders;
                foreach (Collider collider in colliders)
                    AddCollisionSkinVertexData(collider);

                //draw dynamic transparent collision surfaces
                colliders = Application.SceneManager.ActiveScene.TransparentList.DynamicList.Colliders;
                foreach (Collider collider in colliders)
                    AddCollisionSkinVertexData(collider);

                //no vertices to draw - would happen if we forget to call DrawCollisionSkins() above or there were no drawn objects to see!
                if (vertexData.Count == 0)
                    return;

                //draw skin
                Game.GraphicsDevice.Viewport = Application.CameraManager.ActiveCamera.ViewPort;
                effect.View = Application.CameraManager.ActiveCamera.View;
                effect.Projection = Application.CameraManager.ActiveCamera.Projection;
                effect.CurrentTechnique.Passes[0].Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, vertexData.ToArray(), 0, vertexData.Count - 1);

                //reset data
                vertexData.Clear();
            }
        }

        public void AddVertexDataForShape(List<Vector3> shape, Color color)
        {
            if (vertexData.Count > 0)
            {
                Vector3 v = vertexData[vertexData.Count - 1].Position;
                vertexData.Add(new VertexPositionColor(v, color));
                vertexData.Add(new VertexPositionColor(shape[0], color));
            }

            foreach (Vector3 p in shape)
            {
                vertexData.Add(new VertexPositionColor(p, color));
            }
        }

        public void AddVertexDataForShape(List<Vector3> shape, Color color, bool closed)
        {
            AddVertexDataForShape(shape, color);

            Vector3 v = shape[0];
            vertexData.Add(new VertexPositionColor(v, color));
        }

        public void AddVertexDataForShape(List<VertexPositionColor> shape, Color color)
        {
            if (vertexData.Count > 0)
            {
                Vector3 v = vertexData[vertexData.Count - 1].Position;
                vertexData.Add(new VertexPositionColor(v, color));
                vertexData.Add(new VertexPositionColor(shape[0].Position, color));
            }

            foreach (VertexPositionColor vps in shape)
            {
                vertexData.Add(vps);
            }
        }

        public void AddVertexDataForShape(VertexPositionColor[] shape, Color color)
        {
            if (vertexData.Count > 0)
            {
                Vector3 v = vertexData[vertexData.Count - 1].Position;
                vertexData.Add(new VertexPositionColor(v, color));
                vertexData.Add(new VertexPositionColor(shape[0].Position, color));
            }

            foreach (VertexPositionColor vps in shape)
            {
                vertexData.Add(vps);
            }
        }

        public void AddVertexDataForShape(List<VertexPositionColor> shape, Color color, bool closed)
        {
            AddVertexDataForShape(shape, color);

            VertexPositionColor v = shape[0];
            vertexData.Add(v);
        }

        public void AddCollisionSkinVertexData(Collider collider)
        {
            //         if (collider.GameObject.GameObjectType != GameObjectType.Ground)
            {
                wf = collider.Collision.GetLocalSkinWireframe();

                // if the collision skin was also added to the body
                // we have to transform the skin wireframe to the body space
                if (collider.Body.CollisionSkin != null)
                {
                    collider.Body.TransformWireframe(wf);
                }

                AddVertexDataForShape(wf, collisionSkinColor);
            }
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
    }

    public class CollisionUtility
    {
        private static Vector2 leftBottom;
        private static Vector2 leftTop;
        private static Vector2 max;
        private static Vector2 min;
        private static Vector2 rightBottom;
        private static Vector2 rightTop;
        private static Matrix worldMatrix;

        /// <summary>
        /// Calculates an axis aligned rectangle which fully contains an arbitrarily transformed axis aligned rectangle.
        /// </summary>
        /// <param name="rectangle">Original bounding rectangle.</param>
        /// <param name="transform">World transform of the rectangle.</param>
        /// <returns>A new rectangle which contains the trasnformed rectangle.</returns>
        public static Microsoft.Xna.Framework.Rectangle CalculateTransformedBoundingRectangle(Microsoft.Xna.Framework.Rectangle rectangle, Matrix transform)
        {
            //   Matrix inverseMatrix = Matrix.Invert(transform);
            // Get all four corners in local space
            leftTop = new Vector2(rectangle.Left, rectangle.Top);
            rightTop = new Vector2(rectangle.Right, rectangle.Top);
            leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Microsoft.Xna.Framework.Rectangle((int)Math.Round(min.X), (int)Math.Round(min.Y),
                                 (int)Math.Round(max.X - min.X), (int)Math.Round(max.Y - min.Y));
        }

        /// <summary>
        /// Generates a TriangleMesh (i.e. a complex collision surface) from a provided model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static TriangleMesh GetTriangleMesh(Model model,
            Transform transform)
        {
            TriangleMesh triangleMesh = new TriangleMesh();
            List<Vector3> vertexList = new List<Vector3>();
            List<TriangleVertexIndices> indexList = new List<TriangleVertexIndices>();

            ExtractData(vertexList, indexList, model);

            worldMatrix = GetWorldMatrix(Vector3.Zero, transform.Rotation, transform.Scale);

            for (int i = 0; i < vertexList.Count; i++)
            {
                vertexList[i] = Vector3.Transform(vertexList[i], worldMatrix);
            }

            // create the collision mesh
            triangleMesh.CreateMesh(vertexList, indexList, 1, 1.0f);

            return triangleMesh;
        }

        protected static Matrix GetWorldMatrix(Vector3 translation,
            Vector3 rotation, Vector3 scale)
        {
            return Matrix.Identity *
                Matrix.CreateScale(scale) *
                        Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) *
                        Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) *
                        Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z)) *
                                    Matrix.CreateTranslation(translation);
        }

        protected static void ExtractData(List<Vector3> vertices, List<TriangleVertexIndices> indices, Model model)
        {
            Matrix[] bones_ = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(bones_);
            foreach (Microsoft.Xna.Framework.Graphics.ModelMesh mm in model.Meshes)
            {
                int offset = vertices.Count;
                Matrix xform = bones_[mm.ParentBone.Index];
                foreach (ModelMeshPart mmp in mm.MeshParts)
                {
                    Vector3[] a = new Vector3[mmp.NumVertices];
                    int stride = mmp.VertexBuffer.VertexDeclaration.VertexStride;
                    mmp.VertexBuffer.GetData(mmp.VertexOffset * stride, a, 0, mmp.NumVertices, stride);

                    for (int i = 0; i != a.Length; ++i)
                        Vector3.Transform(ref a[i], ref xform, out a[i]);
                    vertices.AddRange(a);

                    if (mmp.IndexBuffer.IndexElementSize != IndexElementSize.SixteenBits)
                        throw new Exception(String.Format("Model uses 32-bit indices, which are not supported."));

                    short[] s = new short[mmp.PrimitiveCount * 3];
                    mmp.IndexBuffer.GetData(mmp.StartIndex * 2, s, 0, mmp.PrimitiveCount * 3);

                    JigLibX.Geometry.TriangleVertexIndices[] tvi = new JigLibX.Geometry.TriangleVertexIndices[mmp.PrimitiveCount];
                    for (int i = 0; i != tvi.Length; ++i)
                    {
                        tvi[i].I0 = s[i * 3 + 2] + offset;
                        tvi[i].I1 = s[i * 3 + 1] + offset;
                        tvi[i].I2 = s[i * 3 + 0] + offset;
                    }
                    indices.AddRange(tvi);
                }
            }
        }
    }
}