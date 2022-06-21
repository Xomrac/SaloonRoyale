using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using OpenCover.Framework.Model;
using UnityEngine;
using Point = Sequencing.Points.Point;

namespace Sequencing
{
    public class SequenceHandler : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private List<Point> points;
        private Point currentPoint;
        private int pointIndex = -1;
        [SerializeField] private AnimationCurve timerCurve;
        
        public Action<Point> ArrivedOnPointSequence;

        public void GoToNextPointSequence()
        {
            pointIndex = Mathf.Clamp(++pointIndex, 0, points.Count - 1);
            
            StartCoroutine(GoToPointSequenceCoroutine(points[pointIndex]));
        }

        private IEnumerator GoToPointSequenceCoroutine(Point sequencePoint)
        {
            var timeToPoint = 2f;
            var timer = 0f;
            var t = 0f;

            while (timer < timeToPoint)
            {
                timer += Time.deltaTime;
                t = timeToPoint / timer;
                
                camera.transform.position = Vector3.Lerp(currentPoint.transform.position, sequencePoint.transform.position, timerCurve.Evaluate(t));
                camera.transform.rotation = Quaternion.Lerp(currentPoint.transform.rotation, sequencePoint.transform.rotation, timerCurve.Evaluate(t));

                yield return null;
            }

            camera.transform.position = sequencePoint.GetCameraPoint().position;
            camera.transform.rotation = sequencePoint.GetCameraPoint().rotation;

            currentPoint = sequencePoint;

            ArrivedOnPointSequence?.Invoke(currentPoint);
        }


    }
}
