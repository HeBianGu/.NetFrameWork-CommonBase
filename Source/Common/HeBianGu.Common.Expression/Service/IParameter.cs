using System.Linq.Expressions;

namespace HeBianGu.Common.Expression
{
    public interface IParameter
    {
        ParameterExpression Expression { get; }
        string Name { get; }
    }
}