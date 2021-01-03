using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private Button deleteButton;

        private IPlayerReposiroty repo;

        private string existingPlayerName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.newPlayer_activity);
            
            repo = PlayersRepositoryService.Instance;

            playerName = (EditText) FindViewById(Resource.Id.newPlayerNameEditText);
            saveButton = (Button) FindViewById(Resource.Id.newPlayerSaveButton);
            deleteButton = (Button) FindViewById(Resource.Id.deletePlayerButton);
            deleteButton.Visibility = ViewStates.Gone;

            saveButton.Click += SaveButton_Click;
            
            existingPlayerName = Intent.GetStringExtra("playerName");
            if (!string.IsNullOrEmpty(existingPlayerName))
            {
                playerName.Text = existingPlayerName;
                deleteButton.Activated = true;
                deleteButton.Visibility = ViewStates.Visible;
            }

            deleteButton.Click += DeleteButton_Click;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder deleteAlert = new AlertDialog.Builder(this);
            deleteAlert.SetMessage("Do you really want to delete this player?");
            deleteAlert.SetTitle("Delete player");

            deleteAlert.SetPositiveButton("Delete", (alert, args) =>
                {
                    repo.DeletePLayer(existingPlayerName);
                    Toast.MakeText(this, "Player was deleted.", ToastLength.Short).Show();
                    Finish();

                }
            );

            deleteAlert.SetNegativeButton("Cancel", (alert, args) =>
            {
                deleteAlert.Dispose();
            });
            deleteAlert.Show();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string fullName = playerName.Text;

            if (!string.IsNullOrEmpty(existingPlayerName))
            {
                if (repo.EditName(existingPlayerName, fullName))
                {
                    Toast.MakeText(this, "Player name was successfully changed.", ToastLength.Short).Show();
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Player name already exists or is invalid. \nPlease choose another name.", ToastLength.Short).Show();
                }
            }
            else
            {
                if (repo.PlayerExists(fullName) == true)
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
}