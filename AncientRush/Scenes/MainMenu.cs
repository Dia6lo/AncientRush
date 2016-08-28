using Bridge.Pixi;
using Bridge.Pixi.Interaction;

namespace AncientRush.Scenes
{
    public class MainMenu : Scene
    {
        private static bool firstTime = true;
        private readonly CaveMan caveMan;
        private readonly Campfire campfire;
        private float timer;
        private bool startPressed;
        private Clouds clouds;
        private Sprite sprite;
        private Sprite title;

        public MainMenu()
        {
            clouds = new Clouds();
            Container.AddChild(clouds.Container);
            var bg = new Sprite(App.Textures.MainMenuBG);
            Container.AddChild(bg);
            caveMan = new CaveMan {Container = {Position = new Point(25, 250)}};
            Container.AddChild(caveMan.Container);
            campfire = new Campfire { Container = { Position = new Point(250, 350) } };
            Container.AddChild(campfire.Container);
            title = new Sprite(App.Textures.Title)
            {
                Position = new Point(50, 50),
                Visible = firstTime
            };
            firstTime = false;
            Container.AddChild(title);
            sprite = Sprite.FromImage("assets/Start.png");
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
            sprite.Visible = false;
        }

        public override void Update(double delta)
        {
            clouds.Update();
            if (!startPressed) return;
            timer += (float)delta;
            if (timer < 1000)
            {
                title.Alpha = Lerp(timer / 1000, 1, 0);
                return;
            }
            if (timer < 2000)
            {
                campfire.BeginExtinguish();
                caveMan.NoticeChanges();
                return;
            }
            if (timer < 3000)
            {
                campfire.FinishExtinguish();
                caveMan.BecomeSad();
            }
            else
                Open<MaterialCollectionScene>();
        }
    }
}