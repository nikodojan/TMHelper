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

        private Button addGameButton;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnResume()
        {
            base.OnResume();
            recentGame = (gameRepo.GetAllGames().Count > 0) ? gameRepo.GetAllGames()[0] : new Game();
            SetupViews();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.mainFragment_layout, container, false);
            gameRepo = GameRepositoryService.Instance;
            recentGame = (gameRepo.GetAllGames().Count > 0) ? gameRepo.GetAllGames()[0] : new Game();
            gameDateTextView = (TextView)view.FindViewById(Resource.Id.recentGameDateMF);
            playerNamesTextView = (TextView)view.FindViewById(Resource.Id.playersViewMF);
            winnerTextView = (TextView)view.FindViewById(Resource.Id.winnerViewMF);
            addGameButton = (Button) view.FindViewById(Resource.Id.addNewGameButtonMF);
            addGameButton.Click += AddGameButton_Click;

            showDetailsImageView = (ImageView)view.FindViewById(Resource.Id.detailsImageMF);
            if (gameRepo.GetAllGames().Count < 1)
            {
                showDetailsImageView.Visibility = ViewStates.Invisible;
            }
            showDetailsImageView.Click += ShowDetailsImageView_Click;

            SetupViews();
            return view;
        }

        private void AddGameButton_Click(object sender, EventArgs e)
        {
            int gameId = gameRepo.AddGame();
            Intent intent = new Intent(this.Activity, typeof(GameDetailsActivity));
            intent.PutExtra("GameID", gameId);
            StartActivity(intent);
        }

        private void ShowDetailsImageView_Click(object sender, EventArgs e)
        {
            if (gameRepo.GetAllGames().Count > 0)
            {
                Intent detailsIntent = new Intent(this.Activity, typeof(GameDetailsActivity));
                detailsIntent.PutExtra("GameID", recentGame.GameId);
                StartActivity(detailsIntent);
            }

        }

        private void SetupViews()
        {
            if (recentGame.Corporations.Count != 0)
            {
                gameDateTextView.Text = recentGame.Date;
                playerNamesTextView.Text = recentGame.AllPlayerNames;
                winnerTextView.Text = recentGame.WinnerNamePoints;
            }
            else
            {
                gameDateTextView.Text = (!string.IsNullOrEmpty(recentGame.Date) ? recentGame.Date : "");
                playerNamesTextView.Text = "No corporations added yet";
                winnerTextView.Text = "";
            }

        }

    }
}