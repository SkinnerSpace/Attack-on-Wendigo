using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class Vision_tests
    {
        [Test]
        public void Vision_observers_are_notified()
        {
            VisionTarget target = new VisionTarget();

            VisionDetector vision = new VisionDetector();
            
            IVisionObserver observer = Substitute.For<IVisionObserver>();
            vision.AddObserver(observer);

            vision.NotifyOnUpdate(target);

            observer.Received().OnTargetUpdate(target);
        }

        [Test]
        public void Target_is_suitable()
        {
            /*VisionTarget target = new VisionTarget() { IsValid = true, type = typeof(IPickable), distance = 2f };

            VisionValidator evaluator = new VisionValidator();
            evaluator.AddSample(typeof(IPickable), 3f);

            bool isSuitable = evaluator.Validate(target);
            Assert.IsTrue(isSuitable);*/
        }

        [Test]
        public void Target_is_not_valid()
        {
            /*VisionTarget target = new VisionTarget() { IsValid = false, type = typeof(IPickable), distance = 2f };

            VisionValidator evaluator = new VisionValidator();
            evaluator.AddSample(typeof(IPickable), 3f);

            bool isSuitable = evaluator.Validate(target);
            Assert.IsFalse(isSuitable);*/
        }

        [Test]
        public void Target_has_wrong_type()
        {
            /*VisionTarget target = new VisionTarget() { IsValid = true, type = typeof(IDamageable), distance = 2f };

            VisionValidator evaluator = new VisionValidator();
            evaluator.AddSample(typeof(IPickable), 3f);

            bool isSuitable = evaluator.Validate(target);
            Assert.IsFalse(isSuitable);*/
        }

        [Test]
        public void Target_is_too_far()
        {
            /*VisionTarget target = new VisionTarget() { IsValid = true, type = typeof(IPickable), distance = 4f };

            VisionValidator evaluator = new VisionValidator();
            evaluator.AddSample(typeof(IPickable), 3f);

            bool isSuitable = evaluator.Validate(target);
            Assert.IsFalse(isSuitable);*/
        }

        [Test]
        public void Active_if_at_least_one_trigger_is_active()
        {
            VisionDetector vision = new VisionDetector();
            VisionTrigger node = new VisionTrigger();

            CreateTrigger(vision, node, typeof(IOldPickable), 3f);
            CreateTrigger(vision, node, typeof(IDamageable), float.PositiveInfinity);

            VisionTarget target = new VisionTarget() { IsValid = true, type = typeof(IOldPickable), distance = 2f };
            vision.NotifyOnUpdate(target);

            Assert.That(node.IsActive);
        }

        [Test]
        public void Not_active_if_none_of_the_triggers_is_active()
        {
            VisionDetector vision = new VisionDetector();
            VisionTrigger node = new VisionTrigger();

            CreateTrigger(vision, node, typeof(IOldPickable), 3f);
            CreateTrigger(vision, node, typeof(IDamageable), float.PositiveInfinity);

            VisionTarget target = new VisionTarget() { IsValid = false, type = typeof(IGroundObserver), distance = 4f };
            vision.NotifyOnUpdate(target);

            Assert.That(!node.IsActive);
        }

        private void CreateTrigger(VisionDetector vision, VisionTrigger node, Type type, float distance)
        {
            VisionTrigger trigger = new VisionTrigger();
            trigger.AddDependee(node);

            VisionValidator evaluator = new VisionValidator();
            evaluator.AddSample(type, distance);
            trigger.SetEvaluator(evaluator);

            vision.AddObserver(trigger);
        }
    }
}
