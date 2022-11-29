using GD.Engine.Managers;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GD.Engine
{
    public class ForwardSceneRenderer : SceneRenderer
    {
        private List<Renderer> renderers;

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
            renderers = scene.OpaqueList.StaticList.Renderers;
            foreach (Renderer renderer in renderers)
                renderer.Draw(graphicsDevice, camera);

            //draw dynamic opaque game objects
            renderers = scene.OpaqueList.DynamicList.Renderers;
            foreach (Renderer renderer in renderers)
                renderer.Draw(graphicsDevice, camera);

            //set opaque
            SetGraphicsStates(false);

            //draw static transparent game objects
            renderers = scene.TransparentList.StaticList.Renderers;
            foreach (Renderer renderer in renderers)
                renderer.Draw(graphicsDevice, camera);

            //draw dynamic transparent game objects
            renderers = scene.TransparentList.DynamicList.Renderers;
            foreach (Renderer renderer in renderers)
                renderer.Draw(graphicsDevice, camera);
        }
    }
}