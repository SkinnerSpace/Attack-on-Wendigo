using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    public class Vision_tests
    {
        [Test]
        public void Vision_detects_objects()
        {
            VisionTarget target = new VisionTarget();

            ICharacterData data = new MockCharacterData();
            IVisionDetector detector = Substitute.For<IVisionDetector>();
            detector.GetTarget().Returns(target);

            VisionController vision = new VisionController(data, detector);

            IVisionObserver observer = Substitute.For<IVisionObserver>();
            vision.Subscribe(observer);

            vision.Update();

            observer.Received().OnUpdate(target);
        }
    }
}
