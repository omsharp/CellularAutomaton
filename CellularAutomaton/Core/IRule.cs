
using System;

namespace CellularAutomaton.Core
{

    //public interface IRule<in TCell, in TGrid>
    //{
    //    Func<TCell, TGrid, bool> GetWhereCondition();

    //    Action<TCell> GetAction();
    //}
    /*******************************************************/

    //public interface IRule<in TCell, in TGrid>
    //{
    //    string Name { get; }

    //    Func<TCell, TGrid, bool> Condition { get; }

    //    Action<TCell> Action { get; }
    //}

    public interface IRuleCondition<TCell, TGrid>
    {
        IRuleAction<TCell,TGrid> WhenTrue(Func<TCell, TGrid, bool> condition);
    }

    public interface IRuleAction<TCell,  TGrid>
    {
        Rule<TCell,TGrid> Do(Action<TCell> action);
    }

    
    public class Rule<TCell, TGrid> : IRuleCondition<TCell,TGrid>,
                                      IRuleAction<TCell,TGrid>
                                          
    {
        public string Name { get; private set; }
        
        public Func<TCell, TGrid, bool> Condition { get; private set; }
        
        public Action<TCell> Action { get; private set; }

        private Rule(string name)
        {
            Name = name;
        }

        public static Rule<TCell,TGrid> MakeRule(string ruleName)
        {
            return new Rule<TCell, TGrid>(ruleName);
        }

        public IRuleAction<TCell, TGrid> WhenTrue(Func<TCell, TGrid, bool> condition)
        {
            Condition = condition;
            return this;
        }

        public Rule<TCell, TGrid> Do(Action<TCell> action)
        {
            Action = action;
            return this;
        }
    }

}
