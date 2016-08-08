namespace SimplySpreadsheet
{
   public interface ComputeBehavior
    {       
       void compute(Cell cell, Cell[,] sheet);
       void cellTypeAnalize(Cell cell);
    }
}
