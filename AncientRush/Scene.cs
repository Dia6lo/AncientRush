using System;
using Bridge.Pixi;

namespace AncientRush
{
    public abstract class Scene
    {
        private float timer;
        private Graphics fade;
        private int fadeDuration;

        protected Scene()
        {
            Container = new Container();
        }

        public void FadeIn(int fadeDuration = 1000)
        {
            this.fadeDuration = fadeDuration;
            fade = new Graphics()
                .LineStyle(1)
                .BeginFill(0)
                .DrawRect(0, 0, App.Width, App.Height)
                .EndFill();
            Container.AddChild(fade);
            Appearing = true;
            timer = 0;
        }

        protected bool Appearing { get; private set; }

        public Container Container { get; private set; }

        public virtual void Update(double delta)
        {
            if (!Appearing) return;
            timer += (float) delta;
            fade.Alpha = Utility.Lerp(timer / fadeDuration, 1, 0);
            if (fade.Alpha <= 0)
                Appearing = false;
        }

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