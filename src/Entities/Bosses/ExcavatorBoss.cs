using System;

namespace JunkyardInventor.Entities.Bosses
{
    public class ExcavatorBoss
    {
        public string Name { get; set; } = "EXCAVATOR MK-7";
        public int Health { get; set; } = 100;
        public float HydraulicPressure { get; set; } = 2500f; // PSI
        public bool EyeLensExposed { get; set; } = false;

        public void Update(float deltaTime)
        {
            Console.WriteLine($"[Boss: {Name}] Status: Scanning Arena. HP: {Health} | Hydraulic System Pressure: {HydraulicPressure} PSI.");

            if (Health > 75)
            {
                Console.WriteLine($"[Boss: {Name}] Phase 1: Swiping giant excavator shovel arm! Debris falling from ceiling.");
            }
            else if (Health > 40)
            {
                EyeLensExposed = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Boss: {Name}] Phase 2: Core Blue Laser charging! Weak spot (Optical Core Lens) is EXPOSED!");
                Console.ResetColor();
            }
            else
            {
                HydraulicPressure -= 150f;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"[Boss: {Name}] Phase 3: Crawling on damaged hydraulic pistons. Emergency vent pipes venting heat steam!");
                Console.ResetColor();
            }
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Boss: {Name}] Damage taken! -{amount} HP. Remaining: {Health}/100.");
            Console.ResetColor();
        }
    }
}
