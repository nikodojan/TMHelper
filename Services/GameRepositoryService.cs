using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMHelper.Models;

namespace TMHelper.Services
{
    class GameRepositoryService : IGameRespository
    {
        private static GameRepositoryService _instance;

        private static List<Game> _gamesList;
        private ISharedPreferences gamesPreferences =
            Application.Context.GetSharedPreferences("games", FileCreationMode.Private);

        private ISharedPreferencesEditor editor;

        private GameRepositoryService()
        {
             editor = gamesPreferences.Edit();
            _gamesList = new List<Game>();
            _gamesList = LoadData();
        }

        public static GameRepositoryService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameRepositoryService();
                }
                return _instance;
            }
        }

        private List<Game> LoadData()
        {
            List<Game> newGamesList = new List<Game>();
            string gamesJsonString = gamesPreferences.GetString("games", "");
            if (!string.IsNullOrEmpty(gamesJsonString))
            {
                newGamesList = JsonConvert.DeserializeObject<List<Game>>(gamesJsonString);
            }
            return newGamesList;
        }

        private void WriteData(List<Game> newGamesList)
        {
            string gamesString = JsonConvert.SerializeObject(newGamesList);
            editor.PutString("games", gamesString);
            editor.Apply();
        }


        public List<Game> GameList
        {
            get { return _gamesList; }
            set { _gamesList = value; }
        }

        public List<Game> GetAllGames()
        {
            return _gamesList;
        }

        public List<int> GetAllIds()
        {
            List<int> existingIds = new List<int>();
            foreach (var g in _gamesList)
            {
                existingIds.Add(g.GameId);
            }
            return existingIds;
        }

        public bool IsExistingId(int id)
        {
            return (GetAllIds().Contains(id)) ? true : false;
        }

        public bool IsExistingPlayer(int id, string name)
        {
            Game game = new Game();
            foreach (var g in _gamesList)
            {
                if (g.GameId == id)
                {
                    game = g;
                }
            }
            foreach (var c in game.Corporations)
            {
                if (c.PlayerName == name)
                {
                    return true;
                }
            }
            return false;
        }

        public int AddGame()
        {
            List<int> existingIds = GetAllIds();
            int newId = (existingIds.Count == 0) ? 1 : existingIds.Max() + 1;
            
            Game newGame = new Game() {GameId = newId, Date = DateTime.Now.ToString("dd/MM/yy"), Corporations = new List<Corporation>()};
            _gamesList.Insert(0, newGame);
            WriteData(_gamesList);
            return newId;
        }
        
        public void AddCorporationToGame(int id, Corporation corporation)
        {
            foreach (var g in _gamesList)
            {
                if (g.GameId == id)
                {
                    g.Corporations.Add(corporation);
                    g.Show = true;
                    WriteData(_gamesList);
                }
            }
        }

        public void DeleteGame(int id)
        {
            if (_gamesList.Count > 0)
            {
                foreach (var g in _gamesList)
                {
                    if (g.GameId == id)
                    {
                        _gamesList.Remove(g);
                        break;
                    }
                }
            }
            WriteData(_gamesList);
        }

        public Game GetGame(int gameId)
        {
            foreach (var g in _gamesList)
            {
                if (g.GameId == gameId)
                {
                    return g;
                }
            }
            return new Game();
        }

        public Corporation GetCorporation(int gameId, string playerName)
        {
            foreach (var c in GetGame(gameId).Corporations)
            {
                if (c.PlayerName == playerName)
                {
                    return c;
                }
            }
            return new Corporation();
        }

        public void DeleteCorporation(int gameId, string playerName)
        {
            
            foreach (var c in GetGame(gameId).Corporations)
            {
                if (c.PlayerName == playerName)
                {
                    GetGame(gameId).Corporations.Remove(c);
                    WriteData(_gamesList);
                    break;
                }
            }
            

        }

        public void UpdateCorporation(int gameId, string playerName, Corporation corp)
        {
            foreach (var c in GetGame(gameId).Corporations)
            {
                if (c.PlayerName == playerName)
                {
                    c.TerraformingRate = corp.TerraformingRate;
                    c.Milestones = corp.Milestones;
                    c.Awards = corp.Awards;
                    c.Greeneries = corp.Greeneries;
                    c.Cities = corp.Cities;
                    c.Cards = corp.Cards;
                    c.TotalPoints = corp.TotalPoints;
                    WriteData(_gamesList);
                    break;
                }
            }
        }


    }
}