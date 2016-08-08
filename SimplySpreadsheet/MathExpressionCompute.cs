using System;
using System.Text.RegularExpressions;
using System.Collections;

namespace SimplySpreadsheet
{
    public class MathExpressionCompute : ComputeBehavior
    {
        public void compute(Cell cell, Cell[,] sheet)
        {       
            //Checking for math expression
            if (cell.getType() != "IsMathExpression")
                return;            
            ArrayList expressionBlocks = new ArrayList(); //Array for expression symbols
            String context = cell.getContext();                 
            int i,m,n ; //indexes for loops
            i = m=n = 0;
            //Looking for linked cells
            for (i = 1; i < context.Length; i++)
            {
                if ((context[i] >= 'A') && (context[i] <= 'Z'))
                {
                    for (m = 0; m < (sheet.Rank + 1); m++)
                    {
                        for (n = 0; n < (sheet.Length / (sheet.Rank + 1)); n++)
                        {
                            //Checking links to NonNegative cells
                            if ((sheet[m, n].getHorizontalIndex() == context[i]) && (sheet[m, n].getVerticalIndex().ToString() == context[i + 1].ToString()) && (sheet[m, n].getType() == "IsNonNegative"))
                            {                                
                                expressionBlocks.Add(sheet[m, n].getContext());
                            }
                            //Checking links to non math cells
                            if ((sheet[m, n].getHorizontalIndex() == context[i]) && (sheet[m, n].getVerticalIndex().ToString() == context[i + 1].ToString()) && ((sheet[m, n].getType() != "IsNonNegative") && (sheet[m, n].getType() != "IsMathExpression")))
                            {
                                cell.setContext("#CantFindResult");
                                cell.setType("IsUnknown");
                                return;
                            }
                            //adding link that does not have number at this moment
                            if ((sheet[m, n].getHorizontalIndex() == context[i]) && (sheet[m, n].getVerticalIndex().ToString() == context[i + 1].ToString()) && (sheet[m, n].getType() == "IsMathExpression"))
                            {
                                expressionBlocks.Add((context[i] + context[i + 1].ToString()));
                            }
                        }
                    }
                    //Checking for loop end
                    if (i !=(context.Length-1))
                        i++;                    
                }
                else
                {
                    //adding math operators or numbers to expression
                    expressionBlocks.Add(context[i]);
                }                                                            
            }
            //Checking if expression has letters
            for (i=0; i<expressionBlocks.Count;i++)
                if (Convert.ToChar(expressionBlocks[i].ToString()[0]) >= 'A' && Convert.ToChar(expressionBlocks[i].ToString()[0]) <= 'Z')                 
                    return;
            //Checking if expression contains only 1 number
            if (expressionBlocks.Count == 1)
            {
                cell.setContext(expressionBlocks[0].ToString()); //Setting this number as current cell context instead of link
                cell.setType("IsNonNegative"); //it can be Negative but for right evaluating it assigns cell type like NonNegative        
                return;
            }
            //Evaluating expression
            int result = 0;
            char operation;            
            result = Convert.ToInt32(expressionBlocks[0].ToString());
            for (i = 1; i < expressionBlocks.Count; i++)
            {
                operation = Convert.ToChar(expressionBlocks[i]);
                switch (operation)
                {
                    case '+': 
                        result = result + Convert.ToInt32(expressionBlocks[i+1].ToString());
                        i ++;
                        break;
                    case '-':
                        result = result - Convert.ToInt32(expressionBlocks[i+1].ToString());
                        i ++;
                        break;
                    case '*':
                        result = result * Convert.ToInt32(expressionBlocks[i+1].ToString());
                        i ++ ;
                        break;
                    case '/':
                        result = result / Convert.ToInt32(expressionBlocks[i+1].ToString());
                        i ++;
                        break;                                          
                }
                if (i == expressionBlocks.Count) break; //Checking for loop end
            }

            cell.setContext((Convert.ToInt32(result)).ToString());                
            cell.setType("IsNonNegative"); //it can be Negative but for right evaluating it assigns cell type like NonNegative                
             
        }

        public void cellTypeAnalize(Cell cell)
        {
            if (Regex.IsMatch(cell.getContext(), "^=([A-Z][1-9]|(0|[1-9][0-9]*))([+-/*]([A-Z][1-9]|(0|[1-9][0-9]*)))*$") == true)
            {
                cell.setType("IsMathExpression");
            }     
        }        
    }
}
