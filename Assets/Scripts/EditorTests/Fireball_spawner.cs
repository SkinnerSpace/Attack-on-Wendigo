using WendigoCharacter;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    namespace Wendigo
    {

        public class Fireball_spawner
        {
#pragma warning disable CS1701

            private const float X_MAX_ANGLE = 50f;
            private const float Y_MAX_ANGLE = 70f;
            FireballSpawnerData data = new FireballSpawnerData() { HorizontalAngle = X_MAX_ANGLE, VerticalAngle = Y_MAX_ANGLE };

            [Test]
            public void Fireball_spawner_exist()
            {
                FireballSpawner fireballSpawner = new FireballSpawner(data);
                Assert.That(fireballSpawner != null);
            }

            [Test]
            public void X_angle_positive_constraint_is_correct()
            {
                FireballSpawner spawner = new FireballSpawner(data);
                Assert.AreEqual(35f, spawner.XPositiveMaxAngle);
            }

            [Test]
            public void X_angle_negative_constraint_is_correct()
            {
                FireballSpawner spawner = new FireballSpawner(data);
                Assert.AreEqual(325f, spawner.XNegativeMaxAngle);
            }

            [Test]
            public void Y_angle_positive_constraint_is_correct()
            {
                FireballSpawner spawner = new FireballSpawner(data);
                Assert.AreEqual(25f, spawner.YPositiveMaxAngle);
            }

            [Test]
            public void Y_angle_negative_constraint_is_correct()
            {
                FireballSpawner spawner = new FireballSpawner(data);
                Assert.AreEqual(335f, spawner.YNegativeMaxAngle);
            }

            [Test]
            public void X_angle_is_constrained_when_positive_threshold_is_reached()
            {
                FireballSpawner spawner = new FireballSpawner(data);
                float angle = spawner.GetConstrainedXAngle(60f);
                Assert.AreEqual(spawner.XPositiveMaxAngle, angle);
            }

            [Test]
            public void X_angle_is_constrained_when_negative_threshold_is_reached()
            {
                FireballSpawner spawner = new FireballSpawner(data);
                float angle = spawner.GetConstrainedXAngle(300f);
                Assert.AreEqual(spawner.XNegativeMaxAngle, angle);
            }

            [Test]
            public void Y_angle_is_constrained_when_positive_threshold_is_reached()
            {
                FireballSpawner spawner = new FireballSpawner(data);
                float angle = spawner.GetConstrainedYAngle(30f);
                Assert.AreEqual(spawner.YPositiveMaxAngle, angle);
            }

            [Test]
            public void Y_angle_is_constrained_when_negative_threshold_is_reached()
            {
                FireballSpawner spawner = new FireballSpawner(data);
                float angle = spawner.GetConstrainedYAngle(320f);
                Assert.AreEqual(spawner.YNegativeMaxAngle, angle);
            }

            [Test]
            public void Angles_are_positively_constrained()
            {
                Vector3 angles = new Vector3(60f, 60f, 0f);

                FireballSpawner spawner = new FireballSpawner(data);
                Vector3 constrainedAngles = spawner.GetConstrainedAngles(angles);

                Assert.AreEqual(new Vector3(35f, 25f, 0f), constrainedAngles);
            }

            [Test]
            public void Angles_are_negatively_constrained()
            {
                Vector3 angles = new Vector3(300f, 300f, 0f);

                FireballSpawner spawner = new FireballSpawner(data);
                Vector3 constrainedAngles = spawner.GetConstrainedAngles(angles);

                Assert.AreEqual(new Vector3(325f, 335f, 0f), constrainedAngles);
            }
        }
    }
}
