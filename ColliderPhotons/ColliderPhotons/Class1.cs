using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BepInEx;
using xiaoye97;
using HarmonyLib;
using kremnev8;
using CommonAPI;
using CommonAPI.Systems;
using CommonAPI.Systems.ModLocalization;

namespace ColliderPhotons
{
    [BepInPlugin("mod.jamesfire.ColliderPhotons", "ColliderPhotons Plug-In", "1.0.0.0")]
    [BepInDependency("me.xiaoye97.plugin.Dyson.LDBTool", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(CommonAPIPlugin.GUID)]
    [CommonAPISubmoduleDependency(nameof(ProtoRegistry), nameof(CustomDescSystem), nameof(UtilSystem))]
    [CommonAPISubmoduleDependency(nameof(LocalizationModule), nameof(CustomDescSystem), nameof(UtilSystem))]
    [BepInProcess("DSPGAME.exe")]
    public class ColliderPhotons : BaseUnityPlugin
    {
        void Awake()
        {
            Logger.LogInfo("ColliderPhotons initializing 1.0");
            LDBTool.PreAddDataAction += addLang;
            LDBTool.PreAddDataAction += GenRegistryPhotons;
        }
        void addLang()
        {
            LocalizationModule.RegisterTranslation("colliderphotons", "Critical Photon");
            LocalizationModule.RegisterTranslation("colliderphotonsdesc", "Critical Photons can be made in our Particle Colliders, it just takes a while.");
            Logger.LogInfo("ColliderPhotons preadd");
        }

        void GenRegistryPhotons() //Common API implementation
        {
            ProtoRegistry.RegisterRecipe(5000, ERecipeType.Particle, 99, new[] { 1120 }, new[] { 2 }, new[] { 1208 }, new[] { 1 }, "colliderphotonsdesc", 1506, 1808);
            //var PhotonItem = LDB.items.Select(1208);
            //RecipeProto PhotonRecipe = LDB.recipes.Select(5000);
            //PhotonItem.recipes.Add(PhotonRecipe);
        }
        //Registry.registerRecipe(RecipeID, RecipeType, Time, Ingredients(Table), Ingredient Amounts(Table), Results(Table), Result Amounts(Table), String Description, Tech ID);
        //Registry.registerItem(ItemID, ItemName, Desc, Icon, GridIndex);
        //Registry.registerString(StringName, StringContents);
    }
}
