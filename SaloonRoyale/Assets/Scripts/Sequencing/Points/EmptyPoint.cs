using UnityEditor;
using UnityEngine;

namespace Sequencing.Points
{
    public class EmptyPoint : Point
    {
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var cameraPoint = GetCameraPoint();

            if (GetCameraPoint() != null)
            {
                Handles.color = Color.black;
                Handles.Label(cameraPoint.position + Vector3.up * 0.5f, "Empty Point");
                
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(cameraPoint.position, 0.2f);
            }
        }
        #endif
    }
}