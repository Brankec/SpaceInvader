using SFML.Graphics;
using SFML.System;

namespace SpaceInvader
{
    class Globals
    {
        private static Texture[] playertxr = new Texture[2];
        public static Vector2i windowSize = new Vector2i(1920 / 2, (int)((double)1080 / (double)1.2));

        public Globals()
        {
            playertxr[0] = new Texture("C:/Users/Gejmer/Documents/Visual Studio 2017/Projects/SpaceInvader/SpaceInvader/images/invader1.png");
            playertxr[1] = new Texture("C:/Users/Gejmer/Documents/Visual Studio 2017/Projects/SpaceInvader/SpaceInvader/images/invader2.png");
        }

        public static void InvaderTexture(ref RectangleShape rect, int index)
        {
            rect.Texture = playertxr[index];
        }
    }
}
