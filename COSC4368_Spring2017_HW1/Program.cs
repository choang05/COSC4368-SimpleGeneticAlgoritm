using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct ChromosomePair
{
    public Chromosome parent1;
    public Chromosome parent2;
}

class Program
{
    //  User-assigned Variables
    public static int populationSize = 20;
    public static int chromosomeBitLength = 10;
    public static int targetChromosome = 1010101010;
    public static int crossoverProbability = 70;

    private static int generationCounter = 1;

    public static void Main(string[] args)
    {
        Console.WriteLine("Population size:\t" + populationSize + "\n"
            + "Chromosome Bit Length:\t" + chromosomeBitLength + "\n"
            + "Target chromosome:\t" + targetChromosome + "\n"
            + "Crossover probability:\t" + crossoverProbability + "%\n");

        Console.WriteLine("Generation: " + generationCounter + "\n");
        GenerationManager genManager = new GenerationManager();
        Chromosome[] generation = genManager.CurrentGen;

        for (int i = 0; i < generation.Length; i++)
        {
            Console.WriteLine("\t" + "Chromosome " + (i) + ":\t" + generation[i].ChromosomeBits + "\t|\tFitness: " + generation[i].GetFitnessValue());
        }

        Console.WriteLine();    //  Create new line for neatness

        genManager.MutateRandomChromosome();

        /*Console.WriteLine("Pair Matches");

        List<ChromosomePair> pairs = new List<ChromosomePair>();
        foreach (Chromosome c in generation)
        {
            ChromosomePair pair = genManager.BasicSelection(); pairs.Add(pair);
            Console.WriteLine(pair.parent1.ChromosomeBits + " " + pair.parent2.ChromosomeBits);
        }*/

        /*Console.WriteLine("1st Generation Before mutation Childrens");

        List<Chromosome> nextgen = new List<Chromosome>();
        foreach (ChromosomePair p in pairs)
        {
            ChromosomePair pair = genManager.Crossover(p);
            if (pair.parent2 == null) { Console.WriteLine(pair.parent1.ChromosomeBits); nextgen.Add(pair.parent1); }
            else
            {
                Console.WriteLine(pair.parent1.ChromosomeBits + " " + pair.parent2.ChromosomeBits);
                nextgen.Add(pair.parent1); nextgen.Add(pair.parent2);
            }
        }

        if (nextgen.Count > populationSize)
        {
            nextgen.RemoveRange(3, nextgen.Count - 1 - 3);
        }*/
        
        //  Perform mutations
        /*Console.WriteLine("First Generation After Mutation Childrens");
        for (int i = 0; i < nextgen.Count; i++)
        {
            nextgen[i] = genManager.Mutate(nextgen[i]);
            Console.WriteLine(nextgen[i].ChromosomeBits);
        }

        genManager.PastGenerations.Add(genManager.CurrentGen);
        genManager.CurrentGen = nextgen.ToArray();
        Console.WriteLine("How many more generations would you like to iterate");

        int iterations = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < iterations; i++)
        {
            genManager.IterateGeneration();
        }*/

        Console.ReadLine();
    }
}
