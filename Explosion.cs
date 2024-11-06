using Raylib_cs;
using System.Diagnostics;
using System.Numerics;

namespace Squelette
{
    public class Explosion
    {
        public const float EXPLOSIONRADIUS = 120;
        public float ExplosionTime = 0;
        public int Stade = 0;
        public Vector2 Position;
        public float Degats;
        public bool DegatFait = false;
        private Rectangle rctSource;
        private Rectangle rctDestination;  
        private Texture2D explosionTexture;
   
        public Explosion(Vector2 pos, float Degats)
        {  
            Position = pos;
            this.Degats = Degats;  
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
                Stade++;
            }

            rctSource = new(explosionTexture.Width / 8 * Stade, 0, explosionTexture.Width / 8, explosionTexture.Height);
            Raylib.DrawTexturePro(explosionTexture, rctSource, new Rectangle(Position - new Vector2(100, 100), 200, 200), new(0, 0), 0, Color.White);


            if (Stade >= 8)
            {
                destroy = true;
            }

            return destroy;
        }
    }
}
