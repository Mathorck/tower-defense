using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Squelette
{
    public class Vagues
    {
        public static int Wave = 50;
        public static int MonstresRestants = 0;
        public static bool VagueTerminee = false;
        public static int NbMonstres = 0;
        private static Random R = new Random();
        private static int Rand;

        public static async Task Update()
        {
            await Task.Delay(3000);
            // for (;;) = while (true)
            for (;;)
            {
                if (!Program.menuOuvert)
                {
                    MonstresRestants = Program.Enemies.Count;
                    if (VagueTerminee)
                    {
                        await Task.Delay(3000);
                        await LancerNouvelleVague();
                    }
                    else
                    {
                        if (MonstresRestants <= 0)
                        {
                            VagueTerminee = true;
                        }
                    }
                }
            }
        }

        public static async Task LancerNouvelleVague()
        {
            Wave++;
            NbMonstres = 5 + (Wave * 3);
            VagueTerminee = false;


            for (int i = 0; i < NbMonstres; i++)
            {
                await Task.Delay(1000);
                Rand = R.Next(1, 3);
                // Bout de code compliqué mais en gros en fonction du chiffre tiré au dessu cela prend soit la porte1 soit la porte2
                // c'est un if sur une ligne
                Program.Enemies.Add(new Enemy(Rand==1?Program.porteMonstre1:Program.porteMonstre2, 5f, 20, 200, 5));
            }
        }
    }
}
