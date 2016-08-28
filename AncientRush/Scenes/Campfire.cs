using Bridge.Pixi;
using Bridge.Pixi.Extras;

namespace AncientRush.Scenes
{
    public class Campfire
    {
        private MovieClip idle;
        private Sprite extinguish;

        public Campfire()
        {
            Container = new Container();
            idle = new MovieClip(new[] { App.Textures.Campfire0, App.Textures.Campfire1 })
            {
                Loop = true,
                AnimationSpeed = 0.1f
            };
            Container.AddChild(idle);
            extinguish = new Sprite(App.Textures.Campfire2)
            {
                Visible = false
            };
            Container.AddChild(extinguish);
            idle.Play();
        }

        public Container Container { get; private set; }

        public void BeginExtinguish()
        {
            idle.Stop();
            idle.Visible = false;
            extinguish.Visible = true;
        }

        public void FinishExtinguish()
        {
            extinguish.Texture = App.Textures.Campfire3;
        }
    }
}