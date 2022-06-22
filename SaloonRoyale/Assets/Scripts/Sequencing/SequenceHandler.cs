using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Point = Sequencing.Points.Point;

namespace Sequencing
{
    public class SequenceHandler : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private AnimationCurve pointToPointCurve;
        [SerializeField] private List<Point> points;
        
        private Point _currentPoint;
        private int _pointIndex = -1;
        
        public Action<Point> OnArrivedPoint;
        
        public void GoToNextPointSequence()
        {
            _pointIndex = Mathf.Clamp(++_pointIndex, 0, points.Count - 1);
            StartCoroutine(GoToPointSequenceCoroutine(points[_pointIndex]));
        }

        public Point GetCurrentPoint()
        {
            return _currentPoint;
        }
        
        private IEnumerator GoToPointSequenceCoroutine(Point sequencePoint)
        {
            var timeToPoint = sequencePoint.GetTimeToPoint();
            var timer = 0f;

            while (timer < timeToPoint)
            {
                timer += Time.deltaTime;
                var t = timeToPoint / timer;
                
                camera.transform.position = Vector3.Lerp(_currentPoint.transform.position, sequencePoint.transform.position, pointToPointCurve.Evaluate(t));
                camera.transform.rotation = Quaternion.Lerp(_currentPoint.transform.rotation, sequencePoint.transform.rotation, pointToPointCurve.Evaluate(t));

                yield return null;
            }

            camera.transform.position = sequencePoint.GetCameraPoint().position;
            camera.transform.rotation = sequencePoint.GetCameraPoint().rotation;

            _currentPoint = sequencePoint;
            OnArrivedPoint?.Invoke(_currentPoint);
        }

        private void OnValidate()
        {
            // Autofetch
            points.Clear();
            var fetchedPoints = GetComponentsInChildren<Point>();
            points = new List<Point>(fetchedPoints);
        }

        private void OnDrawGizmos()
        {
            Point previusPoint = null;
            
            foreach (var point in points)
            {
                if (previusPoint == null)
                {
                    previusPoint = point;
                    continue;
                }
                
                var originPosition = previusPoint.GetCameraPoint();
                var targetPosition = point.GetCameraPoint();
                
                // Draw the lines between points
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(originPosition.position, targetPosition.position);

                previusPoint = point;
            }
        }
    }
}
