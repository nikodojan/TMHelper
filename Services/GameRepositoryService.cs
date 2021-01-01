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
    class GameRepositoryService : IGameRespository
    {
        private static GameRepositoryService _instance;

        private static List<Game> _gamesList;

        private GameRepositoryService()
        {
            InitializeMockData();
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

        private static void InitializeMockData()
        {
            _gamesList = new List<Game>();

            List<Corporation> corps = new List<Corporation>();
            corps.Add(new Corporation() { PlayerName = "Niko", TotalPoints = 121 });
            corps.Add(new Corporation() { PlayerName = "Freja", TotalPoints = 122 });
            List<Corporation> corps2 = new List<Corporation>();
            corps2.Add(new Corporation() { PlayerName = "Hans", TotalPoints = 110 });
            corps2.Add(new Corporation() { PlayerName = "Thomas", TotalPoints = 91 });
            corps2.Add(new Corporation() { PlayerName = "John", TotalPoints = 100 });
            _gamesList.Add(new Game() { GameId = 1 });
            _gamesList.Add(new Game() { GameId = 2 });
            _gamesList[0].Corporations = corps;
            _gamesList[1].Corporations = corps2;

            _gamesList.Add(new Game() { GameId = 3, Corporations = new List<Corporation>() { new Corporation() { PlayerName = "Niko", TotalPoints = 100 } } });
            _gamesList.Add(new Game() { GameId = 4, Corporations = new List<Corporation>() { new Corporation() { PlayerName = "Niko", TotalPoints = 100 } } });

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
            int newId = existingIds.Max() + 1;
            Game newGame = new Game() {GameId = newId, Date = DateTime.Now.ToString("dd/MM/yy"), Corporations = new List<Corporation>()};
            _gamesList.Insert(0, newGame);
            return newId;
        }
        
        public void AddCorporationToGame(int id, Corporation corporation)
        {
            foreach (var g in _gamesList)
            {
                if (g.GameId == id)
                {
                    g.Corporations.Add(corporation);
                }
            }
        }
    }
}