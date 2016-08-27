using AncientRush.Scenes;

namespace AncientRush
{
    public class SceneManager
    {
        public Scene CurrentScene { get; private set; }

        public void Open(Scene scene)
        {
            CurrentScene = scene;
        }

        public void Open<T>() where T : Scene, new()
        {
            Open(new T());
        }
    }
}