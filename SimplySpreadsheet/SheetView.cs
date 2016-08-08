using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SimplySpreadsheet
{
    public class SheetView : ViewInterface
    {
        ComputeBehavior computedCell;
        public Cell[,] readSheet()
        {            
            int Width, Height; //table size
            int LinesCount = System.IO.File.ReadAllLines("table.txt").Length;   //Lines count  in file
            StreamReader FileReader = new StreamReader("table.txt");
            string FileLine = FileReader.ReadLine(); //reading the first line
            Height = Convert.ToInt32(FileLine.Split('\t')[0]);
            Width = Convert.ToInt32(FileLine.Split('\t')[1]);
            Cell[,] sheet = new Cell[Height, Width]; //Cells dimantional array
            int i, j; //temprorary indexes for loops
            Int16 HorizontalIntIndex = 64; //it will be converted to char to know letter analog index. It begins from symbol before 'A' 
            char HorizontalIndex = (char)65; uint VerticalIndex = 1;
            Console.WriteLine("Read sheet: ");            
            for (j = 0; j < LinesCount; j++)
            {
                if (FileReader.Peek() != -1) //if it has something to read
                {
                    VerticalIndex = Convert.ToUInt16(j + 1);
                    HorizontalIntIndex = 64;
                    FileLine = FileReader.ReadLine();
                    i = 0;
                    foreach (string Element in FileLine.Split('\t'))
                    {
                        HorizontalIntIndex += 1;
                        HorizontalIndex = (char)HorizontalIntIndex;                        
                        sheet[j, i] = new Cell(FileLine.Split('\t')[i], HorizontalIndex, VerticalIndex);                        
                        i++;
                    }                    
                }
            }   
            //output read table
            int col = sheet.Length / (sheet.Rank+1); //number of columns in array            
            for (j = 0; j < (sheet.Rank + 1); j++)
            {
                Console.WriteLine("");
                for (i = 0; i < col; i++)
                {
                    Console.Write(sheet[j, i].getContext()+"\t");
                }
            }
            return sheet;
        }

        public void displayResultSheet(Cell[,] sheet)
        {
            Console.WriteLine("\n\nResult table: ");
            int i, j; //temprorary indexes for loops
            int col = sheet.Length / (sheet.Rank+1); //number of clomns in array
            for (int k = 0; k < sheet.Length; k++)
            for (j = 0; j < (sheet.Rank + 1); j++)
            {                
                for (i = 0; i < col; i++)
                {
                    if (sheet[j,i].getType()=="IsLabel")
                        computedCell = new LabelCompute();
                    if (sheet[j, i].getType() == "IsMathExpression")
                        computedCell = new MathExpressionCompute();
                    if (sheet[j, i].getType() == "IsNonNegative")
                        computedCell = new NonNegativeCompute();
                    if (sheet[j, i].getType() == "IsEmpty")
                        computedCell = new EmptyCompute();
                    computedCell.compute(sheet[j, i],sheet);               
                }
            }
            for (j = 0; j < (sheet.Rank + 1); j++)
            {
                Console.WriteLine("");
                for (i = 0; i < col; i++)
                {
                    Console.Write(sheet[j,i].getContext() + "\t");
                }
            }
        }
    }
}
