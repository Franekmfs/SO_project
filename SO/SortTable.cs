using System;

namespace SO
{
    public class SortTable
    {
        public int[,] SortBy(int[,] processData, string by)
        {
            int type = 0;
            if (by == "arrival")
            {
                type = 1;
            }
            if (by == "duration")
            {
                type = 2;
            }
            if (by == "priority")
            {
                type = 3;
            }
            
            int[] temp = new int[4];
            for (int i = 0; i < processData.GetLength(0) - 2; i++)
            {
                for (int j = 0; j < processData.GetLength(0) - 2; j++){
				
                    if (processData[j, type] > processData[j + 1, type]){
                        for (int k = 0; k < 4; k++)
                        {
                            temp[k] = processData[j, k];
                            processData[j, k] = processData[j + 1, k];
                            processData[j + 1, k] = temp[k];
                        }
                    }
                }
            }
            return processData;
        }

        public void DisplayTable(int[,] processData)
        {
            
            for (int i = 0; i < processData.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(processData[i,j] + "\t");
                }
                Console.WriteLine(" ");
            }
            
        }
    }
}
