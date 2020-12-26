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
using Android.Media;
using TMHelper.Models;

namespace TMHelper.MockData
{
    class MockGameRepo
    {
        private static MockGameRepo _instance;
        private static List<Game> _mockGameList;

        private MockGameRepo()
        {
            _mockGameList = new List<Game>();

            List<Corporation> corps = new List<Corporation>();
            corps.Add(new Corporation() { PlayerName = "Niko", TotalPoints = 121 });
            corps.Add(new Corporation() { PlayerName = "Freja", TotalPoints = 122 });


            List<Corporation> corps2 = new List<Corporation>();
            corps2.Add(new Corporation() { PlayerName = "Hans", TotalPoints = 110 });
            corps2.Add(new Corporation() { PlayerName = "Thomas", TotalPoints = 91 });
            corps2.Add(new Corporation() { PlayerName = "John", TotalPoints = 100 });

            _mockGameList.Add(new Game() { GameId = 1 });
            _mockGameList.Add(new Game() { GameId = 2 });

            _mockGameList[0].Corporations = corps;
            _mockGameList[1].Corporations = corps2;
        }

        public static MockGameRepo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MockGameRepo();
                }

                return _instance;
            }
        }

        public List<Game> GameList
        {
            get { return _mockGameList; }
            set { _mockGameList = value; }
        }

    }
}