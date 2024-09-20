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
        public static int Wave = 0;
        public static int MonstresRestants = 0;
        public static bool VagueTerminee = false;
        public static int NbMonstres = 0;

        public static async Task Update()
        {
            await Task.Delay(3000);
            for (; ; )
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

        public static async Task LancerNouvelleVague()
        {
            Wave++;
            NbMonstres = 5 + (Wave * 3);
            VagueTerminee = false;


            for (int i = 0; i < NbMonstres; i++)
            {
                await Task.Delay(1000);
                Program.Enemies.Add(new Enemy(Program.porteMonstre1, 5f, Color.SkyBlue, 20, 200));
            }
        }
    }
}
