using System;
using System.Collections.Generic;

namespace AntsNeuralNet.Core
{
    public interface IAntFarm
    {
        void Start();
        void Suspend();
        void Update();

        //IEnumerable<Object> Ants { get; }
        //IEnumerable<Object> Food { get; }
        //bool FastRender { get; }
        //Object Genome { get; }
        //int Width { get; }
        //int Height { get; }
        //int BestAnt { get; }
        //int BestScore { get; }
    }
}
