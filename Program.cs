using Raylib_cs;
using System.Numerics;

namespace Squelette
{
    internal class Program
    {
        public static void Main()
        {
            ///////////// Création de la fenêtre /////////////
            Raylib.InitWindow(1920, 1080, "Tower Defense");
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
            cheminNonPosable[0] = new Rectangle(420, 80, new Vector2(105, 455));
            cheminNonPosable[1] = new Rectangle(240, 450, new Vector2(235, 85));
            cheminNonPosable[2] = new Rectangle(240, 450, new Vector2(100, 285));
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
            //// autre hitbox
            Rectangle[] objetNp = new Rectangle[13];
            objetNp[0] = new Rectangle(260, 210, new Vector2(50, 50));
            ///// tableau Rectangles a afficher ////
            Rectangle[] btnAffichage = new Rectangle[2];
            // btnMenu
            Rectangle btnMenu = new(10, 10, new Vector2(60, 60));
            btnAffichage[0] = btnMenu;
            bool menuOuvert = false;
            // btnConstruction
            btnAffichage[1] = new Rectangle(300, 10, new Vector2(60, 60));
            //int nbCanon = 0;
            bool modeConstruction = false;
            //// liste Enemy /////
            List<Enemy> enemies = new List<Enemy>();
            //// liste Canon /////
            List<Canon> canons = new List<Canon>();
            //// liste bullets ////
            List<Bullet> bullets = new List<Bullet>();
            // canonPos
            Canon canon = new Canon();
            //// liste Explosion ////
            List<Explosion> explosions = new List<Explosion>();
            //// btnChoixTour /////
            Rectangle[] btnChoixTour = new Rectangle[3];
            btnChoixTour[0] = new Rectangle(0, 0, new Vector2(75, 75));
            btnChoixTour[1] = new Rectangle(0, 0, new Vector2(75, 75));
            btnChoixTour[2] = new Rectangle(0, 0, new Vector2(75, 75));
            bool choixTourOuvert = false;

            //// Déclaration variables Autres ////
            Vector2 mousePoint = new Vector2(0f, 0f);
            string texte = "Pressez ESC pour quitter!";
            bool start = false;
            bool stop = false;
            Vector2 porteMonstre1 = new Vector2(480, 80);
            Vector2 porteMonstre2 = new Vector2(990, 80);
            Vector2 tempMousePosition = new Vector2(0, 0);
            int Argent = 20000;
            int vague = 0;
            Random rand = new Random();

            float vieActuelle = 100;
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
                enemies.Add(new Enemy(porteMonstre1, 5f, Color.SkyBlue, 20, 200));
                enemies.Add(new Enemy(porteMonstre2, 1f, Color.Brown, 50, 200));

                ////////////// Déclarations des textures ////////////////////////////////////////////////////////////
                ////pour libèrer des la place dans la ram lorsqu'on est dans le menu////
                Texture2D fond = Raylib.LoadTexture("./images/backgroundgame.png");
                Texture2D porte = Raylib.LoadTexture("./images/basemonstre1.png");
                Texture2D baseV = Raylib.LoadTexture("./images/base1.png");
                Texture2D cible = Raylib.LoadTexture("./images/Target-icon.png");
                Texture2D coeur = Raylib.LoadTexture("./images/coeur.png");
                Texture2D argent = Raylib.LoadTexture("./images/Argent.png");

                Texture2D cannon = Raylib.LoadTexture(@"./images/Cannon/Cannon.png");
                Texture2D mg = Raylib.LoadTexture(@"./images/Cannon/MG.png");
                Texture2D missileLauncher = Raylib.LoadTexture(@"./images/Cannon/Missile_Launcher.png");

                //Texture2D explosionTexture = Raylib.LoadTexture("./images/Cannon/explosions.png");

                Texture2D[] monstre1run = new Texture2D[] 
                {
                    Raylib.LoadTexture("./images/Monstres/1/run/1_enemies_1_run_000.png"),
                    Raylib.LoadTexture("./images/Monstres/1/run/1_enemies_1_run_002.png"),
                    Raylib.LoadTexture("./images/Monstres/1/run/1_enemies_1_run_004.png"),
                    Raylib.LoadTexture("./images/Monstres/1/run/1_enemies_1_run_006.png"),
                    Raylib.LoadTexture("./images/Monstres/1/run/1_enemies_1_run_008.png"),
                    Raylib.LoadTexture("./images/Monstres/1/run/1_enemies_1_run_010.png"),
                    Raylib.LoadTexture("./images/Monstres/1/run/1_enemies_1_run_012.png"),
                    Raylib.LoadTexture("./images/Monstres/1/run/1_enemies_1_run_014.png"),
                    Raylib.LoadTexture("./images/Monstres/1/run/1_enemies_1_run_016.png"),
                    Raylib.LoadTexture("./images/Monstres/1/run/1_enemies_1_run_018.png")
                };


                ///////////// Boucle principale /////////////
                while (!stop)
                {
                    mousePoint = Raylib.GetMousePosition();

                    texte = bullets.Count().ToString();


                    if (Raylib.CheckCollisionPointRec(mousePoint, btnAffichage[0]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                        menuOuvert = true;

                    if (Raylib.CheckCollisionPointRec(mousePoint, btnAffichage[1]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        if (modeConstruction)
                            modeConstruction = false;
                        else
                            modeConstruction = true;
                        choixTourOuvert = false;
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
                            Direction(enemies[i], enemies, ref vieActuelle);
                            enemies[i].Go();
                        }
                        catch { }
                    }                                           //
                                                                //
                                                                //////////////////////////////////////////////

                    List<Bullet> bulletsToRemove = new List<Bullet>();

                    foreach (Bullet balle in bullets)
                    {

                        if (HitEnnemy(enemies, balle, out Enemy touche))
                        {
                            bulletsToRemove.Add(balle);
                            if (!touche.Placebo)
                            {
                                touche.vie -= 10;
                            }
                        }
                        else if (balle.Position.X > Raylib.GetScreenWidth() || balle.Position.Y > Raylib.GetScreenHeight())
                            bulletsToRemove.Add(balle);
                        else if (enemies.Count == 0)
                            bulletsToRemove.Add(balle);
                        else if (balle.Position.X < 0 || balle.Position.Y < 0)
                            bulletsToRemove.Add(balle);
                    }
                    foreach (Bullet balle in bulletsToRemove)
                    {
                        bullets.Remove(balle.Destroy(explosions));
                    }
                    bullets.Order();





                    if (modeConstruction)
                    {
                        if ((Raylib.IsMouseButtonPressed(MouseButton.Left) && !Raylib.CheckCollisionPointRec(mousePoint, btnAffichage[1]) && !TourCollide(cheminNonPosable, canons)) && !choixTourOuvert)
                        {
                            choixTourOuvert = true;
                            tempMousePosition = mousePoint;
                        }
                        if (Raylib.CheckCollisionPointRec(mousePoint, btnChoixTour[0]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            if (Argent >= 100)
                            {
                                canons.Add(new Canon(tempMousePosition, 1));
                                choixTourOuvert = false;
                                modeConstruction = false;
                                btnChoixTour[0].Position = new Vector2(0, 0);
                                btnChoixTour[1].Position = new Vector2(0, 0);
                                btnChoixTour[2].Position = new Vector2(0, 0);
                                Argent -= 100;
                            }
                        }
                        else if (Raylib.CheckCollisionPointRec(mousePoint, btnChoixTour[1]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            if (Argent >= 200)
                            {
                                canons.Add(new Canon(tempMousePosition, 2));
                                choixTourOuvert = false;
                                modeConstruction = false;
                                btnChoixTour[0].Position = new Vector2(0, 0);
                                btnChoixTour[1].Position = new Vector2(0, 0);
                                btnChoixTour[2].Position = new Vector2(0, 0);
                                Argent -= 200;
                            }
                        }
                        else if (Raylib.CheckCollisionPointRec(mousePoint, btnChoixTour[2]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            if (Argent >= 500)
                            {
                                canons.Add(new Canon(tempMousePosition, 3));
                                choixTourOuvert = false;
                                modeConstruction = false;
                                btnChoixTour[0].Position = new Vector2(0, 0);
                                btnChoixTour[1].Position = new Vector2(0, 0);
                                btnChoixTour[2].Position = new Vector2(0, 0);
                                Argent -= 500;
                            }
                        }
                    }
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.White);
                    DessinerJeuFond(fond);
                    DessinerEntitees( canons);


                    List<Explosion> explosionsToRemove = new List<Explosion>();

                    // Itérer sur les explosions sans les supprimer directement
                    foreach (Explosion explosion in explosions)
                    {
                        if (explosion.UpdateTimer())
                        {
                            explosionsToRemove.Add(explosion);
                        }
                    }

                    // Supprimer les explosions après la boucle
                    foreach (Explosion explosion in explosionsToRemove)
                    {
                        explosions.Remove(explosion);
                    }
                    explosions.Order();


                    foreach (Canon canon2 in canons)
                    {
                        if (Raylib.CheckCollisionPointCircle(Raylib.GetMousePosition(), canon2.Position, canon2.hitbox))
                        {
                            Raylib.DrawCircleLinesV(canon2.Position, canon2.PorteeTir, Color.Black);
                            Raylib.DrawText(canon2.getPrice().ToString(), (int)canon2.Position.X, (int)canon2.Position.Y, 20, Color.Black);
                            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                            {
                                canon2.Upgrade(ref Argent);
                            }
                        }

                        
                        canon2.UpdateTimer();
                        if (enemies.Count != 0)
                        {
                            canon2.Fire(bullets, EnemyLeMieux(enemies,canon2));
                            try
                            {
                                canon2.setRotation(getRotation(EnemyLeMieux(enemies, canon2).position, canon2.Position));
                            }
                            catch
                            {

                            }
                        }
                    }
                    foreach (Enemy enemy in enemies)
                    {
                        enemy.UpdateTimer();
                        switch (enemy.EnemyType)
                        {
                            case 1:
                                enemy.PlayAnime(monstre1run);
                                break;
                        }
                    }
                    foreach (Bullet bullet in bullets)
                    {
                        if (enemies.Count > 0 || bullet.Target != null )
                        {
                            bullet.Rotation = getRotation(bullet.Target.position, bullet.Position, 90);
                        }
                        bullet.Draw();
                    }
                    DessinerGui(texte, mousePoint, btnAffichage, cible, Argent, argent, vieActuelle, coeur);
                    DessinerBase(baseV);
                    DessinerPortesMonstres(porte);

                    DessinerPortesMonstres(porte);
                    if (modeConstruction)
                    {
                        Raylib.DrawText("Mode Construction activé", 370, 30, 20, Color.Red);


                        if (choixTourOuvert)
                        {
                            canon.Place(tempMousePosition);
                            btnChoixTour[0].Position = tempMousePosition + new Vector2(90 - 35, 50);
                            btnChoixTour[1].Position = tempMousePosition + new Vector2(0 - 35, 50);
                            btnChoixTour[2].Position = tempMousePosition + new Vector2(-90 - 35, 50);
                            Raylib.DrawRectangleRounded(btnChoixTour[0], 0.2f, 4, Color.SkyBlue);
                            Raylib.DrawTextureEx(cannon, btnChoixTour[0].Position + new Vector2(52.5f, -7.5f), 45f, 0.3f, Color.White);
                            Raylib.DrawRectangleRounded(btnChoixTour[1], 0.2f, 4, Color.SkyBlue);
                            Raylib.DrawTextureEx(mg, btnChoixTour[1].Position + new Vector2(45, -2.5f), 45f, 0.3f, Color.White);
                            Raylib.DrawRectangleRounded(btnChoixTour[2], 0.2f, 4, Color.SkyBlue);
                            Raylib.DrawTextureEx(missileLauncher, btnChoixTour[2].Position + new Vector2(45, 0), 45f, 0.3f, Color.White);
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
                        DessinerJeuFond(fond);

                        DessinerEntitees( canons);

                        Raylib.DrawText("PAUSE", 822, 225, 80, Color.Black);

                        Raylib.DrawRectangleRec(btnStart, Color.Lime);
                        Raylib.DrawText("Continuer", 845, 480, 50, Color.Black);

                        Raylib.DrawRectangleRec(btnStop, Color.Red);
                        Raylib.DrawText("Quit", 910, 685, 50, Color.Black);

                        DessinerGui(texte, mousePoint, btnAffichage, cible, Argent, argent,vieActuelle,coeur);
                        Raylib.EndDrawing();

                    }
                    /////////////////////////// MENU ///////////////////////////
                }
            }
            Raylib.CloseWindow();
        }

        static bool TourCollide(Rectangle[] chm, List<Canon> canon)
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
                if (Raylib.CheckCollisionCircles(mousePoint, 40f, canon1.Position, 40f))
                    touche = true;
            }
            return touche;
        }
        static void DessinerMenu()
        {
            Raylib.DrawText("Tower Defense", 685, 150, 70, Color.Black);
            if (Raylib.GetScreenHeight() < 1080 || Raylib.GetRenderWidth() < 1920)
                Raylib.DrawText("Votre Experience de jeu ne sera pas optimal votre écran est trop petit minimun:1080p", 12, 12, 20, Color.Red);
            //960
            Raylib.DrawRectangle(750, 450, 420, 110, Color.Gold);
            Raylib.DrawText("Start", 885, 485, 50, Color.Black);

            Raylib.DrawRectangle(750, 650, 420, 110, Color.Gold);
            Raylib.DrawText("Quit", 910, 685, 50, Color.Black);

        }

        static void DessinerPortesMonstres(Texture2D porte)
        {
            /*
            Rectangle porteMonstreD1 = new Rectangle(445, 80, 68, 40);
            Rectangle porteMonstreD2 = new Rectangle(955, 80, 68, 40);

            Raylib.DrawRectangleRec(porteMonstreD1, Color.White);
            Raylib.DrawRectangleRec(porteMonstreD2, Color.White);*/

            Raylib.DrawTexturePro(porte, new Rectangle(0, 0, porte.Width, porte.Height), new Rectangle(445-31, 40, porte.Width/8, porte.Height/8), new Vector2(0, 0), 0.0f, Color.White);
            Raylib.DrawTexturePro(porte, new Rectangle(0, 0, porte.Width, porte.Height), new Rectangle(955-31, 40, porte.Width/8, porte.Height/8), new Vector2(0, 0), 0.0f, Color.White);

        }

        static void DessinerBase(Texture2D baseV)
        {
            //Raylib.DrawRectangle(1860, 910, 75, 70, Color.Green);
            Raylib.DrawTexturePro(baseV, new Rectangle(0, 0, baseV.Width, baseV.Height), new Rectangle(1815, 735, baseV.Width/4, baseV.Height/4), new Vector2(0,0), 0.0f, Color.White);
        }

        static void DessinerEntitees( List<Canon> canons)
        {
            for (int i = 0; i < canons.Count; i++)
                canons[i].Draw();
        }

        static void DessinerGui(string texte, Vector2 mousePoint, Rectangle[] btnAfficher, Texture2D cible, int Argent, Texture2D argent, float vieActuelle, Texture2D coeur)
        {
            Raylib.DrawRectangleGradientV(0, 0, 1920, 80, Color.Blue, Color.DarkBlue);
            // Debug
            Raylib.DrawText(texte, 100, 12, 20, Color.Black);
            Raylib.DrawText($"X:{mousePoint.X} Y:{mousePoint.Y}", 100, 35, 20, Color.Black);
            //
            Raylib.DrawRectangleRounded(btnAfficher[0], 0.2f, 4, Color.SkyBlue);
            Raylib.DrawLineEx(new Vector2(20, 25), new Vector2(60, 25), 6f, Color.Black);
            Raylib.DrawLineEx(new Vector2(20, 40), new Vector2(60, 40), 6f, Color.Black);
            Raylib.DrawLineEx(new Vector2(20, 55), new Vector2(60, 55), 6f, Color.Black);


            Raylib.DrawRectangleRounded(btnAfficher[1], 0.2f, 4, Color.SkyBlue); //Dessin contour bouton menu tours
            Raylib.DrawTextureEx(cible, btnAfficher[1].Position + new Vector2(4.5f, 5), 0, 0.1f, Color.White); //affichage de l'icon dans le menu des tours

            Raylib.DrawRectangleRounded(new Rectangle(1000+650, 10, new(250, 60)), 0.2f, 4, Color.SkyBlue);
            Raylib.DrawTextureEx(argent, new Vector2(1000 + 250 - 50+650, 20), 0f, 0.2f, Color.White);
            Raylib.DrawText(Argent.ToString(), 1010+650, 22, 40, Color.Black);

            if (vieActuelle > 100)
                vieActuelle = 100;

            

            Color color = Color.DarkGray;
            if (vieActuelle > 80)
                color = Color.DarkGreen;
            else if (vieActuelle > 60)
                color = Color.Green;
            else if (vieActuelle > 40)
                color = Color.Yellow;
            else if (vieActuelle > 20)
                color = Color.Orange;
            else if (vieActuelle > 0)
                color = Color.Red;

            Raylib.DrawRectangleRounded(new Rectangle(1380-50+25, 10, new(300-25, 60)), 0.2f, 4, Color.SkyBlue);
            Raylib.DrawRectangle(1365, 25, 200, 30, Color.Black);
            Raylib.DrawRectangle(1365, 25, Convert.ToInt32(vieActuelle) * 2, 30, color);
            Raylib.DrawRectangleLines(1365, 25, Convert.ToInt32(vieActuelle) * 2, 30, Color.Black);

            Raylib.DrawTextureEx(coeur, new(1580, 20), 0f, 0.9f, Color.White);

        }

        static void DessinerJeuFond(Texture2D fond)
        {
            Raylib.DrawTextureEx(fond, new Vector2(0, 80), 0f, 1f, Color.White);
        }

        static bool Collide(Rectangle rect)
        {
            Vector2 mousePoint = Raylib.GetMousePosition();
            return Raylib.CheckCollisionPointRec(mousePoint, rect);
        }

        static void Direction(Enemy monstre, List<Enemy> monstresList, ref float vie)
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
                vie -= monstre.vie;
                monstresList.Remove(monstre);
                monstresList.Order();
            }
        }
        static float getRotation(Vector2 Ennemy, Vector2 Tour)
        {
            float deltaY = Ennemy.Y - Tour.Y;
            float deltaX = Ennemy.X - Tour.X;
            return (float)(Math.Atan2(deltaY, deltaX) * (180.0 / Math.PI));
        }
        static float getRotation(Vector2 Ennemy, Vector2 Tour, float offset)
        {
            float deltaY = Ennemy.Y - Tour.Y;
            float deltaX = Ennemy.X - Tour.X;
            return (float)(Math.Atan2(deltaY, deltaX) * (180.0 / Math.PI)) + offset;
        }
        static bool HitEnnemy(List<Enemy> enemies, Bullet balle, out Enemy Touche)
        {
            bool hit = false;
            Touche = new Enemy(Vector2.Zero);
            foreach (Enemy enemy in enemies)
            {
                if (Raylib.CheckCollisionCircleRec(enemy.position, enemy.size, balle.rctDest))
                {
                    hit = true;
                    Touche = enemy;
                }
            }

            return hit;
        }
        static Enemy EnemyLeMieux(List<Enemy> enemies, Canon canon)
        {
            // Initialiser Mieu avec le premier ennemi de la liste qui est dans la portée du canon
            Enemy? Mieu = null;
            float maxDistance = 10000;

            foreach (Enemy enemy in enemies)
            {
                float distance = Vector2.Distance(canon.Position, enemy.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    Mieu = enemy;
                }
            }

            // Si aucun ennemi n'est trouvé dans la portée, retourner le premier ennemi
            if (Mieu == null)
            {
                Mieu = enemies[0];
            }

            return Mieu;
        }
        static Enemy EnemyLeMieux(List<Enemy> enemies, Bullet canon)
        {
            // Initialiser Mieu avec le premier ennemi de la liste qui est dans la portée du canon
            Enemy? Mieu = null;
            float maxDistance = 10000;

            foreach (Enemy enemy in enemies)
            {
                float distance = Vector2.Distance(canon.Position, enemy.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    Mieu = enemy;
                }
            }

            // Si aucun ennemi n'est trouvé dans la portée, retourner le premier ennemi
            if (Mieu == null)
            {
                if (enemies.Count > 0)
                {
                    Mieu = enemies[0];
                }
            }

            return Mieu;
        }


    }
}