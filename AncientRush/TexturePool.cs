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

        public Texture MainMenuBG = Get("MainMenu");
        public Texture CaveManMenu0 = Get("CaveManMenu0");
        public Texture CaveManMenu1 = Get("CaveManMenu1");
        public Texture CaveManMenu2 = Get("CaveManMenu2");
        public Texture CaveManMenu3 = Get("CaveManMenu3");
        public Texture Campfire0 = Get("Campfire0");
        public Texture Campfire1 = Get("Campfire1");
        public Texture Campfire2 = Get("Campfire2");
        public Texture Campfire3 = Get("Campfire3");
        public Texture Cloud = Get("Cloud");
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