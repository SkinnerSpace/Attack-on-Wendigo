using UnityEngine;
using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    public class Surface_detector_tests
    {
        [Test]
        public void Surface_probe_was_taken()
        {
            ICharacterData data = new MockCharacterData();
            MockSurfaceProbeTaker probeTaker = new MockSurfaceProbeTaker();
            probeTaker.SetProbe(new SurfaceProbe() { position = Vector3.one });

            /*SurfaceDetector surfaceDetector = new SurfaceDetector(data, probeTaker);
            SurfaceProbe probe = surfaceDetector.ProbeTheSurface();

            Assert.AreEqual(Vector3.one, probe.position);*/
        }

        [Test]
        public void Surface_probe_was_not_taken()
        {
            ICharacterData data = new MockCharacterData();
            MockSurfaceProbeTaker probeTaker = new MockSurfaceProbeTaker();

            /*SurfaceDetector surfaceDetector = new SurfaceDetector(data, probeTaker);
            SurfaceProbe probe = surfaceDetector.ProbeTheSurface();

            Assert.AreEqual(false, probe.isValid);*/
        }

        [Test]
        public void Surface_is_found_and_observers_are_notified()
        {
            ICharacterData data = new MockCharacterData();
            MockSurfaceProbeTaker probeTaker = new MockSurfaceProbeTaker();
            SurfaceProbe probe = new SurfaceProbe() { isValid = true };
            probeTaker.SetProbe(probe);

            /*SurfaceDetector surfaceDetector = new SurfaceDetector(data, probeTaker);

            ISurfaceObserver observer = Substitute.For<ISurfaceObserver>();
            surfaceDetector.Subscribe(observer);

            surfaceDetector.Update();

            observer.Received().OnSurfaceFound(probe);*/
        }

        [Test]
        public void Surface_is_not_found_and_observers_are_not_notified()
        {
            ICharacterData data = new MockCharacterData();
            MockSurfaceProbeTaker probeTaker = new MockSurfaceProbeTaker();
            SurfaceProbe probe = new SurfaceProbe() { isValid = false };
            probeTaker.SetProbe(probe);

            /*SurfaceDetector surfaceDetector = new SurfaceDetector(data, probeTaker);

            ISurfaceObserver observer = Substitute.For<ISurfaceObserver>();
            surfaceDetector.Subscribe(observer);

            surfaceDetector.Update();

            observer.DidNotReceive().OnSurfaceFound(probe);*/
        }
    }
}
