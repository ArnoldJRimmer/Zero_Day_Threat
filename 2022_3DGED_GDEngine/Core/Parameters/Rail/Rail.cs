using Microsoft.Xna.Framework;
using System;

namespace GD.Engine.Parameters
{
    /// <summary>
    /// Represents a bounded rail in 3D along which an object can translate.
    /// Typically used by a rail controller attached to a camera which causes
    /// the camera to follow a moving object in a room.
    /// </summary>
    /// <see cref="GDLibrary.Controllers.RailController"/>
    public class Rail
    {
        #region Fields

        private string id;
        private Vector3 start, end, midPoint, look;
        private bool isDirty;
        private float length;

        #endregion Fields

        #region Properties

        public Vector3 Look
        {
            get
            {
                Update();
                return look;
            }
        }

        public float Length
        {
            get
            {
                Update();
                return length;
            }
        }

        public Vector3 MidPoint
        {
            get
            {
                Update();
                return midPoint;
            }
        }

        public Vector3 Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
                isDirty = true;
            }
        }

        public Vector3 End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
                isDirty = true;
            }
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value.Trim();
            }
        }

        #endregion Properties

        #region Constructors & Core

        public Rail(string id, Vector3 start, Vector3 end)
        {
            ID = id;
            Start = start;
            End = end;

            //clean/dirty flag to update relevant fields when a component field has changed
            isDirty = true;
        }

        //Returns true if the position is between start and end, otherwise false
        public bool InsideRail(Vector3 position)
        {
            float distanceToStart = Vector3.Distance(position, start);
            float distanceToEnd = Vector3.Distance(position, end);
            return ((distanceToStart <= length) && (distanceToEnd <= length));
        }

        private void Update()
        {
            if (isDirty)
            {
                length = Math.Abs(Vector3.Distance(start, end));  //remove abs()
                look = Vector3.Normalize(end - start); //L.C = |L|.|C|.Cos(x)
                midPoint = (start + end) / 2;
                isDirty = false;
            }
        }

        #endregion Constructors & Core

        //Add Equals, Clone, ToString, GetHashCode...
    }
}