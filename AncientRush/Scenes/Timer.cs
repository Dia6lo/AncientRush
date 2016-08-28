using System;
using System.Collections.Generic;
using System.Linq;

namespace AncientRush.Scenes
{
    public class Timer
    {
        private float time;
        private readonly List<Tuple<float, Action>> subscribers = new List<Tuple<float, Action>>();
        private Tuple<float, Action> nextSubscriber;

        public void Update(double delta)
        {
            time += (float) delta;
            if (nextSubscriber == null || time < nextSubscriber.Item1) return;
            nextSubscriber.Item2();
            subscribers.Remove(nextSubscriber);
            nextSubscriber = null;
            FindNextSubscriber();
        }

        public void Subscribe(float time, Action action)
        {
            if (this.time > time) return;
            var newSubscriber = new Tuple<float, Action>(time, action);
            subscribers.Add(newSubscriber);
            if (nextSubscriber == null || time < nextSubscriber.Item1)
                nextSubscriber = newSubscriber;
        }

        public void Reset()
        {
            time = 0;
            FindNextSubscriber();
        }

        private void FindNextSubscriber()
        {
            if (subscribers.Any())
                nextSubscriber = subscribers
                    .OrderBy(s => s.Item1)
                    .First();
        }

        public float Time { get { return time; } }
    }
}