using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using System;

namespace Tests
{
    public class Footsteps_sfx_player_tests
    {
#pragma warning disable CS1701

        [Test]
        public void First_step_is_made()
        {
            ISFXPlayer sFXPlayer = Substitute.For<ISFXPlayer>();

            ICharacterData data = new MockCharacterData() { IsGrounded = true, FlatVelocity = new Vector2(0f,100f) };
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

            Walker walker = new Walker(data, chronos);
            walker.Walk(sFXPlayer.PlaySFX);

            Assert.That(walker.firstStepIsMade);
        }

        [Test]
        public void Make_a_step()
        {
            ISFXPlayer sFXPlayer = Substitute.For<ISFXPlayer>();

            ICharacterData data = new MockCharacterData() { IsGrounded = true, FlatVelocity = new Vector2(0f, 100f) };
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

            Walker walker = new Walker(data, chronos);
            walker.Walk(sFXPlayer.PlaySFX);

            sFXPlayer.Received().PlaySFX();
        }
    }
}
