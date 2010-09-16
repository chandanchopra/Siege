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
using Siege.Requisitions.Resolution;

namespace Siege.Requisitions.Extensions.InjectionOverrides
{
    public class InjectionOverrideStore : IInjectionOverrideStore
    {
        private readonly List<IResolutionArgument> arguments = new List<IResolutionArgument>();

        public void Dispose()
        {
            foreach (var item in arguments)
            {
                if (item is IDisposable)
                {
                    (item as IDisposable).Dispose();
                }
            }
        }

        public void Add(List<IResolutionArgument> contextItems)
        {
            arguments.AddRange(contextItems);
        }

        public List<IResolutionArgument> Items
        {
            get { return arguments; }
        }

        public void Clear()
        {
            arguments.Clear();
        }
    }
}