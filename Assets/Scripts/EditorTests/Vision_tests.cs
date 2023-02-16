using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class Vision_tests
    {
        [Test]
        public void Vision_detects_objects()
        {
            VisionTarget target = new VisionTarget();

            IVisionDetector detector = Substitute.For<IVisionDetector>();
            detector.GetTarget().Returns(target);

            VisionController vision = new VisionController(detector);

            IVisionObserver observer = Substitute.For<IVisionObserver>();
            vision.Subscribe(observer);

            vision.Update();

            observer.Received().OnUpdate(target);
        }
    }
}
