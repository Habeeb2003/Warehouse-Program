using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Program
{
    
    class Warehouse
    {

        static void Main(string[] args){
                
				//	 ask user to input state
                Console.Write("Enter state: ");
                string? state = Console.ReadLine();
                //	 ask user to input sku no
                Console.Write("Enter sku no: ");
                string? sku = Console.ReadLine();

				//	total no of stock field
                int totalNo = 0;

				//	file path
                string path = "warehouses (1)(1).csv";
                
                // reading csv file 
                string[] lines = File.ReadAllLines(path);

				// getting the csv headers
                string[] headers = lines[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				// LINQ to querry the file
                IEnumerable<int[]> q = from line in lines.Skip(1) // skiped headers
                        where line.Contains(state!)	//	filter lines by checking if line contains the state
                        // split line into an array and get the index of state in it
                        select line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().Select((element ,index) => (element == state) ? index : -1).Where(index => index != -1).ToArray();	

                foreach (int[] indexes in q)
                {
                	// add number of stocks to totalNo fields
                    foreach (var index in indexes)
                    {
                        // Console.WriteLine(index);
                        totalNo += getNoOfStocks(headers[index], sku!);
                    }
                    // Console.WriteLine("Hi");
                    
                }

                Console.WriteLine("Total no of stocks = {0}", totalNo);
                Console.ReadLine();

        }

        public static int getNoOfStocks(string warehouseName, string skuNo){
        		//	file path
        		string path = "warehouses-with-skus (1)(1).csv";
        		//	read all lines in the file into an array of string
        		string[] lines = File.ReadAllLines(path);
        		// file headers
        		string[] headers = lines[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				
				// LINQ to querry the file
                IEnumerable<string> q = from line in lines.Skip(1)
                		// filter by checking if the first column is equal to the provided skuNo
                        where line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0] == skuNo
                        // select the value under the warehouse column
                        select line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ElementAt(headers.ToList().IndexOf(warehouseName));

                foreach (string stockNo in q)
                {
                    Console.WriteLine(stockNo);
                	// return stock no to main
                    return Convert.ToInt32(stockNo);
                }

	        	return 0;
        	}
    }
}
