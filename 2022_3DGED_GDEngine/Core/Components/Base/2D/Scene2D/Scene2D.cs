using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GD.Engine
{
    /// <summary>
    /// Store all the drawn and updateable GameOjects and call Update and Draw
    /// </summary>
    public class Scene2D : IProvideStats
    {
        #region Fields

        /// <summary>
        /// A unique name to identify a scene (e.g. Haunted House Courtyard)
        /// </summary>
        private string id;

        /// <summary>
        /// Stores all opaque objects in the scene
        /// </summary>
        private List<GameObject> opaqueList;

        /// <summary>
        /// Stores all transparent (i.e. semi or totally transparent) objects in the scene
        /// </summary>
        private List<GameObject> transparentList;

        #endregion Fields

        #region Properties

        public string ID { get => id; set => id = value.Trim(); }
        public List<GameObject> OpaqueList { get => opaqueList; protected set => opaqueList = value; }
        public List<GameObject> TransparentList { get => transparentList; protected set => transparentList = value; }

        #endregion Properties

        #region Constructors

        public Scene2D(string id)
        {
            ID = id;
            opaqueList = new List<GameObject>();
            transparentList = new List<GameObject>();
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

        public GameObject Find(RenderType renderType, Predicate<GameObject> predicate)
        {
            GameObject found = null;

            if (renderType == RenderType.Opaque)
                found = opaqueList.Find(predicate);
            else
                found = transparentList.Find(predicate);

            return found;
        }

        public bool Remove(RenderType renderType, Predicate<GameObject> predicate)
        {
            bool wasRemoved = false;

            if (renderType == RenderType.Opaque)
                wasRemoved = opaqueList.Remove(Find(renderType, predicate));
            else
                wasRemoved = transparentList.Remove(Find(renderType, predicate));

            return wasRemoved;
        }

        public List<GameObject> FindAll(ObjectType objectType, RenderType renderType,
                                Predicate<GameObject> predicate)
        {
            List<GameObject> foundList = null;

            if (renderType == RenderType.Opaque)
                foundList = opaqueList.FindAll(predicate);
            else
                foundList = transparentList.FindAll(predicate);

            return foundList;
        }

        public int RemoveAll(ObjectType objectType, RenderType renderType,
                                Predicate<GameObject> predicate)
        {
            int removeCount = 0;

            if (renderType == RenderType.Opaque)
                removeCount = opaqueList.RemoveAll(predicate);
            else
                removeCount = transparentList.RemoveAll(predicate);

            return removeCount;
        }

        public void Size(out int sizeOpaqueList, out int sizeTransparentList)
        {
            sizeOpaqueList = opaqueList.Count;
            sizeTransparentList = transparentList.Count;
        }

        public int TotalSize()
        {
            return opaqueList.Count + transparentList.Count;
        }

        public void Clear(RenderType renderType)
        {
            if (renderType == RenderType.Opaque)
                opaqueList.Clear();
            else
                transparentList.Clear();
        }

        public void ClearAll()
        {
            opaqueList.Clear();
            transparentList.Clear();
        }

        #endregion Actions - Add, Find, FindAll, Remove, RemoveAll, Size, Clear

        #region Actions - Update

        public virtual void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in opaqueList)
                gameObject.Update(gameTime);

            foreach (GameObject gameObject in transparentList)
                gameObject.Update(gameTime);
        }

        #endregion Actions - Update

        #region DEBUG

#if DEBUG

        public string GetStatistics()
        {
            return $"Op: {opaqueList.Count}, Tr:{transparentList.Count}";
        }

#endif

        #endregion DEBUG
    }
}
