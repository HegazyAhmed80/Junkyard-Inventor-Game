using System;
using System.Collections.Generic;
using JunkyardInventor.Player;

namespace JunkyardInventor.Crafting
{
    public class Workshop
    {
        public class Recipe
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public Dictionary<string, int> Requirements { get; set; }

            public Recipe(string name, string description, Dictionary<string, int> reqs)
            {
                Name = name;
                Description = description;
                Requirements = reqs;
            }
        }

        private Dictionary<string, Recipe> _blueprints = new Dictionary<string, Recipe>(StringComparer.OrdinalIgnoreCase);

        public Workshop()
        {
            // Seed recipes matching the screenshot's Workshop blueprints list and quantities
            _blueprints["Spring Boots"] = new Recipe(
                "Spring Boots", 
                "Increases jump height by releasing coiled spring tension.",
                new Dictionary<string, int> { { "Gears", 3 }, { "Springs", 2 } }
            );

            _blueprints["Magnetic Glove"] = new Recipe(
                "Magnetic Glove", 
                "Allows you to grab and move metal objects.",
                new Dictionary<string, int> { { "Gears", 3 }, { "Springs", 2 }, { "Batteries", 1 } }
            );

            _blueprints["Grapple Hook"] = new Recipe(
                "Grapple Hook", 
                "Motorized grappling hook for swinging across ceiling anchor nodes.",
                new Dictionary<string, int> { { "Cables", 4 }, { "Motors", 1 }, { "Gears", 2 } }
            );

            _blueprints["Hover Drone"] = new Recipe(
                "Hover Drone", 
                "A flying drone that carries cables or presses distant switches.",
                new Dictionary<string, int> { { "Motors", 2 }, { "Batteries", 1 }, { "Circuits", 2 } }
            );

            _blueprints["Portable Bridge"] = new Recipe(
                "Portable Bridge", 
                "Assemble a quick metal path to cross wide horizontal chasms.",
                new Dictionary<string, int> { { "Gears", 4 }, { "Cables", 2 } }
            );

            _blueprints["Jetpack"] = new Recipe(
                "Jetpack", 
                "Uses thrusters to hover, consuming battery charge.",
                new Dictionary<string, int> { { "Motors", 3 }, { "Batteries", 3 }, { "Cables", 2 } }
            );

            _blueprints["Teleporter"] = new Recipe(
                "Teleporter", 
                "Instantly swap location with a deployed telemetry beacon.",
                new Dictionary<string, int> { { "Circuits", 4 }, { "Batteries", 2 } }
            );
        }

        public void DisplayRecipes()
        {
            Console.WriteLine("\n=== WORKSHOP BLUEPRINTS LIST ===");
            foreach (var item in _blueprints.Values)
            {
                Console.WriteLine($"- [{item.Name}] : {item.Description}");
                Console.Write("  Requires: ");
                List<string> reqStrings = new List<string>();
                foreach (var req in item.Requirements)
                {
                    reqStrings.Add($"{req.Value}x {req.Key}");
                }
                Console.WriteLine(string.Join(", ", reqStrings));
            }
            Console.WriteLine("================================\n");
        }

        public bool TryCraftItem(string name, Inventory? inv = null)
        {
            // If no inventory is provided, create a mock user inventory with the exact counts from the screenshot
            if (inv == null)
            {
                inv = new Inventory();
                Console.WriteLine("[Workshop] No inventory passed. Creating template player inventory (8 Gears, 2 Springs, 5 Batteries).");
            }

            if (!_blueprints.ContainsKey(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Workshop] Crafting Failed: Unknown blueprint blueprint '{name}'!");
                Console.ResetColor();
                return false;
            }

            Recipe recipe = _blueprints[name];
            Console.WriteLine($"[Workshop] Attempting to craft [{recipe.Name}]...");

            // Print requirement evaluation
            bool hasEverything = true;
            foreach (var req in recipe.Requirements)
            {
                string part = req.Key;
                int needed = req.Value;
                int owned = inv.Parts.ContainsKey(part) ? inv.Parts[part] : 0;
                
                if (owned >= needed)
                {
                    Console.WriteLine($"  -> Required: {needed}x {part} | You have: {owned} (✓)");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"  -> Required: {needed}x {part} | You have: {owned} (❌ INSUFFICIENT PARTS)");
                    Console.ResetColor();
                    hasEverything = false;
                }
            }

            if (!hasEverything)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Workshop] CRAFTING FAILED: Go back to the level and scavenge more parts!");
                Console.ResetColor();
                return false;
            }

            // Consume components and equip gadget
            inv.ConsumeIngredients(recipe.Requirements);
            inv.EquipGadget(recipe.Name);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[Workshop] CRAFTING SUCCESS! [{recipe.Name}] has been crafted and equipped.");
            Console.ResetColor();
            
            inv.PrintInventory();
            return true;
        }
    }
}
