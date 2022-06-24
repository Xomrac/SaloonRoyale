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
        private int _pointIndex;
        
        public Action<Point> OnArrivedPoint;

        public void GoToNextPointSequence()
        {
            _pointIndex++;
            _pointIndex = Mathf.Clamp(_pointIndex, 0, points.Count - 1);
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

            var startingPosition = camera.transform.position;
            var startingRotation = camera.transform.rotation;

            while (timer < timeToPoint)
            {
                timer += Time.deltaTime;
                var t = timer / timeToPoint;
                
                camera.transform.position = Vector3.Lerp(startingPosition, sequencePoint.GetCameraPoint().position, pointToPointCurve.Evaluate(t));
                camera.transform.rotation = Quaternion.Lerp(startingRotation, sequencePoint.GetCameraPoint().rotation, pointToPointCurve.Evaluate(t));

                yield return null;
            }

            camera.transform.position = sequencePoint.GetCameraPoint().position;
            camera.transform.rotation = sequencePoint.GetCameraPoint().rotation;

            _currentPoint = sequencePoint;
            OnArrivedPoint?.Invoke(_currentPoint);
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
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
        #endif
    }
}
