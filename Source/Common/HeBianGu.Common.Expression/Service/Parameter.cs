using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HeBianGu.Common.Expression
{
    public class Parameter<T> : IParameter
    {
        public Parameter(string name)
        {
            Name = name;

            Expression = System.Linq.Expressions.Expression.Parameter(typeof(T), name);
        }

        public string Name { get; private set; }

        public ParameterExpression Expression { get; private set; }
    }
}
