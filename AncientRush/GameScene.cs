using AncientRush.Scenes;
using AncientRush.Scenes.Controls;
using Bridge.Pixi;

namespace AncientRush
{
    public abstract class GameScene : Scene
    {
        private readonly SlidingMessage goal;

        public GameScene(string goalText, float tipWidth, float tipHeight)
        {
            ArrowBoard = new ArrowBoard();
            ArrowBoard.Container.Position = new Point(675, 10);
            goal = new SlidingMessage(goalText, tipWidth, tipHeight);
            goal.DroppedOut += () => IsGoalOnScreen = false;
            IsGoalOnScreen = true;
        }

        protected ArrowBoard ArrowBoard { get; private set; }

        protected bool IsGoalOnScreen { get; private set; }

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
            Container.AddChild(goal.Container);
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            goal.Update(delta);
        }
    }
}