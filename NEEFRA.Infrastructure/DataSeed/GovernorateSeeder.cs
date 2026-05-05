using MongoDB.Driver;
using NEEFRA.Core.Entities;

namespace NEEFRA_API.Data
{
    public static class GovernorateSeeder
    {
        public static void Seed(IMongoDatabase database)
        {
            // 1. Governorates collection
            var governoratesCollection = database.GetCollection<Governorate>("Governorates");
            if (governoratesCollection.CountDocuments(FilterDefinition<Governorate>.Empty) == 0)
            {
                var governorates = GetGovernorates();
                governoratesCollection.InsertMany(governorates);
                Console.WriteLine("Governorates seeded.");
            }

            // 2. Museums collection
            var museumsCollection = database.GetCollection<Museum>("Museums");
            if (museumsCollection.CountDocuments(FilterDefinition<Museum>.Empty) == 0)
            {
                var govDict = governoratesCollection
                    .Find(FilterDefinition<Governorate>.Empty)
                    .ToList()
                    .ToDictionary(g => g.Name, g => g.Id!);

                var museums = GetMuseums(govDict);
                museumsCollection.InsertMany(museums);
                Console.WriteLine("Museums seeded.");
            }
        }

        private static List<Governorate> GetGovernorates()
        {
            return new List<Governorate>
            {
                new Governorate { Name = "Cairo", Capital = "Cairo", Region = "Lower Egypt", Latitude = 30.0444, Longitude = 31.2357 },
                new Governorate { Name = "Alexandria", Capital = "Alexandria", Region = "Lower Egypt", Latitude = 31.2001, Longitude = 29.9187 },
                new Governorate { Name = "Giza", Capital = "Giza", Region = "Lower Egypt", Latitude = 29.987, Longitude = 31.2118 },
                new Governorate { Name = "Luxor", Capital = "Luxor", Region = "Upper Egypt", Latitude = 25.6872, Longitude = 32.6396 },
                new Governorate { Name = "Aswan", Capital = "Aswan", Region = "Upper Egypt", Latitude = 24.0889, Longitude = 32.8998 },
                new Governorate { Name = "Port Said", Capital = "Port Said", Region = "Lower Egypt", Latitude = 31.2653, Longitude = 32.3019 },
                new Governorate { Name = "Suez", Capital = "Suez", Region = "Lower Egypt", Latitude = 29.9668, Longitude = 32.5498 },
                new Governorate { Name = "Ismailia", Capital = "Ismailia", Region = "Lower Egypt", Latitude = 30.583, Longitude = 32.2654 },
                new Governorate { Name = "Damietta", Capital = "Damietta", Region = "Lower Egypt", Latitude = 31.4165, Longitude = 31.8133 },
                new Governorate { Name = "Dakahlia", Capital = "Mansoura", Region = "Lower Egypt", Latitude = 31.0409, Longitude = 31.3785 },
                new Governorate { Name = "Sharqia", Capital = "Zagazig", Region = "Lower Egypt", Latitude = 30.5877, Longitude = 31.502 },
                new Governorate { Name = "Qalyubia", Capital = "Benha", Region = "Lower Egypt", Latitude = 30.4667, Longitude = 31.1833 },
                new Governorate { Name = "Kafr El Sheikh", Capital = "Kafr El Sheikh", Region = "Lower Egypt", Latitude = 31.1115, Longitude = 30.9396 },
                new Governorate { Name = "Gharbia", Capital = "Tanta", Region = "Lower Egypt", Latitude = 30.7833, Longitude = 31 },
                new Governorate { Name = "Monufia", Capital = "Shibin El Kom", Region = "Lower Egypt", Latitude = 30.5606, Longitude = 31.0089 },
                new Governorate { Name = "Beheira", Capital = "Damanhur", Region = "Lower Egypt", Latitude = 31.0419, Longitude = 30.4684 },
                new Governorate { Name = "Faiyum", Capital = "Faiyum", Region = "Upper Egypt", Latitude = 29.3084, Longitude = 30.8428 },
                new Governorate { Name = "Beni Suef", Capital = "Beni Suef", Region = "Upper Egypt", Latitude = 29.0767, Longitude = 31.0969 },
                new Governorate { Name = "Minya", Capital = "Minya", Region = "Upper Egypt", Latitude = 28.1099, Longitude = 30.7503 },
                new Governorate { Name = "Asyut", Capital = "Asyut", Region = "Upper Egypt", Latitude = 27.1783, Longitude = 31.1859 },
                new Governorate { Name = "Sohag", Capital = "Sohag", Region = "Upper Egypt", Latitude = 26.5586, Longitude = 31.6948 },
                new Governorate { Name = "Qena", Capital = "Qena", Region = "Upper Egypt", Latitude = 26.1642, Longitude = 32.7167 },
                new Governorate { Name = "Red Sea", Capital = "Hurghada", Region = "Eastern Desert", Latitude = 27.2579, Longitude = 33.8116 },
                new Governorate { Name = "New Valley", Capital = "Kharga", Region = "Western Desert", Latitude = 25.451, Longitude = 30.547 },
                new Governorate { Name = "Matrouh", Capital = "Mersa Matruh", Region = "Western Desert", Latitude = 31.3543, Longitude = 27.2372 },
                new Governorate { Name = "North Sinai", Capital = "Arish", Region = "Sinai", Latitude = 31.1249, Longitude = 33.7974 },
                new Governorate { Name = "South Sinai", Capital = "El Tor", Region = "Sinai", Latitude = 29.5, Longitude = 33.8333 }
            };
        }

