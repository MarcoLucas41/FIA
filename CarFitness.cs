using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Chromosomes;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Linq;

namespace GeneticSharp.Runner.UnityApp.Car
{
    public class CarFitness : IFitness
    {
        public CarFitness()
        {
            ChromosomesToBeginEvaluation = new BlockingCollection<CarChromosome>();
            ChromosomesToEndEvaluation = new BlockingCollection<CarChromosome>();
        }

        public float FastestFitness(float Distance, float EllapsedTime, int RoadCompleted)
        {
            float fitness; 
            float reward = 0;

            // chegar com o minimo de Ellapsed time
            if(Distance == 0 || EllapsedTime == 0)
            {
                fitness = 0;
            }
            else fitness = Distance/EllapsedTime;

            // GapRoad Distance = 2280
            // HillRoad Distance = 4450
            // RockyHillRoad Distance = 2780

            float trackDistance = 2280;

            if(Distance >= trackDistance/4)
            {
                reward = (float)(reward + 0.1);
            }
            
            if(Distance >= trackDistance/2)
            {
                reward = (float)(reward + 0.15);
            }
            
            if(Distance >= 0.75*trackDistance)
            {
                reward = (float)(reward + 0.1);
            }

            // uma parte que valoriza chegar À meta/ completar uma corrida

            if(RoadCompleted == 1)
            {
                // DAR RECOMPENSA
                reward = (float)(reward + 0.3);
            }
            fitness = (float)(fitness + reward*fitness);  
            return fitness;  
        }

        public float EnergyEfficientFitness(float Distance, float SumTotalForces, float CarMass,int RoadCompleted)
        {
            float fitness; 
            float reward = 0;
            if(CarMass == 0 || SumTotalForces == 0)
            {
                fitness = 0;
            } 
            // a logica não está errada
            else fitness = (float) Distance/(SumTotalForces/CarMass);

            // GapRoad Distance = 2280
            // HillRoad Distance = 4450
            // RockyHillRoad Distance = 2780

            float trackDistance = 2780;

            if(Distance >= trackDistance/4)
            {
                reward = (float)(reward + 0.1);
            }
            
            if(Distance >= trackDistance/2)
            {
                reward = (float)(reward + 0.15);
            }
            
            if(Distance >= 0.75*trackDistance)
            {
                reward = (float)(reward + 0.1);
            }

            // uma parte que valoriza chegar À meta/ completar uma corrida

            if(RoadCompleted == 1)
            {
                // DAR RECOMPENSA
                reward = (float)(reward + 0.3);
            }
            fitness = (float)(fitness + reward*fitness);   
            return fitness;
        }



        public BlockingCollection<CarChromosome> ChromosomesToBeginEvaluation { get; private set; }
        public BlockingCollection<CarChromosome> ChromosomesToEndEvaluation { get; private set; }
        public double Evaluate(IChromosome chromosome)
        {
            var c = chromosome as CarChromosome;
            ChromosomesToBeginEvaluation.Add(c);

            float fitness = 0; 
            do
            {
                Thread.Sleep(1000);

                
                float Distance = c.Distance;
                float EllapsedTime = c.EllapsedTime;
                float NumberOfWheels = c.NumberOfWheels;
                float CarMass = c.CarMass;
                int RoadCompleted = c.RoadCompleted ? 1 : 0;

                List<float> Velocities = c.Velocities;
                float SumVelocities = c.SumVelocities;
                
                List<float> Accelerations = c.Accelerations;
                float SumAccelerations = c.SumAccelerations;

                List<float> Forces = c.Forces;
                float SumTotalForces = c.SumForces;

                /*YOUR CODE HERE*/
                /*Note que é executado ao longo da simulação*/

                // GapRoad Distance = 2280


                
                // chegar com o minimo de sumTotalForces

                //fitness = FastestFitness(Distance,EllapsedTime,RoadCompleted);
                fitness = EnergyEfficientFitness(Distance,SumTotalForces,CarMass,RoadCompleted);
                

                /*END OF YOUR CODE*/

                c.Fitness = fitness;

            } while (!c.Evaluated);

            ChromosomesToEndEvaluation.Add(c);


            do
            {
                Thread.Sleep(1000);
            } while (!c.Evaluated);

            /*O valor da variável fitness é o valor de aptidão do indivíduo*/

            return fitness;
        }

    }
}