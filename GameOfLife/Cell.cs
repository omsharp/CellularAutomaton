namespace GameOfLife
{
   public class Cell
    {
       public int Row    { get; private set; }
       public int Column { get; private set; }
       
       public LifeStatus LifeStatus { get; set; }

       public Cell(int rowLocation, int columnLocation)
       {
           Row        = rowLocation;
           Column     = columnLocation;
           LifeStatus = LifeStatus.Dead;
       }

    }
}
