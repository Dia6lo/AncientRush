using AncientRush.Scenes;
using Bridge.Pixi;

namespace AncientRush
{
    public abstract class GameScene : Scene
    {
        protected ArrowBoard ArrowBoard { get; private set; }

        public GameScene()
        {
            ArrowBoard = new ArrowBoard();
            ArrowBoard.Container.Position = new Point(675, 10);
        }

        protected void SetupControls(params Direction[] directions)
        {
            foreach (var direction in directions)
            {
                ArrowBoard.Enable(direction);
            }
        }

        protected void AddOverlay()
        {
            Container.AddChild(ArrowBoard.Container);
        }
    }
}