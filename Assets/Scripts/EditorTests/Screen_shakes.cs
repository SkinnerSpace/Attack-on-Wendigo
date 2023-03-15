using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    namespace ScreenShakes
    {
        public class Screen_shakes
        {
            [Test]
            public void Screen_shake_is_created()
            {
                ScreenShake screenShake = ScreenShake.Create();
                Assert.That(screenShake != null);
            }

            [Test]
            public void Shake_is_handled()
            {
                Shake shake = new Shake();
                shake.SetTimer(new ShakeTimer(1f));
                ShakeHandler.Handle(shake);

                Assert.That(shake.Completeness > 0f);
            }

            [Test]
            public void Shake_is_over_on_time_out()
            {
                Shake shake = new Shake();

                GlobalTime.SetDeltaTime(1f);
                shake.SetTimer(new ShakeTimer(1f));
                ShakeHandler.Handle(shake);

                Assert.IsFalse(shake.isActive);
            }

            [Test]
            public void Raw_wave_is_calculated_correctly()
            {
                float rawWave = ShakeHandler.GetRawWave(1f, 8f);
                Assert.AreEqual(1.39876443E-06f, rawWave);
            }

            [Test]
            public void Wave_is_updated()
            {
                GlobalTime.SetDeltaTime(1f);
                ShakeCurve curve = new ShakeCurve(4f, 0.25f, 0.2f);
                ShakeTimer timer = new ShakeTimer(2f);

                Shake shake = new Shake();
                shake.SetCurve(curve);
                shake.SetTimer(timer);

                ShakeHandler.Handle(shake);

                Assert.AreNotEqual(shake.exWave, shake.wave);
            }

            [Test]
            public void Wave_has_passed()
            {
                GlobalTime.SetDeltaTime(1f);
                ShakeCurve curve = new ShakeCurve(4f, 0.25f, 0.2f);
                ShakeTimer timer = new ShakeTimer(2f);

                Shake shake = new Shake();
                shake.SetCurve(curve);
                shake.SetTimer(timer);

                ShakeHandler.Handle(shake);

                Assert.IsTrue(shake.WaveHasPassed());
            }
        }
    }
}
