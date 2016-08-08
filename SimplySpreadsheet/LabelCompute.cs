using System;
using System.Text.RegularExpressions;

namespace SimplySpreadsheet
{
    public class LabelCompute : ComputeBehavior
    {
        public void compute(Cell cell, Cell[,] sheet)
        {
            //Checking for label cell
            if (cell.getType() != "IsLabel")
                return; 
            String tmpStr = cell.getContext();            
            cell.setContext(tmpStr.TrimStart('\''));
        }

        public void cellTypeAnalize(Cell cell)
        {
            if (Regex.IsMatch(cell.getContext(), "^'[A-Za-z]+$") == true)
            {
                cell.setType("IsLabel");
            }     
        }
    }
}
