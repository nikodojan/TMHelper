using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using TMHelper.Models;

namespace TMHelper.Adapters
{
    class HistoryAdapter : RecyclerView.Adapter
    {
        public event EventHandler<HistoryAdapterClickEventArgs> ItemClick;
        public event EventHandler<HistoryAdapterClickEventArgs> ItemLongClick;

        private List<Game> gameList;


        public HistoryAdapter(List<Game> data)
        {
            gameList = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            //itemView = LayoutInflater.From(parent.Context).
            //       Inflate(id, parent, false);

            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.history_row, parent, false);

            var vh = new HistoryAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var games = gameList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as HistoryAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.gameDateTextView.Text = games.Date;
            holder.playerNamesTextView.Text = games.AllPlayerNames;
            holder.gameWinnerTextView.Text = games.WinnerNamePoints;
        }

        public override int ItemCount => gameList.Count;

        void OnClick(HistoryAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(HistoryAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }



    public class HistoryAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }

        public TextView gameDateTextView;
        public TextView playerNamesTextView;
        public TextView gameWinnerTextView;


        public HistoryAdapterViewHolder(View itemView, Action<HistoryAdapterClickEventArgs> clickListener,
                            Action<HistoryAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            gameDateTextView = (TextView) itemView.FindViewById(Resource.Id.gameDateTextView);
            playerNamesTextView = (TextView) itemView.FindViewById(Resource.Id.allPlayersTextView);
            gameWinnerTextView = (TextView) itemView.FindViewById(Resource.Id.winnerTextView);

            //TextView = v;
            itemView.Click += (sender, e) => clickListener(new HistoryAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new HistoryAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class HistoryAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}