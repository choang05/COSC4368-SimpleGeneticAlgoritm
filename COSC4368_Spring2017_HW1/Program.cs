using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Delegates
public delegate int FitnessDelegate(object chromosomeRepresentation);
public delegate ChromosomePair SelectionDelegate();

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
    public static int chromosomeToSearch = 1010101010;

    public static void Main(string[] args)
    {
        Console.WriteLine("1st gen:");
        GenerationManager genManager = new GenerationManager();
        Chromosome[] generation = genManager.CurrentGen;

        for (int i = 0; i < generation.Length; i++)
        {
            Console.WriteLine(i+1 + ".\tChromosome: " + generation[i].ChromosomeString + "\tFitness: " + generation[i].CalculateFitness(Chromosome.SumStringCharacter));
        }

        Console.WriteLine("Pair Matches");

        List<ChromosomePair> pairs = new List<ChromosomePair>();
        foreach (Chromosome c in generation)
        {
            ChromosomePair pair = genManager.BasicSelection(); pairs.Add(pair);
            Console.WriteLine(pair.parent1.ChromosomeString + " " + pair.parent2.ChromosomeString);
        }

        Console.WriteLine("1st Generation Before mutation Childrens");

        List<Chromosome> nextgen = new List<Chromosome>();
        foreach (ChromosomePair p in pairs)
        {
            ChromosomePair pair = genManager.Crossover(p);
            if (pair.parent2 == null) { Console.WriteLine(pair.parent1.ChromosomeString); nextgen.Add(pair.parent1); }
            else
            {
                Console.WriteLine(pair.parent1.ChromosomeString + " " + pair.parent2.ChromosomeString);
                nextgen.Add(pair.parent1); nextgen.Add(pair.parent2);
            }
        }

        if (nextgen.Count > populationSize)
        {
            nextgen.RemoveRange(3, nextgen.Count - 1 - 3);
        }
        
        //  Perform mutations
        Console.WriteLine("First Generation After Mutation Childrens");
        for (int i = 0; i < nextgen.Count; i++)
        {
            nextgen[i] = genManager.Mutate(nextgen[i]);
            Console.WriteLine(nextgen[i].ChromosomeString);
        }

        genManager.PastGenerations.Add(genManager.CurrentGen);
        genManager.CurrentGen = nextgen.ToArray();
        Console.WriteLine("How many more generations would you like to iterate");

        int iterations = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < iterations; i++)
        {
            IterateGeneration(genManager);
        }

        Console.ReadLine();
    }

    public static void IterateGeneration(GenerationManager manager)
    {
        Chromosome[] Gen = manager.CurrentGen;

        foreach (Chromosome c in Gen)
        {
            Console.WriteLine(c.ChromosomeString + " " + c.CalculateFitness(Chromosome.SumStringCharacter));
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
            ChromosomePair pair = manager.Crossover(p);
            if (pair.parent2 == null)
            {
                Console.WriteLine(pair.parent1.ChromosomeString);
                nextgen.Add(pair.parent1);
            }
            else
            {
                Console.WriteLine(pair.parent1.ChromosomeString + " " + pair.parent2.ChromosomeString);
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

        manager.PastGenerations.Add(manager.CurrentGen);
        manager.CurrentGen = nextgen.ToArray();
    }
}
