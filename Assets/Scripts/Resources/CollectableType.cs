using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    public class CollectableType : MonoBehaviour
    {
        public enum Collectable
        {
            Wood,
            Stone,
            Option3,
            Option4,
            Option5,
        }
        public Collectable type;
    }
}