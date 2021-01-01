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

        private PlayersRepositoryService()
        {
            _registeredPlayers = new List<Player>();

            _registeredPlayers.Add(new Player() { FullName = "Niko", GamesPlayed = 10, GamesWon = 5, MaxScore = 110 });
            _registeredPlayers.Add(new Player() { FullName = "Freja", GamesPlayed = 10, GamesWon = 5, MaxScore = 121 });
            _registeredPlayers.Add(new Player() { FullName = "Hans" });
            _registeredPlayers.Add(new Player() { FullName = "Thomas" });
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
        }



    }
}