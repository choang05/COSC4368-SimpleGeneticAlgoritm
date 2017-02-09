using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

//  Struct that contains two chromosome parents. Used for crossovers
public struct ChromosomePair
{
    public Chromosome parent1;
    public Chromosome parent2;
}

//  Strut that contains generation related data such as average fitness and chromosomes
public struct GenerationData
{
    public Chromosome[] chromosomes;
    public int generationNumber;
    public int highestFitness;
    public float averageFitness;
    public int totalFitness;
}

public class GenerationManager
{
    public Chromosome[] CurrentGen;
    public List<GenerationData> PastGenerations = new List<GenerationData>();
    public int GenerationCounter = 1;

    //  Default constructor
    #region GenerationManager()
    public GenerationManager()
    {
        CurrentGen = new Chromosome[Program.PopulationSize];
        for (int i = 0; i < Program.PopulationSize; i++)
        {
            //  Initialize chromosome objects
            CurrentGen[i] = new Chromosome();

            //  Generate initial chromosomes with random bits and determine its fitness value
            CurrentGen[i].GenerateRandomBits();
            CurrentGen[i].CalculateFitnessValue();
        }
    }
    #endregion

    //  Creates a GenerationData structure that stores information of the generation such as chromosomes and average fitness
    #region ArchiveCurrentGeneration()
    public void ArchiveCurrentGeneration()
    {
        //  Create a generation structure to hold all data related to the generation
        GenerationData generation = new GenerationData();
        generation.chromosomes = CurrentGen;
        generation.generationNumber = GenerationCounter;

        //  Calculate average fitness
        int totalFitness = 0;
        for (int i = 0; i < Program.PopulationSize; i++)
            totalFitness += CurrentGen[i].FitnessValue;
        generation.averageFitness = (float)totalFitness / (float)Program.PopulationSize;

        //  Calculate highest fitness
        int currentHighestFitness = 0;
        for (int i = 0; i < Program.PopulationSize; i++)
        {
            int chromosomeFitnessValue = CurrentGen[i].FitnessValue;
            if (chromosomeFitnessValue > currentHighestFitness)
                currentHighestFitness = chromosomeFitnessValue;
        }
        generation.highestFitness = currentHighestFitness;

        //  Calculate total fitness
        foreach (var chromosome in CurrentGen)
            generation.totalFitness += chromosome.FitnessValue;

        //  Add current generation into past generation list
        PastGenerations.Add(generation);
    }
    #endregion

    //  Iterate the current generation by generating a new population
    #region IterateCurrentGeneration()
    public void IterateCurrentGeneration()
    {
        //  Selection - using crossoverPercentage and weighted probabilty, select chromosomes to crossover and replicate
        #region Selection process
        //  Determine crossover and replication size based on crossoverPercentage
        int amountToCrossover = (int)Math.Round(Program.CrossoverPercentage * Program.PopulationSize);
        
        //  Create a list that maps weight index to the chromosome. Index is a section of the chromosome's fitness
        List<Chromosome> weightMap = new List<Chromosome>();
        for (int i = 0; i < CurrentGen.Length; i++)
            for (int j = 0; j < CurrentGen[i].FitnessValue; j++)
                weightMap.Add(CurrentGen[i]);

        //  List that contains the chromosomes that will be crossingover
        List<Chromosome> chromosomesToCrossover = new List<Chromosome>();
        //  be default, replication list is the current generation but elements will be transferred to the crossover list during weighted selection
        List<Chromosome> chromosomeToReplicate = CurrentGen.ToList();

        //  Randomly select a number to match the number to the chromosome to the weight map then add the chromosome to the crossover list
        Random random = new Random();
        for (int i = 0; i < amountToCrossover; i++)
        {
            //  while loop prevents selecting a chromosome already existing in the crossover list
            int randomIndex = 0;
            do
            {
                //  Select random index from weight map
                randomIndex = random.Next(0, weightMap.Count);
                Thread.Sleep(1);    //  Since each instance of random is being generated at the same time, we need to sleep to avoid duplicate randoms
            } while (chromosomesToCrossover.Contains(weightMap[randomIndex]));

            //  Add chromosome of that random index into the crossover list && remove the chromosome from the replication list
            chromosomesToCrossover.Add(weightMap[randomIndex]);
            chromosomeToReplicate.Remove(weightMap[randomIndex]);
        }

        //  If the crossoverChromosome list is odd in the case of an odd population size... 
        //      transfer one chromosomeToReplicate to the crossoverChromosome list so there is a even pairing number
        if (chromosomesToCrossover.Count % 2  != 0) 
        {
            chromosomesToCrossover.Add(chromosomeToReplicate[0]);
            chromosomeToReplicate.RemoveAt(0);
        }
        #endregion

        //  Crossover - perform and cache the crossover given chromosomesToCrossover list
        List<Chromosome> crossoverChromosomes = CrossoverChromosomes(chromosomesToCrossover);
        
        //  Update the current generation with the newly created crossoverChromosomes list and replication list
        CurrentGen = crossoverChromosomes.Concat(chromosomeToReplicate).ToArray();

        //  Mutation - Mutate a random chromosome in the current generation
        MutateRandomChromosome();

        //  Re-Evaluate all chromosomes fitness value
        foreach (Chromosome chromosome in CurrentGen)
            chromosome.CalculateFitnessValue();

        //  Increment generation number
        GenerationCounter++;
    }
    #endregion

