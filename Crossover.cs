using GeneticSharp.Domain.Chromosomes;
using System;
using System.Linq;
using UnityEngine;
using GeneticSharp.Domain.Randomizations;
using System.Collections.Generic;
using GeneticSharp.Domain.Crossovers;

namespace GeneticSharp.Runner.UnityApp.Commons
{
    public class Crossover : ICrossover
    {
        public int ParentsNumber { get; private set; }

        public int ChildrenNumber { get; private set; }

        public int MinChromosomeLength { get; private set; }

        public bool IsOrdered { get; private set; } // indicating whether the operator is ordered (if can keep the chromosome order).

        protected float crossoverProbability;


        public Crossover(float crossoverProbability) : this(2, 2, 2, true)
        {
            this.crossoverProbability = crossoverProbability;
        }

        public Crossover(int parentsNumber, int offSpringNumber, int minChromosomeLength, bool isOrdered)
        {
            ParentsNumber = parentsNumber;
            ChildrenNumber = offSpringNumber;
            MinChromosomeLength = minChromosomeLength;
            IsOrdered = isOrdered;
        }

        public IList<IChromosome> Cross(IList<IChromosome> parents)
        {
            IChromosome parent1 = parents[0];
            IChromosome parent2 = parents[1];
            IChromosome offspring1 = parent1.Clone();
            IChromosome offspring2 = parent2.Clone();

            /* YOUR CODE HERE */
            /*REPLACE THESE LINES BY YOUR CROSSOVER IMPLEMENTATION*/

            // The crossover method implemented is based on the normal biological way of reproduction: an offspring shall possess
            // 2 halfs of genetic material, each from the 2 parents.

            // the halfs can be of different sizes
            
            
            Gene[] parent2Genes= parent2.GetGenes().ToArray();
            Gene[] parent1Genes = parent1.GetGenes().ToArray();

            int index1 =  RandomizationProvider.Current.GetInt(0,parent1Genes.Length);
            int index2 =  RandomizationProvider.Current.GetInt(0,parent2Genes.Length);
            /*
            Gene[] parent2GenesHalf1 = parent2Genes.Take(parent2Genes.Length / 2).ToArray();
            Gene[] parent2GenesHalf2 = parent2Genes.Skip(parent2Genes.Length / 2).ToArray();

            Gene[] parent1GenesHalf1 = parent1Genes.Take(parent1Genes.Length / 2).ToArray();
            Gene[] parent1GenesHalf2 = parent1Genes.Skip(parent1Genes.Length / 2).ToArray();
            */

            Gene[] parent2GenesHalf1 = parent2Genes.Take(index2).ToArray();
            Gene[] parent1GenesHalf2 = parent1Genes.Skip(index2).ToArray();

            Gene[] parent1GenesHalf1 = parent1Genes.Take(index1).ToArray();
            Gene[] parent2GenesHalf2 = parent2Genes.Skip(index1).ToArray();

            offspring1.ReplaceGenes(0, parent2GenesHalf1);
            offspring1.ReplaceGenes(parent2GenesHalf1.Length, parent1GenesHalf2);

            offspring2.ReplaceGenes(0, parent1GenesHalf1);
            offspring2.ReplaceGenes(parent1GenesHalf1.Length, parent2GenesHalf2);
            
            
            /*
            float k = (float) RandomizationProvider.Current.GetDouble(0, 1);
            if(k <= crossoverProbability)
            {
                for(int i = 0; i < parent1.Length; i++)
                {
                    if ((float) RandomizationProvider.Current.GetDouble(0, 1) >= 0.5)
                    {
                        offspring1.ReplaceGene(i,parent2.GetGene(i));
                        offspring2.ReplaceGene(i,parent1.GetGene(i));
                    }
                }
            }
            */
        
            /*END OF YOUR CODE*/

            return new List<IChromosome> { offspring1, offspring2 };
            
        }
    }
}