using MongoDB.Driver;
using NEEFRA.Core.Entities.Piece;
using NEEFRA.Core.Enum;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Infrastructure.DataSeed
{
    public static class ArtifactSeeder
    {
        public static void Seed(IMongoDatabase database)
        {
            var collection = database.GetCollection<Artifact>("Artifacts");

            var count = collection.CountDocuments(_ => true);

            if (count == 0)
            {
                var artifacts = new List<Artifact>
        {
            // --------------------------------------------------------
            new() {
                Name = "a_double_state",
                Interests = I(
                    Interest.NewKingdom, Interest.RoyalStatue, Interest.GodAmun,
                    Interest.GodMut, Interest.GodsAndDivinity, Interest.Realism,
                    Interest.KarnakTemple, Interest.WomenInEgypt, Interest.PortraitSculpture)
            },
            // --------------------------------------------------------
            new() {
                Name = "a_double_state_baster_and_his_wife",
                Interests = I(
                    Interest.NewKingdom, Interest.RoyalOfficials, Interest.MilitaryHistory,
                    Interest.PortraitSculpture, Interest.WomenInEgypt, Interest.RoyalStatue)
            },
            // --------------------------------------------------------
            new() {
                Name = "A_statue_rameses_VI_B_state_Base_c_statue_prisoner",
                Interests = I(
                    Interest.NewKingdom, Interest.RoyalStatue, Interest.GodAmun,
                    Interest.MilitaryHistory, Interest.KarnakTemple, Interest.RamessesIII,
                    Interest.ArchaeologicalDiscovery, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "amenhotep_II_at_practice",
                Interests = I(
                    Interest.NewKingdom, Interest.MilitaryHistory, Interest.Archery,
                    Interest.ThutmoseIII, Interest.RoyalStatue, Interest.Weapons,
                    Interest.KarnakTemple, Interest.Mummies)
            },
            // --------------------------------------------------------
            new() {
                Name = "amenhotep_III_crowned_by_amon_ra",
                Interests = I(
                    Interest.NewKingdom, Interest.AmenhotepIII, Interest.GodAmun,
                    Interest.Akhenaten, Interest.RoyalStatue, Interest.GodsAndDivinity,
                    Interest.KarnakTemple, Interest.ReliefArt)
            },
            // --------------------------------------------------------
            new() {
                Name = "Amenhotep_IV",
                Interests = I(
                    Interest.NewKingdom, Interest.Akhenaten, Interest.GodAten,
                    Interest.AmarnaArt, Interest.RoyalStatue, Interest.GodsAndDivinity,
                    Interest.KarnakTemple, Interest.ReliefArt, Interest.Realism)
            },
            // --------------------------------------------------------
            new() {
                Name = "amenhotep_IV_with_Double_crown_geraniet",
                Interests = I(
                    Interest.NewKingdom, Interest.Akhenaten, Interest.GodAten,
                    Interest.AmarnaArt, Interest.RoyalStatue, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "amenhotep_IV_with_sand",
                Interests = I(
                    Interest.NewKingdom, Interest.Akhenaten, Interest.AmarnaArt,
                    Interest.RoyalStatue, Interest.Stonework, Interest.GodsAndDivinity)
            },
            // --------------------------------------------------------
            new() {
                Name = "Ancient_Egyptian_Faience_Glass_Objects_and_Ushabti_Figures",
                Interests = I(
                    Interest.NewKingdom, Interest.Faience, Interest.FuneraryReligion,
                    Interest.AftLife, Interest.CraftAndManufacturing, Interest.FuneraryArt,
                    Interest.ArchaeologicalDiscovery)
            },
            // --------------------------------------------------------
            new() {
                Name = "Ancient_Egyptian_Masonry_and_Measuring_Tools",
                Interests = I(
                    Interest.NewKingdom, Interest.Engineering, Interest.CraftAndManufacturing,
                    Interest.TempleArchitecture, Interest.Administration, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "Ancient_Egyptian_Metalworking_and_Bronze_Statuettes",
                Interests = I(
                    Interest.Metalworking, Interest.CraftAndManufacturing, Interest.GodsAndDivinity,
                    Interest.GodAmun, Interest.GodHorus, Interest.GodIsis,
                    Interest.NewKingdom, Interest.FuneraryArt)
            },
            // --------------------------------------------------------
            new() {
                Name = "Ancient_Egyptian_Scribe's_Palette_Painting_Tools",
                Interests = I(
                    Interest.AncientWriting, Interest.ScribesAndLiteracy, Interest.CraftAndManufacturing,
                    Interest.GodThoth, Interest.NewKingdom, Interest.FuneraryReligion)
            },
            // --------------------------------------------------------
            new() {
                Name = "Ancient_Egyptian_Stone_Lintel",
                Interests = I(
                    Interest.NewKingdom, Interest.TempleArchitecture, Interest.KarnakTemple,
                    Interest.ThutmoseIII, Interest.Stonework, Interest.ReliefArt)
            },
            // --------------------------------------------------------
            new() {
                Name = "Ancient_Egyptian_Wooden_Model_Boats",
                Interests = I(
                    Interest.BoatBuilding, Interest.RiverNile, Interest.FuneraryReligion,
                    Interest.AftLife, Interest.MiddleKingdom, Interest.Woodworking,
                    Interest.TradeAndEconomy)
            },
            // --------------------------------------------------------
            new() {
                Name = "ancient_Egyptians_went_to_secure_their_journey_to_the_afterlife",
                Interests = I(
                    Interest.FuneraryReligion, Interest.AftLife, Interest.GodOsiris,
                    Interest.FuneraryArt, Interest.NewKingdom, Interest.Mummies)
            },
            // --------------------------------------------------------
            new() {
                Name = "another_state_of_rameses_III",
                Interests = I(
                    Interest.NewKingdom, Interest.RamessesIII, Interest.GodOsiris,
                    Interest.RoyalStatue, Interest.KarnakTemple, Interest.MilitaryHistory,
                    Interest.SeaPeoples)
            },
            // --------------------------------------------------------
            new() {
                Name = "architectural_lintel_tuhutmusis_III",
                Interests = I(
                    Interest.NewKingdom, Interest.ThutmoseIII, Interest.TempleArchitecture,
                    Interest.KarnakTemple, Interest.Stonework, Interest.ReliefArt)
            },
            // --------------------------------------------------------
            new() {
                Name = "Architectural_Ostra_a_with_Building_Plans",
                Interests = I(
                    Interest.Engineering, Interest.TempleArchitecture, Interest.AncientWriting,
                    Interest.CraftAndManufacturing, Interest.NewKingdom, Interest.Administration)
            },
            // --------------------------------------------------------
            new() {
                Name = "base_with_nine_bows",
                Interests = I(
                    Interest.NewKingdom, Interest.MilitaryHistory, Interest.RoyalStatue,
                    Interest.Stonework, Interest.GodsAndDivinity, Interest.Administration)
            },
            // --------------------------------------------------------
            new() {
                Name = "Belisk_of_rameses_III",
                Interests = I(
                    Interest.NewKingdom, Interest.RamessesIII, Interest.Obelisks,
                    Interest.KarnakTemple, Interest.Stonework, Interest.AncientWriting)
            },
            // --------------------------------------------------------
            new() {
                Name = "block_state_of_amenhotep_sonof_habu",
                Interests = I(
                    Interest.NewKingdom, Interest.AmenhotepIII, Interest.BlockStatue,
                    Interest.Engineering, Interest.LuxorTemple, Interest.Administration,
                    Interest.RoyalOfficials, Interest.ScribesAndLiteracy)
            },
            // --------------------------------------------------------
            new() {
                Name = "block_state_of_nespeka_shuty",
                Interests = I(
                    Interest.ThirdIntermediatePeriod, Interest.BlockStatue, Interest.GodAmun,
                    Interest.Administration, Interest.Viziers, Interest.RoyalOfficials,
                    Interest.KarnakTemple, Interest.FuneraryReligion, Interest.GodHathor)
            },
            // --------------------------------------------------------
            new() {
                Name = "block_state_of_yamunedjeh",
                Interests = I(
                    Interest.NewKingdom, Interest.ThutmoseIII, Interest.BlockStatue,
                    Interest.MilitaryHistory, Interest.Engineering, Interest.Obelisks,
                    Interest.Administration, Interest.RoyalOfficials)
            },
            // --------------------------------------------------------
            new() {
                Name = "ceremonial_axe_of_ahmose",
                Interests = I(
                    Interest.NewKingdom, Interest.Ahmose, Interest.HyksosWar,
                    Interest.MilitaryHistory, Interest.Metalworking, Interest.Jewelry,
                    Interest.Weapons, Interest.WomenInEgypt)
            },
            // --------------------------------------------------------
            new() {
                Name = "collection_of_antiquities_belongs_to_the_family_of_priest_Ankh_ef_en_Khonsu",
                Interests = I(
                    Interest.NewKingdom, Interest.FuneraryReligion, Interest.GodAmun,
                    Interest.Administration, Interest.GodsAndDivinity, Interest.FuneraryArt)
            },
            // --------------------------------------------------------
            new() {
                Name = "colossal_statue_of_seti_I",
                Interests = I(
                    Interest.NewKingdom, Interest.RamessesII, Interest.RoyalStatue,
                    Interest.Stonework, Interest.ArchaeologicalDiscovery, Interest.PortraitSculpture)
            },
            // --------------------------------------------------------
            new() {
                Name = "column_from_sand_stone",
                Interests = I(
                    Interest.FirstIntermediatePeriod, Interest.GodAmun, Interest.Columns,
                    Interest.KarnakTemple, Interest.TempleArchitecture, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "column_of_god_amon",
                Interests = I(
                    Interest.FirstIntermediatePeriod, Interest.GodAmun, Interest.Columns,
                    Interest.KarnakTemple, Interest.TempleArchitecture, Interest.GodsAndDivinity)
            },
            // --------------------------------------------------------
            new() {
                Name = "Coptic tombstone",
                Interests = I(
                    Interest.CopticChristian, Interest.FuneraryArt, Interest.FuneraryReligion,
                    Interest.RomanPeriod, Interest.Stonework, Interest.AncientWriting)
            },
            // --------------------------------------------------------
            new() {
                Name = "coptic_grave_stone",
                Interests = I(
                    Interest.CopticChristian, Interest.FuneraryArt, Interest.FuneraryReligion,
                    Interest.RomanPeriod, Interest.Stonework, Interest.AncientWriting)
            },
            // --------------------------------------------------------
            new() {
                Name = "dagger_and_sheath_of_ahmose",
                Interests = I(
                    Interest.NewKingdom, Interest.Ahmose, Interest.Weapons,
                    Interest.MilitaryHistory, Interest.Metalworking, Interest.HyksosWar)
            },
            // --------------------------------------------------------
            new() {
                Name = "Early_christian_stela",
                Interests = I(
                    Interest.CopticChristian, Interest.FuneraryArt, Interest.RomanPeriod,
                    Interest.Stonework, Interest.AncientWriting, Interest.GodsAndDivinity)
            },
            // --------------------------------------------------------
            new() {
                Name = "false_door_stela",
                Interests = I(
                    Interest.PtolemaicPeriod, Interest.FuneraryReligion, Interest.AftLife,
                    Interest.TempleArchitecture, Interest.Stonework, Interest.ReliefArt,
                    Interest.GodsAndDivinity)
            },
            // --------------------------------------------------------
            new() {
                Name = "Fragment_Of_wall_Thutmosis_II_and_hatshepsut",
                Interests = I(
                    Interest.NewKingdom, Interest.Hatshepsut, Interest.ThutmoseIII,
                    Interest.GodAmun, Interest.WomenInEgypt, Interest.ReliefArt,
                    Interest.KarnakTemple, Interest.TradeAndEconomy)
            },
            // --------------------------------------------------------
            new() {
                Name = "gold_coins",
                Interests = I(
                    Interest.RomanPeriod, Interest.IslamicHistory, Interest.TradeAndEconomy,
                    Interest.Metalworking, Interest.CachettesAndHoards, Interest.ArchaeologicalDiscovery)
            },
            // --------------------------------------------------------
            new() {
                Name = "head_of_amenhoteb_III",
                Interests = I(
                    Interest.NewKingdom, Interest.AmenhotepIII, Interest.PortraitSculpture,
                    Interest.RoyalStatue, Interest.Realism, Interest.Stonework,
                    Interest.ArchaeologicalDiscovery)
            },
            // --------------------------------------------------------
            new() {
                Name = "head_of_amenhoteb_III_kuar",
                Interests = I(
                    Interest.NewKingdom, Interest.AmenhotepIII, Interest.PortraitSculpture,
                    Interest.RoyalStatue, Interest.Realism, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "head_of_amenhotep_I",
                Interests = I(
                    Interest.NewKingdom, Interest.Ahmose, Interest.PortraitSculpture,
                    Interest.RoyalStatue, Interest.KarnakTemple, Interest.Stonework,
                    Interest.MiddleKingdom)
            },
            // --------------------------------------------------------
            new() {
                Name = "head_of_nakhtmin",
                Interests = I(
                    Interest.NewKingdom, Interest.Tutankhamun, Interest.MilitaryHistory,
                    Interest.PortraitSculpture, Interest.RoyalStatue, Interest.Horemheb,
                    Interest.RoyalOfficials)
            },
            // --------------------------------------------------------
            new() {
                Name = "head_of_sesostris_III",
                Interests = I(
                    Interest.MiddleKingdom, Interest.SenusretIII, Interest.PortraitSculpture,
                    Interest.Realism, Interest.RoyalStatue, Interest.ArchaeologicalDiscovery)
            },
            // --------------------------------------------------------
            new() {
                Name = "Horemheb_and_his_wife",
                Interests = I(
                    Interest.NewKingdom, Interest.Horemheb, Interest.AmarnaArt,
                    Interest.RoyalStatue, Interest.WomenInEgypt, Interest.MilitaryHistory,
                    Interest.PortraitSculpture, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "inscribed_block_w_for_Akhnaton",
                Interests = I(
                    Interest.NewKingdom, Interest.Akhenaten, Interest.GodAten,
                    Interest.AmarnaArt, Interest.KarnakTemple, Interest.ReliefArt,
                    Interest.TempleArchitecture, Interest.AncientWriting)
            },
            // --------------------------------------------------------
            new() {
                Name = "inscribed_block_with_akhnaton_names",
                Interests = I(
                    Interest.NewKingdom, Interest.Akhenaten, Interest.GodAten,
                    Interest.AmarnaArt, Interest.KarnakTemple, Interest.AncientWriting)
            },
            // --------------------------------------------------------
            new() {
                Name = "jamb_of_incense_hatchepsut",
                Interests = I(
                    Interest.NewKingdom, Interest.Hatshepsut, Interest.GodAmun,
                    Interest.TempleArchitecture, Interest.KarnakTemple, Interest.Stonework,
                    Interest.TradeAndEconomy)
            },
            // --------------------------------------------------------
            new() {
                Name = "Limestone stela of Queen Hatshepsut",
                Interests = I(
                    Interest.NewKingdom, Interest.Hatshepsut, Interest.GodAmun,
                    Interest.WomenInEgypt, Interest.ReliefArt, Interest.KarnakTemple,
                    Interest.RoyalStatue, Interest.GodsAndDivinity,
                    Interest.AncientWriting, Interest.FuneraryReligion, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "limestone_wall_relief_Thutmose III",
                Interests = I(
                    Interest.NewKingdom, Interest.ThutmoseIII, Interest.ReliefArt,
                    Interest.MilitaryHistory, Interest.PortraitSculpture, Interest.TempleArchitecture,
                    Interest.KarnakTemple, Interest.Stonework, Interest.AncientWriting,
                    Interest.GodAmun)
            },
            // --------------------------------------------------------
            new() {
                Name = "Minister_Mentuhotep_writer",
                Interests = I(
                    Interest.MiddleKingdom, Interest.ScribesAndLiteracy, Interest.Viziers,
                    Interest.Administration, Interest.RoyalOfficials, Interest.AncientWriting,
                    Interest.BlockStatue)
            },
            // --------------------------------------------------------
            new() {
                Name = "mummy_of_ahmose",
                Interests = I(
                    Interest.NewKingdom, Interest.Ahmose, Interest.Mummies,
                    Interest.HyksosWar, Interest.MilitaryHistory, Interest.FuneraryReligion,
                    Interest.ArchaeologicalDiscovery)
            },
            // --------------------------------------------------------
            new() {
                Name = "mummy_of_pharone",
                Interests = I(
                    Interest.NewKingdom, Interest.Mummies, Interest.FuneraryReligion,
                    Interest.AftLife, Interest.ArchaeologicalDiscovery, Interest.RoyalStatue)
            },
            // --------------------------------------------------------
            new() {
                Name = "obelisk_of_rameses_III",
                Interests = I(
                    Interest.NewKingdom, Interest.RamessesIII, Interest.Obelisks,
                    Interest.KarnakTemple, Interest.Stonework, Interest.AncientWriting)
            },
            // --------------------------------------------------------
            new() {
                Name = "official_wearing_the_gold_of_honor",
                Interests = I(
                    Interest.NewKingdom, Interest.MilitaryHistory, Interest.Jewelry,
                    Interest.RoyalOfficials, Interest.Administration, Interest.RoyalStatue,
                    Interest.Realism)
            },
            // --------------------------------------------------------
            new() {
                Name = "Osiris_statue_of_King_Mentuhotep_III",
                Interests = I(
                    Interest.MiddleKingdom, Interest.MentuhotepDynasty, Interest.GodOsiris,
                    Interest.FuneraryReligion, Interest.RoyalStatue, Interest.Stonework,
                    Interest.GodsAndDivinity)
            },
            // --------------------------------------------------------
            new() {
                Name = "paser_and_henut",
                Interests = I(
                    Interest.NewKingdom, Interest.RamessesII, Interest.RoyalOfficials,
                    Interest.Administration, Interest.Viziers, Interest.PortraitSculpture)
            },
            // --------------------------------------------------------
            new() {
                Name = "pillar_of_sesotris_I",
                Interests = I(
                    Interest.MiddleKingdom, Interest.SenusretIII, Interest.GodOsiris,
                    Interest.Columns, Interest.KarnakTemple, Interest.FuneraryReligion,
                    Interest.Stonework, Interest.ReliefArt)
            },
            // --------------------------------------------------------
            new() {
                Name = "ramses_II_in_the_double_crown",
                Interests = I(
                    Interest.NewKingdom, Interest.RamessesII, Interest.MilitaryHistory,
                    Interest.RoyalStatue, Interest.Stonework, Interest.PortraitSculpture)
            },
            // --------------------------------------------------------
            new() {
                Name = "relief_of_vectory",
                Interests = I(
                    Interest.NewKingdom, Interest.Tutankhamun, Interest.MilitaryHistory,
                    Interest.ReliefArt, Interest.GodsAndDivinity, Interest.KarnakTemple,
                    Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "royal_bows",
                Interests = I(
                    Interest.NewKingdom, Interest.Tutankhamun, Interest.Archery,
                    Interest.Weapons, Interest.MilitaryHistory, Interest.HyksosWar,
                    Interest.CraftAndManufacturing)
            },
            // --------------------------------------------------------
            new() {
                Name = "sarcophaus",
                Interests = I(
                    Interest.NewKingdom, Interest.FuneraryReligion, Interest.AftLife,
                    Interest.WomenInEgypt, Interest.FuneraryArt, Interest.GodsAndDivinity,
                    Interest.GodIsis, Interest.Stonework, Interest.CachettesAndHoards)
            },
            // --------------------------------------------------------
            new() {
                Name = "Sed_Festival_Sandstone",
                Interests = I(
                    Interest.NewKingdom, Interest.GodsAndDivinity, Interest.RoyalStatue,
                    Interest.TempleArchitecture, Interest.ReliefArt, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "sekhmet_A",
                Interests = I(
                    Interest.NewKingdom, Interest.AmenhotepIII, Interest.GodessSekhmet,
                    Interest.GodsAndDivinity, Interest.MilitaryHistory, Interest.RoyalStatue,
                    Interest.KarnakTemple, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "sekhmet_B",
                Interests = I(
                    Interest.NewKingdom, Interest.AmenhotepIII, Interest.GodessSekhmet,
                    Interest.GodsAndDivinity, Interest.RoyalStatue, Interest.KarnakTemple,
                    Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "sekhmet_c",
                Interests = I(
                    Interest.NewKingdom, Interest.AmenhotepIII, Interest.GodessSekhmet,
                    Interest.GodsAndDivinity, Interest.RoyalStatue, Interest.KarnakTemple,
                    Interest.Stonework, Interest.GodMut)
            },
            // --------------------------------------------------------
            new() {
                Name = "set_of_cosmatic_tools",
                Interests = I(
                    Interest.NewKingdom, Interest.CraftAndManufacturing, Interest.DailyLife,
                    Interest.WomenInEgypt, Interest.FuneraryArt, Interest.ArchaeologicalDiscovery)
            },
            // --------------------------------------------------------
            new() {
                Name = "sobek_and_amenhotep III",
                Interests = I(
                    Interest.NewKingdom, Interest.AmenhotepIII, Interest.GodSobek,
                    Interest.GodsAndDivinity, Interest.RoyalStatue, Interest.Stonework,
                    Interest.ArchaeologicalDiscovery, Interest.CachettesAndHoards,
                    Interest.RiverNile, Interest.FuneraryReligion, Interest.TempleArchitecture)
            },
            // --------------------------------------------------------
            new() {
                Name = "sphinx",
                Interests = I(
                    Interest.NewKingdom, Interest.Tutankhamun, Interest.GodHorus,
                    Interest.GodsAndDivinity, Interest.RoyalStatue, Interest.KarnakTemple,
                    Interest.Ahmose, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "state_of_amenhotep_II",
                Interests = I(
                    Interest.NewKingdom, Interest.MilitaryHistory, Interest.RoyalStatue,
                    Interest.KarnakTemple, Interest.Stonework, Interest.PortraitSculpture)
            },
            // --------------------------------------------------------
            new() {
                Name = "state_of_amenhotep_III_G",
                Interests = I(
                    Interest.NewKingdom, Interest.AmenhotepIII, Interest.RoyalStatue,
                    Interest.KarnakTemple, Interest.Realism, Interest.PortraitSculpture,
                    Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "state_of_Amenhotep_IV",
                Interests = I(
                    Interest.NewKingdom, Interest.Akhenaten, Interest.GodAten,
                    Interest.AmarnaArt, Interest.RoyalStatue, Interest.KarnakTemple,
                    Interest.Realism, Interest.GodsAndDivinity)
            },
            // --------------------------------------------------------
            new() {
                Name = "state_of_god_amon",
                Interests = I(
                    Interest.NewKingdom, Interest.GodAmun, Interest.Tutankhamun,
                    Interest.GodsAndDivinity, Interest.RoyalStatue, Interest.KarnakTemple,
                    Interest.Stonework, Interest.CachettesAndHoards)
            },
            // --------------------------------------------------------
            new() {
                Name = "state_of_rameses_III",
                Interests = I(
                    Interest.NewKingdom, Interest.RamessesIII, Interest.GodOsiris,
                    Interest.MilitaryHistory, Interest.SeaPeoples, Interest.LibyanWars,
                    Interest.RoyalStatue, Interest.KarnakTemple, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "state_of_thai",
                Interests = I(
                    Interest.NewKingdom, Interest.ScribesAndLiteracy, Interest.MilitaryHistory,
                    Interest.RoyalOfficials, Interest.PortraitSculpture, Interest.Woodworking,
                    Interest.AncientWriting)
            },
            // --------------------------------------------------------
            new() {
                Name = "state_of_thutmusis_III",
                Interests = I(
                    Interest.NewKingdom, Interest.ThutmoseIII, Interest.MilitaryHistory,
                    Interest.RoyalStatue, Interest.KarnakTemple, Interest.PortraitSculpture,
                    Interest.Stonework, Interest.CachettesAndHoards)
            },
            // --------------------------------------------------------
            new() {
                Name = "statue_of_Amenhotep_son_of_Hapu",
                Interests = I(
                    Interest.NewKingdom, Interest.AmenhotepIII, Interest.ScribesAndLiteracy,
                    Interest.Engineering, Interest.LuxorTemple, Interest.Administration,
                    Interest.RoyalOfficials, Interest.BlockStatue, Interest.GodsAndDivinity,
                    Interest.KarnakTemple)
            },
            // --------------------------------------------------------
            new() {
                Name = "stela_of_kamose",
                Interests = I(
                    Interest.NewKingdom, Interest.Ahmose, Interest.HyksosWar,
                    Interest.MilitaryHistory, Interest.AncientWriting, Interest.KarnakTemple,
                    Interest.ArchaeologicalDiscovery)
            },
            // --------------------------------------------------------
            new() {
                Name = "stela_of_Queen_Hatshepsut",
                Interests = I(
                    Interest.NewKingdom, Interest.Hatshepsut, Interest.GodAmun,
                    Interest.WomenInEgypt, Interest.ReliefArt, Interest.KarnakTemple,
                    Interest.GodsAndDivinity, Interest.AncientWriting)
            },
            // --------------------------------------------------------
            new() {
                Name = "stela_of_rameses_III",
                Interests = I(
                    Interest.NewKingdom, Interest.RamessesIII, Interest.GodAmun,
                    Interest.MilitaryHistory, Interest.ReliefArt, Interest.Stonework,
                    Interest.GodsAndDivinity)
            },
            // --------------------------------------------------------
            new() {
                Name = "stela_or_wall_decoration",
                Interests = I(
                    Interest.NewKingdom, Interest.ReliefArt, Interest.FuneraryReligion,
                    Interest.GodsAndDivinity, Interest.AncientWriting, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "temple_wall_of_Amenhotep_IV",
                Interests = I(
                    Interest.NewKingdom, Interest.Akhenaten, Interest.GodAten,
                    Interest.AmarnaArt, Interest.ReliefArt, Interest.KarnakTemple,
                    Interest.TempleArchitecture, Interest.CraftAndManufacturing)
            },
            // --------------------------------------------------------
            new() {
                Name = "thutmusis_III",
                Interests = I(
                    Interest.NewKingdom, Interest.ThutmoseIII, Interest.MilitaryHistory,
                    Interest.RoyalStatue, Interest.KarnakTemple, Interest.Hatshepsut,
                    Interest.PortraitSculpture, Interest.Stonework)
            },
            // --------------------------------------------------------
            new() {
                Name = "top_of_niche",
                Interests = I(
                    Interest.TempleArchitecture, Interest.Stonework, Interest.ReliefArt,
                    Interest.GodsAndDivinity, Interest.NewKingdom)
            },
            // --------------------------------------------------------
            new() {
                Name = "two_wooden_masks",
                Interests = I(
                    Interest.LatePeriod, Interest.FuneraryArt, Interest.FuneraryReligion,
                    Interest.Mummies, Interest.RomanPeriod, Interest.Woodworking,
                    Interest.PortraitSculpture)
            },
            // --------------------------------------------------------
            new() {
                Name = "votive_state",
                Interests = I(
                    Interest.NewKingdom, Interest.GodSobek, Interest.GodsAndDivinity,
                    Interest.RoyalOfficials, Interest.Administration, Interest.RoyalStatue,
                    Interest.Stonework, Interest.ArchaeologicalDiscovery)
            },
            // --------------------------------------------------------
            new() {
                Name = "wallpainting_amenhoteb_III",
                Interests = I(
                    Interest.NewKingdom, Interest.AmenhotepIII, Interest.WallPainting,
                    Interest.ReliefArt, Interest.Administration, Interest.WomenInEgypt,
                    Interest.PortraitSculpture, Interest.FuneraryArt)
            },
            // --------------------------------------------------------
            new() {
                Name = "wepones_of_new_kingdom",
                Interests = I(
                    Interest.NewKingdom, Interest.Weapons, Interest.MilitaryHistory,
                    Interest.Archery, Interest.ThutmoseIII, Interest.Tutankhamun,
                    Interest.Metalworking, Interest.CraftAndManufacturing)
            },
            // --------------------------------------------------------
            new() {
                Name = "amenemhat_III",
                Interests = I(
                    Interest.MiddleKingdom, Interest.RoyalStatue, Interest.PortraitSculpture,
                    Interest.Realism, Interest.Stonework, Interest.Administration)
            },
            // --------------------------------------------------------
            new() {
                Name = "amon em Anit",
                Interests = I(
                    Interest.NewKingdom, Interest.RamessesII, Interest.RoyalOfficials,
                    Interest.GodHathor, Interest.Realism, Interest.PortraitSculpture,
                    Interest.TempleArchitecture, Interest.AncientWriting, Interest.Administration)
            },

            // ========================================================
            //  ✅ NEW ARTIFACTS
            // ========================================================

            // --------------------------------------------------------
            
            // --------------------------------------------------------
            new() {
                Name = "eh_hotep_necklace",
                Interests = I(
                    Interest.NewKingdom, Interest.Ahmose, Interest.Jewelry,
                    Interest.Metalworking, Interest.WomenInEgypt, Interest.CraftAndManufacturing,
                    Interest.FuneraryArt, Interest.HyksosWar, Interest.ArchaeologicalDiscovery)
            },
        };

                collection.InsertMany(artifacts);
            Console.WriteLine($"✅ Seeded {artifacts.Count} artifacts with interests.");
        }

        // Helper — converts enum values to List<string>
         static List<string> I(params Interest[] interests)
            => interests.Select(x => x.ToString()).ToList();
    }
    }

    // ============================================================
    //  USAGE — call from Program.cs or wherever you bootstrap:
    // ============================================================
    // var client = new MongoClient("mongodb://localhost:27017");
    // var db = client.GetDatabase("LuxorMuseumDB");
    // await ArtifactSeeder.SeedAsync(db);

}

