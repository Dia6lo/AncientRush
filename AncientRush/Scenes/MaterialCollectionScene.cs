using System;
using System.Collections.Generic;
using Bridge.Html5;
using Bridge.Pixi;
using Bridge.Pixi.Extras;
using Text = Bridge.Pixi.Text;

namespace AncientRush.Scenes
{
    public class MaterialCollectionScene : GameScene
    {
        private float headingAngle;
        private const float Speed = 5;
        private bool isMoving;
        private bool isRotating;
        private Direction rotation;
        private bool isHoldingMaterial;
        private Material currentMaterial;
        private readonly List<Sprite> sticks = new List<Sprite>();
        private readonly List<Sprite> tinders = new List<Sprite>();
        private readonly Random random = new Random();
        private const int StickCount = 2;
        private const int TinderCount = 3;
        private readonly MovieClip caveMan;
        private readonly Sprite tipArrow;
        private float tipArrowDirection = 1;
        private int collectedSticks;
        private int collectedTinders;

        public MaterialCollectionScene() : base("Collect tinder and sticks!", 400, 100)
        {
            var map = new Sprite(App.Textures.Map);
            Container.AddChild(map);
            GenerateMaterials(sticks, App.Textures.CollectibleStick, StickCount);
            GenerateMaterials(tinders, App.Textures.CollectibleTinder, TinderCount);
            var caveManTextures = new[]
            {
                App.Textures.CaveMan0,
                App.Textures.CaveMan1,
                App.Textures.CaveMan2,
                App.Textures.CaveMan3
            };
            tipArrow = new Sprite(App.Textures.Arrow)
            {
                Position = new Point(100, 100),
                Visible = false,
            };
            Container.AddChild(tipArrow);
            caveMan = new MovieClip(caveManTextures)
            {
                Anchor = new Point(0.5f, 0.5f),
                Position = new Point(100, 100),
                Loop = true,
                AnimationSpeed = 0.1f
            };
            Container.AddChild(caveMan);
            Action<Event> onKeyDown = OnKeyDown;
            Action<Event> onKeyUp = OnKeyUp;
            Document.AddEventListener(EventType.KeyDown, onKeyDown);
            Document.AddEventListener(EventType.KeyUp, onKeyUp);
            Closing += () =>
            {
                Document.RemoveEventListener(EventType.KeyDown, onKeyDown);
                Document.RemoveEventListener(EventType.KeyUp, onKeyUp);
            };
            SetupControls(Direction.Up, Direction.Left, Direction.Right);
            AddOverlay();
        }

        private void GenerateMaterials(List<Sprite> sprites, Texture texture, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var sprite = new Sprite(texture)
                {
                    Position = GenerateMaterialPosition(),
                    Anchor = new Point(0.5f, 0.5f)
                };
                Container.AddChild(sprite);
                sprites.Add(sprite);
            }
        }

        private Point GenerateMaterialPosition()
        {
            float x = 0;
            float y = 0;
            while (x < 200 && y < 200)
            {
                x = GetRandomFloat(100, 600);
                y = GetRandomFloat(100, 400);
            }
            return new Point(x, y);
        }

        private float GetRandomFloat(float min, float max)
        {
            var range = max - min;
            var offset = range * (float) random.NextDouble();
            return min + offset;
        }

        private float HeadingRadians
        {
            get { return headingAngle * Pixi.DegToRad; }
        }

        private Point HeadingVector
        {
            get { return new Point((float) Math.Cos(HeadingRadians), (float) Math.Sin(HeadingRadians)); }
        }

        private void OnKeyDown(Event e)
        {
            if (IsGoalOnScreen) return;
            var key = e.As<KeyboardEvent>().GetKey();
            switch (key)
            {
                case Key.Up:
                    isMoving = true;
                    caveMan.GotoAndPlay(1);
                    break;
                case Key.Left:
                    if (!isRotating)
                    {
                        isRotating = true;
                        rotation = Direction.Left;
                    }
                    break;
                case Key.Right:
                    if (!isRotating)
                    {
                        isRotating = true;
                        rotation = Direction.Right;
                    }
                    break;
                default: return;
            }
        }

        private void OnKeyUp(Event e)
        {
            if (IsGoalOnScreen) return;
            var key = e.As<KeyboardEvent>().GetKey();
            switch (key)
            {
                case Key.Up:
                    isMoving = false;
                    caveMan.GotoAndStop(0);
                    break;
                case Key.Left:
                    if (isRotating && rotation == Direction.Left)
                        isRotating = false;
                    break;
                case Key.Right:
                    if (isRotating && rotation == Direction.Right)
                        isRotating = false;
                    break;
                default: return;
            }
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            if (IsGoalOnScreen) return;
            if (isRotating)
            {
                var angleOffset = 5 * (rotation == Direction.Right ? 1 : -1);
                headingAngle += angleOffset;
            }
            caveMan.Rotation = HeadingRadians;
            if (isMoving)
            {
                caveMan.Position = caveMan.Position.Add(HeadingVector.Multiply(Speed));
                caveMan.Position.X = caveMan.Position.X.Clamp(100, 700);
                caveMan.Position.Y = caveMan.Position.Y.Clamp(100, 500);
            }
            CheckCaveCollision();
            CheckGoals();
            CheckCollisions(sticks, Material.Stick);
            CheckCollisions(tinders, Material.Tinder);
            UpdateTipArrow();
        }

        private void CheckGoals()
        {
            if (collectedTinders != TinderCount || collectedSticks != StickCount)
                return;
            Open<FirestarterScene>();
        }

        private void UpdateTipArrow()
        {
            tipArrow.Position.Y += tipArrowDirection;
            if (tipArrow.Position.Y >= 110 || tipArrow.Position.Y <= 100)
                tipArrowDirection *= -1;
        }

        private void CheckCaveCollision()
        {
            if (!isHoldingMaterial) return;
            var cavePosition = new Point(125, 110);
            var range = caveMan.Position.Subtract(cavePosition).Length();
            if (range > 20) return;
            switch (currentMaterial)
            {
                case Material.Stick:
                    collectedSticks++;
                    break;
                case Material.Tinder:
                    collectedTinders++;
                    break;
            }
            isHoldingMaterial = false;
            tipArrow.Visible = false;
        }

        private void CheckCollisions(List<Sprite> sprites, Material material)
        {
            if (isHoldingMaterial) return;
            foreach (var sprite in sprites)
            {
                if (!sprite.Visible) continue;
                var range = caveMan.Position.Subtract(sprite.Position).Length();
                if (range > 20) continue;
                sprite.Visible = false;
                isHoldingMaterial = true;
                tipArrow.Visible = true;
                currentMaterial = material;
            }
        }
    }

    public enum Material
    {
        Stick,
        Tinder
    }
}