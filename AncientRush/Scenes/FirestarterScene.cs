using System;
using AncientRush.Scenes.Controls;
using Bridge.Html5;
using Bridge.Pixi;

namespace AncientRush.Scenes
{
    public class FirestarterScene : Scene
    {
        private Texture leftTexture = Texture.FromImage("assets/Firestarter_1.png");
        private Texture rightTexture = Texture.FromImage("assets/Firestarter_2.png");
        private Sprite hand;
        private Sprite arrow;
        private Side currentSide;
        private ProgressBar progressBar;
        private float k = -2000;

        public FirestarterScene()
        {
            progressBar = new ProgressBar
            {
                Width = 780,
                Height = 50,
                Color = 0x00FF00,
                DarkColor = 0x00AA00,
                Sprite =
                {
                    Position = new Point(10, 10)
                }
            };
            Container.AddChild(progressBar.Sprite);
            arrow = new Sprite(Texture.FromImage("assets/Arrow.png"))
            {
                Anchor = new Point(1, 0.5f),
                Position = new Point(200, 150)
            };
            Container.AddChild(arrow);
            hand = new Sprite(rightTexture)
            {
                Anchor = new Point(0.5f, 0.5f),
                Position = new Point(500, 360)
            };
            currentSide = Side.Right;
            Container.AddChild(hand);
            Document.AddEventListener(EventType.KeyDown, KeyPressed);
        }

        private void KeyPressed(Event @event)
        {
            var e = @event.As<KeyboardEvent>();
            switch (e.Key)
            {
                case "ArrowLeft":
                    CurrentSide = Side.Left;
                    break;
                case "ArrowRight":
                    CurrentSide = Side.Right;
                    break;
                default: return;
            }
        }

        private Side CurrentSide
        {
            set
            {
                if (currentSide == value) return;
                currentSide = value;
                switch (value)
                {
                    case Side.Left:
                        hand.Texture = leftTexture;
                        arrow.Rotation = Pixi.DegToRad * 180;
                        break;
                    case Side.Right:
                        hand.Texture = rightTexture;
                        arrow.Rotation = 0;
                        break;
                }
                progressBar.Progress += 0.1f;
            }
        }

        public override void Update(double delta)
        {
            progressBar.Progress += (float)delta/k;
            progressBar.Update();
        }

        private enum Side
        {
            Left,
            Right
        }
    }
}