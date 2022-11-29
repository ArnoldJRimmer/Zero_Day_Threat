using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GD.App
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
        public string cueName;  //"boom"
    }

    /// <summary>
    /// Countdown/up timer and we need an inventory system
    /// </summary>
    public class StateManager : GameComponent
    {
        private double maxTimeInMS;
        private double totalElapsedTimeMS;
        private List<InventoryItem> inventory;

        public StateManager(Game game, double maxTimeInMS) : base(game)
        {
            this.maxTimeInMS = maxTimeInMS;
            totalElapsedTimeMS = 0;
            inventory = new List<InventoryItem>();

            //Register
        }

        public override void Update(GameTime gameTime)
        {
            totalElapsedTimeMS += gameTime.ElapsedGameTime.Milliseconds;

            if (totalElapsedTimeMS >= maxTimeInMS)
            {
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