using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class PlayerShip : Ship
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            GameController.Instance.PlayerShip = this;
            SoundService.Instance.PlaySound(SoundService.SoundType.ShipSail, true);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            SoundService.Instance.StopSound(SoundService.SoundType.ShipSail);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "EnemyCannonBall")
            {
                CannonBall ball = collision.gameObject.GetComponent<CannonBall>();
                TakeDamage(ball.BallDamage);
                ball.Deactivate(CannonBall.DeactivationReason.ShipHit);
            }
        }
    }
}
