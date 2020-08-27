using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth
{
    public class Clock
    {
        public TimeSpan TotalTime { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        public int FPS { get; protected set; }
        public int FPSUpdateRate { get; set; } = 1000;

        private double _FPSAccumulator;
        private int _FPSElapsedFrames;
        private Stopwatch _stopWatch;

        public Clock()
        {
            _stopWatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Called every frame to update the clock.
        /// </summary>
        public void Update()
        {
            ElapsedTime = _stopWatch.Elapsed - TotalTime;
            TotalTime = _stopWatch.Elapsed;

            _FPSElapsedFrames++;
            _FPSAccumulator += ElapsedTime.TotalMilliseconds;

            if(_FPSAccumulator > FPSUpdateRate)
            {
                FPS = (int)((1000 / _FPSAccumulator) * _FPSElapsedFrames);
            }
        }
    }
}
