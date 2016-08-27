using Bridge.Pixi;
using Bridge.Pixi.Interaction;

namespace AncientRush.Scenes
{
    public class TestScene : Scene
    {
        private static Sprite sprite;

        public TestScene()
        {
            sprite = Sprite.FromImage("assets/Shia.png");
            sprite.Position.Set(200, 200);
            sprite.Anchor.Set(0.5f, 0.5f);
            sprite.Interactive = true;
            sprite.OnMouseDown(OnDown);
            sprite.OnTouchStart(OnDown);
            Container.AddChild(sprite);
        }

        private static void OnDown(InteractionEvent arg)
        {
            sprite.Scale.X = 0.3f;
            sprite.Scale.Y = 0.3f;
        }

        public override void Update()
        {
            sprite.Rotation += 0.2f;
        }
    }
}