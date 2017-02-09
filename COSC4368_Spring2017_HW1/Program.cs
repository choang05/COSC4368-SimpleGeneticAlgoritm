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
    public static float CrossoverPercentage = 0.00f;

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
            PrintGenerationResults(genManager, genManager.GenerationCounter-1);

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

            //  If targetFound is true, stop the iteration && print results
            if (targetFound)
            {
                Console.Write("Target chromosome: " + TargetChromosomeBits + " found. Ending generation iterations...\n");

                //  Print average fitness in each generation
                PrintCompletedResults(genManager, targetIndex);

                break;
            }
            //  else, continue iterating a new generation
            else
            {
                Console.Write("Target chromosome: " + TargetChromosomeBits + " not found. Iterating a new generation...\n");

                //  Iterate a new generation
                genManager.IterateCurrentGeneration();
            }
        }

        // Prevent the console from closing when execution is finished
        Console.ReadLine();
    }

    //  Displays the stats for the current generation
    #region PrintGeneration(GenerationManager genManager, int genNumber)
    static void PrintGenerationResults(GenerationManager genManager, int genNumber)
    {
        Console.Write("\n[Generation " + genManager.PastGenerations[genNumber].generationNumber +"]"+ "\n\n");

        for (int j = 0; j < genManager.PastGenerations[genNumber].chromosomes.Length; j++)
        {
            Console.WriteLine("\t" + "Chromosome " + (j) + ":\t" + genManager.PastGenerations[genNumber].chromosomes[j].ChromosomeBits + "\t|\tFitness: " + genManager.PastGenerations[genNumber].chromosomes[j].FitnessValue);
        }
        Console.Write("\n");
    }
    #endregion

    //  Print statistics on all generations
    #region PrintCompletedResults(GenerationManager genManager, int targetIndex)
    static void PrintCompletedResults(GenerationManager genManager, int targetIndex)
    {
        Console.Write("\n[Results]:" + "\n\n");
        for (int i = 0; i < genManager.PastGenerations.Count; i++)
        {
            Console.Write("\tGeneration " + genManager.PastGenerations[i].generationNumber
                        + "\t|\tAverage fitness: " + genManager.PastGenerations[i].averageFitness
                        + "\t|\tTotal fitness: " + genManager.PastGenerations[i].totalFitness
                        + "\t|\tHighest fitness: " + genManager.PastGenerations[i].highestFitness
                        + "\n");
        }
    }
    #endregion
}
