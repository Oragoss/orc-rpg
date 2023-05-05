using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class InventoryManager
    {
        public enum ItemType
        {
            Wood,
            Stone
        }

        private Dictionary<string, int> inventory = new Dictionary<string, int>();

        public Dictionary<string, int> GetInventory()
        {
            return inventory;
        }
    }
}