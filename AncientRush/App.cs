using System;
using AncientRush.Scenes;
using Bridge.Html5;
using Bridge.Pixi;

namespace AncientRush
{
    public class App
    {
        private static IRenderer renderer;
        private static double currentTime;

        public static SceneManager SceneManager { get; private set; }
        public static TexturePool Textures { get; private set; }

        private static Scene CurrentScene
        {
            get { return SceneManager.CurrentScene; }
        }

        [Ready]
        public static void Main()
        {
            renderer = Pixi.AutoDetectRenderer(800, 600, new RendererOptions {BackgroundColor = 0x1099bb});
            Document.Body.AppendChild(renderer["view"].As<HTMLCanvasElement>());
            Textures = new TexturePool();
            SceneManager = new SceneManager();
            SceneManager.Open<MaterialCollectionScene>();
            Window.RequestAnimationFrame(Animate);
        }

        private static void Animate(double time)
        {
            Window.RequestAnimationFrame(Animate);
            CurrentScene.Update(time - currentTime);
            renderer.Render(CurrentScene.Container);
            currentTime = time;
        }
    }
}