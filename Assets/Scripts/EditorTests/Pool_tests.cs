using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{


    public class Pool_tests
    {
        private const string FIRST_POOL = "First";
        private const string SECOND_POOL = "Second";

        [Test]
        public void PoolTemplatesExist()
        {
            List<PoolTemplate> poolTemplates = GetPoolTemplates();
            Assert.That(poolTemplates.Count > 0);
        }

        [Test]
        public void Pool_is_empty_at_beginning()
        {
            Dictionary<string, Queue<IPooledObject>> pools = GetEmptyPools();
            Assert.That(pools.Values.Count == 0);
        }

        [Test]
        public void Pool_is_filled_by_implementor()
        {
            Dictionary<string, Queue<IPooledObject>> pools = GetEmptyPools();
            PoolImplementor implementor = new PoolImplementor(new MockPoolObjectFactory());
            implementor.ImplementThePool(pools, GetPoolTemplate(FIRST_POOL, 8));

            Assert.That(pools.Values.Count > 0);
        }

        [Test]
        public void New_pool_is_added_when_has_a_unique_tag()
        {
            Dictionary<string, Queue<IPooledObject>> pools = GetEmptyPools();
            PoolImplementor implementor = new PoolImplementor(new MockPoolObjectFactory());

            implementor.ImplementThePool(pools, GetPoolTemplate(FIRST_POOL, 8));
            implementor.ImplementThePool(pools, GetPoolTemplate(SECOND_POOL, 12));

            Assert.That(pools.Values.Count == 2);
        }

        [Test]
        public void Existing_pool_is_expanded_when_has_the_same_tag()
        {
            Dictionary<string, Queue<IPooledObject>> pools = GetEmptyPools();
            PoolImplementor implementor = new PoolImplementor(new MockPoolObjectFactory());

            implementor.ImplementThePool(pools, GetPoolTemplate(FIRST_POOL, 8));
            implementor.ImplementThePool(pools, GetPoolTemplate(FIRST_POOL, 12));

            Assert.That(pools.Values.Count == 1);
            Assert.That(pools[FIRST_POOL].Count == 12);
        }

        private Dictionary<string, Queue<IPooledObject>> GetEmptyPools() => new Dictionary<string, Queue<IPooledObject>>();

        private PoolTemplate GetPoolTemplate(string tag, int size) => new PoolTemplate() { tag = tag, prefab = null, size = size };

        private List<PoolTemplate> GetPoolTemplates()
        {
            List<PoolTemplate> poolTemplates = new List<PoolTemplate>()
            {
                new PoolTemplate(){tag = "First", prefab = null, size = 8},
                new PoolTemplate(){tag = "Second", prefab = null, size = 4},
                new PoolTemplate(){tag = "Third", prefab = null, size = 6}
            };

            return poolTemplates;
        }
    }
}
