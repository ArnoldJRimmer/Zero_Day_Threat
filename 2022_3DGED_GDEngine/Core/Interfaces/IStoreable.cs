using GD.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace GD.Engine
{
    internal interface IStoreable
    {
        public void Add(GameObject gameObject);
        public GameObject Find(ObjectType objectType, RenderType renderType, Predicate<GameObject> predicate);
        public bool Remove(ObjectType objectType, RenderType renderType, Predicate<GameObject> predicate);
        public List<GameObject> FindAll(ObjectType objectType, RenderType renderType, Predicate<GameObject> predicate);
        public int RemoveAll(ObjectType objectType, RenderType renderType, Predicate<GameObject> predicate);
    }
}