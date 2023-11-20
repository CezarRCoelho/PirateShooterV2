using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public interface IDamageable
    {
        public void TakeDamage(float amount) { }
    }
}
