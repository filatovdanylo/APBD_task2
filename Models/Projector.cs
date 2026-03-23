using APBD_TASK2.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD_TASK2.Models
{
    public class Projector : Equipment
    {
        public Resolution Resolution { get; set; }

        public int BrightnessLumens { get; set; }

        public Projector(string name, Resolution resolution, int brightnessLumens) 
            : base(name)
        {
            Resolution = resolution;
            BrightnessLumens = brightnessLumens;
        }
    }
}
