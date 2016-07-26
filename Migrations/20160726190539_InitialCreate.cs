using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomsToGo.FeedService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    additional_image_link = table.Column<string>(nullable: true),
                    adwords_grouping = table.Column<string>(nullable: true),
                    adwords_labels = table.Column<string>(nullable: true),
                    age_group = table.Column<string>(nullable: true),
                    availability = table.Column<string>(nullable: true),
                    brand = table.Column<string>(nullable: true),
                    collection = table.Column<string>(nullable: true),
                    color = table.Column<string>(nullable: true),
                    condition = table.Column<string>(nullable: true),
                    custom_label_0 = table.Column<string>(nullable: true),
                    custom_label_1 = table.Column<string>(nullable: true),
                    custom_label_2 = table.Column<string>(nullable: true),
                    decor = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    expiration_date = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: true),
                    google_product_category = table.Column<string>(nullable: true),
                    gtin = table.Column<string>(nullable: true),
                    identifier_exists = table.Column<string>(nullable: true),
                    image_link = table.Column<string>(nullable: true),
                    item_group_id = table.Column<string>(nullable: true),
                    link = table.Column<string>(nullable: true),
                    material = table.Column<string>(nullable: true),
                    mobile_link = table.Column<string>(nullable: true),
                    mpn = table.Column<string>(nullable: true),
                    online_only = table.Column<string>(nullable: true),
                    pattern = table.Column<string>(nullable: true),
                    price = table.Column<string>(nullable: true),
                    product_type = table.Column<string>(nullable: true),
                    shipping = table.Column<string>(nullable: true),
                    shipping_weight = table.Column<string>(nullable: true),
                    size = table.Column<string>(nullable: true),
                    style = table.Column<string>(nullable: true),
                    tax = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
