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

namespace TMHelper.Models
{
    class Game
    {
        private int _gameId;
        private string _date;
        private List<Corporation> _corporations;

        public int GameId
        {
            get { return _gameId; }
            set { _gameId = value; }
        }

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public List<Corporation> Corporations
        {
            get { return _corporations; }
            set { _corporations = value; }
        }

        public string AllPlayerNames => GetAllPlayerNames();

        public string WinnerNamePoints => GetWinner();


        public Game()
        {
            _corporations = new List<Corporation>();
        }

        public List<string> Players()
        {
            List<string> playerNames = new List<string>();
            foreach (var c in _corporations)
            {
                playerNames.Add(c.PlayerName);
            }
            return playerNames;
        }

        public string GetAllPlayerNames()
        {
            List<string> names = Players();
            if (names.Count == 0)
            {
                return "No participants";
            }
            string namesString = string.Join(", ", names);
            return namesString;
        }

        public int MaxPoints()
        {
            if (_corporations.Count < 1)
            {
                return 0;
            }
            List<int> points = new List<int>();
            foreach (var c in _corporations)
            {
                points.Add(c.TotalPoints);
            }
            int maxPoints = (points.Count != 0) ? points.Max() : 0;
            return maxPoints;
        }

        public List<string> Winner()
        {
            List<string> winnerNames = new List<string>();
            foreach (var c in _corporations)
            {
                if (c.TotalPoints == MaxPoints())
                {
                    winnerNames.Add(c.PlayerName);
                }
            }
            return winnerNames;
        }

        public string GetWinner()
        {
            List<string> winnerNames = Winner();
            string names = "";
            if (winnerNames.Count > 1)
            {
                names = string.Join(", ", winnerNames);
            }
            else
            {
                names = winnerNames[0];
            }
            string winnerString = $"{names}, {MaxPoints()}";
            return winnerString;
        }


    }
}