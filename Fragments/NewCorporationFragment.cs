using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DialogFragment = Android.Support.V4.App.DialogFragment;
using Fragment = Android.Support.V4.App.Fragment;

namespace TMHelper.Fragments
{
    public class NewCorporationFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.newCorporation_activity, container, false);
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}