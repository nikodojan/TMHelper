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
    public class PlayersFragment : Android.Support.V4.App.Fragment
    {

        private RecyclerView playersRecyclerView;
        private PlayersAdapter playersAdapter;
        private IPlayerReposiroty repo;
        private List<Player> playersList;
        private FloatingActionButton addPlayerFloatingActionButton;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            //repo = MockGameRepo.Instance;
            //players = repo.GameList;
            
            //SetupRecyclerView();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);


            repo = PlayersRepositoryService.Instance;
            playersList = repo.GetAllPlayers();
            View view = inflater.Inflate(Resource.Layout.playersFragment_layout, container, false);
            playersRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.playersRecyclerView);
            addPlayerFloatingActionButton = (FloatingActionButton) view.FindViewById(Resource.Id.addPlayerFab);
            addPlayerFloatingActionButton.Click += AddPlayerFloatingActionButton_Click;

            SetupRecyclerView();
            return view;
        }

        private void AddPlayerFloatingActionButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(NewPlayerActivity));
            StartActivity(intent);
        }

        public void SetupRecyclerView()
        {
            
            playersRecyclerView.SetLayoutManager(new LinearLayoutManager(playersRecyclerView.Context));
            playersAdapter = new PlayersAdapter(playersList);
            //playersAdapter.ItemClick += PlayersAdapter_ItemClick;
            //historyAdapter.ItemLongClick += HistoryAdapter_ItemLongClick;

            playersRecyclerView.SetAdapter(playersAdapter);
            
        }

        //private void HistoryAdapter_ItemLongClick(object sender, HistoryAdapterClickEventArgs e)
        //{
            
        //}

        //private void PlayersAdapter_ItemClick(object sender, HistoryAdapterClickEventArgs e)
        //{
        //    var player = playersList[e.Position];
            

        //    Intent intent = new Intent(this.Activity, typeof(GameDetailsActivity));
        //    //Intent intent = new Intent(this, typeof(GameDetailsActivity));
        //    intent.PutExtra("GameID", id);
        //    StartActivity(intent);
        //}
    }
}