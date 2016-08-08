using System;

namespace SimplySpreadsheet 
{
    class SimplySpreadSheet
    {
        public static void Main(String[] args)
        {
            SheetView sheet = new SheetView();
            Cell[,] table;
            table = sheet.readSheet();
            sheet.displayResultSheet(table);
            Console.ReadLine();
        }
    }
}
