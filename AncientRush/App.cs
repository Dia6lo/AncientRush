using AncientRush.Scenes;
using Bridge.Html5;
using Bridge.Pixi;

namespace AncientRush
{
    public class App
    {
        private static IRenderer renderer;

        public static SceneManager SceneManager { get; private set; }

        private static Scene CurrentScene
        {
            get { return SceneManager.CurrentScene; }
        }

        [Ready]
        public static void Main()
        {
            renderer = Pixi.AutoDetectRenderer(800, 600, new RendererOptions {BackgroundColor = 0x1099bb});
            Document.Body.AppendChild(renderer.View);
            SceneManager = new SceneManager();
            SceneManager.Open<MainMenu>();
            Animate();
        }

        private static void Animate()
        {
            Window.RequestAnimationFrame(Animate);
            CurrentScene.Update();
            renderer.Render(CurrentScene.Container);
        }
    }
}