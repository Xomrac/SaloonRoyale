using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using OpenCover.Framework.Model;
using UnityEngine;

namespace Sequencer
{
    public class SequenceHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private List<Point> _points;
        [SerializeField] private Point _currentPoint;

        public Action<Point> ArrivedOnPointSequence()
        {
            
        }

        public void GoToNextPointSequence()
        {
            
        }

        private IEnumerator GoToPointSequenceCoroutine(SequencePoint sequencePoint)
        {
            
        }


    }
}
