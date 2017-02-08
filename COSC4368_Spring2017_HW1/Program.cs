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
    public static float CrossoverPercentage = .70f;

    public static void Main(string[] args)
    {
        //  Print initial conditions
        Console.WriteLine("[User-defined Parameters]" + "\n"
            + "Population size:\t\t" + PopulationSize + "\n"
            + "Chromosome Bit Length:\t\t" + ChromosomeBitLength + "\n"
            + "Target chromosome:\t\t" + TargetChromosomeBits + "\n"
            + "Target chromosome fitness:\t" + TargetFitnessValue + "\n"
            + "Crossover probability:\t\t" + CrossoverPercentage*100 + "%\n");

        //  Initialize generation manager
        GenerationManager genManager = new GenerationManager();

        //  Loop generation iteration until target is found
        bool targetFound = false;
        while (!targetFound)
        //for (int k = 0; k < 3; k++)
        {
            //  Archive the current generationC
            genManager.ArchiveCurrentGeneration();

            //  Print the generation stats
            PrintGeneration(genManager, genManager.GenerationCounter-1);

            //  Evaluate generation for chromosomes that match the target chromosome
            int targetIndex = 0;
            for (int i = 0; i < genManager.CurrentGen.Length; i++)
            {
                //  If there exist a chromosome that matches our target chromosome OR has the target fitness or better, break from for loop.
                if (genManager.CurrentGen[i].ChromosomeBits.Equals(TargetChromosomeBits, StringComparison.OrdinalIgnoreCase)
                    || genManager.CurrentGen[i].FitnessValue >= TargetFitnessValue)
                {
                    targetFound = true;
                    targetIndex = i;
                    break;
                }
            }

            //  If targetFound is true, stop the iteration and print results.
            if (targetFound)
            {
                Console.Write("Target chromosome: " + TargetChromosomeBits + " has been found. Ending generation iterations...\n\n");
                Console.Write("[Results]" + "\n"
                    + "Generations till target chromosome: \t" + genManager.GenerationCounter + "\n"
                    + "Target chromosome's number:\t\t" + targetIndex + "\n"
                    + "Target chromosome's fitness:\t\t" + genManager.CurrentGen[targetIndex].FitnessValue + "\n");
                break;
            }
            //  else, continue iterating a new generation
            else
            {
                Console.Write("Target chromosome: " + TargetChromosomeBits + " has not been found. Iterating a new generation...\n\n");

                //  Iterate a new generation
                genManager.IterateCurrentGeneration();
            }
        }

        // Prevent the console from closing when execution is finished
        Console.ReadLine();
    }

    //  Displays the stats for the current generation
    #region PrintGeneration(GenerationManager genManager, int genNumber)
    static void PrintGeneration(GenerationManager genManager, int genNumber)
    {
        Console.Write("Generation: " + genManager.PastGenerations[genNumber].generationNumber + "\n\n");
        for (int j = 0; j < genManager.PastGenerations[genNumber].Chromosomes.Length; j++)
        {
            Console.WriteLine("\t" + "Chromosome " + (j) + ":\t" + genManager.PastGenerations[genNumber].Chromosomes[j].ChromosomeBits + "\t|\tFitness: " + genManager.PastGenerations[genNumber].Chromosomes[j].FitnessValue);
        }
        Console.Write("\nAverage fitness: " + genManager.PastGenerations[genNumber].averageFitness + "\n");
        Console.Write("Highest fitness: " + genManager.PastGenerations[genNumber].highestFitness + "\n");
        Console.Write("Total fitness: " + genManager.PastGenerations[genNumber].totalFitness + "\n\n");
    }
    #endregion
}
