using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Squelette
{
    internal class Canon
    {
        private float porteeTir = 50f;
        private int niveau = 1;
        private float degats = 10f;
        private float vitesseDattaque = 1.5f;
        public Vector2 Position;
        public Vector2 PositionCanon;
        public int tourChoisie = 0;
        private Texture2D Base = Raylib.LoadTexture(@"./images/Cannon/Tower.png");
        private Texture2D Cannon;
        private bool textureActive = false;
        private float rotation = 0f;

        private int frameWidth;
        private int frameHeight;
        private Rectangle sourceRec;
        private Rectangle destRec;
        private Vector2 origin = new(70,180);

        public int Niveau { get { return niveau; } }
        public float PorteeTir { get { return porteeTir; } }
        public float Degats { get { return degats; } }
        public float VitesseDattaque { get { return vitesseDattaque; } }
        public int TourChoisie
        {
            get { return tourChoisie; }
            set
            {
                tourChoisie = value;
                setTextureCanon();
            }
        }



        public Canon()
        {
            
        }
        public Canon(Vector2 position)
        {
            Position = position;
        }
        public Canon(Vector2 position, int tourChoisie)
        {
            Position = position;
            this.tourChoisie = tourChoisie;
            setTextureCanon();
        }

        public void NiveauSup()
        {
            switch (niveau)
            {
                case 1:
                    niveau = 2;

                    break;
                case 2:
                    niveau = 3;

                    break;
            }
            setTextureCanon();
        }

        public void Draw()
        {
            Raylib.DrawCircleV(Position, 40f, Color.White);
            Raylib.DrawTextureEx(Base, Position - new Vector2(35, 35), 0, 0.30f, Color.White);
            Raylib.DrawTexturePro(Cannon, sourceRec, destRec, origin, rotation, Color.White);
            
        }
        public void Place(Vector2 mousePosition)
        {
            Raylib.DrawTextureEx(Base, mousePosition - new Vector2(35, 35), 0, 0.30f, Color.White);
        }
        public void setRotation(float rotation)
        {
            this.rotation = rotation + 90;
        }
        public void FireBall()
        {

        }

        private void setTextureCanon()
        {
            switch (TourChoisie)
            {
                case 1:
                    switch (Niveau)
                    {
                        case 1:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Cannon.png");
                            break;
                        case 2:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Cannon2.png");
                            break;
                        case 3:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Cannon3.png");
                            break;
                    }
                    break;
                case 2:
                    switch (Niveau)
                    {
                        case 1:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/MG.png");
                            break;
                        case 2:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/MG2.png");
                            break;
                        case 3:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/MG3.png");
                            break;
                    }
                    break;
                case 3:
                    switch (Niveau)
                    {
                        case 1:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Missile_Launcher.png");
                            break;
                        case 2:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Missile_Launcher2.png");
                            break;
                        case 3:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Missile_Launcher3.png");
                            break;
                    }
                    break;
            }
            textureActive = true;
            frameWidth = Cannon.Width;
            frameHeight = Cannon.Height;
            destRec = new Rectangle(Position.X + 0, Position.Y + 0, frameWidth / 3, frameHeight / 3);
            sourceRec = new Rectangle(0, 0, frameWidth, frameHeight);
            origin = new Vector2(frameWidth/2/3, frameHeight/4*3/3);

        }

    }
}
