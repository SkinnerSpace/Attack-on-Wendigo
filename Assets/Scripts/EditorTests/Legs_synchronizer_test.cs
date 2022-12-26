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
        public void Legs_sync_is_created()
        {
            LegsSync legsSync = new LegsSync(new List<ILeg>());
            Assert.That(legsSync != null);
        }

        [Test]
        public void Legs_sync_walks()
        {
            List<ILeg> legs = new List<ILeg>();
            legs.Add(Substitute.For<ILeg>());

            LegsSync legsSync = new LegsSync(legs);

            legsSync.Walk();

            legs[0].Received().SetNextStep();
            legs[0].Received().Update();
        }

        [Test]
        public void Legs_sync_index_changes_when_step_is_over()
        {
            List<ILeg> legs = new List<ILeg>();
            legs.Add(Substitute.For<ILeg>());
            legs.Add(Substitute.For<ILeg>());

            LegsSync legsSync = new LegsSync(legs);
            legsSync.StepIsOver();

            Assert.AreEqual(1, legsSync.Index);
        }

        [Test]
        public void Legs_sync_step_magninute_observers_are_notified()
        {
            List<ILeg> legs = new List<ILeg>();
            legs.Add(Substitute.For<ILeg>());

            LegsSync legsSync = new LegsSync(legs);

            IStepMagnitudeObserver observer = Substitute.For<IStepMagnitudeObserver>();
            legsSync.AddStepMagnitudeObserver(observer);

            legsSync.Walk();

            observer.Received().ReceiveStepMagnitude(0f);
        }
    }
}
