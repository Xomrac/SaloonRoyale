using UnityEngine;

namespace Sequencing.Points
{
    public abstract class Point : MonoBehaviour
    {
        [SerializeField] private Transform cameraPoint;
        [SerializeField, Range(0f, 10.0f)] private float timeToPoint = 1.5f;

        public Transform GetCameraPoint()
        {
            return cameraPoint;
        }

        public float GetTimeToPoint()
        {
            return timeToPoint;
        }
    }
}
