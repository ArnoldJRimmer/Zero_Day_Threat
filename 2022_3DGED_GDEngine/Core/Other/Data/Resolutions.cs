using Microsoft.Xna.Framework;

namespace GD.Engine.Data
{
    /// <summary>
    /// Provide user-friendly mechanism to access screen resolutions
    /// </summary>
    /// <see cref="GD.App.Main.InitializeGraphics(Microsoft.Xna.Framework.Vector2, bool)"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Display_resolution"/>
    public sealed class Resolutions
    {
        public sealed class FourThree
        {
            public static readonly Vector2 VGA = new Vector2(640, 480);
            public static readonly Vector2 XGA = new Vector2(1024, 768);
            public static readonly Vector2 SXGA = new Vector2(1280, 1024);
            public static readonly Vector2 UXGA = new Vector2(1600, 1200);
        }

        public sealed class SixteenNine
        {
            public static readonly Vector2 HD = new Vector2(1280, 720);
            public static readonly Vector2 FullHD = new Vector2(1920, 1080);
            public static readonly Vector2 WQHHD = new Vector2(2560, 1440);
        }

        //TODO - add resolutions and ratios, if required
    }
}