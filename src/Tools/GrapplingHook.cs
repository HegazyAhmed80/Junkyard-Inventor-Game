using System;
using JunkyardInventor.Player;

namespace JunkyardInventor.Tools
{
    public class GrapplingHook
    {
        public float CableLength { get; set; } = 15.0f;
        public float ReelSpeed { get; set; } = 8.5f;

        public void AnchorAndPull(PlayerController player, object target)
        {
            Console.WriteLine("[GrapplingHook] Launching copper-reinforced steel cable hook...");
            
            // Calculate mock tension
            Random rng = new Random();
            float fakeTension = (float)rng.NextDouble() * 500f + 100f;
            
            Console.WriteLine($"[GrapplingHook] Connected! Anchor target: {target.GetType().Name}. Cable Tension: {fakeTension:F2} N.");
            Console.WriteLine("[GrapplingHook] Solving Verlets integration constraints: Position correction applied. Dampening swing amplitude.");
            
            // Update player vertical velocity mockingly
            player.VelocityY = ReelSpeed;
            player.PositionY += 3.0f; 
            
            Console.WriteLine($"[GrapplingHook] Reel-in initiated. Player pulled upward to Y = {player.PositionY:F2}.");
        }
    }
}