        private static List<Museum> GetMuseums(Dictionary<string, string> govDict)
        {
            var museums = new List<Museum>();

            void AddMuseum(string govName, string name, string location, double lat, double lng,
                string info, string hours, decimal egyptAdult, decimal egyptStudent,
                decimal foreignAdult, decimal foreignStudent)
            {
                if (govDict.TryGetValue(govName, out var govId))
                {
                    var m = new Museum
                    {
                        Name = name,
                        Location = location,
                        Latitude = lat,
                        Longitude = lng,
                        GeneralInfo = info,
                        Open_Hours = hours,
                        TicketEgyptAdultPrice = egyptAdult,
                        TicketEgyptStudentPrice = egyptStudent,
                        TicketForienerAdultPrice = foreignAdult,
                        TicketForienerStudentPrice = foreignStudent,
                        GovernorateId = govId
                    };

                    if(m.Name== "Luxor Museum")
                    {
                        m.Id = "000000000000000000000001";
                    }
                    museums.Add(m);
                }
            }

            // Cairo
            AddMuseum("Cairo", "The Egyptian Museum (Tahrir)", "Tahrir Square, Cairo", 30.0478, 31.2357,
                "Home to over 120,000 artifacts including Tutankhamun's treasures.",
                "9:00 AM - 5:00 PM (closed Fridays)", 30, 15, 200, 100);
            AddMuseum("Cairo", "Museum of Islamic Art", "Bab Al Khalq, Cairo", 30.0442, 31.2445,
                "World's largest collection of Islamic art.",
                "9:00 AM - 4:00 PM", 20, 10, 120, 60);
            AddMuseum("Cairo", "Coptic Museum", "Old Cairo", 30.0058, 31.2301,
                "Artifacts from Egypt's Christian heritage.",
                "9:00 AM - 4:00 PM", 15, 8, 100, 50);
            AddMuseum("Cairo", "National Museum of Egyptian Civilization (NMEC)", "Fustat, Cairo", 30.0075, 31.2489,
                "Royal Mummies Hall and comprehensive Egyptian history.",
                "9:00 AM - 5:00 PM", 50, 25, 300, 150);

            // Alexandria
            AddMuseum("Alexandria", "Alexandria National Museum", "Tariq El Horreya St, Alexandria", 31.1998, 29.9077,
                "Housed in a restored palace, covers Alexandria's Greco-Roman history.",
                "9:00 AM - 4:30 PM", 25, 12, 150, 75);
            AddMuseum("Alexandria", "Royal Jewelry Museum", "Zizenia, Alexandria", 31.1975, 29.919,
                "Artifacts from the Muhammad Ali dynasty.",
                "9:00 AM - 4:00 PM", 20, 10, 100, 50);

            // Giza
            AddMuseum("Giza", "Grand Egyptian Museum (GEM)", "Al Haram, Giza", 29.9933, 31.1198,
                "The newest and largest archaeological museum (partial opening).",
                "10:00 AM - 6:00 PM", 60, 30, 450, 200);
            AddMuseum("Giza", "Imhotep Museum", "Saqqara, Giza", 29.8712, 31.2161,
                "Dedicated to the architect of the Step Pyramid.",
                "8:00 AM - 4:00 PM", 15, 8, 80, 40);

            // Luxor
            AddMuseum("Luxor", "Luxor Museum", "Corniche El Nil, Luxor", 25.696, 32.6446,
                "Masterpieces from Theban temples and tombs.",
                "9:00 AM - 4:00 PM & 5:00 PM - 9:00 PM", 40, 20, 200, 100);
            AddMuseum("Luxor", "Mummification Museum", "Luxor Corniche", 25.6955, 32.6397,
                "Dedicated to the art of mummification.",
                "9:00 AM - 4:00 PM", 20, 10, 120, 60);

            // Aswan
            AddMuseum("Aswan", "Nubia Museum", "Aswan, near Elephantine Island", 24.0822, 32.887,
                "Nubian culture, history, and artifacts rescued from flooding.",
                "9:00 AM - 5:00 PM", 30, 15, 180, 90);
            AddMuseum("Aswan", "Aswan Museum", "Elephantine Island", 24.085, 32.886,
                "Artifacts from Aswan and Nubia.",
                "9:00 AM - 4:00 PM", 10, 5, 50, 25);

            // Port Said
            AddMuseum("Port Said", "Port Said National Museum", "Palestine St, Port Said", 31.259, 32.284,
                "History of the Suez Canal and Port Said.",
                "10:00 AM - 4:00 PM", 10, 5, 60, 30);

            // Suez
            AddMuseum("Suez", "Suez National Museum", "Al Geish St, Suez", 29.967, 32.552,
                "Suez Canal and regional history.",
                "10:00 AM - 4:00 PM", 10, 5, 60, 30);

            // Ismailia
            AddMuseum("Ismailia", "Ismailia Museum", "Muhammad Ali St, Ismailia", 30.584, 32.265,
                "Archaeological finds from the Suez Canal region.",
                "9:00 AM - 3:00 PM", 8, 4, 40, 20);

            // Damietta
            AddMuseum("Damietta", "Damietta Museum", "Al Bahr St, Damietta", 31.417, 31.815,
                "Local history and traditional crafts.",
                "10:00 AM - 2:00 PM", 5, 3, 30, 15);

            // Dakahlia
            AddMuseum("Dakahlia", "Mansoura Museum", "El Gomhouria St, Mansoura", 31.041, 31.379,
                "Artifacts from the Delta region.",
                "9:00 AM - 3:00 PM", 5, 3, 30, 15);

            // Sharqia
            AddMuseum("Sharqia", "Zagazig Museum", "El Horreya St, Zagazig", 30.588, 31.502,
                "Pharaonic and Graeco-Roman finds from Tell Basta.",
                "10:00 AM - 2:00 PM", 5, 3, 30, 15);

            // Qalyubia
            AddMuseum("Qalyubia", "Benha Museum", "Benha City Center", 30.466, 31.183,
                "Local archaeological collection.",
                "9:00 AM - 2:00 PM", 5, 3, 25, 12);

            // Kafr El Sheikh
            AddMuseum("Kafr El Sheikh", "Kafr El Sheikh Museum", "El Horreya St", 31.112, 30.94,
                "Ancient Buto (Tell El Fara'in) artifacts.",
                "10:00 AM - 2:00 PM", 5, 3, 30, 15);

            // Gharbia
            AddMuseum("Gharbia", "Tanta Museum", "Al Bahr St, Tanta", 30.783, 31.0,
                "Regional history and textiles.",
                "9:00 AM - 2:00 PM", 5, 3, 25, 12);

            // Monufia
            AddMuseum("Monufia", "Shibin El Kom Museum", "Abd Salam Arif St", 30.561, 31.009,
                "Local heritage museum.",
                "9:00 AM - 2:00 PM", 3, 2, 20, 10);

            // Beheira
            AddMuseum("Beheira", "Damanhur Museum", "El Gomhouria St, Damanhur", 31.042, 30.468,
                "Artifacts from the Western Delta.",
                "10:00 AM - 2:00 PM", 5, 3, 25, 12);

            // Faiyum
            AddMuseum("Faiyum", "Karanis Open Air Museum", "Kom Aushim, Faiyum", 29.517, 30.9,
                "Greco-Roman site with on-site museum.",
                "8:00 AM - 4:00 PM", 10, 5, 50, 25);

            // Beni Suef
            AddMuseum("Beni Suef", "Beni Suef Museum", "El Nasr St", 29.077, 31.097,
                "Local antiquities from Ihnasya el-Medina.",
                "9:00 AM - 2:00 PM", 5, 3, 25, 12);

            // Minya
            AddMuseum("Minya", "Minya Museum", "Corniche El Nil", 28.109, 30.751,
                "Holds treasures from Akhetaten (Tell el-Amarna).",
                "9:00 AM - 4:00 PM", 10, 5, 60, 30);

            // Asyut
            AddMuseum("Asyut", "Asyut National Museum", "El Gomhouria St", 27.178, 31.186,
                "Artifacts from the Middle Egyptian region.",
                "10:00 AM - 2:00 PM", 5, 3, 30, 15);

            // Sohag
            AddMuseum("Sohag", "Sohag Museum", "Sohag University Campus", 26.559, 31.695,
                "Coptic and Pharaonic collections.",
                "9:00 AM - 3:00 PM", 5, 3, 30, 15);

            // Qena
            AddMuseum("Qena", "Qena Museum", "Dendera Road, Qena", 26.164, 32.717,
                "Finds from Dendera and Naqada.",
                "9:00 AM - 4:00 PM", 8, 4, 40, 20);

            // Red Sea
            AddMuseum("Red Sea", "Hurghada Museum", "Hurghada, Touristic Promenade", 27.258, 33.812,
                "Modern museum covering Egyptian history.",
                "10:00 AM - 10:00 PM", 15, 8, 120, 60);

            // New Valley
            AddMuseum("New Valley", "Kharga Museum", "El Kharga, near Temple of Hibis", 25.451, 30.547,
                "Desert and oasis heritage.",
                "9:00 AM - 3:00 PM", 5, 3, 30, 15);

            // Matrouh
            AddMuseum("Matrouh", "Rommel Museum", "Mersa Matruh Corniche", 31.355, 27.236,
                "WWII memorabilia and ancient artifacts.",
                "10:00 AM - 2:00 PM", 5, 3, 30, 15);

            // North Sinai
            AddMuseum("North Sinai", "Arish Museum", "Arish, near the coast", 31.125, 33.797,
                "Sinai's Bedouin and ancient history.",
                "9:00 AM - 2:00 PM", 5, 3, 30, 15);

            // South Sinai
            AddMuseum("South Sinai", "Sharm el Sheikh Museum", "Sharm El Sheikh, Peace Road", 27.914, 34.33,
                "Display of Sinai's natural and cultural history.",
                "10:00 AM - 9:00 PM", 10, 5, 80, 40);

            return museums;
        }
    }
}