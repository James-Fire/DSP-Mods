using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BepInEx;
using xiaoye97;
using HarmonyLib;

namespace FusionStars
{
    [BepInPlugin("mod.jamesfire.FusionStars", "FusionStars Plug-In", "1.0.0.0")]
    [BepInDependency("me.xiaoye97.plugin.Dyson.LDBTool", BepInDependency.DependencyFlags.HardDependency)]
    [BepInProcess("DSPGAME.exe")]
    public class DeutStar : BaseUnityPlugin
    {
        void Awake()
		{
			Logger.LogInfo("FusionStars Initialization 1.0");
			LDBTool.EditDataAction = (Action<Proto>)Delegate.Combine(LDBTool.EditDataAction, new Action<Proto>(this.EditFuelItems));
			LDBTool.EditDataAction = (Action<Proto>)Delegate.Combine(LDBTool.EditDataAction, new Action<Proto>(this.EditStar));
		}
		void EditFuelItems(Proto p)
		{
			if (p is ItemProto && (p.ID == 1802))
			{
				ItemProto itemProto = p as ItemProto;
				itemProto.FuelType = 6;
				//itemProto.HeatValue = 60000000L;
				Logger.LogInfo("FusionStars Deuterium edited");
			}
			if (p is ItemProto && (p.ID == 1803))
			{
				ItemProto itemProto = p as ItemProto;
				itemProto.HeatValue = 2500000000L;
				Logger.LogInfo("FusionStars Antimatter edited");
			}
		}
		void EditStar(Proto p)
		{
			if (p is ItemProto && (p.ID == 2210))
            {
				ItemProto itemProto = LDB.items.Select(2210);
				PrefabDesc prefabDesc = itemProto.prefabDesc;
				itemProto.prefabDesc.fuelMask = 6;
				itemProto.prefabDesc.useFuelPerTick = 250000L;
				ItemProto.fuelNeeds[6] = new int[] { 1802, 1803 };
				Logger.LogInfo("FusionStars Star edited");
			}
        }
	}
}
