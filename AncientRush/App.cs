using AncientRush.Scenes;
using Bridge.Html5;
using Bridge.Pixi;

namespace AncientRush
{
    public class App
    {
        private static IRenderer renderer;
        private static Scene scene;

        [Ready]
        public static void Main()
        {
            renderer = Pixi.AutoDetectRenderer(800, 600, new RendererOptions {BackgroundColor = 0x1099bb});
            Document.Body.AppendChild(renderer.View);
            scene = new MainMenu();
            Animate();
        }

        private static void Animate()
        {
            Window.RequestAnimationFrame(Animate);
            scene.Update();
            renderer.Render(scene.Container);
        }
    }
}