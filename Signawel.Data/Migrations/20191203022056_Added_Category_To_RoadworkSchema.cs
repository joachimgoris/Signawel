using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class Added_Category_To_RoadworkSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "0adb2933-3ff8-48d8-9058-6f6398e1cdde");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "0bc39bbf-d02f-4837-8895-1bbb255539d3");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "0fe26c29-7fee-488e-8fbf-39c36249fcdb");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "17a1922d-5b05-432c-9f97-9c0a279ecaf8");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "1874c3c5-0e4f-4565-a4d3-e179b77e993e");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "1c98ca9a-02ac-4f6a-b41f-0eee6cb6627e");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "1f889685-5360-4cf8-8745-0be139625427");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "217d9c61-4a81-4116-a24f-4390ab456d69");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "29c54b7d-762c-4102-bfd9-fb652b322a69");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "30217cff-55e0-480b-819e-f6ea724c2f16");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "4ba0fa02-d22e-4191-8a0f-7abb80066b3c");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "5aab5ebe-d390-4bf5-bb2d-ef696c34a741");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "5de99735-8b0c-4c70-876a-df6837196787");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "5e1df4df-66e1-44b4-a9d6-57c16abf0fc8");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "6fd3c011-73b9-46ef-aacf-27f7c174477f");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "705af8bc-c5b0-4ca8-8e68-078239ca8676");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "70d6ea53-9218-4ea5-acab-7b97443d7f69");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "729764fb-a0fa-4a86-845c-2c57e1fc0fae");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "74f22229-da71-4c41-9424-8ca12f239f6b");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "7943928f-b2dd-4617-9bb2-05d9d33939bc");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "80451bee-07af-4820-b567-173e47e6a6b3");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "829cef46-3a6e-4fe9-b531-f82cc488f908");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "97d1185e-7db0-4cb5-bb4b-694396479283");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "99de3e3b-f8fa-4bbd-b1bb-c365d6bd83dc");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "9c2efdf8-d514-4c75-b9ab-472bc9f9827f");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "9c814573-1fcb-4be9-b1b9-5810656d3f0c");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "9dc1f7e2-bc2a-4c7a-a28e-b542aa06e0c2");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "a956e3fb-eeac-42e4-befd-220075c52ee0");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "af72dfea-3872-4902-93f8-c5fe25506859");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "b950becf-150b-4f74-bbf5-b0a6b7fbed44");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "b96d0451-e8c7-4bf8-ad34-4be2d27120c4");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "c12aac6d-5513-4b68-b331-0a403f8950b4");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "c28f5a5e-4249-40b3-ab84-62810ea39445");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "c73dab2d-8bdf-417c-9bc8-936ee7106146");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "c7fb8ef5-f3e3-4fe7-a164-d2a9b8a3fb06");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "d7b2f2cb-13b7-462b-8036-682dff1f1583");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "dbdfc118-65cf-4175-b1cb-7c83917246cb");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "dc0432f1-72c2-4cb2-9c52-b28be52c6a71");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "f284f8ad-e758-4171-a30d-6e49bfbafc36");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "f98eb09c-02c1-4da9-82ac-431f9481651a");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "fbd9b409-9e54-4025-aa72-b9f92c767607");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "ffb77b12-f2d7-4c4a-bc0b-8bd3e6c7a2be");

            migrationBuilder.AddColumn<byte>(
                name: "category",
                table: "roadwork_schemas",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "2ed2b7f0-920f-42c8-9641-c19bc4351d52", "Alken" },
                    { "dd851bbb-7142-4571-b38e-3b84a6827ef1", "Kortessem" },
                    { "8806db27-8592-4efc-bd62-39591be1d843", "Lanaken" },
                    { "a0f34c73-5e7a-46d3-9f50-a45db8b12af4", "Leopoldsburg" },
                    { "4decce7a-7c71-47fc-9533-3d8a77894ed0", "Lommel" },
                    { "19852520-94a3-4046-b13f-0b2ba57943a2", "Lummen" },
                    { "1e9877a0-118f-4a7e-83fc-65af43f5a53e", "Maaseik" },
                    { "7475a7bb-9f21-43a0-b5b8-e629b05bca3b", "Maasmechelen" },
                    { "8ec2708b-416a-4fa5-bdaf-15da8ba77d11", "Nieuwerkerken" },
                    { "204a046a-df35-416a-92a3-9eb68f441e10", "Oudsbergen" },
                    { "31c0d37e-9c27-4266-8160-1d4ee2e2d51b", "Peer" },
                    { "5259afcc-4637-4e8c-9aff-27866a1d724b", "Pelt" },
                    { "7d4f7a82-188f-4f7b-b1cd-196a42fafe7a", "Riemst" },
                    { "92247aa1-afb9-4d0b-9074-408bb643cbab", "Sint-Truiden" },
                    { "339a4da6-04b1-4026-8673-5255ed6c405c", "Tessenderlo" },
                    { "d05d1093-261c-4bc8-8b3c-e8ddd598b4aa", "Tongeren" },
                    { "5de00e5a-2d92-4fc9-b2ff-64e48164dfdf", "Voeren" },
                    { "ae7747e8-5f19-4c36-a707-c356fb419689", "Wellen" },
                    { "c9b2583a-662e-4e8e-9184-44e8607a98c3", "Kinrooi" },
                    { "edf5920d-2948-40da-8474-1b6688264894", "Houthalen-Helchteren" },
                    { "cd2eaa2b-ec9f-4e34-bc3b-2134315ea60a", "Hoeselt" },
                    { "cb4f18b3-a53a-4fc1-a0b8-bb1da3db783b", "Heusden-Zolder" },
                    { "9b2f41cb-8645-4fba-bf84-8f8b964fd301", "As" },
                    { "e9a18b63-e2c2-49eb-9dc7-abcf5c3aad38", "Beringen" },
                    { "5bcc30a4-88fc-42a8-ba9a-44dab91c76d8", "Bilzen" },
                    { "02a53b9a-1e33-4c02-992b-33a7404d50a5", "Bocholt" },
                    { "bfb0a6c3-550e-4878-a438-83d20149bedb", "Borgloon" },
                    { "df0c28c3-3bf0-4704-9181-93753fb5237c", "Bree" },
                    { "4f00bbf5-536f-43ae-8658-3440b6b4626b", "Diepenbeek" },
                    { "81c1dca2-7a9b-4a0f-ad13-6ba3c360ad47", "Dilsen-Stokkem" },
                    { "fdc0b6b8-3706-4eb8-aac2-abaf28cae7e5", "Zonhoven" },
                    { "fd55a3db-295a-4f97-8d54-ce7766d71694", "Genk" },
                    { "355bb2ef-c8c7-4d7e-aaf1-13f51762e9c7", "Halen" },
                    { "edbe3747-aca4-438d-923f-7084ac379557", "Ham" },
                    { "79ba7376-e9b2-41e3-a012-7c1c5d10d23c", "Hamont-Achel" },
                    { "b454e8e0-e965-4ad0-a5b2-22359c57c295", "Hasselt" },
                    { "2a3ec33a-2d6e-4394-90a3-2445905806ff", "Hechelt-Eksel" },
                    { "cd3cdd35-d5b9-4880-8898-bdfdf6c484fe", "Heers" },
                    { "e17f7686-b533-4315-81e7-94c2ca28e3a1", "Herk-de-Stad" },
                    { "5c89fe67-ecd2-4411-a426-6076e18adbd7", "Herstappe" },
                    { "81b3f9c2-9608-443d-8508-5543ab6c651e", "Gingelom" },
                    { "56dae4f6-c18b-4743-a4f8-53313acb6e44", "Zutendaal" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "02a53b9a-1e33-4c02-992b-33a7404d50a5");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "19852520-94a3-4046-b13f-0b2ba57943a2");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "1e9877a0-118f-4a7e-83fc-65af43f5a53e");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "204a046a-df35-416a-92a3-9eb68f441e10");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "2a3ec33a-2d6e-4394-90a3-2445905806ff");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "2ed2b7f0-920f-42c8-9641-c19bc4351d52");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "31c0d37e-9c27-4266-8160-1d4ee2e2d51b");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "339a4da6-04b1-4026-8673-5255ed6c405c");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "355bb2ef-c8c7-4d7e-aaf1-13f51762e9c7");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "4decce7a-7c71-47fc-9533-3d8a77894ed0");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "4f00bbf5-536f-43ae-8658-3440b6b4626b");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "5259afcc-4637-4e8c-9aff-27866a1d724b");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "56dae4f6-c18b-4743-a4f8-53313acb6e44");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "5bcc30a4-88fc-42a8-ba9a-44dab91c76d8");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "5c89fe67-ecd2-4411-a426-6076e18adbd7");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "5de00e5a-2d92-4fc9-b2ff-64e48164dfdf");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "7475a7bb-9f21-43a0-b5b8-e629b05bca3b");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "79ba7376-e9b2-41e3-a012-7c1c5d10d23c");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "7d4f7a82-188f-4f7b-b1cd-196a42fafe7a");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "81b3f9c2-9608-443d-8508-5543ab6c651e");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "81c1dca2-7a9b-4a0f-ad13-6ba3c360ad47");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "8806db27-8592-4efc-bd62-39591be1d843");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "8ec2708b-416a-4fa5-bdaf-15da8ba77d11");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "92247aa1-afb9-4d0b-9074-408bb643cbab");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "9b2f41cb-8645-4fba-bf84-8f8b964fd301");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "a0f34c73-5e7a-46d3-9f50-a45db8b12af4");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "ae7747e8-5f19-4c36-a707-c356fb419689");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "b454e8e0-e965-4ad0-a5b2-22359c57c295");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "bfb0a6c3-550e-4878-a438-83d20149bedb");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "c9b2583a-662e-4e8e-9184-44e8607a98c3");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "cb4f18b3-a53a-4fc1-a0b8-bb1da3db783b");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "cd2eaa2b-ec9f-4e34-bc3b-2134315ea60a");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "cd3cdd35-d5b9-4880-8898-bdfdf6c484fe");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "d05d1093-261c-4bc8-8b3c-e8ddd598b4aa");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "dd851bbb-7142-4571-b38e-3b84a6827ef1");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "df0c28c3-3bf0-4704-9181-93753fb5237c");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "e17f7686-b533-4315-81e7-94c2ca28e3a1");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "e9a18b63-e2c2-49eb-9dc7-abcf5c3aad38");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "edbe3747-aca4-438d-923f-7084ac379557");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "edf5920d-2948-40da-8474-1b6688264894");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "fd55a3db-295a-4f97-8d54-ce7766d71694");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "fdc0b6b8-3706-4eb8-aac2-abaf28cae7e5");

            migrationBuilder.DropColumn(
                name: "category",
                table: "roadwork_schemas");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    image_path = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    order_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "829cef46-3a6e-4fe9-b531-f82cc488f908", "Alken" },
                    { "70d6ea53-9218-4ea5-acab-7b97443d7f69", "Kortessem" },
                    { "9dc1f7e2-bc2a-4c7a-a28e-b542aa06e0c2", "Lanaken" },
                    { "5aab5ebe-d390-4bf5-bb2d-ef696c34a741", "Leopoldsburg" },
                    { "1874c3c5-0e4f-4565-a4d3-e179b77e993e", "Lommel" },
                    { "1c98ca9a-02ac-4f6a-b41f-0eee6cb6627e", "Lummen" },
                    { "b96d0451-e8c7-4bf8-ad34-4be2d27120c4", "Maaseik" },
                    { "729764fb-a0fa-4a86-845c-2c57e1fc0fae", "Maasmechelen" },
                    { "d7b2f2cb-13b7-462b-8036-682dff1f1583", "Nieuwerkerken" },
                    { "30217cff-55e0-480b-819e-f6ea724c2f16", "Oudsbergen" },
                    { "f284f8ad-e758-4171-a30d-6e49bfbafc36", "Peer" },
                    { "99de3e3b-f8fa-4bbd-b1bb-c365d6bd83dc", "Pelt" },
                    { "af72dfea-3872-4902-93f8-c5fe25506859", "Riemst" },
                    { "5e1df4df-66e1-44b4-a9d6-57c16abf0fc8", "Sint-Truiden" },
                    { "4ba0fa02-d22e-4191-8a0f-7abb80066b3c", "Tessenderlo" },
                    { "80451bee-07af-4820-b567-173e47e6a6b3", "Tongeren" },
                    { "74f22229-da71-4c41-9424-8ca12f239f6b", "Voeren" },
                    { "0fe26c29-7fee-488e-8fbf-39c36249fcdb", "Wellen" },
                    { "f98eb09c-02c1-4da9-82ac-431f9481651a", "Kinrooi" },
                    { "29c54b7d-762c-4102-bfd9-fb652b322a69", "Houthalen-Helchteren" },
                    { "a956e3fb-eeac-42e4-befd-220075c52ee0", "Hoeselt" },
                    { "dc0432f1-72c2-4cb2-9c52-b28be52c6a71", "Heusden-Zolder" },
                    { "0adb2933-3ff8-48d8-9058-6f6398e1cdde", "As" },
                    { "dbdfc118-65cf-4175-b1cb-7c83917246cb", "Beringen" },
                    { "fbd9b409-9e54-4025-aa72-b9f92c767607", "Bilzen" },
                    { "ffb77b12-f2d7-4c4a-bc0b-8bd3e6c7a2be", "Bocholt" },
                    { "705af8bc-c5b0-4ca8-8e68-078239ca8676", "Borgloon" },
                    { "0bc39bbf-d02f-4837-8895-1bbb255539d3", "Bree" },
                    { "b950becf-150b-4f74-bbf5-b0a6b7fbed44", "Diepenbeek" },
                    { "6fd3c011-73b9-46ef-aacf-27f7c174477f", "Dilsen-Stokkem" },
                    { "97d1185e-7db0-4cb5-bb4b-694396479283", "Zonhoven" },
                    { "17a1922d-5b05-432c-9f97-9c0a279ecaf8", "Genk" },
                    { "c73dab2d-8bdf-417c-9bc8-936ee7106146", "Halen" },
                    { "c7fb8ef5-f3e3-4fe7-a164-d2a9b8a3fb06", "Ham" },
                    { "9c2efdf8-d514-4c75-b9ab-472bc9f9827f", "Hamont-Achel" },
                    { "5de99735-8b0c-4c70-876a-df6837196787", "Hasselt" },
                    { "c28f5a5e-4249-40b3-ab84-62810ea39445", "Hechelt-Eksel" },
                    { "1f889685-5360-4cf8-8745-0be139625427", "Heers" },
                    { "7943928f-b2dd-4617-9bb2-05d9d33939bc", "Herk-de-Stad" },
                    { "217d9c61-4a81-4116-a24f-4390ab456d69", "Herstappe" },
                    { "c12aac6d-5513-4b68-b331-0a403f8950b4", "Gingelom" },
                    { "9c814573-1fcb-4be9-b1b9-5810656d3f0c", "Zutendaal" }
                });
        }
    }
}
