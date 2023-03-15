using WendigoCharacter;
using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    namespace Wendigo
    {
        public class Animation_event_controller
        {
            [Test]
            public void Animation_event_controller_exist()
            {
                AnimationEventController eventController = new AnimationEventController();
                Assert.That(eventController != null);
            }

            [Test]
            public void Shake_the_earth()
            {
                IShakeManager shakeManager = Substitute.For<IShakeManager>();
                AnimationEventController eventController = new AnimationEventController();
                eventController.ShakeTheEarth();

                //shakeManager.Received().AddAndLaunch();
            }
        }
    }
}
