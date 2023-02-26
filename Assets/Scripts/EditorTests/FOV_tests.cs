using UnityEngine;
using NUnit.Framework;
using NSubstitute;


namespace Tests
{
    public class FOV_tests
    {
        [Test]
        public void FOV_does_not_change_when_stand()
        {
            /*ICharacterData data = new MockCharacterData() { 
                MinFOV = 80f, 
                MaxFOV = 120f, 
                FOVChangeSpeed = 10f, 
                Speed = 300f, 
                GroundDeceleration = 20f };

            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);


            Assert.AreEqual(data.MinFOV, data.FOV);*/
        }

        /*[Test]
        public void FOV_increases_when_move()
        {
            ICharacterData data = new MockCharacterData()
            {
                FlatVelocity = new Vector2(100f, 0f),
                MinFOV = 80f,
                MaxFOV = 120f,
                FOVChangeSpeed = 10f,
                Speed = 300f,
                GroundDeceleration = 20f
            };

            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

 
            Assert.Greater(data.FOV, data.MinFOV);
        }

        [Test]
        public void FOV_decreases_when_stop()
        {
            ICharacterData data = new MockCharacterData()
            {
                FlatVelocity = new Vector2(100f, 0f),
                MinFOV = 80f,
                MaxFOV = 120f,
                FOVChangeSpeed = 10f,
                Speed = 300f,
                GroundDeceleration = 20f
            };

            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);


            Assert.Less(data.FOV, data.MaxFOV);
        }*/
    }
}
