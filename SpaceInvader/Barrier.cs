using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    class Barrier
    {
        public RectangleShape barrierRect = new RectangleShape(new Vector2f(120, 70));

        public Barrier(int position)
        {
            barrierRect.Position = new Vector2f(barrierRect.Size.X/2 + (barrierRect.Size.X*2) * position, 600);
            barrierRect.FillColor = new Color(0, 0, 255);
        }

        public void TrackProjectile(ref List<Projectile> projectile)
        {
            for (int i = 0; i < projectile.Count; i++)
            {
                if (projectile[i].projectileRect.GetGlobalBounds().Intersects(barrierRect.GetGlobalBounds())) // checks if the projectile is within the bounds
                {
                    //isDead = true;
                    projectile[i].isDead = true;
                }
            }
        }
    }
}
