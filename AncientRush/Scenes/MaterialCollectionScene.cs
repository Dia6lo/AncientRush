using System;
using System.Collections.Generic;
using Bridge.Html5;
using Bridge.Pixi;
using Bridge.Pixi.Extras;

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
        private Material material;
        private List<Sprite> sticks = new List<Sprite>();
        private List<Sprite> tinders = new List<Sprite>();
        private Random random = new Random();
        private const int StickCount = 2;
        private const int TinderCount = 3;
        private MovieClip caveMan;

        public MaterialCollectionScene()
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
            caveMan = new MovieClip(caveManTextures)
            {
                Anchor = new Point(0.5f, 0.5f),
                Position = new Point(100, 100),
                Loop = true,
                AnimationSpeed = 0.1f
            };
            Container.AddChild(caveMan);
            Document.AddEventListener(EventType.KeyDown, OnKeyDown);
            Document.AddEventListener(EventType.KeyUp, OnKeyUp);
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
        }
    }

    public enum Material
    {
        Stick,
        Tinder
    }
}