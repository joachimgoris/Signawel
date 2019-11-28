using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class CitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "10ae4cbb-da20-46dd-a5c7-e84ac154ec5c", "Alken" },
                    { "1c08220f-8b1b-4a29-b6a1-e0e206316368", "Kortessem" },
                    { "388a8d5d-31c6-4c41-aece-1c84d8964fe8", "Lanaken" },
                    { "5d409b28-276f-4c79-93bf-eb3c05c2b2e9", "Leopoldsburg" },
                    { "89a075c1-2871-4f82-87ce-05d8ab71bee4", "Lommel" },
                    { "b8fb2a7a-4c08-480f-b44f-06af4391dae0", "Lummen" },
                    { "75f62a96-5d9a-4302-8878-cdf166bd420d", "Maaseik" },
                    { "0816a85c-cf1b-4098-a627-6c2c2be21a44", "Maasmechelen" },
                    { "947dac05-622e-465c-a0c9-fffc51c116c7", "Nieuwerkerken" },
                    { "b6c2d29c-b490-4049-b83b-aa180d1b64ed", "Oudsbergen" },
                    { "eff66d7f-2204-43f0-8f9e-5251e0231ddc", "Peer" },
                    { "874c82f6-dbf7-44b1-a1a1-fd0ed5038b6d", "Pelt" },
                    { "abda5417-31c7-4310-b5d1-1e0856468085", "Riemst" },
                    { "c54f182d-bbca-4c98-bdcc-652f2356f801", "Sint-Truiden" },
                    { "fb2d071e-c077-47e6-9052-e0b30789f8f9", "Tessenderlo" },
                    { "775f3629-d176-4128-9d02-43ea8aa0f3ae", "Tongeren" },
                    { "f40240d0-f8b0-4d6b-8eb5-3af5cbdf2d3b", "Voeren" },
                    { "f6cd4777-90e5-425f-990c-3b16f629b367", "Wellen" },
                    { "44857e82-ea0b-4125-8aad-f523e4550098", "Kinrooi" },
                    { "c1d7ab62-c2b5-43bd-a1e6-293178968db3", "Houthalen-Helchteren" },
                    { "a88262ca-9849-4488-b3df-083160419ce4", "Hoeselt" },
                    { "e3d21daf-77dc-4c10-8b76-45b947aeb00b", "Heusden-Zolder" },
                    { "ba15685c-21ef-43ba-b137-2bf6dcb6014b", "As" },
                    { "b6eaea68-96cc-42e0-af25-50a4d91f1827", "Beringen" },
                    { "f4a3c949-aa86-42ec-b411-f38793f9eb7a", "Bilzen" },
                    { "fc01fd7a-5d4a-4365-b8f3-2d72f05054f0", "Bocholt" },
                    { "ea26e298-8638-420a-a66e-342b56c19479", "Borgloon" },
                    { "982ad41a-c655-4406-9c00-b1c8b10d9591", "Bree" },
                    { "43b03c97-6cd1-4348-a7c9-0a7c9257f8a1", "Diepenbeek" },
                    { "178bae7e-a3f2-4a74-800d-ba76729455e6", "Dilsen-Stokkem" },
                    { "d2bd75ea-048e-4d3a-b804-1de5d4c381b5", "Zonhoven" },
                    { "0d702a37-aff8-4762-9d52-47e4032bd1b7", "Genk" },
                    { "5f41fbc7-a5c0-4cd5-a543-d2d2b3540916", "Halen" },
                    { "646a4dc7-32d6-43bd-abc5-39b91e33ceb2", "Ham" },
                    { "d7be3170-1168-4bb1-808e-ea4327f0d18d", "Hamont-Achel" },
                    { "2ef35437-1f5c-4a4b-bf89-0cf2cc462c1e", "Hasselt" },
                    { "4cb665e3-20c0-44f4-a4ba-27209df8780e", "Hechelt-Eksel" },
                    { "3397df4f-d8d7-4204-aa14-4933ea6a566d", "Heers" },
                    { "1d9a7c95-d352-44c6-a44c-2796b7f145a3", "Herk-de-Stad" },
                    { "7120c3e6-65b0-4e09-b1a2-4c68f0c37c61", "Herstappe" },
                    { "0970f669-30a5-4ce0-9ce0-634c7211d556", "Gingelom" },
                    { "a26462fd-7a7e-4dd1-abfb-5c9a3d6bcf20", "Zutendaal" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "0816a85c-cf1b-4098-a627-6c2c2be21a44");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "0970f669-30a5-4ce0-9ce0-634c7211d556");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "0d702a37-aff8-4762-9d52-47e4032bd1b7");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "10ae4cbb-da20-46dd-a5c7-e84ac154ec5c");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "178bae7e-a3f2-4a74-800d-ba76729455e6");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "1c08220f-8b1b-4a29-b6a1-e0e206316368");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "1d9a7c95-d352-44c6-a44c-2796b7f145a3");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "2ef35437-1f5c-4a4b-bf89-0cf2cc462c1e");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "3397df4f-d8d7-4204-aa14-4933ea6a566d");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "388a8d5d-31c6-4c41-aece-1c84d8964fe8");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "43b03c97-6cd1-4348-a7c9-0a7c9257f8a1");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "44857e82-ea0b-4125-8aad-f523e4550098");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "4cb665e3-20c0-44f4-a4ba-27209df8780e");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "5d409b28-276f-4c79-93bf-eb3c05c2b2e9");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "5f41fbc7-a5c0-4cd5-a543-d2d2b3540916");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "646a4dc7-32d6-43bd-abc5-39b91e33ceb2");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "7120c3e6-65b0-4e09-b1a2-4c68f0c37c61");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "75f62a96-5d9a-4302-8878-cdf166bd420d");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "775f3629-d176-4128-9d02-43ea8aa0f3ae");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "874c82f6-dbf7-44b1-a1a1-fd0ed5038b6d");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "89a075c1-2871-4f82-87ce-05d8ab71bee4");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "947dac05-622e-465c-a0c9-fffc51c116c7");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "982ad41a-c655-4406-9c00-b1c8b10d9591");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "a26462fd-7a7e-4dd1-abfb-5c9a3d6bcf20");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "a88262ca-9849-4488-b3df-083160419ce4");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "abda5417-31c7-4310-b5d1-1e0856468085");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "b6c2d29c-b490-4049-b83b-aa180d1b64ed");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "b6eaea68-96cc-42e0-af25-50a4d91f1827");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "b8fb2a7a-4c08-480f-b44f-06af4391dae0");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "ba15685c-21ef-43ba-b137-2bf6dcb6014b");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "c1d7ab62-c2b5-43bd-a1e6-293178968db3");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "c54f182d-bbca-4c98-bdcc-652f2356f801");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "d2bd75ea-048e-4d3a-b804-1de5d4c381b5");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "d7be3170-1168-4bb1-808e-ea4327f0d18d");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "e3d21daf-77dc-4c10-8b76-45b947aeb00b");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "ea26e298-8638-420a-a66e-342b56c19479");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "eff66d7f-2204-43f0-8f9e-5251e0231ddc");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "f40240d0-f8b0-4d6b-8eb5-3af5cbdf2d3b");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "f4a3c949-aa86-42ec-b411-f38793f9eb7a");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "f6cd4777-90e5-425f-990c-3b16f629b367");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "fb2d071e-c077-47e6-9052-e0b30789f8f9");

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "id",
                keyValue: "fc01fd7a-5d4a-4365-b8f3-2d72f05054f0");
        }
    }
}
