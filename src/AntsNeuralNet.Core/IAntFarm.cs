namespace AntsNeuralNet.Core
{
    public interface IAntFarm
    {
        void Start();
        void Suspend();
        void Update();
        void Draw(IAntPainter painter);
        
        bool FastRender { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        int BestAnt { get; }
    }
}
