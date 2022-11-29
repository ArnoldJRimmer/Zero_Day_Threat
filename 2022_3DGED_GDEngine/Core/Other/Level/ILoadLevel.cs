namespace GD.Engine
{
    /// <summary>
    /// Any level will implment this interface and provide these methods
    /// </summary>
    public interface ILoadLevel
    {
        public void InitializeLevel(string title, float worldScale);

        public void SetTitle(string title);

        public void LoadMediaAssets();

        public void LoadSounds();

        public void LoadTextures();

        public void LoadModels();

        public void InitializeCurves();

        public void InitializeRails();

        public void InitializeScenes();

        public void InitializeEffects();

        public void InitializeCameras();

        public void InitializeDrawnContent(float worldScale);
    }
}