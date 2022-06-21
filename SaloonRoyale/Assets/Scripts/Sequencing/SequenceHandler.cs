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
        [SerializeField] private Point currentPoint;
        
        public Action<Point> ArrivedOnPointSequence;

        public void GoToNextPointSequence()
        {
            var pointIndex = 0;
            
            //var distance = Vector3.distance(camera.transform, points[pointIndex]);
            //if (distance<= 0.1f){
            //    pointIndex++;
            //    else {

            StartCoroutine(GoToPointSequenceCoroutine(points[pointIndex]));

        }

        private IEnumerator GoToPointSequenceCoroutine(Point sequencePoint)
        {
            var timeToPoint = 2f;
            camera.transform.position = Vector3.Slerp(currentPoint.transform.position, sequencePoint.transform.position, timeToPoint);
            
            //currentPoint = camera.transform;

            ArrivedOnPointSequence.Invoke(currentPoint);
            
            yield break;
        }


    }
}
