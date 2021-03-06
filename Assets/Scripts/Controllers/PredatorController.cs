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
        private int initialSpawnChance = 10;

        [SerializeField] private Transform spawnTransform;
        [SerializeField] private GameObject predatorObjectToSpawn;
        [SerializeField] private float timeBeforePredatorsStartSpawning = 60f;
        private float _originalPredatorSpawnTime;
        private bool _bCanSpawn = false;
        private float currentSpawnChance;
        [SerializeField] private int increaseSpawnChanceOnFail = 5;

        private Random _randomValue;
        private float _secondCounter;
        private bool onEndScreen;

        private void Start()
        {
            _randomValue = new Random();
            _originalPredatorSpawnTime = timeBeforePredatorsStartSpawning;
            currentSpawnChance = initialSpawnChance;
        }

        private void SpawnTiger()
        {
            Instantiate(predatorObjectToSpawn, spawnTransform);
            _timeSinceLastSpawn = 0;
        }

        private void Update()
        {
            if (onEndScreen)
            {
                return;
            }
            // We want to have a chance to spawn a tiger from behind the Dodo calculated every frame but 
            // only if enough time has passed
            if (!_bCanSpawn)
            {
                timeBeforePredatorsStartSpawning -= Time.deltaTime;
                if (timeBeforePredatorsStartSpawning <= 0)
                {
                    Debug.Log("Tigers can now spawn");
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
                    if (_randomValue.Next(101) < currentSpawnChance)
                    {
                        SpawnTiger();
                        currentSpawnChance = initialSpawnChance;
                    }
                    else
                    {
                        currentSpawnChance += increaseSpawnChanceOnFail;
                    }
                }
            }
        }

        public void onGameReset()
        {
            _timeSinceLastSpawn = 0f;
            timeBeforePredatorsStartSpawning = _originalPredatorSpawnTime;
            currentSpawnChance = initialSpawnChance;
            onEndScreen = false;
        }
        
        public void onGameEnd()
        {
            _bCanSpawn = false;
            onEndScreen = true;
        }
    }
}