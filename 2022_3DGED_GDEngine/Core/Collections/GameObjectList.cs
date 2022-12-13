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
        private ObjectList staticList;

        /// <summary>
        /// Stores any object which can be added/removed (e.g. pickup)
        /// </summary>
        private ObjectList dynamicList;

        #endregion Fields

        #region Properties

        public ObjectList StaticList
        { get { return staticList; } }

        public ObjectList DynamicList
        { get { return dynamicList; } }

        #endregion Properties

        #region Constructors

        public GameObjectList()
        {
            staticList = new ObjectList();
            dynamicList = new ObjectList();
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
            sizeStaticList = staticList.Size();
            sizeDynamicList = dynamicList.Size();
        }

        public int TotalSize()
        {
            return staticList.Size() + dynamicList.Size();
        }

        public void Clear(ObjectType objectType)
        {
            if (objectType == ObjectType.Static)
                staticList.Clear();
            else
                dynamicList.Clear();
        }

        #endregion Actions - Add, Find, FindAll, Remove, RemoveAll, Size, Clear

        #region Actions - Update

        public virtual void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in staticList.GameObjects)
                gameObject.Update(gameTime);

            foreach (GameObject gameObject in dynamicList.GameObjects)
                gameObject.Update(gameTime);
        }

        #region DEBUG

#if DEBUG

        public string GetPerfStats()
        {
            return $"[S:{staticList.Size()},D:{dynamicList.Size()}]";
        }

#endif

        #endregion DEBUG

        #endregion Actions - Update
    }
}