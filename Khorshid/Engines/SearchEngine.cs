using Khorshid.Data;
using Khorshid.Extensions;
using Khorshid.Models;
using Khorshid.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Khorshid.Engines
{
    public static class SearchEngine
    {
        public static void ApplySearchOnCollection(string term, ObservableCollection<TownData> collection)
        {
            var searchResult = new HashSet<TownData>();
            var context = new KhorshidContext();
            var rawData = context.TownData;

            var data = rawData.Select(item => new
            {
                item.Id,
                item.Price,
                item.Tag,
                Town = item.Town.Replace(")", "").Replace("(", "").Replace("-", "").Replace("آ", "ا")
            }).ToList();

            if (int.TryParse(term, out int parsedTerm))
            {
                var desiredItem = rawData.FirstOrDefault(dataItem => dataItem.Id == parsedTerm);
                if (desiredItem != null) searchResult.Add(desiredItem);
            }

            data.Where(item =>
                   item.Town.StartsWith(term)
                )
                .ToList()
                .ForEach(item =>
                    searchResult.Add(rawData.First(dataItem => dataItem.Id == item.Id))
                );

            data.Where(item => item.Town.Split(' ').ToList().Any(tagItem => tagItem.StartsWith(term)))
                        .ToList()
                        .ForEach(item => searchResult.Add(rawData.First(dataItem => dataItem.Id == item.Id)));

            data.Where(
                    item => item.Tag.Split(' ').ToList().Any(tagItem => tagItem.StartsWith(term))
                    )
                    .ToList()
                    .ForEach(item =>
                        searchResult.Add(rawData.First(dataItem => dataItem.Id == item.Id))
                    );

            // Search by tokenizing terms
            var termSplit = term.Split(' ');
            if (termSplit.Length > 1)
            {
                data.Where(
                   townData =>
                   {
                       string[] townSplit = townData.Town.Split(' ');
                       return termSplit.ToList().TrueForAll(str => townSplit.Any(townItem => townItem.StartsWith(str)));
                   }
                   )
                   .ToList()
                   .ForEach(item =>
                         searchResult.Add(rawData.First(dataItem => dataItem.Id == item.Id))
                   );
            }

            collection.Clear();
            searchResult.ToList().ForEach(item => collection.Add(item.ApplyModifications()));
        }

        public static void ApplySearchOnCollection(string term, ObservableCollection<Customer> collection)
        {
            var searchResult = new HashSet<Customer>();
            var context = new KhorshidContext();
            var rawData = context.Customers;

            var data = rawData.Select(item => new
            {
                item.Id,
                item.SubscriptionId,
                item.Address,
                item.MobileNumber,
                item.PhoneNumber,
                Name = item.Name.Replace("آ", "ا")
            }).ToList();

            if (int.TryParse(term, out int parsedTerm))
            {
                var desiredItem = rawData.FirstOrDefault(dataItem => dataItem.SubscriptionId == parsedTerm);
                if (desiredItem != null) searchResult.Add(desiredItem);
            }

            data.Where(item =>
                   item.Name.StartsWith(term)
                )
                .ToList()
                .ForEach(item =>
                    searchResult.Add(rawData.First(dataItem => dataItem.Id == item.Id))
                );

            data.Where(item => item.Name.Split(' ').ToList().Any(tagItem => tagItem.StartsWith(term)))
                        .ToList()
                        .ForEach(item => searchResult.Add(rawData.First(dataItem => dataItem.Id == item.Id)));

            // Search by tokenizing terms
            var termSplit = term.Split(' ');
            if (termSplit.Length > 1)
            {
                data.Where(
                   townData =>
                   {
                       string[] townSplit = townData.Name.Split(' ');
                       return termSplit.ToList().TrueForAll(str => townSplit.Any(townItem => townItem.StartsWith(str)));
                   }
                   )
                   .ToList()
                   .ForEach(item =>
                         searchResult.Add(rawData.First(dataItem => dataItem.Id == item.Id))
                   );
            }

            collection.Clear();
            searchResult.ToList().ForEach(item => collection.Add(item));
        }
    }
}
