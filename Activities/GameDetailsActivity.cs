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
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using TMHelper.Adapters;
using TMHelper.Fragments;
using TMHelper.Models;
using TMHelper.Services;

namespace TMHelper.Activities
{
    [Activity(Label = "GameDetailsActivity", Theme = "@style/AppTheme", MainLauncher = false)]
    public class GameDetailsActivity : AppCompatActivity
    {
        private RecyclerView corporationsRecyclerView;
        private CorporationsAdapter corporationsAdapter;
        private IGameRespository repo;
        private Game game;
        private List<Corporation> corpList;
        private FloatingActionButton addCorpFloatingActionButton;
        private int gameId;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.gameDetails_activity);

            // Create your application here
            corporationsRecyclerView = (RecyclerView) FindViewById(Resource.Id.corporationsRecyclerView);
            repo = GameRepositoryService.Instance;
            gameId = Intent.GetIntExtra("GameID",0);

            foreach (var g in repo.GetAllGames())
            {
                if (g.GameId == gameId)
                {
                    game = g;
                }
            }

            corpList = game.Corporations;
            addCorpFloatingActionButton = (FloatingActionButton) FindViewById(Resource.Id.addCorporationFab);
            addCorpFloatingActionButton.Click += AddCorpFloatingActionButton_Click;
            SetupRecyclerView();
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetupRecyclerView();
        }

        private void AddCorpFloatingActionButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(NewCorporationActivity));
            intent.PutExtra("gameId", gameId);
            StartActivity(intent);
        }

        public void SetupRecyclerView()
        {
            corporationsRecyclerView.SetLayoutManager(new LinearLayoutManager(corporationsRecyclerView.Context));
            corporationsAdapter = new CorporationsAdapter(corpList);
            corporationsRecyclerView.SetAdapter(corporationsAdapter);
        }



    }
}