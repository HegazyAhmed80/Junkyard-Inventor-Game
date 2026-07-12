using System;
using System.Collections.Generic;

namespace JunkyardInventor.Player
{
    public class Inventory
    {
        // Dict containing raw scrap parts and their counts
        public Dictionary<string, int> Parts { get; private set; } = new Dictionary<string, int>();

        // Equipped slots (Max 3 slots shown in screenshot: Magnetic Glove, Grapple, Spring Boots)
        public string?[] EquippedItems { get; private set; } = new string?[3];

        public Inventory()
        {
            // Seed some baseline garbage pieces
            Parts["Gears"] = 8;
            Parts["Springs"] = 2;
            Parts["Batteries"] = 5;
            Parts["Magnet Coils"] = 0;
            Parts["Motors"] = 0;
            Parts["Cables"] = 0;
            Parts["Circuits"] = 0;
        }

        public void AddScrap(string name, int amount)
        {
            if (Parts.ContainsKey(name))
                Parts[name] += amount;
            else
                Parts[name] = amount;

            Console.WriteLine($"[Inventory] Collected: +{amount}x {name} (Total: {Parts[name]})");
        }

        public bool RemoveScrap(string name, int amount)
        {
            if (!Parts.ContainsKey(name) || Parts[name] < amount)
            {
                return false;
            }
            Parts[name] -= amount;
            return true;
        }

        public bool HasIngredients(Dictionary<string, int> recipe)
        {
            foreach (var kvp in recipe)
            {
                string part = kvp.Key;
                int requiredAmount = kvp.Value;
                int currentAmount = Parts.ContainsKey(part) ? Parts[part] : 0;
                
                if (currentAmount < requiredAmount)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ConsumeIngredients(Dictionary<string, int> recipe)
        {
            if (!HasIngredients(recipe)) return false;

            foreach (var kvp in recipe)
            {
                Parts[kvp.Key] -= kvp.Value;
            }
            return true;
        }

        public bool EquipGadget(string gadget)
        {
            // Check if already equipped
            for (int i = 0; i < EquippedItems.Length; i++)
            {
                if (EquippedItems[i] == gadget) return true;
            }

            // Find empty slot
            for (int i = 0; i < EquippedItems.Length; i++)
            {
                if (EquippedItems[i] == null)
                {
                    EquippedItems[i] = gadget;
                    Console.WriteLine($"[Inventory] Equipped {gadget} in Slot {i + 1}!");
                    return true;
                }
            }

            // If full, override the first slot
            Console.WriteLine($"[Inventory] All slots full. Replacing Slot 1 with {gadget}.");
            EquippedItems[0] = gadget;
            return true;
        }

        public bool HasEquipped(string gadget)
        {
            foreach (var item in EquippedItems)
            {
                if (item == gadget) return true;
            }
            return false;
        }

        public void PrintInventory()
        {
            Console.WriteLine("\n--- INVENTORY BACKPACK ---");
            foreach (var kvp in Parts)
            {
                if (kvp.Value > 0)
                {
                    Console.WriteLine($" * {kvp.Key}: {kvp.Value}");
                }
            }
            Console.WriteLine("--------------------------");
            Console.WriteLine($"EQUIPPED: [Slot 1: {EquippedItems[0] ?? "Empty"}] [Slot 2: {EquippedItems[1] ?? "Empty"}] [Slot 3: {EquippedItems[2] ?? "Empty"}]\n");
        }
    }
}
