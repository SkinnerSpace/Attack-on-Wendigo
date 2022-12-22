using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Legs_synchronizer_test
    {
        [Test]
        public void Legs_are_added_correctly()
        {
            LegsSync legsSync = new LegsSync();
            AddFakeLegs(3, legsSync);

            Assert.AreEqual(2, legsSync.GetLegsCount());
        }

        [Test]
        public void Legs_switch_when_step_is_over()
        {
            LegsSync legsSync = new LegsSync();
            AddFakeLegs(2, legsSync);

            legsSync.StepIsOver();

            Assert.AreEqual(1, legsSync.Index);
        }

        private void AddFakeLegs(int count, ILegsSync legsSync)
        {
            for (int i = 0; i < count; i++)
                legsSync.AddLeg(Substitute.For<ILeg>());
        }
    }
}
