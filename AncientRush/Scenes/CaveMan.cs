using Bridge.Pixi;
using Bridge.Pixi.Extras;

namespace AncientRush.Scenes
{
    public class CaveMan
    {
        private MovieClip idle;
        private Sprite emotions;

        public CaveMan()
        {
            Container = new Container();
            idle = new MovieClip(new[] {App.Textures.CaveManMenu0, App.Textures.CaveManMenu1})
            {
                Loop = true,
                AnimationSpeed = 0.05f
            };
            Container.AddChild(idle);
            emotions = new Sprite(App.Textures.CaveManMenu2)
            {
                Visible = false
            };
            Container.AddChild(emotions);
            idle.Play();
        }

        public Container Container { get; private set; }

        public void NoticeChanges()
        {
            idle.Stop();
            idle.Visible = false;
            emotions.Visible = true;
        }

        public void BecomeSad()
        {
            emotions.Texture = App.Textures.CaveManMenu3;
        }
    }
}