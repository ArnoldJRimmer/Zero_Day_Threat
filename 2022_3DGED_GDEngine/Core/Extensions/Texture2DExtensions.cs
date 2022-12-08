using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

public static class Texture2DExtensions
{
    public static Vector2 GetCenter(this Texture2D target)
    {
        return new Vector2(target.Width / 2.0f, target.Height / 2.0f);
    }
}