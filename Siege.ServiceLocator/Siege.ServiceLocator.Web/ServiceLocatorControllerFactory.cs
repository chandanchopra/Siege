/*   Copyright 2009 - 2010 Marcus Bratton

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

using System;
using System.Web.Mvc;

namespace Siege.ServiceLocator.Web
{
    public class ServiceLocatorControllerFactory : DefaultControllerFactory
    {
        private readonly IServiceLocator locator;

        public ServiceLocatorControllerFactory(IServiceLocator locator)
		{
			this.locator = locator;
		}

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return base.GetControllerInstance(requestContext, controllerType);
            if (!locator.HasTypeRegistered(controllerType))
                return base.GetControllerInstance(requestContext, controllerType);
            return locator.GetInstance<ControllerBase>(controllerType);
        }
    }
}
