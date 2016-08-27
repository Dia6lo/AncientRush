using Bridge.Pixi;

namespace AncientRush.Scenes
{
    public abstract class Scene
    {
        protected Scene()
        {
            Container = new Container();
        }

        public Container Container { get; }

        public abstract void Update();
    }
}