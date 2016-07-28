using System;
using System.Linq;

public static class Extensions
{
    public static bool ContainsWords(this string s, string words)
    {
    	if(s == null) return false;
        s = s.Trim();
        words = words.Trim();
        
        return 
            s.Equals(words, StringComparison.OrdinalIgnoreCase)
         || s.StartsWith(words + " ", StringComparison.OrdinalIgnoreCase)
         || s.EndsWith(" " + words, StringComparison.OrdinalIgnoreCase)
         || s.EndsWith(" " + words + ".", StringComparison.OrdinalIgnoreCase) 
         || s.IndexOf(" " + words + " ", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public static IQueryable<FeedItem> TextSearch(this IQueryable<FeedItem> feed, string textSearch)
    {
        return feed.Where(i =>
                i.additional_image_link.ContainsWords(textSearch) ||
                i.adwords_grouping.ContainsWords(textSearch) ||
                i.adwords_labels.ContainsWords(textSearch) ||
                i.additional_image_link.ContainsWords(textSearch) ||
                i.adwords_grouping.ContainsWords(textSearch) ||
                i.adwords_labels.ContainsWords(textSearch) ||
                i.age_group.ContainsWords(textSearch) ||
                i.availability.ContainsWords(textSearch) ||
                i.brand.ContainsWords(textSearch) ||
                i.collection.ContainsWords(textSearch) ||
                i.condition.ContainsWords(textSearch) ||
                i.custom_label_0.ContainsWords(textSearch) ||
                i.custom_label_1.ContainsWords(textSearch) ||
                i.custom_label_2.ContainsWords(textSearch) ||
                i.decor.ContainsWords(textSearch) ||
                i.description.ContainsWords(textSearch) ||
                i.expiration_date.ContainsWords(textSearch) ||
                i.gender.ContainsWords(textSearch) ||
                i.google_product_category.ContainsWords(textSearch) ||
                i.gtin.ContainsWords(textSearch) ||
                i.Id.ContainsWords(textSearch) ||
                i.identifier_exists.ContainsWords(textSearch) ||
                i.image_link.ContainsWords(textSearch) ||
                i.item_group_id.ContainsWords(textSearch) ||
                i.link.ContainsWords(textSearch) ||
                i.material.ContainsWords(textSearch) ||
                i.mobile_link.ContainsWords(textSearch) ||
                i.mpn.ContainsWords(textSearch) ||
                i.online_only.ContainsWords(textSearch) ||
                i.pattern.ContainsWords(textSearch) ||
                i.price.ContainsWords(textSearch) ||
                i.product_type.ContainsWords(textSearch) ||
                i.shipping.ContainsWords(textSearch) ||
                i.shipping_weight.ContainsWords(textSearch) ||
                i.size.ContainsWords(textSearch) ||
                i.style.ContainsWords(textSearch) ||
                i.tax.ContainsWords(textSearch) ||
                i.title.ContainsWords(textSearch)
            );
    }
}