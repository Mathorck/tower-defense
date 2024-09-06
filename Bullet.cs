using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Squelette
{
    internal class Bullet
    {
        public Vector2 Position;
        public float Rotation;
        public float Size;

        private Rectangle rctSource;
        public Rectangle rctDest;
        private Vector2 origin;
        private Texture2D texture;
        private int type;
        

        public Bullet(float rotation, Vector2 position, int type, float Size)
        {
            Rotation = rotation;
            Position = position;
            this.Size = Size;
            this.type = type;
            switch (type)
            {
                case 1:
                    this.texture = Raylib.LoadTexture(@"./images/Cannon/Bullet_Cannon.png");
                    break;
                case 2:
                    this.texture = Raylib.LoadTexture(@"./images/Cannon/Bullet_MG.png");
                    break;
                case 3:
                    this.texture = Raylib.LoadTexture(@"./images/Cannon/Missile.png");
                    break;
            }
            int frameWidth = texture.Width;
            int frameHeight = texture.Height;
            rctDest = new Rectangle(Position.X + 0, Position.Y + 0, frameWidth / 2 * Size, frameHeight / 2 * Size);
            rctSource = new Rectangle(0, 0, frameWidth, frameHeight);
            origin = new Vector2(frameWidth / 4, frameHeight / 4);
        }
        public void Draw()
        {
            Vector2 direction = new Vector2((float)Math.Cos((Rotation-90) * (Math.PI / 180.0)), (float)Math.Sin((Rotation-90) * (Math.PI / 180.0)));

            Position += direction * 15; 

            rctDest.X = Position.X;
            rctDest.Y = Position.Y;

            Raylib.DrawTexturePro(texture, rctSource, rctDest, origin, Rotation, Color.White);

            
        }
        public Bullet Destroy()
        {
            if (type == 3)
            {
                // Explode
            }
            Raylib.UnloadTexture(texture);
            return this;
        }
    }
}
