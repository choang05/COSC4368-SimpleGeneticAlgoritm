using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

public class Chromosome
{
    public string ChromosomeString
    {
        set;
        get;
    }

    //  Evaluate the fitness value by calculating all the 1's in odd indexes and 0's in even indexes
    public int GetFitnessValue()
    {
        int fitnessValue = 0;

        for (int i = 0; i < ChromosomeString.Length; i++)
        {
            if (ChromosomeString[i] == '1' && IsOdd(i+1))
            {
                fitnessValue++;
            }
            else if (ChromosomeString[i] == '0' && !IsOdd(i+1))
            {
                fitnessValue++;
            }
        }

        return fitnessValue;
    }

    //  Chromosome default constructor with given bit length
    public Chromosome()
    {
        this.ChromosomeString = String.Empty;

        //  Generate random bit 10 times
        Random random = new Random();
        for (int i = 0; i < Program.chromosomeBitLength; i++)
        {
            this.ChromosomeString += "" + random.Next(0, 2);

            //  Since each instance of random is being generated at the same time, we need to sleep each instance to avoid duplicate randoms
            Thread.Sleep(1);
        }
    }

    //  Returns boolean if the given value is odd
    public static bool IsOdd(int value)
    {
        return value % 2 != 0;
    }
}