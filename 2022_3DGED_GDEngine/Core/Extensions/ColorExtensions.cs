using Microsoft.Xna.Framework;

public static class ColorExtensions
{
    public static Color Lerp(this Color start, Color end, float lerpFactor, byte alpha)
    {
        //Lerp between R, G, B, and A channels for each color
        return new Color((int)MathHelper.Lerp(start.R, end.R, lerpFactor),
                    (int)MathHelper.Lerp(start.G, end.G, lerpFactor),
                        (int)MathHelper.Lerp(start.B, end.B, lerpFactor),
                        alpha);
    }
}