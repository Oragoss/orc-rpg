using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    public class GenerateCollectable : MonoBehaviour
    {
        [SerializeField] int numberOfCollectables;
        [SerializeField] GameObject collectable;

        public void Generate()
        {
            var random = new System.Random(Guid.NewGuid().GetHashCode());
            int randomNumber = random.Next(numberOfCollectables);
            var notZero = randomNumber == 0 ? 1 : randomNumber;
            for (int i = 0; i < notZero; i++)
            {
                int plusOrMinus = notZero % 2 != 0 ? 1 : -1;
                Instantiate(collectable, new Vector3(transform.position.x + plusOrMinus, transform.position.y, transform.position.z), transform.rotation);
            }
        }
    }
}