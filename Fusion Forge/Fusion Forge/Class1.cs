using UnityEngine;
using System;
using System.Collections.Generic;
using BepInEx;
using xiaoye97;
using HarmonyLib;
using CommonAPI;
using CommonAPI.Systems;

namespace FusionForge
{
    [BepInPlugin("mod.jamesfire.FusionForge", "FusionForge Plug-In", "1.1.0.0")]
    [BepInDependency("me.xiaoye97.plugin.Dyson.LDBTool", BepInDependency.DependencyFlags.HardDependency)]
    [CommonAPISubmoduleDependency(nameof(ProtoRegistry), nameof(UtilSystem))]
    [BepInDependency(CommonAPIPlugin.GUID)]
    [BepInProcess("DSPGAME.exe")]
    public class FusionForge : BaseUnityPlugin
    {
        void Awake()
        {
            Logger.LogInfo("FusionForge initializing 1.1");
            //resources.LoadAssetBundle("FusionForge");
            #region Strings
            //Tech Strings
            ProtoRegistry.RegisterString("FusionTech", "Advanced Fusion");
            ProtoRegistry.RegisterString("FusionTechDesc", "Utilizing information and methods learned from Antimatter containment, we can create powerful and efficient fusion devices.");
            ProtoRegistry.RegisterString("FusionTechConc", "Now we can make a fully fledged Fusion Forge. It is capable of fusing raw materials very quickly, as well as more mundane Particle Collider recipes.");

            //Machine Strings
            ProtoRegistry.RegisterString("FusionForge", "Fusion Forge");
            ProtoRegistry.RegisterString("FusionForgeDesc", "With advanced gravity and electromagnetic manipulation technology, this device can easily fuse pure materials. It is also capable of more mundane Particle Collider recipes.");

            //Recipe Strings
            ProtoRegistry.RegisterString("IronFusion", "Iron Ingot Fusion");
            ProtoRegistry.RegisterString("IronFusionDesc", "Fusing pure iron ingots from deuterium is a long process, due to being the Iron Peak of fusion.");
            ProtoRegistry.RegisterString("CopperFusion", "Copper Ingot Fusion");
            ProtoRegistry.RegisterString("CopperFusionDesc", "Fusing pure copper ingots from deuterium is a long process, due to copper being past the Iron Peak.");
            ProtoRegistry.RegisterString("CoalFusion", "Coal Fusion");
            ProtoRegistry.RegisterString("CoalFusionDesc", "Carbon is fairly easy to produce by way of fusion, if you have the right equipment.");
            ProtoRegistry.RegisterString("MagnetFusion", "Magnet Fusion");
            ProtoRegistry.RegisterString("MagnetFusionDesc", "Fusing pure iron ingots from deuterium is a long process, due to being the Iron Peak of fusion.");
            ProtoRegistry.RegisterString("SiliconFusion", "Silicon Ingot Fusion");
            ProtoRegistry.RegisterString("SiliconFusionDesc", "Silicon is a medium difficulty fusion product, but is still doable with some time.");
            ProtoRegistry.RegisterString("TitaniumFusion", "Titanium Ingot Fusion");
            ProtoRegistry.RegisterString("TitaniumFusionDesc", "Titanium fusion production is difficult, but less so than iron or copper.");
            ProtoRegistry.RegisterString("StoneFusion", "Stone Fusion");
            ProtoRegistry.RegisterString("StoneFusionDesc", "An amourphous solid, made up of several elements, stone is still a fusable material.");

            ProtoRegistry.RegisterString("OrganicFusion", "Organic Crystal Fusion");
            ProtoRegistry.RegisterString("OrganicFusionDesc", "Fusing Organic Crystal is complicated, due to the crystal structure present.");
            ProtoRegistry.RegisterString("OpticalFusion", "Optical Grating Crystal Fusion");
            ProtoRegistry.RegisterString("OpticalFusionDesc", "Fusing Optical Grating Crystal is possible, but requires care to get the correct surface features.");
            ProtoRegistry.RegisterString("FireFusion", "Fire Ice Fusion");
            ProtoRegistry.RegisterString("FireFusionDesc", "Fire Ice is fairly easy to produce by way of fusion, if you have the right equipment.");
            ProtoRegistry.RegisterString("SpinoFusion", "Spinoform Stalagmite Crystal Fusion");
            ProtoRegistry.RegisterString("SpinoFusionDesc", "Spinoform Stalagmite Crystal is fairly easy to produce by way of fusion, if you have the right equipment.");
            ProtoRegistry.RegisterString("FractalFusion", "Fractal Silicon Fusion");
            ProtoRegistry.RegisterString("FractalFusionDesc", "Fractal Silicon is a medium difficulty fusion product, but is still doable with some time.");
            ProtoRegistry.RegisterString("KimberFusion", "Kimberlite Ore Fusion");
            ProtoRegistry.RegisterString("KimberFusionDesc", "Kimberlite Ore fusion production is difficult, but less so than iron or copper.");
            ProtoRegistry.RegisterString("UnipolarFusion", "Unipolar Magnet Fusion");
            ProtoRegistry.RegisterString("UnipolarFusionDesc", "Unipolar Magnets have special magnetic properties that make them difficult to fuse, but they are still a fusable material.");

            #endregion

         #region Forge
            /*Logger.LogInfo("Making Forge");

            var FusionForgeModel = LDB.items.Select(2210);//Artificial Star Item
            var FusionForgeItem = LDB.items.Select(2310);//Particle Collider Item
            var FusionForgeRecipe = LDB.recipes.Select(39);//Particle Collider recipe
            var preTech = LDB.techs.Select(1144); //Advanced Fusion tech

            ItemProto FusionForgeI = FusionForgeItem.Copy();
            RecipeProto FusionForgeR = FusionForgeRecipe.Copy();

            ItemProto fusionforgeitem = ProtoRegistry.RegisterItem(5000, "FusionForge", "FusionForgeDesc", "Assets/BetterMachines/Icons/smelter-2", ProtoRegistry.GetGridIndex(2, 5, 4), 50, EItemType.Production);
            Logger.LogInfo("Fusion Forge Item");

            RecipeProto fusionforgerecipe = ProtoRegistry.RegisterRecipe(5001, ERecipeType.Assemble, 1200, new[] { 2210, 1205, 1209 }, new[] { 1, 12, 6 }, new[] { fusionforgeitem.ID }, new[] { 1 }, "FusionForgeDesc", 1851, 2212);
            Logger.LogInfo("Fusion Forge recipe");*/
        #endregion

            #region Forge Tech
            Logger.LogInfo("Making Forge tech");
            var ArtStarTech = LDB.techs.Select(1144); //Artificial Star tech
            var GravLensTech = LDB.techs.Select(1704); //Gravity Lens tech

            //RegisterTech(int id, string name, string description, string conclusion, string iconPath, int[] preTechs, int[] jellos, int[] jelloRate, long hashNeeded, int[] unlockRecipes, Vector2 position)

            //ProtoRegistry.RegisterTech(5002, "FusionTech", "FusionTechDesc", "FusionTechConc", string iconPath, new[] { ArtStarTech, GravLensTech }, new[] { 6001, 6002, 6003, 6005 }, new[] { 10, 10, 10, 10 }, 432000, new[] { fusionforgerecipe.ID }, new Vector2(65, -3));
        #endregion

        #region Recipes
        Logger.LogInfo("Making Forging Recipes");
            double BaseTime = 60;
            double PowerDivisor = 10;
            double PowerExponent = 2;
            double RareResWeightPenalty = 1.1;//Rare Resources require more weight and have more mass than their components would suggest.
            var SigDigs = 0;
            double GrapheneWeight = 2.27;
            double GrapheneMass = 12;
            int FusionTech = 1144;

            double HydrogenWeight = 1.41; //Determined by taking the energy output of burning hydrogen(In-game), and dividing by the energy/mole of typical hydrogen combustion. Covert to density, then use the result of that as a multiplier to normal hydrogen density. Also x2 because deuterium is twice as heavy as normal hydrogen.

            double IronWeight = 7.87; //This Element's density. Units don't matter, as long as all elements use the same units. This is used comparitively, to determine graphene and deuterium usage.
            double IronMass = 55; //Mass is essentially the difficulty of fusing this item. I take the atomic mass of the element in question, and put it through an exponential equation to get the time required for fusing.
            int IronGraphene = (int)(Math.Ceiling(IronMass / GrapheneMass * (IronWeight / GrapheneWeight))); //This is how much graphene the recipe requires to fuse. It is determined by dividing the relative weight of a unit of the target material by the relative weight of a unit of graphene
            int IronDeut = (int)(Math.Ceiling(IronMass / GrapheneMass * ((IronWeight % GrapheneWeight) * HydrogenWeight))); //This is how much deuterium the recipe requires to fuse. It is determined by dividing the remainder relative weight of a unit of the target material (After dividing it for the graphene calculation) by the relative weight of a unit of graphene
            int IronTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((IronMass - GrapheneMass) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double CopperWeight = 8.96;
            double CopperMass = 63;
            int CopperGraphene = (int)(Math.Ceiling(CopperMass / GrapheneMass * (CopperWeight / GrapheneWeight)));
            int CopperDeut = (int)(Math.Ceiling(CopperMass / GrapheneMass * ((CopperWeight % GrapheneWeight) * HydrogenWeight)));
            int CopperTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((CopperMass - GrapheneMass) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double GraphiteWeight = 1;
            double GraphiteMass = 15;
            int GraphiteDeut = (int)(Math.Ceiling(GraphiteMass / 2 * ((GraphiteWeight / HydrogenWeight) * HydrogenWeight)));
            int GraphiteTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((GraphiteMass - GrapheneMass) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double SiliconWeight = 2.33;
            double SiliconMass = 28;
            int SiliconGraphene = (int)(Math.Ceiling(SiliconMass / GrapheneMass * (SiliconWeight / GrapheneWeight)));
            int SiliconDeut = (int)(Math.Ceiling(SiliconMass / GrapheneMass * ((SiliconWeight % GrapheneWeight) * HydrogenWeight)));
            int SiliconTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((SiliconMass - GrapheneMass) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double TitaniumWeight = 4.5;
            double TitaniumMass = 47;
            int TitaniumGraphene = (int)(Math.Ceiling(TitaniumMass / GrapheneMass * (TitaniumWeight / GrapheneWeight)));
            int TitaniumDeut = (int)(Math.Ceiling(TitaniumMass / GrapheneMass * ((TitaniumWeight % GrapheneWeight) * HydrogenWeight)));
            int TitaniumTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((TitaniumMass - GrapheneMass) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double StoneWeight = 2.2; //Did some searching, picked this. Sand has a typical density of 1.6, more solid rock goes up to 2.6.
            double StoneMass = 22; //Since stone is largely made up of a variety of fairly light elements, this is good enough.
            int StoneGraphene = (int)(Math.Ceiling(StoneMass / GrapheneMass * (StoneWeight / GrapheneWeight)));
            int StoneDeut = (int)(Math.Ceiling(StoneMass / GrapheneMass * ((StoneWeight % GrapheneWeight) * HydrogenWeight)));
            int StoneTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((StoneMass - GrapheneMass) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double OrganicWeight = (int)(Math.Ceiling(((GraphiteWeight * 5) + 3 + 1) * RareResWeightPenalty)); //Density of rare resources is determined by averaging the density of either the materials used to make it, or the materials it replaces in the shortcut recipe.
            double OrganicMass = (int)(Math.Ceiling(16 * RareResWeightPenalty)); // Rare Resource mass (Which is used in the previously-mentioned exponential equation to get recipe time) is determined by the highest of all the items it is replacing, then given the Weight Penalty.
            int OrganicGraphene = (int)(Math.Ceiling((OrganicMass / GrapheneMass) * (OrganicWeight / GrapheneWeight)));
            int OrganicDeut = (int)(Math.Ceiling((OrganicMass / GrapheneMass) * (OrganicWeight % GrapheneWeight) * HydrogenWeight));
            int OrganicTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((OrganicMass - GrapheneMass) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double OpticalWeight = (int)(Math.Ceiling((OrganicWeight + 3 * TitaniumWeight) / 8));
            double OpticalMass = (int)(Math.Ceiling((Math.Max(OrganicMass, TitaniumMass)) * RareResWeightPenalty));
            int OpticalGraphene = (int)(Math.Ceiling((OpticalMass / GrapheneMass) * (OpticalWeight / GrapheneWeight)));
            int OpticalDeut = (int)(Math.Ceiling((OpticalMass / GrapheneMass) * (OpticalWeight % GrapheneWeight) * HydrogenWeight));
            int OpticalTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((OpticalMass - GrapheneMass) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double FireWeight = (int)(Math.Ceiling((GrapheneWeight * 2 + 1) / 2) * RareResWeightPenalty);
            double FireMass = (int)(Math.Ceiling(((GrapheneMass * 2 + 1) * RareResWeightPenalty) / 2));
            int FireDeut = (int)(Math.Ceiling(FireMass * (FireWeight / HydrogenWeight)));
            int FireTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((FireMass - 1) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double SpinoWeight = (int)(Math.Ceiling((((TitaniumWeight / 2 + GrapheneWeight * 1.5) / 2)/3) * RareResWeightPenalty));
            double SpinoMass = (int)(Math.Ceiling(TitaniumMass * RareResWeightPenalty));
            int SpinoDeut = (int)(Math.Ceiling(SpinoMass * (SpinoWeight / GrapheneWeight) * HydrogenWeight));
            int SpinoTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((SpinoMass - 1) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double FractalWeight = (int)(Math.Ceiling((SiliconWeight / 2) * RareResWeightPenalty));
            double FractalMass = (int)(Math.Ceiling(SiliconMass * RareResWeightPenalty));
            int FractalGraphene = (int)(Math.Ceiling((FractalMass / GrapheneMass) * (FractalWeight / GrapheneWeight)));
            int FractalDeut = (int)(Math.Ceiling((FractalMass / GrapheneMass) * (FractalWeight % GrapheneWeight) * HydrogenWeight));
            int FractalTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((FractalMass - GrapheneMass) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double KimberWeight = (int)(Math.Ceiling(2 * GraphiteWeight) * RareResWeightPenalty);
            double KimberMass = (int)(Math.Ceiling(GraphiteMass * RareResWeightPenalty));
            int KimberDeut = (int)(Math.Ceiling(KimberMass * (KimberWeight / HydrogenWeight)));
            int KimberTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((KimberMass - 1) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            double UnipolarWeight = (int)(Math.Ceiling((((GrapheneWeight * 2 + (12 * IronWeight + 3 * CopperWeight)) / 10) / 17) * RareResWeightPenalty));
            double UnipolarMass = (int)(Math.Ceiling(CopperMass * RareResWeightPenalty));
            int UnipolarGraphene = (int)(Math.Ceiling((UnipolarMass / GrapheneMass) * (UnipolarWeight / GrapheneWeight)));
            int UnipolarDeut = (int)(Math.Ceiling(UnipolarMass * (UnipolarWeight % HydrogenWeight)));
            int UnipolarTime = (int)(Math.Round(BaseTime * Math.Pow(PowerExponent, ((UnipolarMass - GrapheneMass) / PowerDivisor)), SigDigs, MidpointRounding.AwayFromZero));//Ticks. Divide by 60 for seconds.

            var IronIcon = LDB.items.Select(1101);//Iron ingot item
            var CopperIcon = LDB.items.Select(1104);//Copper ingot item
            var GraphiteIcon = LDB.items.Select(1006);//Coal item; Changing it to coal from graphite because Proliferator Mk1 requires coal
            var MagnetIcon = LDB.items.Select(1102);//Magnet item
            var SiliconIcon = LDB.items.Select(1105);//Silicon ingot item
            var TitaniumIcon = LDB.items.Select(1106);//Titanium ingot item
            var StoneIcon = LDB.items.Select(1005); ;//Stone

            var OrganicIcon = LDB.items.Select(1117);//Organic Crystal item
            var OpticalIcon = LDB.items.Select(1014);//Optical Grating Crystal item
            var FireIcon = LDB.items.Select(1011);//Fire Ice item
            var SpinoIcon = LDB.items.Select(1015);//Spinoform Stalagmite Crystal item
            var FractalIcon = LDB.items.Select(1013);//Fractal Silicon item
            var KimberIcon = LDB.items.Select(1012);//Kimberlite Ore item
            var UnipolarIcon = LDB.items.Select(1016); ;//Unipolar Magnet item

            //RecipeProto RegisterRecipe(int id, int type, int time, int[] input, int[] inCounts, int[] output, int[] outCounts, string description, int techID, int gridIndex, string name, string iconPath)

            ProtoRegistry.RegisterRecipe(5003, ERecipeType.Particle, IronTime, new[] { 1121, 1123 }, new[] { IronDeut, IronGraphene }, new[] {1101}, new[] { 1 } , "IronFusionDesc", FusionTech, 2801, "IronFusion", IronIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5004, ERecipeType.Particle, CopperTime, new[] { 1121, 1123 }, new[] { CopperDeut, CopperGraphene }, new[] { 1104 }, new[] { 1 } , "CopperFusionDesc", FusionTech, 2802, "CopperFusion", CopperIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5005, ERecipeType.Particle, GraphiteTime, new[] { 1121 }, new[] { GraphiteDeut }, new[] { 1006 }, new[] { 1 } , "GraphiteFusionDesc", FusionTech, 2803, "GraphiteFusion", GraphiteIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5006, ERecipeType.Particle, IronTime, new[] { 1121, 1123 }, new[] { IronDeut, IronGraphene }, new[] { 1102 }, new[] { 1 } , "MagnetFusionDesc", FusionTech, 2804, "MagnetFusion", MagnetIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5007, ERecipeType.Particle, SiliconTime, new[] { 1121, 1123 }, new[] { SiliconDeut, SiliconGraphene }, new[] { 1105 }, new[] { 1 } , "SiliconFusionDesc", FusionTech, 2805, "SiliconFusion", SiliconIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5008, ERecipeType.Particle, TitaniumTime, new[] { 1121, 1123 }, new[] { TitaniumDeut, TitaniumGraphene }, new[] { 1106 }, new[] { 1 } , "TitaniumFusionDesc", FusionTech, 2806, "TitaniumFusion", TitaniumIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5009, ERecipeType.Particle, StoneTime, new[] { 1121, 1123 }, new[] { StoneDeut, StoneGraphene }, new[] { 1005 }, new[] { 1 } , "StoneFusionDesc", FusionTech, 2807, "StoneFusion", StoneIcon.IconPath);

            ProtoRegistry.RegisterRecipe(5010, ERecipeType.Particle, OrganicTime, new[] { 1121, 1123 }, new[] { OrganicDeut, OrganicGraphene }, new[] { 1117 }, new[] { 1 } , "OrganicFusionDesc", FusionTech, 2808, "OrganicFusion", OrganicIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5011, ERecipeType.Particle, OpticalTime, new[] { 1121, 1123 }, new[] { OpticalDeut, OpticalGraphene }, new[] { 1014 }, new[] { 1 } , "OpticalFusionDesc", FusionTech, 2809, "OpticalFusion", OpticalIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5012, ERecipeType.Particle, FireTime, new[] { 1121 }, new[] { FireDeut }, new[] { 1011 }, new[] { 1 } , "FireFusionDesc", FusionTech, 2810, "FireFusion", FireIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5013, ERecipeType.Particle, SpinoTime, new[] { 1121 }, new[] { SpinoDeut }, new[] { 1015 }, new[] { 1 } , "SpinoFusionDesc", FusionTech, 2811, "SpinoFusion", SpinoIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5014, ERecipeType.Particle, FractalTime, new[] { 1121, 1123 }, new[] { FractalDeut, FractalGraphene }, new[] { 1013 }, new[] { 1 } , "FractalFusionDesc", FusionTech, 2812, "FractalFusion", FractalIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5015, ERecipeType.Particle, KimberTime, new[] { 1121, }, new[] { KimberDeut }, new[] { 1012 }, new[] { 1 } , "KimberFusionDesc", FusionTech, 2813, "KimberFusion", KimberIcon.IconPath);
            ProtoRegistry.RegisterRecipe(5016, ERecipeType.Particle, UnipolarTime, new[] { 1121, 1123 }, new[] { UnipolarDeut, UnipolarGraphene }, new[] { 1016 }, new[] { 1 } , "UnipolarFusionDesc", FusionTech, 2814, "UnipolarFusion", UnipolarIcon.IconPath);



            //Add these recipes to the item, so it shows on them.
            RecipeProto FusionIronR = LDB.recipes.Select(5003);
            RecipeProto FusionCopperR = LDB.recipes.Select(5004);
            RecipeProto FusionMagnetR = LDB.recipes.Select(5006);
            RecipeProto FusionSiliconR = LDB.recipes.Select(5007);
            RecipeProto FusionTitaniumR = LDB.recipes.Select(5008);

            IronIcon.recipes.Add(FusionIronR);
            CopperIcon.recipes.Add(FusionCopperR);
            //GraphiteIcon.recipes.Add(FusionGraphiteR); Since this was changed to coal, it doesn't go on graphite anymore.
            MagnetIcon.recipes.Add(FusionMagnetR);
            SiliconIcon.recipes.Add(FusionSiliconR);
            TitaniumIcon.recipes.Add(FusionTitaniumR);
        #endregion
        }
    }
}
