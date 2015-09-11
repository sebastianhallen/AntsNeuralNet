using System.Drawing;

namespace AntsNeuralNet.Core
{
    public interface IAntPainter
    {
        void DrawUberAnt(PointF p);
        void DrawAnt(PointF p);
        void DrawFood(PointF p);
    }
}