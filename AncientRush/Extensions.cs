using System;
using Bridge.Html5;
using Bridge.Pixi;

namespace AncientRush
{
    public static class Extensions
    {
        public static void Set(this Point point, Point value)
        {
            point.Set(value.X, value.Y);
        }

        public static Point Subtract(this Point point, Point value)
        {
            return new Point(point.X - value.X, point.Y - value.Y);
        }

        public static Point Add(this Point point, Point value)
        {
            return new Point(point.X + value.X, point.Y + value.Y);
        }

        public static Point Multiply(this Point point, float value)
        {
            return new Point(point.X * value, point.Y * value);
        }

        public static void Normalize(this Point point)
        {
            var val = 1.0f/(float) Math.Sqrt(point.X* point.X + point.Y * point.Y);
            point.X *= val;
            point.Y *= val;
        }

        public static float Length(this Point point)
        {
            return (float)Math.Sqrt(point.X*point.X + point.Y*point.Y);
        }

        public static Key GetKey(this KeyboardEvent e)
        {
            switch (e.Key)
            {
                case "ArrowUp": return Key.Up;
                case "ArrowDown": return Key.Down;
                case "ArrowLeft": return Key.Left;
                case "ArrowRight": return Key.Right;
                default: return Key.Unknown;
            }
        }

        public static float Clamp(this float value, float min, float max)
        {
            return (value < min) ? min : (value > max ? max : value);
        }
    }
}