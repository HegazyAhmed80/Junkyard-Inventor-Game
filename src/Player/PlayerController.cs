using System;
using JunkyardInventor.Tools;

namespace JunkyardInventor.Player
{
    public class PlayerController
    {
        public float PositionX { get; set; } = 1.0f;
        public float PositionY { get; set; } = 1.0f;
        public float VelocityX { get; set; } = 0.0f;
        public float VelocityY { get; set; } = 0.0f;
        
        // HUD stats from panels 1 & 3
        public int Health { get; set; } = 100;
        public int Energy { get; set; } = 80;

        public Inventory Inventory { get; private set; }

        public PlayerController()
        {
            Inventory = new Inventory();
        }

        public void Move(float deltaX, float deltaY)
        {
            PositionX += deltaX;
            PositionY += deltaY;
            Console.WriteLine($"[PlayerController] Moved to position: ({PositionX:F2}, {PositionY:F2}). State: Walking.");
        }

        public void UseEquippedTool(string toolName, object target)
        {
            if (!Inventory.HasEquipped(toolName))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[PlayerController] Cannot use '{toolName}'! It is not equipped in your slots!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"[PlayerController] Activating gadget: {toolName} on target: {target.GetType().Name}...");

            switch (toolName)
            {
                case "Magnetic Glove":
                    var glove = new MagneticGlove();
                    glove.ApplyMagneticForce(this, target);
                    break;
                case "Grapple Hook":
                    var grapple = new GrapplingHook();
                    grapple.AnchorAndPull(this, target);
                    break;
                case "Spring Boots":
                    var boots = new SpringBoots();
                    boots.PerformHighJump(this);
                    break;
                default:
                    Console.WriteLine($"[PlayerController] Gadget '{toolName}' active but has no special physics callback defined.");
                    break;
            }
        }
    }
}
