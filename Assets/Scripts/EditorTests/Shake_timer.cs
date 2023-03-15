using NUnit.Framework;

namespace Tests
{
    namespace ScreenShakes
    {
        public class Shake_timer
        {
            [Test]
            public void Completeness_is_set_to_one_on_time_out()
            {
                GlobalTime.SetDeltaTime(1f);
                ShakeTimer shakeTimer = new ShakeTimer(1f);
                shakeTimer.CountDown();

                Assert.AreEqual(1f, shakeTimer.GetCompleteness());
            }
        }
    }
}
