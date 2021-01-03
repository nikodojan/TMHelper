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

namespace TMHelper.Services
{
    interface IPlayerReposiroty
    {

        public void AddPlayer(string name);

        public List<Player> GetAllPlayers();

        public List<string> PlayerNamesList();

        public bool PlayerExists(string name);
        public void SetPlayerStatistic();
        public void DeletePLayer(string name);
        public bool EditName(string oldName, string newName);



    }
}