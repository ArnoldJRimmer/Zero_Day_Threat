using System;
using System.Collections.Generic;

namespace GD.Engine
{
    public class ObjectList
    {
        private List<Renderer> renderers;
        private List<Collider> colliders;
        private List<GameObject> gameObjects;

        public List<Renderer> Renderers { get => renderers; set => renderers = value; }
        public List<Collider> Colliders { get => colliders; set => colliders = value; }
        public List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }

        public ObjectList()
        {
            renderers = new List<Renderer>();
            colliders = new List<Collider>();
            gameObjects = new List<GameObject>();
        }

        public void Add(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
            CheckComponents(gameObject, ComponentChangeType.Add);
        }

        public void Remove(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
            CheckComponents(gameObject, ComponentChangeType.Remove);
        }

        public GameObject Find(Predicate<GameObject> predicate)
        {
            return gameObjects.Find(predicate);
        }

        public List<GameObject> FindAll(Predicate<GameObject> predicate)
        {
            return gameObjects.FindAll(predicate);
        }

        public int RemoveAll(Predicate<GameObject> predicate)
        {
            List<GameObject> removeList = gameObjects.FindAll(predicate);
            foreach (GameObject gameObject in removeList)
            {
                Remove(gameObject);
            }

            int removed = removeList.Count;
            removeList.Clear();
            return removed;
        }

        protected void CheckComponents(GameObject gameObject, ComponentChangeType type)
        {
            for (int i = 0; i < gameObject.Components.Count; i++)
            {
                var component = gameObject.Components[i];

                if (component is Renderer renderer)
                {
                    if (type == ComponentChangeType.Add)
                        AddRenderer(renderer);
                    else if (type == ComponentChangeType.Remove)
                        RemoveRenderer(renderer);
                }
                else if (component is Collider collider)
                {
                    if (type == ComponentChangeType.Add)
                        AddCollider(collider);
                    else if (type == ComponentChangeType.Remove)
                        RemoveCollider(collider);
                }
            }
        }

        protected void AddRenderer(Renderer renderer)
        {
            if (renderers.Contains(renderer))
                return;

            renderers.Add(renderer);
            //TODO - sort by alpha
            //    renderers.Sort((x, y) => y.Material.Alpha.CompareTo(x.Material.Alpha));
        }

        protected void RemoveRenderer(Renderer renderer)
        {
            if (renderers.Contains(renderer))
                renderers.Remove(renderer);
        }

        protected void AddCollider(Collider collider)
        {
            if (colliders.Contains(collider))
                return;

            colliders.Add(collider);
            //  colliders.Sort();
        }

        protected void RemoveCollider(Collider collider)
        {
            if (colliders.Contains(collider))
                colliders.Remove(collider);
        }

        public int Size()
        {
            return gameObjects.Count;
        }

        public void Clear()
        {
            gameObjects.Clear();
            colliders.Clear();
            renderers.Clear();
        }

        /// <summary>
        /// Actions that we can apply to a game object in a list
        /// </summary>
        public enum ComponentChangeType
        {
            Add, Remove
        }
    }
}