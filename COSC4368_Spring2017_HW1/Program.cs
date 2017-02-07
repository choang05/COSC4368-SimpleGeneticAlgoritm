using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    //  User-assigned Variables
    public static int PopulationSize = 20;
    public static int ChromosomeBitLength = 10;
    public static string TargetChromosomeBits = "1010101010";
    public static int TargetFitnessValue = 10;
    public static int CrossoverPercentage = 70;

    public static void Main(string[] args)
    {
        //  Print initial conditions
        Console.WriteLine("[User-defined Parameters]" + "\n"
            + "Population size:\t\t" + PopulationSize + "\n"
            + "Chromosome Bit Length:\t\t" + ChromosomeBitLength + "\n"
            + "Target chromosome:\t\t" + TargetChromosomeBits + "\n"
            + "Target chromosome fitness:\t" + TargetFitnessValue + "\n"
            + "Crossover probability:\t\t" + CrossoverPercentage + "%\n");

        //  Initialize generation manager
        GenerationManager genManager = new GenerationManager();

        //  Print the generation stats
        PrintGeneration(genManager);

        genManager.MutateRandomChromosome();    //  Mutation test

        //genManager.CurrentGen[6].ChromosomeBits = targetChromosomeBits; // TEST

        //  Evaluate generation for chromosomes that match the target chromosome
        bool targetFound = false;
        int targetIndex = 0;
        for (int i = 0; i < genManager.CurrentGen.Length; i++)
        {
            //  If there exist a chromosome that matches our target chromosome OR has the target fitness or better, break from for loop.
            if (genManager.CurrentGen[i].ChromosomeBits.Equals(TargetChromosomeBits, StringComparison.OrdinalIgnoreCase)
                || genManager.CurrentGen[i].GetFitnessValue() >= TargetFitnessValue)
            {
                targetFound = true;
                targetIndex = i;
                break;
            }
        }

        //  If targetFound is true, stop the iteration and print results.
        if (targetFound)
        {
            Console.Write("\nTarget chromosome: " + TargetChromosomeBits + " has been found. Ending generation iterations...\n\n");
            Console.Write("[Results]" + "\n"
                + "Generations till target chromosome: \t" + genManager.GenerationCounter + "\n"
                + "Target chromosome's number:\t\t" + targetIndex + "\n"
                + "Target chromosome's fitness:\t\t" + genManager.CurrentGen[targetIndex].GetFitnessValue() + "\n");
        }
        //  else, continue iterating a new generation
        else
        {
            Console.Write("\nTarget chromosome: " + TargetChromosomeBits + " has not been found. Iterating a new generation...\n\n");
            // iterate
        }

        //  test crossover
        ChromosomePair parents;
        parents.parent1 = genManager.CurrentGen[0];
        parents.parent2 = genManager.CurrentGen[1];

        parents = genManager.CrossoverChromosomes(parents);

        /*Console.WriteLine("Pair Matches");

        List<ChromosomePair> pairs = new List<ChromosomePair>();
        foreach (Chromosome c in generation)
        {
            ChromosomePair pair = genManager.BasicSelection(); 
            pairs.Add(pair);
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
    
    //  Displays the stats for the current generation
    static void PrintGeneration(GenerationManager genManager)
    {
        Console.WriteLine("Generation: " + genManager.GenerationCounter + "\n");

        for (int i = 0; i < genManager.CurrentGen.Length; i++)
        {
            Console.WriteLine("\t" + "Chromosome " + (i) + ":\t" + genManager.CurrentGen[i].ChromosomeBits + "\t|\tFitness: " + genManager.CurrentGen[i].GetFitnessValue());
        }

        Console.WriteLine();    //  Create new line for neatness
    }
}
