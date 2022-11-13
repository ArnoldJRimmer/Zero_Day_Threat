using GD.Engine.Collections;
using GD.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GD.Engine
{
    /// <summary>
    /// Store all the drawn and updateable GameOjects and call Update and Draw
    /// </summary>
    public class Scene
    {
        #region Fields

        /// <summary>
        /// A unique name to identify a scene (e.g. Haunted House Courtyard)
        /// </summary>
        private string id;

        /// <summary>
        /// Stores all opaque objects in the scene
        /// </summary>
        private GameObjectList opaqueList;

        /// <summary>
        /// Stores all transparent (i.e. semi or totally transparent) objects in the scene
        /// </summary>
        private GameObjectList transparentList;

        #endregion Fields

        #region Properties

        public string ID { get => id; set => id = value.Trim(); }
        public GameObjectList OpaqueList { get => opaqueList; protected set => opaqueList = value; }
        public GameObjectList TransparentList { get => transparentList; protected set => transparentList = value; }

        #endregion Properties

        #region Constructors

        public Scene(string id)
        {
            ID = id;
            opaqueList = new GameObjectList();
            transparentList = new GameObjectList();
        }

        #endregion Constructors

        #region Actions - Add, Find, FindAll, Remove, RemoveAll, Size, Clear

        public void Add(GameObject gameObject)
        {
            if (gameObject.RenderType == RenderType.Opaque)
                opaqueList.Add(gameObject);
            else
                transparentList.Add(gameObject);
        }

        public GameObject Find(ObjectType objectType, RenderType renderType,
                                    Predicate<GameObject> predicate)
        {
            GameObject found = null;

            if (renderType == RenderType.Opaque)
                found = opaqueList.Find(objectType, predicate);
            else
                found = transparentList.Find(objectType, predicate);

            return found;
        }

        public bool Remove(ObjectType objectType, RenderType renderType, Predicate<GameObject> predicate)
        {
            bool wasRemoved = false;

            if (renderType == RenderType.Opaque)
                wasRemoved = opaqueList.Remove(objectType, predicate);
            else
                wasRemoved = transparentList.Remove(objectType, predicate);

            return wasRemoved;
        }

        public List<GameObject> FindAll(ObjectType objectType, RenderType renderType,
                                Predicate<GameObject> predicate)
        {
            List<GameObject> foundList = null;

            if (renderType == RenderType.Opaque)
                foundList = opaqueList.FindAll(objectType, predicate);
            else
                foundList = transparentList.FindAll(objectType, predicate);

            return foundList;
        }

        public int RemoveAll(ObjectType objectType, RenderType renderType,
                                Predicate<GameObject> predicate)
        {
            int removeCount = 0;

            if (renderType == RenderType.Opaque)
                removeCount = opaqueList.RemoveAll(objectType, predicate);
            else
                removeCount = transparentList.RemoveAll(objectType, predicate);

            return removeCount;
        }

        public void Size(RenderType renderType, out int sizeStaticList, out int sizeDynamicList)
        {
            if (renderType == RenderType.Opaque)
                opaqueList.Size(out sizeStaticList, out sizeDynamicList);
            else
                transparentList.Size(out sizeStaticList, out sizeDynamicList);
        }

        public int TotalSize()
        {
            return opaqueList.TotalSize() + transparentList.TotalSize();
        }

        public void Clear(ObjectType objectType, RenderType renderType)
        {
            if (renderType == RenderType.Opaque)
                opaqueList.Clear(objectType);
            else
                transparentList.Clear(objectType);
        }

        public void ClearAll()
        {
            opaqueList.Clear(ObjectType.Static);
            transparentList.Clear(ObjectType.Static);

            opaqueList.Clear(ObjectType.Dynamic);
            transparentList.Clear(ObjectType.Dynamic);
        }

        #endregion Actions - Add, Find, FindAll, Remove, RemoveAll, Size, Clear

        #region Actions - Update

        public virtual void Update(GameTime gameTime)
        {
            opaqueList.Update(gameTime);
            transparentList.Update(gameTime);
        }

        //public virtual void Draw(GameTime gameTime, Camera camera)
        //{
        //    opaqueList.Draw(gameTime, camera);
        //    transparentList.Draw(gameTime, camera);
        //}

        #endregion Actions - Update

        #region DEBUG

#if DEBUG

        public string GetPerfStats()
        {
            return $"Op: {opaqueList.GetPerfStats()}, Tr:{transparentList.GetPerfStats()}";
        }

#endif

        #endregion DEBUG
    }
}