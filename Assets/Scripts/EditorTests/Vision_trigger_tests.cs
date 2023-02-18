using NUnit.Framework;

namespace Tests
{
    public class Vision_trigger_tests
    {
        [Test]
        public void Trigger_on_command()
        {
            VisionTrigger trigger = new VisionTrigger();
            trigger.SetActive(true);

            Assert.That(trigger.IsActive);
        }

        [Test]
        public void One_trigger_is_connected_to_the_other()
        {
            VisionTrigger firstTrigger = new VisionTrigger();
            VisionTrigger secondTrigger = new VisionTrigger();

            secondTrigger.AddDependee(firstTrigger);
            secondTrigger.SetActive(true);

            Assert.That(firstTrigger.IsActive);
        }

        [Test]
        public void Active_if_at_least_one_trigger_is_active()
        {
            VisionTrigger firstTrigger = new VisionTrigger();
            VisionTrigger secondTrigger = new VisionTrigger();
            VisionTrigger thirdTrigger = new VisionTrigger();

            secondTrigger.AddDependee(firstTrigger);
            secondTrigger.SetActive(true);

            thirdTrigger.AddDependee(firstTrigger);
            thirdTrigger.SetActive(false);

            Assert.That(firstTrigger.IsActive);
        }

        [Test]
        public void Not_active_if_none_of_the_triggers_are_active()
        {
            VisionTrigger firstTrigger = new VisionTrigger();
            VisionTrigger secondTrigger = new VisionTrigger();
            VisionTrigger thirdTrigger = new VisionTrigger();

            secondTrigger.AddDependee(firstTrigger);
            secondTrigger.SetActive(false);

            thirdTrigger.AddDependee(firstTrigger);
            thirdTrigger.SetActive(false);

            Assert.That(!firstTrigger.IsActive);
        }

        [Test]
        public void Trigger_chain_from_start_to_end()
        {
            VisionTrigger firstTrigger = new VisionTrigger();
            VisionTrigger secondTrigger = new VisionTrigger();
            VisionTrigger thirdTrigger = new VisionTrigger();

            secondTrigger.AddDependee(firstTrigger);
            thirdTrigger.AddDependee(secondTrigger);
            thirdTrigger.SetActive(true);

            Assert.That(firstTrigger.IsActive);
        }

        [Test]
        public void Trigger_chain_in_middle_triggers_only_one_half()
        {
            VisionTrigger firstTrigger = new VisionTrigger();
            VisionTrigger secondTrigger = new VisionTrigger();
            VisionTrigger thirdTrigger = new VisionTrigger();

            secondTrigger.AddDependee(firstTrigger);
            thirdTrigger.AddDependee(secondTrigger);

            secondTrigger.SetActive(true);

            Assert.That(firstTrigger.IsActive);
            Assert.That(!thirdTrigger.IsActive);
        }

        [Test]
        public void Deactivate_if_all_the_triggers_are_inactive()
        {
            VisionTrigger firstTrigger = new VisionTrigger();
            VisionTrigger secondTrigger = new VisionTrigger();
            VisionTrigger thirdTrigger = new VisionTrigger();

            secondTrigger.AddDependee(firstTrigger);
            thirdTrigger.AddDependee(firstTrigger);
            
            firstTrigger.SetActive(true);
            secondTrigger.SetActive(true);
            thirdTrigger.SetActive(true);

            secondTrigger.SetActive(false);
            thirdTrigger.SetActive(false);

            Assert.That(!firstTrigger.IsActive);
        }
    }
}
