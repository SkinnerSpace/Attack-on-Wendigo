using NUnit.Framework;

namespace Tests
{

    namespace Spawner
    {
        public class Spawner_tests
        {
/*            [Test]
            public void Spawn_no_more_than_concurrently_allowed()
            {
                const int ALLOWED = 3;

                WendigoSpawnerData data = new WendigoSpawnerData(){
                    allowedConcurrentCount = ALLOWED,
                    initialSpawnCount = 30
                };
                data.Initialize();

                WendigoSpawnerLogic spawnerLogic = new WendigoSpawnerLogic(){
                    data = data
                };

                SpawnIfPossible(10, spawnerLogic);

                Assert.AreEqual(ALLOWED, data.currentCount);
            }

            [Test]
            public void Live_count_decrements_on_death()
            {
                const int ALLOWED = 3;

                WendigoSpawnerData data = new WendigoSpawnerData()
                {
                    allowedConcurrentCount = ALLOWED,
                    initialSpawnCount = 30
                };
                data.Initialize();

                WendigoSpawnerLogic spawnerLogic = new WendigoSpawnerLogic(){
                    data = data
                };

                SpawnIfPossible(ALLOWED, spawnerLogic);
                spawnerLogic.OnDeath();

                Assert.AreEqual(ALLOWED-1, data.currentCount);
            }

            [Test]
            public void Spawn_one_more_time_if_one_has_died()
            {
                const int ALLOWED_CONCURRENT_COUNT = 3;

                WendigoSpawnerData data = new WendigoSpawnerData()
                {
                    allowedConcurrentCount = ALLOWED_CONCURRENT_COUNT,
                    initialSpawnCount = 30
                };
                data.Initialize();

                WendigoSpawnerLogic spawnerLogic = new WendigoSpawnerLogic(){
                    data = data
                };

                SpawnIfPossible(ALLOWED_CONCURRENT_COUNT, spawnerLogic);
                spawnerLogic.OnDeath();
                spawnerLogic.Spawn();

                Assert.AreEqual(ALLOWED_CONCURRENT_COUNT, data.currentCount);
            }

            [Test]
            public void Spawn_no_more_than_left_to_spawn()
            {
                const int INITIAL_COUNT = 10;

                WendigoSpawnerData data = new WendigoSpawnerData() {
                    allowedConcurrentCount = 100,
                    initialSpawnCount = INITIAL_COUNT
                };
                data.Initialize();

                WendigoSpawnerLogic spawnerLogic = new WendigoSpawnerLogic(){
                    data = data
                };

                SpawnIfPossible(15, spawnerLogic);

                Assert.AreEqual(INITIAL_COUNT, data.currentCount);
                Assert.AreEqual(0, data.leftToSpawnCount);
            }

            [Test]
            public void Observers_are_notified_on_spawn()
            {
                bool notifiedOnSpawn = false;

                WendigoSpawnerLogic spawnerLogic = new WendigoSpawnerLogic() {
                    data = new WendigoSpawnerData()
                };

                spawnerLogic.SubscribeOnSpawn(() => notifiedOnSpawn = true);
                spawnerLogic.Spawn();

                Assert.IsTrue(notifiedOnSpawn);
            }

            private void SpawnIfPossible(int times, WendigoSpawnerLogic spawner)
            {
                for (int i = 0; i < times; i++){
                    spawner.SpawnIfPossible();
                }
            }

            private void Spawn(int times, WendigoSpawnerLogic spawner)
            {
                for (int i = 0; i < times; i++){
                    spawner.Spawn();
                }
            }*/
        }
    }
}
