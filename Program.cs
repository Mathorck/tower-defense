using Raylib_cs;
using System.Numerics;

namespace Squelette
{
    internal class Program
    {
        // Auteurs   : Elias Fahme, Mathéo Monnier
        // Nom       : Tower Defense

        const int PRIXCANON = 100;
        const int PRIXROCKETLAUNCHER = 500;
        const int PRIXMG = 750;

        const int NBMAXTOUR = 10;
        public static int NbTour = 0;

        public static bool DebugActivated = true;

        public static bool ModeConstruction = false;
        public static bool ChoixTourOuvert = false;
        public static bool MenuOuvert = false;
        public static bool ModeDestruction = false;

        public static Vector2 MousePoint = new Vector2(0, 0);
        public static Vector2 tempMousePosition = new Vector2(0, 0);
        public static Vector2 porteMonstre1 = new Vector2(480, 80);
        public static Vector2 porteMonstre2 = new Vector2(990, 80);

        public static Rectangle[] ObjetNonPosable = new Rectangle[24];
        public static Rectangle[] CheminNonPosable = new Rectangle[13];
        public static Rectangle[] BlockNonPosable = new Rectangle[4];
        public static Rectangle[] BtnAffichage = new Rectangle[3];
        public static Rectangle[] BtnChoixTour = new Rectangle[3];

        //// liste Enemy /////                                     
        public static List<Enemy> Enemies = new List<Enemy>();
        //// liste Canon /////                                     
        public static List<Canon> Canons = new List<Canon>();
        //// liste bullets ////                                    
        public static List<Bullet> Bullets = new List<Bullet>();
        //// liste Explosion ////                                  
        public static List<Explosion> Explosions = new List<Explosion>();

        public static int Money = 500;
        public static float VieActuelle = 100;

        //// Textures ////
        public static Texture2D Fond;
        public static Texture2D BaseV;
        public static Texture2D Cible;
        public static Texture2D Coeur;
        public static Texture2D Argent;
        public static Texture2D Poubelle;
        public static Texture2D Crane;
        public static Texture2D Vague;

        public static Texture2D Cannon;
        public static Texture2D Mg;
        public static Texture2D MissileLauncher;

        private static Canon canon = new Canon();

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
            CheminNonPosable[0] = new Rectangle(440, 80, new Vector2(80, 455));
            CheminNonPosable[1] = new Rectangle(250, 455, new Vector2(235, 80));
            CheminNonPosable[2] = new Rectangle(250, 455, new Vector2(75, 270));
            CheminNonPosable[3] = new Rectangle(250, 650, new Vector2(315, 75));
            CheminNonPosable[4] = new Rectangle(570, 260, new Vector2(345, 85));
            CheminNonPosable[5] = new Rectangle(825, 340, new Vector2(85, 135));
            CheminNonPosable[6] = new Rectangle(755, 390, new Vector2(105, 210));
            CheminNonPosable[7] = new Rectangle(835, 520, new Vector2(330, 75));
            CheminNonPosable[8] = new Rectangle(1070, 585, new Vector2(95, 270));
            CheminNonPosable[9] = new Rectangle(1010, 775, new Vector2(95, 215));
            CheminNonPosable[10] = new Rectangle(1015, 900, new Vector2(905, 85));
            CheminNonPosable[11] = new Rectangle(565, 260, new Vector2(100, 465));
            CheminNonPosable[12] = new Rectangle(945, 80, new Vector2(95, 465));

            ObjetNonPosable[0] = new Rectangle(135, 140, new Vector2(50, 60));
            ObjetNonPosable[1] = new Rectangle(255, 210, new Vector2(50, 60));
            ObjetNonPosable[2] = new Rectangle(115, 295, new Vector2(85, 95));
            ObjetNonPosable[3] = new Rectangle(65, 660, new Vector2(55, 60));
            ObjetNonPosable[4] = new Rectangle(135, 790, new Vector2(50, 60));
            ObjetNonPosable[5] = new Rectangle(320, 785, new Vector2(65, 60));
            ObjetNonPosable[6] = new Rectangle(60, 840, new Vector2(320, 195));
            ObjetNonPosable[7] = new Rectangle(515, 785, new Vector2(55, 60));
            ObjetNonPosable[8] = new Rectangle(515, 910, new Vector2(50, 60));
            ObjetNonPosable[9] = new Rectangle(380, 920, new Vector2(75, 115));
            ObjetNonPosable[10] = new Rectangle(705, 785, new Vector2(75, 60));
            ObjetNonPosable[11] = new Rectangle(765, 660, new Vector2(130, 120));
            ObjetNonPosable[12] = new Rectangle(835, 845, new Vector2(65, 60));
            ObjetNonPosable[13] = new Rectangle(775, 920, new Vector2(50, 45));
            ObjetNonPosable[14] = new Rectangle(645, 150, new Vector2(50, 60));
            ObjetNonPosable[15] = new Rectangle(775, 85, new Vector2(50, 60));
            ObjetNonPosable[16] = new Rectangle(1100, 85, new Vector2(50, 60));
            ObjetNonPosable[17] = new Rectangle(1225, 215, new Vector2(50, 60));
            ObjetNonPosable[18] = new Rectangle(1345, 145, new Vector2(55, 60));
            ObjetNonPosable[19] = new Rectangle(1345, 80, new Vector2(565, 505));
            ObjetNonPosable[20] = new Rectangle(1300, 805, new Vector2(25, 35));
            ObjetNonPosable[21] = new Rectangle(1345, 725, new Vector2(125, 55));
            ObjetNonPosable[22] = new Rectangle(1470, 780, new Vector2(65, 75));
            ObjetNonPosable[23] = new Rectangle(1680, 725, new Vector2(35, 40));

            BlockNonPosable[0] = new Rectangle(0, 0, 80, 1080);
            BlockNonPosable[1] = new Rectangle(0, 0, 1920, 100);
            BlockNonPosable[2] = new Rectangle(1840, 0, 80, 1920);
            BlockNonPosable[3] = new Rectangle(0, 1000, 1920, 80);

            ///// tableau Rectangles a afficher ////

            // btnMenu
            BtnAffichage[0] = new(10, 10, new Vector2(60, 60));
            // btnConstruction
            BtnAffichage[1] = new Rectangle(300, 10, new Vector2(60, 60));
            // btn destruction
            BtnAffichage[2] = new Rectangle(220, 10, new Vector2(60, 60));

            // canonPos
            Canon canon = new Canon();

            //// btnChoixTour /////
            BtnChoixTour[0] = new Rectangle(0, 0, new Vector2(75, 75));
            BtnChoixTour[1] = new Rectangle(0, 0, new Vector2(75, 75));
            BtnChoixTour[2] = new Rectangle(0, 0, new Vector2(75, 75));


