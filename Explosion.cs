using Raylib_cs;
using System.Numerics;

namespace Squelette
{
    public class Explosion
    {
        public float ExplosionTime = 0;
        private int stade = 0;
        public Vector2 Position;
        Rectangle rctSource;
        Rectangle rctDestination;
        Texture2D explosionTexture;

        public Explosion(Vector2 pos)
        {
            Position = pos;
            rctDestination = new Rectangle(pos, 50, 50);
            Console.WriteLine("Explosion !!!!");
            explosionTexture = Raylib.LoadTexture("./images/Cannon/explosion.png");
        }

        public bool UpdateTimer()
        {
            ExplosionTime += 0.15f;
            bool destroy = false;

            if (ExplosionTime > 1f)
            {
                ExplosionTime = 0f;
                stade++;
                Console.WriteLine($"Stade : {stade}");
            }

            rctSource = new(explosionTexture.Width/8*stade, 0, explosionTexture.Width / 8, explosionTexture.Height);
            Raylib.DrawTexturePro(explosionTexture, rctSource, new Rectangle(Position -new Vector2(100,100), 200, 200), new(0, 0), 0, Color.White);

            
            if (stade >= 8)
            {
                Console.WriteLine("SuprrExp");
                destroy = true;
            }
            
            return destroy;
        }
    }
}
