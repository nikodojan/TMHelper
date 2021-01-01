using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using TMHelper.Models;

namespace TMHelper.Adapters
{
    class CorporationsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<CorporationsAdapterClickEventArgs> ItemClick;
        public event EventHandler<CorporationsAdapterClickEventArgs> ItemLongClick;
        
        private List<Corporation> corpList;

        public CorporationsAdapter(List<Corporation> data)
        {
            corpList = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            //itemView = LayoutInflater.From(parent.Context).
            //       Inflate(id, parent, false);

            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.corporation_row, parent, false);

            var vh = new CorporationsAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var corporation = corpList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as CorporationsAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.playerName.Text = corporation.PlayerName;
            holder.terraformingRate.Text = corporation.TerraformingRate.ToString();
            holder.milestones.Text = corporation.Milestones.ToString();
            holder.awards.Text = corporation.Awards.ToString();
            holder.greeneries.Text = corporation.Greeneries.ToString();
            holder.cities.Text = corporation.Cities.ToString();
            holder.cards.Text = corporation.Cards.ToString();
            holder.totalPoints.Text = corporation.TotalPoints.ToString();

        }

        public override int ItemCount => corpList.Count;

        void OnClick(CorporationsAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(CorporationsAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class CorporationsAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView playerName;
        public TextView terraformingRate;
        public TextView milestones;
        public TextView awards;
        public TextView greeneries;
        public TextView cities;
        public TextView cards;
        public TextView totalPoints;

        public CorporationsAdapterViewHolder(View itemView, Action<CorporationsAdapterClickEventArgs> clickListener,
                            Action<CorporationsAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            playerName = (TextView) itemView.FindViewById(Resource.Id.corpRowPlayerName);
            terraformingRate = (TextView)itemView.FindViewById(Resource.Id.corpRowTR);
            milestones = (TextView)itemView.FindViewById(Resource.Id.corpRowMS);
            awards = (TextView)itemView.FindViewById(Resource.Id.corpRowAW);
            greeneries = (TextView)itemView.FindViewById(Resource.Id.corpRowGR);
            cities = (TextView)itemView.FindViewById(Resource.Id.corpRowCT);
            cards = (TextView)itemView.FindViewById(Resource.Id.corpRowCD);
            totalPoints = (TextView)itemView.FindViewById(Resource.Id.corpRowTP);

            //TextView = v;
            itemView.Click += (sender, e) => clickListener(new CorporationsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new CorporationsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class CorporationsAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}