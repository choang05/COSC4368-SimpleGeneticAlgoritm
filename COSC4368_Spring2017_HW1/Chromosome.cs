using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Chromosome
{
    public string ChromosomeString
    {
        set;
        get;
    }

    public int CalculateFitness(FitnessDelegate fitnessFunction)
    {
        return fitnessFunction(ChromosomeString);
    }

    public static int SumStringCharacter(object chromosomestring)
    {
        int sum = 0; foreach (char c in (string)chromosomestring)
        {
            if (c == '1')
            {
                sum++;
            }
            else if (c == '0')
            {

            }
            else
            {
                //tODO:AddErrorCondition
            }
        }

        return sum;
    }

    public Chromosome()
    {
        Random r = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int16.MaxValue));
        this.ChromosomeString = String.Empty;
        for (int i = 0; i < 10; i++)
        {
            this.ChromosomeString += "" + r.Next() % 2;
        }
    }
}
