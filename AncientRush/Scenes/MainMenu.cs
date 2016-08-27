using Bridge.Pixi;
using Bridge.Pixi.Interaction;

namespace AncientRush.Scenes
{
    public class MainMenu : Scene
    {
        public MainMenu()
        {
            var sprite = Sprite.FromImage("assets/Start.png");
            sprite.Position.Set(400, 300);
            sprite.Anchor.Set(0.5f, 0.5f);
            sprite.Interactive = true;
            sprite.OnMouseDown(OnDown);
            sprite.OnTouchStart(OnDown);
            Container.AddChild(sprite);
        }

        private void OnDown(InteractionEvent arg)
        {
           Open<TestScene>();
        }

        public override void Update()
        {
        }
    }
}