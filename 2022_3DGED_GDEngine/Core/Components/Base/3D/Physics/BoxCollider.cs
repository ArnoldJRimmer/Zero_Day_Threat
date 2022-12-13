using BEPUphysics.Entities.Prefabs;
using GD.Engine.Globals;
using Microsoft.Xna.Framework;
using Box = BEPUphysics.Entities.Prefabs.Box;

namespace GD.Engine
{
    public class BoxCollider : Component
    {
        private Box box;

        //TODO - change to get Transform and use those fields
        public BoxCollider(Vector3 position, float width, float height, float length, float mass)
        {
            box = new Box(new BEPUutilities.Vector3(position.X, position.Y, position.Z), width, height, length, mass);
            Application.PhysicsManager.Space.Add(box);
        }
    }
}