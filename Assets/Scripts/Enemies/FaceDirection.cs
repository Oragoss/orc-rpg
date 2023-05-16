using System.Collections;
using UnityEngine;
using Pathfinding;

namespace Assets.Scripts.Enemies
{
    public class FaceDirection : MonoBehaviour
    {
        [SerializeField] AIPath aiPath;

        private void Reset()
        {
            if (aiPath == null)
                aiPath = GetComponentInParent<AIPath>();
        }

        private void Start()
        {
            if(aiPath == null)
            aiPath = GetComponentInParent<AIPath>();
        }

        void Update()
        {
            if (aiPath.desiredVelocity.x >= 0.01f) //moving to the right
            {
                transform.eulerAngles = new Vector3(0f, 0f, -90f);
            }
            else if (aiPath.desiredVelocity.x <= -0.01f) //moving to the left
            {
                transform.eulerAngles = new Vector3(0f, 0f, 90f);
            }
        }
    }
}