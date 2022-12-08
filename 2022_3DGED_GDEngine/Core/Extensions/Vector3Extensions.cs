using Microsoft.Xna.Framework;
using System;

/// <summary>
/// Adds extra methods to Vector3 using Extensions feature in C#
/// Note - We CANNOT use a namespace when we add extension methods. If we do, then the extensions will not appear in the target class e.g. Vector3
/// </summary>
///<seealso cref="https://www.tutorialsteacher.com/csharp/csharp-extension-method"/>
public static class Vector3Extensions
{
    public static Vector2 To2D(this Vector3 target)
    {
        return new Vector2(target.X, target.Y);
    }

    #region Set to a value in target

    public static void Set(this ref Vector3 target, float? x, float? y, float? z)
    {
        if (x.HasValue)
            target.X = x.Value;
        if (y.HasValue)
            target.Y = y.Value;
        if (z.HasValue)
            target.Z = z.Value;
    }

    public static void Set(this ref Vector3 target, Vector3 translation)
    {
        target.X = translation.X;
        target.Y = translation.Y;
        target.Z = translation.Z;
    }

    public static void Set(this ref Vector3 target, ref Vector3 translation)
    {
        target.X = translation.X;
        target.Y = translation.Y;
        target.Z = translation.Z;
    }

    #endregion Set to a value in target

    #region Add/Remove a value from target

    public static void Add(this ref Vector3 target, float? x, float? y, float? z)
    {
        if (x.HasValue)
            target.X += x.Value;
        if (y.HasValue)
            target.Y += y.Value;
        if (z.HasValue)
            target.Z += z.Value;
    }

    public static void Add(this ref Vector3 target, Vector3 delta)
    {
        target.X += delta.X;
        target.Y += delta.Y;
        target.Z += delta.Z;
    }

    public static void Add(this ref Vector3 target, ref Vector3 delta)
    {
        target.X += delta.X;
        target.Y += delta.Y;
        target.Z += delta.Z;
    }

    #endregion Add/Remove a value from target

    /// <summary>
    /// Adds round functionality to Vector3 from user-defined integer precision
    /// </summary>
    /// <param name="target">Vector3</param>
    /// <param name="precision">Integer</param>
    public static void Round(this ref Vector3 target, int precision)
    {
        //TODO - throw an exception on negative precision

        target.X = (float)Math.Round(target.X, precision);
        target.Y = (float)Math.Round(target.Y, precision);
        target.Z = (float)Math.Round(target.Z, precision);
    }

    /// <summary>
    /// Adds round functionality to Vector3 from user-defined integer precision
    /// </summary>
    /// <param name="target">Vector3</param>
    /// <param name="precision">Integer</param>
    public static Vector3 GetNewRounded(this Vector3 target, int precision)
    {
        return new Vector3((float)Math.Round(target.X, precision),
        (float)Math.Round(target.Y, precision),
        (float)Math.Round(target.Z, precision));
    }

    /// <summary>
    /// Converts a Vector3 to a Quaternion
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static Quaternion ToQuaternion(this Vector3 target)
    {
        return Quaternion.CreateFromYawPitchRoll(target.Y, target.X, target.Z);
    }

    /// <summary>
    /// Converts a Vector3 to a Quaternion
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static void ToQuaternion(this Vector3 target, ref Quaternion quaternion)
    {
        quaternion = Quaternion.CreateFromYawPitchRoll(target.Y, target.X, target.Z);
    }
}