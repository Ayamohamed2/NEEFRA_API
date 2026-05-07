using MongoDB.Driver;
using NEEFRA.Core.Entities;
using NEEFRA_API.Models;

namespace NEEFRA_API.Data
{
    public static class MuseumExtrasSeeder
    {
        // Luxor Museum has hardcoded Id = "000000000000000000000001" in GovernorateSeeder
        // All seed data here is linked to Luxor Museum as a demo
        private const string LuxorMuseumId = "69f92ca87c0ecbe67075785c";

        public static void Seed(IMongoDatabase database)
        {
            SeedGovernoratePhotos(database);
            SeedMuseumFacilities(database);
            SeedNearbyHotels(database);
            SeedNearbyRestaurants(database);
            SeedGiftShops(database);
            SeedCafes(database);
        }

        // ════════════════════════════════════════════════════════════════════
        // Governorate Photos  (linked to Governorates by Name lookup)
        // ════════════════════════════════════════════════════════════════════
        private static void SeedGovernoratePhotos(IMongoDatabase database)
        {
            var photosCollection = database.GetCollection<GovernoratePhoto>("GovernoratePhotos");
            if (photosCollection.CountDocuments(FilterDefinition<GovernoratePhoto>.Empty) > 0) return;

            var governoratesCollection = database.GetCollection<Governorate>("Governorates");
            var govDict = governoratesCollection
                .Find(FilterDefinition<Governorate>.Empty)
                .ToList()
                .ToDictionary(g => g.Name, g => g.Id!);

            var photos = new List<GovernoratePhoto>();

            void AddPhoto(string govName, string url, string caption, bool isPrimary = false)
            {
                if (govDict.TryGetValue(govName, out var govId))
                    photos.Add(new GovernoratePhoto
                    {
                        GovernorateId = govId,
                        PhotoUrl = url,
                        Caption = caption,
                        IsPrimary = isPrimary,
                        UploadedAt = DateTime.UtcNow
                    });
            }

            // Cairo
            AddPhoto("Cairo", "gv/cairo.jpg", "The Nile and Cairo skyline", true);
     

            // Alexandria
            AddPhoto("Alexandria", "gv/alexandria.webp", "Alexandria Corniche", true);
         

            // Giza
            AddPhoto("Giza", "gv/giza.jpg", "The Great Pyramids", true);


            // Luxor
            AddPhoto("Luxor", "gv/luxor.avif", "Karnak Temple", true);


            // Aswan
            AddPhoto("Aswan", "gv/aswan.avif", "Elephantine Island", true);
       

            photosCollection.InsertMany(photos);
            Console.WriteLine($"GovernoratePhotos seeded – {photos.Count} records.");
        }

        // ════════════════════════════════════════════════════════════════════
        // Museum Facilities  (one record per museum)
        // ════════════════════════════════════════════════════════════════════
        private static void SeedMuseumFacilities(IMongoDatabase database)
        {
            var collection = database.GetCollection<MuseumFacilities>("MuseumFacilities");
            if (collection.CountDocuments(FilterDefinition<MuseumFacilities>.Empty) > 0) return;

            var museumsCollection = database.GetCollection<Museum>("Museums");
            var museums = museumsCollection.Find(FilterDefinition<Museum>.Empty).ToList();

            var facilities = museums.Select(m => new MuseumFacilities
            {
                MuseumId = m.Id!,
                HasWifi = m.Name is "Grand Egyptian Museum (GEM)" or "Hurghada Museum" or "Luxor Museum" or "National Museum of Egyptian Civilization (NMEC)",
                IsWheelchairAccessible = m.Name is "Grand Egyptian Museum (GEM)" or "National Museum of Egyptian Civilization (NMEC)" or "Nubia Museum",
                HasAudioGuide = m.Name is "Grand Egyptian Museum (GEM)" or "The Egyptian Museum (Tahrir)" or "Luxor Museum" or "Alexandria National Museum",
                HasLockers = m.Name is "Grand Egyptian Museum (GEM)" or "The Egyptian Museum (Tahrir)" or "Nubia Museum",
                AudioGuideLanguages = m.Name is "Grand Egyptian Museum (GEM)" or "The Egyptian Museum (Tahrir)" ? "Arabic, English, French, Spanish, German" :
                                      m.Name is "Luxor Museum" or "Alexandria National Museum" ? "Arabic, English, French" : null,
                Notes = null,
                ImageUrl=$"download.jfif"
            }).ToList();

            collection.InsertMany(facilities);
            Console.WriteLine($"MuseumFacilities seeded – {facilities.Count} records.");
        }

