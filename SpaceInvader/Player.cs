using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SpaceInvader
{
    class Player
    {
        public Vector2f velocity = new Vector2f(0, 0);
        public RectangleShape playerRect = new RectangleShape(new Vector2f(100, 100));
        private bool isFired = false; // checked so the player only shoots once per projectile life span

        public List<Projectile> projectiles = new List<Projectile>();

        public void PlayerControls() // Player controls
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) //Right
            {
                velocity.X = 1;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A)) //Left
            {
                velocity.X = -1;
            }
            else // Stop
            {
                velocity.X = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && !isFired) // fire
            {
                fire();
                isFired = true;
            }

            UpdatePlayer(); // Updates the player
        }
        private void UpdatePlayer()
        {
            if (!(playerRect.Position.X < 0 && velocity.X < 0) &&
                !((playerRect.Position.X + playerRect.Size.X) > Globals.windowSize.X && velocity.X > 0)) //Window bounds
            {
                playerRect.Position = new Vector2f(playerRect.Position.X + velocity.X, playerRect.Position.Y + velocity.Y); //Sets the players position
            }
            if (isFired)
            {
                for (int i = 0; i < projectiles.Count; i++)
                {
                    if (!projectiles[i].isDead)
                    {
                        projectiles[i].shoot(); // Call the method shoot which will update projectiles position;
                    }
                    else
                    {
                        projectiles.RemoveAt(i); // Removes the instance of the, out of window bounds, projectile
                        isFired = false; // When the projectile gets destroyed isFired turns to false
                    }
                }
            }
        }
        private void fire() // Fires the projectile upwards
        {
            projectiles.Add(new Projectile(playerRect.Position.X + playerRect.Size.X/2, playerRect.Position.Y));
            
        }

        private static Player _Instance; //Singleton
        private Player()
        {   //Setting up starting position(bottom middle)
            playerRect.Position = new Vector2f(playerRect.Position.X + Globals.windowSize.X/2 - playerRect.Size.X/2, Globals.windowSize.Y - (int)(playerRect.Size.Y*1.5));
            playerRect.FillColor = new Color(0, 255, 0);
        }
        public static Player getInstance()
        {
            if (_Instance == null)
            {
                _Instance = new Player();
            }

            return _Instance;
        }
    }
}
