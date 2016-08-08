using System.Text.RegularExpressions;

namespace SimplySpreadsheet
{
   public class NonNegativeCompute : ComputeBehavior
    {
       public void compute(Cell cell, Cell[,] sheet)
       {
           //Checking for NonNegative cell
           if (cell.getType() != "IsNonNegative")
               return; 
       }

       public void cellTypeAnalize(Cell cell)
       {
           if (Regex.IsMatch(cell.getContext(), "^[0-9]+$") == true)
           {
               cell.setType("IsNonNegative");
           }
       }
    }
}
