using Raylib_cs;
using System.Numerics;

public class Enemy
{
    public Vector2 position;
    public float size = 35;
    public float speed = 0.1f;
    public int dir = 1;
    public Color couleur = Color.Red;
    public float vie = 20;
    public bool Placebo = false;
    public int recompense = 100;
    private float temp = 0.0f;
    public int EnemyType = 1;
    private int runState = 0;
    private int deadState = 0;
    public bool Mourrant = false;

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
    }
    public Enemy(Vector2 position, float vitesse)
    {
        this.position = position;
        this.speed = vitesse;
    }
    public Enemy(Vector2 position, float vitesse, Color couleur)
    {
        this.position = position;
        this.speed = vitesse;
        this.couleur = couleur;
    }
    public Enemy(Vector2 position, float vitesse, int vie)
    {
        this.position = position;
        this.speed = vitesse;
        this.vie = vie;
    }
    public Enemy(Vector2 position, float vitesse, Color couleur, int vie, int recompense)
    {
        this.position = position;
        this.speed = vitesse;
        this.couleur = couleur;
        this.vie = vie;
        this.recompense = recompense;
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

    public void PlayDieAnime(Texture2D[] texture)
    {
        
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
                //Retirer de la liste des mourrant
            }
            else
                Raylib.DrawTextureEx(texture[deadState], position - new Vector2(50, 70), 0f, 0.5f, Color.White);
        }

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
            Raylib.DrawTextureEx(texture[runState], position - new Vector2(50, 70), 0f, 0.5f, Color.White);
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
