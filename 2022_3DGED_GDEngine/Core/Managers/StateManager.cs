using GD.Engine.Events;
using Microsoft.Xna.Framework;
using SharpDX.DirectWrite;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GD.Engine
{
    public enum ItemType : sbyte
    {
        Story,
        Health,
        Ammo,
        Quest,
        Prop
    }

    public class InventoryItem
    {
        public string uniqueID;
        public string name;
        public ItemType itemType;
        public string description;
        public int value;
        private string audioCueName;  //"boom"
    }

    /// <summary>
    /// Countdown/up timer and we need an inventory system
    /// </summary>
    public class StateManager : GameComponent
    {
        private float maxTimeInMS;
        private float totalElapsedTimeMS;
        private float remainingTimeMs;
        private List<InventoryItem> inventory;

        public StateManager(Game game, float maxTimeInMS) : base(game)
        {
            this.MaxTimeInMS = maxTimeInMS;
            TotalElapsedTimeMS = 0;
            Inventory = new List<InventoryItem>();
            remainingTimeMs = maxTimeInMS;
            //Register
        }

        public float MaxTimeInMS { get => maxTimeInMS; set => maxTimeInMS = value; }
        public float TotalElapsedTimeMS { get => totalElapsedTimeMS; set => totalElapsedTimeMS = value; }
        public List<InventoryItem> Inventory { get => inventory; set => inventory = value; }
        public float RemainingTimeMs { get => remainingTimeMs; set => remainingTimeMs = value; }

        public override void Update(GameTime gameTime)
        {
            TotalElapsedTimeMS += gameTime.ElapsedGameTime.Milliseconds;

            RemainingTimeMs -= gameTime.ElapsedGameTime.Milliseconds;
            if (TotalElapsedTimeMS >= MaxTimeInMS || RemainingTimeMs <=0)
            {
                CheckWinLose();
                //object[] parameters = { "You win!", totalElapsedTimeMS, "win_soundcue" };
                //EventDispatcher.Raise(
                //    new EventData(EventCategoryType.Player,
                //    EventActionType.OnLose,
                //    parameters));
            }

            //check game state
            //if win then
            //CheckWinLose()
            //show win toast
            //if lose then
            //show lose toast
            //fade to black
            //show restart screen

            base.Update(gameTime);
        }

        private bool CheckWinLose()
        {
            
            return false;
            //check individual game items
        }
    }
}