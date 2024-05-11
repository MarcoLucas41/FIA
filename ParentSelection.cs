using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Infrastructure.Framework.Texts;
using GeneticSharp.Runner.UnityApp.Car;
using UnityEngine;

public class ParentSelection : SelectionBase
{
    public ParentSelection() : base(2)
    {
    }

    protected override IList<IChromosome> PerformSelectChromosomes(int number, Generation generation)
    {

        IList<CarChromosome> population = generation.Chromosomes.Cast<CarChromosome>().ToList();
        IList<IChromosome> parents = new List<IChromosome>();

        /* YOUR CODE HERE */
        /*REPLACE THESE LINES BY YOUR PARENT SELECTION IMPLEMENTATION*/
        float sumFitness = 0;
        
        for(int i = 0; i < population.Count; i++)
        {
            sumFitness = sumFitness + population[i].Fitness;
        }
        
        int index,j = 0;
        float pointer,partial;
        
        while(j < number)
        {
            pointer = (float) RandomizationProvider.Current.GetDouble(0, 1);
            partial = 0;
            index = 0;
            while(partial <= pointer)
            {
                partial += population[index].Fitness/sumFitness;
                index++;
            }
            parents.Add(population[index-1]);
            j++;
        }
        
        /*END OF YOUR CODE*/

        return parents;
    }
}
