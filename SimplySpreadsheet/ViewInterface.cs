using System;

namespace SimplySpreadsheet
{
   public interface ViewInterface
    {
       Cell[,] readSheet();
       void displayResultSheet(Cell[,] sheet);
    }
}
