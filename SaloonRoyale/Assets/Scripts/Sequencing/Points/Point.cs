using UnityEngine;

namespace Sequencing.Points
{
    public abstract class Point : MonoBehaviour
    {
        [SerializeField] private Transform cameraPoint;

        public Transform GetCameraPoint()
        {
            return cameraPoint;
        }
    }
}
