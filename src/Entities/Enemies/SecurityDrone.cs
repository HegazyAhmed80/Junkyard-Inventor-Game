using System;

namespace JunkyardInventor.Entities.Enemies
{
    public class SecurityDrone
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PatrolSpeed { get; set; } = 1.2f;
        public bool AlertActive { get; set; } = false;

        public SecurityDrone(float positionX, float positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public void Update(float playerX, float playerY)
        {
            // Simulate lazy back-and-forth patrol on X axis
            PositionX += (float)Math.Sin(DateTime.Now.Ticks / 10000000.0) * PatrolSpeed * 0.1f;
            
            Console.WriteLine($"[SecurityDrone] Hovering at ({PositionX:F2}, {PositionY:F2}). Scanning sector...");

            // Simulate targeting red laser at player or objects (bottom-left panel)
            float dx = playerX - PositionX;
            float dy = playerY - PositionY;
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);

            if (dist < 8.0f)
            {
                AlertActive = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[SecurityDrone] LASER TARGET ACQUIRED! Projecting warning beam to target at ({playerX:F2}, {playerY:F2}).");
                Console.ResetColor();
            }
            else
            {
                AlertActive = false;
                Console.WriteLine("[SecurityDrone] Sweeping laser array across floor tiles. Status: Idle.");
            }
        }
    }
}
