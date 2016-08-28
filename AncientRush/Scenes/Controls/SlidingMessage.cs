using System;
using Bridge.Pixi;

namespace AncientRush.Scenes.Controls
{
    public class SlidingMessage
    {
        private float timer;
        private readonly Graphics goalContainer;
        private State currentState = State.DropIn;
        private float dropInHeight;
        private float stayHeight;
        private float dropOutHeight;

        public SlidingMessage(string goalText, float tipWidth, float tipHeight)
        {
            dropInHeight = -tipHeight;
            stayHeight = App.Height / 2 - tipHeight / 2;
            dropOutHeight = App.Height;
            goalContainer = new Graphics
            {
                Position = new Point(App.Width / 2 - tipWidth / 2, App.Height / 2 - tipHeight / 2)
            };
            goalContainer.LineStyle(2)
                .BeginFill(0x2F9E2F)
                .DrawRoundedRect(0, 0, tipWidth, tipHeight, 10)
                .EndFill();
            var text = new Text(goalText)
            {
                Anchor = new Point(0.5f, 0.5f),
                Position = new Point(tipWidth / 2, tipHeight / 2)
            };
            goalContainer.AddChild(text);
        }

        public Container Container
        {
            get { return goalContainer; }
        }

        public event Action DroppedOut;

        public void Update(double delta)
        {
            if (currentState == State.DroppedOut) return;
            timer += (float)delta;
            switch (currentState)
            {
                case State.DropIn:
                    HandleGoalDropIn();
                    break;
                case State.Stay:
                    HandleGoalStay();
                    break;
                case State.DropOut:
                    HandleGoalDropOut();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleGoalDropIn()
        {
            if (timer < 250)
                goalContainer.Position.Y = Utility.SinMotion(250, timer, dropInHeight, stayHeight);
            else
            {
                currentState = State.Stay;
                timer = 0;
            }
        }

        private void HandleGoalStay()
        {
            if (timer <= 1000) return;
            currentState = State.DropOut;
            timer = 0;
        }

        private void HandleGoalDropOut()
        {
            if (timer < 250)
                goalContainer.Position.Y = Utility.SinMotion(250, timer, stayHeight, dropOutHeight);
            else
            {
                currentState = State.DroppedOut;
                if (DroppedOut != null)
                    DroppedOut();
            }

        }

        private enum State
        {
            DropIn,
            Stay,
            DropOut,
            DroppedOut
        }
    }
}