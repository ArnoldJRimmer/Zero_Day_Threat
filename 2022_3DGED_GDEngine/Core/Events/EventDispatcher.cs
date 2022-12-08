using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GD.Engine.Events
{
    public enum EventRepeatType : sbyte
    {
        Once,
        OnceAfter,
        Many
    }

    /// <summary>
    /// Encapsulates the fields of an event within the game
    /// </summary>
    public class EventData
    {
        #region Fields

        private EventCategoryType eventCategoryType;
        private EventActionType eventActionType;
        private object[] parameters;

        #endregion Fields

        #region Properties

        public EventCategoryType EventCategoryType { get => eventCategoryType; set => eventCategoryType = value; }
        public EventActionType EventActionType { get => eventActionType; set => eventActionType = value; }
        public object[] Parameters { get => parameters; set => parameters = value; }

        #endregion Properties

        #region Constructors

        public EventData(EventCategoryType eventCategoryType,
            EventActionType eventActionType)
            : this(eventCategoryType, eventActionType, null)
        {
        }

        public EventData(EventCategoryType eventCategoryType,
            EventActionType eventActionType, object[] parameters)
        {
            EventCategoryType = eventCategoryType;
            EventActionType = eventActionType;
            Parameters = parameters;
        }

        #endregion Constructors

        #region Housekeeping

        public override bool Equals(object obj)
        {
            return obj is EventData data &&
                   eventCategoryType == data.eventCategoryType &&
                   eventActionType == data.eventActionType &&
                   EqualityComparer<object[]>.Default.Equals(parameters, data.parameters);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(eventCategoryType, eventActionType, parameters);
        }

        public override string ToString()
        {
            if (parameters == null)
                return $"{eventCategoryType}, {eventActionType}, [no params]";
            else
            {
                string parametersAsString = String.Join(",", Array.ConvertAll(parameters, item => item.ToString()));
                return $"{eventCategoryType}, {eventActionType}, {parametersAsString}";
            }
        }

        #endregion Housekeeping
    }

    /// <summary>
    /// Supports subscription and notification to system wide events
    /// </summary>
    public class EventDispatcher : GameComponent
    {
        #region Statics

        /// <summary>
        /// Controls access to the queue and prevents the same event from existing in the queue for a single update cycle (e.g. when playing a sound based on keyboard press)
        /// </summary>
        private static HashSet<EventData> sentinelSet;

        /// <summary>
        /// Stores FIFO queue of the events sent in the game
        /// </summary>
        private static Queue<EventData> queue;

        /// <summary>
        /// Stores mapping of event category to delegate function provided by the subscriber
        /// </summary>
        private static Dictionary<EventCategoryType, List<EventHandlerDelegate>> dictionary;

        #endregion Statics

        #region Delegates

        public delegate void EventHandlerDelegate(EventData eventData);

        #endregion Delegates

        #region Constructors

        public EventDispatcher(Game game) : base(game)
        {
            queue = new Queue<EventData>();
            sentinelSet = new HashSet<EventData>();
            dictionary = new Dictionary<EventCategoryType,
                List<EventHandlerDelegate>>();
        }

        #endregion Constructors

        #region Subscribe & Publish

        /// <summary>
        /// Called by a component to subscribe to a category of events
        /// </summary>
        /// <param name="eventCategoryType">EventCategoryType</param>
        /// <param name="del">EventHandlerDelegate</para
        public static void Subscribe(EventCategoryType eventCategoryType,
            EventHandlerDelegate del)
        {
            if (!dictionary.ContainsKey(eventCategoryType))
                dictionary.Add(eventCategoryType, new List<EventHandlerDelegate>());

            dictionary[eventCategoryType].Add(del);
        }

        /// <summary>
        /// Called by a component to unsubscribe to a category of events
        /// </summary>
        /// <param name="eventCategoryType">EventCategoryType</param>
        /// <param name="del">EventHandlerDelegate</param>
        /// <returns></returns>
        public static bool Unsubscribe(EventCategoryType eventCategoryType,
            EventHandlerDelegate del)
        {
            if (dictionary.ContainsKey(eventCategoryType))
            {
                List<EventHandlerDelegate> list = dictionary[eventCategoryType];
                list.Remove(del);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Called by a sender to raise an event with category, action and optional parameters
        /// e.g. Sound, OnPlay, "celebrate.wav"
        /// </summary>
        /// <param name="eventData">EventData</param>
        public static void Raise(EventData eventData)
        {
            if (!sentinelSet.Contains(eventData))
            {
                queue.Enqueue(eventData);
                sentinelSet.Add(eventData);
            }
        }

        #endregion Subscribe & Publish

        #region Update

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                //access the event
                EventData eventData = queue.Dequeue();

                //process the event and notify the subscribing method
                if (dictionary.ContainsKey(eventData.EventCategoryType))
                    foreach (EventHandlerDelegate delegateFunction in dictionary[eventData.EventCategoryType])
                        delegateFunction(eventData);

                //remove from sentinel set
                sentinelSet.Remove(eventData);
            }
        }

        #endregion Update
    }
}