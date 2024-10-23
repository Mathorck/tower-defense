using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Squelette
{
    public class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Color Color;
        public float Alpha;
        private Random R = new Random();

        public Particle(float x, float y, float speed)
        {
            Position = new Vector2(x, y);
            Velocity = new Vector2(0, -speed);
            Color = new Color(R.Next(20, 100), 0, 0, 255);
            Alpha = (float)R.Next(10, 100)/100f;
        }

        public void Update()
        {
            Position += Velocity;
            Alpha -= 0.005f;
            if (Alpha < 0) Alpha = 0;
        }

        public void Draw()
        {
            Color.A = (byte)(Alpha * 255);
            Raylib.DrawCircleV(Position, 5, Color);
        }
    }
}
