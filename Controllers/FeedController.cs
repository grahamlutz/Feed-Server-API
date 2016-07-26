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

namespace RoomsToGo.FeedService.Controllers
{
    // TODO: Learn to return proper HTTP Status Codes
    // TODO: Deploy
    // TODO: WP shortcode
    // TODO: WP shortcode examples


    [Route("api/[controller]")]
    public class FeedController : Controller
    {
        public static object dbLock = new object();

        private FeedItem[] GetDbData()
        {
            lock(dbLock)
            {
                using(var db = new FeedDataContext())
                {
                    return db.Items.AsQueryable().ToArray();
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
        public FeedItem[] Get(string textSearch, string booleanSearch, string orderBy = "id", int limit = 50)
        {
            // THIS WORKS!!! (brand%20%3D%20%22Rooms%20To%20Go%22)%20AND%20collection.Contains(%22Aber%22)

            IQueryable<FeedItem> result = GetDbData().AsQueryable();
            
            if(!string.IsNullOrEmpty(booleanSearch))
            {
                result = result.Where(booleanSearch);
            }
            if(!string.IsNullOrEmpty(textSearch))
            {
                result = result.Where(i =>
                    i.additional_image_link.ContainsCaseInsensitive(textSearch) ||
                    i.adwords_grouping.ContainsCaseInsensitive(textSearch) ||
                    i.adwords_labels.ContainsCaseInsensitive(textSearch) ||
                    i.additional_image_link.ContainsCaseInsensitive(textSearch) ||
                    i.adwords_grouping.ContainsCaseInsensitive(textSearch) ||
                    i.adwords_labels.ContainsCaseInsensitive(textSearch) ||
                    i.age_group.ContainsCaseInsensitive(textSearch) ||
                    i.availability.ContainsCaseInsensitive(textSearch) ||
                    i.brand.ContainsCaseInsensitive(textSearch) ||
                    i.collection.ContainsCaseInsensitive(textSearch) ||
                    i.condition.ContainsCaseInsensitive(textSearch) ||
                    i.custom_label_0.ContainsCaseInsensitive(textSearch) ||
                    i.custom_label_1.ContainsCaseInsensitive(textSearch) ||
                    i.custom_label_2.ContainsCaseInsensitive(textSearch) ||
                    i.decor.ContainsCaseInsensitive(textSearch) ||
                    i.description.ContainsCaseInsensitive(textSearch) ||
                    i.expiration_date.ContainsCaseInsensitive(textSearch) ||
                    i.gender.ContainsCaseInsensitive(textSearch) ||
                    i.google_product_category.ContainsCaseInsensitive(textSearch) ||
                    i.gtin.ContainsCaseInsensitive(textSearch) ||
                    i.Id.ContainsCaseInsensitive(textSearch) ||
                    i.identifier_exists.ContainsCaseInsensitive(textSearch) ||
                    i.image_link.ContainsCaseInsensitive(textSearch) ||
                    i.item_group_id.ContainsCaseInsensitive(textSearch) ||
                    i.link.ContainsCaseInsensitive(textSearch) ||
                    i.material.ContainsCaseInsensitive(textSearch) ||
                    i.mobile_link.ContainsCaseInsensitive(textSearch) ||
                    i.mpn.ContainsCaseInsensitive(textSearch) ||
                    i.online_only.ContainsCaseInsensitive(textSearch) ||
                    i.pattern.ContainsCaseInsensitive(textSearch) ||
                    i.price.ContainsCaseInsensitive(textSearch) ||
                    i.product_type.ContainsCaseInsensitive(textSearch) ||
                    i.shipping.ContainsCaseInsensitive(textSearch) ||
                    i.shipping_weight.ContainsCaseInsensitive(textSearch) ||
                    i.size.ContainsCaseInsensitive(textSearch) ||
                    i.style.ContainsCaseInsensitive(textSearch) ||
                    i.tax.ContainsCaseInsensitive(textSearch) ||
                    i.title.ContainsCaseInsensitive(textSearch)
                );
            }

            return result.OrderBy(orderBy).Take(limit).ToArray();
        }
    }
}
