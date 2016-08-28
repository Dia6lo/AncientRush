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

        protected static float SinMotion(float time, float currentTime, float from, float to)
        {
            var v = (float)Math.Sin(currentTime / time * (Math.PI / 2));
            return Lerp(v, from, to);
        }

        private static float Lerp(float amount, float value1, float value2)
        {
            return value1 + (value2 - value1) * amount;
        }
    }
}