using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

public class Chromosome
{
    public string ChromosomeBits;
    public int FitnessValue = 0;

    //  Chromosome default constructor with given bit length
    public Chromosome()
    {
        this.ChromosomeBits = String.Empty;
    }

    //  Evaluate the fitness value by calculating all the 1's in odd indexes and 0's in even indexes
    #region CalculateFitnessValue()
    public void CalculateFitnessValue()
    {
        //  Reset fitness
        FitnessValue = 0;
        
        //  iterate through each chromosome bit and increase fitness value based on 1's and 0's bit index
        for (int i = 0; i < ChromosomeBits.Length; i++)
        {
            //  if the bit is a 1 and it's index is odd...
            if (ChromosomeBits[i] == '1' && IsOdd(i+1))
            {
                FitnessValue++;
            }
            //  if the bit is a 0 and it's index is even...
            else if (ChromosomeBits[i] == '0' && !IsOdd(i+1))
            {
                FitnessValue++;
            }
        }
    }
    #endregion

    //  Creates a random bit sequence for the chromosome
    #region GenerateRandomBits()
    public void GenerateRandomBits()
    {
        Random random = new Random();
        for (int i = 0; i < Program.ChromosomeBitLength; i++)
        {
            this.ChromosomeBits += "" + random.Next(0, 2);

            //  Since each instance of random is being generated at the same time, we need to sleep to avoid duplicate randoms
            Thread.Sleep(1);
        }
    }
    #endregion

    //  Returns boolean if the given value is odd
    #region IsOdd(int value)
    private bool IsOdd(int value)
    {
        return value % 2 != 0;
    }
    #endregion
}