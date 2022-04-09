using System;
using System.Linq;
using Mapbox.Utils;

namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Calculates the given angle to a smoother value.
    /// </summary>
    public class AngleSmootherLowPas
    {
        private const double SMOOTHING_FACTOR = 0.5;
        private const double DEG_TO_RAD = Math.PI / 180.0d;
        private const double RAD_TO_DEG = 180.0d / Math.PI;
        
        private readonly int _measurements = 5;
        private readonly CircularBuffer<double> _angles;

        public AngleSmootherLowPas()
        {
            _angles = new CircularBuffer<double>(_measurements);
        }

        public AngleSmootherLowPas(int measurements)
        {
            _measurements = measurements;
            _angles = new CircularBuffer<double>(_measurements);

        }
        
        public void Add(double angle)
        {
            angle = angle < 0 ? angle + 360 : angle >= 360 ? angle - 360 : angle;
            _angles.Add(angle); 
        }

        public double Calculate()
        {
            double[] angles = _angles.Reverse().ToArray();
            double lastSin = Math.Sin(angles[0] * DEG_TO_RAD);
            double lastCos = Math.Cos(angles[0] * DEG_TO_RAD);
            
            for (int i = 1; i < angles.Length; i++)
            {
                double angle = angles[i];
                lastSin = SMOOTHING_FACTOR * Math.Sin(angle * DEG_TO_RAD) + (1 - SMOOTHING_FACTOR) * lastSin;
                lastCos = SMOOTHING_FACTOR * Math.Cos(angle * DEG_TO_RAD) + (1 - SMOOTHING_FACTOR) * lastCos;
            }
            
            double finalAngle = Math.Round(Math.Atan2(lastSin, lastCos) * RAD_TO_DEG, 2);
            finalAngle = finalAngle < 0 ? finalAngle + 360 : finalAngle >= 360 ? finalAngle - 360 : finalAngle;
            return finalAngle;
        }
    }
}
