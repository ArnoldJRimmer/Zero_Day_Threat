#region Pre-compiler directives

#define HI_RES

#endregion

using GD.Engine.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GD.App
{

    public class AppData
    {
        #region Graphics

#if HI_RES
        public static readonly Vector2 APP_RESOLUTION = Resolutions.SixteenNine.HD;
        public static readonly Vector2 boundingScreen = Resolutions.SixteenNine.HD;

#else
        public static readonly Vector2 APP_RESOLUTION = Resolutions.FourThree.VGA;
        public static readonly Vector2 boundingScreen = Resolutions.FourThree.VGA;
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

        #region Camera - Security Camera

        public static readonly float SECURITY_CAMERA_MAX_ANGLE = 45;
        public static readonly float SECURITY_CAMERA_ANGULAR_SPEED_MUL = 50;
        public static readonly Vector3 SECURITY_CAMERA_ROTATION_AXIS = new Vector3(0, 1, 0);

        #endregion Camera - Security Camera

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
        public static readonly Vector2 PLAYER_ROTATE_SPEED_VECTOR2 = new Vector2(0.0004f, 0.0004f);

        #endregion Movement Constants

        #region GameObect names
        public static readonly string SATILITE_GAMEOBJECT_NAME = "Satilite Model";
        public static readonly string CONSOLE_GAMEOBJECT_NAME = "Console Model";
        #endregion GameObject names
    }
}