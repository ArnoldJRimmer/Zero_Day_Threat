#region Using Statements

using Microsoft.Xna.Framework;

#endregion

namespace JigLibX.Utils
{
    /// <summary>
    /// Class JiggleUnsafe
    /// </summary>
    public sealed class JiggleUnsafe
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="index"></param>
        /// <returns>float</returns>
        public static unsafe float Get(ref Vector3 vec, int index)
        {
            fixed (Vector3* adr = &vec)
            {
                return ((float*)adr)[index];
            }
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="index"></param>
        /// <returns>float</returns>
        public static unsafe float Get(Vector3 vec, int index)
        {
            Vector3* adr = &vec;
            return ((float*)adr)[index];
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="index"></param>
        /// <returns>Vector3</returns>
        public static unsafe Vector3 Get(Matrix mat, int index)
        {
            float* adr = &mat.M11;
            adr += index;
            return ((Vector3*)adr)[index];
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="index"></param>
        /// <returns>Vector3</returns>
        public static unsafe Vector3 Get(ref Matrix mat, int index)
        {
            fixed (float* adr = &mat.M11)
            {
                return ((Vector3*)(adr + index))[index];
            }
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="index"></param>
        /// <param name="vec"></param>
        public static unsafe void Get(ref Matrix mat, int index, out Vector3 vec)
        {
            fixed (float* adr = &mat.M11)
            {
                vec = ((Vector3*)(adr + index))[index];
            }
        }
    }
}