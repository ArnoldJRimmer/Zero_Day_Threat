namespace GD.Engine
{
    /// <summary>
    /// Create a behaviour that we can attach to a game object that stores the objects value, description, and any other information we think might be useful to have when we pick the object.
    /// </summary>
    public class PickupBehaviour : Component
    {
        #region Fields

        private string desc;
        private int value;

        //add other fields e.g. string cueName - used to play sound when we pick object up

        #endregion Fields

        #region Properties

        public string Desc { get => desc; }
        public int Value { get => value; }

        #endregion Properties

        #region Constructors

        public PickupBehaviour(string desc, int value)
        {
            this.desc = desc;
            this.value = value;
        }

        #endregion Constructors
    }
}