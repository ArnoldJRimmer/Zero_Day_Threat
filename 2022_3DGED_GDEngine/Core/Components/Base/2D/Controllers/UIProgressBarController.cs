using GD.Engine.Events;
using Microsoft.Xna.Framework;
using System;

namespace GD.Engine
{
    public class UIProgressBarController : Component
    {
        #region Fields

        private int currentValue;
        private int maxValue;
        private int startValue;

        #endregion Fields

        #region Properties

        public int CurrentValue
        {
            get
            {
                return currentValue;
            }
            set
            {
                currentValue = ((value >= 0) && (value <= maxValue)) ? value : 0;
            }
        }

        private TextureMaterial2D textureMaterial2D;

        public int MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                maxValue = (value >= 0) ? value : 0;
            }
        }

        public int StartValue
        {
            get
            {
                return startValue;
            }
            set
            {
                startValue = (value >= 0) ? value : 0;
            }
        }

        #endregion Properties

        public UIProgressBarController(int startValue, int maxValue)
        {
            StartValue = startValue;
            MaxValue = maxValue;
            CurrentValue = startValue;

            //listen for UI events to change the SourceRectangle
            EventDispatcher.Subscribe(EventCategoryType.UI, HandleEvents);
        }

        #region Action - Events

        private void HandleEvents(EventData eventData)
        {
            if (eventData.EventActionType == EventActionType.OnHealthDelta)
            {
                //get the name of the ui object targeted by this event
                var targetObjectName = eventData.Parameters[0] as string;

                //is it for me?
                if (targetObjectName != null && gameObject.Name.Equals(targetObjectName))
                    CurrentValue = currentValue + (int)eventData.Parameters[1];
            }
        }

        #endregion Action - Events

        public override void Update(GameTime gameTime)
        {
            //get material to access source rectangle
            if (textureMaterial2D == null)
                textureMaterial2D = gameObject.GetComponent<Renderer2D>().Material as TextureMaterial2D;

            //how much of a percentage of the width of the image does the current value represent?
            var widthMultiplier = (float)currentValue / maxValue;

            //now set the amount of visible rectangle using the current value
            textureMaterial2D.SourceRectangleWidth = (int)Math.Round(widthMultiplier * textureMaterial2D.OriginalSourceRectangle.Width);
        }
    }
}