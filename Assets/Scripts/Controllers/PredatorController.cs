using System;
using UnityEngine;
using Random = System.Random;

namespace Controllers
{
    public class PredatorController : MonoBehaviour
    {
        [SerializeField] private float minimumTimeBetweenSpawns = 15f;
        private float _timeSinceLastSpawn = 0f;
        [SerializeField] [Range(0, 100)] private int spawnChance = 100;
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private GameObject predatorObjectToSpawn;

        private Random _randomValue;

        private void Start()
        {
            _randomValue = new Random();
        }

        private void SpawnTiger()
        {
            Instantiate(predatorObjectToSpawn, spawnTransform);
            _timeSinceLastSpawn = 0;
        }

        private void Update()
        {
            // We want to have a chance to spawn a tiger from behind the Dodo calculated every frame but 
            // only if enough time has passed
            _timeSinceLastSpawn += Time.deltaTime;

            if (_timeSinceLastSpawn > minimumTimeBetweenSpawns)
            {
                if (_randomValue.Next(101) > spawnChance)
                {
                    SpawnTiger();
                }
            }
        }
    }
}