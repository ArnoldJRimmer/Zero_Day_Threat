using GD.Engine.Collections;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GD.Engine
{
    // Path has an array of transforms each transform being it's own tile
    // Requires an easy constructor and a full constructor and Add piece method
    public class Path
    {
        private string pathName;
        private List<Transform> pieces;
        private List<Boolean> states;
        private int size;
        private bool pathFormed;

        #region Constructor
        public Path(string name)
        {
            PathName = name;
            size = 0;
            pieces = new List<Transform>();
            States = new List<Boolean>();
            PathFormed = false;
        }

        public Path(string pathName, List<Transform> pieces)
        {
            this.pathName = pathName;
            this.pieces = pieces;
            this.size = pieces.Count;
            States = new List<Boolean>(size);
            PathFormed = false;
        }
        #endregion ConstructorS

        #region Properties

        public string PathName { get => pathName; set => pathName = value; }
        public List<Transform> Pieces { get => pieces; set => pieces = value; }
        public int Size { get => size; set => size = value; }
        public bool PathFormed { get => pathFormed; set => pathFormed = value; }
        public List<bool> States { get => states; set => states = value; }

        #endregion Properties

        public void AddPiece(Transform piece)
        {
            if(piece!=null) 
            {
                pieces.Add(piece);
                size++;
                states.Add(false);
            }
        }

        public void setState(Boolean state, int pos)
        {
            if (pos <= size)
            {
                States[pos] = state;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Path path &&
                   pathName == path.pathName &&
                   EqualityComparer<List<Transform>>.Default.Equals(pieces, path.pieces) &&
                   size == path.size &&
                   pathFormed == path.pathFormed;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(pathName, pieces, size, pathFormed);
        }
    }
}
