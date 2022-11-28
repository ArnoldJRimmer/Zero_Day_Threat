using GD.Engine.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace GD.Engine
{
    public class ForwardSceneRenderer : SceneRenderer
    {
        public ForwardSceneRenderer(GraphicsDevice graphiceDevice)
        {
            base.Initialize(graphiceDevice);
        }

        public override void Draw(GraphicsDevice graphicsDevice, Camera camera, Scene scene)
        {
            //set viewport for the active camera (e.g. split screen)
            graphicsDevice.Viewport = camera.ViewPort;

            //sort by alpha
            //   scene.Renderers.Sort((x, y) => y.Material.Alpha.CompareTo(x.Material.Alpha));

            //set opaque
            SetGraphicsStates(true);

            //draw static opaque game objects
            foreach (GameObject gameObject in scene.OpaqueList.StaticList)
                gameObject.GetComponent<Renderer>().Draw(graphicsDevice, camera);

            //draw dynamic opaque game objects
            foreach (GameObject gameObject in scene.OpaqueList.DynamicList)
                gameObject.GetComponent<Renderer>().Draw(graphicsDevice, camera);

            //set opaque
            SetGraphicsStates(false);

            //draw static transparent game objects
            foreach (GameObject gameObject in scene.TransparentList.StaticList)
                gameObject.GetComponent<Renderer>().Draw(graphicsDevice, camera);

            //draw dynamic opaque game objects
            foreach (GameObject gameObject in scene.TransparentList.DynamicList)
                gameObject.GetComponent<Renderer>().Draw(graphicsDevice, camera);
        }
    }
}