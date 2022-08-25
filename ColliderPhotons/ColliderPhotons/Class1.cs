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

namespace ColliderPhotons
{
    [BepInPlugin("mod.jamesfire.ColliderPhotons", "ColliderPhotons Plug-In", "1.0.0.0")]
    [BepInDependency("me.xiaoye97.plugin.Dyson.LDBTool", BepInDependency.DependencyFlags.HardDependency)]
    [BepInProcess("DSPGAME.exe")]
    public class ColliderPhotons : BaseUnityPlugin
    {
        void Awake()
        {
            Logger.LogInfo("ColliderPhotons initializing 1.0");
            LDBTool.PreAddDataAction += addLang;
            LDBTool.PreAddDataAction += GenColliderPhotons;
        }
        void addLang()
        {
            StringProto name = new StringProto();
            StringProto desc = new StringProto();
            name.ID = 5019;
            name.Name = "Critical Photon";
            name.name = "Critical Photon";
            name.ZHCN = "Critical Photon";
            name.ENUS = "Critical Photon";
            name.FRFR = "Critical Photon";

            desc.ID = 5020;
            desc.Name = "Critical Photons can be made in our Particle Colliders, it just takes a while.";
            desc.name = "Critical Photons can be made in our Particle Colliders, it just takes a while.";
            desc.ZHCN = "Critical Photons can be made in our Particle Colliders, it just takes a while.";
            desc.ENUS = "Critical Photons can be made in our Particle Colliders, it just takes a while.";
            desc.FRFR = "Critical Photons can be made in our Particle Colliders, it just takes a while.";

            LDBTool.PreAddProto(ProtoType.String, name);
            LDBTool.PreAddProto(ProtoType.String, desc);
            Logger.LogInfo("ColliderPhotons preadd");


        }
        void GenColliderPhotons()
        {
            var oriColphotons = LDB.recipes.Select(40);//Deuterium in Collider
            RecipeProto newColphotons = oriColphotons.Copy();
            Logger.LogInfo("ColliderPhotons variables");
            var preTech = LDB.techs.Select(1506); //Dirac Inversion

            var PhotonIcon = LDB.items.Select(1208);//Iron ingot item
            Traverse.Create(newColphotons).Field("_iconSprite").SetValue(PhotonIcon.iconSprite);
            newColphotons.ID = 5000;
            newColphotons.Name = "Collider Photon";
            newColphotons.name = "Collider Photon";
            newColphotons.Description = "Critical Photons can be made in our Particle Colliders, it just takes a while.";
            newColphotons.description = "Critical Photons can be made in our Particle Colliders, it just takes a while.";
            newColphotons.Items = new int[] { 1120 }; //Hydrogen
            newColphotons.Results = new int[] { 1208 }; //Critical Photon
            newColphotons.ItemCounts = new int[] { 2 };
            newColphotons.ResultCounts = new int[] { 1 };
            newColphotons.TimeSpend = 1200;//Ticks. Divide by 60 for seconds.
            newColphotons.preTech = preTech;
            newColphotons.Explicit = true;
            newColphotons.GridIndex = 1708;
            newColphotons.SID = newColphotons.GridIndex.ToString();
            newColphotons.sid = newColphotons.GridIndex.ToString();
            Logger.LogInfo("ColliderPhotons recipe created");

            LDBTool.PostAddProto(ProtoType.Recipe, newColphotons);
            Logger.LogInfo("ColliderPhotons recipe added");
        }

        void GenRegistryPhotons()
        { 
            Registry.registerString("colliderphotons", "Critical Photon");
            Registry.registerString("colliderphotonsdesc",
                "Critical Photons can be made in our Particle Colliders, it just takes a while.");
            Registry.registerRecipe(5000, ERecipeType.Assemble, 60, new[] { 1120 }, new[] { 2 }, new[] { 1208 }, new[] { 1 }, "colliderphotonsdesc", 1506);
        }
        //Registry.registerRecipe(RecipeID, RecipeType, Time, Ingredients(Table), Ingredient Amounts(Table), Results(Table), Result Amounts(Table), String Description, Tech ID);
        //Registry.registerItem(ItemID, ItemName, Desc, Icon, GridIndex);
        //Registry.registerString(StringName, StringContents);
    }
}
