using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovator.Client.QueryModel
{
  public class LikeOperator : BinaryOperator, IBooleanOperator, INormalize
  {
    public override int Precedence => (int)PrecedenceLevel.Comparison;
    QueryItem ITableProvider.Table { get; set; }

    public virtual IExpression Normalize()
    {
      SetTable();
      return this;
    }

    public override void Visit(IExpressionVisitor visitor)
    {
      visitor.Visit(this);
    }

    internal static IExpression FromMethod(string name, IExpression left, IExpression right)
    {
      if (right is StringLiteral str)
      {
        switch (name.ToLowerInvariant())
        {
          case "startswith":
            right = new PatternList()
            {
              Patterns =
              {
                new Pattern() {
                  Matches =
                  {
                    new Anchor() { Type = AnchorType.Start_Absolute },
                    new StringMatch(str.Value)
                  }
                }
              }
            };
            break;
          case "endswith":
            right = new PatternList()
            {
              Patterns =
              {
                new Pattern() {
                  Matches =
                  {
                    new StringMatch(str.Value),
                    new Anchor() { Type = AnchorType.End_Absolute }
                  }
                }
              }
            };
            break;
          case "contains":
            right = new PatternList()
            {
              Patterns =
              {
                new Pattern() {
                  Matches = { new StringMatch(str.Value) }
                }
              }
            };
            break;
          default:
            throw new NotSupportedException();
        }

        return new LikeOperator() { Left = left, Right = right }.Normalize();
      }
      else
      {
        switch (name.ToLowerInvariant())
        {
          case "startswith":
            return new Functions.StartsWith()
            {
              String = left,
              Find = right
            };
          case "endswith":
            return new Functions.EndsWith()
            {
              String = left,
              Find = right
            };
          case "contains":
            return new Functions.Contains()
            {
              String = left,
              Find = right
            };
          default:
            throw new NotSupportedException();
        }
      }
    }
  }
}
