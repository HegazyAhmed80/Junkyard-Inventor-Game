using System;
using JunkyardInventor.Core;
using JunkyardInventor.Crafting;

namespace JunkyardInventor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("====================================================");
            Console.WriteLine("        JUNKYARD INVENTOR - SIMULATION ENGINE       ");
            Console.WriteLine("====================================================");
            Console.ResetColor();

            if (args.Length > 0)
            {
                if (args[0] == "--craft" && args.Length > 1)
                {
                    SimulateCrafting(args[1]);
                    return;
                }
                else if (args[0] == "--boss")
                {
                    SimulateBossBattle();
                    return;
                }
            }

            // Default level simulation
            SimulateLevelExploration();
        }

        static void SimulateLevelExploration()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n[SCENARIO 1: SCRAPYARD EXPLORATION & CRAFTING]");
            Console.ResetColor();
            
            GameLoop loop = new GameLoop();
            loop.StartLevel("Underground Laboratory (Rainy Industrial)");
            
            // Run a couple of update ticks to simulate scavenging and solving
            for (int i = 0; i < 5; i++)
            {
                loop.Update(0.016f); // 60 FPS tick
            }
            
            loop.Stop();
        }

        static void SimulateCrafting(string item)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n[SCENARIO 2: WORKSHOP CRAFTING BENCH]");
            Console.ResetColor();

            var workshop = new Workshop();
            workshop.DisplayRecipes();
            workshop.TryCraftItem(item);
        }

        static void SimulateBossBattle()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[SCENARIO 3: BOSS ENCOUNTER - EXCAVATOR MK-7]");
            Console.ResetColor();

            GameLoop loop = new GameLoop();
            loop.StartBossBattle();
            
            for (int i = 0; i < 4; i++)
            {
                loop.Update(0.016f);
            }
            
            loop.Stop();
        }
    }
}
