using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GenerationManager
{
    int populationSize = 20;

    public Chromosome[] CurrentGen;
    public List<object> PastGenerations = new List<object>();
    public SelectionDelegate SelectionAlgorithm;
    public float CrossoverProbability = .50f;
    public float MutationProbability = .25f;

    public GenerationManager()
    {
        CurrentGen = new Chromosome[populationSize];
        for (int i = 0; i < populationSize; i++)
        {
            CurrentGen[i] = new Chromosome();
        }
    }

    public ChromosomePair BasicSelection()
    {
        ChromosomePair pair = new ChromosomePair();
        int currentHighest = -1;
        int secondHighest = -1;

        for (int i = 0; i < populationSize; i++)
        {
            Random r = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int16.MaxValue));
            int val = (CurrentGen[i].CalculateFitness(Chromosome.SumStringCharacter) + (r.Next() % 14));
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
    }
    public ChromosomePair Crossover(ChromosomePair pair)
    {
        ChromosomePair par = new ChromosomePair();
        Random random = new Random();
        //Random r = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int16.MaxValue));
        //bool doCrossover = (((r.Next() % 100) + 1) < CrossoverProbability);
        float crossoverChance = random.Next(0, 100) / 100;
        Console.WriteLine("Crossover chance: " + crossoverChance);
        if (crossoverChance < CrossoverProbability)
        {
            par.parent1 = GenerateChild(pair);
        }
        else
        {
            par.parent1 = pair.parent1;
            par.parent2 = pair.parent2;
        }

        return par;
    }
    private Chromosome GenerateChild(ChromosomePair pair)
    {
        Byte[] parentOneArray = getByteArrayFromString(pair.parent1.ChromosomeString);
        Byte[] parentTwoArray = getByteArrayFromString(pair.parent2.ChromosomeString);
        Chromosome ret = new Chromosome();

        for (int i = 0; i < parentOneArray.Length; i++)
        {
            parentOneArray[i] = (byte)(parentOneArray[i] | parentTwoArray[i]);
        }

        ret.ChromosomeString = getStringFromByteArray(parentOneArray); return ret;
    }
    private byte[] getByteArrayFromString(string p)
    {
        List<byte> ret = new List<byte>(); foreach (Char c in p) { if (c == '1') { ret.Add(1); } else { ret.Add(0); } }
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
    }

    public Chromosome Mutate(Chromosome entry)
    {
        //Random random = new Random();
        //Random r = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int16.MaxValue));
        //bool doMutation = (((r.Next() % 100)) < MutationProbability);
        //float mutationChance = random.Next(0, 100) / 100;
        //Console.WriteLine("mutation chance: " + mutationChance);
        Chromosome ret = new Chromosome();
        ret.ChromosomeString = entry.ChromosomeString;

        if (AttemptMutation(MutationProbability))
        {
            if (entry.ChromosomeString.IndexOf('0') >= 0)
            {
                byte[] tmp = getByteArrayFromString(entry.ChromosomeString);
                tmp[entry.ChromosomeString.IndexOf('0')] = 1;
                ret.ChromosomeString = getStringFromByteArray(tmp);
            }
        }

        return ret;
    }

    private bool AttemptMutation(float probability)
    {
        bool isSuccessful = false;

        Random random = new Random();



        return isSuccessful;
    }
    private bool AttemptCrossover(float probability)
    {
        bool isSuccessful = false;

        Random random = new Random();



        return isSuccessful;
    }
}


