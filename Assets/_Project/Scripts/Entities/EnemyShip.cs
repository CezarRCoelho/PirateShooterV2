using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class EnemyShip : Ship
    {
        [SerializeField] private int enemyScoreValue = 1;

        private bool _scoreGiven = false;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "CannonBall")
            {
                CannonBall ball = collision.gameObject.GetComponent<CannonBall>();
                TakeDamage(ball.BallDamage);
                if (!Alive && !_scoreGiven)
                {
                    GameController.Instance.AddScore(enemyScoreValue);
                    _scoreGiven = true;
                }
                ball.Deactivate(CannonBall.DeactivationReason.ShipHit);
            }
        }
    }
}
