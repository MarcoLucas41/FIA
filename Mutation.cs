using System;
using System.Diagnostics;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Runner.UnityApp.Car;
using UnityEngine;

public class Mutation : IMutation
{
    public bool IsOrdered { get; private set; } // indicating whether the operator is ordered (if can keep the chromosome order).
    private CarSampleConfig m_config;

    public Mutation()
    {
        IsOrdered = true;
    }

     public static double Gaussian(double x, double mu, double sigma)
    {
        return 1 / (Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-0.5 * Math.Pow((x - mu) / sigma, 2));
    }

    public void Mutate(IChromosome chromosome, float probability)
    {
        
        /* YOUR CODE HERE */
        /*REPLACE THESE LINES BY YOUR MUTATION IMPLEMENTATION*/

        // The mutation process should involve the low probability of a change on the float value on a random gene
        // Therefore, the index and the new value of the gene should be randomized, based on the probability passed 
        // as function arg


        // String geneInfo = "";
        // foreach (Gene g in chromosome.GetGenes()) {
        //     geneInfo += " '" + g.Value.ToString() + "' ";
        // }

        // UnityEngine.Debug.Log("HERE");
        // UnityEngine.Debug.Log(geneInfo);
        int numberOfGenes = chromosome.GetGenes().Length;
        for(int i = 0; i < numberOfGenes; i++)
        {
            float k = (float) RandomizationProvider.Current.GetDouble(0, 1);
            if (k <= probability) 
            {
                //double valueToBeMutated = (double)chromosome.GetGene(i).Value;
                // gerar um novo veículo e buscar o mesmo gene nesse indice
                UnityEngine.Debug.Log("MUTATION");
                CarChromosome newChromossome = new CarChromosome(((CarChromosome)chromosome).getConfig());
                Gene newGene = newChromossome.GetGene(i);
                chromosome.ReplaceGene(i,newGene);
                //valueToBeMutated = RandomizationProvider.Current.GetDouble(GeneticAlgorithmConfigurations.minGeneValue, GeneticAlgorithmConfigurations.maxGeneValue);
                //valueToBeMutated = Gaussian(valueToBeMutated, 0, GeneticAlgorithmConfigurations.sigmaMutation);
                //chromosome.ReplaceGene(i, new Gene(valueToBeMutated));
            }
        }
     
      

        

        // Gene newGene = newChromosome.GetGene(geneIndex);
        // float geneValue = (float) newGene.Value;
        // geneValue = geneValue + 1;
        // chromosome.ReplaceGene(geneIndex, new Gene(geneValue));
        
        // UnityEngine.Debug.Log("REPLACED " + geneIndex + " WITH " + geneValue);
        // UnityEngine.Debug.Log(geneInfo);
        /*END OF YOUR CODE*/
    }

}
