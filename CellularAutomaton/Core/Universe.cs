using System;
using System.Collections.Generic;
using System.Linq;

namespace CellularAutomaton.Core
{
    public class Universe<TCell, TGrid> where TCell : Cell 
                                        where TGrid : ICellularContainer<TCell>
    {
        private Rule<TCell, TGrid> _initialRule;

        /// <summary>
        /// Fired after the NextCycle method finishes.
        /// </summary>
        public EventHandler CycleFinished;

        /// <summary>
        /// Gets a grid of all the cells in this universe.
        /// </summary>
        public TGrid Grid { get; private set; }

        /// <summary>
        /// Gets the count of all the cycles done so far. 
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Gets or sets the list of rules to be applied this universe.
        /// </summary>
        public List<Rule<TCell,TGrid>> Rules { get; set; }

        /// <summary>
        /// Use method chaining to make a new rule, then add it to the Rules list of Universe.
        /// </summary>
        public IRuleCondition<TCell, TGrid> MakeNewRule(string ruleName)
        {
            return Rule<TCell, TGrid>.MakeRule(ruleName);
        } 

        public Universe(TGrid grid)
        {
            if (ReferenceEquals(grid, null))
                throw new ArgumentNullException("grid", "Argument can't be null!");

            Age   = 0;
            Grid  = grid;
            Rules = new List<Rule<TCell, TGrid>>();
        }

        public Universe(TGrid grid, Rule<TCell,TGrid> initialRule) : this(grid)
        {
            if(initialRule == null)
                throw new ArgumentNullException("initialRule", "Argument can't be null!");

            Initialize(initialRule);
        }

        private void Initialize(Rule<TCell, TGrid> initialRule)
        {
            foreach (var cell in Grid.Cells.Where(cell => initialRule.Condition(cell, Grid)))
            {
                initialRule.Action(cell);
            }
        }

        private void ApplyRules()
        {
            if (Rules.Count < 1) return;

            var rules = Rules.Select(r =>
            {
                var condition = r.Condition;
                
                return new {                    //use the returned lambda to find the wanted cells
                             List = Grid.Cells.Where(cell => condition(cell, Grid)),
                             r.Action
                           };
            });

            foreach (var rule in rules)
            {
                foreach (var cell in rule.List)
                {
                    rule.Action(cell);
                }
            }
        }

        /// <summary>
        /// Moves this universe to the next cycle, applying all the rules in the Rules list to this universe.
        /// Fires the CycleFinished event after it's done.
        /// </summary>
        public void NextCycle()
        {
            ApplyRules();

            Age++;

            if (CycleFinished != null)
                CycleFinished(this, new EventArgs());
        }

        /// <summary>
        /// Returns a string identifier of this Universe.
        /// </summary>
        public override string ToString()
        {
            return string.Format("Universe");
        }

    }


}