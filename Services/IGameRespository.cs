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
    interface IGameRespository
    {
        public List<Game> GetAllGames();
        public int AddGame();
        public void AddCorporationToGame(int id, Corporation corporation);
        public List<int> GetAllIds();
        public bool IsExistingId(int id);

        public bool IsExistingPlayer(int id, string name);
        public void DeleteGame(int id);
        public Game GetGame(int gameId);
        public Corporation GetCorporation(int gameId, string playerName);
        public void DeleteCorporation(int gameId, string playerName);
        public void UpdateCorporation(int gameId, string playerName, Corporation corp);



    }
}