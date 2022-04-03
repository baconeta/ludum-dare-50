using System;
using UnityEngine;
using Random = System.Random;

namespace Controllers
{
    public class PredatorController : MonoBehaviour
    {
        [SerializeField] private float minimumTimeBetweenSpawns = 15f;
        private float _timeSinceLastSpawn = 0f;

        [SerializeField] [Range(0, 100)] [Tooltip("% chance to spawn a predator per second it is valid to do so")]
        private int spawnChance = 100;

        [SerializeField] private Transform spawnTransform;
        [SerializeField] private GameObject predatorObjectToSpawn;
        [SerializeField] private float timeBeforePredatorsStartSpawning = 60f;
        private bool _bCanSpawn = false;

        private Random _randomValue;
        private float _secondCounter;

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
            if (!_bCanSpawn)
            {
                timeBeforePredatorsStartSpawning -= Time.deltaTime;
                if (timeBeforePredatorsStartSpawning <= 0)
                {
                    _bCanSpawn = true;
                }

                return;
            }

            _timeSinceLastSpawn += Time.deltaTime;

            if (_timeSinceLastSpawn > minimumTimeBetweenSpawns)
            {
                _secondCounter += Time.deltaTime;
                if (_secondCounter >= 1)
                {
                    _secondCounter = 0;
                    if (_randomValue.Next(101) < spawnChance)
                    {
                        SpawnTiger();
                    }
                }
            }
        }
    }
}