        // ════════════════════════════════════════════════════════════════════
        // Nearby Hotels  (linked to Luxor Museum as demo)
        // ════════════════════════════════════════════════════════════════════
        private static void SeedNearbyHotels(IMongoDatabase database)
        {
            var collection = database.GetCollection<NearbyHotel>("NearbyHotels");
            if (collection.CountDocuments(FilterDefinition<NearbyHotel>.Empty) > 0) return;

            var hotels = new List<NearbyHotel>
            {
                new NearbyHotel
                {
                    MuseumId = LuxorMuseumId,
                    Name = "Sofitel Winter Palace Luxor",
                    Address = "Corniche El Nil, Luxor",
                    Latitude = 25.6978,
                    Longitude = 32.6379,
                    DistanceInKm = 0.3,
                    StarRating = 5,
                    PhoneNumber = "+20-95-238-0422",
                    Website = "https://sofitel.accor.com",
                },
                new NearbyHotel
                {
                    MuseumId = LuxorMuseumId,
                    Name = "Steigenberger Nile Palace",
                    Address = "Khalid Ibn Walid St, Luxor",
                    Latitude = 25.6942,
                    Longitude = 32.6394,
                    DistanceInKm = 0.6,
                    StarRating = 5,
                    PhoneNumber = "+20-95-236-6999",
                    Website = "https://steigenberger.com",
                },
                new NearbyHotel
                {
                    MuseumId = LuxorMuseumId,
                    Name = "Luxor Hotel",
                    Address = "Corniche El Nil, Luxor",
                    Latitude = 25.6988,
                    Longitude = 32.6371,
                    DistanceInKm = 0.2,
                    StarRating = 3,
                    PhoneNumber = "+20-95-238-0011",
                }
            };

            collection.InsertMany(hotels);
            Console.WriteLine($"NearbyHotels seeded – {hotels.Count} records.");
        }

        // ════════════════════════════════════════════════════════════════════
        // Nearby Restaurants  (linked to Luxor Museum as demo)
        // ════════════════════════════════════════════════════════════════════
        private static void SeedNearbyRestaurants(IMongoDatabase database)
        {
            var collection = database.GetCollection<NearbyRestaurant>("NearbyRestaurants");
            if (collection.CountDocuments(FilterDefinition<NearbyRestaurant>.Empty) > 0) return;

            var restaurants = new List<NearbyRestaurant>
            {
                new NearbyRestaurant
                {
                    MuseumId = LuxorMuseumId,
                    Name = "1886 Restaurant",
                    Address = "Winter Palace Hotel, Corniche El Nil",
                    Latitude = 25.6978,
                    Longitude = 32.638,
                    DistanceInKm = 0.3,
                    CuisineType = "International / Fine Dining",
                    PriceRange = "$$$",
                    PhoneNumber = "+20-95-238-0422",
                },
                new NearbyRestaurant
                {
                    MuseumId = LuxorMuseumId,
                    Name = "Sofra Restaurant",
                    Address = "Mohammed Farid St, Luxor",
                    Latitude = 25.6955,
                    Longitude = 32.6412,
                    DistanceInKm = 0.5,
                    CuisineType = "Egyptian",
                    PriceRange = "$$",
                    PhoneNumber = "+20-95-235-9752",
                },
                new NearbyRestaurant
                {
                    MuseumId = LuxorMuseumId,
                    Name = "Al-Sahaby Lane Restaurant",
                    Address = "Luxor Corniche",
                    Latitude = 25.6963,
                    Longitude = 32.6403,
                    DistanceInKm = 0.4,
                    CuisineType = "Egyptian / Nubian",
                    PriceRange = "$",
                }
            };

            collection.InsertMany(restaurants);
            Console.WriteLine($"NearbyRestaurants seeded – {restaurants.Count} records.");
        }

        // ════════════════════════════════════════════════════════════════════
        // Gift Shops  (linked to Luxor Museum as demo)
        // ════════════════════════════════════════════════════════════════════
        private static void SeedGiftShops(IMongoDatabase database)
        {
            var collection = database.GetCollection<GiftShop>("GiftShops");
            if (collection.CountDocuments(FilterDefinition<GiftShop>.Empty) > 0) return;

            var shops = new List<GiftShop>
            {
                new GiftShop
                {
                    MuseumId = LuxorMuseumId,
                    Name = "Luxor Museum Gift Shop",
                    Description = "Official museum shop selling replicas, books, and Egyptian souvenirs.",
                    OpenHours = "9:00 AM - 4:00 PM",
                    Location = "Ground Floor, near main entrance",
                }
            };

            collection.InsertMany(shops);
            Console.WriteLine($"GiftShops seeded – {shops.Count} records.");
        }

        // ════════════════════════════════════════════════════════════════════
        // Cafes  (linked to Luxor Museum as demo)
        // ════════════════════════════════════════════════════════════════════
        private static void SeedCafes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Cafe>("Cafes");
            if (collection.CountDocuments(FilterDefinition<Cafe>.Empty) > 0) return;

            var cafes = new List<Cafe>
            {
                new Cafe
                {
                    MuseumId = LuxorMuseumId,
                    Name = "Luxor Museum Café",
                    Description = "Light snacks, coffee, and juices in a relaxing garden setting.",
                    OpenHours = "9:00 AM - 4:00 PM",
                    Location = "Garden area, east wing",
                    HasOutdoorSeating = true
                }
            };

            collection.InsertMany(cafes);
            Console.WriteLine($"Cafes seeded – {cafes.Count} records.");
        }
    }
}
