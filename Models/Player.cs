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
    class Player
    {
        private string _fullName;
        public int _gamesPlayed;
        private int _gamesWon;
        private int _maxScore;

        public Player()
        {
            
        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public int GamesPlayed
        {
            get { return _gamesPlayed; }
            set { _gamesPlayed = value; }
        }

        public int GamesWon
        {
            get { return _gamesWon; }
            set { _gamesWon = value; }
        }

        public int MaxScore
        {
            get { return _maxScore; }
            set { _maxScore = value; }
        }




    }
}