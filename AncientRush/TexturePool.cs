using Bridge.Pixi;

namespace AncientRush
{
    public class TexturePool
    {
        private const string Folder = "assets";

        private static Texture Get(string name)
        {
            return Texture.FromImage(Folder + "/" + name + ".png");
        }

        public Texture Firestarter1 = Get("Firestarter_1");
        public Texture Firestarter2 = Get("Firestarter_2");
        public Texture Arrow = Get("Arrow");
        public Texture Map = Get("Map");
        public Texture CollectibleStick = Get("CollectibleStick");
        public Texture CollectibleTinder = Get("CollectibleTinder");
        public Texture CaveMan0 = Get("CaveMan0");
        public Texture CaveMan1 = Get("CaveMan1");
        public Texture CaveMan2 = Get("CaveMan2");
        public Texture CaveMan3 = Get("CaveMan3");
        public Texture ArrowBoard = Get("ArrowBoard");
        public Texture ArrowLeft = Get("ArrowLeft");
        public Texture ArrowRight = Get("ArrowRight");
        public Texture ArrowUp = Get("ArrowUp");
        public Texture ArrowDown = Get("ArrowDown");
    }
}