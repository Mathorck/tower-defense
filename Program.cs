using Projet_Tower_Defense.tours;
using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Projet_Tower_Defense
{
    internal class Program
    {
        public static void Main()
        {
            ///////////// Création de la fenêtre /////////////
            Raylib.InitWindow(1920, 1080, "Hello World");
            Raylib.ToggleFullscreen();
            Raylib.SetTargetFPS(60);

            ///////////// Déclaration Variables /////////////
            //// Déclaration Variables hitbox ////
            // BtnStart
            Rectangle btnStart = new(750, 450, new Vector2(420, 110));
            // BtnLeave
            Rectangle btnStop = new(750, 650, new Vector2(420, 110));
        
            //// Hitbox non posables //////
            Rectangle[] cheminNonPosable = new Rectangle[13];
            cheminNonPosable[0] = new Rectangle(420,80,new Vector2(105,455));
            cheminNonPosable[1] = new Rectangle(240,450,new Vector2(235, 85));
            cheminNonPosable[2] = new Rectangle(240,450,new Vector2(100, 285));
            cheminNonPosable[3] = new Rectangle(240, 650, new Vector2(410, 70));
            cheminNonPosable[4] = new Rectangle(565, 260, new Vector2(345, 85));
            cheminNonPosable[5] = new Rectangle(825, 340, new Vector2(85, 135));
            cheminNonPosable[6] = new Rectangle(755, 390, new Vector2(105, 210));
            cheminNonPosable[7] = new Rectangle(835, 520, new Vector2(330, 75));
            cheminNonPosable[8] = new Rectangle(1070, 585, new Vector2(95, 270));
            cheminNonPosable[9] = new Rectangle(1010, 775, new Vector2(95, 215));
            cheminNonPosable[10] = new Rectangle(1015, 900, new Vector2(905, 85));
            cheminNonPosable[11] = new Rectangle(565, 260, new Vector2(100, 465));
            cheminNonPosable[12] = new Rectangle(945, 80, new Vector2(95, 465));
            ///// tableau Rectangles a afficher ////
            Rectangle[] btnAffichage = new Rectangle[2];
            // btnMenu
            Rectangle btnMenu = new(10, 10, new Vector2(60, 60));
            btnAffichage[0] = btnMenu;
            bool menuOuvert = false;
            // btnConstruction
            btnAffichage[1] = new Rectangle(300, 10, new Vector2(60, 60));
            int nbCanon = 0;
            bool modeConstruction = false;
            int tourChoisie = 1;
            //// liste Enemy /////
            List<Enemy> enemies = new List<Enemy>();
            //// liste Canon /////
            List<Canon> canons = new List<Canon>();
            // canonPos
            Canon canon = new Canon();
        
            //// Déclaration variables Autres ////
            Vector2 mousePoint = new Vector2(0f,0f);
            string texte = "Pressez ESC pour quitter!";
            bool start = false;
            bool stop = false;
            Vector2 PorteMonstre1 = new Vector2(480, 80);
            Vector2 PorteMonstre2 = new Vector2(990, 80);
            int vieDeLaBase = 100;
            Rectangle PorteMonstreD1 = new Rectangle(445, 80, 68, 40);
            Rectangle PorteMonstreD2 = new Rectangle(955, 80, 68, 40);

            //déclaration variables barre de vie///
            int totalVies = 10;
            int vieActuelle = 100;
            int largeurVie = 20;
            int HauteurVie = 40;
            int posBarreVieX = 585;
            int posBarreVieY = 40;

             
            ///////////// Boucle menu /////////////
            while (!start && !stop)
            {
                mousePoint = Raylib.GetMousePosition();

            

                //////// Boutton Vérif ////////////

                // BtnStart
                if (Raylib.CheckCollisionPointRec(mousePoint, btnStart) && Raylib.IsMouseButtonDown(MouseButton.Left))
                {
                    texte = "Ok btn start Bon";
                    start = true;
                }
                // BtnStop
                else if (Raylib.CheckCollisionPointRec(mousePoint, btnStop) && Raylib.IsMouseButtonDown(MouseButton.Left))
                {
                    texte = "Ok btn Stop Bon";
                    stop = true;
                }
                else
                {
                    texte = "Pas Ok";
                }



                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
            
                // Debug
                Raylib.DrawText(texte, 12, 12, 20, Color.Black);
                Raylib.DrawText($"X:{mousePoint.X} Y:{mousePoint.Y}", 12, 35, 20, Color.Black);
                //

                DessinerMenu();

                Raylib.EndDrawing();

            }
            if (start)
            {
                enemies.Add(new Enemy(PorteMonstre1,5f, Color.SkyBlue, 20));
                enemies.Add(new Enemy(PorteMonstre2,0.5f, Color.Brown, 50));
                
                

                Texture2D Fond = Raylib.LoadTexture("./images/backgroundgame.png");
                ///////////// Boucle principale /////////////
                while (!stop)
                {
                    mousePoint = Raylib.GetMousePosition();
                
                
                    if (Collide(cheminNonPosable[0]) || Collide(cheminNonPosable[1]) || Collide(cheminNonPosable[2]) || Collide(cheminNonPosable[3]) || Collide(cheminNonPosable[4]) || Collide(cheminNonPosable[5]) || Collide(cheminNonPosable[6]) || Collide(cheminNonPosable[7]) || Collide(cheminNonPosable[8]) || Collide(cheminNonPosable[9]) || Collide(cheminNonPosable[10]) || Collide(cheminNonPosable[11]) || Collide(cheminNonPosable[12]))
                        texte = "Non";
                    else
                        texte = "Ok";
                
                
                    if (Raylib.CheckCollisionPointRec(mousePoint, btnAffichage[0]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                        menuOuvert = true;

                    if (Raylib.CheckCollisionPointRec(mousePoint, btnAffichage[1]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        if (modeConstruction)
                            modeConstruction = false;
                        else
                            modeConstruction = true;
                    }

                    if (Raylib.IsKeyDown(KeyboardKey.Escape))
                        modeConstruction = false;


                    if (modeConstruction)
                    {
                        switch (tourChoisie)
                        {
                            case 1:
                                
                                break;
                        }
                    }



                    /////////////////////////Semble shlag de ouf//
                                                                //
                                                                //
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        try
                        {
                            Direction(enemies[i], enemies);
                            enemies[i].Go();
                        }catch { }

                    }                                           //
                                                                //
                    //////////////////////////////////////////////

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.White);
                    DessinerJeuFond(Fond);

                    DessinerEntitees(enemies,canons);

                    if (modeConstruction)
                    {
                        for (int i = 0; i < cheminNonPosable.Length; i++)
                            Raylib.DrawRectangleRec(cheminNonPosable[i], new Color(255,0,0,25));
                        canon.Place();
                        if (Raylib.IsMouseButtonPressed(MouseButton.Left) && !Raylib.CheckCollisionPointRec(mousePoint, btnAffichage[1]) && !TourCollide(cheminNonPosable, canons))
                        {
                            canons.Add(new Canon(Raylib.GetMousePosition()));
                            modeConstruction = false;
                        }
                    }

                    DessinerGui(texte, mousePoint, btnAffichage);
                    DessinerBase();
                    DessinerPortesMonstres();
                    DessinerBarreDeVie(ref vieActuelle);
                    

                    Raylib.EndDrawing();

                    //////////////////////////// MENU ///////////////////////
                    while (menuOuvert)
                    {
                        mousePoint = Raylib.GetMousePosition();
                        if ((Raylib.CheckCollisionPointRec(mousePoint, btnStart) && Raylib.IsMouseButtonDown(MouseButton.Left)) || (Raylib.CheckCollisionPointRec(mousePoint, btnMenu) && Raylib.IsMouseButtonPressed(MouseButton.Left)))
                            menuOuvert = false;

                        else if (Raylib.CheckCollisionPointRec(mousePoint, btnStop) && Raylib.IsMouseButtonDown(MouseButton.Left))
                        {
                            menuOuvert = false;
                            stop = true;
                        }


                        modeConstruction = false;

                        Raylib.BeginDrawing();
                        Raylib.ClearBackground(Color.White);
                        DessinerJeuFond(Fond);

                        DessinerEntitees(enemies, canons);


                        Raylib.DrawText("PAUSE", 822, 225, 80, Color.Black);

                        Raylib.DrawRectangleRec(btnStart, Color.Lime);
                        Raylib.DrawText("Continuer", 845, 480, 50, Color.Black);

                        Raylib.DrawRectangleRec(btnStop, Color.Red);
                        Raylib.DrawText("Quit", 910, 685, 50, Color.Black);

                        DessinerGui(texte, mousePoint, btnAffichage);
                        Raylib.EndDrawing();

                    }
                    /////////////////////////// MENU ///////////////////////////
                }
            }

            Raylib.CloseWindow();
        }

        static bool TourCollide(Rectangle[] chm,List<Canon> canon)
        {
            Vector2 mousePoint = Raylib.GetMousePosition();
            
            bool touche = false;
            foreach (Rectangle bout in chm)
            {
                if (Raylib.CheckCollisionCircleRec(mousePoint, 40f, bout))
                    touche = true;
            }
            foreach (Canon canon1 in canon)
            {
                if (Raylib.CheckCollisionCircles(mousePoint,40f,canon1.Position,40f))
                    touche = true;
            }


            return touche;
        }

        static void DessinerBarreDeVie(ref int vieActuelle)
        {
            Raylib.DrawRectangle(585, 30, vieActuelle*2, 30, Color.Green);
            Raylib.DrawRectangleLines(585, 30, vieActuelle * 2, 30, Color.Black);

            Texture2D coeur = Raylib.LoadTexture("./images/coeur.png");
            Rectangle Coeur = new Rectangle(555, 20, 50, 50);
           
            Raylib.DrawTexturePro(coeur, new Rectangle(0, 0, coeur.Width, coeur.Height), Coeur, new Vector2(0, 0), 0.0f, Color.White);

            
            
        }
        
        static void DessinerMenuTours()
        {

       
        }

        static void DessinerMenu()
        {
            Raylib.DrawText("Tower Defense", 685, 150, 70, Color.Black);
            if (Raylib.GetScreenHeight() < 1080 || Raylib.GetRenderWidth() < 1920)
                Raylib.DrawText("Votre Experience de jeu ne sera pas optimal votre écran est trop petit minimun:1080p", 12, 12, 20, Color.Red);
            //960
            Raylib.DrawRectangle(750,450,420,110, Color.Gold);
            Raylib.DrawText("Start", 885, 485, 50, Color.Black);

            Raylib.DrawRectangle(750, 650, 420, 110, Color.Gold);
            Raylib.DrawText("Quit", 910, 685, 50, Color.Black);

        }

        static void DessinerPortesMonstres()
        {
            Rectangle PorteMonstreD1 = new Rectangle(445, 80, 68, 40);
            Rectangle PorteMonstreD2 = new Rectangle(955, 80, 68, 40);

            Texture2D porte = Raylib.LoadTexture("./images/PorteMonstre.png");
            
            Raylib.DrawRectangleRec(PorteMonstreD1, Color.White);
            Raylib.DrawRectangleRec(PorteMonstreD2, Color.White);

            Raylib.DrawTexturePro(porte, new Rectangle(0, 0, porte.Width, porte.Height), new Rectangle(445, 80, 68, 40), new Vector2(0, 0), 0.0f, Color.White);
            Raylib.DrawTexturePro(porte, new Rectangle(0, 0, porte.Width, porte.Height), PorteMonstreD2, new Vector2(0, 0), 0.0f, Color.White);
        }

        static void DessinerBase()
        {
            Raylib.DrawRectangle(1860, 910, 75, 70, Color.Green);
        }

        static void DessinerEntitees(List<Enemy> enemies, List<Canon> canons) 
        {
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].Draw();
            for (int i = 0; i < canons.Count; i++)
                canons[i].Draw();
        }
  
        static void DessinerGui(string texte, Vector2 mousePoint, Rectangle[] btnAfficher)
        {
            Raylib.DrawRectangleGradientV(0, 0, 1920, 80, Color.Blue, Color.DarkBlue);
            // Debug
            Raylib.DrawText(texte, 100, 12, 20, Color.Black);
            Raylib.DrawText($"X:{mousePoint.X} Y:{mousePoint.Y}", 100, 35, 20, Color.Black);
            //
            //Raylib.DrawRectangleRec(btnAfficher[0], Color.SkyBlue);
            Raylib.DrawRectangleRounded(btnAfficher[0], 0.2f, 4 ,Color.SkyBlue);
            Raylib.DrawLineEx(new Vector2(20, 25),new Vector2(60, 25),6f,Color.Black);
            Raylib.DrawLineEx(new Vector2(20, 40), new Vector2(60, 40), 6f, Color.Black);
            Raylib.DrawLineEx(new Vector2(20, 55), new Vector2(60, 55), 6f, Color.Black);
            

            Raylib.DrawRectangleRounded(btnAfficher[1], 0.2f, 4 , Color.SkyBlue); //Dessin contour bouton menu tours
            Raylib.DrawTextureEx(Raylib.LoadTexture("./images/Target-icon.png"), btnAfficher[1].Position+new Vector2(4.5f,5),0,0.1f,Color.White); //affichage de l'icon dans le menu des tours

        }

        static void DessinerJeuFond(Texture2D Fond)
        {
            Raylib.DrawTextureEx(Fond, new Vector2(0, 80), 0f, 1f, Color.White);
        }

        static bool Collide(Rectangle rect)
        {
            Vector2 mousePoint = Raylib.GetMousePosition();
            return Raylib.CheckCollisionPointRec(mousePoint, rect);
        }

        static void Direction(Enemy monstre, List<Enemy> monstresList)
        {
            if (monstre.position == new Vector2(480, 490))
                monstre.dir = 2;

            else if (monstre.position == new Vector2(290, 490))
                monstre.dir = 1;

            else if (monstre.position == new Vector2(290, 680))
                monstre.dir = 4;

            else if (monstre.position == new Vector2(610, 680))
                monstre.dir = 3;

            else if (monstre.position == new Vector2(610, 310))
                monstre.dir = 4;

            else if (monstre.position == new Vector2(860, 310))
                monstre.dir = 1;

            else if (monstre.position == new Vector2(860, 430))
                monstre.dir = 2;

            else if (monstre.position == new Vector2(800, 430))
                monstre.dir = 1;

            else if (monstre.position == new Vector2(800, 560))
                monstre.dir = 4;

            else if (monstre.position == new Vector2(990, 560))
                monstre.dir = 4;

            else if (monstre.position == new Vector2(1120, 560))
                monstre.dir = 1;

            else if (monstre.position == new Vector2(1120, 810))
                monstre.dir = 2;

            else if (monstre.position == new Vector2(1060, 810))
                monstre.dir =1;

            else if (monstre.position == new Vector2(1060, 940))
                monstre.dir = 4;

            if (monstre.position == new Vector2(1910, 940))
            {
                monstresList.Remove(monstre);
                monstresList.Order();
            }
        }

    }
}