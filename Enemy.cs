using Raylib_cs;
using System.Numerics;

public class Enemy
{
    public Vector2 position;
    public  float size = 35;
    public float speed = 0.1f;
    public int dir = 1;
    public Color couleur = Color.Red;
    public  float vie = 100;
    public bool Placebo = false;
    public int recompense = 100;
    private float temp = 0.0f;
    public int EnemyType = 1; 
    private int runState = 0;
    private int deadState = 0;
    public bool Mourrant = false;
    public  float vieMax = 100;
    public  float pourcentageVie;

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
    public Enemy(Vector2 position, float vitesse, int recompense, int type)
    {
        this.position = position;
        this.speed = vitesse;
        this.recompense = recompense;
        this.EnemyType = type;

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
    }

    public void DessinerLifeBar()
    {
        if ( vie!= vieMax)
        {
            Raylib.DrawRectangle(Convert.ToInt32(position.X + size / 2 - 75 / 2), Convert.ToInt32(position.Y - 50), 75, 5, Color.Black);
            double val = (75.0/100.0) * pourcentageVie;
            Raylib.DrawRectangle(Convert.ToInt32(position.X + size / 2 - 75 / 2), Convert.ToInt32(position.Y - 50), Convert.ToInt32(val), 5, Color.Red);
        }
    }

    public void UpdateLife()
    {
        pourcentageVie = (float)(vie/(vieMax/100));
    }

    public bool PlayDieAnime(Texture2D[] texture)
    {
        bool isDead = false;
        if (Mourrant)
        {
            speed = 0;
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
            DrawTexture(texture,runState);
        }
    }
    public void DrawTexture(Texture2D[] texture, int state)
    {
        switch (EnemyType)
        {
            case 1:
                Raylib.DrawTextureEx(texture[state], position - new Vector2(50, 70), 0f, 0.5f, Color.White);
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
                Raylib.DrawTextureEx(texture[state], position - new Vector2(50, 75), 0f, 0.4f, Color.White);
                break;
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
