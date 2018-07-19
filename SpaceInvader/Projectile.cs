using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace SpaceInvader
{
    class Projectile
    {
        public bool playerProjectile = false;
        public bool isDead = false;
        public RectangleShape projectileRect = new RectangleShape(new Vector2f(5, 20));
        private Vector2f velocity = new Vector2f(0, 05f);

        public Projectile(float positionX, float positionY, bool playerProjectile = false)
        {
            this.playerProjectile = playerProjectile;
            Vector2f position = new Vector2f(positionX, positionY); // for convertion reasons
            projectileRect.Position = position;
        }

        public void Update()
        {
            if (playerProjectile)
            {
                projectileRect.Position -= velocity;
            }
            else
            {
                projectileRect.Position += velocity;
            }

            LifeSpan();
        }

        public void LifeSpan() // Add a life span for the projectile
        {
            if (projectileRect.Position.Y < 0 && playerProjectile)
            {
                isDead = true;
            }
            else if(projectileRect.Position.Y > Globals.windowSize.Y && !playerProjectile)
            {
                isDead = true;
            }
        }
    }
}
