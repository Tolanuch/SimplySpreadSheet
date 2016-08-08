using System.Text.RegularExpressions;

namespace SimplySpreadsheet
{
   public class EmptyCompute : ComputeBehavior
    {
       public void compute(Cell cell, Cell[,] sheet)
       {
           //Checking for empty cell
           if (cell.getType() != "IsEmpty")
               return; 
       }

       public void cellTypeAnalize(Cell cell)
       {
           if (Regex.IsMatch(cell.getContext(), "^$") == true)
           {
               cell.setType("IsEmpty");
           }
       }   

    }
}
