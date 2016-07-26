using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace helloWorld.Migrations
{
    [DbContext(typeof(FeedDataContext))]
    partial class FeedDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("FeedItem", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("additional_image_link");

                    b.Property<string>("adwords_grouping");

                    b.Property<string>("adwords_labels");

                    b.Property<string>("age_group");

                    b.Property<string>("availability");

                    b.Property<string>("brand");

                    b.Property<string>("collection");

                    b.Property<string>("color");

                    b.Property<string>("condition");

                    b.Property<string>("custom_label_0");

                    b.Property<string>("custom_label_1");

                    b.Property<string>("custom_label_2");

                    b.Property<string>("decor");

                    b.Property<string>("description");

                    b.Property<string>("expiration_date");

                    b.Property<string>("gender");

                    b.Property<string>("google_product_category");

                    b.Property<string>("gtin");

                    b.Property<string>("identifier_exists");

                    b.Property<string>("image_link");

                    b.Property<string>("item_group_id");

                    b.Property<string>("link");

                    b.Property<string>("material");

                    b.Property<string>("mobile_link");

                    b.Property<string>("mpn");

                    b.Property<string>("online_only");

                    b.Property<string>("pattern");

                    b.Property<string>("price");

                    b.Property<string>("product_type");

                    b.Property<string>("shipping");

                    b.Property<string>("shipping_weight");

                    b.Property<string>("size");

                    b.Property<string>("style");

                    b.Property<string>("tax");

                    b.Property<string>("title");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });
        }
    }
}
