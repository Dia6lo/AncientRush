using System;
using Bridge.Pixi;

namespace AncientRush.Scenes.Controls
{
    public class ArrowBoard
    {
        private Sprite board;
        private Sprite left;
        private Sprite right;
        private Sprite up;
        private Sprite down;

        public ArrowBoard()
        {
            board = new Sprite(App.Textures.ArrowBoard);
            board.AddChild(left = new Sprite(App.Textures.ArrowLeft) {Position = new Point(4, 38)});
            board.AddChild(right = new Sprite(App.Textures.ArrowRight) {Position = new Point(72, 38)});
            board.AddChild(up = new Sprite(App.Textures.ArrowUp) {Position = new Point(38, 4)});
            board.AddChild(down = new Sprite(App.Textures.ArrowDown) {Position = new Point(38, 38)});
            Disable(Direction.Up);
            Disable(Direction.Down);
            Disable(Direction.Right);
            Disable(Direction.Left);
        }

        public Container Container
        {
            get { return board; }
        }

        public void Enable(Direction direction)
        {
            GetSprite(direction).Tint = 0xFFFFFF;
        }

        public void Disable(Direction direction)
        {
            GetSprite(direction).Tint = 0x777777;
        }

        private Sprite GetSprite(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left: return left;
                case Direction.Right: return right;
                case Direction.Up: return up;
                case Direction.Down: return down;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }
    }
}