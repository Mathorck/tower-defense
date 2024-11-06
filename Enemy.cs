using Raylib_cs;
using System;
using System.Numerics;

namespace Squelette
{


    public class Enemy
    {
        public Vector2 position;
        public float size = 35;
        public float speed = 0.1f;
        public int dir = 1;
        public Color couleur = Color.Red;
        public float vie = 100;
        public bool Placebo = false;
        public int Recompense = 100;
        private float temp = 0.0f;
        public int EnemyType = 1;
        private int runState = 0;
        private int deadState = 0;
        public bool Mourrant = false;
        public float vieMax = 100;
        public float pourcentageVie;
        private int dejaDonne = 0;


        //   3      
        // 2 0 4    
        //   1      
        public Enemy()
        {
            Placebo = true;
        }
        public Enemy(Vector2 position)
        {
            this.position = position;
            Placebo = true;
        }
        public Enemy(Vector2 position, int type)
        {
            this.position = position;
            this.EnemyType = type;

            switch (EnemyType)
            {
                case 1:
                    Recompense = 25;
                    speed = 5f;
                    vieMax = 20;
                    break;
                case 2:
                    Recompense = 45;
                    speed = 2f;
                    vieMax = 75;
                    break;
                case 3:
                    Recompense = 25;
                    speed = 5f;
                    vieMax = 55;
                    break;
                case 4:
                    Recompense = 45;
                    speed = 2f;
                    vieMax = 150;
                    break;
                case 5:
                    Recompense = 50;
                    speed = 1f;
                    vieMax = 200;
                    break;
                case 6:
                    Recompense = 65;
                    speed = 5f;
                    vieMax = 225;
                    break;
                case 7:
                    Recompense = 80;
                    speed = 2f;
                    vieMax = 350;
                    break;
                case 8:
                    Recompense = 85;
                    speed = 2f;
                    vieMax = 500;
                    break;
                case 9:
                    Recompense = 200;
                    speed = 10f;
                    vieMax = 250;
                    break;
                case 10:
                    Recompense = 1000;
                    speed = 0.5f;
                    vieMax = 1000 * Vagues.HardnessOfTheWave;
                    break;
            }

            // calcule vie max
            vie = vieMax;
        }

        public void Go()
        {
            if (dir == 1)
            {
                position.Y += speed;
            }
            else if (dir == 2)
            {
                position.X -= speed;
            }
            else if (dir == 3)
            {
                position.Y -= speed;
            }
            else if (dir == 4)
            {
                position.X += speed;
            }

            if (position.X > Raylib.GetScreenWidth()+20 || position.Y > Raylib.GetScreenHeight()+20)
            {
                vie = -1;
            }
        }

        public void DessinerLifeBar()
        {
            if (vie != vieMax)
            {
                Raylib.DrawRectangle(Convert.ToInt32(position.X + size / 2 - 75 / 2), Convert.ToInt32(position.Y - 50), 75, 5, Color.Black);
                double val = (75.0 / 100.0) * pourcentageVie;
                Raylib.DrawRectangle(Convert.ToInt32(position.X + size / 2 - 75 / 2), Convert.ToInt32(position.Y - 50), Convert.ToInt32(val), 5, Color.Red);
            }
        }

        public void UpdateLife()
        {
            pourcentageVie = (float)(vie / (vieMax / 100));
        }

        public bool PlayDieAnime(Texture2D[] texture)
        {
            bool isDead = false;
            if (Mourrant)
            {
                speed = 0;
                if (dejaDonne < Recompense)
                {
                    if (Recompense - dejaDonne > 100)
                    {
                        Program.Money += 100;
                        dejaDonne += 100;
                    }
                    else if (Recompense > 100 && Recompense - dejaDonne > 10)
                    {
                        Program.Money += 10;
                        dejaDonne += 10;
                    }
                    else
                    {
                        Program.Money += 1;
                        dejaDonne += 1;
                    }

                }
                if (getTimer() > 1f)
                {
                    ResetTimer();
                    if (deadState > texture.Length - 2)
                        deadState = -1;
                    else
                        deadState++;
                }
                if (deadState == -1)
                {
                    isDead = true;
                }
                else
                    DrawTexture(texture, deadState);
            }
            return isDead;
        }

        public void PlayRunAnime(Texture2D[] texture)
        {
            if (!Mourrant)
            {
                if (getTimer() > 1 / speed)
                {
                    ResetTimer();
                    if (runState > texture.Length - 2)
                        runState = 0;
                    else
                        runState++;
                }
                DrawTexture(texture, runState);
            }
        }
        public void DrawTexture(Texture2D[] texture, int state)
        {
            switch (EnemyType)
            {
                case 1:
                    Raylib.DrawTextureEx(texture[state], position - new Vector2(50, 70), 0f, 0.45f, Color.White);
                    break;
                case 2:
                    Raylib.DrawTextureEx(texture[state], position - new Vector2(45, 75), 0f, 0.35f, Color.White);
                    break;
                case 3:
                    Raylib.DrawTextureEx(texture[state], position - new Vector2(50, 75), 0f, 0.4f, Color.White);
                    break;
                case 4:
                    Raylib.DrawTextureEx(texture[state], position - new Vector2(50, 75), 0f, 0.4f, Color.White);
                    break;
                case 5:
                    Raylib.DrawTextureEx(texture[state], position - new Vector2(37, 75), 0f, 0.4f, Color.White);
                    break;
                case 6:
                    Raylib.DrawTextureEx(texture[state], position - new Vector2(37, 75), 0f, 0.4f, Color.White);
                    break;
                case 7:
                    Raylib.DrawTextureEx(texture[state], position - new Vector2(37, 75), 0f, 0.4f, Color.White);
                    break;
                case 8:
                    Raylib.DrawTextureEx(texture[state], position - new Vector2(50, 75), 0f, 0.4f, Color.White);
                    break;
                case 9:
                    Raylib.DrawTextureEx(texture[state], position - new Vector2(37, 75), 0f, 0.4f, Color.White);
                    break;
                case 10:
                    Raylib.DrawTextureEx(texture[state], position - new Vector2(60, 165), 0f, 0.5f, Color.White);
                    break;
                default:
                    throw new NotImplementedException("Le type de monstre entré n'existe pas.");
            }
        }

        public void UpdateTimer()
        {
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
    }
}