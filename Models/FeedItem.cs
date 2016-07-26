using System.ComponentModel.DataAnnotations;

public class FeedItem
{
    [Key]
    public string Id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string google_product_category { get; set; }
    public string product_type { get; set; }
    public string link { get; set; }
    public string image_link { get; set; }
    public string additional_image_link { get; set; }
    public string condition { get; set; }
    public string availability { get; set; }
    public string price { get; set; }
    public string brand { get; set; }
    public string gtin { get; set; }
    public string mpn { get; set; }
    public string item_group_id { get; set; }
    public string collection { get; set; }
    public string color { get; set; }
    public string material { get; set; }
    public string style { get; set; }
    public string decor { get; set; }
    public string pattern { get; set; }
    public string size { get; set; }
    public string gender { get; set; }
    public string age_group { get; set; }
    public string tax { get; set; }
    public string shipping { get; set; }
    public string shipping_weight { get; set; }
    public string online_only { get; set; }
    public string expiration_date { get; set; }
    public string adwords_grouping { get; set; }
    public string adwords_labels { get; set; }
    public string mobile_link { get; set; }
    public string custom_label_0 { get; set; }
    public string custom_label_1 { get; set; }
    public string custom_label_2 { get; set; }
    public string identifier_exists { get; set; }

}