using GD.Engine.Events;
using Microsoft.Xna.Framework;

namespace GD.Engine.Managers
{
    /// <summary>
    /// Renders the active ui/menu scene
    /// </summary>
    public class UserInterfaceManager : SceneManager<Scene2D>
    {
        public UserInterfaceManager(Game game) : base(game)
        {
        }

        protected override void HandleEvent(EventData eventData)
        {
            if (eventData.EventCategoryType == EventCategoryType.Menu)
            {
                if (eventData.EventActionType == EventActionType.OnPlay)
                    StatusType = StatusType.Off;
                else if (eventData.EventActionType == EventActionType.OnPause)
                    StatusType = StatusType.Updated | StatusType.Drawn;
            }
        }
    }
}