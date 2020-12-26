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
using TMHelper.MockData;
using TMHelper.Models;

namespace TMHelper
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {

        private RecyclerView historyRecyclerView;
        private HistoryAdapter historyAdapter;
        private MockGameRepo repo;
        private List<Game> gameList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            // View setups
            historyRecyclerView = (RecyclerView) FindViewById(Resource.Id.historyRecyclerView);
            repo = MockGameRepo.Instance;
            gameList = repo.GameList;
            SetupRecyclerView();

            //Click Event Handler

        }
        
        public void SetupRecyclerView()
        {
            historyRecyclerView.SetLayoutManager(new LinearLayoutManager(historyRecyclerView.Context));
            historyAdapter = new HistoryAdapter(gameList);
            historyAdapter.ItemClick += HistoryAdapter_ItemClick;
            historyAdapter.ItemLongClick += HistoryAdapter_ItemLongClick;

            historyRecyclerView.SetAdapter(historyAdapter);
        }

        private void HistoryAdapter_ItemLongClick(object sender, HistoryAdapterClickEventArgs e)
        {
            Toast.MakeText(this, "Game was held", ToastLength.Short).Show();
        }

        private void HistoryAdapter_ItemClick(object sender, HistoryAdapterClickEventArgs e)
        {
            Toast.MakeText(this, "Game was clicked", ToastLength.Short).Show();
        }

        
    }
}