using WendigoCharacter;
using NUnit.Framework;

namespace Tests
{
    namespace Wendigo
    {
        public class Fireball_spawner
        {
#pragma warning disable CS1701

            [Test]
            public void Fireball_spawner_exist()
            {
                FireballSpawner fireballSpawner = new FireballSpawner();
                Assert.That(fireballSpawner != null);
            }
        }
    }
}
