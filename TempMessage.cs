using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Squelette
{
    public class TempMessage
    {
        private float tempRestant;
        private string texte;
        private Color couleur;
        private bool bouge;

        private float angle;
        private bool sensHoraire;

        private string Texte
        {
            get { return texte; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    texte = "ERREUR";
                else
                    texte = value;
            }
        }
        private float TempRestant
        {
            get { return tempRestant; }
            set
            {
                if (value <= 0)
                    tempRestant = 3;
                else
                    tempRestant = value;
            }
        }

        public TempMessage(string texte, float tempRestant, Color couleur, bool bouge) 
        {
            Texte = texte;
            TempRestant = tempRestant;
            this.bouge = bouge;
            this.couleur = couleur;
            angle = 0;
            sensHoraire = false;
        }

        public bool Affichage()
        {
            float ScreenX = Raylib.GetRenderWidth()/2;
            float ScreenY = Raylib.GetRenderHeight()/2;
            if (bouge)
            {
                if (angle >= 12)
                    sensHoraire = true;
                else if (angle <= -12)
                    sensHoraire = false;


                if (sensHoraire)
                    angle -= 0.35f;
                else
                    angle += 0.35f;
            }
            tempRestant -= 0.01f;

            Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), Texte, 100, 10f);

            // Utilisez la moitié de la largeur et de la hauteur comme origine
            Vector2 textOrigin = new Vector2(textSize.X / 2, textSize.Y / 2);

            Raylib.DrawTextPro(
                Raylib.GetFontDefault(),
                Texte,
                new Vector2(ScreenX, ScreenY), // Position du texte
                textOrigin,  // Origine au centre du texte pour une rotation centrale
                angle,         // Exemple de rotation de 45 degrés
                100,         // Taille du texte
                10f,         // Espacement des caractères
                couleur  // Couleur
            );

            if (tempRestant <= 0)
                return !false;
            else return !true;
        }
    }
}
