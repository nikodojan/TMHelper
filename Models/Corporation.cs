using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMHelper.Models
{
    public class Corporation
    {

        public string PlayerName { get; set; }
        public int TotalPoints { get; set; }
        public int TerraformingRate { get; set; }
        public int Milestones { get; set; }
        public int Awards { get; set; }
        public int Greeneries { get; set; }
        public int Cities { get; set; }
        public int Cards { get; set; }
}
}