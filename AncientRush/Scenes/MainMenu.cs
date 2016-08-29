using System;
using AncientRush.Scenes.Controls;
using Bridge.Html5;
using Bridge.Pixi;

namespace AncientRush.Scenes
{
    public class MainMenu : Scene
    {
        private static bool firstTime = true;
        private readonly CaveMan caveMan;
        private readonly Campfire campfire;
        private Timer timer;
        private bool spacePressed;
        private readonly Clouds clouds;
        private readonly Sprite title;
        private readonly Sprite subTitle;
        private readonly Action<Event> onKeyDown;
        private SlidingMessage message;

        public MainMenu()
        {
            if (App.Audio != null)
                App.Audio.Remove();
            App.Audio = new HTMLAudioElement("assets/GF.mp3");
            Document.Body.AppendChild(App.Audio);
            App.Audio.Play();
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
            Container.AddChild(title);
            subTitle = new Sprite(App.Textures.PressSpace)
            {
                Anchor = new Point(0.5f, 0.5f),
                Position = new Point(400, 200),
                Visible = firstTime
            };
            Container.AddChild(subTitle);
            onKeyDown = OnKeyDown;
            if (firstTime)
                Document.AddEventListener(EventType.KeyDown, onKeyDown);
            else
                TriggerIntro();
            FadeIn(firstTime ? 2500 : 500);
        }

        private void TriggerIntro()
        {
            spacePressed = true;
            timer = new Timer();
            timer.Subscribe(1000, () =>
            {
                message = new SlidingMessage(firstTime ? "Mine happy!" : "Mine happy again!", 300, 100);
                Container.AddChild(message.Container);
            });
            timer.Subscribe(3000, () =>
            {
                campfire.BeginExtinguish();
                caveMan.NoticeChanges();
            });
            timer.Subscribe(4000, () =>
            {
                Container.RemoveChild(message.Container);
                message = new SlidingMessage("Uh-oh", 100, 100);
                Container.AddChild(message.Container);
            });
            timer.Subscribe(6000, () =>
            {
                campfire.FinishExtinguish();
                caveMan.BecomeSad();
            });
            timer.Subscribe(7000, () =>
            {
                Container.RemoveChild(message.Container);
                message = new SlidingMessage("Mine sad", 150, 100);
                Container.AddChild(message.Container);
            });
            timer.Subscribe(9000, () =>
            {
                firstTime = false;
                Open<MaterialCollectionScene>();
            });
        }

        private void OnKeyDown(Event @event)
        {
            if (Appearing) return;
            if (!@event.As<KeyboardEvent>().Key.Equals(" ")) return;
            Document.RemoveEventListener(EventType.KeyDown, onKeyDown);
            TriggerIntro();
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            clouds.Update();
            if (Appearing || !spacePressed) return;
            timer.Update(delta);
            if (message != null)
                message.Update(delta);
            if (timer.Time > 1000) return;
            var alpha = Utility.Lerp(timer.Time / 1000, 1, 0);
            title.Alpha = alpha;
            subTitle.Alpha = alpha;
        }
    }
}