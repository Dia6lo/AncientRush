using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.Pixi;

namespace AncientRush.Scenes.Controls
{
    public class Clouds
    {
        private readonly Dictionary<Sprite, float> clouds = new Dictionary<Sprite, float>();
        private readonly Random random = new Random();

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
            foreach (var cloud in clouds.ToList())
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