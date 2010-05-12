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

using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.FactorySupport;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Siege.ServiceLocation;
using Siege.ServiceLocation.Exceptions;
using Siege.ServiceLocation.Resolution;

namespace Siege.SeviceLocation.WindsorAdapter
{
    public class WindsorAdapter : IServiceLocatorAdapter
    {
        private IKernel kernel;

        public WindsorAdapter()
            : this(new DefaultKernel())
        {
        }

        public WindsorAdapter(IKernel kernel)
        {
            this.kernel = kernel;
            if(this.kernel.GetFacilities().OfType<FactorySupportFacility>().Count() == 0)
            {
            	this.kernel.AddFacility<FactorySupportFacility>();
            }
        }

        public void RegisterBinding(Type baseBinding, Type targetBinding)
        {
            kernel.Register(Component.For(baseBinding).ImplementedBy(targetBinding));
        }

        public void Dispose()
        {
            //bug in windsor lol
            //kernel.Dispose();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return (IEnumerable<object>)kernel.ResolveAll(serviceType);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return kernel.ResolveAll<TService>();
        }

		public object GetInstance(Type type, string key, params IResolutionArgument[] parameters)
		{
			try
			{
				Dictionary<string, object> args = new Dictionary<string, object>();

				foreach (ConstructorParameter parameter in parameters.OfType<ConstructorParameter>())
				{
					args.Add(parameter.Name, parameter.Value);
				}

				return kernel.Resolve(key, type, args);
			}
			catch (ComponentNotFoundException ex)
			{
				throw new RegistrationNotFoundException(type, key, ex);
			}
		}

		public object GetInstance(Type type, params IResolutionArgument[] parameters)
		{
			try
			{
				Dictionary<string, object> args = new Dictionary<string, object>();

				foreach (ConstructorParameter parameter in parameters.OfType<ConstructorParameter>())
				{
					args.Add(parameter.Name, parameter.Value);
				}

				return kernel.Resolve(type, args);
			}
			catch (Exception ex)
			{
				throw new RegistrationNotFoundException(type, ex);
			}
		}

        public bool HasTypeRegistered(Type type)
        {
            return kernel.HasComponent(type);
        }

        public Type ConditionalUseCaseBinding
        {
            get { return typeof(ConditionalUseCaseBinding<>); }
        }

        public Type DefaultUseCaseBinding
        {
            get { return typeof(DefaultUseCaseBinding<>); }
        }

        public Type KeyBasedUseCaseBinding
        {
            get { return typeof(KeyBasedUseCaseBinding<>); }
        }

        public Type OpenGenericUseCaseBinding
        {
            get { return typeof(OpenGenericUseCaseBinding); }
        }
    }
}