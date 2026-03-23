using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_TASK2.Models
{
    public class Camera : Equipment
    {
        public int ResolutionMegapixels { get; set; }
        public int WeightGrams { get; set; }
        public bool CanShootVideo { get; set; }

        public Camera(string name, int resolutionMegapixels, int weightGrams, bool canShootVideo) 
            : base(name)
        {
            ResolutionMegapixels = resolutionMegapixels;
            WeightGrams = weightGrams;
            CanShootVideo = canShootVideo;
        }
    }
}
