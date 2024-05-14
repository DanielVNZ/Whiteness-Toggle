using Game.Settings;
using Game.Tools;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;
using Whiteness_Toggle;

[HarmonyPatch(typeof(ToolSystem), "UpdateInfoviewColors")]
public static class InfoviewUpdatePatch
{
    static WhitenessSystem _whitenesSystem;


   

    static void Postfix(ToolSystem __instance)
    {
        // Get the WhitenessSystem instance
       
        if (_whitenesSystem == null)
        {
            _whitenesSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<WhitenessSystem>();
        }

        if (__instance.activeInfoview?.active == true && Mod.m_Setting.ToggleWhiteness == true)
        {
            // Update the shader when the Infoview is updated
            Shader.SetGlobalInt("colossal_InfoviewOn", _whitenesSystem.whitenessToggle ? 1 : 0);
            Mod.log.Info("UPDATED FROM HARMONEY PATCH");
        }
    }
}