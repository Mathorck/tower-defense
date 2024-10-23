using System.Runtime.CompilerServices;

namespace Squelette
{
    public class Vagues
    {
        public static int Wave = 0;
        public static int MonstresRestants = 0;
        public static bool VagueTerminee = false;
        public static int NbMonstres = 0;
        public static bool WaitForEnnemy = false;
        public static int HardnessOfTheWave;
        public static int NombreRestantDeMonstre = 0;

        private static Random R = new Random();
        private static int Rand;
        private static int RandMonstre;
        private static int randomTime;

        public static async Task Update()
        {
            await Task.Delay(3000);
            while (Program.VieActuelle > 0)
            {
                if (!Program.MenuOuvert)
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
            Console.WriteLine("INFO: THREAD: Thread wawe stopped");
        }

        public static async Task LancerNouvelleVague()
        {
            bool bossAlreadySpawned =false;
            Wave++;
            NbMonstres = 5 + (Wave * 3);
            VagueTerminee = false;
            NombreRestantDeMonstre = NbMonstres;


            if (Wave > 45)
                HardnessOfTheWave = 10;
            else if (Wave > 40)
                HardnessOfTheWave = 9;
            else if (Wave > 35)
                HardnessOfTheWave = 8;
            else if (Wave > 30)
                HardnessOfTheWave = 7;
            else if (Wave > 25)
                HardnessOfTheWave = 6;
            else if (Wave > 20)
                HardnessOfTheWave = 5;
            else if (Wave > 15)
                HardnessOfTheWave = 4;
            else if (Wave > 10)
                HardnessOfTheWave = 3;
            else if (Wave > 0)
                HardnessOfTheWave = 2;


            for (int i = 0; i < NbMonstres; i++)
            {
                if (Wave % 10 == 0 && !bossAlreadySpawned)
                {
                    bossAlreadySpawned = true;
                    Program.Enemies.Add(new Enemy(Rand == 1 ? Program.porteMonstre1 : Program.porteMonstre2, 10));
                }

                randomTime = R.Next(1000/HardnessOfTheWave, 2000/HardnessOfTheWave);
                await Task.Delay(randomTime);

                Rand = R.Next(1, 3);
                RandMonstre = R.Next(1, HardnessOfTheWave);

                // Bout de code compliqué mais en gros en fonction du chiffre tiré au dessu cela prend soit la porte1 soit la porte2
                // c'est un if sur une ligne
                Program.Enemies.Add(new Enemy(Rand == 1 ? Program.porteMonstre1 : Program.porteMonstre2, RandMonstre));
            }


        }
    }
}
