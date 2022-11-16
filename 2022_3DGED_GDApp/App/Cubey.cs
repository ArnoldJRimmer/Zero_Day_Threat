using GD.Core;
using GD.Engine;
using GD.Engine.Events;
using GD.Engine.Globals;
using GD.Engine.Inputs;
using GD.Engine.Managers;
using GD.Engine.Parameters;
using GD.Engine.Utilities;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Application = GD.Engine.Globals.Application;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Cue = GD.Engine.Managers.Cue;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace GD.Engine
{
    public class Cubey
    {
        //Break down what i need it to do
        //1: Be able to draw 6 faces and make one cube
        //2: What is a cube made of
        // - GameObject
        private GameObject face;
        
        public GameObject CubeyBoi(string objectname,Vector3 scale,Vector3 rot, Vector3 translation,IEffect effect,Texture2D texture, Mesh mesh,Color color)
        {
            face = null;
            face = new GameObject(objectname, ObjectType.Static, RenderType.Opaque);
            face.Transform = new Transform(scale,rot,translation);
            face.AddComponent(new Renderer(effect, new Material(texture,1,color),mesh));
            return face;
        }


    }
}
