using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace Tests
{
    public class Titan_creation_test
    {
        [Test]
        public void Ggingerbread_assembly_is_created()
        {
            TitansAssembly gingerbreadAssembly = TitansAssemblyFactory.Create(TitanTypes.GINGERBREAD);
            Assert.That(gingerbreadAssembly != null);
        }

        [Test]
        public void Gingerbread_is_created()
        {
            GingerbreadAssembly gingerbreadAssembly = TitansAssemblyFactory.Create(TitanTypes.GINGERBREAD) as GingerbreadAssembly;
            gingerbreadAssembly.SetCoreComponents(new GingerbreadData(), Substitute.For<ITransformProxy>(), Substitute.For<IClock>());
            Titan gingerbread = gingerbreadAssembly.Assemble();

            Assert.That(gingerbread != null);
        }

        [Test]
        public void Gingerbread_has_clock()
        {
            GingerbreadAssembly gingerbreadAssembly = TitansAssemblyFactory.Create(TitanTypes.GINGERBREAD) as GingerbreadAssembly;
            gingerbreadAssembly.SetCoreComponents(new GingerbreadData(), Substitute.For<ITransformProxy>(), Substitute.For<IClock>());
            Titan gingerbread = gingerbreadAssembly.Assemble();

            Assert.That(gingerbread.clock != null);
        }

        [Test]
        public void Gingerbread_has_movement_controller()
        {
            TitanData data = new GingerbreadData();
            ITransformProxy transformProxy = new FakeTransformProxy(Vector3.zero);

            GingerbreadAssembly gingerbreadAssembly = TitansAssemblyFactory.Create(TitanTypes.GINGERBREAD) as GingerbreadAssembly;
            gingerbreadAssembly.SetCoreComponents(data, transformProxy, Substitute.For<IClock>());
            gingerbreadAssembly.CreateMovementController(new List<ILeg>(), new Torso(Substitute.For<ITransformProxy>()));
            Titan gingerbread = gingerbreadAssembly.Assemble();

            Assert.That(gingerbread.movementController != null);
        }

        [Test]
        public void Gingerbread_has_target_pointer()
        {
            GingerbreadAssembly gingerbreadAssembly = TitansAssemblyFactory.Create(TitanTypes.GINGERBREAD) as GingerbreadAssembly;
            gingerbreadAssembly.SetCoreComponents(new GingerbreadData(), Substitute.For<ITransformProxy>(), Substitute.For<IClock>());
            Titan gingerbread = gingerbreadAssembly.Assemble();

            Assert.That(gingerbread.targetPointer != null);
        }
    }
}
