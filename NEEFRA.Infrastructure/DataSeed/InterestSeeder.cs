using MongoDB.Bson;
using MongoDB.Driver;
using NEEFRA.Core.Entities.Inerests;


namespace NEEFRA.Core.DataSeed
{
    public class InterestSeeder
    {
        public static void Seed(IMongoDatabase database)
        {
            var collection = database.GetCollection<Interest>("Interests");

            var count = collection.CountDocuments(_ => true);

            if (count == 0)
            {
                var interests = new List<Interest>
        {
            // --- Dynasties / Periods ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "NewKingdom" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "MiddleKingdom" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "OldKingdom" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "PreDynastic" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "FirstIntermediatePeriod" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "ThirdIntermediatePeriod" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "LatePeriod" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "PtolemaicPeriod" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "RomanPeriod" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "CopticChristian" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "IslamicHistory" },

            // --- Pharaohs & Royalty ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Akhenaten" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "AmenhotepIII" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Hatshepsut" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "ThutmoseIII" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "RamessesII" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "RamessesIII" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Tutankhamun" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Horemheb" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Ahmose" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "SenusretIII" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "MentuhotepDynasty" },

            // --- Religion & Gods ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "GodAmun" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "GodOsiris" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "GodSobek" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "GodessSekhmet" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "GodHorus" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "GodThoth" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "GodAten" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "GodHathor" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "GodMut" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "GodIsis" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "GodsAndDivinity" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "FuneraryReligion" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "AftLife" },

            // --- Art & Sculpture ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "RoyalStatue" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "BlockStatue" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "ReliefArt" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "WallPainting" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Realism" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "AmarnaArt" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "FuneraryArt" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "PortraitSculpture" },

            // --- Military & War ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "MilitaryHistory" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Weapons" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Archery" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "HyksosWar" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "SeaPeoples" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "LibyanWars" },

            // --- Architecture & Engineering ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "TempleArchitecture" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "KarnakTemple" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "LuxorTemple" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Engineering" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Obelisks" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Columns" },

            // --- Daily Life & Society ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "AncientWriting" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "ScribesAndLiteracy" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "TradeAndEconomy" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "CraftAndManufacturing" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "WomenInEgypt" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "ChildrenAndFamily" },

            // --- Materials & Craft ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Metalworking" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Faience" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Stonework" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Woodworking" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Jewelry" },

            // --- Exploration & Discovery ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "ArchaeologicalDiscovery" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "CachettesAndHoards" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Mummies" },

            // --- Daily Life ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "DailyLife" },

            // --- Boats & Navigation ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "BoatBuilding" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "RiverNile" },

            // --- Administration ---
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Viziers" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "RoyalOfficials" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Administration" },
        };

                collection.InsertMany(interests);
            }
        }
    }
}
