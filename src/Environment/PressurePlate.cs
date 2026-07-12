using System;

namespace JunkyardInventor.Environment
{
    public class PressurePlate
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public bool IsPressed { get; private set; } = false;

        public PressurePlate(float positionX, float positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public void TriggerPress()
        {
            IsPressed = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[PressurePlate] Click! The plate has been fully pressed down by the heavy metallic crate.");
            Console.WriteLine("[Environment] Elevator Gate opens! Path to exit door is clear.");
            Console.ResetColor();
        }
    }
}
