using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class GameSetting
    {
        public float GameTime
        {
            get => _gameTime;
            private set => _gameTime = value;
        }
        public float SpawnTime
        {
            get => _spawnTime;
            private set => _spawnTime = value;
        }
        [SerializeField] private float _gameTime = 0;
        [SerializeField] private float _spawnTime = 0;

        public GameSetting(float gameTime, float spawnTime)
        {
            SpawnTime = spawnTime;
            GameTime = gameTime;
        }
    }
}