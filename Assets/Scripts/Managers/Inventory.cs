using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class Inventory
    {
        public enum Collectable
        {
            Wood,
            Stone,
            Eggs,
            Crops
        }

        public string Name { get; set; }
        public int Amount { get; set; }
        public Collectable Type { get; set; }
        public Sprite Sprite { get; set; }
    }
}