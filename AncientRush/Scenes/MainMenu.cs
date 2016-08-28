using System;
using Bridge.Html5;
using Bridge.Pixi;

namespace AncientRush.Scenes
{
    public class MainMenu : Scene
    {
        private static bool firstTime = true;
        private readonly CaveMan caveMan;
        private readonly Campfire campfire;
        private float timer;
        private bool spacePressed;
        private readonly Clouds clouds;
        private readonly Sprite title;
        private readonly Sprite subTitle;
        private readonly Action<Event> onKeyDown;

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
            subTitle = new Sprite(App.Textures.PressSpace)
            {
                Anchor = new Point(0.5f, 0.5f),
                Position = new Point(400, 200)
            };
            Container.AddChild(subTitle);
            onKeyDown = OnKeyDown;
            Document.AddEventListener(EventType.KeyDown, onKeyDown);
            FadeIn(firstTime ? 2500 : 500);
        }

        private void OnKeyDown(Event @event)
        {
            if (Appearing) return;
            if (!@event.As<KeyboardEvent>().Key.Equals(" ")) return;
            Document.RemoveEventListener(EventType.KeyDown, onKeyDown);
            spacePressed = true;
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            clouds.Update();
            if (Appearing || !spacePressed) return;
            timer += (float)delta;
            if (timer < 1000)
            {
                var alpha = Lerp(timer / 1000, 1, 0);
                title.Alpha = alpha;
                subTitle.Alpha = alpha;
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