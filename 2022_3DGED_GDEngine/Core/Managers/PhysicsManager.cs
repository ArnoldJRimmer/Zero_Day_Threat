using BEPUphysics;
using Microsoft.Xna.Framework;

namespace GD.Engine
{
    public class PhysicsManager : GameComponent
    {
        #region Fields

        private Space space;

        #endregion Fields

        #region Properties

        public Space Space { get => space; set => space = value; }

        #endregion Properties

        #region Constructors

        public PhysicsManager(Game game) : base(game)
        {
            space = new Space();
        }

        #endregion Constructors

        #region Actions - Update

        public override void Update(GameTime gameTime)
        {
            // space.Update(gameTime.ElapsedGameTime.Milliseconds);

            space.Update();
            base.Update(gameTime);
        }

        #endregion Actions - Update
    }
}