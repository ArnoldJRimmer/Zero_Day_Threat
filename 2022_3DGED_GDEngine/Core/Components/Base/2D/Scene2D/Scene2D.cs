using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GD.Engine
{
    /// <summary>
    /// Store all the drawn and updateable GameOjects and call Update and Draw
    /// </summary>
    public class Scene2D : IProvideStats, IUpdateable
    {
        #region Fields

        /// <summary>
        /// A unique name to identify a scene (e.g. Haunted House Courtyard)
        /// </summary>
        private string id;

        /// <summary>
        /// Stores all opaque objects in the scene
        /// </summary>
        private List<GameObject> objectList;

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> UpdateOrderChanged;

        #endregion Fields

        #region Properties

        public string ID { get => id; set => id = value.Trim(); }
        public List<GameObject> ObjectList { get => objectList; protected set => objectList = value; }

        public bool Enabled => throw new NotImplementedException();

        public int UpdateOrder => throw new NotImplementedException();

        #endregion Properties

        #region Constructors

        public Scene2D(string id)
        {
            ID = id;
            objectList = new List<GameObject>();
        }

        #endregion Constructors

        #region Actions - Add, Find, FindAll, Remove, RemoveAll, Size, Clear

        public void Add(GameObject gameObject)
        {
            objectList.Add(gameObject);
        }

        public GameObject Find(Predicate<GameObject> predicate)
        {
            return objectList.Find(predicate);
        }

        public bool Remove(Predicate<GameObject> predicate)
        {
            return objectList.Remove(Find(predicate)); ;
        }

        public List<GameObject> FindAll(Predicate<GameObject> predicate)
        {
            return objectList.FindAll(predicate);
        }

        public int RemoveAll(Predicate<GameObject> predicate)
        {
            return objectList.RemoveAll(predicate);
        }

        public int Size()
        {
            return objectList.Count;
        }

        public void Clear()
        {
            objectList.Clear();
        }

        public void ClearAll()
        {
            objectList.Clear();
        }

        #endregion Actions - Add, Find, FindAll, Remove, RemoveAll, Size, Clear

        #region Actions - Update

        public virtual void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in objectList)
                gameObject.Update(gameTime);
        }

        #endregion Actions - Update

        #region DEBUG

#if DEBUG

        public string GetStatistics()
        {
            return $"{objectList.Count}";
        }

#endif

        #endregion DEBUG
    }
}