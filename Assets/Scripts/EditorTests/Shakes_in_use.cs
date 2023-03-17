using System;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    namespace Shake_tests
    {
        public class Shakes_in_use
        {
            [Test]
            public void Screen_shake_is_created()
            {
                IShakeBuilder screenShake = ShakeBuilderImp.Create();
                Assert.That(screenShake != null);
            }

            [Test]
            public void Shake_is_launched()
            {
                GlobalTime.SetDeltaTime(0.5f);

                Shake shake = (Shake) ShakeBuilderImp.Create().withTime(1f).WithAxis(1f, 1f, 0f).WithCurve(4f, 0.25f, 0.25f).Build(); 
                shake.Update();

                Assert.AreNotEqual(Vector3.zero, shake.data.Direction);
                Assert.AreNotEqual(Vector3.zero, shake.data.Angle);
                Assert.IsTrue(shake.IsActive);
            }

            [Test]
            public void Shake_is_handled()
            {
                GlobalTime.SetDeltaTime(0.5f);

                Shake shake = (Shake) ShakeBuilderImp.Create().WithCurve(4f, 0.25f, 0.25f).withTime(1f).Build();
                shake.Update();

                Assert.That(shake.Wave.completeness > 0f);
            }

            [Test]
            public void Wave_is_moving()
            {
                GlobalTime.SetDeltaTime(0.1f);

                Shake shake = (Shake) ShakeBuilderImp.Create().WithCurve(4f, 0.25f, 0.25f).WithAxis(1f, 1f, 0f).withTime(1f).Build();

                shake.Update();
                Vector3 dirOnLaunch = shake.data.Angle;
                shake.Update();

                Assert.AreNotEqual(0f, shake.data.Direction.magnitude);
                Assert.AreNotEqual(dirOnLaunch, shake.data.Direction);
            }

            [Test]
            public void Shake_is_over_on_time_out()
            {
                GlobalTime.SetDeltaTime(1f);

                Shake shake = (Shake) ShakeBuilderImp.Create().withTime(1f).WithCurve(4f, 0.25f, 0.25f).Build();
                shake.Update();

                Assert.IsFalse(shake.IsActive);
            }

            [Test]
            public void Axis_is_randomized_correctly()
            {
                Vector3 axis = new Vector3(1f, 1f, 0f);
                Vector3 randomizedAxis = ShakeUtils.DivertVector(axis);

                Assert.AreNotEqual(Vector3.zero, randomizedAxis);
                Assert.AreNotEqual(axis, randomizedAxis);
            }

            [Test]
            public void Gets_random_angle()
            {
                Vector3 randomAngle = ShakeUtils.GetRandAngle();
                Assert.AreNotEqual(Vector3.zero, randomAngle);
            }

            [Test]
            public void Direction_is_diverted_correctly()
            {
                Vector3 direction = new Vector3(0.3f, 0.3f, 0f);
                Vector3 axis = new Vector3(1f, 1f, 0f);

                Vector3 divertedDirection = ShakeUtils.DivertDirection(direction, axis);

                Assert.AreNotEqual(direction, divertedDirection);
                Assert.AreEqual(1f, Mathf.Round(divertedDirection.magnitude));
            }
      
            [Test]
            public void Wave_completeness_is_set()
            {
                GlobalTime.SetDeltaTime(0.5f);

                Shake shake = (Shake) ShakeBuilderImp.Create().WithCurve(4f, 0.2f, 0.2f).WithStrength(8f, 2f).withTime(1f).Build();
                shake.Update();

                Assert.AreEqual(0.5f, shake.Wave.completeness);
            }

            [Test]
            public void Wave_value_is_correct()
            {
                GlobalTime.SetDeltaTime(0.35f);

                Shake shake = (Shake) ShakeBuilderImp.Create().WithCurve(4f, 0.2f, 0.2f).WithStrength(8f, 2f).withTime(1f).Build();
                shake.Update();

                Assert.AreEqual(5.80133417E-07f, shake.Wave.Value);
            }

            [Test]
            public void Default_attenuation_is_set_to_one()
            {
                Shake shake = (Shake) ShakeBuilderImp.Create().Build();
                Assert.AreEqual(1f, shake.Attenuation.Amount);
            }

            [Test]
            public void Max_pos_displacement_is_correct()
            {
                Shake shake = (Shake) ShakeBuilderImp.Create().WithAxis(1f, 1f, 0f).WithCurve(4f, 0.2f, 0.2f).WithStrength(8f, 2f).withTime(1f).Build();
                shake.Update();

                Assert.AreNotEqual(Vector3.zero, shake.GetMaxPosDisplacement());
            }

            [Test]
            public void Max_angle_displacement_is_correct()
            {
                Shake shake = (Shake) ShakeBuilderImp.Create().WithCurve(4f, 0.2f, 0.2f).WithStrength(8f, 2f).withTime(1f).Build();
                shake.Update();

                Assert.AreNotEqual(Vector3.zero, shake.GetMaxAngleDisplacement());
            }

            [Test]
            public void Shake_gives_correct_displacement()
            {
                GlobalTime.SetDeltaTime(0.16f);

                Shake shake = (Shake) ShakeBuilderImp.Create().WithCurve(4f, 0.25f, 0.25f).WithAxis(1f, 1f, 0f).WithStrength(8f, 2f).withTime(1f).Build();
                shake.Update();

                IShakeDisplacement displacement = shake.GetDisplacement();

                Assert.AreNotEqual(Vector3.zero, displacement.Position);
                Assert.That(displacement.Position.magnitude > 1f);

                Assert.AreNotEqual(Vector3.zero, displacement.Angle);
                Assert.That(displacement.Angle.magnitude > 0.1f);
            }
        }
    }
}
