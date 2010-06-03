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
using System.Reflection;
using Siege.ServiceLocation.Planning;

namespace Siege.ServiceLocation.SiegeAdapter.Maps
{
    public class TypeMap : AbstractMap
    {
        public void Add(Type from, Type to, string key)
        {
            if (!entries.ContainsKey(from))
            {
                var list = new TypeMapList(from);
                list.Add(to, key);
                entries.Add(from, list);
            }
        }

        public bool Contains(Type type)
        {
            if (!entries.ContainsKey(type) && type.IsClass)
            {
                Add(type, type, null);
            }

            return entries.ContainsKey(type);
        }

        private Type CreateGeneric(Type type, string name)
        {
            Type definition = type.GetGenericTypeDefinition();
            var entry = (TypeMapList) entries[definition];
            List<MappedType> types = entry.MappedTypes;
            MappedType item = GetMappedType(name, types) ?? entry.MappedTypes[0];

            if (!item.To.IsGenericType) return item.To;

            return item.To.MakeGenericType(type.GetGenericArguments());
        }

        private bool CanConstructGenericType(Type type)
        {
            if (!type.IsGenericType) return false;

            if (entries.ContainsKey(type.GetGenericTypeDefinition())) return true;

            return false;
        }

        public MappedType GetMappedType(Type type, string name)
        {
            return SelectItem(type, name);
        }

        private MappedType SelectItem(Type type, string name)
        {
            if (!entries.ContainsKey(type) && CanConstructGenericType(type))
            {
                Type generic = CreateGeneric(type, name);

                Add(generic, generic, null);

                type = generic;
            }

            if (!entries.ContainsKey(type)) return null;

            var entry = (TypeMapList) entries[type];
            List<MappedType> types = entry.MappedTypes;

            return GetMappedType(name, types);
        }

        private MappedType GetMappedType(string name, List<MappedType> types)
        {
            for (int i = 0; i < types.Count; i++)
            {
                MappedType type = types[i];

                if (type.Name == name) return type;
            }

            return null;
        }
    }

    internal class TypeMapList : AbstractMapList
    {
        private List<Type> registeredTypes = new List<Type>();

        public TypeMapList(Type type)
        {
            Type = type;
            MappedTypes = new List<MappedType>();
        }

        public List<MappedType> MappedTypes { get; private set; }

        public void Add(Type to, string name)
        {
            if (registeredTypes.Contains(to)) return;
            if (!to.IsClass)
                throw new ApplicationException("Cannot map type " + Type + " to the interface " + to +
                                               ". You must map to a class.");

            registeredTypes.Add(to);
            MappedTypes.Add(new MappedType(Type, to, name));
        }
    }

    public class MappedType
    {
        public MappedType(Type from, Type to, string name)
        {
            Name = name;
            From = from;
            To = to;
            Candidates = new List<ConstructorCandidate>();

            BuildCandidateList();
        }

        public string Name { get; private set; }
        public Type From { get; private set; }
        public Type To { get; private set; }
        public List<ConstructorCandidate> Candidates { get; private set; }

        private void BuildCandidateList()
        {
            ConstructorInfo[] constructors = To.GetConstructors();
            int constructorCount = constructors.Length;
            for (int counter = 0; counter < constructorCount; counter++)
            {
                ConstructorInfo constructor = constructors[counter];
                var candidate = new ConstructorCandidate {Type = To};
                ParameterInfo[] parameters = constructor.GetParameters();
                int count = parameters.Length;

                for (int i = 0; i < count; i++)
                {
                    ParameterInfo parameter = parameters[i];
                    var summary = new ParameterSummary
                                      {
                                          Position = parameter.Position,
                                          ParameterType = parameter.ParameterType,
                                          Name = parameter.Name
                                      };

                    candidate.Parameters.Add(summary);
                }

                candidate.Instantiate = array => constructor.Invoke(array);

                Candidates.Add(candidate);
            }
        }
    }
}