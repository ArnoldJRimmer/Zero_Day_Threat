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
}