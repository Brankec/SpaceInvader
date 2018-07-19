using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SpaceInvader
{
    class Invader
    {
        public RectangleShape invaderRect = new RectangleShape(new Vector2f(40, 40));
        public bool isDead = false;
        private static Vector2f velocity = new Vector2f(0.1f, 0);
        private Vector2i invaderPosition = new Vector2i(0, 0);

        private static float levelPosition; // The invaders position in the grid 5*11

        public Invader(Vector2i position)
        {
            invaderPosition = position;
            levelPosition = (Globals.windowSize.Y / invaderRect.Size.Y * 3) * invaderPosition.Y;
            invaderRect.Position = new Vector2f(invaderRect.Size.X*2 + (Globals.windowSize.X / invaderRect.Size.X * 3) * invaderPosition.X, invaderRect.Size.Y * 2 + (Globals.windowSize.Y / invaderRect.Size.Y*3) * invaderPosition.Y); // Initial position
        }

        public void UpdateInvader()
        {
            MoveInvader();
        }
        private void MoveInvader()
        {
            if (!(invaderRect.Position.X < 0 && velocity.X < 0) &&
                !((invaderRect.Position.X + invaderRect.Size.X) > Globals.windowSize.X && velocity.X > 0))
            {
                invaderRect.Position = new Vector2f(invaderRect.Position.X + velocity.X, invaderRect.Position.Y + velocity.Y);
            }
            else
            {
                velocity.X *= -1;
            }

            //if (isCollide)
            //{
            //    invaderRect.Position = new Vector2f(invaderRect.Position.X, invaderRect.Position.Y + 50);
            //}
        }


        public void TrackPlayerProjectile(ref List<Projectile> playerProjectile)
        {
            for (int i = 0; i < playerProjectile.Count; i++)
            {
                if (playerProjectile[i].projectileRect.GetGlobalBounds().Intersects(invaderRect.GetGlobalBounds())) // checks if the projectile is within the bounds
                {
                    isDead = true;
                    playerProjectile[i].isDead = true;
                }
            }
        }
    }
}
