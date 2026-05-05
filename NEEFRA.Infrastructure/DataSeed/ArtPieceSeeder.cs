using MongoDB.Bson;
using MongoDB.Driver;
using NEEFRA.Core.Entities.Inerests;
using NEEFRA_API.Models;
using StackExchange.Redis;

public static class ArtPieceSeeder
{
    // ⚠️ Replace this with the actual Museum ObjectId from your DB
    private const string MuseumId = "000000000000000000000001";

    public static void Seed(IMongoDatabase database)
    {
        var collection = database.GetCollection<ArtPiece>("ArtPieces");

        var count = collection.CountDocuments(_ => true);

        if (count == 0)
        {
            var artPieces = new List<ArtPiece>
        {
            new() {
                Name = "a_double_state",
                ImageUrl = "pieces/a_double_state.jpg",
                MuseumId = MuseumId,
                Latitude = 0,
                Longitude = 0,
                Floor = 0
            },
            new() {
                // Floor 1 — "Double Statue of Baser and his Wife" → valid: true
                Name = "a_double_state_baster_and_his_wife",
                ImageUrl = "pieces/a_double_state_baster_and_his_wife.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70725,
                Longitude = 32.6444444444,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Statue of Ramesses, Statue of a Prisoner, Base with Captive Heads" → valid: true
                Name = "A_statue_rameses_VI_B_state_Base_c_statue_prisoner",
                ImageUrl = "pieces/A_statue_rameses_VI_B_state_Base_c_statue_prisoner.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7072777778,
                Longitude = 32.6443333333,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Statue of Amenhotep II" → valid: false
                Name = "amenhotep_II_at_practice",
                ImageUrl = "pieces/amenhotep_II_at_practice.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70725,
                Longitude = 32.6445277778,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "King Amenhotep III Crowned" → valid: false
                Name = "amenhotep_III_crowned_by_amon_ra",
                ImageUrl = "pieces/amenhotep_III_crowned_by_amon_ra.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7071944444,
                Longitude = 32.6445,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Amenhotep IV with the Double Crown" → valid: true
                Name = "Amenhotep_IV",
                ImageUrl = "pieces/Amenhotep_IV.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7075,
                Longitude = 32.6443888889,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Amenhotep IV with the Double Crown" (granite variant) → valid: true
                Name = "amenhotep_IV_with_Double_crown_geraniet",
                ImageUrl = "pieces/amenhotep_IV_with_Double_crown_geraniet.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7075,
                Longitude = 32.6443888889,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Statue of King Akhenaten, Half-Statue wearing Double Crown Holding Royal Insignia" → valid: true
                Name = "amenhotep_IV_with_sand",
                ImageUrl = "pieces/amenhotep_IV_with_sand.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7076944444,
                Longitude = 32.6445277778,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Faience and Glass" → valid: true
                Name = "Ancient_Egyptian_Faience_Glass_Objects_and_Ushabti_Figures",
                ImageUrl = "pieces/Ancient_Egyptian_Faience_Glass_Objects_and_Ushabti_Figures.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074444444,
                Longitude = 32.6443611111,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Measuring Tools" → valid: true
                Name = "Ancient_Egyptian_Masonry_and_Measuring_Tools",
                ImageUrl = "pieces/Ancient_Egyptian_Masonry_and_Measuring_Tools.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074722222,
                Longitude = 32.6443611111,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Ibis God Statue, Palette, Scribe Palette, Reed Brush, Color Palette" → valid: true
                Name = "Ancient_Egyptian_Metalworking_and_Bronze_Statuettes",
                ImageUrl = "pieces/Ancient_Egyptian_Metalworking_and_Bronze_Statuettes.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074444444,
                Longitude = 32.6444444444,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Ibis God Statue, Palette, Scribe Palette, Reed Brush, Color Palette" → valid: true
                Name = "Ancient_Egyptian_Scribe's_Palette_Painting_Tools",
                ImageUrl = "pieces/Ancient_Egyptian_Scribe's_Palette_Painting_Tools.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074444444,
                Longitude = 32.6444444444,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 2 — "Decorated Door Lintel of Thutmose III" → valid: true
                Name = "Ancient_Egyptian_Stone_Lintel",
                ImageUrl = "pieces/Ancient_Egyptian_Stone_Lintel.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7076666667,
                Longitude = 32.6446666667,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Model of a Ceremonial Boat" → valid: true
                Name = "Ancient_Egyptian_Wooden_Model_Boats",
                ImageUrl = "pieces/Ancient_Egyptian_Wooden_Model_Boats.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073888889,
                Longitude = 32.6443888889,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Archaeological Accessories" → valid: true
                Name = "ancient_Egyptians_went_to_secure_their_journey_to_the_afterlife",
                ImageUrl = "pieces/ancient_Egyptians_went_to_secure_their_journey_to_the_afterlife.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074166667,
                Longitude = 32.6444722222,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Statue of Ramesses III, Schist" → valid: true
                Name = "another_state_of_rameses_III",
                ImageUrl = "pieces/another_state_of_rameses_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073611111,
                Longitude = 32.6443888889,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 2 — "Decorated Door Lintel" → valid: true
                Name = "architectural_lintel_tuhutmusis_III",
                ImageUrl = "pieces/architectural_lintel_tuhutmusis_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7076944444,
                Longitude = 32.6446388889,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Architectural Elements" → valid: true
                Name = "Architectural_Ostra_a_with_Building_Plans",
                ImageUrl = "pieces/Architectural_Ostra_a_with_Building_Plans.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073888889,
                Longitude = 32.6443888889,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Base Inscribed with the Nine Bows" → valid: true
                Name = "base_with_nine_bows",
                ImageUrl = "pieces/base_with_nine_bows.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073888889,
                Longitude = 32.6445277778,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Obelisk of King Ramesses III" → valid: false
                Name = "Belisk_of_rameses_III",
                ImageUrl = "pieces/Belisk_of_rameses_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074444444,
                Longitude = 32.6446111111,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Block Statue of Amenhotep Son of Hapu" → valid: false
                Name = "block_state_of_amenhotep_sonof_habu",
                ImageUrl = "pieces/block_state_of_amenhotep_sonof_habu.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7071666667,
                Longitude = 32.6445555556,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Block Statue of Vizier Nes-Baqa-Shuty" → valid: false
                Name = "block_state_of_nespeka_shuty",
                ImageUrl = "pieces/block_state_of_nespeka_shuty.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074166667,
                Longitude = 32.6446111111,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Block Statue of Yamu-Nej" → valid: true
                Name = "block_state_of_yamunedjeh",
                ImageUrl = "pieces/block_state_of_yamunedjeh.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073333333,
                Longitude = 32.6443055556,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Weapons of the New Kingdom" → valid: false (ceremonial axe is a weapon)
                Name = "ceremonial_axe_of_ahmose",
                ImageUrl = "pieces/ceremonial_axe_of_ahmose.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70725,
                Longitude = 32.6445,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Archaeological Accessories" → valid: true
                Name = "collection_of_antiquities_belongs_to_the_family_of_priest_Ankh_ef_en_Khonsu",
                ImageUrl = "pieces/collection_of_antiquities_belongs_to_the_family_of_priest_Ankh_ef_en_Khonsu.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074166667,
                Longitude = 32.6444722222,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Standing Statue of King Seti I" → valid: true
                Name = "colossal_statue_of_seti_I",
                ImageUrl = "pieces/colossal_statue_of_seti_I.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70725,
                Longitude = 32.6444166667,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Sandstone Column" → valid: true
                Name = "column_from_sand_stone",
                ImageUrl = "pieces/column_from_sand_stone.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7077222222,
                Longitude = 32.6446111111,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Pillar of the God Amun" → valid: true
                Name = "column_of_god_amon",
                ImageUrl = "pieces/column_of_god_amon.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7077222222,
                Longitude = 32.6446944444,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 2 — "Coptic Funerary Stela" → valid: true
                Name = "Coptic tombstone",
                ImageUrl = "pieces/Coptic_tombstone.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7078055556,
                Longitude = 32.6446111111,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 2 — "Coptic Funerary Stela" → valid: true
                Name = "coptic_grave_stone",
                ImageUrl = "pieces/coptic_grave_stone.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7078055556,
                Longitude = 32.6446111111,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Weapons of the New Kingdom" → valid: false
                Name = "dagger_and_sheath_of_ahmose",
                ImageUrl = "pieces/dagger_and_sheath_of_ahmose.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70725,
                Longitude = 32.6445,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Stela from the Early Christian Era" → valid: true
                Name = "Early_christian_stela",
                ImageUrl = "pieces/Early_christian_stela.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7078333333,
                Longitude = 32.6446111111,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 2 — "False Door Stela" → valid: true
                Name = "false_door_stela",
                ImageUrl = "pieces/false_door_stela.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7078333333,
                Longitude = 32.6446388889,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 2 — "King Thutmose II and Queen Hatshepsut" → valid: true
                Name = "Fragment_Of_wall_Thutmosis_II_and_hatshepsut",
                ImageUrl = "pieces/Fragment_Of_wall_Thutmosis_II_and_hatshepsut.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7075,
                Longitude = 32.6444722222,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Gold Coins of the Roman Era" → valid: true
                Name = "gold_coins",
                ImageUrl = "pieces/gold_coins.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7076944444,
                Longitude = 32.6446111111,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 2 — "Head of Amenhotep I, Sandstone" → valid: true
                Name = "head_of_amenhoteb_III",
                ImageUrl = "pieces/head_of_amenhoteb_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073055556,
                Longitude = 32.6445277778,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Amenhotep III" → valid: true
                Name = "head_of_amenhoteb_III_kuar",
                ImageUrl = "pieces/head_of_amenhoteb_III_kuar.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7077777778,
                Longitude = 32.6446666667,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 2 — "Head of Amenhotep I, Sandstone" → valid: true
                Name = "head_of_amenhotep_I",
                ImageUrl = "pieces/head_of_amenhotep_I.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073055556,
                Longitude = 32.6445277778,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Head of Commander Nakht-Min" → valid: true
                Name = "head_of_nakhtmin",
                ImageUrl = "pieces/head_of_nakhtmin.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70725,
                Longitude = 32.6443888889,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Senusret III" (head of Sesostris III) → valid: true
                Name = "head_of_sesostris_III",
                ImageUrl = "pieces/head_of_sesostris_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70775,
                Longitude = 32.6446944444,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Horemheb and his Wife" → valid: false
                Name = "Horemheb_and_his_wife",
                ImageUrl = "pieces/Horemheb_and_his_wife.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073055556,
                Longitude = 32.6445555556,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 2 — "Inscribed Block with Cartouches of Akhenaten's Names" → valid: true
                Name = "inscribed_block_w_for_Akhnaton",
                ImageUrl = "pieces/inscribed_block_w_for_Akhnaton.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7075277778,
                Longitude = 32.6445277778,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 2 — "Inscribed Block with Cartouches Containing King Akhenaten's Names" → valid: true
                Name = "inscribed_block_with_akhnaton_names",
                ImageUrl = "pieces/inscribed_block_with_akhnaton_names.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073611111,
                Longitude = 32.6442777778,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 2 — "Architectural Fragment with Inscription of Hatshepsut" → valid: true
                Name = "jamb_of_incense_hatchepsut",
                ImageUrl = "pieces/jamb_of_incense_hatchepsut.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074444444,
                Longitude = 32.6444444444,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Stela of Queen Hatshepsut" → valid: true
                Name = "Limestone stela of Queen Hatshepsut",
                ImageUrl = "pieces/Limestone stela of Queen Hatshepsut.jpg",
                MuseumId = MuseumId,
                Latitude = 0,
                Longitude = 0,
                Floor = 0
            },
            new() {
                // Floor 2 — "Section of Wall Decoration" / Thutmose III → valid: true
                Name = "limestone_wall_relief_Thutmose III",
                ImageUrl = "pieces/limestone_wall_relief_Thutmose III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7078055556,
                Longitude = 32.6446388889,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Vizier Mentuhotep in Scribe Form" → valid: false
                Name = "Minister_Mentuhotep_writer",
                ImageUrl = "pieces/Minister_Mentuhotep_writer.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073055556,
                Longitude = 32.6441944444,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Mummy of King Ahmose" → valid: true
                Name = "mummy_of_ahmose",
                ImageUrl = "pieces/mummy_of_ahmose.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073055556,
                Longitude = 32.6444444444,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Royal Mummy" → valid: true
                Name = "mummy_of_pharone",
                ImageUrl = "pieces/mummy_of_pharone.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70725,
                Longitude = 32.6444444444,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Obelisk of King Ramesses III" → valid: false
                Name = "obelisk_of_rameses_III",
                ImageUrl = "pieces/obelisk_of_rameses_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074444444,
                Longitude = 32.6446111111,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Official Decorated with the Medal of Bravery" → valid: true
                Name = "official_wearing_the_gold_of_honor",
                ImageUrl = "pieces/official_wearing_the_gold_of_honor.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073888889,
                Longitude = 32.64425,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 2 — "Mentuhotep III in Osiride Form" → valid: true
                Name = "Osiris_statue_of_King_Mentuhotep_III",
                ImageUrl = "pieces/Osiris_statue_of_King_Mentuhotep_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7076388889,
                Longitude = 32.6446666667,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Double Statue of Baser and his Wife" (paser = baser) → valid: true
                Name = "paser_and_henut",
                ImageUrl = "pieces/paser_and_henut.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70725,
                Longitude = 32.6444444444,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 2 — "Pillar of Senusret I" → valid: true
                Name = "pillar_of_sesotris_I",
                ImageUrl = "pieces/pillar_of_sesotris_I.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7078611111,
                Longitude = 32.6445555556,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Statue of King Ramesses II with the Double Crown" → valid: true
                Name = "ramses_II_in_the_double_crown",
                ImageUrl = "pieces/ramses_II_in_the_double_crown.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7072222222,
                Longitude = 32.6444722222,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Victory Celebration Inscription" → valid: true
                Name = "relief_of_vectory",
                ImageUrl = "pieces/relief_of_vectory.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073333333,
                Longitude = 32.6443055556,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Royal Bows" → valid: false
                Name = "royal_bows",
                ImageUrl = "pieces/royal_bows.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7072777778,
                Longitude = 32.6445555556,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Pink Granite Sarcophagus of Queen Tausert" → valid: true
                Name = "sarcophaus",
                ImageUrl = "pieces/sarcophaus.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074444444,
                Longitude = 32.6445277778,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 2 — "Heb-Sed Festival Celebration, Sandstone" → valid: true
                Name = "Sed_Festival_Sandstone",
                ImageUrl = "pieces/Sed_Festival_Sandstone.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70775,
                Longitude = 32.6445277778,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Sekhmet with the Solar Disc" → valid: true
                Name = "sekhmet_A",
                ImageUrl = "pieces/sekhmet_A.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074444444,
                Longitude = 32.6443333333,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Statue of Sekhmet" → valid: true
                Name = "sekhmet_B",
                ImageUrl = "pieces/sekhmet_B.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073055556,
                Longitude = 32.6443055556,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Head of Sekhmet from a Colossal Statue" → valid: false
                Name = "sekhmet_c",
                ImageUrl = "pieces/sekhmet_c.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70725,
                Longitude = 32.6443055556,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Household Tools" → valid: true
                Name = "set_of_cosmatic_tools",
                ImageUrl = "pieces/set_of_cosmatic_tools.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7075,
                Longitude = 32.6445,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Double Statue of the God Amun and his Wife the Goddess Mut" → valid: true
                Name = "sobek_and_amenhotep III",
                ImageUrl = "pieces/sobek_and_amenhotep III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7076944444,
                Longitude = 32.6446944444,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Sphinx Making an Offering" → valid: false
                Name = "sphinx",
                ImageUrl = "pieces/sphinx.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7076666667,
                Longitude = 32.6446944444,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Statue of Amenhotep II" → valid: false
                Name = "state_of_amenhotep_II",
                ImageUrl = "pieces/state_of_amenhotep_II.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70725,
                Longitude = 32.6445277778,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Amenhotep III" → valid: true
                Name = "state_of_amenhotep_III_G",
                ImageUrl = "pieces/state_of_amenhotep_III_G.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7077777778,
                Longitude = 32.6446666667,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Statue of King Akhenaten, Half-Statue wearing Double Crown Holding Royal Insignia" → valid: true
                Name = "state_of_Amenhotep_IV",
                ImageUrl = "pieces/state_of_Amenhotep_IV.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7076944444,
                Longitude = 32.6445277778,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Statue of the God Amun" → valid: false
                Name = "state_of_god_amon",
                ImageUrl = "pieces/state_of_god_amon.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7077222222,
                Longitude = 32.6447777778,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Statue of Ramesses III, Schist" → valid: true
                Name = "state_of_rameses_III",
                ImageUrl = "pieces/state_of_rameses_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073611111,
                Longitude = 32.6443888889,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Statue of the Royal Scribe and Chief of Archers, Thay" → valid: false
                Name = "state_of_thai",
                ImageUrl = "pieces/state_of_thai.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074166667,
                Longitude = 32.64425,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Thutmose III" / "Statue of Thutmose III Seated on his Throne" → valid: true
                Name = "state_of_thutmusis_III",
                ImageUrl = "pieces/state_of_thutmusis_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074166667,
                Longitude = 32.6445277778,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Block Statue of Amenhotep Son of Hapu" → valid: false
                Name = "statue_of_Amenhotep_son_of_Hapu",
                ImageUrl = "pieces/statue_of_Amenhotep_son_of_Hapu.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7071666667,
                Longitude = 32.6445555556,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Stela of King Kamose" → valid: true
                Name = "stela_of_kamose",
                ImageUrl = "pieces/stela_of_kamose.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073888889,
                Longitude = 32.6445555556,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Stela of Queen Hatshepsut" → valid: true (using floor1 entry)
                Name = "stela_of_Queen_Hatshepsut",
                ImageUrl = "pieces/stela_of_Queen_Hatshepsut.jpg",
                MuseumId = MuseumId,
                Latitude = 0,
                Longitude = 0,
                Floor = 0
            },
            new() {
                // Floor 1 — "Stela of King Ramesses III" → valid: false
                Name = "stela_of_rameses_III",
                ImageUrl = "pieces/stela_of_rameses_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7075277778,
                Longitude = 32.6446111111,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 2 — "Section of Wall Decoration" → valid: true
                Name = "stela_or_wall_decoration",
                ImageUrl = "pieces/stela_or_wall_decoration.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7078055556,
                Longitude = 32.6446388889,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 2 — "Architectural Fragment with Inscription of Hatshepsut" → valid: true
                Name = "temple_wall_of_Amenhotep_IV",
                ImageUrl = "pieces/temple_wall_of_Amenhotep_IV.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074444444,
                Longitude = 32.6444444444,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Thutmose III" → valid: true
                Name = "thutmusis_III",
                ImageUrl = "pieces/thutmusis_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70775,
                Longitude = 32.6446388889,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 2 — "Mihrab Apex / Niche Peak" → valid: true
                Name = "top_of_niche",
                ImageUrl = "pieces/top_of_niche.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7077777778,
                Longitude = 32.6446944444,
                Floor = 2,
                Valid = true
            },
            new() {
                // Floor 1 — "Baboon Statue, Wax Statue, and Three Bronze Statues from Tomb of Ramesses XI" → valid: true
                Name = "two_wooden_masks",
                ImageUrl = "pieces/two_wooden_masks.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7074444444,
                Longitude = 32.6444166667,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Votive Statue of the New Kingdom" → valid: true
                Name = "votive_state",
                ImageUrl = "pieces/votive_state.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7075833333,
                Longitude = 32.6446388889,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Amenhotep III" → valid: true
                Name = "wallpainting_amenhoteb_III",
                ImageUrl = "pieces/wallpainting_amenhoteb_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7077777778,
                Longitude = 32.6446666667,
                Floor = 1,
                Valid = true
            },
            new() {
                // Floor 1 — "Weapons of the New Kingdom" → valid: false
                Name = "wepones_of_new_kingdom",
                ImageUrl = "pieces/wepones_of_new_kingdom.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70725,
                Longitude = 32.6445,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Amenemhat III" → valid: false
                Name = "amenemhat_III",
                ImageUrl = "pieces/amenemhat_III.jpg",
                MuseumId = MuseumId,
                Latitude = 25.70775,
                Longitude = 32.6447777778,
                Floor = 1,
                Valid = false
            },
            new() {
                // Floor 1 — "Statue of Amun-em-inet in Supplication" → valid: true
                Name = "amon em Anit",
                ImageUrl = "pieces/amenemint_as_beggar.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7073333333,
                Longitude = 32.6442222222,
                Floor = 1,
                Valid = true
            },

            new() {
                // Floor 2 — "Two Necklaces of Silver and Agate" → valid: true
                Name = "eh_hotep_necklace",
                ImageUrl = "pieces/eh_hotep_necklace.jpg",
                MuseumId = MuseumId,
                Latitude = 25.7076666667,
                Longitude = 32.6445277778,
                Floor = 2,
                Valid = true
            },
        };
            collection.InsertMany(artPieces);
            Console.WriteLine($"✅ Seeded {artPieces.Count} art pieces.");

        }
    }
}

// ============================================================
//  USAGE — call from Program.cs or wherever you bootstrap:
// ============================================================
// var client = new MongoClient("mongodb://localhost:27017");
// var db = client.GetDatabase("YourDatabaseName");
// await ArtPieceSeeder.SeedAsync(db);