using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace GD.Engine.Timer
{
    /// <summary>
    /// Provide coroutines (as in Unity) for use in delaying game events
    /// </summary>
    /// <see cref="https://raw.githubusercontent.com/demonixis/C3DE/master/C3DE/Utils/Coroutine.cs"/>
    public sealed class CoroutineManager : GameComponent
    {
        #region Statics

        private static Stopwatch stopWatch = new Stopwatch();

        #endregion Statics

        #region Fields

        private List<IEnumerator> routines;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Returns the number of active coroutines.
        /// </summary>
        public int Count => routines.Count;

        /// <summary>
        /// Indicates if the coroutine manager is running.
        /// </summary>
        public bool Running => routines.Count > 0;

        #endregion Properties

        #region Constructors

        public CoroutineManager(Game game) : base(game)
        {
            routines = new List<IEnumerator>();
        }

        #endregion Constructors

        #region Actions - Start/Stop

        /// <summary>
        /// Add the coroutine to the manager.
        /// </summary>
        /// <param name="routine"></param>
        public void Start(IEnumerator routine) => routines.Add(routine);

        /// <summary>
        /// Remove the coroutine from the manager.
        /// </summary>
        /// <param name="routine"></param>
        public void Stop(IEnumerator routine) => routines.Remove(routine);

        /// <summary>
        /// Stop all coroutines.
        /// </summary>
        public void StopAll() => routines.Clear();

        private bool MoveNext(IEnumerator routine)
        {
            if (routine.Current is IEnumerator)
            {
                if (MoveNext((IEnumerator)routine.Current))
                    return true;
            }

            return routine.MoveNext();
        }

        /// <summary>
        /// A coroutine that waits for n seconds. It depends of the time scale.
        /// </summary>
        /// <returns>The for seconds.</returns>
        /// <param name="time">Time.</param>
        public static IEnumerator WaitForSeconds(float time)
        {
            stopWatch = Stopwatch.StartNew();
            while (stopWatch.Elapsed.TotalSeconds < time)
                yield return 0;
        }

        /// <summary>
        /// A coroutine that waits for n seconds. It doesn't depend of the time scale.
        /// </summary>
        /// <returns>The for real seconds.</returns>
        /// <param name="time">Time.</param>
        public static IEnumerator WaitForRealSeconds(float time, float timeScale)
        {
            stopWatch = Stopwatch.StartNew();
            while (stopWatch.Elapsed.TotalSeconds * timeScale < time)
                yield return 0;
        }

        #endregion Actions - Start/Stop

        #region Actions - Update

        public override void Update(GameTime gameTime)
        {
            for (var i = 0; i < routines.Count; i++)
            {
                if (routines[i].Current is IEnumerator)
                {
                    if (MoveNext((IEnumerator)routines[i].Current))
                        continue;
                }

                if (!routines[i].MoveNext())
                    routines.RemoveAt(i--);
            }
        }

        #endregion Actions - Update
    }
}