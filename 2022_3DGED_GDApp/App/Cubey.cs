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
using System.Windows.Forms;

namespace GD.Engine
{
    public class Cubey 
    {
        //Break down what i need it to do
        //1: Be able to draw 6 faces and make one cube
        //2: What is a cube made of
        private GameObject face;

        public GameObject CubeyBoi(string objectname,Vector3 scale,Vector3 rot, Vector3 translation,BasicEffect effect,Texture2D texture, ModelMesh mesh, Keys key)
        {
            face = null;
            face = new GameObject(objectname, ObjectType.Dynamic, RenderType.Opaque);
            face.Transform = new Transform(scale,rot,translation);
            face.AddComponent(new Renderer(new GDBasicEffect(effect), new Material(texture,1),mesh));
            face.AddComponent(new CubeController(new Vector3(1, 0, 0), MathHelper.ToRadians(1.1f), key));
            return face;
        }

        public GameObject CubeyBoi(string objectname, Vector3 scale, Vector3 rot, Vector3 translation, IEffect effect, Texture2D texture, ModelMesh mesh, Color color)
        {
            face = null;
            face = new GameObject(objectname, ObjectType.Static, RenderType.Opaque);
            face.Transform = new Transform(scale, rot, translation);
            //Content.RootDirectory = "Content";
            //GraphicsDeviceManager _graphics = new GraphicsDeviceManager(this);
            //System.ArgumentNullException: 'The GraphicsDevice must not be null when creating new resources. (Parameter 'graphicsDevice')'
            //texture = Content.Load<Texture2D>("Assets/Textures/SkyBox/basicwall");
            face.AddComponent(new Renderer(effect, new Material(texture, 1, color), mesh));
            return face;
        }

        public GameObject[] Cube(GameObject face1,GameObject face2,GameObject face3 ,GameObject face4, GameObject face5, GameObject face6)
        {
            GameObject[] cube = new GameObject[] {face1, face2, face3, face4, face5, face6};
            return cube;
        }


    }
}
