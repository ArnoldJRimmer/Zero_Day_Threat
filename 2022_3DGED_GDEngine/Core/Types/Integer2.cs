using System;

namespace GD.Core.Types
{
    public class Integer2 : ICloneable
    {
        /// <summary>
        /// See expression bodied members
        /// </summary>
        /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members"/>
        public static Integer2 UnitX => new Integer2(1, 0);

        public static Integer2 UnitY => new Integer2(0, 1);
        public static Integer2 One => new Integer2(1, 1);
        public static Integer2 Zero => new Integer2(0, 0);
        //{
        //    get
        //    {
        //        return new Integer2(0, 0);
        //    }
        //}

        private int x, y;
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public Integer2(int x, int y)
        {
            this.x = x; this.y = y;
        }

        //TODO - arithmetic operator (+, -, *, /)

        public static bool operator ==(Integer2 lhs, Integer2 rhs)
        {
            return lhs.x == rhs.X && lhs.y == rhs.Y;
        }

        public static bool operator !=(Integer2 lhs, Integer2 rhs)
        {
            return lhs.x != rhs.X || lhs.y != rhs.Y;
        }

        public new string ToString()
        {
            return $"[{x}, {y}]";
        }

        public object Clone()
        {
            //shallow - original and clone point to same object in RAM
            //deep - original and clone point to DIFFERENT objects in RAM
            //when we see 'new' realise that we are allocating new space in RAM
            return new Integer2(x, y);
        }

        public object GetShallowCopy()
        {
            //shallow returns a reference to the existing object
            return this;
        }

        public override bool Equals(object obj)
        {
            //this typecast will cause runtime exception
            //Integer2 theInt = (Integer2)obj;

            //this typecast will set theInt to null if it fails
            Integer2 theInt = obj as Integer2;
            if (theInt == null)
                return false;

            return x == theInt.X && y == theInt.Y;

            //return obj is Integer2 integer &&
            //       x == integer.x &&
            //       y == integer.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }
}