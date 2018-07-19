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

        public void Init()
        {
            ContextSettings settings = new ContextSettings();
            settings.DepthBits = 24;
            settings.MajorVersion = 3;
            settings.MinorVersion = 3;

            _window = new RenderWindow(new VideoMode((uint)Globals.windowSize.X, (uint)Globals.windowSize.Y), "Space Invader");
            _window.SetFramerateLimit(60);
        }

        void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            Close();
        }
        public void Close()
        {
            _window.Close();
        }

        public void Clear()
        {
            _window.Clear();
        }

        public void Update()
        {
            _window.Display();
        }

        public void DrawPlayer(ref Player player)
        {
            _window.Draw(player.playerRect);

            for(int i = 0; i < player.projectiles.Count; i++)
            {
                _window.Draw(player.projectiles[i].projectileRect);
            }
        }
        public void DrawInvaders(ref Invader[,] invaders)
        {
            for (int i = 0; i < invaders.GetLength(0); i++)
            {
                for (int j = 0; j < invaders.GetLength(1); j++)
                {
                    if (invaders[i, j] != null) // checks if the element is null
                    {
                        _window.Draw(invaders[i, j].invaderRect);
                        for (int p = 0; p < invaders[i, j].projectiles.Count; p++)
                        {
                            _window.Draw(invaders[i, j].projectiles[p].projectileRect);
                        }
                    }
                }
            }
        }

        public void CheckForEvents()
        {
            _window.DispatchEvents();
            _window.Closed += OnClose;
        }

        public bool IsOpen()
        {
            return _window.IsOpen;
        }



        private static Display _Instance; // Singleton
        private Display() { }
        public static Display GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new Display();
            }

            return _Instance;
        }
    }
}

