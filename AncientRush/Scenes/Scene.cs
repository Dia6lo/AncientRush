using Bridge.Pixi;

namespace AncientRush.Scenes
{
    public abstract class Scene
    {
        protected Scene()
        {
            Container = new Container();
        }

        public Container Container { get; private set; }

        public virtual void Update() { }

        protected void Open(Scene scene)
        {
            App.SceneManager.Open(scene);
        }

        protected void Open<T>() where T: Scene, new()
        {
            App.SceneManager.Open<T>();
        }
    }
}