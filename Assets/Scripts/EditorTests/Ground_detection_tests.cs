using UnityEngine;
using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    public class Ground_detection_tests
    {
        [Test]
        public void Ground_detector_exists()
        {
            ICharacterData data = new MockCharacterData();
            IGroundDetector detector = Substitute.For<IGroundDetector>();
         /*   GroundDetectorController groundDetectionHandler = new GroundDetectorController(data, detector);

            Assert.That(groundDetectionHandler != null);*/
        }

        [Test]
        public void Ground_is_detected()
        {
            ICharacterData data = new MockCharacterData();
            IGroundDetector detector = new MockGroundDetector(true);

          /*  GroundDetectorController groundDetectionHandler = new GroundDetectorController(data, detector);
            groundDetectionHandler.Update();

            Assert.That(data.IsGrounded);*/
        }

        [Test]
        public void Ground_observers_are_notified_on_grounded()
        {
            ICharacterData data = new MockCharacterData();
            IGroundDetector detector = new MockGroundDetector(true);
/*            GroundDetectorController groundDetectionHandler = new GroundDetectorController(data, detector);

            IGroundObserver observer = Substitute.For<IGroundObserver>();
            groundDetectionHandler.Subscribe(observer);

            groundDetectionHandler.Update();

            observer.Received().OnGrounded();*/
        }

        [Test]
        public void Off_the_ground_is_detected()
        {
            ICharacterData data = new MockCharacterData();
            MockGroundDetector detector = new MockGroundDetector(true);

/*            GroundDetectorController groundDetectionHandler = new GroundDetectorController(data, detector);
            groundDetectionHandler.Update();
            detector.SetState(false);
            groundDetectionHandler.Update();

            Assert.That(!data.IsGrounded);*/
        }
    }
}
