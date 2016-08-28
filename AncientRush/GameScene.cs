using System;
using AncientRush.Scenes;
using Bridge.Pixi;

namespace AncientRush
{
    public abstract class GameScene : Scene
    {
        private float timer;
        private readonly Graphics goalContainer;
        private readonly Text goalText;
        private GoalState goalState = GoalState.DropIn;
        private float dropInHeight;
        private float stayHeight;
        private float dropOutHeight;

        public GameScene(string goalText, float tipWidth, float tipHeight)
        {
            dropInHeight = -tipHeight;
            stayHeight = App.Height / 2 - tipHeight / 2;
            dropOutHeight = App.Height;
            ArrowBoard = new ArrowBoard();
            ArrowBoard.Container.Position = new Point(675, 10);
            goalContainer = new Graphics
            {
                Position = new Point(App.Width / 2 - tipWidth / 2, App.Height / 2 - tipHeight / 2)
            };
            goalContainer.LineStyle(2)
                .BeginFill(0x2F9E2F)
                .DrawRoundedRect(0, 0, tipWidth, tipHeight, 10)
                .EndFill();
            this.goalText = new Text(goalText)
            {
                Anchor = new Point(0.5f, 0.5f),
                Position = new Point(tipWidth / 2, tipHeight / 2)
            };
            goalContainer.AddChild(this.goalText);
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
            Container.AddChild(goalContainer);
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            if (goalState == GoalState.DroppedOut) return;
            timer += (float) delta;
            switch (goalState)
            {
                case GoalState.DropIn:
                    HandleGoalDropIn();
                    break;
                case GoalState.Stay:
                    HandleGoalStay();
                    break;
                case GoalState.DropOut:
                    HandleGoalDropOut();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleGoalDropIn()
        {
            if (timer < 250)
                goalContainer.Position.Y = SinMotion(250, timer, dropInHeight, stayHeight);
            else
            {
                goalState = GoalState.Stay;
                timer = 0;
            }
        }

        private void HandleGoalStay()
        {
            if (timer <= 750) return;
            goalState = GoalState.DropOut;
            timer = 0;
        }

        private void HandleGoalDropOut()
        {
            if (timer < 250)
                goalContainer.Position.Y = SinMotion(250, timer, stayHeight, dropOutHeight);
            else
            {
                goalState = GoalState.DroppedOut;
                IsGoalOnScreen = false;
            }

        }

        private enum GoalState
        {
            DropIn,
            Stay,
            DropOut,
            DroppedOut
        }
    }
}