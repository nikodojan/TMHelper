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
using System.Net.Mime;
using System.Text;
using TMHelper.Activities;
using TMHelper.Models;
using TMHelper.Services;

namespace TMHelper.Fragments
{
    public class MainFragment : Android.Support.V4.App.Fragment
    {
        private IGameRespository gameRepo;
        private Game recentGame;

        private TextView gameDateTextView;
        private TextView playerNamesTextView;
        private TextView winnerTextView;

        private ImageView showDetailsImageView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnResume()
        {
            base.OnResume();
            SetupViews();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.mainFragment_layout, container, false);
            gameRepo = GameRepositoryService.Instance;
            recentGame = gameRepo.GetAllGames()[0];
            gameDateTextView = (TextView) view.FindViewById(Resource.Id.recentGameDateMF);
            playerNamesTextView = (TextView) view.FindViewById(Resource.Id.playersViewMF);
            winnerTextView = (TextView) view.FindViewById(Resource.Id.winnerViewMF);
            showDetailsImageView = (ImageView) view.FindViewById(Resource.Id.detailsImageMF);
            showDetailsImageView.Click += ShowDetailsImageView_Click;
            SetupViews();
            return view;
        }

        private void ShowDetailsImageView_Click(object sender, EventArgs e)
        {
            Intent detailsIntent = new Intent(this.Activity, typeof(GameDetailsActivity));
            detailsIntent.PutExtra("GameID", recentGame.GameId);
            StartActivity(detailsIntent);
        }

        private void SetupViews()
        {
            gameDateTextView.Text = recentGame.Date;
            playerNamesTextView.Text = recentGame.AllPlayerNames;
            winnerTextView.Text = recentGame.WinnerNamePoints;
        }

    }
}