using System;
using Bridge.Pixi;

namespace AncientRush
{
    public abstract class Scene
    {
        protected Scene()
        {
            Container = new Container();
        }

        public Container Container { get; private set; }

        public virtual void Update(double delta) { }

        protected void Open(Scene scene)
        {
            if (Closing != null)
                Closing();
            App.SceneManager.Open(scene);
        }

        protected void Open<T>() where T: Scene, new()
        {
            if (Closing != null)
                Closing();
            App.SceneManager.Open<T>();
        }

        protected event Action Closing;
    }
}