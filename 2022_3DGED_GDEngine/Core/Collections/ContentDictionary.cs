using GD.Engine.Globals;
using GD.Engine.Utilities;
using System;
using System.Collections.Generic;

namespace GD.Engine.Collections
{
    /// <summary>
    /// Provide generic map to load and store game content and allow Dispose() to be called on all content
    /// </summary>
    /// <typeparam name="V">MonoGame or user-defined class that optionally implements IDisposable</typeparam>
    public class ContentDictionary<V> : Dictionary<string, V>, IDisposable
    {
        #region Fields

        private string name;

        #endregion Fields

        #region Properties

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value.Trim();
            }
        }

        #endregion Properties

        public ContentDictionary(string name = "asset dictionary")
        {
            Name = name;
        }

        public virtual bool Add(string key, string assetPath)
        {
            key = StringUtility.ParseNameFromPath(key.Trim());

            if (!ContainsKey(key))
            {
                Add(key, Application.Content.Load<V>(assetPath));
                return true;
            }
            return false;
        }

        //same as Load() above but uses assetPath to form key string from regex
        public virtual bool Add(string assetPath)  //"Assets/Props/crates", crates
        {
            return Add(Utilities.StringUtility.ParseNameFromPath(assetPath), assetPath);
        }

        public new bool Remove(string key)
        {
            key = key.Trim();
            if (ContainsKey(key))
            {
                //unload from RAM
                Dispose(this[key]);
                //remove from dictionary
                Remove(key);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Disposes of the contents of the dictionary.
        /// </summary>
        /// <seealso cref="https://robertgreiner.com/iterating-through-a-dictionary-in-csharp/"/>
        public virtual void Dispose()
        {
            //copy values from dictionary to list
            List<V> list = new List<V>(Values);

            for (int i = 0; i < list.Count; i++)
            {
                Dispose(list[i]);
            }

            //empty the list
            list.Clear();

            //clear the dictionary
            Clear();
        }

        public virtual void Dispose(V value)
        {
            //if this is a disposable object (e.g. model, sound, font, texture) then call its dispose
            if (value is IDisposable)
            {
                ((IDisposable)value).Dispose();
            }
            //if it's just a user-defined or C# object, then set to null for garbage collection
            else
            {
                value = default; //null
            }
        }
    }
}