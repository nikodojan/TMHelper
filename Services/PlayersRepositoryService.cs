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
using Newtonsoft.Json;
using TMHelper.Models;

namespace TMHelper.Services
{
    class PlayersRepositoryService : IPlayerReposiroty
    {
        private static PlayersRepositoryService _instance;
        private static List<Player> _registeredPlayers;
        private IGameRespository gameRepo;

        private ISharedPreferences playerPreferences =
            Application.Context.GetSharedPreferences("players", FileCreationMode.Private);

        private ISharedPreferencesEditor editor;

        private PlayersRepositoryService()
        {
            gameRepo = GameRepositoryService.Instance;
            editor = playerPreferences.Edit();

            _registeredPlayers = LoadData();

            //_registeredPlayers = new List<Player>();

            //_registeredPlayers.Add(new Player() { FullName = "Niko" });
            //_registeredPlayers.Add(new Player() { FullName = "Freja" });
            //_registeredPlayers.Add(new Player() { FullName = "Hans" });
            //_registeredPlayers.Add(new Player() { FullName = "Thomas" });
        }

        private List<Player> LoadData()
        {
            List<Player> newPlayerList = new List<Player>();
            string playersJsonString = playerPreferences.GetString("players", "");
            if (!string.IsNullOrEmpty(playersJsonString))
            {
                newPlayerList = JsonConvert.DeserializeObject<List<Player>>(playersJsonString);
            }
            return newPlayerList;
        }

        private void WriteData(List<Player> newPlayerList)
        {
            string playersString = JsonConvert.SerializeObject(newPlayerList);
            editor.PutString("players", playersString);
            editor.Apply();
        }

        public static PlayersRepositoryService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayersRepositoryService();
                }

                return _instance;
            }
        }

        public List<Player> GetAllPlayers()
        {
            if (_registeredPlayers != null)
            {
                return _registeredPlayers;
            }
            return new List<Player>();
        }

        public List<string> PlayerNamesList()
        {
            List<string> names = new List<string>();
            foreach (var p in GetAllPlayers())
            {
                names.Add(p.FullName);
            }
            return names;
        }

        public bool PlayerExists(string name)
        {
            foreach (var p in _registeredPlayers)
            {
                if (p.FullName == name)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddPlayer(string name)
        {
            Player newPlayer = new Player() { FullName = name };
            _registeredPlayers.Add(newPlayer);
            WriteData(_registeredPlayers);
        }

        public void SetPlayerStatistic()
        {
            SetGamesPlayed();
            SetGamesWon();
            SetMaxScore();
        }

        public void SetGamesWon()
        {
            foreach (var player in _registeredPlayers)
            {
                player.GamesWon = 0;
                foreach (var game in gameRepo.GetAllGames())
                {
                    foreach (var w in game.Winner())
                    {
                        if (player.FullName == w)
                        {
                            player.GamesWon++;
                        }
                    }
                }
            }
        }

        public void SetGamesPlayed()
        {
            foreach (var player in _registeredPlayers)
            {
                player.GamesPlayed = 0;
                foreach (var game in gameRepo.GetAllGames())
                {
                    foreach (var n in game.Players())
                    {
                        if (player.FullName == n)
                        {
                            player.GamesPlayed++;
                        }
                    }
                }
            }
        }

        public void SetMaxScore()
        {
            foreach (var player in _registeredPlayers)
            {
                player.MaxScore = 0;
                List<int> allScores = new List<int>();
                foreach (var game in gameRepo.GetAllGames())
                {
                    foreach (var corp in game.Corporations)
                    {
                        if (player.FullName == corp.PlayerName)
                        {
                            allScores.Add(corp.TotalPoints);
                        }
                    }
                }
                player.MaxScore = (allScores.Count > 0) ? allScores.Max() : 0 ;
            }
        }

        public void DeletePLayer(string name)
        {
            if (_registeredPlayers.Count > 0)
            {
                foreach (var p in _registeredPlayers)
                {
                    if (p.FullName == name)
                    {
                        _registeredPlayers.Remove(p);
                        WriteData(_registeredPlayers);
                        break;
                    }
                }
            }
        }

        public bool EditName(string oldName, string newName)
        {
            if (!string.IsNullOrEmpty(newName) && !PlayerExists(newName))
            {
                foreach (var player in _registeredPlayers)
                {
                    if (player.FullName == oldName)
                    {
                        player.FullName = newName;
                        return true;
                    }
                }
            }
            WriteData(_registeredPlayers);
            return false;
        }



    }
}