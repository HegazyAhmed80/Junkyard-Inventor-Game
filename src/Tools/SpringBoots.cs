using System;
using JunkyardInventor.Player;

namespace JunkyardInventor.Tools
{
    public class SpringBoots
    {
        public float SpringConstantK { get; set; } = 350.0f; // Hooke's Law constant
        public float CompressionHeight { get; set; } = 0.5f;

        public void PerformHighJump(PlayerController player)
        {
            Console.WriteLine("[SpringBoots] Locking leg gears... Compressing dual-hydraulic steel springs...");
            
            // Hooke's Law: F = -k * x
            float potentialEnergy = 0.5f * SpringConstantK * CompressionHeight * CompressionHeight;
            float launchVelocity = (float)Math.Sqrt(2 * potentialEnergy / 70f); // assuming player mass of 70kg
            
            player.VelocityY = launchVelocity;
            player.PositionY += 6f; // Instant vertical leap jump simulation
            
            Console.WriteLine($"[SpringBoots] Springs RELEASED! Generated potential energy: {potentialEnergy:F2} Joules. Jump velocity boosted by {launchVelocity:F2} m/s.");
            Console.WriteLine($"[SpringBoots] Player soared to Y = {player.PositionY:F2}!");
        }
    }
}
