using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Managers
{
    public class InventoryManager
    {
        public Inventory inventory = new Inventory();

        private List<Inventory> playerInventory = new List<Inventory>();
        private Dictionary<string, Inventory> chestInventories = new();

        #region Player Inventory
        public List<Inventory> GetPlayerInventory()
        {
            return playerInventory;
        }

        public void RemoveItemFromPlayerInventory(string name)
        {
            var itemToRemove = playerInventory.SingleOrDefault(r => r.Name == name);
            playerInventory.Remove(itemToRemove);
        }

        public void AddItemToPlayerInventory(Inventory newItem)
        {
            playerInventory.Add(newItem);
        }

        public void SavePlayerInventory()
        {
            //TODO: write to file here
        }
        #endregion

        #region Chest Inventory
        //TODO: Chest inventory
        #endregion
    }
}