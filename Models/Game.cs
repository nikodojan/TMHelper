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
            _date = DateTime.Now.ToString("dd / MM / yy");
            _corporations = new List<Corporation>();

        }

        public string GetAllPlayerNames()
        {
            List<string> names = new List<string>();
            foreach (var c in _corporations)
            {
                names.Add(c.PlayerName);
            }

            string namesString = string.Join(", ", names);
            return namesString;
        }

        public string GetWinner()
        {
            List<int> points = new List<int>();
            foreach (var c in _corporations)
            {
                points.Add(c.TotalPoints);
            }
            int maxPoints = points.Max();

            List<string> winnerNames = new List<string>();
            foreach (var c in _corporations)
            {
                if (c.TotalPoints == maxPoints)
                {
                    winnerNames.Add(c.PlayerName);
                }
            }

            string names = "";
            if (winnerNames.Count > 1)
            {
                names = string.Join(", ", winnerNames);
            }
            else
            {
                names = winnerNames[0];
            }

            string winnerString = $"{names}, {maxPoints}";

            return winnerString;
        }


    }
}