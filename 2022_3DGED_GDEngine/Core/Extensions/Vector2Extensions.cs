//step 1 - give class name ending in Extensions
//step 2 - no namespace
//step 3 - static class
using Microsoft.Xna.Framework;
using System;

public static class Vector2Extensions
{
    //Given myVec with (-1, 5) we could do myVec.Absolute() => (1, 5)

    //step 4 - add new method
    public static void Absolute(this ref Vector2 target)
    {
        target.X = Math.Abs(target.X);
        target.Y = Math.Abs(target.Y);
    }

    public static Vector2 Lerp(this ref Vector2 start, Vector2 end, float lerpFactor)
    {
        return new Vector2(MathHelper.Lerp(start.X, end.X, lerpFactor), MathHelper.Lerp(start.Y, end.Y, lerpFactor));
    }

    ///// <summary>
    /////
    ///// </summary>
    ///// <param name="start"></param>
    ///// <param name="end"></param>
    ///// <param name="lerpFactor"></param>
    ///// <returns></returns>
    ///// <see cref="https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/"/>
    //public static Vector2 SmoothStep(this ref Vector2 start, Vector2 end, float lerpFactor)
    //{
    //    float dot = Vector2.Dot(start, end);
    //    MathHelper.Clamp(dot, -1.0f, 1.0f);
    //    float theta = (float)Math.Acos(dot) * lerpFactor;
    //    Vector2 relativeVec = end - start * dot;
    //    relativeVec.Normalize();
    //    return ((start * (float)Math.Cos(theta)) + (relativeVec * (float)Math.Sin(theta)));
    //}
}