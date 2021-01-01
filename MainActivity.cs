using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Support.V7.Widget;
using TMHelper.Activities;
using TMHelper.Adapters;
using TMHelper.Models;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using TMHelper.Fragments;

namespace TMHelper
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : Android.Support.V4.App.FragmentActivity
    {
        private BottomNavigationView _bottomNavigation;
        private ViewPager _viewPager;
        private Android.Support.V4.App.Fragment[] _fragments;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            InitializeFragments();
            ViewPagerAdapter adapter = new ViewPagerAdapter(SupportFragmentManager, _fragments);
            _viewPager = (ViewPager)FindViewById(Resource.Id.viewPager1);
            _viewPager.Adapter = adapter;

            //Views
            _bottomNavigation = (BottomNavigationView)FindViewById(Resource.Id.bottom_navigation);

            _bottomNavigation.NavigationItemSelected += NavigationView_NavigationItemSelected;
            _viewPager.PageSelected += ViewPager_PageSelected;
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            var item = _bottomNavigation.Menu.GetItem(e.Position);
            _bottomNavigation.SelectedItemId = item.ItemId;
        }

        void NavigationView_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            _viewPager.SetCurrentItem(e.Item.Order, true);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        void InitializeFragments()
        {
            _fragments = new Android.Support.V4.App.Fragment[]
            {
                new MainFragment(), new HistoryFragment(), new PlayersFragment()
            };

        }

    }
}