    //  Crossover all chromosomes in given list
    #region CrossoverChromosomes(List<Chromosome> chromosomesToCrossover)
    public List<Chromosome> CrossoverChromosomes(List<Chromosome> chromosomesToCrossover)
    {
        //  Create a return list to hold the crossovered chromosomes
        List<Chromosome> crossoverChromosomes = new List<Chromosome>();

        //  Create a list of avaliable chromosomes for crossing over
        List<Chromosome> avaliableToCrossover = new List<Chromosome>();
        avaliableToCrossover = chromosomesToCrossover;

        //  Loop and crossover parents from the avaliableToCrossover list until there are no more parents to cross over
        Random random = new Random();
        while (avaliableToCrossover.Count > 0)
        {
            //  Create parent pair from randomly select parents, remove them from avaliable list, and perform crossover
            ChromosomePair parents;

            parents.parent1 = avaliableToCrossover[random.Next(0, avaliableToCrossover.Count)];
            avaliableToCrossover.Remove(parents.parent1);
            Thread.Sleep(1);    //  Since each instance of random is being generated at the same time, we need to sleep to avoid duplicate randoms

            parents.parent2 = avaliableToCrossover[random.Next(0, avaliableToCrossover.Count)];
            avaliableToCrossover.Remove(parents.parent2);
            Thread.Sleep(1);    //  Since each instance of random is being generated at the same time, we need to sleep to avoid duplicate randoms

            ChromosomePair children = CrossoverChromosomePair(parents);

            //  add crossover chromosome children to the crossoverChromosome list
            crossoverChromosomes.Add(children.parent1);
            crossoverChromosomes.Add(children.parent2);
        }

        return crossoverChromosomes;
    }
    #endregion

    //  Crossover chromosome parents using single-point crossover
    #region CrossoverChromosomePair(ChromosomePair parents)
    public ChromosomePair CrossoverChromosomePair(ChromosomePair parents)
    {
        ChromosomePair newParents = parents;

        //Console.Write("Parent 1:\t" + parents.parent1.ChromosomeBits + "\n");
        //Console.Write("Parent 2:\t" + parents.parent2.ChromosomeBits + "\n\n");

        string parent1RightBits = parents.parent1.ChromosomeBits.Substring(parents.parent1.ChromosomeBits.Length / 2, parents.parent1.ChromosomeBits.Length / 2);
        string parent2RightBits = parents.parent2.ChromosomeBits.Substring(parents.parent2.ChromosomeBits.Length / 2, parents.parent2.ChromosomeBits.Length / 2);

        //Console.Write("Parent 1 right bits:\t" + parent1RightBits + "\n");
        //Console.Write("Parent 2 right bits:\t" + parent2RightBits + "\n\n");

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

        //Console.Write("New parent 1:\t" + newParents.parent1.ChromosomeBits + "\n");
        //Console.Write("New parent 2:\t" + newParents.parent2.ChromosomeBits + "\n");

        return newParents;
    }
    #endregion

    //  Randomly selects a chromosome from the current generation and flips a random bit within the chromosome
    #region MutateRandomChromosome()
    public void MutateRandomChromosome()
    {
        Random random = new Random();

        //  Select a random chromosome in the current generation
        int randomChromosomeIndex = random.Next(0, Program.PopulationSize);

        //Console.Write("Chromosome to mutate index:\t" + randomChromosomeIndex + "\n");

        //  Since each instance of random is being generated at the same time, we need to sleep to avoid duplicate randoms
        Thread.Sleep(1);

        //Console.Write("Chromosome to mutate:\t" + CurrentGen[randomChromosomeIndex].ChromosomeBits + "\n");

        //  Select a random bit in the chromosome to flip
        int randomBitIndex = random.Next(0, Program.ChromosomeBitLength);

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

        //Console.Write("New mutated chromosome:\t" + CurrentGen[randomChromosomeIndex].ChromosomeBits + "\n");
    }
    #endregion
}


