using System;
using AncientRush.Scenes.Controls;
using Bridge.Html5;
using Bridge.Pixi;

namespace AncientRush.Scenes
{
    public class FirestarterScene : GameScene
    {
        private readonly Texture leftTexture = App.Textures.Firestarter1;
        private readonly Texture rightTexture = App.Textures.Firestarter2;
        private readonly Sprite hand;
        private readonly ProgressBar progressBar;
        private const float ProgressBarReductionSpeed = -6000;
        private Direction currentDirection;

        public FirestarterScene(): base("Start fire!", 200, 100)
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
            hand = new Sprite(rightTexture)
            {
                Anchor = new Point(0.5f, 0.5f),
                Position = new Point(500, 360)
            };
            CurrentDirection = Direction.Right;
            Container.AddChild(hand);
            Document.AddEventListener(EventType.KeyDown, KeyPressed);
            AddOverlay();
        }

        private void KeyPressed(Event e)
        {
            if (IsGoalOnScreen) return;
            var key = e.As<KeyboardEvent>().GetKey();
            switch (key)
            {
                case Key.Left:
                    CurrentDirection = Direction.Left;
                    break;
                case Key.Right:
                    CurrentDirection = Direction.Right;
                    break;
                default: return;
            }
        }

        private Direction CurrentDirection
        {
            set
            {
                if (currentDirection == value) return;
                currentDirection = value;
                switch (value)
                {
                    case Direction.Left:
                        hand.Texture = leftTexture;
                        ArrowBoard.Disable(Direction.Left);
                        ArrowBoard.Enable(Direction.Right);
                        break;
                    case Direction.Right:
                        hand.Texture = rightTexture;
                        ArrowBoard.Disable(Direction.Right);
                        ArrowBoard.Enable(Direction.Left);
                        break;
                }
                progressBar.Progress += 0.025f;
            }
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            if (IsGoalOnScreen) return;
            progressBar.Progress += (float)delta/ProgressBarReductionSpeed;
            progressBar.Update();
        }
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}