            //// Déclaration variables Autres ////
            string texte = "Debug Actif";
            bool start = false;
            bool stop = false;


            Random rand = new Random();

            // déclaration des textures
            #region Textures2D Ennemy (Ne pas ouvrir danger de mort)
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
            Texture2D[] monstre1die = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/1/die/1_enemies_1_die_000.png"),
                    Raylib.LoadTexture("./images/Monstres/1/die/1_enemies_1_die_002.png"),
                    Raylib.LoadTexture("./images/Monstres/1/die/1_enemies_1_die_004.png"),
                    Raylib.LoadTexture("./images/Monstres/1/die/1_enemies_1_die_006.png"),
                    Raylib.LoadTexture("./images/Monstres/1/die/1_enemies_1_die_008.png"),
                    Raylib.LoadTexture("./images/Monstres/1/die/1_enemies_1_die_010.png"),
                    Raylib.LoadTexture("./images/Monstres/1/die/1_enemies_1_die_012.png")
            };

            Texture2D[] monstre2run = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/2/run/2_enemies_1_run_000.png"),
                    Raylib.LoadTexture("./images/Monstres/2/run/2_enemies_1_run_002.png"),
                    Raylib.LoadTexture("./images/Monstres/2/run/2_enemies_1_run_004.png"),
                    Raylib.LoadTexture("./images/Monstres/2/run/2_enemies_1_run_006.png"),
                    Raylib.LoadTexture("./images/Monstres/2/run/2_enemies_1_run_008.png"),
                    Raylib.LoadTexture("./images/Monstres/2/run/2_enemies_1_run_010.png"),
                    Raylib.LoadTexture("./images/Monstres/2/run/2_enemies_1_run_012.png"),
                    Raylib.LoadTexture("./images/Monstres/2/run/2_enemies_1_run_014.png"),
                    Raylib.LoadTexture("./images/Monstres/2/run/2_enemies_1_run_016.png"),
                    Raylib.LoadTexture("./images/Monstres/2/run/2_enemies_1_run_018.png")
            };
            Texture2D[] monstre2die = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/2/die/2_enemies_1_die_001.png"),
                    Raylib.LoadTexture("./images/Monstres/2/die/2_enemies_1_die_003.png"),
                    Raylib.LoadTexture("./images/Monstres/2/die/2_enemies_1_die_005.png"),
                    Raylib.LoadTexture("./images/Monstres/2/die/2_enemies_1_die_007.png"),
                    Raylib.LoadTexture("./images/Monstres/2/die/2_enemies_1_die_009.png"),
                    Raylib.LoadTexture("./images/Monstres/2/die/2_enemies_1_die_011.png"),
                    Raylib.LoadTexture("./images/Monstres/2/die/2_enemies_1_die_013.png"),
                    Raylib.LoadTexture("./images/Monstres/2/die/2_enemies_1_die_015.png"),
                    Raylib.LoadTexture("./images/Monstres/2/die/2_enemies_1_die_019.png")
            };

            Texture2D[] monstre3run = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/3/run/3_enemies_1_run_000.png"),
                    Raylib.LoadTexture("./images/Monstres/3/run/3_enemies_1_run_002.png"),
                    Raylib.LoadTexture("./images/Monstres/3/run/3_enemies_1_run_004.png"),
                    Raylib.LoadTexture("./images/Monstres/3/run/3_enemies_1_run_006.png"),
                    Raylib.LoadTexture("./images/Monstres/3/run/3_enemies_1_run_008.png"),
                    Raylib.LoadTexture("./images/Monstres/3/run/3_enemies_1_run_010.png"),
                    Raylib.LoadTexture("./images/Monstres/3/run/3_enemies_1_run_012.png"),
                    Raylib.LoadTexture("./images/Monstres/3/run/3_enemies_1_run_014.png"),
                    Raylib.LoadTexture("./images/Monstres/3/run/3_enemies_1_run_016.png"),
                    Raylib.LoadTexture("./images/Monstres/3/run/3_enemies_1_run_018.png")
            };
            Texture2D[] monstre3die = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/3/die/3_enemies_1_die_000.png"),
                    Raylib.LoadTexture("./images/Monstres/3/die/3_enemies_1_die_002.png"),
                    Raylib.LoadTexture("./images/Monstres/3/die/3_enemies_1_die_004.png"),
                    Raylib.LoadTexture("./images/Monstres/3/die/3_enemies_1_die_006.png"),
                    Raylib.LoadTexture("./images/Monstres/3/die/3_enemies_1_die_008.png"),
                    Raylib.LoadTexture("./images/Monstres/3/die/3_enemies_1_die_010.png"),
                    Raylib.LoadTexture("./images/Monstres/3/die/3_enemies_1_die_012.png"),
                    Raylib.LoadTexture("./images/Monstres/3/die/3_enemies_1_die_016.png"),
                    Raylib.LoadTexture("./images/Monstres/3/die/3_enemies_1_die_018.png")
            };

            Texture2D[] monstre4run = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/4/run/4_enemies_1_run_000.png"),
                    Raylib.LoadTexture("./images/Monstres/4/run/4_enemies_1_run_002.png"),
                    Raylib.LoadTexture("./images/Monstres/4/run/4_enemies_1_run_004.png"),
                    Raylib.LoadTexture("./images/Monstres/4/run/4_enemies_1_run_006.png"),
                    Raylib.LoadTexture("./images/Monstres/4/run/4_enemies_1_run_008.png"),
                    Raylib.LoadTexture("./images/Monstres/4/run/4_enemies_1_run_010.png"),
                    Raylib.LoadTexture("./images/Monstres/4/run/4_enemies_1_run_012.png"),
                    Raylib.LoadTexture("./images/Monstres/4/run/4_enemies_1_run_014.png"),
                    Raylib.LoadTexture("./images/Monstres/4/run/4_enemies_1_run_016.png"),
                    Raylib.LoadTexture("./images/Monstres/4/run/4_enemies_1_run_018.png")
            };
            Texture2D[] monstre4die = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/4/die/4_enemies_1_die_000.png"),
                    Raylib.LoadTexture("./images/Monstres/4/die/4_enemies_1_die_002.png"),
                    Raylib.LoadTexture("./images/Monstres/4/die/4_enemies_1_die_004.png"),
                    Raylib.LoadTexture("./images/Monstres/4/die/4_enemies_1_die_006.png"),
                    Raylib.LoadTexture("./images/Monstres/4/die/4_enemies_1_die_008.png"),
                    Raylib.LoadTexture("./images/Monstres/4/die/4_enemies_1_die_010.png"),
                    Raylib.LoadTexture("./images/Monstres/4/die/4_enemies_1_die_012.png"),
                    Raylib.LoadTexture("./images/Monstres/4/die/4_enemies_1_die_014.png"),
                    Raylib.LoadTexture("./images/Monstres/4/die/4_enemies_1_die_016.png"),
                    Raylib.LoadTexture("./images/Monstres/4/die/4_enemies_1_die_018.png")
            };

            Texture2D[] monstre5run = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/5/run/5_enemies_1_run_000.png"),
                    Raylib.LoadTexture("./images/Monstres/5/run/5_enemies_1_run_002.png"),
                    Raylib.LoadTexture("./images/Monstres/5/run/5_enemies_1_run_004.png"),
                    Raylib.LoadTexture("./images/Monstres/5/run/5_enemies_1_run_006.png"),
                    Raylib.LoadTexture("./images/Monstres/5/run/5_enemies_1_run_008.png"),
                    Raylib.LoadTexture("./images/Monstres/5/run/5_enemies_1_run_010.png"),
                    Raylib.LoadTexture("./images/Monstres/5/run/5_enemies_1_run_012.png"),
                    Raylib.LoadTexture("./images/Monstres/5/run/5_enemies_1_run_014.png"),
                    Raylib.LoadTexture("./images/Monstres/5/run/5_enemies_1_run_016.png"),
                    Raylib.LoadTexture("./images/Monstres/5/run/5_enemies_1_run_018.png")
            };
            Texture2D[] monstre5die = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/5/die/5_enemies_1_die_000.png"),
                    Raylib.LoadTexture("./images/Monstres/5/die/5_enemies_1_die_002.png"),
                    Raylib.LoadTexture("./images/Monstres/5/die/5_enemies_1_die_004.png"),
                    Raylib.LoadTexture("./images/Monstres/5/die/5_enemies_1_die_006.png"),
                    Raylib.LoadTexture("./images/Monstres/5/die/5_enemies_1_die_008.png"),
                    Raylib.LoadTexture("./images/Monstres/5/die/5_enemies_1_die_010.png"),
                    Raylib.LoadTexture("./images/Monstres/5/die/5_enemies_1_die_012.png"),
                    Raylib.LoadTexture("./images/Monstres/5/die/5_enemies_1_die_014.png"),
                    Raylib.LoadTexture("./images/Monstres/5/die/5_enemies_1_die_016.png"),
                    Raylib.LoadTexture("./images/Monstres/5/die/5_enemies_1_die_018.png")
            };

            Texture2D[] monstre6run = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/6/run/6_enemies_1_run_000.png"),
                    Raylib.LoadTexture("./images/Monstres/6/run/6_enemies_1_run_002.png"),
                    Raylib.LoadTexture("./images/Monstres/6/run/6_enemies_1_run_004.png"),
                    Raylib.LoadTexture("./images/Monstres/6/run/6_enemies_1_run_006.png"),
                    Raylib.LoadTexture("./images/Monstres/6/run/6_enemies_1_run_008.png"),
                    Raylib.LoadTexture("./images/Monstres/6/run/6_enemies_1_run_010.png"),
                    Raylib.LoadTexture("./images/Monstres/6/run/6_enemies_1_run_012.png"),
                    Raylib.LoadTexture("./images/Monstres/6/run/6_enemies_1_run_014.png"),
                    Raylib.LoadTexture("./images/Monstres/6/run/6_enemies_1_run_016.png"),
                    Raylib.LoadTexture("./images/Monstres/6/run/6_enemies_1_run_018.png")
            };
            Texture2D[] monstre6die = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/6/die/6_enemies_1_die_000.png"),
                    Raylib.LoadTexture("./images/Monstres/6/die/6_enemies_1_die_002.png"),
                    Raylib.LoadTexture("./images/Monstres/6/die/6_enemies_1_die_004.png"),
                    Raylib.LoadTexture("./images/Monstres/6/die/6_enemies_1_die_006.png"),
                    Raylib.LoadTexture("./images/Monstres/6/die/6_enemies_1_die_008.png"),
                    Raylib.LoadTexture("./images/Monstres/6/die/6_enemies_1_die_010.png"),
                    Raylib.LoadTexture("./images/Monstres/6/die/6_enemies_1_die_012.png"),
                    Raylib.LoadTexture("./images/Monstres/6/die/6_enemies_1_die_014.png"),
                    Raylib.LoadTexture("./images/Monstres/6/die/6_enemies_1_die_016.png"),
                    Raylib.LoadTexture("./images/Monstres/6/die/6_enemies_1_die_018.png")
            };

            Texture2D[] monstre7run = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/7/run/7_enemies_1_run_000.png"),
                    Raylib.LoadTexture("./images/Monstres/7/run/7_enemies_1_run_002.png"),
                    Raylib.LoadTexture("./images/Monstres/7/run/7_enemies_1_run_004.png"),
                    Raylib.LoadTexture("./images/Monstres/7/run/7_enemies_1_run_006.png"),
                    Raylib.LoadTexture("./images/Monstres/7/run/7_enemies_1_run_008.png"),
                    Raylib.LoadTexture("./images/Monstres/7/run/7_enemies_1_run_010.png"),
                    Raylib.LoadTexture("./images/Monstres/7/run/7_enemies_1_run_012.png"),
                    Raylib.LoadTexture("./images/Monstres/7/run/7_enemies_1_run_014.png"),
                    Raylib.LoadTexture("./images/Monstres/7/run/7_enemies_1_run_016.png"),
                    Raylib.LoadTexture("./images/Monstres/7/run/7_enemies_1_run_018.png")
            };
            Texture2D[] monstre7die = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/7/die/7_enemies_1_die_000.png"),
                    Raylib.LoadTexture("./images/Monstres/7/die/7_enemies_1_die_002.png"),
                    Raylib.LoadTexture("./images/Monstres/7/die/7_enemies_1_die_004.png"),
                    Raylib.LoadTexture("./images/Monstres/7/die/7_enemies_1_die_006.png"),
                    Raylib.LoadTexture("./images/Monstres/7/die/7_enemies_1_die_008.png"),
                    Raylib.LoadTexture("./images/Monstres/7/die/7_enemies_1_die_010.png"),
                    Raylib.LoadTexture("./images/Monstres/7/die/7_enemies_1_die_012.png"),
                    Raylib.LoadTexture("./images/Monstres/7/die/7_enemies_1_die_018.png"),
                    Raylib.LoadTexture("./images/Monstres/7/die/7_enemies_1_die_019.png")
            };

            Texture2D[] monstre8run = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/8/run/8_enemies_1_run_000.png"),
                    Raylib.LoadTexture("./images/Monstres/8/run/8_enemies_1_run_002.png"),
                    Raylib.LoadTexture("./images/Monstres/8/run/8_enemies_1_run_004.png"),
                    Raylib.LoadTexture("./images/Monstres/8/run/8_enemies_1_run_006.png"),
                    Raylib.LoadTexture("./images/Monstres/8/run/8_enemies_1_run_008.png"),
                    Raylib.LoadTexture("./images/Monstres/8/run/8_enemies_1_run_010.png"),
                    Raylib.LoadTexture("./images/Monstres/8/run/8_enemies_1_run_012.png"),
                    Raylib.LoadTexture("./images/Monstres/8/run/8_enemies_1_run_014.png"),
                    Raylib.LoadTexture("./images/Monstres/8/run/8_enemies_1_run_016.png"),
                    Raylib.LoadTexture("./images/Monstres/8/run/8_enemies_1_run_018.png")
            };
            Texture2D[] monstre8die = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/8/die/8_enemies_1_die_000.png"),
                    Raylib.LoadTexture("./images/Monstres/8/die/8_enemies_1_die_002.png"),
                    Raylib.LoadTexture("./images/Monstres/8/die/8_enemies_1_die_004.png"),
                    Raylib.LoadTexture("./images/Monstres/8/die/8_enemies_1_die_006.png"),
                    Raylib.LoadTexture("./images/Monstres/8/die/8_enemies_1_die_008.png"),
                    Raylib.LoadTexture("./images/Monstres/8/die/8_enemies_1_die_010.png"),
                    Raylib.LoadTexture("./images/Monstres/8/die/8_enemies_1_die_012.png"),
                    Raylib.LoadTexture("./images/Monstres/8/die/8_enemies_1_die_014.png"),
                    Raylib.LoadTexture("./images/Monstres/8/die/8_enemies_1_die_016.png"),
                    Raylib.LoadTexture("./images/Monstres/8/die/8_enemies_1_die_018.png")
            };

            Texture2D[] monstre9run = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/9/run/9_enemies_1_run_000.png"),
                    Raylib.LoadTexture("./images/Monstres/9/run/9_enemies_1_run_002.png"),
                    Raylib.LoadTexture("./images/Monstres/9/run/9_enemies_1_run_004.png"),
                    Raylib.LoadTexture("./images/Monstres/9/run/9_enemies_1_run_006.png"),
                    Raylib.LoadTexture("./images/Monstres/9/run/9_enemies_1_run_008.png"),
                    Raylib.LoadTexture("./images/Monstres/9/run/9_enemies_1_run_010.png"),
                    Raylib.LoadTexture("./images/Monstres/9/run/9_enemies_1_run_012.png"),
                    Raylib.LoadTexture("./images/Monstres/9/run/9_enemies_1_run_014.png"),
                    Raylib.LoadTexture("./images/Monstres/9/run/9_enemies_1_run_016.png"),
                    Raylib.LoadTexture("./images/Monstres/9/run/9_enemies_1_run_018.png")
            };
            Texture2D[] monstre9die = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/9/die/9_enemies_1_die_000.png"),
                    Raylib.LoadTexture("./images/Monstres/9/die/9_enemies_1_die_002.png"),
                    Raylib.LoadTexture("./images/Monstres/9/die/9_enemies_1_die_004.png"),
                    Raylib.LoadTexture("./images/Monstres/9/die/9_enemies_1_die_006.png"),
                    Raylib.LoadTexture("./images/Monstres/9/die/9_enemies_1_die_008.png"),
                    Raylib.LoadTexture("./images/Monstres/9/die/9_enemies_1_die_010.png"),
                    Raylib.LoadTexture("./images/Monstres/9/die/9_enemies_1_die_012.png"),
                    Raylib.LoadTexture("./images/Monstres/9/die/9_enemies_1_die_014.png"),
                    Raylib.LoadTexture("./images/Monstres/9/die/9_enemies_1_die_016.png"),
                    Raylib.LoadTexture("./images/Monstres/9/die/9_enemies_1_die_018.png")
            };

            Texture2D[] monstre10run = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/10/run/10_enemies_1_run_000.png"),
                    Raylib.LoadTexture("./images/Monstres/10/run/10_enemies_1_run_002.png"),
                    Raylib.LoadTexture("./images/Monstres/10/run/10_enemies_1_run_004.png"),
                    Raylib.LoadTexture("./images/Monstres/10/run/10_enemies_1_run_006.png"),
                    Raylib.LoadTexture("./images/Monstres/10/run/10_enemies_1_run_008.png"),
                    Raylib.LoadTexture("./images/Monstres/10/run/10_enemies_1_run_010.png"),
                    Raylib.LoadTexture("./images/Monstres/10/run/10_enemies_1_run_012.png"),
                    Raylib.LoadTexture("./images/Monstres/10/run/10_enemies_1_run_014.png"),
                    Raylib.LoadTexture("./images/Monstres/10/run/10_enemies_1_run_016.png"),
                    Raylib.LoadTexture("./images/Monstres/10/run/10_enemies_1_run_018.png")
            };
            Texture2D[] monstre10die = new Texture2D[]
            {
                    Raylib.LoadTexture("./images/Monstres/10/die/10_enemies_1_die_000.png"),
                    Raylib.LoadTexture("./images/Monstres/10/die/10_enemies_1_die_002.png"),
                    Raylib.LoadTexture("./images/Monstres/10/die/10_enemies_1_die_004.png"),
                    Raylib.LoadTexture("./images/Monstres/10/die/10_enemies_1_die_006.png"),
                    Raylib.LoadTexture("./images/Monstres/10/die/10_enemies_1_die_008.png"),
                    Raylib.LoadTexture("./images/Monstres/10/die/10_enemies_1_die_010.png"),
                    Raylib.LoadTexture("./images/Monstres/10/die/10_enemies_1_die_012.png"),
                    Raylib.LoadTexture("./images/Monstres/10/die/10_enemies_1_die_014.png"),
                    Raylib.LoadTexture("./images/Monstres/10/die/10_enemies_1_die_016.png"),
                    Raylib.LoadTexture("./images/Monstres/10/die/10_enemies_1_die_018.png")
            };
            #endregion


            ///////////// Boucle menu /////////////
            while (!start && !stop)
            {
                MousePoint = Raylib.GetMousePosition();

                //////// Boutton Vérif ////////////
                // BtnStart
                if (Raylib.CheckCollisionPointRec(MousePoint, btnStart) && Raylib.IsMouseButtonDown(MouseButton.Left))
                    start = true;

                // BtnStop
                else if (Raylib.CheckCollisionPointRec(MousePoint, btnStop) && Raylib.IsMouseButtonDown(MouseButton.Left))
                    stop = true;


                ////// Début Dessin ///////
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                //// Si le Debug est activé
                if (DebugActivated)
                {
                    Raylib.DrawText(texte, 12, 12, 20, Color.Black);
                    Raylib.DrawText($"X:{MousePoint.X} Y:{MousePoint.Y}", 12, 35, 20, Color.Black);
                }
                DessinerMenu();
                Raylib.EndDrawing();

            }
            if (start)
            {
                ////////////// Déclarations des textures ////////////////////////////////////////////////////////////
                //// pour libèrer des la place dans la ram lorsqu'on est dans le menu ////
                //// Aussi pour éviter de charger trop longtemps dans les menu ////
                Fond = Raylib.LoadTexture("./images/backgroundgame.png");
                BaseV = Raylib.LoadTexture("./images/base1.png");
                Cible = Raylib.LoadTexture("./images/Target-icon.png");
                Coeur = Raylib.LoadTexture("./images/Coeur.png");
                Argent = Raylib.LoadTexture("./images/Argent.png");
                Poubelle = Raylib.LoadTexture("./images/poubelle.png");
                Crane = Raylib.LoadTexture("./images/crane.png");
                Vague = Raylib.LoadTexture("./images/Wave.png");

                //// Image Canon ////
                Cannon = Raylib.LoadTexture(@"./images/Cannon/Cannon.png");
                Mg = Raylib.LoadTexture(@"./images/Cannon/MG.png");
                MissileLauncher = Raylib.LoadTexture(@"./images/Cannon/Missile_Launcher.png");

                Task.Run(Vagues.Update);

                ///////////// Boucle principale /////////////
                while (!stop)
                {
                    MousePoint = Raylib.GetMousePosition();
                    if (Raylib.CheckCollisionPointRec(MousePoint, BtnAffichage[0]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                        MenuOuvert = true;

                    if (Raylib.CheckCollisionPointRec(MousePoint, BtnAffichage[1]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        ModeDestruction = false;
                        if (ModeConstruction)
                            ModeConstruction = false;
                        else
                            ModeConstruction = true;

                        ChoixTourOuvert = false;
                    }
                    if (Raylib.CheckCollisionPointRec(MousePoint, BtnAffichage[2]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        ModeConstruction = false;
                        if (ModeDestruction)
                            ModeDestruction = false;
                        else
                            ModeDestruction = true;

                        ChoixTourOuvert = false;
                    }
                    ModeDeconstruction();

                    if (Raylib.IsKeyPressed(KeyboardKey.Escape))
                        MenuOuvert = true;

                    Vagues.WaitForEnnemy = true;
                    for (int i = 0; i < Enemies.Count; i++)
                    {
                        try
                        {
                            Direction(Enemies[i]);
                            Enemies[i].Go();
                        }
                        catch { }
                    }
                    Vagues.WaitForEnnemy = false;


                    List<Bullet> bulletsToRemove = new List<Bullet>();

                    Vagues.WaitForEnnemy = true;
                    foreach (Bullet balle in Bullets)
                    {

                        if (HitEnnemy(balle, out Enemy touche) || !Enemies.Contains(balle.Target) || balle.Target.Mourrant)
                        {
                            bulletsToRemove.Add(balle);
                        }
                        else if (balle.Position.X > Raylib.GetScreenWidth() || balle.Position.Y > Raylib.GetScreenHeight())
                            bulletsToRemove.Add(balle);
                        else if (Enemies.Count == 0)
                            bulletsToRemove.Add(balle);
                        else if (balle.Position.X < 0 || balle.Position.Y < 0)
                            bulletsToRemove.Add(balle);
                    }
                    Vagues.WaitForEnnemy = false;
                    foreach (Bullet balle in bulletsToRemove)
                    {
                        Bullets.Remove(balle.Destroy(Explosions));
                    }
                    Bullets.Order();
                    bulletsToRemove.Clear();

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.White);
                    DessinerJeuFond();
                    DessinerEntitees(monstre1run, monstre2run, monstre3run, monstre4run, monstre5run, monstre6run, monstre7run, monstre8run, monstre9run, monstre10run, monstre1die, monstre2die, monstre3die, monstre4die, monstre5die, monstre6die, monstre7die, monstre8die, monstre9die, monstre10die);
                    ExplosionM();
                    DessinerBase();
                    MenuConstruction();
                    DessinMenuConstruction();
                    DessinerGui();
                    killEnemy();

                    foreach (Rectangle r in ObjetNonPosable) 
                    {
                        Raylib.DrawRectangleRec(r, new(255,0,0,50));
                    }
                    foreach ( Rectangle r in CheminNonPosable) 
                    {
                        Raylib.DrawRectangleRec(r, new(255, 0, 0, 50));
                    }
                    Raylib.EndDrawing();

                    //////////////////////////// MENU ///////////////////////
                    while (MenuOuvert)
                    {
                        MousePoint = Raylib.GetMousePosition();
                        if ((Raylib.CheckCollisionPointRec(MousePoint, btnStart) && Raylib.IsMouseButtonDown(MouseButton.Left)) || (Raylib.CheckCollisionPointRec(MousePoint, BtnAffichage[0]) && Raylib.IsMouseButtonPressed(MouseButton.Left)))
                            MenuOuvert = false;
                        else if (Raylib.CheckCollisionPointRec(MousePoint, btnStop) && Raylib.IsMouseButtonDown(MouseButton.Left))
                        {
                            MenuOuvert = false;
                            stop = true;
                        }

                        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
                            MenuOuvert = false;

                        ModeDestruction = false;
                        ModeConstruction = false;

                        Raylib.BeginDrawing();
                        Raylib.ClearBackground(Color.White);
                        DessinerJeuFond();
                        DessinerEntitees(monstre1run, monstre2run, monstre3run, monstre4run, monstre5run, monstre6run, monstre7run, monstre8run, monstre9run, monstre10run, monstre1die, monstre2die, monstre3die, monstre4die, monstre5die, monstre6die, monstre7die, monstre8die, monstre9die, monstre10die);
                        DessinerBase();
                        MenuConstruction();
                        DessinMenuConstruction();
                        DessinerGui();

                        Raylib.DrawText("PAUSE", 822, 225, 80, Color.Black);

                        Raylib.DrawRectangleRec(btnStart, Color.Lime);
                        Raylib.DrawText("Continuer", 845, 480, 50, Color.Black);

                        Raylib.DrawRectangleRec(btnStop, Color.Red);
                        Raylib.DrawText("Quit", 910, 685, 50, Color.Black);

                        Raylib.EndDrawing();
                    }
                    /////////////////////////// MENU ////////////////////////////
                }
            }
            #region Unload Textures

            Raylib.UnloadTexture(Fond);
            Raylib.UnloadTexture(BaseV);
            Raylib.UnloadTexture(Cible);
            Raylib.UnloadTexture(Coeur);
            Raylib.UnloadTexture(Argent);
            Raylib.UnloadTexture(Poubelle);
            Raylib.UnloadTexture(Cannon);
            Raylib.UnloadTexture(Mg);
            Raylib.UnloadTexture(MissileLauncher);

            foreach (Texture2D texture in monstre1run)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre2run)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre3run)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre4run)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre5run)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre6run)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre7run)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre8run)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre9run)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre10run)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre1die)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre2die)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre3die)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre4die)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre5die)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre6die)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre7die)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre8die)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre9die)
                Raylib.UnloadTexture(texture);

            foreach (Texture2D texture in monstre10die)
                Raylib.UnloadTexture(texture);
            #endregion

            Raylib.CloseWindow();
        }

        #region Dessin

        static void DessinerMenu()
        {
            // Titre //
            Raylib.DrawText("Tower Defense", 685, 150, 70, Color.Black);
            // Avertissment si l'écran est trop petit //
            if (Raylib.GetScreenHeight() < 1080 || Raylib.GetRenderWidth() < 1920)
                Raylib.DrawText("Votre Experience de jeu ne sera pas optimal votre écran est trop petit minimun:1080p", 12, 12, 20, Color.Red);
            // BtnStart //
            Raylib.DrawRectangle(750, 450, 420, 110, Color.Gold);
            Raylib.DrawText("Start", 885, 485, 50, Color.Black);
            // BtnQuit //
            Raylib.DrawRectangle(750, 650, 420, 110, Color.Gold);
            Raylib.DrawText("Quit", 910, 685, 50, Color.Black);

        }


        static void DessinerBase()
        {
            //Raylib.DrawRectangle(1860, 910, 75, 70, Color.Green);
            Raylib.DrawTexturePro(BaseV, new Rectangle(0, 0, BaseV.Width, BaseV.Height), new Rectangle(1815, 735, BaseV.Width / 4, BaseV.Height / 4), new Vector2(0, 0), 0.0f, Color.White);
        }

        static void DessinerEntitees(Texture2D[] monstre1run, Texture2D[] monstre2run, Texture2D[] monstre3run, Texture2D[] monstre4run, Texture2D[] monstre5run, Texture2D[] monstre6run, Texture2D[] monstre7run, Texture2D[] monstre8run, Texture2D[] monstre9run, Texture2D[] monstre10run, Texture2D[] monstre1die, Texture2D[] monstre2die, Texture2D[] monstre3die, Texture2D[] monstre4die, Texture2D[] monstre5die, Texture2D[] monstre6die, Texture2D[] monstre7die, Texture2D[] monstre8die, Texture2D[] monstre9die, Texture2D[] monstre10die)
        {
            foreach (Bullet bullet in Bullets)
            {
                Vagues.WaitForEnnemy = true;
                if (Enemies.Count > 0 || bullet.Target != null)
                {
                    // il y a un ! pour dire au compilateur "Ici c'est pas null"
                    bullet.Rotation = getRotation(bullet.Target!.position, bullet.Position, 90);
                }
                Vagues.WaitForEnnemy = false;
                bullet.Draw();
            }

            foreach (Canon canon in Canons)
                canon.Draw();

            List<Enemy> EnemyToRemove = new List<Enemy>();
            Vagues.WaitForEnnemy = true;
            Enemies.Reverse();
            foreach (Enemy enemy in Enemies.ToList()) // Utiliser ToList pour éviter les modifications pendant l'itération
            {
                enemy.DessinerLifeBar();
                enemy.UpdateLife();

                if (!MenuOuvert)
                    enemy.UpdateTimer();

                switch (enemy.EnemyType)
                {
                    case 1:
                        PlayAnime(enemy, monstre1run, monstre1die, EnemyToRemove);
                        break;
                    case 2:
                        PlayAnime(enemy, monstre2run, monstre2die, EnemyToRemove);
                        break;
                    case 3:
                        PlayAnime(enemy, monstre3run, monstre3die, EnemyToRemove);
                        break;
                    case 4:
                        PlayAnime(enemy, monstre4run, monstre4die, EnemyToRemove);
                        break;
                    case 5:
                        PlayAnime(enemy, monstre5run, monstre5die, EnemyToRemove);
                        break;
                    case 6:
                        PlayAnime(enemy, monstre6run, monstre6die, EnemyToRemove);
                        break;
                    case 7:
                        PlayAnime(enemy, monstre7run, monstre7die, EnemyToRemove);
                        break;
                    case 8:
                        PlayAnime(enemy, monstre8run, monstre8die, EnemyToRemove);
                        break;
                    case 9:
                        PlayAnime(enemy, monstre9run, monstre9die, EnemyToRemove);
                        break;
                    case 10:
                        PlayAnime(enemy, monstre10run, monstre10die, EnemyToRemove);
                        break;
                }

            }
            Enemies.Reverse();

            // Après avoir parcouru la collection, supprimer les ennemis marqués
            foreach (Enemy deadGuy in EnemyToRemove)
            {
                Enemies.Remove(deadGuy);
            }

            // Réorganiser la liste après les suppressions, si nécessaire
            Enemies.Order();
            EnemyToRemove.Clear();
            Vagues.WaitForEnnemy = false;

            foreach (Canon canon2 in Canons)
            {
                if (Raylib.CheckCollisionPointCircle(Raylib.GetMousePosition(), canon2.Position, canon2.hitbox) && !ModeDestruction)
                {
                    Raylib.DrawCircleLinesV(canon2.Position, canon2.PorteeTir, Color.Black);
                    Raylib.DrawText(canon2.getPrice().ToString(), (int)canon2.Position.X - 15, (int)canon2.Position.Y + 35, 20, Color.Black);
                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        canon2.Upgrade(ref Money);
                    }
                }

                Vagues.WaitForEnnemy = true;
                canon2.UpdateTimer();
                if (Enemies.Count != 0)
                {
                    if (!EnemyLeMieux(canon2).Placebo)
                    {
                        canon2.Fire(Bullets, EnemyLeMieux(canon2));
                        try
                        {
                            canon2.setRotation(getRotation(EnemyLeMieux(canon2).position, canon2.Position, 0));
                        }
                        catch { }
                    }
                }
                Vagues.WaitForEnnemy = false;
            }
        }
        static void PlayAnime(Enemy enemy, Texture2D[] textureRun, Texture2D[] textureDie, List<Enemy> EnemyToRemove)
        {
            enemy.PlayRunAnime(textureRun);
            if (enemy.PlayDieAnime(textureDie))
            {
                Vagues.NombreRestantDeMonstre--;
                EnemyToRemove.Add(enemy);

            }

        }

        static void DessinerGui()
        {
            Raylib.DrawRectangleGradientV(0, 0, 1920, 80, Color.Blue, Color.DarkBlue);
            if (DebugActivated)
                Raylib.DrawText($"X:{MousePoint.X} Y:{MousePoint.Y}", 100, 35, 20, Color.Black);

            // Menu Burger
            Raylib.DrawRectangleRounded(BtnAffichage[0], 0.2f, 4, Color.SkyBlue);
            Raylib.DrawLineEx(new Vector2(20, 25), new Vector2(60, 25), 6f, Color.Black);
            Raylib.DrawLineEx(new Vector2(20, 40), new Vector2(60, 40), 6f, Color.Black);
            Raylib.DrawLineEx(new Vector2(20, 55), new Vector2(60, 55), 6f, Color.Black);

            // Menu Construction
            Raylib.DrawRectangleRounded(BtnAffichage[1], 0.2f, 4, Color.SkyBlue); //Dessin contour bouton menu tours
            Raylib.DrawTextureEx(Cible, BtnAffichage[1].Position + new Vector2(4.5f, 5), 0, 0.1f, Color.White); //affichage de l'icon dans le menu des tours

            // Menu Destruction
            Raylib.DrawRectangleRounded(BtnAffichage[2], 0.2f, 4, Color.SkyBlue);
            Raylib.DrawTextureEx(Poubelle, BtnAffichage[2].Position + new Vector2(4.5f, 5), 0, 0.1f, Color.White);

            // Nombre d'ennemi Restant 
            Raylib.DrawRectangleRounded(new Rectangle(1100, 10, 230, 60), 0.2f, 4, Color.SkyBlue);
            Raylib.DrawTextureEx(Crane, new Vector2(1180, 10) + new Vector2(95, 5), 0, 0.45f, Color.White);
            Raylib.DrawText($" {Vagues.NombreRestantDeMonstre} / {Vagues.NbMonstres}", 1100, 22, 40, Color.Black);

            // Nombre de Vagues Restantes
            Raylib.DrawRectangleRounded(new Rectangle(910, 10, 160, 60), 0.2f, 4, Color.SkyBlue);
            Raylib.DrawTextureEx(Vague, new Vector2(980, 10), 0,0.15f, Color.White);
            Raylib.DrawText($"{Vagues.Wave}", 930, 22, 40, Color.Black);

            // Argent
            Raylib.DrawRectangleRounded(new Rectangle(1000 + 650, 10, new(250, 60)), 0.2f, 4, Color.SkyBlue);
            Raylib.DrawTextureEx(Argent, new Vector2(1000 + 200 + 650, 20), 0f, 0.2f, Color.White);
            Raylib.DrawText(Money.ToString(), 1010 + 650, 22, 40, Color.Black);

            // Barre de vie
            if (VieActuelle > 100)
                VieActuelle = 100;
            Color color = Color.DarkGray;
            if (VieActuelle > 80)
                color = Color.DarkGreen;
            else if (VieActuelle > 60)
                color = Color.Green;
            else if (VieActuelle > 40)
                color = Color.Yellow;
            else if (VieActuelle > 20)
                color = Color.Orange;
            else if (VieActuelle > 0)
                color = Color.Red;
            Raylib.DrawRectangleRounded(new Rectangle(1380 - 50 + 25, 10, new(300 - 25, 60)), 0.2f, 4, Color.SkyBlue);
            Raylib.DrawRectangle(1365, 25, 200, 30, Color.Black);
            Raylib.DrawRectangle(1365, 25, Convert.ToInt32(VieActuelle) * 2, 30, color);
            Raylib.DrawRectangleLines(1365, 25, Convert.ToInt32(VieActuelle) * 2, 30, Color.Black);
            Raylib.DrawTextureEx(Coeur, new(1580, 20), 0f, 0.9f, Color.White);


        }

        static void DessinerJeuFond()
        {
            Raylib.DrawTextureEx(Fond, new Vector2(0, 80), 0f, 1f, Color.White);
        }
        #endregion

        #region Calcul Enemies
        static void Direction(Enemy monstre)
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
                VieActuelle -= monstre.vie;
                Vagues.WaitForEnnemy = true;
                Enemies.Remove(monstre);
                Enemies.Order();
                Vagues.WaitForEnnemy = false;
                Vagues.NombreRestantDeMonstre--;
            }
        }

        static float getRotation(Vector2 Ennemy, Vector2 Tour, float offset)
        {
            float deltaY = Ennemy.Y - Tour.Y;
            float deltaX = Ennemy.X - Tour.X;
            return (float)(Math.Atan2(deltaY, deltaX) * (180.0 / Math.PI)) + offset;
        }

        static bool HitEnnemy(Bullet balle, out Enemy Touche)
        {
            bool hit = false;
            Touche = new Enemy(Vector2.Zero);
            Vagues.WaitForEnnemy = true;
            foreach (Enemy enemy in Enemies)
            {
                if (Raylib.CheckCollisionCircleRec(enemy.position, enemy.size, balle.rctDest) && !enemy.Mourrant)
                {
                    hit = true;
                    Touche = enemy;
                    if (!(balle.Type == 3))
                        Touche.vie -= balle.Degats;
                }
            }
            Vagues.WaitForEnnemy = false;

            return hit;
        }

        static Enemy EnemyLeMieux(Canon canon)
        {
            // Initialiser Mieu avec le premier ennemi de la liste qui est dans la portée du canon

            Enemy? Mieu = null;
            float maxDistance = canon.PorteeTir;

            Enemies.Reverse();

            foreach (Enemy enemy in Enemies)
            {
                float distance = Vector2.Distance(canon.Position, enemy.position);
                if (distance <= canon.PorteeTir && !enemy.Mourrant)
                {
                    maxDistance = distance;
                    Mieu = enemy;

                }
            }

            Enemies.Reverse();

            // Si aucun ennemi n'est trouvé dans la portée, retourner le premier ennemi
            if (Mieu == null)
            {
                Mieu = new Enemy();
            }

            return Mieu;
        }

        static void ExplosionM()
        {
            List<Explosion> explosionsToRemove = new List<Explosion>();
            foreach (Explosion explosion in Explosions)
            {
                if (explosion.Stade == 2)
                {
                    foreach (Enemy enemy in Enemies)
                    {
                        if (Raylib.CheckCollisionCircles(enemy.position, enemy.size, explosion.Position, Explosion.EXPLOSIONRADIUS) && !explosion.DegatFait)
                        {
                            enemy.vie -= explosion.Degats;
                            explosion.DegatFait = true;
                        }

                    }
                }
                if (explosion.UpdateTimer())
                {
                    explosionsToRemove.Add(explosion);
                }
            }
            foreach (Explosion explosion in explosionsToRemove)
            {
                Explosions.Remove(explosion);
            }
            Explosions.Order();
            explosionsToRemove.Clear();
        }

        #endregion

        #region Construction

        static void MenuConstruction()
        {
            if (ModeConstruction)
            {
                Raylib.DrawText("Mode Construction activé", 370, 30, 20, Color.Red);

                if (ChoixTourOuvert)
                {
                    canon.Place(tempMousePosition,Color.White);
                    BtnChoixTour[0].Position = tempMousePosition + new Vector2(90 - 35, 50);
                    BtnChoixTour[1].Position = tempMousePosition + new Vector2(0 - 35, 50);
                    BtnChoixTour[2].Position = tempMousePosition + new Vector2(-90 - 35, 50);
                    Raylib.DrawRectangleRounded(BtnChoixTour[0], 0.2f, 4, Color.SkyBlue);
                    Raylib.DrawTextureEx(Cannon, BtnChoixTour[0].Position + new Vector2(52.5f, -7.5f), 45f, 0.3f, Color.White);
                    Raylib.DrawText(PRIXCANON.ToString(), (int)BtnChoixTour[0].X + 5, (int)BtnChoixTour[0].Y + 5, 15, Color.Black);
                    Raylib.DrawRectangleRounded(BtnChoixTour[1], 0.2f, 4, Color.SkyBlue);
                    Raylib.DrawTextureEx(Mg, BtnChoixTour[1].Position + new Vector2(45, -2.5f), 45f, 0.3f, Color.White);
                    Raylib.DrawText(PRIXROCKETLAUNCHER.ToString(), (int)BtnChoixTour[1].X + 5, (int)BtnChoixTour[1].Y + 5, 15, Color.Black);
                    Raylib.DrawRectangleRounded(BtnChoixTour[2], 0.2f, 4, Color.SkyBlue);
                    Raylib.DrawTextureEx(MissileLauncher, BtnChoixTour[2].Position + new Vector2(45, 0), 45f, 0.3f, Color.White);
                    Raylib.DrawText(PRIXMG.ToString(), (int)BtnChoixTour[2].X + 5, (int)BtnChoixTour[2].Y + 5, 15, Color.Black);
                }
                else
                {
                    canon.Place(MousePoint,!(TourCollide(CheminNonPosable)|| TourCollide(BlockNonPosable) /*|| TourCollide()*/) ? Color.White : new Color(0,0,0,35));
                }
            }
        }
        static void killEnemy()
        {
            Vagues.WaitForEnnemy = true;
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.vie <= 0)
                {
                    enemy.Mourrant = true;
                }
            }
            Vagues.WaitForEnnemy = false;
        }

        static bool TourCollide(Rectangle[] chm)
        {
            MousePoint = Raylib.GetMousePosition();

            bool touche = false;
            foreach (Rectangle bout in chm)
            {
                if (Raylib.CheckCollisionCircleRec(MousePoint, 40f, bout))
                    touche = true;
            }
            foreach (Canon canon1 in Canons)
            {
                if (Raylib.CheckCollisionCircles(MousePoint, 40f, canon1.Position, 40f))
                    touche = true;
            }
            return touche;
        }

        static bool TourCollideMousePointer(out Canon? canon)
        {
            foreach (Canon canon1 in Canons)
            {
                if (Raylib.CheckCollisionPointCircle(MousePoint,canon1.Position,40f))
                {
                    canon = canon1;
                    return true;
                }
            }
            canon = null;
            return false;
        }

        static void DessinMenuConstruction()
        {
            if (ModeDestruction)
            {
                Raylib.DrawRectangleLinesEx(new Rectangle(0, 80, Raylib.GetRenderWidth(), Raylib.GetScreenHeight() - 80f), 7f, Color.Red);
            }

            if (ModeConstruction)
            {
                if ((Raylib.IsMouseButtonPressed(MouseButton.Left) && !Raylib.CheckCollisionPointRec(MousePoint, BtnAffichage[1]) && !TourCollide(CheminNonPosable)) && !TourCollide(BlockNonPosable) && !TourCollide(ObjetNonPosable) && !ChoixTourOuvert)
                {
                    ChoixTourOuvert = true;
                    tempMousePosition = MousePoint;
                }
                if (Raylib.CheckCollisionPointRec(MousePoint, BtnChoixTour[0]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    if (Money >= PRIXCANON)
                    {
                        Canons.Add(new Canon(tempMousePosition, 1));
                        ChoixTourOuvert = false;
                        ModeConstruction = false;
                        BtnChoixTour[0].Position = new Vector2(0, 0);
                        BtnChoixTour[1].Position = new Vector2(0, 0);
                        BtnChoixTour[2].Position = new Vector2(0, 0);
                        Money -= PRIXCANON;
                    }
                }
                else if (Raylib.CheckCollisionPointRec(MousePoint, BtnChoixTour[1]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    if (Money >= PRIXROCKETLAUNCHER)
                    {
                        Canons.Add(new Canon(tempMousePosition, 2));
                        ChoixTourOuvert = false;
                        ModeConstruction = false;
                        BtnChoixTour[0].Position = new Vector2(0, 0);
                        BtnChoixTour[1].Position = new Vector2(0, 0);
                        BtnChoixTour[2].Position = new Vector2(0, 0);
                        Money -= PRIXROCKETLAUNCHER;
                    }
                }
                else if (Raylib.CheckCollisionPointRec(MousePoint, BtnChoixTour[2]) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    if (Money >= PRIXMG)
                    {
                        Canons.Add(new Canon(tempMousePosition, 3));
                        ChoixTourOuvert = false;
                        ModeConstruction = false;
                        BtnChoixTour[0].Position = new Vector2(0, 0);
                        BtnChoixTour[1].Position = new Vector2(0, 0);
                        BtnChoixTour[2].Position = new Vector2(0, 0);
                        Money -= PRIXMG;
                    }
                }
            }
        }
        static void ModeDeconstruction()
        {
            if (ModeDestruction)
            {
                // dire qu'il est activé

                if (TourCollideMousePointer(out Canon? canon) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    if (canon != null)
                    {
                        Canons.Remove(canon);
                    }
                }
            }
        }

        #endregion
    }
}