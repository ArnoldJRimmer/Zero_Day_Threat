using GD.Engine.Inputs;

namespace GD.Engine.Globals
{
    /// <summary>
    /// Static class that contains input objects used in the engine.
    /// </summary>
    public class Input //TODO - : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets or sets keyboard inputs
        /// </summary>
        public static KeyboardComponent Keys { get; set; }

        /// <summary>
        /// Gets or sets mouse inputs.
        /// </summary>
        public static MouseComponent Mouse { get; set; }

        /// <summary>
        /// Gets or sets gamepad inputs.
        /// </summary>
        public static GamepadComponent Gamepad { get; set; }

        #endregion Properties
    }
}