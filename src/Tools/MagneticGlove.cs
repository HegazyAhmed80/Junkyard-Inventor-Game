using System;
using JunkyardInventor.Player;
using JunkyardInventor.Environment;

namespace JunkyardInventor.Tools
{
    public class MagneticGlove
    {
        public float MagneticFieldStrengthTesla { get; set; } = 4.5f; // Strong coil magnet
        public float CooldownSeconds { get; set; } = 1.0f;

        public void ApplyMagneticForce(PlayerController player, object target)
        {
            Console.WriteLine("[MagneticGlove] Generating high-frequency magnetic polarity field...");
            
            if (target is PressurePlate plate)
            {
                // Calculate fake vector distance
                float dx = plate.PositionX - player.PositionX;
                float dy = plate.PositionY - player.PositionY;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                // Inverse square law simulation (funny style)
                float force = MagneticFieldStrengthTesla / (distance * distance + 0.1f);
                
                Console.WriteLine($"[MagneticGlove] Distance to metal object: {distance:F2} meters. Force Vector F = {force:F3} Newtons.");
                
                if (distance < 12.0f)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("[MagneticGlove] Pulling Metal Crate towards the target PressurePlate...");
                    Console.ResetColor();
                    plate.TriggerPress();
                }
                else
                {
                    Console.WriteLine("[MagneticGlove] Warning: Target is out of magnetic reach! Walk closer.");
                }
            }
            else
            {
                // Generic target interaction (e.g. boss shield)
                Console.WriteLine($"[MagneticGlove] Magnetically targeting a non-metallic raw structure {target.GetType().Name}. Field vibrating harmlessly.");
            }
        }
    }
}
