using JigLibX.Collision;
using JigLibX.Geometry;
using JigLibX.Physics;
using Microsoft.Xna.Framework;

namespace GD.Engine
{
    /// <summary>
    /// Provides physics behaviour for the game object
    /// </summary>
    public class Collider : Component
    {
        #region Fields

        /// <summary>
        /// Holds the mass, position, angular momentum, velocity of the collidable object
        /// </summary>
        protected Body body;

        /// <summary>
        /// Holds the primitive (e.g. sphere, capsule, box) which tests for collisions
        /// </summary>
        protected CollisionSkin collision;

        protected bool isHandlingCollision;
        protected bool isTrigger;

        #endregion Fields

        #region Properties

        public Body Body { get => body; protected set => body = value; }
        public CollisionSkin Collision { get => collision; protected set => collision = value; }
        public bool IsTrigger { get => isTrigger; }
        public bool IsHandlingCollision { get => isHandlingCollision; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Create a collider with response handling.
        /// A collider that is a trigger will generate a response but you will
        /// be able to walk through it, as in Unity
        /// </summary>
        /// <param name="isHandlingCollision"></param>
        /// <param name="isTrigger"></param>
        public Collider(GameObject gameObject,
            bool isHandlingCollision = false, bool isTrigger = false)
        {
            this.isHandlingCollision = isHandlingCollision;
            this.isTrigger = isTrigger;

            //cache the transform
            transform = gameObject.Transform;
            //instanciate a new body
            Body = new Body();
            //set the parent game object to be the attached drawn object (used when collisions occur)
            Body.Parent = gameObject;
            //instanciate a collision skin (which will have a primitive added e.g. sphere, capsule, trianglemesh)
            Collision = new CollisionSkin(Body, isTrigger);
            //set the skin as belonging to the body
            Body.CollisionSkin = Collision;

            //add collision reponse handling
            if (isHandlingCollision)
                Body.CollisionSkin.callbackFn += HandleCollision;
        }

        #endregion Constructors

        #region Actions - Physics related

        /// <summary>
        /// Called whenever this collider collides with another collidable (i.e. the collidee)
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="collidee"></param>
        /// <returns>True if collidee is not a trigger, otherwise false</returns>
        protected virtual bool HandleCollision(CollisionSkin collider,
            CollisionSkin collidee)
        {
            //get the game object that we just collided with
            HandleResponse(collidee.Owner.Parent as GameObject);

            //this return determines if we can pass through (false) or be stopped by (true) a collider
            return !collidee.IsTrigger;
        }

        protected virtual void HandleResponse(GameObject parentGameObject)
        {
            //DONT ADD CODE SPECIFIC TO YOUR GAME IN COLLIDER
            //see classes that inherit from this collider e.g. MyPlayerCollider

            //int x = 0;
            //if (parentGameObject.GameObjectType == GameObjectType.Consumable)
            //{
            //    //SceneManager - remove!
            //    //PhysicsManager - remove!
            //    object[] param = { parentGameObject,
            //        "sad_monkey", 10, "you just picked up a monkey!"};
            //    EventDispatcher.Raise(new EventData(EventCategoryType.GameObject,
            //        EventActionType.OnRemoveObject, param));
            //}
        }

        /// <summary>
        /// Used to add a collision primitive to the collider behaviour
        /// </summary>
        /// <param name="primitive">Primitive</param>
        /// <param name="materialProperties">MaterialProperties</param>
        public virtual void AddPrimitive(Primitive primitive, MaterialProperties materialProperties)
        {
            if (Collision == null)
                throw new System.NullReferenceException("CollisionSkin is null! Did you add the Collider to the GameObject using AddComponent() before calling this method?");

            Collision?.AddPrimitive(primitive, materialProperties);
        }

        /// <summary>
        /// Must be called after we instanciate the new collider behaviour in order for the body to participate in the physics system
        /// </summary>
        public virtual void Enable(GameObject gameObject, bool isImmovable, float mass)
        {
            //set whether the object can move
            Body.Immovable = isImmovable;
            //calculate the centre of mass
            Vector3 com = SetMass(mass);
            //adjust skin so that it corresponds to the 3D mesh as drawn on screen
            Body.MoveTo(transform.Translation,
                gameObject.Transform.RotationMatrix);
            //set the centre of mass
            Collision.ApplyLocalTransform(new JigLibX.Math.Transform(-com, Matrix.Identity));
            //enable so that any applied forces (e.g. gravity) will affect the object
            Body.EnableBody();
        }

        /// <summary>
        /// Sets the physics mass of the body
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        protected Vector3 SetMass(float mass)
        {
            float junk;
            Vector3 com;
            Matrix it, itCoM;
            PrimitiveProperties primitiveProperties = new PrimitiveProperties(PrimitiveProperties.MassDistributionEnum.Solid, PrimitiveProperties.MassTypeEnum.Density, mass);
            Collision.GetMassProperties(primitiveProperties, out junk, out com, out it, out itCoM);
            Body.BodyInertia = itCoM;
            Body.Mass = junk;
            return com;
        }

        #endregion Actions - Physics related

        public override void Update(GameTime gameTime)
        {
            //TODO - only update if body is active

            transform.World
                = Matrix.CreateScale(transform.Scale) *
                    collision.GetPrimitiveLocal(0).Transform.Orientation *
                        body.Orientation *
                                          Matrix.CreateTranslation(body.Position);
        }
    }
}