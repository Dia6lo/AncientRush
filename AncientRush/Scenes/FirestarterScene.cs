using System;
using AncientRush.Scenes.Controls;
using Bridge.Html5;
using Bridge.Pixi;

namespace AncientRush.Scenes
{
    public class FirestarterScene : Scene
    {
        private readonly Texture leftTexture = App.Textures.Firestarter1;
        private readonly Texture rightTexture = App.Textures.Firestarter2;
        private readonly Sprite hand;
        private readonly Sprite arrow;
        private readonly ProgressBar progressBar;
        private const float ProgressBarReductionSpeed = -6000;
        private Side currentSide;

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
            arrow = new Sprite(App.Textures.Arrow)
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

        private void KeyPressed(Event e)
        {
            var key = e.As<KeyboardEvent>().GetKey();
            switch (key)
            {
                case Key.Left:
                    CurrentSide = Side.Left;
                    break;
                case Key.Right:
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
                progressBar.Progress += 0.025f;
            }
        }

        public override void Update(double delta)
        {
            progressBar.Progress += (float)delta/ProgressBarReductionSpeed;
            progressBar.Update();
        }
    }

    public enum Side
    {
        Left,
        Right
    }
}