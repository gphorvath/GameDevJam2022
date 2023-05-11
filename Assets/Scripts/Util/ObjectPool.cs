using System.Collections.Generic;
using UnityEngine;

namespace RPG.Util
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject poolObjectPrefab;
        [SerializeField] private int poolSize = 10;

        private Queue<GameObject> poolQueue = new Queue<GameObject>();

        private void Start()
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject poolObject = Instantiate(poolObjectPrefab, transform);
                poolObject.SetActive(false);
                poolQueue.Enqueue(poolObject);
            }
        }

        public GameObject GetObject()
        {
            if (poolQueue.Count > 0)
            {
                GameObject poolObject = poolQueue.Dequeue();
                poolObject.SetActive(true);
                return poolObject;
            }
            else
            {
                // If pool is empty, create a new object (optional)
                GameObject poolObject = Instantiate(poolObjectPrefab, transform);
                return poolObject;
            }
        }

        public void ReturnObject(GameObject poolObject)
        {
            poolObject.SetActive(false);
            poolQueue.Enqueue(poolObject);
        }
    }
}