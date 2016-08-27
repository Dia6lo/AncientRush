using System;
using Bridge.Pixi;

namespace AncientRush.Scenes.Controls
{
    public class ProgressBar
    {
        private Graphics graphics = new Graphics();
        private float progress;

        public event Action MaxReached;
        public event Action MinReached;

        public DisplayObject Sprite
        {
            get { return graphics; }
        }

        public int Color { get; set; }

        public int DarkColor { get; set; }

        public float Height { get; set; }

        public float Width { get; set; }

        public float Progress
        {
            get { return progress; }
            set
            {
                if (progress == value) return;
                if (value <= 0)
                {
                    progress = 0;
                    if (MinReached != null)
                        MinReached();
                }
                else if (value >= 1)
                {
                    progress = 1;
                    if (MaxReached != null)
                        MaxReached();
                }
                else
                    progress = value;
            }
        }

        public void Update()
        {
            const float radius = 10;
            const float minWidth = radius * 2;
            var progressWidth = progress == 0 ? minWidth : minWidth + (Width - minWidth) *progress;
            graphics.Clear()
                .LineStyle(2, DarkColor)
                .BeginFill(Color)
                .DrawRoundedRect(0, 0, progressWidth, Height, radius)
                .EndFill()
                .MoveTo(0, 0)
                .LineStyle(2)
                .DrawRoundedRect(0, 0, Width, Height, radius);
        }
    }
}