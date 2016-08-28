using System;
using System.Collections.Generic;
using Bridge.Pixi;
using Bridge.Pixi.Interaction;

namespace AncientRush.Scenes
{
    public class MainMenu : Scene
    {
        private readonly CaveMan caveMan;
        private readonly Campfire campfire;
        private float timer;
        private bool startPressed;
        private Clouds clouds;

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
            campfire.BeginExtinguish();
            caveMan.NoticeChanges();
        }

        public override void Update(double delta)
        {
            clouds.Update();
            if (!startPressed) return;
            timer += (float)delta;
            if (timer < 1000) return;
            if (timer < 2000)
            {
                campfire.FinishExtinguish();
                caveMan.BecomeSad();
            }
            else
                Open<MaterialCollectionScene>();
        }
    }

    public class Clouds
    {
        private Dictionary<Sprite, float> clouds = new Dictionary<Sprite, float>();
        private Random random = new Random();

        public Clouds()
        {
            Container = new Container();
            AddCloud(new Sprite(App.Textures.Cloud) {
                Position = new Point(-App.Textures.Cloud.Width, 0),
                Scale = new Point(1, 1)},
                2);
            AddCloud(new Sprite(App.Textures.Cloud) {
                Position = new Point(App.Width, 100),
                Scale = new Point(0.75f, 0.75f)},
                -5);
        }

        public Container Container { get; private set; }

        public void Update()
        {
            var maxWidth = App.Textures.Cloud.Width;
            foreach (var cloud in clouds)
            {
                cloud.Key.X += cloud.Value;
                var finishedMoving = cloud.Value > 0 && cloud.Key.X > App.Width ||
                                     cloud.Value < 0 && cloud.Key.X < -maxWidth;
                if (!finishedMoving) continue;
                Container.RemoveChild(cloud.Key);
                clouds.Remove(cloud.Key);
            }
            var spawnNewCloud = random.Next(100) < 1;
            if (!spawnNewCloud) return;
            var scale = (float) random.Next(5, 10) / 10;
            var direction = random.Next(0, 2) == 0 ? Direction.Left : Direction.Right;
            var speed = (float) random.NextDouble() * 5;
            if (direction == Direction.Left)
                speed *= -1;
            var height = random.Next(-50, 150);
            var newCloud = new Sprite(App.Textures.Cloud)
            {
                Scale = new Point(scale, scale),
                Position = direction == Direction.Right ? new Point(-maxWidth, height) : new Point(App.Width, height)
            };
            AddCloud(newCloud, speed);
        }

        private void AddCloud(Sprite cloud, float speed)
        {
            clouds.Add(cloud, speed);
            Container.AddChild(cloud);
        }
    }
}