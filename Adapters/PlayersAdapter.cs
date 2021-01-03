using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using TMHelper.Models;

namespace TMHelper.Adapters
{
    class PlayersAdapter : RecyclerView.Adapter
    {
        public event EventHandler<PlayersAdapterClickEventArgs> ItemClick;
        public event EventHandler<PlayersAdapterClickEventArgs> ItemLongClick;
        private List<Player> playersList;

        public PlayersAdapter(List<Player> data)
        {
            playersList = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            //itemView = LayoutInflater.From(parent.Context).
            //       Inflate(id, parent, false);

            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.player_row, parent, false);

            var vh = new PlayersAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var player = playersList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as PlayersAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.PlayerNameTextView.Text = player.FullName;
            holder.TotalGamesTextView.Text = player.GamesPlayed.ToString();
            holder.GamesWonTextView.Text = player.GamesWon.ToString();
            holder.MaxPointsTextView.Text = player.MaxScore.ToString();
        }

        public override int ItemCount => playersList.Count;

        void OnClick(PlayersAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(PlayersAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class PlayersAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView PlayerNameTextView;
        public TextView TotalGamesTextView;
        public TextView GamesWonTextView;
        public TextView MaxPointsTextView;


        public PlayersAdapterViewHolder(View itemView, Action<PlayersAdapterClickEventArgs> clickListener,
                            Action<PlayersAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            PlayerNameTextView = (TextView) itemView.FindViewById(Resource.Id.playerRowNameTxt);
            TotalGamesTextView = (TextView) itemView.FindViewById(Resource.Id.playerRowGamesTotalValue);
            GamesWonTextView = (TextView) itemView.FindViewById(Resource.Id.playerRowGamesWonValue);
            MaxPointsTextView = (TextView) itemView.FindViewById(Resource.Id.playerRowMaxPointsValue);
            
            //TextView = v;
            itemView.Click += (sender, e) => clickListener(new PlayersAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new PlayersAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class PlayersAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}