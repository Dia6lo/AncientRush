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
        private const float ProgressBarReductionSpeed = -10000;
        private Direction currentDirection;
        private float timer;
        private Sprite smoke;
        private float progress;
        private Action<Event> keyPressed;

        public FirestarterScene(): base("Mine rubs sticks!", 300, 100)
        {
            var bg = new Sprite(App.Textures.CaveClose);
            Container.AddChild(bg);
            smoke = new Sprite(App.Textures.Smoke0)
            {
                Alpha = 0.75f,
                Anchor = new Point(0.5f, 0.5f),
                Position = new Point(400, 475),
                Visible = false
            };
            keyPressed = KeyPressed;
            hand = new Sprite(rightTexture)
            {
                Anchor = new Point(0.5f, 0.5f),
                Position = new Point(500, 360)
            };
            CurrentDirection = Direction.Right;
            Container.AddChild(hand);
            Container.AddChild(smoke);
            Document.AddEventListener(EventType.KeyDown, keyPressed);
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
                SetProgress(progress + 0.025f);
            }
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            SetProgress(progress + (float)delta / ProgressBarReductionSpeed);
        }

        private void SetProgress(float value)
        {
            if (progress == value) return;
            if (value <= 0)
            {
                progress = 0;
                smoke.Visible = false;
                return;
            }
            if (value > 0.1f)
                smoke.Visible = true;
            if (value >= 1)
            {
                smoke.Texture = App.Textures.Smoke2;
                progress = 1;
                Document.RemoveEventListener(EventType.KeyDown, keyPressed);
                Open<MainMenu>();
                return;
            }
            progress = value;
            if (value <= 0.33f)
                smoke.Texture = App.Textures.Smoke0;
            else if (value <= 0.66f)
                smoke.Texture = App.Textures.Smoke1;
            else
                smoke.Texture = App.Textures.Smoke2;
        }
    }
}