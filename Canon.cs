using Raylib_cs;
using System.Numerics;

namespace Squelette
{
    internal class Canon
    {
        private float porteeTir = 500f;
        public float hitbox = 40f;
        private int niveau = 1;
        private float degats = 10f;
        private float vitesseDattaque = 10f;
        public Vector2 Position;
        public int tourChoisie = 0;
        private Texture2D Base = Raylib.LoadTexture(@"./images/Cannon/Tower.png");
        private Texture2D Cannon;
        private float rotation = 0f;
        private float bulletSize = 1;

        private int frameWidth;
        private int frameHeight;
        private Rectangle sourceRec;
        private Rectangle destRec;
        private Vector2 origin = new(70, 180);
        private float temp = 0.0f;

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


        public void Draw()
        {
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
        public bool Fire(List<Bullet> bullets, Enemy Target)
        {
            bool IsTimerReady = getTimer() > vitesseDattaque;
            if (IsTimerReady)
            {
                bullets.Add(new Bullet(rotation, Position, TourChoisie, bulletSize, Target, Degats));
                ResetTimer();
            }

            return IsTimerReady;
        }
        public void UpdateTimer()
        {
            if (!Program.menuOuvert)
                temp += 0.1f;
        }
        public float getTimer()
        {

            return temp;
        }
        public void ResetTimer()
        {
            temp = 0.0f;
        }



        private void setTextureCanon()
        {
            Raylib.UnloadTexture(Cannon);
            switch (TourChoisie)
            {
                case 1:
                    switch (Niveau)
                    {
                        case 1:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Cannon.png");
                            vitesseDattaque = 10f;
                            porteeTir = 250f;
                            degats = 10f;

                            break;
                        case 2:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Cannon2.png");
                            vitesseDattaque = 5f;
                            break;
                        case 3:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Cannon3.png");
                            vitesseDattaque = 9f;
                            bulletSize = 1.5f;
                            break;
                    }
                    break;
                case 2:
                    switch (Niveau)
                    {
                        case 1:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/MG.png");
                            vitesseDattaque = 1f;
                            degats = 3f;
                            break;
                        case 2:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/MG2.png");
                            vitesseDattaque = 0.5f;
                            degats = 3f;
                            break;
                        case 3:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/MG3.png");
                            vitesseDattaque = 0.2f;
                            degats = 3f;
                            break;
                    }
                    break;
                case 3:
                    switch (Niveau)
                    {
                        case 1:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Missile_Launcher.png");
                            vitesseDattaque = 20f;
                            break;
                        case 2:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Missile_Launcher2.png");
                            vitesseDattaque = 20f - 20f / 3;
                            break;
                        case 3:
                            Cannon = Raylib.LoadTexture(@"./images/Cannon/Missile_Launcher3.png");
                            vitesseDattaque = 20f / 3;
                            break;
                    }
                    break;
            }
            frameWidth = Cannon.Width;
            frameHeight = Cannon.Height;
            destRec = new Rectangle(Position.X + 0, Position.Y + 0, frameWidth / 3, frameHeight / 3);
            sourceRec = new Rectangle(0, 0, frameWidth, frameHeight);
            origin = new Vector2(frameWidth / 2 / 3, frameHeight / 4 * 3 / 3);
        }

        public int getPrice()
        {
            int Argent = 0;
            if (TourChoisie == 1)
            {
                if (niveau == 1)
                    Argent = 400;
                else if (niveau == 2)
                    Argent = 800;
            }
            else if (TourChoisie == 2)
            {
                if (niveau == 1)
                    Argent = 1000;
                else if (niveau == 2)
                    Argent = 2500;
            }
            else if (TourChoisie == 3)
            {
                if (niveau == 1)
                    Argent = 1000;
                else if (niveau == 2)
                    Argent = 1500;
            }
            return Argent;
        }
        public void Upgrade(ref int Argent)
        {
            if (TourChoisie == 1)
            {
                if (niveau == 1 && Argent >= 400)
                {
                    Argent -= 400;
                    niveau++;
                }
                else if (niveau == 2 && Argent >= 800)
                {
                    Argent -= 800;
                    niveau++;
                }
            }
            else if (TourChoisie == 2)
            {
                if (niveau == 1 && Argent >= 1000)
                {
                    Argent -= 1000;
                    niveau++;
                }
                else if (niveau == 2 && Argent >= 2500)
                {
                    Argent -= 2500;
                    niveau++;
                }
            }
            else if (TourChoisie == 3)
            {
                if (niveau == 1 && Argent >= 1000)
                {
                    Argent -= 1000;
                    niveau++;
                }
                else if (niveau == 2 && Argent >= 1500)
                {
                    Argent -= 1500;
                    niveau++;
                }
            }



            setTextureCanon();
        }
    }
}