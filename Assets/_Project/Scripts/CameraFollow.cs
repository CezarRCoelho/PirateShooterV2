using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateShooter
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _objectToFollow = null;
        [SerializeField] private float speedToFollow = 0;

        void FixedUpdate()
        {
            Follow();
        }

        void Follow()
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, _objectToFollow.position, speedToFollow * Time.deltaTime);
            newPosition.z = -10;
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 5 + 1 * speedToFollow, 2f * Time.deltaTime);
            transform.position = newPosition;
        }
    }

    
}
