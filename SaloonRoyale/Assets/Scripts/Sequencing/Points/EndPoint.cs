using UnityEditor;
using UnityEngine;

namespace Sequencing.Points
{
    public class EndPoint : Point
    {
        private void OnDrawGizmos()
        {
            var cameraPoint = GetCameraPoint();

            if (GetCameraPoint() != null)
            {
                Handles.color = Color.white;
                Handles.Label(cameraPoint.position + Vector3.up * 0.5f, "End Point");
                
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(cameraPoint.position, 0.25f);
            }
        }
    }
}