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
using Android.Support.V7.App;

namespace TMHelper.Activities
{
    [Activity(Label = "AboutActivity", Theme = "@style/AppTheme", MainLauncher = false)]
    public class AboutActivity : AppCompatActivity
    {
        private Button closeButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.aboutActivity_layout);
            closeButton = (Button) FindViewById(Resource.Id.closeButton);
            closeButton.Click += CloseButton_Click;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}