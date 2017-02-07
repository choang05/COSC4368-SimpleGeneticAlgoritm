using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

public struct ChromosomePair
{
    public Chromosome parent1;
    public Chromosome parent2;
}

public class GenerationManager
{
    public Chromosome[] CurrentGen;
    public List<object> PastGenerations = new List<object>();
    public int GenerationCounter = 1;

    //  Default constructor
    public GenerationManager()
    {
        CurrentGen = new Chromosome[Program.populationSize];
        for (int i = 0; i < Program.populationSize; i++)
        {
            CurrentGen[i] = new Chromosome();
        }
    }

    /*public ChromosomePair BasicSelection()
    {
        ChromosomePair pair = new ChromosomePair();
        int currentHighest = -1;
        int secondHighest = -1;

        for (int i = 0; i < Program.populationSize; i++)
        {
            Random r = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int16.MaxValue));
            int val = (CurrentGen[i].GetFitnessValue() + (r.Next() % 14));
            if (val >= currentHighest)
            {
                pair.parent2 = pair.parent1;
                pair.parent1 = CurrentGen[i];
                secondHighest = currentHighest;
                currentHighest = val;
            }
        }

        if (secondHighest == -1)
        {
            return BasicSelection();
        }

        return pair;
    }*/

    //  Crossover chromosome parents using single-point crossover
    public ChromosomePair CrossoverChromosomes(ChromosomePair parents)
    {
        ChromosomePair newParents = parents;

        Console.Write("Parent 1:\t" + parents.parent1.ChromosomeBits + "\n");
        Console.Write("Parent 2:\t" + parents.parent2.ChromosomeBits + "\n\n");

        string parent1RightBits = parents.parent1.ChromosomeBits.Substring(parents.parent1.ChromosomeBits.Length / 2, parents.parent1.ChromosomeBits.Length / 2);
        string parent2RightBits = parents.parent2.ChromosomeBits.Substring(parents.parent2.ChromosomeBits.Length / 2, parents.parent2.ChromosomeBits.Length / 2);

        Console.Write("Parent 1 right bits:\t" + parent1RightBits + "\n");
        Console.Write("Parent 2 right bits:\t" + parent2RightBits + "\n\n");

        //  Swap the parent1's right half bits with parent2's right half bits
        var sb1 = new StringBuilder(parents.parent1.ChromosomeBits);
        sb1.Remove(parents.parent1.ChromosomeBits.Length / 2, parents.parent1.ChromosomeBits.Length / 2);
        sb1.Insert(parents.parent1.ChromosomeBits.Length / 2, parent2RightBits);
        newParents.parent1.ChromosomeBits = sb1.ToString();

        //  Swap the parent2's right half bits with parent1's right half bits
        var sb2 = new StringBuilder(parents.parent2.ChromosomeBits);
        sb2.Remove(parents.parent2.ChromosomeBits.Length / 2, parents.parent2.ChromosomeBits.Length / 2);
        sb2.Insert(parents.parent2.ChromosomeBits.Length / 2, parent1RightBits);
        newParents.parent2.ChromosomeBits = sb2.ToString();

        Console.Write("New parent 1:\t" + newParents.parent1.ChromosomeBits + "\n");
        Console.Write("New parent 2:\t" + newParents.parent2.ChromosomeBits + "\n");

        return newParents;
    }

    /*private Chromosome GenerateChild(ChromosomePair pair)
    {
        Byte[] parentOneArray = getByteArrayFromString(pair.parent1.ChromosomeBits);
        Byte[] parentTwoArray = getByteArrayFromString(pair.parent2.ChromosomeBits);
        Chromosome chromosome = new Chromosome();

        for (int i = 0; i < parentOneArray.Length; i++)
        {
            parentOneArray[i] = (byte)(parentOneArray[i] | parentTwoArray[i]);
        }

        chromosome.ChromosomeBits = getStringFromByteArray(parentOneArray);
        return chromosome;
    }

    private byte[] getByteArrayFromString(string p)
    {
        List<byte> ret = new List<byte>();
        foreach (Char c in p)
        {
            if (c == '1')
            {
                ret.Add(1);
            }
            else
            {
                ret.Add(0);
            }
        }
        return ret.ToArray();
    }

    private string getStringFromByteArray(byte[] p)
    {
        string a = string.Empty;

        foreach (byte c in p)
        {
            a += c;
        }

        return a;
    }*/

