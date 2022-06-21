using UnityEditor;
using UnityEngine;

namespace Sequencing.Points
{
    public class StartPoint : Point
    {
        private void OnDrawGizmos()
        {
            var cameraPoint = GetCameraPoint();

            if (GetCameraPoint() != null)
            {
                Handles.color = Color.black;
                Handles.Label(cameraPoint.position + Vector3.up * 0.5f, "Start Point");

                Gizmos.color = Color.white;
                Gizmos.DrawSphere(cameraPoint.position, 0.25f);
            }
        }
    }
}