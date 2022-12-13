using GD.Engine.Events;
using GD.Engine.Inputs;
using System.Collections.Generic;

namespace GD.Engine
{
    public class ButtonCollider2D : Collider2D
    {
        #region Fields

        private Dictionary<MouseButton, List<EventData>> events;

        #endregion Fields

        #region Properties

        public Dictionary<MouseButton, List<EventData>> Events { get => events; set => events = value; }

        #endregion Properties

        #region Constructors

        public ButtonCollider2D(GameObject gameObject, Renderer2D spriteRenderer) : base(gameObject, spriteRenderer)
        {
            events = new Dictionary<MouseButton, List<EventData>>();
        }

        #endregion Constructors

        public void AddEvent(MouseButton mouseButton, EventData eventData)
        {
            if (events.ContainsKey(mouseButton))
            {
                events[mouseButton].Add(eventData);
            }
            else
            {
                List<EventData> list = new List<EventData>();
                list.Add(eventData);
                events.Add(mouseButton, list);
            }
        }

        #region Actions - Handle Mouse Events

        protected override void HandleMouseClick(MouseButton mouseButton)
        {
            List<EventData> eventList;

            //get events for this button
            events.TryGetValue(mouseButton, out eventList);

            if (eventList != null)
            {
                //raise all events
                foreach (EventData eventData in eventList)
                    EventDispatcher.Raise(eventData);
            }
        }

        protected override void HandleMouseHover()
        {
            List<EventData> eventList;

            //get events for this button
            events.TryGetValue(MouseButton.Hover, out eventList);

            if (eventList != null)
            {
                //raise all events
                foreach (EventData eventData in eventList)
                    EventDispatcher.Raise(eventData);
            }
        }

        protected override void HandleMouseScroll(int scrollDelta)
        {
            List<EventData> eventList;

            //get events for this button
            events.TryGetValue(MouseButton.Hover, out eventList);

            if (eventList != null)
            {
                //raise all events
                foreach (EventData eventData in eventList)
                    EventDispatcher.Raise(eventData);
            }
        }

        #endregion Actions - Handle Mouse Events
    }
}