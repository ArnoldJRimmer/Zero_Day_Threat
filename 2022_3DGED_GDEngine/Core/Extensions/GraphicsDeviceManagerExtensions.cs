using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public static class GraphicsDeviceManagerExtensions
{
    /// <summary>
    /// Gets the scale factor necessary to apply to a texture2D object to fit the current graphics resolution
    /// </summary>
    /// <param name="graphics">GraphicsDeviceManager</param>
    /// <param name="texture">Texture2D</param>
    /// <returns>Vector2</returns>
    public static Vector2 GetScaleFactorForResolution(this GraphicsDeviceManager graphics, Texture2D texture, Vector2 additionalScaleFactor)
    {
        //prevents passing Vector2.Zero
        if (additionalScaleFactor.Length() == 0)
            additionalScaleFactor = Vector2.One;

        return additionalScaleFactor * new Vector2((float)Math.Ceiling((float)graphics.PreferredBackBufferWidth / texture.Width),
            (float)Math.Ceiling((float)graphics.PreferredBackBufferHeight / texture.Height));
    }
}