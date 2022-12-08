using GD.Engine.Globals;
using GD.Engine.Inputs;
using Microsoft.Xna.Framework;

namespace GD.Engine
{
    public class Collider2D : Component
    {
        #region Fields

        private Rectangle bounds;

        #endregion Fields

        #region Properties

        public Rectangle Bounds { get => bounds; set => bounds = value; }

        #endregion Properties

        public Collider2D(GameObject gameObject, Renderer2D renderer2D)
        {
            var scaledDimensions = gameObject.Transform.Scale.To2D() * renderer2D.Material.UnscaledDimensions;

            bounds = new Rectangle(
              (int)gameObject.Transform.Translation.X,
              (int)gameObject.Transform.Translation.Y,
              (int)scaledDimensions.X,
              (int)scaledDimensions.Y);
        }

        public override void Update(GameTime gameTime)
        {
            CheckMouseOver();
            base.Update(gameTime);
        }

        public virtual void CheckMouseOver()
        {
            if (bounds.Intersects(Input.Mouse.Bounds))
            {
                //check for hover
                HandleMouseHover();

                //check for scroll
                var scrollDelta = Input.Mouse.GetDeltaFromScrollWheel();
                if (scrollDelta != 0)
                    HandleMouseScroll(scrollDelta);

                //check for clicks
                if (Input.Mouse.WasJustClicked(Inputs.MouseButton.Left))
                    HandleMouseClick(Inputs.MouseButton.Left);
                else if (Input.Mouse.WasJustClicked(Inputs.MouseButton.Middle))
                    HandleMouseClick(Inputs.MouseButton.Middle);
                else if (Input.Mouse.WasJustClicked(Inputs.MouseButton.Right))
                    HandleMouseClick(Inputs.MouseButton.Right);
            }
        }

        protected virtual void HandleMouseScroll(int scrollDelta)
        {
        }

        protected virtual void HandleMouseClick(MouseButton mouseButton)
        {
        }

        protected virtual void HandleMouseHover()
        {
        }
    }
}