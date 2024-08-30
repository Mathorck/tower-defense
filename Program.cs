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
            ///autre hitbox
            Rectangle[] objetNP = new Rectangle[13];
            objetNP[0] = new Rectangle(260,210,new Vector2(50,50));
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
            //// btnChoixTour /////
            Rectangle[] btnChoixTour = new Rectangle[3];
            btnChoixTour[0] = new Rectangle(0, 0, new Vector2(75, 75));
            btnChoixTour[1] = new Rectangle(0, 0, new Vector2(75, 75));
            btnChoixTour[2] = new Rectangle(0, 0, new Vector2(75, 75));
            bool ChoixTourOuvert = false;

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
            Vector2 tempMousePosition = new Vector2(0,0);
        
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
                
                ////////////// Déclarations des textures ////////////////////////////////////////////////////////////
                ///pour libèrer des la place dans la ram lorsqu'on est dans le menu//
                Texture2D Fond = Raylib.LoadTexture("./images/backgroundgame.png");
                Texture2D porte = Raylib.LoadTexture("./images/PorteMonstre.png");
                Texture2D cible = Raylib.LoadTexture("./images/Target-icon.png");

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
                        ChoixTourOuvert = false;
                    }

                    if (Raylib.IsKeyDown(KeyboardKey.Escape))
                        modeConstruction = false;

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


                    if (modeConstruction)
                    {
                        if ((Raylib.IsMouseButtonPressed(MouseButton.Left) && !Raylib.CheckCollisionPointRec(mousePoint, btnAffichage[1]) && !TourCollide(cheminNonPosable, canons)))
                        {
                            ChoixTourOuvert = true;
                            tempMousePosition = mousePoint;

                            //canons.Add(new Canon(Raylib.GetMousePosition()));
                            //modeConstruction = false;
                        }
                        if (!Raylib.CheckCollisionPointRec(mousePoint, btnChoixTour[0])&& Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            canons.Add(new Canon(tempMousePosition, 1));
                            //ChoixTourOuvert = false;
                            //modeConstruction = false;
                        }
                        else if (!Raylib.CheckCollisionPointRec(mousePoint, btnChoixTour[1])&& Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            canons.Add(new Canon(tempMousePosition, 2));
                            //ChoixTourOuvert = false;
                            //modeConstruction = false;
                        }
                        else if (!Raylib.CheckCollisionPointRec(mousePoint, btnChoixTour[2]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            canons.Add(new Canon(tempMousePosition, 3));
                            //ChoixTourOuvert = false;
                            //modeConstruction = false;
                        }
                    }
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.White);
                    DessinerJeuFond(Fond);
                    DessinerEntitees(enemies,canons);
                    DessinerGui(texte, mousePoint, btnAffichage, cible);
                    DessinerBase();
                    DessinerPortesMonstres(porte);
                    if (modeConstruction)
                    {
                        Raylib.DrawText("Mode Construction activé", 370, 30, 20, Color.Red);

                        if (ChoixTourOuvert)
                        {
                            canon.Place(tempMousePosition);
                            btnChoixTour[0].Position = tempMousePosition + new Vector2(90-35, 50);
                            btnChoixTour[1].Position = tempMousePosition + new Vector2(0-35, 50);
                            btnChoixTour[2].Position = tempMousePosition + new Vector2(-90-35, 50);
                            Raylib.DrawRectangleRounded(btnChoixTour[0], 0.2f, 4, Color.SkyBlue);
                            Raylib.DrawRectangleRounded(btnChoixTour[1], 0.2f, 4, Color.SkyBlue);
                            Raylib.DrawRectangleRounded(btnChoixTour[2], 0.2f, 4, Color.SkyBlue);
                        }
                        else
                        {
                            canon.Place(mousePoint);
                        }
                    }
                    
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

                        DessinerGui(texte, mousePoint, btnAffichage, cible);
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

        static void DessinerPortesMonstres(Texture2D porte)
        {
            Rectangle PorteMonstreD1 = new Rectangle(445, 80, 68, 40);
            Rectangle PorteMonstreD2 = new Rectangle(955, 80, 68, 40);

            

            Raylib.DrawRectangleRec(PorteMonstreD1, Color.White);
            Raylib.DrawRectangleRec(PorteMonstreD2, Color.White);

            Raylib.DrawTexturePro(porte, new Rectangle(0, 0, porte.Width, porte.Height), PorteMonstreD1, new Vector2(0, 0), 0.0f, Color.White);
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
  
        static void DessinerGui(string texte, Vector2 mousePoint, Rectangle[] btnAfficher, Texture2D Cible)
        {
            Raylib.DrawRectangleGradientV(0, 0, 1920, 80, Color.Blue, Color.DarkBlue);
            // Debug
            Raylib.DrawText(texte, 100, 12, 20, Color.Black);
            Raylib.DrawText($"X:{mousePoint.X} Y:{mousePoint.Y}", 100, 35, 20, Color.Black);
            //
            Raylib.DrawRectangleRounded(btnAfficher[0], 0.2f, 4 ,Color.SkyBlue);
            Raylib.DrawLineEx(new Vector2(20, 25),new Vector2(60, 25),6f,Color.Black);
            Raylib.DrawLineEx(new Vector2(20, 40), new Vector2(60, 40), 6f, Color.Black);
            Raylib.DrawLineEx(new Vector2(20, 55), new Vector2(60, 55), 6f, Color.Black);
            

            Raylib.DrawRectangleRounded(btnAfficher[1], 0.2f, 4 , Color.SkyBlue); //Dessin contour bouton menu tours
            Raylib.DrawTextureEx(Cible, btnAfficher[1].Position+new Vector2(4.5f,5),0,0.1f,Color.White); //affichage de l'icon dans le menu des tours

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
                monstre.dir = 1;

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