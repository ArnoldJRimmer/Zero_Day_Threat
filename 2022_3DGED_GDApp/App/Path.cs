﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GD.Engine
{
    // Path has an array of transforms each transform being it's own tile
    // Requires an easy constructor and a full constructor and Add piece method
    public class Path
    {
        private string pathName;
        private List<Transform> pieces;
        private int size;
        private bool pathFormed;

        #region Constructor
        public Path(string name)
        {
            PathName = name;
            size = 3;
            pieces = new List<Transform>();
            PathFormed= false;
        }

        public Path(string pathName, List<Transform> pieces)
        {
            this.pathName = pathName;
            this.pieces = pieces;
            this.size = pieces.Count;
            PathFormed = false;
        }
        #endregion ConstructorS

        #region Properties

        public string PathName { get => pathName; set => pathName = value; }
        public List<Transform> Pieces { get => pieces; set => pieces = value; }
        public int Size { get => size; set => size = value; }
        public bool PathFormed { get => pathFormed; set => pathFormed = value; }

        #endregion Properties

        public void AddPiece(Transform piece)
        {
            if(piece!=null) 
            {
                
                pieces.Add(piece);
                size++;
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