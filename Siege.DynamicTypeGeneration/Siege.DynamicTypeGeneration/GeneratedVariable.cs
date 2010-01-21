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
using System.Linq.Expressions;
using System.Reflection;
using Siege.DynamicTypeGeneration.Actions;

namespace Siege.DynamicTypeGeneration
{
    public class GeneratedVariable : ILocalIndexer
    {
        private readonly Func<Type> type;
        private readonly Func<MethodInfo> methodInfo;
        private readonly Func<BuilderBundle> builderBundle;
        protected readonly int localIndex;
        protected IList<ITypeGenerationAction> actions;
        protected readonly GeneratedMethod method;

        public GeneratedVariable(Func<Type> type, int localIndex, IList<ITypeGenerationAction> actions, GeneratedMethod method)
        {
            this.type = type;
            this.localIndex = localIndex;
            this.actions = actions;
            this.method = method;
        }

        public GeneratedVariable(Type type, int localIndex, IList<ITypeGenerationAction> actions, GeneratedMethod method)
        {
            this.type = () => type;
            this.localIndex = localIndex;
            this.actions = actions;
            this.method = method;
        }

        public GeneratedVariable(Func<MethodInfo> methodInfo, int localIndex, IList<ITypeGenerationAction> actions, GeneratedMethod method)
        {
            this.methodInfo = methodInfo;
            this.localIndex = localIndex;
            this.actions = actions;
            this.method = method;
        }

        public GeneratedVariable(Func<BuilderBundle> builderBundle, int localIndex, IList<ITypeGenerationAction> actions, GeneratedMethod method)
        {
            this.builderBundle = builderBundle;
            this.localIndex = localIndex;
            this.actions = actions;
            this.method = method;
        }

        public Type Type
        {
            get { return type() ?? builderBundle().TypeBuilder ?? methodInfo().ReturnType; }
        }

        public void AssignFrom(Func<ILocalIndexer> item)
        {
            item();
            actions.Add(new VariableAssignmentAction(() => method.MethodBuilder(), localIndex));
        }

        public void Pop(Func<ILocalIndexer> item)
        {
            item();
            actions.Add(new PopAction(method));
        }

        public int LocalIndex
        {
            get { return localIndex; }
        }

        public ILocalIndexer Invoke<TType>(Expression<Action<TType>> expression)
        {
            MethodCallExpression methodCall = expression.Body as MethodCallExpression;
            return Invoke(methodCall.Method);
        }

        public ILocalIndexer Invoke<TType>(Expression<Action<TType>> expression, GeneratedVariable variable)
        {
            MethodCallExpression methodCall = expression.Body as MethodCallExpression;
            
            return Invoke(methodCall.Method, variable);
        }

        public ILocalIndexer Invoke(MethodInfo methodInfo)
        {
            List<IGeneratedParameter> parameters = new List<IGeneratedParameter>();

            int argCount = 0;
            foreach (ParameterInfo info in methodInfo.GetParameters())
            {
                argCount++;
                parameters.Add(new ExpressionParameter(info.ParameterType, argCount));
            }

            actions.Add(new VariableLoadAction(method, this.LocalIndex));
            return method.Call(() => methodInfo, () => parameters);
        }

        public ILocalIndexer Invoke(MethodInfo methodInfo, GeneratedVariable variable)
        {
            actions.Add(new VariableLoadAction(method, this.LocalIndex));
            return method.Call(() => methodInfo, variable);
        }

        public void AssignFrom(GeneratedField field)
        {
            actions.Add(new FieldLoadAction(method, field.Field));
            actions.Add(new VariableAssignmentAction(() => method.MethodBuilder(), localIndex));
        }

        public MethodInfo GetMethod<TType>(Expression<Action<TType>> expression)
        {
            MethodCallExpression methodCall = expression.Body as MethodCallExpression;
            return methodCall.Method;
        }
    }
}
