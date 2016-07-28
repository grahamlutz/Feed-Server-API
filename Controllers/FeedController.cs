using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Caching.Memory;

namespace RoomsToGo.FeedService.Controllers
{
    // TODO: Learn to return proper HTTP Status Codes
    // TODO: Deploy
    // TODO: WP shortcode
    // TODO: WP shortcode examples


    [Route("api/[controller]")]
    public class FeedController : Controller
    {
        private IMemoryCache cache;
        private static object dbLock = new object();

        public FeedController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        private FeedItem[] GetDbData()
        {
            const string cacheKey = "FeedController.GetDbData";
            var result = this.cache.Get(cacheKey) as FeedItem[];

            if(result != null)
            {
                return result;
            }

            lock(dbLock)
            {
                using(var db = new FeedDataContext())
                {
                    result = db.Items.AsQueryable().ToArray();
                    if(result.Length.Equals(0))
                    {
                        this.Refresh().Wait();
                        result = db.Items.AsQueryable().ToArray();
                    }

                    return this.cache.Set(cacheKey, result, DateTimeOffset.Now.AddHours(1));
                }
            }
        }


        [HttpGet("refresh")]
        public async Task<string> Refresh()
        {
            var client = new HttpClient();
            var result = (await client.GetStringAsync("http://storage1.merchantadvantage.com/macm2543/AllRegionProducts.txt"))
                .Split(System.Environment.NewLine.ToCharArray());

            lock(dbLock)
            {
                using(var db = new FeedDataContext())
                {
                    var log = new StringBuilder();
                    try
                    {
                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();

                        var columns = result[0].Split('\t');

                        for(int i = 1; i < result.Length; i++)
                        {
                            log.AppendLine("Processing " + result[i]);

                            var line = result[i].Split('\t');
                            if(line.Length <= 1)
                            {
                                continue;
                            }

                            var item = new FeedItem();
                            for (int j = 0; j < columns.Length; j++)
                            {
                                var columnName = columns[j];
                                var columnValue = line[j];

                                log.AppendLine("Setting value for column " + columnName + " to " + columnValue);

                                var feedItemType = item.GetType();
                                var feedItemProperty = feedItemType.GetProperty(columnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                if(feedItemProperty != null)
                                {
                                    feedItemProperty.GetSetMethod().Invoke(item, new object[] { columnValue });
                                }else
                                {
                                    log.AppendLine("Property not found: " + columnName);
                                    throw new Exception("Property not found: " + columnName);
                                }
                            }

                            db.Items.Add(item);
                        }

                        var rowsAffected = db.SaveChanges();

                        return "Success! ("+rowsAffected+")";
                    }
                    catch (System.Exception ex)
                    {

                        return log.ToString() + "\n\n\n" + ex.Message;
                    }
                }
            }
        }

        // GET api/values
        [HttpGet]
        public FeedItem[] Get(string textSearch, string broadSearch, string booleanSearch, string orderBy = "id", int limit = 50)
        {
            // THIS WORKS!!! (brand%20%3D%20%22Rooms%20To%20Go%22)%20AND%20collection.Contains(%22Aber%22)

            var result = GetDbData();
            
            if(limit > 1000)
            {
                limit = 1000;
            }

            orderBy = string.IsNullOrEmpty(orderBy)
                ? "id"
                : orderBy;

            if(!string.IsNullOrWhiteSpace(booleanSearch))
            {
                result = result.AsQueryable().Where(booleanSearch.Trim()).ToArray();
            }
            if(!string.IsNullOrWhiteSpace(textSearch))
            {
                textSearch = textSearch.Trim();
                result = result.AsQueryable().TextSearch(textSearch).ToArray();
            }
            if(!string.IsNullOrEmpty(broadSearch))
            {
                broadSearch = broadSearch.Trim();
                var primaryResults = result.AsQueryable().TextSearch(broadSearch).OrderBy(orderBy).Take(limit).ToArray();
                var secondaryResults = new List<FeedItem>();

                var terms = broadSearch.Split(' ');

                if(primaryResults.Length <= limit)
                {
                    var take = limit - primaryResults.Length;

                    foreach (var term in terms)
                    {
                        var searchTerm = term.Trim();
                        if(!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            var resultSet = result.AsQueryable().TextSearch(searchTerm).OrderBy(orderBy).Take(take).ToArray();
                            secondaryResults.AddRange(resultSet);
                        }
                    }

                    var resultCount = primaryResults.Length + secondaryResults.Count;

                    if(resultCount < limit)
                    {
                        limit = resultCount;
                    }

                    result = new FeedItem[limit];
                    primaryResults.CopyTo(result, 0);
                    secondaryResults.AsQueryable().OrderBy(orderBy).Take(take).ToArray().CopyTo(result, primaryResults.Length);
                }

                return result.ToArray();
            }

            return result.AsQueryable().OrderBy(orderBy).Take(limit).ToArray();
        }
    }
}
