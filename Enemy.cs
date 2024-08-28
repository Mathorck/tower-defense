using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

public class Enemy
{
    public Vector2 position;
    private float size = 30;
    public float speed = 0.1f;
    public int dir = 1;
    public Color couleur = Color.Red;
    public int vie = 3;
    

    //   3
    // 2 0 4
    //   1

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
    public Enemy(Vector2 position, float vitesse, Color couleur, int vie)
    {
        this.position = position;
        this.speed = vitesse;
        this.couleur = couleur;
        this.vie = vie;
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

    public void Draw()
    {
        Raylib.DrawCircle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), size, couleur);
        Raylib.DrawText(vie.ToString(), Convert.ToInt32(position.X-11), Convert.ToInt32(position.Y-size/3), 20, Color.Black);
    }
}
