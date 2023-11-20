using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class CannonBallView : MonoBehaviour
    {
        [SerializeField] private CannonBall _cannonBall = null;

        [SerializeField] private List<ParticleSystem> _groundHitParticles = null;
        [SerializeField] private List<ParticleSystem> _shipHitParticles = null;
        [SerializeField] private List<ParticleSystem> _rockHitParticles = null;
        [SerializeField] private List<ParticleSystem> _ballHitParticles = null;
        [SerializeField] private List<ParticleSystem> _timeoutParticles = null;

        private void OnEnable()
        {
            _cannonBall.OnDeactivate += OnDeactivateHandler;
        }
        private void OnDisable()
        {
            _cannonBall.OnDeactivate -= OnDeactivateHandler;
        }
        private void OnDeactivateHandler(CannonBall.DeactivationReason deactivationReason)
        {
            switch (deactivationReason)
            {
                case CannonBall.DeactivationReason.Timeout:
                    SoundService.Instance.PlaySound(SoundService.SoundType.BallHitWater);
                    if (_timeoutParticles != null)
                        foreach (ParticleSystem particle in _timeoutParticles)
                            SpawnParticle(particle);
                    break;
                case CannonBall.DeactivationReason.BallHit:
                    SoundService.Instance.PlaySound(SoundService.SoundType.BallHitBall);
                    if (_ballHitParticles != null)
                        foreach (ParticleSystem particle in _ballHitParticles)
                            SpawnParticle(particle);
                    break;
                case CannonBall.DeactivationReason.ShipHit:
                    SoundService.Instance.PlaySound(SoundService.SoundType.BallHitShip);
                    if (_shipHitParticles != null)
                    foreach (ParticleSystem particle in _shipHitParticles)
                            SpawnParticle(particle);
                    break;
                case CannonBall.DeactivationReason.GroundHit:
                    SoundService.Instance.PlaySound(SoundService.SoundType.BallHitGround);
                    if (_groundHitParticles != null)
                        foreach (ParticleSystem particle in _groundHitParticles)
                            SpawnParticle(particle);
                    break;
                case CannonBall.DeactivationReason.RockHit:
                    SoundService.Instance.PlaySound(SoundService.SoundType.BallHitRock);
                    if (_rockHitParticles != null)
                        foreach (ParticleSystem particle in _rockHitParticles)
                            SpawnParticle(particle);
                    break;
                default:
                    break;
            }
        }

        private void SpawnParticle(ParticleSystem particle)
        {
            ParticleSystem newParticle = Instantiate(particle);
            newParticle.transform.position = transform.position;
        }
    }
}
