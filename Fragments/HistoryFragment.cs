using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.Design.Widget;
using TMHelper.Activities;
using TMHelper.Adapters;
using TMHelper.Models;
using TMHelper.Services;

namespace TMHelper.Fragments
{
    public class HistoryFragment : Android.Support.V4.App.Fragment
    {

        private RecyclerView historyRecyclerView;
        private HistoryAdapter historyAdapter;
        private IGameRespository repo;
        private List<Game> gameList;
        private FloatingActionButton addGameButton;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnResume()
        {
            base.OnResume();
            SetupRecyclerView();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            repo = GameRepositoryService.Instance;
            gameList = repo.GetAllGames();
            View view = inflater.Inflate(Resource.Layout.historyFragment_layout, container, false);
            historyRecyclerView = (RecyclerView) view.FindViewById(Resource.Id.historyRecyclerView);
            FloatingActionButton addGameButton = (FloatingActionButton) view.FindViewById(Resource.Id.addGameFab);
            addGameButton.Click += AddGameButton_Click;
            SetupRecyclerView();
            return view;
        }

        private void AddGameButton_Click(object sender, EventArgs e)
        {
            int gameId = repo.AddGame();
            Intent intent = new Intent(this.Activity, typeof(GameDetailsActivity));
            intent.PutExtra("GameID", gameId);
            StartActivity(intent);
        }

        public void SetupRecyclerView()
        {
            historyRecyclerView.SetLayoutManager(new LinearLayoutManager(historyRecyclerView.Context));
            historyAdapter = new HistoryAdapter(gameList);
            historyAdapter.ItemClick += HistoryAdapter_ItemClick;
            historyRecyclerView.SetAdapter(historyAdapter);
        }

        private void HistoryAdapter_ItemClick(object sender, HistoryAdapterClickEventArgs e)
        {
            var game = gameList[e.Position];
            int id = game.GameId;
            Intent intent = new Intent(this.Activity, typeof(GameDetailsActivity));
            intent.PutExtra("GameID", id);
            StartActivity(intent);
        }
    }
}