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
using TMHelper.Models;
using TMHelper.Services;

namespace TMHelper.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false, NoHistory = true)]
    public class NewPlayerActivity : Activity
    {
        private EditText playerName;
        private Button saveButton;

        private IPlayerReposiroty repo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.newPlayer_activity);
            
            // Create your application here
            repo = PlayersRepositoryService.Instance;

            playerName = (EditText) FindViewById(Resource.Id.newPlayerNameEditText);
            saveButton = (Button) FindViewById(Resource.Id.newPlayerSaveButton);

            saveButton.Click += SaveButton_Click;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string fullName = playerName.Text;
            if(repo.PlayerExists(fullName) == true)
            {
                Toast.MakeText(this, "Player name already exists. \nPlease choose another name.", ToastLength.Short).Show();
            }
            else
            {
                repo.AddPlayer(fullName);
                Finish();
            }
        }
    }
}