using System;
using NUnit.Framework;
using UnityEngine;

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
            public void Shake_is_launched()
            {
                Shake shake = new Shake();
                shake.SetAxis(new Vector3(1f, 1f, 0f));
                shake.Launch();

                Assert.AreNotEqual(Vector3.zero, shake.Dir);
                Assert.AreNotEqual(Vector3.zero, shake.Angle);
                Assert.IsTrue(shake.isActive);
            }

            [Test]
            public void Shake_is_handled()
            {
                Shake shake = new Shake();
                shake.SetTimer(new ShakeTimer(1f));
                shake.Proceed();

                Assert.That(shake.Completeness > 0f);
            }

            [Test]
            public void Wave_is_moving()
            {
                GlobalTime.SetDeltaTime(0.1f);

                Vector3 axis = new Vector3(1f, 1f, 0f);
                Shake shake = new Shake();

                ShakeCurve curve = new ShakeCurve(4f, 0.2f, 0.2f);

                shake.SetCurve(curve);
                shake.SetTimer(new ShakeTimer(1f));
                shake.SetAxis(axis);
                shake.Launch();
                Vector3 dirOnLaunch = shake.Angle;

                shake.Proceed();

                Assert.AreNotEqual(0f, shake.Dir.magnitude);
                Assert.AreNotEqual(dirOnLaunch, shake.Dir);
            }

            [Test]
            public void Shake_is_over_on_time_out()
            {
                Shake shake = new Shake();

                GlobalTime.SetDeltaTime(1f);
                shake.SetTimer(new ShakeTimer(1f));
                shake.Proceed();

                Assert.IsFalse(shake.isActive);
            }

            [Test]
            public void Raw_wave_is_calculated_correctly()
            {
                ShakeCurve curve = new ShakeCurve(8f, 0.2f, 0.2f);

                Shake shake = new Shake();
                shake.SetCompleteness(1f);
                shake.SetCurve(curve);

                float rawWave = shake.GetRawWave();
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

                shake.Proceed();

                Assert.AreNotEqual(shake.exWave, shake.wave);
            }

            [Test]
            public void Wave_has_passed()
            {
                GlobalTime.SetDeltaTime(0.1f);
                ShakeCurve curve = new ShakeCurve(4f, 0.25f, 0.2f);
                ShakeTimer timer = new ShakeTimer(2f);

                Shake shake = new Shake();
                shake.SetCurve(curve);
                shake.SetTimer(timer);

                Tick(shake.Proceed, 10);
                Assert.IsTrue(shake.HasPassed());
            }

            [Test]
            public void Axis_is_randomized_correctly()
            {
                Vector3 axis = new Vector3(1f, 1f, 0f);
                Shake shake = new Shake();
                Vector3 randomizedAxis = shake.RandomizeVector(axis);
                Assert.AreNotEqual(axis, randomizedAxis);
            }

            [Test]
            public void Gets_random_angle()
            {
                Shake shake = new Shake();
                Vector3 randomAngle = shake.GetRandAngle();
                Assert.AreNotEqual(Vector3.zero, randomAngle);
            }

            [Test]
            public void Direction_is_modified_correctly()
            {
                Vector3 axis = new Vector3(1f, 1f, 0f);
                Vector3 direction = new Vector3(0.3f, 0.3f, 0f);

                Shake shake = new Shake();
                shake.SetAxis(axis);
                shake.SetDir(direction);

                shake.ModifyDirection();

                Assert.AreEqual(1f, Mathf.Round(shake.Dir.magnitude));
            }
            
            [Test]
            public void Direction_is_set()
            {
                Vector3 direction = Vector3.up;
                Shake shake = new Shake();
                shake.SetDir(direction);
                Assert.AreEqual(direction, shake.Dir);
            }

            [Test]
            public void Amplitude_is_calculated_correctly()
            {
                float amplitude = Amplitude.Calculate(0.5f, 0.25f, 0.25f);
                Assert.AreEqual(0.444444478f, amplitude);
            }

            [Test]
            public void Shake_gives_correct_displacement()
            {
                GlobalTime.SetDeltaTime(0.8f);

                Shake shake = new Shake();
                shake.SetAxis(new Vector3(0.8f, 0.2f, 0f));
                shake.SetCurve(new ShakeCurve(4f, 0.25f, 0.2f));
                shake.SetStrength(new ShakeStrength(8f, 1f));
                shake.SetTimer(new ShakeTimer(1f));
                shake.SetAttenuation(1f);

                shake.Launch();
                shake.Proceed();
                ShakeDisplacement displacement = shake.GetDisplacement();

                Assert.AreNotEqual(Vector3.zero, displacement.position);
                Assert.AreNotEqual(Vector3.zero, displacement.angle);
            }

            [Test]
            public void Max_pos_displacement_is_correct()
            {
                ShakeStrength strength = new ShakeStrength(1f, 1f);
                Vector3 axis = new Vector3(1f, 1f, 0f);
                Shake shake = new Shake();

                shake.SetStrength(strength);
                shake.SetAxis(axis);
                shake.Launch();

                Assert.AreNotEqual(Vector3.zero, shake.MaxPosDisplacement);
            }

            [Test]
            public void Max_angle_displacement_is_correct()
            {
                ShakeStrength strength = new ShakeStrength(1f, 1f);
                Vector3 axis = new Vector3(1f, 1f, 0f);
                Shake shake = new Shake();

                shake.SetStrength(strength);
                shake.SetAxis(axis);
                shake.Launch();

                Assert.AreNotEqual(Vector3.zero, shake.MaxAngleDisplacement);
            }

            private void Tick(Action action, int iterations)
            {
                for (int i = 0; i < iterations; i++){
                    action();
                }
            }
        }
    }
}
