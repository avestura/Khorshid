using Khorshid.Extensions;
using Khorshid.Models;
using Khorshid.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khorshid.Engines
{
    public static class SearchEngine
    {

        public static void ApplySearchOnCollection(string term, ObservableCollection<TownPriceViewModel> collection)
        {
            var searchResult = new HashSet<TownData>();

            var rawData = App.CurrentApp.Configuration.TownData;

            var data = rawData.Select(item => new TownData()
            {
                TownId = item.TownId,
                Price = item.Price,
                Tag = item.Tag,
                Town = item.Town.Replace(")", "").Replace("(", "").Replace("-", "").Replace("آ", "ا")
            });

            data.Where(item =>
                   item.Town.StartsWith(term)
                )
                .ToList()
                .ForEach(item =>
                    searchResult.Add(rawData.First(dataItem => dataItem.TownId == item.TownId))
                );

            data.Where(item => item.Town.Split(' ').ToList().Any(tagItem => tagItem.StartsWith(term)))
                        .ToList()
                        .ForEach(item => searchResult.Add(rawData.First(dataItem => dataItem.TownId == item.TownId)));

            data.Where(
                    item => item.Tag.Split(' ').ToList().Any(tagItem => tagItem.StartsWith(term))
                    )
                    .ToList()
                    .ForEach(item =>
                        searchResult.Add(rawData.First(dataItem => dataItem.TownId == item.TownId))
                    );

            // Search by tokenizing terms
            var termSplit = term.Split(' ');
            if (termSplit.Length > 1)
            {
                data.Where(
                   item =>
                   {
                       var townSplit = item.Town.Split(' ');
                       return termSplit.ToList().TrueForAll(str => townSplit.Any(townItem => townItem.StartsWith(str)));
                   }
                   )
                   .ToList()
                   .ForEach(item =>
                         searchResult.Add(rawData.First(dataItem => dataItem.TownId == item.TownId))
                   );
            }

            collection.Clear();
            searchResult.ToList().ForEach(item => collection.Add(item.AsViewModel()));
        }

    }
}
