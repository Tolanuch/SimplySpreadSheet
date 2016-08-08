using System;

namespace SimplySpreadsheet
{
   public class Cell
    {
       protected String context;
       protected char horizontalIndex;
       protected uint verticalIndex;
       protected String type="IsUnknown";
       protected ComputeBehavior computedCell;

       public Cell()
       {
       }

       public Cell(String context, char horIn, uint verIn)
       {           
           this.context = context;
           this.horizontalIndex = horIn;
           this.verticalIndex = verIn;
           this.cellTypeAnalize();
       }
              
       public void computeCell(Cell[,] sheet)
       {
           computedCell.compute(this, sheet);
       }
       //setting cell context
       public void setContext(String context)
       {
           this.context = context;
       }
       //getting cell context
       public String getContext()
       {
           return this.context;
       }
       //setting horizontal cell index
       public void setHorizontalIndex(char horIn)
       {
           this.horizontalIndex = horIn;
       }
       //getting horizontal cell index
       public char getHorizontalIndex()
       {
           return this.horizontalIndex;
       }
       //setting vertical cell index
       public void setVerticalIndex(uint verIn)
       {
           this.verticalIndex = verIn;
       }
       //getting vertical cell index
       public uint getVerticalIndex()
       {
           return this.verticalIndex;
       }       

       public void setType(String type)
       {
           this.type=type;
       }

       //getting cell type
       public String getType()
       {
           return this.type;
       }
       //Cell type analizing
       void cellTypeAnalize()
       {
           computedCell = new EmptyCompute();
           computedCell.cellTypeAnalize(this); //Checking empty cell
           computedCell = new MathExpressionCompute();
           computedCell.cellTypeAnalize(this); //Checking math expression cell
           computedCell = new LabelCompute();
           computedCell.cellTypeAnalize(this); //Cheking label cell
           computedCell = new NonNegativeCompute();
           computedCell.cellTypeAnalize(this); //Checking non-negative cell
           if (this.type == "IsUnknown")
               this.context = "#AnUnknownCellType"; //Assigning unknown type to cell if it didn`t define cell above
       }
       
    }
}