    //  Randomly selects a chromosome from the current generation and flips a random bit within the chromosome
    public void MutateRandomChromosome()
    {
        Random random = new Random();

        //  Select a random chromosome in the current generation
        int randomChromosomeIndex = random.Next(0, Program.populationSize);

        //Console.Write("Chromosome to mutate index:\t" + randomChromosomeIndex + "\n");

        //  Since each instance of random is being generated at the same time, we need to sleep to avoid duplicate randoms
        Thread.Sleep(1);

        Console.Write("Chromosome to mutate:\t" + CurrentGen[randomChromosomeIndex].ChromosomeBits + "\n");

        //  Select a random bit in the chromosome to flip
        int randomBitIndex = random.Next(0, Program.chromosomeBitLength);

        //  Since each instance of random is being generated at the same time, we need to sleep to avoid duplicate randoms
        Thread.Sleep(1);

        //Console.Write("Mutatated bit index:\t" + randomBitIndex + "\n");

        //  Flip bit in chromosome (mutate) with string replacment
        StringBuilder sb = new StringBuilder(CurrentGen[randomChromosomeIndex].ChromosomeBits);
        if (CurrentGen[randomChromosomeIndex].ChromosomeBits[randomBitIndex] == '1')
        {
            sb[randomBitIndex] = '0';
            //Console.Write("Flipped 1 to 0\t" + "\n");

        }
        else if (CurrentGen[randomChromosomeIndex].ChromosomeBits[randomBitIndex] == '0')
        {
            sb[randomBitIndex] = '1';
            //Console.Write("Flipped 0 to 1\t" + "\n");
        }
        CurrentGen[randomChromosomeIndex].ChromosomeBits = sb.ToString();

        Console.Write("New mutated chromosome:\t" + CurrentGen[randomChromosomeIndex].ChromosomeBits + "\n");
    }

    /*public void IterateGeneration()
    {
        Chromosome[] Gen = CurrentGen;

        foreach (Chromosome c in Gen)
        {
            Console.WriteLine(c.ChromosomeBits + " " + c.GetFitnessValue());
        }

        Console.WriteLine("Pair Matches"); List<ChromosomePair> pairs = new List<ChromosomePair>();

        foreach (Chromosome c in Gen)
        {
            ChromosomePair pair = manager.BasicSelection(); pairs.Add(pair);
            Console.WriteLine(pair.parent1.ChromosomeString + " " + pair.parent2.ChromosomeString);
        }

        Console.WriteLine("First Generation Before mutation Childrens");
        List<Chromosome> nextgen = new List<Chromosome>();
        foreach (ChromosomePair p in pairs)
        {
            ChromosomePair pair = Crossover(p);
            if (pair.parent2 == null)
            {
                Console.WriteLine(pair.parent1.ChromosomeBits);
                nextgen.Add(pair.parent1);
            }
            else
            {
                Console.WriteLine(pair.parent1.ChromosomeBits + " " + pair.parent2.ChromosomeBits);
                nextgen.Add(pair.parent1); nextgen.Add(pair.parent2);
            }
        }

        if (nextgen.Count > 4)
        {
            nextgen.RemoveRange(3, nextgen.Count - 1 - 3);
        }

        //Run mutations
        Console.WriteLine("First Generation After Mutation Childrens");

        for (int i = 0; i < nextgen.Count; i++)
        {
            nextgen[i] = manager.Mutate(nextgen[i]);
            Console.WriteLine(nextgen[i].ChromosomeString);
        }

        PastGenerations.Add(CurrentGen);
        CurrentGen = nextgen.ToArray();
    }*/
}


