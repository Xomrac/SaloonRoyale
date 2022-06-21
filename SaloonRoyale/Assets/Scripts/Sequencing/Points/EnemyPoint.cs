using UnityEditor;
using UnityEngine;

namespace Sequencing.Points
{
    public class EnemyPoint: Point
    {
        [SerializeField] private Character enemy;

        public Character GetEnemy()
        {
            return enemy;
        }
        
        private void OnDrawGizmos()
        {
            var cameraPoint = GetCameraPoint();

            if (GetCameraPoint() != null)
            {
                Handles.color = Color.white;
                Handles.Label(cameraPoint.position + Vector3.up * 0.5f, "Enemy Point");
                
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(cameraPoint.position, Vector3.one * 0.2f);
            }

            if (enemy != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(cameraPoint.position, enemy.transform.position + Vector3.up * 1.5f);
                
                Handles.color = Color.white;
                Handles.Label(enemy.transform.position + Vector3.up * 1.5f, "Enemy");
            }
        }
    }
}