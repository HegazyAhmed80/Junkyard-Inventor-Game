using System;
using JunkyardInventor.Player;
using JunkyardInventor.Entities.Enemies;
using JunkyardInventor.Entities.Bosses;
using JunkyardInventor.Environment;

namespace JunkyardInventor.Core
{
    public class GameLoop
    {
        private PlayerController _player;
        private SecurityDrone? _drone;
        private PressurePlate? _plate;
        private ExcavatorBoss? _boss;
        private string _currentLevel = "";
        private float _timeInLevel = 0f;
        private bool _isRunning = false;
        private bool _isBossFight = false;

        public GameLoop()
        {
            _player = new PlayerController();
        }

        public void StartLevel(string levelName)
        {
            _currentLevel = levelName;
            _isRunning = true;
            _isBossFight = false;
            _timeInLevel = 0f;

            Console.WriteLine($"[GameLoop] Loading level: '{_currentLevel}'...");
            Console.WriteLine("[GameLoop] Spawning environment layers: RustySunsetBackground, RainParticleSystem, JunkyardTiles.");
            
            // Populate initial raw material pickups in the world (as seen in panel 1 of the screenshot)
            Console.WriteLine("[GameLoop] Placing loot containers: Found 1 Chest containing: [Magnet Coil]");
            _player.Inventory.AddScrap("Magnet Coil", 1);
            _player.Inventory.AddScrap("Gears", 3);
            _player.Inventory.AddScrap("Springs", 4);
            _player.Inventory.AddScrap("Batteries", 2);
            _player.Inventory.AddScrap("Cables", 5);
            _player.Inventory.AddScrap("Motors", 1);

            // Print initial state
            _player.Inventory.PrintInventory();

            // Spawn entities (as seen in panel 3 of the screenshot)
            _drone = new SecurityDrone(positionX: 12f, positionY: 5f);
            _plate = new PressurePlate(positionX: 15f, positionY: 1f);
            
            Console.WriteLine($"[GameLoop] Level loaded. Spawned 1 SecurityDrone at ({_drone.PositionX},{_drone.PositionY}) and 1 PressurePlate at ({_plate.PositionX},{_plate.PositionY}).");
        }

        public void StartBossBattle()
        {
            _currentLevel = "Boss Arena: The Rusty Trench";
            _isRunning = true;
            _isBossFight = true;
            _timeInLevel = 0f;

            Console.WriteLine($"[GameLoop] Loading boss arena: '{_currentLevel}'...");
            _boss = new ExcavatorBoss();
            
            // Give player pre-crafted tools for the boss fight (as seen in panel 4)
            _player.Inventory.EquipGadget("Magnetic Glove");
            _player.Inventory.EquipGadget("Grapple Hook");
            _player.Inventory.EquipGadget("Spring Boots");
        }

        public void Update(float deltaTime)
        {
            if (!_isRunning) return;

            _timeInLevel += deltaTime;
            Console.WriteLine($"\n--- Frame Update Tick (Elapsed Level Time: {_timeInLevel:F3}s) ---");

            if (_isBossFight && _boss != null)
            {
                // Boss loop simulation
                _boss.Update(deltaTime);
                
                // Simulate player grappling onto the boss weakpoint
                if (_boss.Health > 50)
                {
                    _player.UseEquippedTool("Grapple Hook", _boss);
                    _boss.TakeDamage(25);
                }
                else if (_boss.Health > 0)
                {
                    _player.UseEquippedTool("Magnetic Glove", _boss);
                    _boss.TakeDamage(30);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[GameLoop] SUCCESS! Excavator MK-7 has been dismantled into reusable screws!");
                    Console.ResetColor();
                    _isRunning = false;
                }
            }
            else
            {
                // Standard Level loop simulation
                // 1. Move player closer to the gate
                _player.Move(2.5f, 0f);

                // 2. Drone patrols and targets player/box
                if (_drone != null)
                {
                    _drone.Update(_player.PositionX, _player.PositionY);
                }

                // 3. Player realizes there is a gap and a metal crate. Uses Magnetic Glove to slide box.
                if (_player.PositionX > 5f && !_player.Inventory.HasEquipped("Magnetic Glove"))
                {
                    Console.WriteLine("[Player] Gap detected! Opening workshop to craft Magnetic Glove...");
                    // Fast-craft glove
                    _player.Inventory.EquipGadget("Magnetic Glove");
                }

                if (_player.Inventory.HasEquipped("Magnetic Glove") && _plate != null && !_plate.IsPressed)
                {
                    _player.UseEquippedTool("Magnetic Glove", _plate);
                }

                // 4. Check level exit condition
                if (_plate != null && _plate.IsPressed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[GameLoop] EXIT UNLOCKED! Player reached the elevator exit door.");
                    Console.ResetColor();
                    _isRunning = false;
                }
            }
        }

        public void Stop()
        {
            _isRunning = false;
            Console.WriteLine("\n[GameLoop] Level simulation stopped. Scrap totals saved to player workspace.");
        }
    }
}
