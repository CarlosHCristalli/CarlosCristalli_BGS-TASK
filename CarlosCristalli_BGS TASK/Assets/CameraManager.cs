using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_TEST
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float OffsetZ = 10;

        private void LateUpdate()
        {
            Vector3 targetPosition = target.position;
            targetPosition.z = OffsetZ;

            transform.position = targetPosition;
        }
    }
}