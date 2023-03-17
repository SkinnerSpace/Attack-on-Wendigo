using NUnit.Framework;

namespace Tests
{
    namespace Shake_tests
    {
        public class Shake_amplitude
        {
            [Test]
            public void Returns_highest_value_in_the_middle()
            {
                float amplitude = Amplitude.Calculate(0.5f, 0.25f, 0.25f);
                Assert.AreEqual(1f, amplitude);
            }

            [Test]
            public void Returns_zero_at_the_beginning()
            {
                float amplitude = Amplitude.Calculate(0f, 0.25f, 0.25f);
                Assert.AreEqual(0f, amplitude);
            }

            [Test]
            public void Returns_zero_at_the_end()
            {
                float amplitude = Amplitude.Calculate(1f, 0.25f, 0.25f);
                Assert.AreEqual(0f, amplitude);
            }

            [Test]
            public void Returns_correct_value_on_attack()
            {
                float amplitude = Amplitude.Calculate(0.1f, 0.25f, 0.25f);
                Assert.AreEqual(0.632455528f, amplitude);
            }

            [Test]
            public void Returns_correct_value_on_release()
            {
                float amplitude = Amplitude.Calculate(0.9f, 0.25f, 0.25f);
                Assert.AreEqual(0.0177777875f, amplitude);
            }
        }
    }
}
