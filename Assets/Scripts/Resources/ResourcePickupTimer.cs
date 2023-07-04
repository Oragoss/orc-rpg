using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    public class ResourcePickupTimer : MonoBehaviour
    {
        [HideInInspector]
        public bool canBePickedUp;

        [Tooltip("How much time has this object has spent in the world.")]
        [SerializeField] float timeInTheWorld;

        [Tooltip("How much time has this object has in the world before it's deleted.")]
        [SerializeField] float expirationTIme = 600f;

        [Tooltip("How much time needed before the player is allowed to pick up this object.")]
        [SerializeField] float pickupDelayTime = 0.25f;

        void Update()
        {
            timeInTheWorld += Time.deltaTime;

            if(timeInTheWorld >= expirationTIme)
            {
                Destroy(gameObject);
            }
            else if(timeInTheWorld >= pickupDelayTime)
            {
                canBePickedUp = true;
            }

        }
    }
}