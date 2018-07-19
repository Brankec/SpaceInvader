using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace SpaceInvader
{
    class Projectile
    {
        public bool playerProjectile = true;
        public bool isDead = false;
        public RectangleShape projectileRect = new RectangleShape(new Vector2f(2, 10));
        private Vector2f velocity = new Vector2f(0, 0.5f);

        public Projectile(float positionX, float positionY)
        {
            Vector2f position = new Vector2f(positionX, positionY); // for convertion reasons
            projectileRect.Position = position;
        }

        public void shoot()
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
            if (projectileRect.Position.Y < 0)
            {
                isDead = true;
            }
        }
    }
}
