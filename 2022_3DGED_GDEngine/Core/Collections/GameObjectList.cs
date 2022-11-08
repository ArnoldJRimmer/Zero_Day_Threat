using GD.Engine.Globals;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GD.Engine.Collections
{
    /// <summary>
    /// Stores all static and dynamic game objects
    /// There will be two GameObjectList in the scene (opaque, transparent)
    /// </summary>
    /// <see cref="Scene"/>
    public class GameObjectList
    {
        #region Fields

        /// <summary>
        /// Stores any object which persists during game
        /// </summary>
        private List<GameObject> staticList;

        /// <summary>
        /// Stores any object which can be added/removed (e.g. pickup)
        /// </summary>
        private List<GameObject> dynamicList;

        //TODO - List<Renderers> for static and dynamic objects

        #endregion Fields

        #region Properties

        public List<GameObject> StaticList
        { get { return staticList; } }

        public List<GameObject> DynamicList
        { get { return dynamicList; } }

        #endregion Properties

        #region Constructors

        public GameObjectList()
        {
            staticList = new List<GameObject>();
            dynamicList = new List<GameObject>();
        }

        #endregion Constructors

        #region Actions - Add, Find, FindAll, Remove, RemoveAll, Size, Clear

        public void Add(GameObject gameObject)
        {
            if (gameObject.ObjectType == ObjectType.Static)
                staticList.Add(gameObject);
            else
                dynamicList.Add(gameObject);
        }

        public GameObject Find(ObjectType objectType, Predicate<GameObject> predicate)
        {
            GameObject found = null;

            if (objectType == ObjectType.Static)
                found = staticList.Find(predicate);
            else
                found = dynamicList.Find(predicate); ;

            return found;
        }

        public bool Remove(ObjectType objectType, Predicate<GameObject> predicate)
        {
            GameObject found = Find(objectType, predicate);

            if (found == null)
                return false;

            if (objectType == ObjectType.Static)
                staticList.Remove(found);
            else
                dynamicList.Remove(found);

            return true;
        }

        public List<GameObject> FindAll(ObjectType objectType, Predicate<GameObject> predicate)
        {
            List<GameObject> foundList = null;

            if (objectType == ObjectType.Static)
                foundList = staticList.FindAll(predicate);
            else
                foundList = dynamicList.FindAll(predicate);

            return foundList;
        }

        public int RemoveAll(ObjectType objectType, Predicate<GameObject> predicate)
        {
            int removeCount = 0;

            if (objectType == ObjectType.Static)
                removeCount = staticList.RemoveAll(predicate);
            else
                removeCount = dynamicList.RemoveAll(predicate);

            return removeCount;
        }

        public void Size(out int sizeStaticList, out int sizeDynamicList)
        {
            sizeStaticList = staticList.Count;
            sizeDynamicList = dynamicList.Count;
        }

        public int TotalSize()
        {
            return staticList.Count + dynamicList.Count;
        }

        public void Clear(ObjectType objectType)
        {
            if (objectType == ObjectType.Static)
                staticList.Clear();
            else
                dynamicList.Clear();
        }

        #endregion Actions - Add, Find, FindAll, Remove, RemoveAll, Size, Clear

        #region Actions - Update, Draw

        public virtual void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in staticList)
                gameObject.Update(gameTime);

            foreach (GameObject gameObject in dynamicList)
                gameObject.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, Camera camera)
        {
            //TODO - add inefficiency with GetComponent
            foreach (GameObject gameObject in staticList)
                gameObject.GetComponent<Renderer>().Draw(Application.GraphicsDevice, camera);

            foreach (GameObject gameObject in dynamicList)
                gameObject.GetComponent<Renderer>().Draw(Application.GraphicsDevice, camera);
        }

        #region DEBUG

#if DEBUG

        public string GetPerfStats()
        {
            return $"[S:{staticList.Count},D:{dynamicList.Count}]";
        }

#endif

        #endregion DEBUG

        #endregion Actions - Update, Draw
    }
}