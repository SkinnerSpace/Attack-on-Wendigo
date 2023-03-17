using System;
using UnityEngine;
using NUnit.Framework;

namespace Tests
{
    namespace Shake_tests
    {
        public class Shake_wave
        {
            [Test]
            public void Raw_wave_is_calculated_correctly()
            {
                ShakeCurve curve = new ShakeCurve(4f, 0.25f, 0.25f);
                ShakeWave wave = new ShakeWave(curve);
                float raw = wave.GetRaw(0.2f);

                Assert.AreEqual(-0.95105648f, raw);
            }

            [Test]
            public void Attuned_wave_on_attack_is_correct()
            {
                ShakeCurve curve = new ShakeCurve(4f, 0.25f, 0.25f);
                ShakeWave wave = new ShakeWave(curve);
                wave.Update(0.1f);

                Assert.AreEqual(0.371748f, wave.Value);
            }

            [Test]
            public void Attuned_wave_in_the_middle_is_correct()
            {
                ShakeCurve curve = new ShakeCurve(4f, 0.25f, 0.25f);
                ShakeWave wave = new ShakeWave(curve);
                wave.Update(0.5f);

                Assert.AreEqual(3.49691106E-07f, wave.Value);
            }

            [Test]
            public void Attuned_wave_on_release_is_correct()
            {
                ShakeCurve curve = new ShakeCurve(4f, 0.25f, 0.25f);
                ShakeWave wave = new ShakeWave(curve);
                wave.Update(0.9f);

                Assert.AreEqual(-0.0104495166f, wave.Value);
            }

            [Test]
            public void Wave_has_passed()
            {
                GlobalTime.SetDeltaTime(0.1f);

                ShakeCurve curve = new ShakeCurve(16f, 0.25f, 0.25f);
                ShakeWave wave = new ShakeWave(curve);

                ShakeTimer timer = new ShakeTimer(1f);

                Tick(Handle, 2);
                void Handle() { 
                    timer.CountDown();
                    wave.Update(timer.Progress); 
                }

                Assert.IsTrue(wave.HasPassed());
            }

            private void Tick(Action action, int iterations)
            {
                for (int i = 0; i < iterations; i++)
                {
                    action();
                }
            }
        }
    }
}
