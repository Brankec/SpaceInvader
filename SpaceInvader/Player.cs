﻿using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SpaceInvader
{
    class Player
    {
        public Vector2f velocity = new Vector2f(0, 0);
        public RectangleShape playerRect = new RectangleShape(new Vector2f(100, 50));
        public bool isDead = false;
        //public bool isFired = false;

        private Time moveStep = new Time();
        private Clock moveClock = new Clock();

        public List<Projectile> projectiles = new List<Projectile>();

        public void PlayerControls() // Player controls
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) // Right
            {
                velocity.X = 3;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A)) // Left
            {
                velocity.X = -3;
            }
            else // Stop
            {
                velocity.X = 0;
            }

            if (moveStep.AsSeconds() > 0.3f)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space)) // Fire
                {
                    Fire();
                    //isFired = true;
                }
                moveClock.Restart();
            }

            UpdatePlayer(); // Updates the player
        }
        private void UpdatePlayer()
        {
            moveStep = moveClock.ElapsedTime;

            if (!(playerRect.Position.X < 0 && velocity.X < 0) &&
                !((playerRect.Position.X + playerRect.Size.X) > Globals.windowSize.X && velocity.X > 0)) //Window bounds
            {
                playerRect.Position = new Vector2f(playerRect.Position.X + velocity.X, playerRect.Position.Y + velocity.Y); //Sets the players position
            }
            //if (isFired)
            {
                for (int i = 0; i < projectiles.Count; i++)
                {
                    if (!projectiles[i].isDead)
                    {
                        projectiles[i].Update(); // Call the method shoot which will update projectiles position;
                    }
                    else
                    {
                        projectiles.RemoveAt(i); // Removes the instance of the, out of window bounds, projectile
                        //isFired = false; // When the projectile gets destroyed isFired turns to false
                    }
                }
            }
        }
        private void Fire() // Fires the projectile upwards
        {
            projectiles.Add(new Projectile(playerRect.Position.X + playerRect.Size.X/2, playerRect.Position.Y, true));
        }
        public void TrackInvaderProjectile(ref List<Projectile> invaderProjectile)
        {
            for (int i = 0; i < invaderProjectile.Count; i++)
            {
                if (invaderProjectile[i].projectileRect.GetGlobalBounds().Intersects(playerRect.GetGlobalBounds())) // checks if the projectile is within the bounds
                {
                    isDead = true;
                    invaderProjectile[i].isDead = true;
                }
            }
        }

        private static Player _Instance; //Singleton
        private Player()
        {   //Setting up starting position(bottom middle)
            playerRect.Position = new Vector2f(playerRect.Position.X + Globals.windowSize.X/2 - playerRect.Size.X/2, Globals.windowSize.Y - (int)(playerRect.Size.Y*1.5));
            //playerRect.FillColor = new Color(0, 255, 0);

            Texture playertxr = new Texture("C:/Users/Gejmer/Documents/Visual Studio 2017/Projects/SpaceInvader/SpaceInvader/images/player.png");
            playerRect.Texture = playertxr;
        }
        public static Player GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new Player();
            }

            return _Instance;
        }
    }
}
