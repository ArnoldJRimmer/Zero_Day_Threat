using GD.Engine.Events;
using GD.Engine.Globals;
using Microsoft.Xna.Framework;
using System;

namespace GD.Engine
{
    public class PickingManager : PausableGameComponent
    {
        private float pickStartDistance;
        private float pickEndDistance;
        private Predicate<GameObject> collisionPredicate;
        private GameObject pickedObject;

        public PickingManager(Game game,
           float pickStartDistance, float pickEndDistance,
           Predicate<GameObject> collisionPredicate)
           : base(game)
        {
            this.pickStartDistance = pickStartDistance;
            this.pickEndDistance = pickEndDistance;
            this.collisionPredicate = collisionPredicate;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsUpdated)
                HandleMouse(gameTime);

            base.Update(gameTime);
        }

        protected virtual void HandleMouse(GameTime gameTime)
        {
            //if (Input.Mouse.WasJustClicked(Inputs.MouseButton.Left))
            GetPickedObject();

            //predicate was matched and i should notify
            if (pickedObject != null)
            {
                object[] parameters = { pickedObject };
                EventDispatcher.Raise(new EventData(EventCategoryType.Picking,
                    EventActionType.OnObjectPicked, parameters));

                if (Input.Mouse.WasJustClicked(Inputs.MouseButton.Right))
                    EventDispatcher.Raise(new EventData(EventCategoryType.GameObject,
                        EventActionType.OnRemoveObject, parameters));
            }
            else
            {
                EventDispatcher.Raise(new EventData(EventCategoryType.Picking,
                 EventActionType.OnNoObjectPicked));
            }
        }

        private void GetPickedObject()
        {
            Vector3 pos;
            Vector3 normal;

            pickedObject =
                Input.Mouse.GetPickedObject(Application.CameraManager.ActiveCamera,
                Application.CameraManager.ActiveCameraTransform,
                pickStartDistance, pickEndDistance,
                out pos, out normal) as GameObject;

            if (pickedObject != null && collisionPredicate(pickedObject))
            {
                //TODO - here is where you decide what to do!
                // System.Diagnostics.Debug.WriteLine(pickedObject.GameObjectType);

                //var behaviour = pickedObject.GetComponent<PickupBehaviour>();

                //if (behaviour != null)
                //   System.Diagnostics.Debug.WriteLine($"{behaviour.Desc} - {behaviour.Value}");

                ////OnRemove
                ////OnPlay3D/2D

                //object[] parameters = { pickedObject };
                ////OnPickup
                //EventDispatcher.Raise(new EventData(
                //    EventCategoryType.GameObject,
                //    EventActionType.OnRemoveObject,
                //    parameters));

                //System.Diagnostics.Debug.WriteLine($"{pickedObject.Name} - {pickedObject.ID}");
            }
            else
                pickedObject = null;
        }
    }
}