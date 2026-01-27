

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Jaygor.People.Bussinness.helpers
{
    public static class BusinessHelper
    {
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>
	     (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			HashSet<TKey> knownKeys = new HashSet<TKey>();
			foreach (TSource element in source)
			{
				if (knownKeys.Add(keySelector(element)))
				{
					yield return element;
				}
			}
		}

        // public string GetHour
    }
}
