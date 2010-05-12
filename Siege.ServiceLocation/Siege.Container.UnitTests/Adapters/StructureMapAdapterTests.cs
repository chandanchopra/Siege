﻿/*   Copyright 2009 - 2010 Marcus Bratton

     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
*/

using NUnit.Framework;
using Siege.ServiceLocation.UnitTests.TestClasses;
using StructureMap;

namespace Siege.ServiceLocation.UnitTests.Adapters
{
    [TestFixture]
    public class StructureMapAdapterTests : SiegeContainerTests
    {
        private Container container;
        protected override IServiceLocatorAdapter GetAdapter()
        {
            container = new Container();
            return new StructureMapAdapter.StructureMapAdapter(container);
        }

        protected override void RegisterWithoutSiege()
        {
            container.Configure(registry => registry.ForRequestedType<IUnregisteredInterface>().TheDefaultIsConcreteType<UnregisteredClass>());
        }

        public override void Should_Not_Be_Able_To_Bind_An_Interface_To_A_Type_With_A_Name_When_No_Name_Provided()
        {
            base.Should_Not_Be_Able_To_Bind_An_Interface_To_A_Type_With_A_Name_When_No_Name_Provided();
            Assert.IsTrue(locator.GetInstance<ITestInterface>() is TestCase1);
        }

		[Ignore("StructureMap sucks")]
		public override void Should_Use_Unregistered_Constructor_Argument_With_Name()
		{
			base.Should_Use_Unregistered_Constructor_Argument_With_Name();
		}
    }
}