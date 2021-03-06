﻿using SFML.System;

namespace SpaceInvader
{
    class Program
    {
        static bool gameOver = false;
        static int invaderCount = 0;

        static void Main(string[] args)
        {
            Globals g = new Globals();// DO NOT USE
            Barrier[] barriers = new Barrier[4]; // The original had 4 barriers
            InitializeBarriers(ref barriers); 
            Invader[,] invaders = new Invader[5, 11]; // The original had a grid of 5 x 11 invaders
            InitializeInvaders(ref invaders);

            Player player = Player.GetInstance();

            Display display = Display.GetInstance();
            display.Init();
            while (display.IsOpen())
            {
                display.Clear(); // Clears the window from the previous display
                display.CheckForEvents(); // Checks for events such as closing the window

                player.PlayerControls();
                Loop(ref player, ref invaders, ref barriers);

                if (player.isDead || gameOver)
                {
                    display.Close();
                    for(int i = 0; i < 100; i++)
                    {
                        System.Console.WriteLine("YOU LOST!");
                    }
                }

                display.DrawPlayer(ref player); // Player rectangle being passed to draw
                display.DrawInvaders(ref invaders); // Invader rectangle being passed to draw
                display.DrawBarriers(ref barriers);
                display.Update(); // Draws on the window from the buffer
            }
        }

        static void InitializeInvaders(ref Invader[,] invaders)
        {
            for(int i = 0; i < invaders.GetLength(0); i++)
            {
                for (int j = 0; j < invaders.GetLength(1); j++)
                {
                    invaders[i, j] = new Invader(new Vector2i(j, i), j, i); // j is row, i is column
                }
            }
        }
        static void InitializeBarriers(ref Barrier[] barriers)
        {
            for (int i = 0; i < barriers.Length; i++)
            {
                barriers[i] = new Barrier(i);
            }
        }

        static void Loop(ref Player player, ref Invader[,] invaders, ref Barrier[] barriers) // Loops through all the invaders and updates their position
        {
            for (int i = 0; i < invaders.GetLength(0); i++)
            {
                for (int j = 0; j < invaders.GetLength(1); j++)
                {
                    if (invaders[i, j] != null) // checks if the element is already null
                    {
                        if (!invaders[i, j].isDead)
                        {
                            invaders[i, j].UpdateInvader();
                            invaders[i, j].TrackPlayerProjectile(ref player.projectiles); // tracks if the player hit invader
                            player.TrackInvaderProjectile(ref invaders[i, j].projectiles); // tracks if the invader hit the player

                            for (int p = 0; p < barriers.Length; p++)
                            {
                                barriers[p].TrackProjectile(ref invaders[i, j].projectiles);
                            }

                        }
                        else
                        {
                            invaders[i, j] = null;
                        }
                    }
                }
            }
            for(int i = 0; i < barriers.Length; i++)
            {
                barriers[i].TrackProjectile(ref player.projectiles);
            }

            if (invaderCount == invaders.GetLength(0)* invaders.GetLength(1))
            {
                gameOver = true;
            }
        }
    }
}
