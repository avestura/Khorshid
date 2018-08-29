using Khorshid.DataAccessLayer;
using Khorshid.Models;
using Khorshid.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khorshid.Extensions
{
    public static class Extensions
    {
        public static TownData ApplyModifications(this TownData town)
         => new TownData()
            {
                Id = town.Id,
                Town = town.Town,
                Price = town.Price + " تومان",
                Tag = town.Tag
            };

        public static IEnumerable<TownData> AsViewModelIEnumerable(this IEnumerable<TownData> towns)
        {
            foreach (var town in towns) yield return town.ApplyModifications();
        }

    }
}
