using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.DB.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlatOfficeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelYear = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "DECIMAL(10,0)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentWay = table.Column<int>(type: "int", nullable: false),
                    DeliveryType = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryContactId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryContacts_DeliveryContactId",
                        column: x => x.DeliveryContactId,
                        principalTable: "DeliveryContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserComparableProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComparableProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserComparableProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavoriteProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserCartId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItems_UserCarts_UserCartId",
                        column: x => x.UserCartId,
                        principalTable: "UserCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("f001cef9-4959-472a-85ca-581117d0f43d"), "Cadillac", "Red", 4750807m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2005, "Escalade EXT" },
                    { new Guid("8b3f3af5-2567-448a-8a03-b596272e2d45"), "Ford", "Pink", 1213658m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1996, "Contour" },
                    { new Guid("43da4814-1e5c-47a5-8b9a-b433d6e187b5"), "Toyota", "Yellow", 2040788m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1999, "4Runner" },
                    { new Guid("e0004137-556f-4d09-aeda-3421583e3834"), "Lotus", "Puce", 2091325m, "Hungary", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1991, "Elan" },
                    { new Guid("5f796195-67a4-4ef9-8cbd-402293a9d996"), "Aston Martin", "Turquoise", 1888085m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2009, "DB9" },
                    { new Guid("b63a9087-ca17-408d-b12f-e557a4203dc8"), "Ford", "Crimson", 4252306m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1988, "Aerostar" },
                    { new Guid("0841367e-6b49-4713-9c5d-1988b409daad"), "Pontiac", "Blue", 2360243m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2005, "Grand Prix" },
                    { new Guid("4cb5d073-3a40-4eda-8e50-25fe8199d20c"), "Dodge", "Purple", 2272618m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1999, "Ram 3500" },
                    { new Guid("6a0f0d95-5758-4859-bdac-77f343659a31"), "Ford", "Violet", 1152579m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2006, "F150" },
                    { new Guid("7c48a65d-8ad5-4a94-b3ed-ca527d8d1774"), "Nissan", "Turquoise", 351386m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2006, "Murano" },
                    { new Guid("10993eb4-24b1-4876-b73d-bb59468921e1"), "Honda", "Goldenrod", 3041462m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2001, "Accord" },
                    { new Guid("021ff5d7-3370-43c7-8974-60ba4391b3c8"), "Cadillac", "Green", 951946m, "Poland", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2008, "STS" },
                    { new Guid("d1e1d528-1ce5-4bc0-b593-02e4570b28e5"), "Maybach", "Crimson", 3323675m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2003, "62" },
                    { new Guid("904288d5-c430-49bc-bb3a-257ec8d9f232"), "Scion", "Orange", 596871m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2011, "xB" },
                    { new Guid("198d3fb3-34a3-4c8a-a351-72dff869f60d"), "Chrysler", "Aquamarine", 3472492m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1999, "300" },
                    { new Guid("c04dbdea-de92-4547-b0a1-1a2ac3ac8280"), "Toyota", "Teal", 3394337m, "Colombia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2000, "4Runner" },
                    { new Guid("e060b0bc-b0c4-4fdd-a207-60c2d9c1a433"), "Pontiac", "Fuscia", 4096217m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1992, "Trans Sport" },
                    { new Guid("8b6fa8e8-8a37-4d43-8a46-bda09f793a75"), "GMC", "Indigo", 1780071m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2000, "Safari" },
                    { new Guid("49f280c9-0d38-4ed4-be01-529236bc6121"), "Land Rover", "Goldenrod", 774026m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2009, "LR3" },
                    { new Guid("a02d30c1-7f20-40c2-8977-d3da32f88269"), "Ford", "Aquamarine", 3460249m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1985, "Bronco II" },
                    { new Guid("91ac1158-77ec-4a0d-981c-34433a46e796"), "Cadillac", "Red", 4940328m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2006, "DTS" },
                    { new Guid("c887365a-24cf-41e0-9206-752bd7c5d974"), "GMC", "Violet", 1701072m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1996, "1500" },
                    { new Guid("b844e8ed-e309-4c2a-8cc3-a8f66174b933"), "Nissan", "Maroon", 1429841m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2008, "Xterra" },
                    { new Guid("dd887df1-9209-4060-9e35-289a6dcac0b2"), "Mazda", "Indigo", 3646774m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1988, "626" },
                    { new Guid("4f48675f-a1b4-425b-b7b3-1158ca77f8e4"), "Honda", "Aquamarine", 2605434m, "France", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2001, "Civic" },
                    { new Guid("bd7ba5d8-7846-492a-9903-0284d56f6290"), "Mazda", "Indigo", 3826330m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2011, "Mazda3" },
                    { new Guid("d868daf5-376f-4604-89c8-117d76118c8b"), "Dodge", "Orange", 1096265m, "France", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2002, "Neon" },
                    { new Guid("31964e0c-f345-4eda-9860-1e517835fb9e"), "Lamborghini", "Teal", 1679173m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1990, "Countach" },
                    { new Guid("4ecb3905-35a3-446b-b93a-ddbbbf0601ef"), "Chrysler", "Green", 4175881m, "Sweden", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1996, "New Yorker" },
                    { new Guid("df54886a-107d-40d5-8120-2af35996ae6c"), "Mercedes-Benz", "Pink", 4467863m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2000, "CL-Class" },
                    { new Guid("82b5a3e2-0442-4c2c-8ec6-b0765144f031"), "Volkswagen", "Green", 1827921m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1999, "Cabriolet" },
                    { new Guid("306b328e-d4e1-428d-9bbd-6cb0999d4a96"), "Hyundai", "Purple", 3089609m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2005, "Sonata" },
                    { new Guid("b63c592a-fd9d-45d6-9239-8232e0ecc01e"), "Ford", "Puce", 3130160m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1994, "Lightning" },
                    { new Guid("d234c780-c024-45cc-a1ee-df360925a526"), "Nissan", "Turquoise", 2579229m, "France", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2001, "Altima" },
                    { new Guid("43f6c2e8-8692-49d3-9646-2fa8b85fa871"), "Toyota", "Puce", 1455605m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2001, "Celica" },
                    { new Guid("70e4396c-d881-4c0c-b56e-f4582eda4837"), "Lamborghini", "Blue", 1017174m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1997, "Diablo" },
                    { new Guid("63828396-a26b-42f6-9eb5-b6406c558f93"), "Ford", "Green", 2712867m, "Denmark", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2008, "Edge" },
                    { new Guid("15d23ecf-5acf-4e1e-af54-ffbd4cd439e1"), "Oldsmobile", "Aquamarine", 3632680m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1999, "Aurora" },
                    { new Guid("931f1d89-c8c6-4919-85aa-3ff9105a8431"), "Dodge", "Teal", 1036181m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1999, "Neon" },
                    { new Guid("0234222d-4ad6-4a76-aa53-c9c5f0068ebe"), "Scion", "Blue", 2241051m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2009, "xB" },
                    { new Guid("9223e120-1279-41b7-8655-bb3fed675c47"), "Mazda", "Red", 4080250m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2004, "Tribute" },
                    { new Guid("8f1eff23-22e8-4e7f-8f7e-766a70851a46"), "Mercury", "Goldenrod", 3438795m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1988, "Topaz" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("fb6872e3-d47f-4621-989f-1dbe8250f0ab"), "Dodge", "Mauv", 2994797m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2008, "Grand Caravan" },
                    { new Guid("155f3f98-38b7-4696-9c23-e7bf76076919"), "Volkswagen", "Goldenrod", 3911529m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2006, "GTI" },
                    { new Guid("51cf493d-645e-48aa-99fb-015fb8b10f95"), "Audi", "Mauv", 4287447m, "Sweden", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2011, "A8" },
                    { new Guid("ac71b9fc-bd09-4b70-a91f-45bdd2233b75"), "Chrysler", "Pink", 2432467m, "Czech Republic", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2000, "Concorde" },
                    { new Guid("ffa3ca27-1f1c-4f3d-a12d-c25c4343801e"), "Maserati", "Goldenrod", 4936689m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2003, "Spyder" },
                    { new Guid("8125eab4-8d30-44bf-80fe-71f30c733010"), "Volvo", "Indigo", 747014m, "Sweden", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2011, "XC60" },
                    { new Guid("64dc4f74-3765-4786-87c3-5a9db663c0a7"), "MINI", "Teal", 494752m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2012, "Countryman" },
                    { new Guid("8f859a19-0894-4da2-ac60-ee6b818cc5e5"), "Buick", "Maroon", 2023423m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1992, "Skylark" },
                    { new Guid("f89ae802-f143-4c53-836f-a97658f0b8c0"), "Cadillac", "Khaki", 4118376m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2000, "Catera" },
                    { new Guid("34ab88e4-722b-4836-ac6b-a694d2677398"), "Lincoln", "Blue", 429568m, "Japan", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1988, "Town Car" },
                    { new Guid("7e5ca632-5484-4b6a-a590-42edf59b8eba"), "Chevrolet", "Violet", 1488589m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1972, "Corvette" },
                    { new Guid("fdf25021-915e-4f8d-9a2e-87f588bf524f"), "Ford", "Indigo", 364136m, "Japan", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1995, "F-Series" },
                    { new Guid("37c37544-147b-4e83-b139-8efd72ec4266"), "Chevrolet", "Aquamarine", 2653963m, "Japan", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2012, "Express 2500" },
                    { new Guid("e0a7256c-27a6-4fd9-ab18-804ae9699d9f"), "Honda", "Teal", 2465677m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2002, "Accord" },
                    { new Guid("db6b4d6e-faf3-49f4-ab67-770d8c61e844"), "Honda", "Green", 337502m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2006, "Civic Si" },
                    { new Guid("fc20b3a2-9a9e-4d5c-be9f-601ff78754ac"), "Hyundai", "Mauv", 254434m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1993, "Sonata" },
                    { new Guid("be6fe60a-e99f-4975-b614-9e2a14c41547"), "GMC", "Mauv", 415059m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2008, "Canyon" },
                    { new Guid("496c154f-4e66-4693-9ffd-3ba3a4835ff4"), "Scion", "Crimson", 2862431m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2005, "xB" },
                    { new Guid("e93e3546-2879-4043-b123-629db3a55849"), "Chevrolet", "Indigo", 3866160m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2010, "Express 2500" },
                    { new Guid("3df7a05c-bce5-4e04-ad17-6a24b33c4173"), "Chevrolet", "Maroon", 2083097m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1997, "2500" },
                    { new Guid("7e1bff12-c3ef-4aae-a561-d735d5daa33d"), "Hummer", "Pink", 2477561m, "Poland", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2007, "H2" },
                    { new Guid("489124a1-c633-470d-98e1-316f7ea121eb"), "Subaru", "Turquoise", 887486m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1992, "Justy" },
                    { new Guid("976275ce-2f47-4d65-b7cf-3a357c427287"), "GMC", "Yellow", 1270269m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2002, "Sierra 1500" },
                    { new Guid("4c2032cb-eb9b-4168-840e-84bea9299eca"), "Ford", "Teal", 2193132m, "Colombia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1987, "Courier" },
                    { new Guid("7f00e6e9-ca2e-4c21-b93b-e26bbd6c9025"), "Jeep", "Teal", 3182770m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1997, "Cherokee" },
                    { new Guid("78748ea8-e302-41ef-92a8-050f0c003e4c"), "Dodge", "Blue", 2183473m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2003, "Durango" },
                    { new Guid("787d3228-df38-448f-b128-a2035f24a908"), "Chevrolet", "Teal", 313373m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1971, "Vega" },
                    { new Guid("dd0a22d9-0bca-4e4f-a1bf-e23d86534ed6"), "Geo", "Orange", 4125983m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1995, "Prizm" },
                    { new Guid("fd09983c-4385-4a0b-954d-544c9965749f"), "GMC", "Blue", 4658659m, "Germany", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2006, "Sierra 3500HD" },
                    { new Guid("c9e43e43-2680-47f4-a7e4-9d7f17b5f3b1"), "Lexus", "Red", 4954800m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1996, "ES" },
                    { new Guid("cac3e078-0041-4b0b-9dae-be5f04a08448"), "Toyota", "Green", 1059494m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1993, "Corolla" },
                    { new Guid("dfd31b9c-5b8f-4eff-9e98-ddf8c2d34d75"), "Land Rover", "Teal", 3352603m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2009, "Freelander" },
                    { new Guid("19345f2d-396b-4613-b478-d02995bf19d1"), "Buick", "Puce", 2333168m, "France", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1995, "Regal" },
                    { new Guid("85a97963-3990-4653-94c5-77be976d8b2f"), "Kia", "Red", 2111952m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2008, "Carens" },
                    { new Guid("3f3ba7fa-dc0d-4910-8ad5-fcb97ec57cbc"), "Ford", "Purple", 890934m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2003, "Explorer" },
                    { new Guid("fb495ba1-9e76-4525-93e9-759ba072dc30"), "Ford", "Khaki", 3108309m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1986, "LTD" },
                    { new Guid("d2962da6-e003-453e-8e42-98903678a03a"), "Subaru", "Mauv", 3962737m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2011, "Outback" },
                    { new Guid("a7ff8228-69b3-4483-9c76-1539a686ae84"), "Pontiac", "Mauv", 2137824m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2000, "Bonneville" },
                    { new Guid("c330d4f6-fce2-4fa7-b3e3-785975285f0d"), "Toyota", "Pink", 1193637m, "Poland", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1995, "Corolla" },
                    { new Guid("5cc07564-8bb7-45dd-9ec8-ea3ad8b6bb20"), "Audi", "Yellow", 1158288m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2003, "S6" },
                    { new Guid("9af00f67-8e57-4d5b-82a6-971777d8d10e"), "Volvo", "Turquoise", 4434375m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1999, "C70" },
                    { new Guid("537e415e-1917-46c9-8d00-f362d2939cc8"), "Volkswagen", "Maroon", 3198103m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1988, "Type 2" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("60d7ef46-ac21-461c-ac76-9e67046ab565"), "Mazda", "Blue", 263540m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1994, "Navajo" },
                    { new Guid("64620f85-0819-4bf4-96bd-4537309b363d"), "Mitsubishi", "Purple", 2630933m, "Czech Republic", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1992, "Precis" },
                    { new Guid("e37bbea3-314c-4b11-8820-ad0353f71c63"), "Chrysler", "Khaki", 917326m, "Serbia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1992, "Town & Country" },
                    { new Guid("5f7d36c7-c16d-4c6d-814a-3995f89dfd32"), "Chevrolet", "Turquoise", 4274414m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1998, "Lumina" },
                    { new Guid("74036a60-e011-4061-981e-decf2215ac32"), "Toyota", "Blue", 3238697m, "Japan", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1998, "RAV4" },
                    { new Guid("85b3a498-bffb-4cc4-a69f-e33a3d43aa4d"), "GMC", "Mauv", 2492622m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1999, "Suburban 2500" },
                    { new Guid("23c094c1-3e60-41bd-a738-fa3b9cff6eb3"), "Volvo", "Khaki", 3761419m, "Czech Republic", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2004, "V70" },
                    { new Guid("4845efd1-d031-4ce1-bce1-a0a17cf0cc20"), "Mitsubishi", "Maroon", 1717347m, "France", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1994, "Truck" },
                    { new Guid("25c46042-1676-44e0-a976-4595878e3ab5"), "Mazda", "Indigo", 2470024m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2010, "Mazda5" },
                    { new Guid("2787a662-d771-4b2c-a88a-cf5f537c759b"), "BMW", "Orange", 1470221m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2012, "X5 M" },
                    { new Guid("b5d0fd93-c59d-4354-a1a0-6a80e42fe78d"), "Mercedes-Benz", "Red", 1631055m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2009, "SLK-Class" },
                    { new Guid("09d555f1-3315-449e-a4fe-e84e64ca106b"), "Buick", "Pink", 336981m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2000, "Park Avenue" },
                    { new Guid("5b94c8fa-3243-4c1c-a4b9-96f8928d90f5"), "Subaru", "Turquoise", 1644231m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1992, "Legacy" },
                    { new Guid("0746bba5-0e8e-4867-9716-37b99344df64"), "Ford", "Crimson", 2772310m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2003, "Escape" },
                    { new Guid("9caba485-ebee-4572-8c47-f0c62eb11b02"), "GMC", "Crimson", 4229342m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2011, "Terrain" },
                    { new Guid("34a28a18-0ee4-4c9e-9272-df83ccd302e3"), "Mitsubishi", "Purple", 2117064m, "Sweden", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1993, "Pajero" },
                    { new Guid("8297751a-47dc-4f0e-bfe6-16e267af98aa"), "Buick", "Aquamarine", 4040135m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2004, "Rendezvous" },
                    { new Guid("1f3e8294-d26c-4dc1-81cd-7e421e5dbf03"), "Volvo", "Aquamarine", 2798016m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2012, "S60" },
                    { new Guid("9ac2feaa-2ebf-464b-b20d-e1f38ae53009"), "GMC", "Fuscia", 3283433m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1999, "Envoy" },
                    { new Guid("112b658b-6371-479b-be5f-d2ab4a0efd1d"), "Chevrolet", "Red", 1186686m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1993, "Cavalier" },
                    { new Guid("0e4d3027-8479-4812-beb9-9b363e9a4a65"), "Chevrolet", "Turquoise", 4286840m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1997, "Astro" },
                    { new Guid("de757e49-c779-4fa2-a605-2f28d0c6e408"), "Mitsubishi", "Khaki", 4769359m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1998, "Pajero" },
                    { new Guid("43a98aeb-51c2-4524-9794-64a27045dde7"), "Lincoln", "Purple", 441733m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2011, "Town Car" },
                    { new Guid("a1be3635-71c1-4083-a2fd-d98ec90ca929"), "Toyota", "Turquoise", 576913m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2009, "Matrix" },
                    { new Guid("6cada8df-7414-4982-ac12-27cdcbbd19d7"), "Chevrolet", "Khaki", 632844m, "Colombia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1996, "Sportvan G30" },
                    { new Guid("15b365b6-5531-4e3c-aacc-3bfb93a6f22c"), "Chevrolet", "Yellow", 771156m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1969, "Corvette" },
                    { new Guid("b05ec967-0d26-44ed-a879-a6e435057819"), "MINI", "Indigo", 221456m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2008, "Clubman" },
                    { new Guid("f9804400-b47d-4c79-919c-4b65c58f4c43"), "Pontiac", "Mauv", 3744802m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1996, "Sunfire" },
                    { new Guid("a6a58f36-6dff-4677-8990-3d66e1d1e00f"), "Nissan", "Pink", 841679m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1993, "240SX" },
                    { new Guid("f7d350af-7071-41d8-bb59-30c7e05317db"), "Pontiac", "Khaki", 961404m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1972, "Grand Prix" },
                    { new Guid("7c932668-aaee-4e68-9ca5-8cff6de27893"), "Jaguar", "Orange", 1003111m, "Japan", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2005, "XK Series" },
                    { new Guid("7e7b055b-1d1e-489c-afee-f3151131e9f3"), "Pontiac", "Turquoise", 1087266m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1986, "Fiero" },
                    { new Guid("983db6f5-888e-44db-ac1c-9a4ad9091872"), "Porsche", "Teal", 3968433m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2011, "911" },
                    { new Guid("841b280f-7eea-4264-85b4-f0142bdfb07a"), "Chevrolet", "Maroon", 711278m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2005, "Malibu" },
                    { new Guid("def7767a-b17e-493d-9541-4e058ab5e91d"), "Toyota", "Teal", 1063078m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1998, "T100 Xtra" },
                    { new Guid("2624623c-4d6e-4cee-9c87-1f7fc4c89fd8"), "Volvo", "Crimson", 3042516m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2013, "C70" },
                    { new Guid("87ef7872-dd82-470d-964e-05e3ebbcaae5"), "Lincoln", "Green", 687839m, "Colombia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2006, "LS" },
                    { new Guid("7c18dd03-eaa8-4fb8-af2a-50ad2541adeb"), "Lexus", "Orange", 1755957m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2000, "LS" },
                    { new Guid("18cf7341-7879-45ed-9947-f6b629d65047"), "Subaru", "Fuscia", 3460846m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2004, "Forester" },
                    { new Guid("4524ded6-0e2b-49d1-aab7-dfcf64de05ee"), "GMC", "Violet", 2710798m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2005, "Envoy XUV" },
                    { new Guid("73863574-6483-4188-8e32-fe207b0ca445"), "Suzuki", "Aquamarine", 4149156m, "Sweden", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2007, "XL7" },
                    { new Guid("f3aa854f-1986-4b6a-b446-c0db54876c95"), "GMC", "Fuscia", 1028284m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1995, "Vandura G2500" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("df0103fa-23a5-40a0-8f9a-3f9e0c53dbcf"), "Volkswagen", "Red", 2332526m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1998, "rio" },
                    { new Guid("04c1da28-e809-498e-9f4b-9b8bf7b3ad4d"), "Saab", "Turquoise", 1160561m, "Poland", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2008, "9-5" },
                    { new Guid("a31196b5-4aab-42bd-a7a6-1b9672d95416"), "Mitsubishi", "Indigo", 3623865m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1997, "Challenger" },
                    { new Guid("a48cf2d1-c9f6-43f3-961f-1f0080da9a85"), "Ford", "Aquamarine", 3805756m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2003, "Escape" },
                    { new Guid("5fbd7711-67ac-4dd0-95ca-76b1fea7ded9"), "Pontiac", "Fuscia", 4180691m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1971, "GTO" },
                    { new Guid("1e50b069-4920-43fe-adf6-a0c2f3bcf59b"), "Dodge", "Puce", 3566386m, "Germany", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2004, "Ram 2500" },
                    { new Guid("dcadcc6a-1ca3-4e2b-94c3-1c6a8eea3d51"), "Hyundai", "Pink", 1219587m, "Sweden", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2002, "Sonata" },
                    { new Guid("3d195aaa-45cc-424b-9a64-98001288aa62"), "Mazda", "Red", 3899960m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1992, "RX-7" },
                    { new Guid("ba28a786-0b7b-4670-ad43-2cb54260c1d4"), "Acura", "Khaki", 849358m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1997, "NSX" },
                    { new Guid("39fcf857-864b-41ab-b789-287d05c408bf"), "Jaguar", "Crimson", 2645187m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2008, "XJ" },
                    { new Guid("de5bde88-b8a8-4e86-b0a8-b62bcc9dd872"), "Kia", "Khaki", 4877890m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2012, "Sedona" },
                    { new Guid("58e5eefc-8e77-480a-a53e-9a81e82e4933"), "Toyota", "Pink", 4557768m, "France", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2008, "Tundra" },
                    { new Guid("c4558c7b-c94e-4a7a-8804-08e15faabcdf"), "Chevrolet", "Violet", 3267327m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1993, "1500" },
                    { new Guid("b19a26f3-d8a9-4074-9cae-688bf09680c5"), "Ford", "Khaki", 2522305m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2002, "Explorer" },
                    { new Guid("905d1bba-b415-4588-b3bb-2c0f0e04586f"), "Buick", "Crimson", 1853718m, "Japan", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1993, "Coachbuilder" },
                    { new Guid("90761784-375e-4bf6-9f6b-a83b2ad7fe7c"), "Bentley", "Pink", 4874180m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2010, "Continental Flying Spur" },
                    { new Guid("8ab6c2ee-5ed4-499d-811a-26e5b68b9a34"), "Hyundai", "Purple", 2623945m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2000, "Sonata" },
                    { new Guid("e9d23b5d-2c83-4be9-8f88-fde1c3d0f6d1"), "Eagle", "Fuscia", 1418934m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1992, "Summit" },
                    { new Guid("cb1c3ebe-1032-4154-8095-91edaa76b4f3"), "Jeep", "Indigo", 3158772m, "Sweden", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2006, "Grand Cherokee" },
                    { new Guid("96338d77-4c1a-498d-97ee-caecf53c38fc"), "Panoz", "Indigo", 3602434m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2008, "Esperante" },
                    { new Guid("cba508ec-a8d3-4a1e-a09e-207878d788ee"), "Dodge", "Purple", 961680m, "Czech Republic", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1992, "Ram Wagon B250" },
                    { new Guid("ef381db1-bdd7-4f28-aecb-7f42b6bec16b"), "Pontiac", "Aquamarine", 3644103m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1998, "Firebird" },
                    { new Guid("1dfc6765-d0aa-48fc-9fae-27de08ce64a6"), "Audi", "Pink", 1235199m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2009, "S4" },
                    { new Guid("ce498971-3988-473c-ba5c-e969094d4aff"), "Ford", "Fuscia", 2579319m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2006, "Freestyle" },
                    { new Guid("6c6c0ec3-7ae0-4b6d-9cc2-e01776ca7fe4"), "Saab", "Crimson", 524330m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2011, "9-4X" },
                    { new Guid("5a393ca3-fb80-45cc-890d-ab0a9468456b"), "Pontiac", "Pink", 393882m, "Czech Republic", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1991, "Firefly" },
                    { new Guid("1411e3f8-1442-46e1-83c3-dff2d1db33a4"), "BMW", "Pink", 3086926m, "Colombia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2006, "7 Series" },
                    { new Guid("ff770043-a544-421c-8053-f5bc2a63239d"), "Isuzu", "Indigo", 4744960m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1996, "Trooper" },
                    { new Guid("aa29ee72-f5b3-4c40-9326-53968e109409"), "Toyota", "Red", 1500461m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2002, "Camry" },
                    { new Guid("0bc761e2-cb50-4d66-babe-83e7d398c513"), "Toyota", "Aquamarine", 4638174m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1997, "Tacoma" },
                    { new Guid("84a84dad-c1fa-4086-848c-627115f446f7"), "Infiniti", "Aquamarine", 470866m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1996, "J" },
                    { new Guid("51ef3194-d677-46cc-9e60-ed9ac97a2b0e"), "Volkswagen", "Pink", 679436m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2002, "Golf" },
                    { new Guid("baab7011-50ec-4a70-8d19-8769fca7e0e9"), "Eagle", "Pink", 4280021m, "France", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1992, "Summit" },
                    { new Guid("5526492f-c06b-42f9-b27a-4e2babdd6f26"), "Geo", "Violet", 4328377m, "Czech Republic", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1993, "Storm" },
                    { new Guid("440219a8-71f5-434c-8c05-9cc8c0542f3e"), "Ford", "Violet", 1382322m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1991, "Tempo" },
                    { new Guid("4760e238-f5f9-4d1f-bf00-7465f68acf0e"), "Lexus", "Teal", 2727471m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2007, "IS" },
                    { new Guid("f58e8a1e-1c1c-479c-892e-02122ddf836b"), "Mercedes-Benz", "Yellow", 2953946m, "Serbia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2005, "G-Class" },
                    { new Guid("5e605a0e-0f8f-4f55-8181-0a1a29af6733"), "Mitsubishi", "Aquamarine", 690217m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1999, "3000GT" },
                    { new Guid("b811b47b-40f0-4e02-9e3e-e0e3c564cacc"), "Chevrolet", "Khaki", 685887m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2012, "Impala" },
                    { new Guid("189e94f6-02e0-44c6-bae8-bbd0a2a1b128"), "Nissan", "Indigo", 1070147m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1991, "Sentra" },
                    { new Guid("4219b259-c9fb-414a-a0c8-cd3d22ceee6b"), "Lincoln", "Pink", 367858m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1989, "Continental Mark VII" },
                    { new Guid("df5d15a1-57dd-4bc3-8b04-f607145a698d"), "Bentley", "Blue", 3310274m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2012, "Continental Super" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("5d83e0cc-fd1d-4e05-be70-2bb2fae2f4b9"), "Lincoln", "Goldenrod", 417093m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2009, "MKS" },
                    { new Guid("5ad15f8e-5622-4a57-a51a-78c3fd8ff969"), "Mazda", "Red", 2558002m, "Sweden", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1988, "Familia" },
                    { new Guid("512cc543-9336-481a-b2a6-7a77dafa070f"), "Hyundai", "Maroon", 898523m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2002, "Santa Fe" },
                    { new Guid("f7903a59-be4f-45e2-9726-f1a379f2e2e6"), "Ford", "Khaki", 2152901m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1992, "Bronco" },
                    { new Guid("a54cf754-260a-4464-9394-cfeb0fe33b88"), "Subaru", "Teal", 1831550m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1994, "Legacy" },
                    { new Guid("01de55e4-6361-43d5-bff1-772b0160b23a"), "Lexus", "Turquoise", 2736355m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2005, "LS" },
                    { new Guid("af920be5-2417-407c-820e-fb04bf3b49ff"), "Honda", "Puce", 1292845m, "Serbia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1996, "Odyssey" },
                    { new Guid("61d8af97-3a2c-46c2-a1b1-cc3c2bfbf1d8"), "Toyota", "Goldenrod", 3113684m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2012, "Prius c" },
                    { new Guid("b35e39e6-8069-48d2-9dcc-1f9a8da7ad76"), "Mercedes-Benz", "Turquoise", 1778753m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1993, "300D" },
                    { new Guid("bf81fbf4-2483-4cbb-a5eb-7fb8afc20ff1"), "Kia", "Orange", 3315657m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2004, "Sorento" },
                    { new Guid("c5986067-09fe-47e6-b311-ed8c7ff284d7"), "Lincoln", "Aquamarine", 2187254m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2006, "LS" },
                    { new Guid("676656d0-c0a0-4935-9e2b-38989b0594ca"), "Dodge", "Violet", 2096730m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1992, "D250" },
                    { new Guid("9629b144-a198-4ee5-86fe-4acf79dc5925"), "BMW", "Fuscia", 1473948m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1998, "3 Series" },
                    { new Guid("4019e7bb-0c78-4915-8a9c-a99c6f8ab25c"), "Jeep", "Mauv", 1993038m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1997, "Wrangler" },
                    { new Guid("a67027f7-2e28-4652-884b-dc8c3e5dad93"), "Toyota", "Violet", 3928599m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1996, "Avalon" },
                    { new Guid("db2af17c-b591-4a48-b580-2e2f74c6edb5"), "Mitsubishi", "Indigo", 546131m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1994, "RVR" },
                    { new Guid("5db121f1-f2ad-4968-87b7-4bf982883ca3"), "Buick", "Maroon", 599518m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2004, "Regal" },
                    { new Guid("822b866b-7b27-4fdb-9166-4eb100dd7409"), "Chevrolet", "Green", 1229002m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2002, "Avalanche" },
                    { new Guid("4b7a602a-af97-4bae-986f-a06f6d30852f"), "Hyundai", "Yellow", 3801469m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2005, "Santa Fe" },
                    { new Guid("eafcf801-92c5-4364-bc5b-d52f8c9908f0"), "Dodge", "Pink", 638195m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2003, "Ram Van 2500" },
                    { new Guid("a4f15d4b-75d3-4711-b023-5264e45cc3c6"), "Kia", "Aquamarine", 3526365m, "Colombia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2001, "Optima" },
                    { new Guid("64dc6da2-c773-47c5-ab98-58dbb4bb11b9"), "Mitsubishi", "Yellow", 2165441m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1995, "Eclipse" },
                    { new Guid("2d51d433-5587-4932-9307-30530bc29766"), "Nissan", "Turquoise", 3071571m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2007, "Frontier" },
                    { new Guid("b4299dce-6e75-49c7-8f45-ca69d478bbd3"), "Volkswagen", "Maroon", 4704763m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2011, "Jetta" },
                    { new Guid("55dfbf7e-ca51-4815-b146-92ef18fdecb5"), "Toyota", "Maroon", 223197m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2000, "RAV4" },
                    { new Guid("c78e3c31-9aef-459d-884a-6e5a0a1b1dd1"), "Chevrolet", "Goldenrod", 1545897m, "Japan", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1996, "Cavalier" },
                    { new Guid("0e659d14-7eb2-4a96-92e7-eed930cb93f7"), "Mazda", "Blue", 4444988m, "Colombia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1990, "Familia" },
                    { new Guid("0e3894f4-fe7f-4676-a4d6-7aeac5657b74"), "Mitsubishi", "Puce", 1653908m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1997, "Diamante" },
                    { new Guid("89718735-3bb9-449c-9010-7ffa52f9fedc"), "Ford", "Maroon", 2319727m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1996, "Thunderbird" },
                    { new Guid("2eabf795-bd92-46ce-b079-85e212fb028d"), "Dodge", "Fuscia", 1292488m, "Poland", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2001, "Neon" },
                    { new Guid("17efb001-845d-4b18-a8b9-ee0af069d510"), "Mitsubishi", "Turquoise", 2269883m, "Sweden", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2011, "Endeavor" },
                    { new Guid("fd1f7f56-77c5-4951-892f-14edaedbc779"), "Mitsubishi", "Violet", 1605012m, "Sweden", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2004, "Montero Sport" },
                    { new Guid("440bb2f4-ed75-4f01-99c5-7f1a3633d82f"), "Jeep", "Khaki", 978461m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2010, "Liberty" },
                    { new Guid("5edcb828-4bea-432d-9acd-357d2cbdb7ec"), "Kia", "Mauv", 4270115m, "Serbia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2005, "Sportage" },
                    { new Guid("b86568da-2514-42a8-91d0-0683adab4e2e"), "Morgan", "Pink", 1102908m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2008, "Aero 8" },
                    { new Guid("461763c3-db51-4a8e-8d70-a91844f2e4b2"), "Tesla", "Blue", 3087680m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2011, "Roadster" },
                    { new Guid("e956260b-7648-4d80-9cf4-c9a195b64b9e"), "Chevrolet", "Maroon", 3448353m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2007, "Uplander" },
                    { new Guid("cb41a118-8678-4720-8514-696302fdeb44"), "Mazda", "Green", 3235366m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2000, "626" },
                    { new Guid("575ef4e4-cdd4-4c36-afa3-0ed15a1653ec"), "Jeep", "Khaki", 1039063m, "Colombia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2012, "Liberty" },
                    { new Guid("20f8b08a-8946-4e63-b41a-dbf558820141"), "Geo", "Orange", 2697913m, "Sweden", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1997, "Metro" },
                    { new Guid("4ae34098-4870-4661-af98-580e22bbfa53"), "Mitsubishi", "Yellow", 2604187m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1989, "L300" },
                    { new Guid("48f01dfc-cc12-47d6-845a-d2e6185b81be"), "Mitsubishi", "Purple", 355513m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2001, "Diamante" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("0c7e8d75-c80d-45d0-a03c-dad3dbfab81b"), "Mercury", "Red", 4158212m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2005, "Montego" },
                    { new Guid("0756aac3-d08b-49f5-a84c-9168189472b7"), "Ford", "Orange", 3114576m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2003, "E250" },
                    { new Guid("a45cbfa9-509b-4006-ba5a-d0a87583da62"), "Lincoln", "Violet", 4496433m, "Sweden", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1991, "Continental Mark VII" },
                    { new Guid("c1ecf53b-5f4a-4742-af37-5c5b0a274256"), "Chevrolet", "Green", 390372m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1998, "Malibu" },
                    { new Guid("d81fa49e-e8a6-444b-a487-fe186a7bfef2"), "Chevrolet", "Aquamarine", 1161312m, "Czech Republic", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1992, "3500" },
                    { new Guid("fa64d609-a294-4526-8d85-efbda91276d1"), "Ford", "Puce", 3767944m, "France", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2007, "Focus" },
                    { new Guid("8eea5097-fb7c-4a59-abce-38ad31f5fed5"), "GMC", "Aquamarine", 394028m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2000, "Savana 2500" },
                    { new Guid("7a2c0642-b71e-40ef-8ce1-e205ce7bdf07"), "Toyota", "Blue", 2639856m, "Japan", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2011, "Corolla" },
                    { new Guid("831c7a04-ffd4-4550-af83-3f996f2bc6e4"), "Dodge", "Goldenrod", 2759682m, "Czech Republic", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2010, "Avenger" },
                    { new Guid("98424105-f014-4179-9075-e21a126f3669"), "Volkswagen", "Mauv", 3081583m, "Japan", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1999, "New Beetle" },
                    { new Guid("c13ea519-2827-46fb-b8cc-e4a80cf2517a"), "Chevrolet", "Mauv", 2712716m, "Poland", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2008, "Suburban 2500" },
                    { new Guid("d29978df-8bf6-43ef-bb29-618789d14bcc"), "Acura", "Teal", 2439103m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2004, "MDX" },
                    { new Guid("fac9cd21-28b3-450f-944f-d1397830ec36"), "Suzuki", "Turquoise", 1403671m, "France", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2007, "Reno" },
                    { new Guid("d15b1cc0-f5f2-446a-9997-28e068eddd5c"), "Toyota", "Indigo", 3267919m, "France", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2009, "Highlander" },
                    { new Guid("147c84e5-d127-4034-951e-eac8f38fe8bf"), "Lotus", "Turquoise", 2373838m, "France", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1998, "Esprit" },
                    { new Guid("6d844b21-7cab-449f-b6b6-36fdd044ba3c"), "Volkswagen", "Red", 994665m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2012, "New Beetle" },
                    { new Guid("9f33ca4d-0d94-41c4-afa0-5b8b9cc0dc77"), "Jeep", "Puce", 3005144m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2001, "Cherokee" },
                    { new Guid("24b0059e-1594-43fa-9dd5-5911f4bf6a91"), "Chevrolet", "Yellow", 805144m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2004, "TrailBlazer" },
                    { new Guid("e7afe3f6-c60b-4e29-aaa1-c8f8a886dfab"), "Ford", "Goldenrod", 666596m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2005, "Five Hundred" },
                    { new Guid("b3b75256-449c-45f1-a851-f5af6ecbe942"), "Land Rover", "Teal", 1986460m, "Sweden", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1992, "Defender" },
                    { new Guid("f1ef3f2c-26de-4944-805c-34d3aafcd798"), "Chrysler", "Khaki", 2639550m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2004, "300M" },
                    { new Guid("1d6c6faf-368f-45ca-b829-b88a3de503b2"), "Plymouth", "Green", 3173449m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1992, "Colt Vista" },
                    { new Guid("d8134257-195b-41b0-9053-9badb475adac"), "Subaru", "Mauv", 3296362m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1995, "Impreza" },
                    { new Guid("6d2c1498-f66c-4ed7-ac6b-ed41c0212f81"), "Pontiac", "Violet", 3605163m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2008, "G5" },
                    { new Guid("a95e5cca-5b31-4807-9638-32d46cbd1b7c"), "Ram", "Crimson", 699362m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2011, "1500" },
                    { new Guid("8db0cc3f-c310-46d6-9ad5-5067b851484b"), "Acura", "Yellow", 1986994m, "Japan", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1990, "Legend" },
                    { new Guid("e9a209ec-411b-4b29-a9e8-c66f1352aee0"), "Volkswagen", "Blue", 1697156m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1984, "Golf" },
                    { new Guid("d84c94d7-5ebc-4528-8b84-a15a2f783004"), "Hyundai", "Yellow", 471857m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2011, "Elantra" },
                    { new Guid("0a30c792-06ce-4845-a4ac-240a35fb0fc1"), "Dodge", "Teal", 595881m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1998, "Neon" },
                    { new Guid("a82daf59-b46a-40fe-8eb5-b97cbbd8aac7"), "Mitsubishi", "Mauv", 309637m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1987, "Cordia" },
                    { new Guid("f1ac09e9-5cd9-4db6-8fb6-e9ce8d51420d"), "Dodge", "Purple", 1445137m, "Spain", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1995, "Ram 1500 Club" },
                    { new Guid("a5953838-f271-48ee-88a2-36b12f08e8b3"), "Ram", "Turquoise", 836972m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2011, "1500" },
                    { new Guid("60d384b0-561c-4649-9726-cdaba594f783"), "Mercury", "Green", 3208473m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2008, "Mountaineer" },
                    { new Guid("05dcdf8e-519f-4b22-a3d3-5ea67f53177e"), "GMC", "Violet", 3210402m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2003, "Sierra 2500" },
                    { new Guid("d27aa4e5-63b7-4551-aa7d-91529c30f40c"), "Volvo", "Violet", 569295m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2010, "S40" },
                    { new Guid("3e1deb49-a266-4a16-9872-873587a47b54"), "Lexus", "Violet", 672327m, "Poland", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1996, "LS" },
                    { new Guid("90b48f6b-45c0-4843-82dc-dc6cae8185f4"), "Toyota", "Turquoise", 1366954m, "Colombia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1992, "Land Cruiser" },
                    { new Guid("c270c78d-379f-4f7e-b6df-412dd3f139bf"), "Suzuki", "Green", 1721621m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2004, "Vitara" },
                    { new Guid("f55c6e3c-40aa-40ea-915b-86768fa5b639"), "Audi", "Teal", 4852035m, "Japan", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1994, "90" },
                    { new Guid("8ec8850b-cff4-47ee-abcd-9480f3c0547b"), "Chrysler", "Maroon", 1504205m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1998, "Sebring" },
                    { new Guid("7e10fa11-f719-49a0-95ca-82cbe8cd5f26"), "Acura", "Pink", 1487738m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1997, "CL" },
                    { new Guid("c6d9a865-db68-4348-b340-2cad486fe021"), "Chevrolet", "Aquamarine", 3578399m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1967, "Camaro" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("4b0c1ab4-0136-40fd-ba17-7735ec751667"), "Toyota", "Crimson", 3805745m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1997, "Land Cruiser" },
                    { new Guid("7609aff4-aea8-40cf-9c76-077a368778a5"), "Toyota", "Red", 3520870m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2008, "Yaris" },
                    { new Guid("d3357d22-7ba4-45b9-83f7-18e3d15cb158"), "Chevrolet", "Orange", 2858424m, "France", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2003, "Tracker" },
                    { new Guid("9d6c70b7-3e4a-47ce-a54f-4768e011dea9"), "Subaru", "Crimson", 4812064m, "Poland", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1989, "Legacy" },
                    { new Guid("558c2a8b-5089-4cdd-b809-e5fdfc1387ff"), "Honda", "Turquoise", 2179571m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2007, "Accord" },
                    { new Guid("001ec7a0-91e7-4b54-a426-0503b36d1219"), "Subaru", "Puce", 3516652m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1994, "Alcyone SVX" },
                    { new Guid("f120acf4-af59-4b8f-b28b-972401c4d388"), "Acura", "Violet", 1934908m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2010, "TSX" },
                    { new Guid("0cb22270-0422-4718-b292-548f6a67a9d6"), "BMW", "Pink", 4249463m, "Colombia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2007, "3 Series" },
                    { new Guid("e45dfff2-73dd-4890-9444-a8de1694b0cc"), "Volkswagen", "Goldenrod", 3999420m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1999, "Passat" },
                    { new Guid("4bb469cc-689b-4c48-bc6f-a9d2995a5e50"), "Aston Martin", "Mauv", 1404139m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2008, "DBS" },
                    { new Guid("035a742f-569d-4a2a-8861-9809d62f9018"), "Volkswagen", "Puce", 3177946m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1993, "Eurovan" },
                    { new Guid("444af591-0ab2-42a3-b036-e3083eef56ea"), "Pontiac", "Pink", 856903m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2008, "G8" },
                    { new Guid("abef6221-efe0-4760-a80b-f4e3ac864dd3"), "Volkswagen", "Goldenrod", 4809286m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1998, "Golf" },
                    { new Guid("bebc51d3-0439-4962-997d-1d803c3b951f"), "Chevrolet", "Maroon", 4607732m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2008, "Cobalt" },
                    { new Guid("f046a0bc-f913-462e-89a6-f8fd4d842bc1"), "Dodge", "Indigo", 1533334m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2012, "Journey" },
                    { new Guid("a4dd90ff-1b9a-48dc-8e51-3dce525100cb"), "Buick", "Maroon", 2346043m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1999, "LeSabre" },
                    { new Guid("cf23f6e9-db61-4ccf-803a-67adc31d5097"), "Land Rover", "Fuscia", 3199799m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1987, "Range Rover" },
                    { new Guid("21263631-9fd6-468d-b6e7-d2c71f8f7b74"), "Ford", "Khaki", 1213130m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1965, "Mustang" },
                    { new Guid("f14f22e0-38fa-461d-8a2d-f392c827ac95"), "Kia", "Teal", 3694349m, "Colombia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2011, "Forte" },
                    { new Guid("7d4c075c-63bf-4ce9-be8a-97bd04f80cca"), "Chevrolet", "Indigo", 3067639m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2006, "HHR Panel" },
                    { new Guid("91ae1272-a43f-4b7d-8923-a4ffe7d59bb8"), "Maybach", "Blue", 4172427m, "Sweden", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2007, "57" },
                    { new Guid("0109a73b-1127-4cf8-a79a-4564aeaae4e0"), "Pontiac", "Fuscia", 2445431m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1988, "Grand Am" },
                    { new Guid("def6cc1d-b6eb-4c8e-9059-0ec7bb865644"), "Mitsubishi", "Mauv", 2638396m, "Colombia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1994, "RVR" },
                    { new Guid("d3c68644-59ac-4257-a0a1-ac76e4759d10"), "Saturn", "Yellow", 3180100m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2006, "Ion" },
                    { new Guid("dfd9216c-0132-4c6c-b375-5b42653a5cb8"), "Dodge", "Orange", 613184m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1992, "Dynasty" },
                    { new Guid("87ab2464-c7a7-43b2-b59e-a14644ef91f7"), "Saturn", "Goldenrod", 2300111m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2008, "Outlook" },
                    { new Guid("da246c68-f490-49a4-89d7-f40deeef4933"), "Volkswagen", "Turquoise", 2827327m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1998, "New Beetle" },
                    { new Guid("2b6d9acf-9846-4db2-a37f-6359b387a4ca"), "GMC", "Purple", 4768599m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2006, "Yukon" },
                    { new Guid("ccffcd3a-a5e5-4d93-8ceb-1b97cb03af66"), "Mazda", "Orange", 3298471m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2004, "MX-5" },
                    { new Guid("ed9edf75-53dd-489f-960d-7d99b47e0f3f"), "Toyota", "Green", 1131382m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2012, "Tundra" },
                    { new Guid("209a5e11-0e0c-40aa-ab21-552ce3e51ac2"), "Mazda", "Aquamarine", 2713260m, "Colombia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1989, "626" },
                    { new Guid("9e480ee3-e788-44f5-95d1-605435b99134"), "Audi", "Indigo", 4085250m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2008, "RS4" },
                    { new Guid("f29b77d2-96a2-4cb0-9f60-ba15aece97ad"), "Dodge", "Violet", 2551733m, "Poland", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1992, "Ram Wagon B150" },
                    { new Guid("13f00b35-9032-44ba-8e37-db989cd78113"), "GMC", "Goldenrod", 4823385m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2006, "Savana" },
                    { new Guid("989d4239-0e1f-476d-a907-241c7774f037"), "Porsche", "Crimson", 396339m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1984, "944" },
                    { new Guid("2caafe71-a089-4139-ba1c-e5b860ba1534"), "Mercedes-Benz", "Red", 1654558m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2005, "E-Class" },
                    { new Guid("760c65da-67bd-42d1-b05f-107b8741df28"), "GMC", "Green", 4375143m, "Serbia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1993, "Suburban 1500" },
                    { new Guid("973e6411-20ac-4e74-bc54-0c1e1133fb13"), "Cadillac", "Yellow", 1134051m, "Sweden", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1998, "Seville" },
                    { new Guid("d2e43a9f-2360-474c-ad9b-fd0541b13d03"), "Daewoo", "Fuscia", 4194747m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2000, "Nubira" },
                    { new Guid("ac54e637-8962-4490-88d7-f2369f157e38"), "Cadillac", "Turquoise", 467422m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2006, "Escalade EXT" },
                    { new Guid("b0684b7d-e361-4966-80cc-9be68e140589"), "Oldsmobile", "Mauv", 3476095m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1992, "Bravada" },
                    { new Guid("72c41c08-dc1f-487d-b724-6c878a534cfd"), "Buick", "Yellow", 4088340m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2009, "LaCrosse" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("b32e8018-68ac-4ab2-9c5d-a242434055e0"), "Subaru", "Goldenrod", 4455842m, "Poland", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2000, "Impreza" },
                    { new Guid("930a7a9f-ce38-4721-b85f-81d8d0097221"), "Mercedes-Benz", "Teal", 3824189m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1984, "S-Class" },
                    { new Guid("faaca8a3-6450-41d8-bd6d-d02709ca8a51"), "GMC", "Mauv", 1473736m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2002, "Savana 2500" },
                    { new Guid("b891cc12-11ff-494a-913f-84115da1df86"), "Land Rover", "Pink", 3202262m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1989, "Range Rover" },
                    { new Guid("a12beeec-da35-4c8e-b139-f140fd82f5db"), "Honda", "Turquoise", 2598039m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1996, "Civic" },
                    { new Guid("fdb851a0-b6e1-4c4a-bebd-18a043ae2b97"), "Maybach", "Maroon", 832760m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2003, "62" },
                    { new Guid("8171c190-2c5a-4345-8fa1-217a4258df4c"), "Lincoln", "Violet", 3800132m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1992, "Mark VII" },
                    { new Guid("f614f79b-a993-4676-86b7-d2e3be555e9a"), "Mercedes-Benz", "Blue", 2477695m, "Japan", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2009, "SL-Class" },
                    { new Guid("914d3364-93b4-40ac-85d1-ec47de96a3dd"), "Buick", "Puce", 3738263m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2011, "LaCrosse" },
                    { new Guid("cb46f382-e312-4166-b0ee-fd629a07a16f"), "Pontiac", "Mauv", 3857258m, "France", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1990, "Sunbird" },
                    { new Guid("12043741-215e-4cf5-ac01-8b3e98091814"), "Mitsubishi", "Maroon", 1820619m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2009, "Tundra" },
                    { new Guid("8ebea521-2618-487f-9c29-a5c990f7ba4b"), "Chevrolet", "Aquamarine", 2805031m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2005, "Malibu" },
                    { new Guid("b26b374b-0e1f-40c7-9c85-74d45ad4d4ad"), "Kia", "Khaki", 3428208m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2009, "Mohave/Borrego" },
                    { new Guid("6c63a40e-2b89-46b0-ab56-75d47964fad5"), "Ford", "Khaki", 2696604m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2000, "Econoline E350" },
                    { new Guid("a58ae7e7-2cf5-4776-99de-733348ea676d"), "Jaguar", "Crimson", 3313664m, "Poland", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2002, "S-Type" },
                    { new Guid("e567237f-e412-4439-875e-c753e029c00a"), "Suzuki", "Turquoise", 2739527m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2008, "Forenza" },
                    { new Guid("6576ebc4-7ff2-4ec8-adb4-d703f5eec0ac"), "Honda", "Purple", 1590057m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1985, "Civic" },
                    { new Guid("3063e2de-b71a-47ba-95c8-c95b186739a4"), "Chevrolet", "Orange", 951992m, "Denmark", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2011, "Malibu" },
                    { new Guid("b1bbf3f3-8a35-4763-9904-8d3babdd9262"), "Lotus", "Purple", 2827602m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2005, "Elise" },
                    { new Guid("16a626d7-b3c2-479f-9aae-7605dfd182fb"), "Cadillac", "Yellow", 1990037m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1998, "Catera" },
                    { new Guid("8a4c1e71-2d95-4153-b792-9d99d2315cab"), "BMW", "Khaki", 440638m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2007, "M6" },
                    { new Guid("00447153-bd66-4f26-aa0f-28de747af29b"), "Suzuki", "Indigo", 1687598m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2007, "XL-7" },
                    { new Guid("6b026ba7-3bea-47d2-8a69-c51a6b262518"), "Toyota", "Green", 4858703m, "Japan", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2000, "Celica" },
                    { new Guid("d96f7a1b-bd1f-4cab-936c-882f03a2b54c"), "Hyundai", "Purple", 3410355m, "Poland", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2002, "Santa Fe" },
                    { new Guid("d08ffef2-8c55-40b7-8e67-068ab9835ff4"), "Hyundai", "Indigo", 3530616m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2011, "Elantra" },
                    { new Guid("b6c2d365-efc5-40c7-a9a7-13b58c6d42c9"), "Ferrari", "Maroon", 4654354m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2010, "599 GTB Fiorano" },
                    { new Guid("b3df52e4-7807-41cd-bbcd-828c7a326341"), "Ford", "Fuscia", 3580645m, "Poland", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2001, "Escort" },
                    { new Guid("6f841584-31b7-46d7-8092-cd8a232dca4e"), "Audi", "Fuscia", 886287m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1991, "V8" },
                    { new Guid("c295a8f6-0e7b-47c6-804e-dd95ce461cb7"), "Cadillac", "Red", 3163038m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2009, "DTS" },
                    { new Guid("f7a942c6-7ef2-4916-9fd9-41bfd98270b3"), "Ram", "Blue", 1747771m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2012, "1500" },
                    { new Guid("ea1487bc-a9cd-4591-84ce-a57f3bd9cff1"), "Volkswagen", "Goldenrod", 790413m, "France", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2007, "Passat" },
                    { new Guid("2f957b2d-aa56-4940-849d-c738207a7062"), "Nissan", "Teal", 1407958m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2004, "Quest" },
                    { new Guid("4b7da9de-5c3b-4185-aa51-63ad37db9a93"), "Acura", "Fuscia", 2653491m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2004, "NSX" },
                    { new Guid("3a9d683d-b25d-4ff5-b47e-bf8a013e7346"), "Cadillac", "Turquoise", 1666846m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2010, "SRX" },
                    { new Guid("8ecc6f7d-bf8d-413b-9f56-97527b0a8cf1"), "Toyota", "Indigo", 4623760m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2007, "Matrix" },
                    { new Guid("6de68e61-9041-4227-aa39-20bb2584753e"), "GMC", "Maroon", 2799216m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1998, "Envoy" },
                    { new Guid("cb9e6aab-4366-4265-955b-32ef1876bc88"), "Hyundai", "Pink", 3804823m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2005, "Accent" },
                    { new Guid("93773ba8-e1d0-42ef-a92d-e6d1b6af5513"), "Hyundai", "Goldenrod", 2076332m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2000, "Elantra" },
                    { new Guid("747c5cf0-caf3-49e1-8d0e-95e51bf205e5"), "Mercedes-Benz", "Puce", 3000176m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1993, "300E" },
                    { new Guid("e8a4769c-9c48-4003-bc6f-f54f0f4ab681"), "Dodge", "Khaki", 4350803m, "Serbia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1998, "Avenger" },
                    { new Guid("de04345c-2ec3-4fc7-a000-0306f88b5f72"), "Jaguar", "Mauv", 253095m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2008, "XJ" },
                    { new Guid("03a07112-bbaa-4025-a7bf-ee7908539626"), "Infiniti", "Turquoise", 438783m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2005, "Q" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("31dd2d5f-48c3-4295-a29a-7097c7f1568b"), "Oldsmobile", "Indigo", 2431334m, "France", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1992, "98" },
                    { new Guid("ccc00bca-37cf-404c-9d2a-0d4b150aeabb"), "BMW", "Red", 1938700m, "Japan", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2008, "M5" },
                    { new Guid("a98d5eba-7936-4939-85eb-6bf306f74f85"), "Lincoln", "Maroon", 2294290m, "Czech Republic", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1989, "Continental" },
                    { new Guid("cc76eb1d-9ee6-4501-bd87-dc5435691af1"), "Bentley", "Khaki", 1778507m, "Poland", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2007, "Azure" },
                    { new Guid("aa57d37d-aaa4-4804-91fb-ea76cda632cf"), "Mercedes-Benz", "Teal", 4063321m, "Czech Republic", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1989, "S-Class" },
                    { new Guid("3759da36-7797-4d46-89bf-5caf17ec27e0"), "Porsche", "Pink", 3751705m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2005, "Cayenne" },
                    { new Guid("7cec30ca-5738-4a84-a3db-467011c45cde"), "Lexus", "Pink", 2476062m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2010, "HS" },
                    { new Guid("2ced4823-7fe4-4bc9-a92a-5720ad082348"), "Chevrolet", "Pink", 1565619m, "Poland", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2010, "Malibu" },
                    { new Guid("5ca6f16c-2694-440d-9e6f-3d5d6a6b4fac"), "Porsche", "Green", 2325625m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1991, "911" },
                    { new Guid("43ce0070-8df7-44a1-aa03-1c1cee6e63b1"), "Lexus", "Red", 3021791m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2010, "LS Hybrid" },
                    { new Guid("5ee08637-4ee8-46d3-90c4-f8276c03a2ff"), "Ford", "Fuscia", 1135284m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1997, "F350" },
                    { new Guid("1eb195fb-4f90-45f7-9518-999b9d2fc244"), "Acura", "Fuscia", 2107515m, "Japan", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2011, "TSX" },
                    { new Guid("964237e8-e7cf-45e2-9116-6d58c9179ba1"), "Aston Martin", "Yellow", 690412m, "Poland", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2011, "Rapide" },
                    { new Guid("ac2f18fe-059f-4d53-afc1-5652ee4688ce"), "GMC", "Khaki", 4088079m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1998, "Savana 3500" },
                    { new Guid("e8773ede-6844-4d4c-bcc2-4d263688c7f4"), "Mitsubishi", "Orange", 1448758m, "France", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1987, "Chariot" },
                    { new Guid("6d74d1a0-6d0f-4897-b5ff-f1c906c0fc6b"), "Cadillac", "Mauv", 706582m, "Poland", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2011, "Escalade ESV" },
                    { new Guid("10faa120-1ad3-42a2-b41d-bb8ca72a7ad0"), "Mitsubishi", "Violet", 3468069m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2007, "Galant" },
                    { new Guid("34c96d89-f7ba-4817-8448-128220065fc5"), "Suzuki", "Khaki", 3293462m, "Czech Republic", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1989, "Sidekick" },
                    { new Guid("a0098cc4-31f9-482d-b95b-a32853686071"), "Ford", "Maroon", 4387679m, "Japan", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2005, "Crown Victoria" },
                    { new Guid("4db0bed8-ad53-43e3-a582-d8184c685702"), "Nissan", "Green", 2617879m, "Sweden", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2005, "Armada" },
                    { new Guid("419b8628-4e7e-4cfd-b6a0-85b6ca79e619"), "Ford", "Orange", 3593270m, "Sweden", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2012, "F-Series Super Duty" },
                    { new Guid("a85b2a68-1aaa-420d-9469-0b21c32847d6"), "BMW", "Orange", 2783496m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2011, "X6 M" },
                    { new Guid("eb1bd539-e9df-4910-87c9-4ed90ab61adb"), "Bentley", "Fuscia", 3693684m, "Colombia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2012, "Continental Super" },
                    { new Guid("1f09e61f-ab84-4a0c-8eda-4c1b23f9638b"), "Ram", "Orange", 2058353m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2011, "Dakota" },
                    { new Guid("c7054697-0eb4-4844-9542-014b6579e4d4"), "Mitsubishi", "Red", 2378262m, "Czech Republic", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1986, "Starion" },
                    { new Guid("e95c6c86-65e3-4003-b811-c282e73ca7f7"), "Ford", "Indigo", 3717355m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2006, "E250" },
                    { new Guid("58e35e31-1eca-4792-893d-cfa8a48caf6c"), "Chevrolet", "Fuscia", 950862m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2009, "Malibu" },
                    { new Guid("7f3a5b90-9249-42ea-80a0-0a5bbab9fe80"), "Mercury", "Fuscia", 3377441m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1995, "Sable" },
                    { new Guid("9dec177e-7b85-407d-ba74-0ea7b09cadd3"), "Mercedes-Benz", "Goldenrod", 537022m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1995, "SL-Class" },
                    { new Guid("19bc5432-7b74-418f-a0f8-341102f30b7b"), "Ford", "Teal", 3263100m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1993, "Econoline E150" },
                    { new Guid("ba7aec10-45d0-49ad-8ee6-ddbe95371796"), "Volvo", "Pink", 4747313m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2010, "XC90" },
                    { new Guid("92bced76-82ba-4f44-af74-70eb7b31a6f9"), "Plymouth", "Blue", 2863884m, "Sweden", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1998, "Grand Voyager" },
                    { new Guid("c96dc613-1372-4746-87d7-47fed78a990b"), "Suzuki", "Khaki", 409136m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2002, "XL-7" },
                    { new Guid("4bb0c8a4-bfaa-4cb2-b6ed-d4d9ae9384e5"), "Dodge", "Yellow", 4054236m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2000, "Viper" },
                    { new Guid("68bfe1d6-a659-4407-aa2a-d38b10af42b1"), "Ferrari", "Puce", 4796969m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2006, "F430" },
                    { new Guid("48e18ca1-fdca-4fa4-9c98-b57918cc1ffc"), "Saab", "Yellow", 4349940m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2006, "9-3" },
                    { new Guid("0720504b-aa55-4764-a4f5-fa401a76a985"), "Chevrolet", "Indigo", 4926444m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2008, "Silverado" },
                    { new Guid("ae98356a-c342-4bbd-9a6d-1b8a3d47893e"), "Porsche", "Goldenrod", 1520489m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2012, "Cayman" },
                    { new Guid("dfb198f2-f037-4a7c-a517-1c42c1bfb93c"), "Mazda", "Green", 1215835m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2011, "Mazda3" },
                    { new Guid("e661ed2c-b85e-43f8-b54b-bbc690feada8"), "Volkswagen", "Crimson", 1866380m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1984, "Jetta" },
                    { new Guid("17b2236f-a35a-4a00-a3ee-c92307eaae0b"), "Ford", "Pink", 4613031m, "Colombia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1985, "Bronco II" },
                    { new Guid("9bccaf1a-fe8a-4d97-a6fc-9a7e4d091177"), "Pontiac", "Aquamarine", 3479274m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1970, "Grand Prix" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("669ce77e-6c7c-4980-bf2c-f8739bbde0a0"), "Nissan", "Khaki", 998940m, "Czech Republic", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2003, "Altima" },
                    { new Guid("8e736ba6-d42d-498a-8a7d-e173ea43a556"), "Lincoln", "Indigo", 1312312m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1986, "Town Car" },
                    { new Guid("5c52cc19-77e2-4a68-8c83-014e689146d4"), "Ford", "Khaki", 2308041m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1997, "Probe" },
                    { new Guid("584be470-2fbe-49df-b952-6574917855f5"), "Volkswagen", "Red", 1134979m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2000, "New Beetle" },
                    { new Guid("9987474c-8b9e-4a2a-a65e-9094dcfc1577"), "BMW", "Pink", 2070663m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2003, "Z4" },
                    { new Guid("40ad005f-03df-4e8a-becd-27cb16059154"), "Maybach", "Green", 4706957m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2006, "62" },
                    { new Guid("5b6a8cd2-f579-4f45-8eb5-65647dade8be"), "Honda", "Indigo", 2682806m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1990, "Civic" },
                    { new Guid("40fa2a42-4251-4c3a-a750-29a802ef5d1b"), "Holden", "Maroon", 1915451m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2004, "Monaro" },
                    { new Guid("e3bd655a-a14e-4ef8-9df8-6458a8e1f4b9"), "Dodge", "Fuscia", 2818458m, "Poland", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2008, "Ram 3500" },
                    { new Guid("b936475a-015a-4fa1-addc-5eb4e04dc576"), "GMC", "Violet", 2577764m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2004, "Savana 2500" },
                    { new Guid("81de1e10-ef18-46d4-aa95-b19294cb49c5"), "Lincoln", "Pink", 2384508m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1985, "Town Car" },
                    { new Guid("64e0ef64-6d21-43ca-ba71-7cf6fe6c07bb"), "Lincoln", "Maroon", 3348027m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1988, "Continental Mark VII" },
                    { new Guid("6608646e-4042-4aa9-a816-27188015e65a"), "Mitsubishi", "Turquoise", 4339622m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2000, "Galant" },
                    { new Guid("f46c41dd-fe91-498b-a9c0-768c59fcc48f"), "Infiniti", "Crimson", 1005066m, "Colombia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2007, "G" },
                    { new Guid("2265d5d1-4e80-4c5e-91e5-d1db55ced2d4"), "Mercury", "Fuscia", 1348111m, "Japan", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1991, "Topaz" },
                    { new Guid("4413823c-8b31-422e-912e-c6d354e46865"), "Lincoln", "Khaki", 2252806m, "Czech Republic", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2007, "Town Car" },
                    { new Guid("6663978a-7e10-4fff-b9dc-e1fee347b80f"), "Acura", "Purple", 545521m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1995, "NSX" },
                    { new Guid("11c38755-5665-4325-b1b3-8ed2ecc60a34"), "GMC", "Indigo", 3432987m, "Colombia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2000, "Sierra 3500" },
                    { new Guid("a9a9b132-1bfd-40af-a5f2-f96dcc26653d"), "Chrysler", "Violet", 1966597m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1999, "Sebring" },
                    { new Guid("b82e1b60-9d25-493b-81f4-c94191cad34e"), "Cadillac", "Crimson", 2716499m, "Japan", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2007, "Escalade EXT" },
                    { new Guid("af281acd-11cc-465d-a5b1-0dd2265a2318"), "Ford", "Teal", 1659699m, "Sweden", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1987, "Escort" },
                    { new Guid("1aec4003-ec21-405c-b1e7-05802c22143b"), "Pontiac", "Fuscia", 2539467m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1999, "Montana" },
                    { new Guid("1d37c646-ad5f-48e7-9e65-e952fb3027ef"), "Volkswagen", "Violet", 851218m, "Czech Republic", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2003, "Jetta" },
                    { new Guid("5c8ed2a6-ee03-47ce-bbc3-81c4e5dde7c1"), "Toyota", "Teal", 1504482m, "Serbia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1996, "RAV4" },
                    { new Guid("f67df490-f02a-474e-b1b5-c6197501ba18"), "Geo", "Fuscia", 2182560m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1995, "Prizm" },
                    { new Guid("a65d587b-9d50-4f51-bb49-8336d519d9b2"), "Mercedes-Benz", "Purple", 4717833m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2011, "S-Class" },
                    { new Guid("865b459d-9961-495d-85e2-e8757910299c"), "Ford", "Crimson", 3342636m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2006, "F250" },
                    { new Guid("441d77d5-93bc-409c-a192-b405668bf0f0"), "Cadillac", "Purple", 3852239m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1997, "Seville" },
                    { new Guid("6fdd3ed7-eea7-4c00-8b84-d6756d7b9f99"), "Ford", "Khaki", 3399979m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1987, "Tempo" },
                    { new Guid("0c0d76a0-a99d-4dac-9cb3-5618d1d36cba"), "BMW", "Aquamarine", 4160243m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2001, "X5" },
                    { new Guid("b8c03af0-4e97-4470-8cfa-e1d412f0b542"), "Mercedes-Benz", "Maroon", 4176528m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2009, "S-Class" },
                    { new Guid("403b1950-c438-4ae7-bd4a-80cce1d383c3"), "Mercedes-Benz", "Aquamarine", 1282271m, "France", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2011, "SL-Class" },
                    { new Guid("4b4b858f-e9b2-46c3-898b-130beeb47cd5"), "Cadillac", "Violet", 2254622m, "Colombia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2008, "SRX" },
                    { new Guid("c6508a7d-8237-4101-9f95-ee5a5fcd3423"), "Infiniti", "Mauv", 4882326m, "Colombia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2011, "FX" },
                    { new Guid("e05a4064-abcb-413a-8c26-8f116fb850d5"), "Mazda", "Purple", 1486072m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1995, "929" },
                    { new Guid("95d96431-d2f7-4025-886a-ba3cf21ed80e"), "GMC", "Violet", 2572564m, "Sweden", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2010, "Yukon" },
                    { new Guid("6b985639-f9ef-4f0d-88a8-ef4f04d6cf27"), "Ford", "Purple", 3353264m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2003, "ZX2" },
                    { new Guid("6e2d41b9-ae05-4042-9446-1509541702f5"), "Chevrolet", "Yellow", 1725967m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2006, "SSR" },
                    { new Guid("aeac85ab-f285-4015-ab6d-130aa413b76f"), "Bentley", "Yellow", 277876m, "Czech Republic", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2007, "Arnage" },
                    { new Guid("5abc9ff5-b856-493d-bcd3-e70d28e34064"), "Ford", "Red", 1966664m, "Czech Republic", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1967, "Country" },
                    { new Guid("9b482d7a-f2bf-4f62-87c9-a58979cc1f0f"), "Honda", "Mauv", 2726816m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2001, "Civic" },
                    { new Guid("6814d6ab-093b-4b4d-8996-bd833c5531c9"), "Suzuki", "Red", 783754m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2007, "Forenza" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("e61a7529-41c5-4c9e-9640-9207d62929cb"), "Mercury", "Teal", 4290147m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2000, "Grand Marquis" },
                    { new Guid("c0fe9185-b952-495b-bd97-9b9031f6bf4c"), "Ford", "Green", 385898m, "Poland", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1985, "LTD" },
                    { new Guid("5b425f09-605c-4de7-81e9-0dc501cede65"), "Hyundai", "Orange", 1605036m, "Germany", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2002, "Sonata" },
                    { new Guid("82c703a6-1706-4da7-93e7-be84da9fef1b"), "Chevrolet", "Yellow", 2651582m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1992, "Lumina" },
                    { new Guid("8289074e-7b34-4a07-aa11-aedd33a85435"), "Suzuki", "Mauv", 875511m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1995, "Samurai" },
                    { new Guid("55ca1992-5d05-4dce-ab73-db6f3b061b6b"), "Ford", "Yellow", 3490770m, "Japan", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2004, "Focus" },
                    { new Guid("127b50be-869d-4392-90bf-68256c162a66"), "Honda", "Yellow", 829713m, "Czech Republic", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2003, "Accord" },
                    { new Guid("57ef5570-a4bb-4b88-bf1b-1d322f4c5819"), "Ford", "Teal", 3484289m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2000, "Escape" },
                    { new Guid("bd5b89a1-490c-4408-924c-79e610bae197"), "Toyota", "Pink", 883598m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1997, "Avalon" },
                    { new Guid("8fc94c54-3feb-482c-9ebd-84ac1b3e79ba"), "Mercury", "Blue", 1404441m, "Czech Republic", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1999, "Grand Marquis" },
                    { new Guid("cfdba0c4-cdbc-4591-a7e3-42fecd33b4c2"), "Nissan", "Goldenrod", 4756010m, "Japan", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2003, "Maxima" },
                    { new Guid("a1b4bb0d-44ee-4521-b4ce-b9190186800c"), "Eagle", "Red", 1509089m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1991, "Talon" },
                    { new Guid("b6c43aad-566e-4f1f-8ed3-153e62594040"), "Ford", "Goldenrod", 2464192m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1988, "Tempo" },
                    { new Guid("a567bb6f-c7f1-44a8-8b0d-3d3ae58f1d23"), "Saab", "Red", 511903m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2012, "9-3" },
                    { new Guid("48246884-4fca-4a1a-b884-011b4a429a6c"), "Eagle", "Fuscia", 439360m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1995, "Talon" },
                    { new Guid("88a4e6e3-c81d-478a-91f7-d6314f1c8262"), "Saab", "Red", 3482172m, "Poland", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1991, "900" },
                    { new Guid("eaa37a87-a00e-444c-b662-3d54652ca2c7"), "Aston Martin", "Green", 2731331m, "Sweden", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2007, "V8 Vantage" },
                    { new Guid("ce45a364-5680-4ca5-bc7a-de44fe4a5342"), "Mercury", "Aquamarine", 3664492m, "China", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1985, "Topaz" },
                    { new Guid("2e95911f-31a1-4564-904b-786d905e5ca6"), "Chevrolet", "Purple", 2753853m, "Japan", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2010, "Traverse" },
                    { new Guid("316dce5e-1d60-411d-a6c3-c5c18e3df92f"), "Dodge", "Maroon", 2987014m, "Denmark", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1998, "Ram 1500 Club" },
                    { new Guid("b73cea8e-5de0-48f2-8d09-65bd3b7e7970"), "Audi", "Goldenrod", 1901300m, "Japan", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2000, "A6" },
                    { new Guid("bcd24506-bbfd-4657-abd4-f26c24835c61"), "Saturn", "Yellow", 3511612m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1997, "S-Series" },
                    { new Guid("11453532-d5c2-401d-bcfa-dabb2b91d5db"), "Volvo", "Orange", 4852937m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2003, "C70" },
                    { new Guid("8e113fac-c2de-4c72-8cff-a479827df9f3"), "Chevrolet", "Puce", 4901564m, "Poland", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1977, "Camaro" },
                    { new Guid("baf596f8-adac-45e8-bd54-bf75afdde8e8"), "Volkswagen", "Pink", 3250962m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2012, "New Beetle" },
                    { new Guid("40015a6a-2c14-4c59-b5bb-5f58f30cd25b"), "Chevrolet", "Blue", 1366853m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1993, "3500" },
                    { new Guid("f8dc36e8-5ab5-4430-abec-2cc5673bee08"), "Lexus", "Turquoise", 535942m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1992, "LS" },
                    { new Guid("b7b61119-e130-4a00-a022-d606d118021e"), "Chevrolet", "Puce", 1298544m, "Japan", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1999, "Lumina" },
                    { new Guid("8b0aacc7-ca35-4deb-9223-6f37e3305951"), "Mazda", "Yellow", 2009829m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2001, "Millenia" },
                    { new Guid("f19b7476-0163-4b17-9577-d4f7abd04a44"), "Chevrolet", "Orange", 2433231m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1973, "Monte Carlo" },
                    { new Guid("bc11df2b-b14a-48dd-8fdc-18d490d76309"), "Mazda", "Turquoise", 1144284m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2008, "Tribute" },
                    { new Guid("fcbc3cf2-0536-477f-a0a9-a211d1ee8bf0"), "Audi", "Mauv", 4584846m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2010, "A3" },
                    { new Guid("236d82f3-4f50-4650-ae12-32bbc68317aa"), "Toyota", "Violet", 2384107m, "Russia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2007, "Tacoma" },
                    { new Guid("9abcba8e-dd2b-45b7-8b85-2fbc7ca9de2c"), "Honda", "Mauv", 2496571m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2006, "Ridgeline" },
                    { new Guid("ceb6c8c6-edc3-4a3b-af20-c4ea7f8297b8"), "Lexus", "Mauv", 1440774m, "Colombia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1995, "LS" },
                    { new Guid("5d37af66-870f-4bbb-b8b6-549b8ba85278"), "Mercedes-Benz", "Pink", 3950574m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2000, "M-Class" },
                    { new Guid("b8217f5f-3993-4413-a138-0cb69285f4fd"), "Nissan", "Goldenrod", 3016852m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1995, "Sentra" },
                    { new Guid("f2365cf9-0b0a-4ae7-ae43-cd5619690255"), "Ford", "Crimson", 4125752m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1992, "Bronco" },
                    { new Guid("217cb4f3-ab69-4b4f-8592-8c9cd6fb1a51"), "Lincoln", "Yellow", 1887201m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2010, "MKZ" },
                    { new Guid("ec482732-ed34-47b1-a9d6-e03fde7fc2cb"), "Plymouth", "Khaki", 2523708m, "Russia", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1995, "Grand Voyager" },
                    { new Guid("4e2aef06-9ce5-4203-ad37-0b8849d1e9a1"), "BMW", "Fuscia", 387742m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1992, "7 Series" },
                    { new Guid("95cc36f6-1c81-4482-98fc-a404bd167cbc"), "Volkswagen", "Goldenrod", 1758048m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2010, "Eos" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Cost", "Country", "ImageUrl", "IsDeleted", "ModelYear", "Title" },
                values: new object[,]
                {
                    { new Guid("1df72d06-94a3-4363-9fc8-105c88a21ba6"), "Oldsmobile", "Green", 4892592m, "Japan", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1992, "88" },
                    { new Guid("1ca171e9-e5e5-4e25-8290-49effa6be6bd"), "Lexus", "Fuscia", 235478m, "Colombia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1989, "LS" },
                    { new Guid("c26e1c66-eb68-4f8b-a4d1-bc469f5be0ab"), "GMC", "Violet", 1973657m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2009, "Savana 2500" },
                    { new Guid("f746727a-8b9a-439d-83af-a015d4fbec2e"), "Lotus", "Pink", 3983976m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1984, "Esprit Turbo" },
                    { new Guid("9d6ad440-6e67-4fb9-832a-7e4042584f5f"), "Jeep", "Fuscia", 2435344m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2003, "Liberty" },
                    { new Guid("ebde9abc-71b4-4145-aadd-c26acd31c842"), "Infiniti", "Red", 225802m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1994, "Q" },
                    { new Guid("32fc3dac-1fd5-431a-aad8-e9f9b0082461"), "Porsche", "Blue", 2768070m, "Denmark", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2009, "Cayman" },
                    { new Guid("9c2e5186-99d8-41db-985a-f6f0aff581f6"), "Audi", "Teal", 4607045m, "Poland", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2009, "S5" },
                    { new Guid("7d3fa0e9-8e50-4853-a5ed-aedf2eb332df"), "Honda", "Goldenrod", 3621378m, "Colombia", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1987, "Accord" },
                    { new Guid("03791e50-b055-4ecf-a80c-0f99e4a054c1"), "Chevrolet", "Blue", 4876881m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1999, "S10" },
                    { new Guid("6d27f0eb-7e95-4b3a-abc7-60f6db04aaa9"), "Mitsubishi", "Khaki", 4516630m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1987, "Pajero" },
                    { new Guid("6e4d1477-1671-4f93-b249-4b905e8ad61c"), "Mitsubishi", "Puce", 3043399m, "Poland", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1993, "Truck" },
                    { new Guid("7ceeb5a3-99e8-4de2-b2e4-b8d47fc8ff84"), "Nissan", "Mauv", 1263146m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2007, "350Z" },
                    { new Guid("1e9a0793-b802-471d-83b7-236e02adb343"), "Jeep", "Indigo", 535403m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2001, "Wrangler" },
                    { new Guid("e8670e53-6640-4bf7-b483-cb5dedac7e56"), "Lincoln", "Teal", 2438570m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1987, "Continental Mark VII" },
                    { new Guid("3d662859-9498-4fed-880e-aadc4a64f95a"), "GMC", "Red", 952781m, "Serbia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1999, "Yukon" },
                    { new Guid("d0145b65-1cd5-4b75-b650-943b2edfc2c4"), "Audi", "Turquoise", 1233595m, "Poland", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1999, "A6" },
                    { new Guid("b8e5fe3f-2e8b-4760-b528-0e5a83f6876f"), "Lexus", "Yellow", 646690m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2008, "IS-F" },
                    { new Guid("f92e27aa-c047-4f17-b4b1-3c89b9a47743"), "BMW", "Khaki", 4792742m, "Hungary", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2004, "7 Series" },
                    { new Guid("cb135cb0-2976-4d57-98e7-85769355e874"), "Mitsubishi", "Fuscia", 2338021m, "Sweden", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1995, "Chariot" },
                    { new Guid("8344fec2-6511-497b-925d-a1f9d90f393a"), "Lexus", "Goldenrod", 4053039m, "France", "http://dummyimage.com/200x200.png/dddddd/000000", false, 2011, "CT" },
                    { new Guid("f17fbeee-4a97-43fa-bcb0-736c5e4f35a0"), "Chevrolet", "Pink", 3862663m, "Sweden", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2006, "Silverado 1500" },
                    { new Guid("345bd0d8-912f-4db9-94e6-b54970d218c2"), "Mazda", "Violet", 2088882m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2011, "RX-8" },
                    { new Guid("c8ec600e-69a9-4c65-8a61-3e636cee5240"), "Volkswagen", "Aquamarine", 1781708m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1988, "Type 2" },
                    { new Guid("ffbfed29-4ef8-4cdd-9d40-354aacdbd9af"), "Daewoo", "Violet", 3451728m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2001, "Lanos" },
                    { new Guid("a88b94d4-7c59-4baf-90ce-a08cd75c0cbe"), "Honda", "Orange", 4522483m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1996, "Civic" },
                    { new Guid("9353532d-0712-46c0-b6df-c3d1f9e4a1b2"), "Volkswagen", "Pink", 3161454m, "Poland", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 1991, "Golf" },
                    { new Guid("1a15b4f6-41f8-4f55-9276-8fc51aab8bba"), "Chevrolet", "Mauv", 3705463m, "China", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1991, "Caprice" },
                    { new Guid("0cbf1fdb-f6bb-40f1-8d6f-6420997ca255"), "Jaguar", "Puce", 961841m, "France", "http://dummyimage.com/200x200.png/ff4444/ffffff", false, 2006, "X-Type" },
                    { new Guid("6fa4686e-cbc5-4705-8e45-3bba9daf3431"), "Volkswagen", "Violet", 846405m, "Russia", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1998, "New Beetle" },
                    { new Guid("a75cda6a-38e3-45db-930d-ee73ba401ba0"), "Pontiac", "Green", 1285953m, "Japan", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1990, "6000" },
                    { new Guid("e54ecb24-8d9d-4d67-8334-2532a08352ca"), "Isuzu", "Puce", 3448079m, "Japan", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 1999, "Hombre" },
                    { new Guid("8e5fe29c-1e84-468c-9e1e-5661b56af131"), "BMW", "Aquamarine", 646912m, "France", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2008, "7 Series" },
                    { new Guid("edd71791-e35f-46f8-ac19-0780d6cc5430"), "Lincoln", "Red", 3809008m, "Sweden", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2010, "Town Car" },
                    { new Guid("1093eca3-3422-4c22-aa98-3a3a7eff8d0c"), "Toyota", "Khaki", 3276264m, "China", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 2011, "Tacoma" },
                    { new Guid("51ab385e-f8ef-4e94-979c-a2f0f88f15c1"), "Lexus", "Orange", 2062741m, "China", "http://dummyimage.com/200x200.png/cc0000/ffffff", false, 2005, "GS" },
                    { new Guid("c0e319f7-08c8-48cb-91b5-5f06ec6f9c76"), "Chevrolet", "Blue", 3575463m, "Sweden", "http://dummyimage.com/200x200.png/5fa2dd/ffffff", false, 1993, "Sportvan G30" },
                    { new Guid("e374a4b5-8bdd-48f9-8724-6b85a60d96d2"), "Saab", "Red", 3434267m, "Russia", "http://dummyimage.com/200x200.png/dddddd/000000", false, 1987, "900" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_OrderId",
                table: "CartItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserCartId",
                table: "CartItems",
                column: "UserCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryContactId",
                table: "Orders",
                column: "DeliveryContactId");

            migrationBuilder.CreateIndex(
                name: "IX_UserComparableProducts_ProductId",
                table: "UserComparableProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteProducts_ProductId",
                table: "UserFavoriteProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "UserComparableProducts");

            migrationBuilder.DropTable(
                name: "UserFavoriteProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "UserCarts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "DeliveryContacts");
        }
    }
}
