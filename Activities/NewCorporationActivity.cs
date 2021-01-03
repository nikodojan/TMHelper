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
using TMHelper.Adapters;
using TMHelper.Models;
using TMHelper.Services;

namespace TMHelper.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false, NoHistory = true)]
    public class NewCorporationActivity : Activity
    {
        private Spinner nameSpinner;
        private EditText terraformingRate;
        private EditText greeneries;
        private EditText cities;
        private EditText cards;
        private TextView totalPoints;
        private RadioButton ms0PointsRadioButton;
        private RadioButton ms5PointsRadioButton;
        private RadioButton ms10PointsRadioButton;
        private RadioButton ms15PointsRadioButton;
        private RadioButton aw0PointsRadioButton;
        private RadioButton aw2PointsRadioButton;
        private RadioButton aw5PointsRadioButton;
        private FloatingActionButton saveButton;
        private FloatingActionButton deleteButton;
        private LinearLayout playerLayout;
        private TextView titleTextView;

        int trPoints;
        int milestonesPoints;
        int awardsPoints;
        int greeneriesPoints;
        int citiesPoints;
        int cardsPoints;
        private int totalPointsVal;
        private string playerName;
        
        private IPlayerReposiroty playerRepo;
        private IGameRespository gameRepo;
        private List<string> playerNamesList;

        private int gameId;
        private bool isExisting;
        private int existingGameId;
        private string existingPlayerName;
        private Corporation existingCorporation;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CorporationDetails_layout);

            playerRepo = PlayersRepositoryService.Instance;
            gameRepo = GameRepositoryService.Instance;
            gameId = Intent.GetIntExtra("gameId", 1);

            isExisting = Intent.GetBooleanExtra("isExisting", false);
            if (isExisting)
            {
                existingPlayerName = Intent.GetStringExtra("playerName");
                existingGameId = Intent.GetIntExtra("GameID", 0);
                existingCorporation = gameRepo.GetCorporation(existingGameId, existingPlayerName);
            }

            ConnectViews();

            if (isExisting)
            {
                SetupExistingCorporationValues();
                playerLayout.Visibility = ViewStates.Gone;
            }

            
        }

        void SetupExistingCorporationValues()
        {
            titleTextView.Text = $"Editing {existingPlayerName}'s corporation";
            terraformingRate.Text = existingCorporation.TerraformingRate.ToString();
            greeneries.Text = existingCorporation.Greeneries.ToString();
            cities.Text = existingCorporation.Cities.ToString();
            cards.Text = existingCorporation.Cards.ToString();

            if (existingCorporation.Milestones == 15)
            {
                ms15PointsRadioButton.Checked = true;
                milestonesPoints = 15;
            }
            else if (existingCorporation.Milestones == 5)
            {
                ms5PointsRadioButton.Checked = true;
                milestonesPoints = 5;
            }
            else if (existingCorporation.Milestones == 10)
            {
                ms10PointsRadioButton.Checked = true;
                milestonesPoints = 10;
            }
            else
            {
                ms0PointsRadioButton.Checked = true;
                milestonesPoints = 0;
            }

            if (existingCorporation.Awards == 5)
            {
                aw5PointsRadioButton.Checked = true;
                awardsPoints = 5;
            }
            else if (existingCorporation.Awards == 2)
            {
                aw2PointsRadioButton.Checked = true;
                awardsPoints = 2;
            }
            else
            {
                aw0PointsRadioButton.Checked = true;
                awardsPoints = 0;
            }

        }

        void ConnectViews()
        {
            playerNamesList = playerRepo.PlayerNamesList();
            SetupNameSpinner();

            terraformingRate = (EditText) FindViewById(Resource.Id.terraformingRateEntry);
            terraformingRate.TextChanged += TerraformingRate_TextChanged;
            greeneries = (EditText) FindViewById(Resource.Id.greeneriesEntry);
            greeneries.TextChanged += Greeneries_TextChanged;
            cities = (EditText) FindViewById(Resource.Id.citiesEntry);
            cities.TextChanged += Cities_TextChanged;
            cards = (EditText) FindViewById(Resource.Id.cardsEntry);
            cards.TextChanged += Cards_TextChanged;
            totalPoints = (TextView) FindViewById(Resource.Id.totalPointsView);

            ms0PointsRadioButton = (RadioButton) FindViewById(Resource.Id.ms0pointsRadio);
            ms0PointsRadioButton.Click += Ms0PointsRadioButton_Click;
            ms5PointsRadioButton = (RadioButton)FindViewById(Resource.Id.ms5pointsRadio);
            ms5PointsRadioButton.Click += Ms5PointsRadioButton_Click;
            ms10PointsRadioButton = (RadioButton)FindViewById(Resource.Id.ms10pointsRadio);
            ms10PointsRadioButton.Click += Ms10PointsRadioButton_Click;
            ms15PointsRadioButton = (RadioButton)FindViewById(Resource.Id.ms15pointsRadio);
            ms15PointsRadioButton.Click += Ms15PointsRadioButton_Click;

            aw0PointsRadioButton = (RadioButton) FindViewById(Resource.Id.aw0pointsRadio);
            aw0PointsRadioButton.Click += Aw0PointsRadioButton_Click;
            aw2PointsRadioButton = (RadioButton)FindViewById(Resource.Id.aw2pointsRadio);
            aw2PointsRadioButton.Click += Aw2PointsRadioButton_Click;
            aw5PointsRadioButton = (RadioButton)FindViewById(Resource.Id.aw5pointsRadio);
            aw5PointsRadioButton.Click += Aw5PointsRadioButton_Click;

            saveButton = (FloatingActionButton) FindViewById(Resource.Id.saveCorporationFab);
            saveButton.Click += SaveButton_Click;
            deleteButton = (FloatingActionButton) FindViewById(Resource.Id.deleteCorporationFab);

            titleTextView = (TextView) FindViewById(Resource.Id.titleTextCD);

            playerLayout = (LinearLayout) FindViewById(Resource.Id.playerNameLayoutCD);

            if (!isExisting)
            {
                deleteButton.Visibility = ViewStates.Gone;
                titleTextView.Text = "Create a new corporation";
            }

            deleteButton.Click += DeleteButton_Click;

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder deleteAlert = new AlertDialog.Builder(this);
            deleteAlert.SetMessage("Do you really want to delete this corporation?");
            deleteAlert.SetTitle("Delete corporation");

            deleteAlert.SetPositiveButton("Delete", (alert, args) =>
                {
                    gameRepo.DeleteCorporation(existingGameId, existingPlayerName);
                    Toast.MakeText(this, "Corporation was deleted.", ToastLength.Short).Show();
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
            if (isExisting)
            {
                existingCorporation.TerraformingRate = trPoints;
                existingCorporation.Milestones = milestonesPoints;
                existingCorporation.Awards = awardsPoints;
                existingCorporation.Greeneries = greeneriesPoints;
                existingCorporation.Cities = citiesPoints;
                existingCorporation.Cards = cardsPoints;
                existingCorporation.TotalPoints = totalPointsVal;
                gameRepo.UpdateCorporation(existingGameId, existingPlayerName, existingCorporation);
                Toast.MakeText(this, "The Corporation was updated.", ToastLength.Long).Show();
                Finish();
            }
            else
            {
                Corporation newCorporation = new Corporation();
                newCorporation.PlayerName = playerName;
                newCorporation.TerraformingRate = trPoints;
                newCorporation.Milestones = milestonesPoints;
                newCorporation.Awards = awardsPoints;
                newCorporation.Greeneries = greeneriesPoints;
                newCorporation.Cities = citiesPoints;
                newCorporation.Cards = cardsPoints;
                newCorporation.TotalPoints = totalPointsVal;

                if (gameRepo.IsExistingId(gameId))
                {
                    if (!gameRepo.IsExistingPlayer(gameId, playerName))
                    {
                        gameRepo.AddCorporationToGame(gameId, newCorporation);
                        Toast.MakeText(this, "The new corporation was added to the game.", ToastLength.Long).Show();
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, "There is already another corporation for this player.", ToastLength.Long).Show();
                    }

                }
                else
                {
                    Toast.MakeText(this, "Sorry! An error has occured. Please try again.", ToastLength.Long).Show();
                }
            }

        }

        private void SetupNameSpinner()
        {
            nameSpinner = (Spinner)FindViewById(Resource.Id.playerNameSpinner);
            var spinnerAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, playerNamesList);
            spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            nameSpinner.Adapter = spinnerAdapter;

            nameSpinner.ItemSelected += NameSpinner_ItemSelected;

            if (isExisting)
            {
                nameSpinner.Activated = false;
            }

        }

        private void NameSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position != -1)
            {
                playerName = playerNamesList[e.Position];
            }
        }

        #region Radio Buttons
        //Awards RadioButton click handler
        private void Aw5PointsRadioButton_Click(object sender, EventArgs e)
        {
            ClearAwardChoices();
            aw5PointsRadioButton.Checked = true;
            awardsPoints = 5;
            ChangeTotalValue();
        }

        private void Aw2PointsRadioButton_Click(object sender, EventArgs e)
        {
            ClearAwardChoices();
            aw2PointsRadioButton.Checked = true;
            awardsPoints = 2;
            ChangeTotalValue();
        }

        private void Aw0PointsRadioButton_Click(object sender, EventArgs e)
        {
            ClearAwardChoices();
            aw0PointsRadioButton.Checked = true;
            awardsPoints = 0;
            ChangeTotalValue();
        }

        private void ClearAwardChoices()
        {
            aw0PointsRadioButton.Checked = false;
            aw2PointsRadioButton.Checked = false;
            aw5PointsRadioButton.Checked = false;
        }

        //Milestones RadioButton click event handler
        private void Ms15PointsRadioButton_Click(object sender, EventArgs e)
        {
            ClearMilestonesChoices();
            ms15PointsRadioButton.Checked = true;
            milestonesPoints = 15;
            ChangeTotalValue();
        }

        private void Ms10PointsRadioButton_Click(object sender, EventArgs e)
        {
            ClearMilestonesChoices();
            ms10PointsRadioButton.Checked = true;
            milestonesPoints = 10;
            ChangeTotalValue();
        }

        private void Ms5PointsRadioButton_Click(object sender, EventArgs e)
        {
            ClearMilestonesChoices();
            ms5PointsRadioButton.Checked = true;
            milestonesPoints = 5;
            ChangeTotalValue();
        }

        private void Ms0PointsRadioButton_Click(object sender, EventArgs e)
        {
            ClearMilestonesChoices();
            ms0PointsRadioButton.Checked = true;
            milestonesPoints = 0;
            ChangeTotalValue();
        }

        private void ClearMilestonesChoices()
        {
            ms0PointsRadioButton.Checked = false;
            ms5PointsRadioButton.Checked = false;
            ms10PointsRadioButton.Checked = false;
            ms15PointsRadioButton.Checked = false;
        }



        #endregion


        private void Cards_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            int number;
            if (string.IsNullOrEmpty(cards.Text))
            {
                cardsPoints = 0;
            }
            else
            {
                bool parse = Int32.TryParse(cards.Text, out number);
                if (parse)
                {
                    cardsPoints = number;
                }
                else
                {
                    Toast.MakeText(this, "Only numbers are allowed for Card points", ToastLength.Long).Show();
                }
            }

            ChangeTotalValue();
        }

        private void Cities_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            int number;
            if (string.IsNullOrEmpty(cities.Text))
            {
                citiesPoints = 0;
            }
            else
            {
                bool parse = Int32.TryParse(cities.Text, out number);
                if (parse)
                {
                    citiesPoints = number;
                }
                else
                {
                    Toast.MakeText(this, "Only numbers are allowed for City points", ToastLength.Long).Show();
                }
            }

            ChangeTotalValue();
        }

        private void Greeneries_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            int number;
            if (string.IsNullOrEmpty(greeneries.Text))
            {
                greeneriesPoints = 0;
            }
            else
            {
                bool parse = Int32.TryParse(greeneries.Text, out number);
                if (parse)
                {
                    greeneriesPoints = number;
                }
                else
                {
                    Toast.MakeText(this, "Only numbers are allowed for Greenery points", ToastLength.Long).Show();
                }
            }

            ChangeTotalValue();
        }

        private void TerraformingRate_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            int number;
            if (string.IsNullOrEmpty(terraformingRate.Text))
            {
                trPoints = 0;
            }
            else
            {
                bool parse = Int32.TryParse(terraformingRate.Text, out number);
                if (parse)
                {
                    trPoints = number;
                }
                else
                {
                    Toast.MakeText(this, "Only numbers are allowed for TR", ToastLength.Long).Show();
                }
            }

            ChangeTotalValue();
        }

        private void CalculateTotal()
        {
            totalPointsVal = trPoints + milestonesPoints + awardsPoints + greeneriesPoints + citiesPoints + cardsPoints;
        }

        private void ChangeTotalValue()
        {
            CalculateTotal();
            totalPoints.Text = totalPointsVal.ToString();
        }
    }


}