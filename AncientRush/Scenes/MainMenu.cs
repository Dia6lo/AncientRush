using Bridge.Pixi;
using Bridge.Pixi.Extras;
using Bridge.Pixi.Interaction;

namespace AncientRush.Scenes
{
    public class MainMenu : Scene
    {
        private Container cloudContainer = new Container();
        private CaveMan caveMan;
        private float timer;
        private bool startPressed;

        public MainMenu()
        {
            Container.AddChild(cloudContainer);
            var bg = new Sprite(App.Textures.MainMenuBG);
            Container.AddChild(bg);
            caveMan = new CaveMan {Container = {Position = new Point(25, 250)}};
            Container.AddChild(caveMan.Container);
            var sprite = Sprite.FromImage("assets/Start.png");
            sprite.Position.Set(400, 300);
            sprite.Anchor.Set(0.5f, 0.5f);
            sprite.Interactive = true;
            sprite.OnceMouseDown(OnOnceMouseDown);
            sprite.OnceTouchStart(OnOnceMouseDown);
            Container.AddChild(sprite);
        }

        private void OnOnceMouseDown(InteractionEvent arg)
        {
            startPressed = true;
            caveMan.NoticeChanges();
        }

        public override void Update(double delta)
        {
            if (!startPressed) return;
            timer += (float)delta;
            if (timer < 1000) return;
            if (timer < 2000)
                caveMan.BecomeSad();
            else
                Open<MaterialCollectionScene>();
        }
    }

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