using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Plants
{
    public class GrowthCycle : MonoBehaviour
    {
        public enum Collectable
        {
            Planted,
            Sapling,
            Adult
        }
        public Collectable type;

        [SerializeField] string name;
        [SerializeField] List<GameObject> GrowthCyclePrefabs = new List<GameObject>();

        [Tooltip("How much time this object must spend growing before moving on to the next stage. If 0, this object is at its final form.")]
        [SerializeField] float nextStageLimit;
        [Tooltip("How much time has this object spent growing.")]
        [SerializeField] float growthTime;

        public void SaveGrowthCycleTimeAndStage()
        {
            //Save this somewhere
        }

        public void SetCollectableType(Collectable newType)
        {
            //Do this if you need to make newType an int
            //type = (Collectable)Enum.ToObject(typeof(Collectable), newType);
            type = newType;
        }

        private void Start()
        {
            //TODO: Get saved growthTime

            if(type == Collectable.Adult)
            {
                print($"{transform.name} is an adult, turning off GrowthCycle");
                gameObject.GetComponent<GrowthCycle>().enabled = false;
            } 
            else
            {
                //Set growth stage here
                //Set growthTime here
            }
        }

        private void Update()
        {
            growthTime += Time.deltaTime;
            Growth();
        }

        private void Growth()
        {
            if (nextStageLimit > 0 && nextStageLimit <= growthTime)
            {
                switch ((int)type)
                {
                    case (int)Collectable.Planted:
                        print($"{transform.name} is growing into a Sapling");
                        InstantiateNextObjectInGrowthCycle(GrowthCyclePrefabs[1], Collectable.Sapling);
                        break;
                    case (int)Collectable.Sapling:
                        print($"{transform.name} is growing into an Adult");
                        InstantiateNextObjectInGrowthCycle(GrowthCyclePrefabs[2], Collectable.Adult);
                        break;
                    default:
                        break;
                }
            }
        }

        private void InstantiateNextObjectInGrowthCycle(GameObject prefab, Collectable newCollectableType)
        {
            var go = Instantiate(prefab);
            go.transform.SetParent(transform);
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;
            go.transform.localScale = transform.localScale;
            go.GetComponent<GrowthCycle>().SetCollectableType(newCollectableType);

            System.Random rand = new System.Random(Guid.NewGuid().GetHashCode());
            int guidNumber = rand.Next(0, 2147483647);
            string growthCycleName = type.ToString();
            go.transform.name = $"{name}_{growthCycleName}_{guidNumber}";

            go.transform.SetParent(GameObject.Find("Crops").transform);
            //TODOHave the save manager delete this object from the saved objects list and replace it with go
            Destroy(gameObject);
        }

        private void OnApplicationQuit()
        {
            SaveGrowthCycleTimeAndStage();
        }
    }
}