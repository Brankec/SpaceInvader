using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SpaceInvader
{
    class Display
    {
        private RenderWindow _window;

        public void init()
        {
            ContextSettings settings = new ContextSettings();
            settings.DepthBits = 24;
            settings.MajorVersion = 3;
            settings.MinorVersion = 3;

            _window = new RenderWindow(new VideoMode((uint)Globals.windowSize.X, (uint)Globals.windowSize.Y), "Space Invader");
        }

        void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            close();
        }
        public void close()
        {
            _window.Close();
        }

        public void clear()
        {
            _window.Clear();
        }

        public void update()
        {
            _window.Display();
        }

        public void drawPlayer(ref Player player)
        {
            _window.Draw(player.playerRect);

            for(int i = 0; i < player.projectiles.Count; i++)
            {
                _window.Draw(player.projectiles[i].projectileRect);
            }
        }
        public void drawInvaders(ref Invader[,] invaders)
        {
            for (int i = 0; i < invaders.GetLength(0); i++)
            {
                for (int j = 0; j < invaders.GetLength(1); j++)
                {
                    _window.Draw(invaders[i, j].rectangleShape);
                }
            }
        }

        public void checkForEvents()
        {
            _window.DispatchEvents();
            _window.Closed += OnClose;
        }

        public bool isOpen()
        {
            return _window.IsOpen;
        }



        private static Display _Instance; //Singleton
        private Display() { }
        public static Display getInstance()
        {
            if (_Instance == null)
            {
                _Instance = new Display();
            }

            return _Instance;
        }
    }
}

