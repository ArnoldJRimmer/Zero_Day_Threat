using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GD.Engine
{
    /// <summary>
    /// Base object in the game
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
    }
}