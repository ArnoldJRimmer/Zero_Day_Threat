using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GD.Engine
{
    /// <summary>
    /// Base object in the game for 3D, 2D and un-drawn (e.g., Camera) objects
    /// </summary>
    /// <see cref="GameObjectList"/>
    /// <seealso cref="Scene"/>
    public class GameObject
    {
        #region Fields

        /// <summary>
        /// Friendly name for the current object
        /// </summary>
        protected string name;

        /// <summary>
        /// Stores S, R, T of GameObject to generate the world matrix
        /// </summary>
        protected Transform transform;

        /// <summary>
        /// List of all attached components
        /// </summary>
        protected List<Component> components;

        /// <summary>
        /// Static (persists for scene) or Dynamic (add/remove during game)
        /// </summary>
        /// <see cref="Scene"/>
        private ObjectType objectType;

        /// <summary>
        /// Opaque or transparent
        /// </summary>
        /// <see cref="Scene"/>
        private RenderType renderType;

        /// <summary>
        /// Used when we collide with a GameObject to filter if it should be handled
        /// </summary>
        /// <see cref="Collider.HandleResponse(GameObject)"/>
        private GameObjectType gameObjectType = GameObjectType.Prop;

        #endregion Fields

        #region Properties

        public ObjectType ObjectType
        { get { return objectType; } }

        public RenderType RenderType
        { get { return renderType; } }

        /// <summary>
        /// Gets/sets the game object name
        /// </summary>
        public string Name { get => name; set => name = value.Trim(); }

        /// <summary>
        /// Gets/sets the transform associated with the current game object
        /// </summary>
        public Transform Transform { get => transform; set => transform = value; }

        /// <summary>
        /// Gets a list of all components (e.g. controllers, behaviours, camera) of the current object
        /// </summary>
        public List<Component> Components { get => components; }
        public GameObjectType GameObjectType { get => gameObjectType; set => gameObjectType = value; }

        #endregion Properties

        #region Constructors

        public GameObject(string name)
            : this(name, ObjectType.Static, RenderType.Opaque)
        {
            //RISK - Any object made with this constructor will be static and opaque!
        }

        public GameObject(string name,
            ObjectType objectType, RenderType renderType)
        {
            Name = name;
            components = new List<Component>();
            this.objectType = objectType;
            this.renderType = renderType;
        }

        #endregion Constructors

        #region Actions - Add, Remove, Get Component

        /// <summary>
        /// Adds a component to the game object
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Component component)
        {
            //set the component to have access to game object and transform
            if (component.transform == null)
                component.transform = transform;

            if (component.gameObject == null)
                component.gameObject = this;

            //add to the list of components
            components.Add(component);
        }

        /// <summary>
        /// Gets a component by type e.g. Camera
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().Equals(typeof(T)))
                    return components[i] as T;
            }
            return null;
        }

        /// <summary>
        /// Removes a component by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool RemoveComponent(Predicate<Component> predicate)
        {
            Component target = components.Find(predicate);
            components.Remove(target);
            return target != null;
        }

        /// <summary>
        /// Removes a component by type e.g. Camera
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool RemoveComponent<T>() where T : Component
        {
            Component target = GetComponent<T>();
            components.Remove(target);
            return target != null;
        }

        #endregion Actions - Add, Remove, Get Component

        #region Actions - Update

        /// <summary>
        /// Called each update to call an update on all components of the game object
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            //TODO - Add check for IsUpdateable
            for (int i = 0; i < components.Count; i++)
                components[i].Update(gameTime);
        }

        #endregion Actions - Update

        #region Utility

        /// <summary>
        /// Object to target vector, also provides access to distance from object to target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Vector3 GetNormalizedVectorTo(GameObject target, out float distance)
        {
            //camera to target object vector
            Vector3 vectorToTarget = target.Transform.Translation - transform.Translation;

            //distance from camera to target
            distance = vectorToTarget.Length();

            //camera to target object vector
            vectorToTarget.Normalize();

            return vectorToTarget;
        }

        /// <summary>
        /// Object to target vector, no distance
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public Vector3 GetNormalizedVectorTo(GameObject target)
        {
            //camera to target object vector
            return Vector3.Normalize(target.Transform.Translation - transform.Translation);
        }

        #endregion Utility
    }
}