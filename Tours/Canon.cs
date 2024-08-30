using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Projet_Tower_Defense.tours
{
    internal class Canon
    {
        private float porteeTir = 50f;
        private int niveau = 0;
        private float degats = 10f;
        private float vitesseDattaque = 1.5f;
        public Vector2 Position;
        public int tourChoisie = 0;
        

        public int Niveau { get { return niveau; } }
        public float PorteeTir { get {  return porteeTir; } }
        public float Degats {  get { return degats; } }
        public float VitesseDattaque { get { return vitesseDattaque; } }


        public Canon()
        {

        }
        public Canon(Vector2 position)
        {
            this.Position = position;
        }
        public Canon(Vector2 position, int tourChoisie)
        {
            this.Position = position;
            this.tourChoisie = tourChoisie;
        }

        public void NiveauSup()
        {
            switch (niveau)
            {
                case 0:
                    niveau = 1; 
                    // ajouter buff ici
                    break;
                case 1: 
                    niveau = 2; 

                    break;
                case 2: 
                    niveau = 3;

                    break;
                case 3:
                    niveau = 4;

                    break;
                case 4: 
                    niveau = 5;

                    break;
            }
        }
      
        public void Draw()
        {
            Raylib.DrawCircleV(Position, 40f, Color.White);
        }
        public void Place()
        {
            Raylib.DrawCircleV(Raylib.GetMousePosition(), 40f, Color.White);
        }
        

    }
}
