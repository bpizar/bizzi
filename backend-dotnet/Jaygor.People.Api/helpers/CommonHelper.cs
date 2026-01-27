using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Jaygor.People.Api.helpers
{
    public static class CommonHelper
    {
        public static string GetSubStringText(string txt,int maxchars)
        {
            return txt.Length > maxchars ? string.Format("{0}...", txt.Substring(0,maxchars-1)) : txt;
        }

		public static string UrlWebSite()
		{
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            var emailSection = configuration.GetSection("Common");          
            return emailSection.GetValue<string>("UrlWebSite");
        }


	

        public static string StaffWorkingHourDefault()
		{
			var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			var configuration = builder.Build();
			var emailSection = configuration.GetSection("Common");
			return emailSection.GetValue<string>("StaffWorkingHourDefault");
		}

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

    }



}
