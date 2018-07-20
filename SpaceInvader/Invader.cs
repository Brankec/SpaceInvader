using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;

namespace SpaceInvader
{
    class Invader
    {
        public RectangleShape invaderRect = new RectangleShape(new Vector2f(40, 40));
        public bool isDead = false;
        private static Vector2f velocity = new Vector2f(Globals.windowSize.X/80, 0);
        private Vector2i invaderPosition = new Vector2i(0, 0);

        public List<Projectile> projectiles = new List<Projectile>();

        private static float newLevelPosition; // The invaders position in the grid 5*11
        private static int level = 1;
        private static int tempLevel = 0;
        private static int animation = 0;

        private Time moveStep = new Time();
        private Time randomTime = new Time();
        private Clock moveClock = new Clock();
        private Clock randomClock = new Clock();
        Random rnd1; // random projectile fire
        Random rnd2; // random projectile fire
        Random rndShoot; // random projectile fire

        private Vector2i grid;

        public Invader(Vector2i position, int gridX, int gridY)
        {
            rnd1 = new Random(position.X * position.Y * 10);
            rnd2 = new Random((position.Y + 1) / (position.X + 1) * 100);
            rndShoot = new Random(invaderPosition.X + invaderPosition.Y + gridX + gridY);
            grid = new Vector2i(gridX, gridY);

            invaderPosition = position;
            UpdateLevel();
            invaderRect.Position = new Vector2f(invaderRect.Size.X*2 + (Globals.windowSize.X / invaderRect.Size.X * 3) * invaderPosition.X, newLevelPosition); // Initial position

            Globals.InvaderTexture(ref invaderRect, animation);
        }

        public void UpdateInvader()
        {

            if (level < 7)
            {
                tempLevel = level; // at high speeds the invaders get out of sequence so I limited them to speed 10 as max
            }

            moveStep = moveClock.ElapsedTime;
            randomTime = randomClock.ElapsedTime;

            MoveInvader();

            for (int i = 0; i < projectiles.Count; i++)
            {
                if (!projectiles[i].isDead)
                {
                    projectiles[i].Update();
                }
                else
                {
                    projectiles.RemoveAt(i); // Removes the instance of the, out of window bounds, projectile
                }
            }

            if (invaderPosition.Y == 4)
            {
                Fire();
            }

        }

        private void MoveInvader()
        {
            if (!(invaderRect.Position.X < 0 && velocity.X < 0) &&
                !((invaderRect.Position.X + invaderRect.Size.X) > Globals.windowSize.X && velocity.X > 0))
            {
                if (moveStep.AsSeconds() > 1f / (float)tempLevel)
                {
                    invaderRect.Position = new Vector2f(invaderRect.Position.X + velocity.X, invaderRect.Position.Y + velocity.Y);
                    Globals.InvaderTexture(ref invaderRect, animation);

                    if (animation == 0)
                    {
                        animation = 1;
                    }
                    else if (animation == 1)
                    {
                        animation = 0;
                    }

                    moveClock.Restart();
                }
            }
            else
            {
                velocity.X *= -1;
                if(level < 10)
                    level++;
            }

            UpdateLevel();

            if (invaderRect.Position.Y != newLevelPosition)
            {
                invaderRect.Position = new Vector2f(invaderRect.Position.X, newLevelPosition);
            }

        }
        private void UpdateLevel()
        {
            newLevelPosition = ((Globals.windowSize.Y / invaderRect.Size.Y * 3) * invaderPosition.Y) + 20 * level;
        }
        private void Fire()
        {
            if (randomTime.AsSeconds() > rndShoot.Next(1,20))
            {
                if (rnd1.Next(1,20) == rnd2.Next(1,20))
                {
                    projectiles.Add(new Projectile(invaderRect.Position.X + invaderRect.Size.X/2, invaderRect.Position.Y));
                }

                randomClock.Restart();
            }
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
