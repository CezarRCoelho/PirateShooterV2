using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{

    public class EnemySpawner : MonoBehaviour
    {
        public enum SpawnAreaType
        {
            Oval,
            Rectangle
        }
        [SerializeField] private Transform _spawnPlace = null;
        [SerializeField] private List<EnemyShip> _enemyPrefabs = null;
        [SerializeField] private SpawnAreaType _spawnAreaType;
        [SerializeField] private float _areaRadius = 0;
        private void OnEnable()
        {
            InvokeRepeating(nameof(SpawnEnemy), 0, GameController.Instance.GameSetting.SpawnTime);
        }
        private void OnDisable()
        {
            CancelInvoke();
        }
        private void SpawnEnemy()
        {
            float angle = Random.value * Mathf.PI * 2;
            Vector3 spawnDirection = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle) * 0.7f);

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, spawnDirection, _areaRadius);
            int spawnAttempts = 0;
            while (raycastHit2D.collider != null)
            {
                if (spawnAttempts >= 10) return;

                angle = Random.value * Mathf.PI * 2;
                spawnDirection = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle) * 0.7f);
                raycastHit2D = Physics2D.Raycast(transform.position, spawnDirection, _areaRadius);
                spawnAttempts++;
            }

            Vector3 spawnPosition = _spawnPlace.position + spawnDirection * _areaRadius;
            EnemyShip newEnemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)]);
            newEnemy.transform.position = spawnPosition;
        }
    }
}
