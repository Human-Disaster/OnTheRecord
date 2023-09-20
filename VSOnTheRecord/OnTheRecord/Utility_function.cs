using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !SERVER
using UnityEngine;
#endif

namespace OnTheRecord
{
    public static class Checkrand
    {
#if SERVER
        private static Random? _instance = null;
#endif

        public static float Checkrand_float(float min, float max)
        {
#if !SERVER
            return Random.Range(min, max);
#else
            if (_instance == null) _instance = new Random();
            return _instance.NextSingle() * (max - min) + min;
#endif
        }

        public static int Checkrand_int(int min, int max)
        {
#if !SERVER
            return Random.Range(min, max);
#else
            if (_instance == null) _instance = new Random();
            return _instance.Next(min, max);
#endif
        }
    }
}
