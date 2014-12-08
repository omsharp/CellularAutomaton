using System;

namespace CellularAutomaton
{

    public class MovingToNextGenerationFailedException : Exception
    {
        public MovingToNextGenerationFailedException(string msg)
            : base(msg)
        {
        }
    }
}
