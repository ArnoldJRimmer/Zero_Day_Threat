#region Pre-compiler directives

//#define DEMO
#define HI_RES

#endregion

using GD.Engine.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GD.App
{
#if DEMO

    public enum CameraIDType : sbyte
    {
        First,
        Third,
        Security
    }

#endif

    public class AppData
    {
        #region Graphics

#if HI_RES
        public static readonly Vector2 APP_RESOLUTION = Resolutions.SixteenNine.HD;
#else
        public static readonly Vector2 APP_RESOLUTION = Resolutions.FourThree.VGA;
#endif

        #endregion Graphics

        #region World Scale

        public static readonly float SKYBOX_WORLD_SCALE = 2000;

        #endregion World Scale

        #region Camera - General

        public static readonly float CAMERA_FOV_INCREMENT_LOW = 1;
        public static readonly float CAMERA_FOV_INCREMENT_MEDIUM = 2;
        public static readonly float CAMERA_FOV_INCREMENT_HIGH = 4;

        #endregion

        #region Camera - First Person

        public static readonly string FIRST_PERSON_CAMERA_NAME = "fpc 1";
        public static readonly float FIRST_PERSON_MOVE_SPEED = 0.009f;
        public static readonly float FIRST_PERSON_STRAFE_SPEED = 0.6f * FIRST_PERSON_MOVE_SPEED;
        public static readonly Vector3 FIRST_PERSON_DEFAULT_CAMERA_POSITION = new Vector3(0, 2, 5);

        public static readonly float FIRST_PERSON_CAMERA_FCP = 3000;
        public static readonly float FIRST_PERSON_CAMERA_NCP = 0.1f;

        public static readonly float FIRST_PERSON_HALF_FOV
             = MathHelper.PiOver2 / 2.0f;

        #endregion Camera - First Person

        #region Camera - Third Person

        public static readonly string THIRD_PERSON_CAMERA_NAME = "third person camera";

        #endregion

        #region Camera - Security Camera

        public static readonly float SECURITY_CAMERA_MAX_ANGLE = 45;
        public static readonly float SECURITY_CAMERA_ANGULAR_SPEED_MUL = 50;
        public static readonly Vector3 SECURITY_CAMERA_ROTATION_AXIS = new Vector3(0, 1, 0);
        public static readonly string SECURITY_CAMERA_NAME = "security camera 1";

        #endregion Camera - Security Camera

        #region Camera - Curve

        public static readonly string CURVE_CAMERA_NAME = "curve camera 1";

        #endregion

        #region Input Key Mappings

        public static readonly Keys[] KEYS_ONE = { Keys.W, Keys.S, Keys.A, Keys.D };
        public static readonly Keys[] KEYS_TWO = { Keys.U, Keys.J, Keys.H, Keys.K };

        #endregion Input Key Mappings

        #region Movement Constants

        public static readonly float PLAYER_MOVE_SPEED = 0.1f;
        private static readonly float PLAYER_STRAFE_SPEED_MULTIPLIER = 0.75f;
        public static readonly float PLAYER_STRAFE_SPEED = PLAYER_STRAFE_SPEED_MULTIPLIER * PLAYER_MOVE_SPEED;

        //can use either same X-Y rotation for camera controller or different
        public static readonly float PLAYER_ROTATE_SPEED_SINGLE = 0.001f;

        //why bother? can you tilt your head at the same speed as you rotate it?
        public static readonly Vector2 PLAYER_ROTATE_SPEED_VECTOR2 = new Vector2(0.0004f, 0.0003f);

        #endregion Movement Constants

        public static readonly double MAX_GAME_TIME_IN_MSECS = 2500; //180000
    }
}