using Game.Buildings;
using Game.Modding;
using Game.Prefabs;
using Game.Rendering;
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
using static Game.Rendering.Debug.RenderPrefabRenderer;
using static Game.UI.InGame.InfoviewsUISystem;



[HarmonyPatch(typeof(BuildingInfomodePrefab), "Initialize")]
public class BuildingColorPatch
{
    // Patch the Initialize method to modify the color after the original method has been called
    [HarmonyPostfix]
    public static void Initialize_Postfix(EntityManager entityManager, Entity entity, BuildingInfomodePrefab __instance)
    {
        // Fetch the InfoviewBuildingData component from the entity
        if (entityManager.HasComponent<InfoviewBuildingData>(entity))
        {
            var buildingData = entityManager.GetComponentData<InfoviewBuildingData>(entity);

            // Check if the BuildingType is School
            if (buildingData.m_Type == BuildingType.School)
            {
                // Modify the color of the School building
                Mod.log.Info("Changing color of School building to blue.");

                // Set the color to blue (RGBA(0.000, 0.000, 1.000, 1.000))
                __instance.m_Color = new UnityEngine.Color(0.000f, 0.000f, 1.000f, 1.000f);
            }

        }
    }
}

















/*[HarmonyPatch(typeof(Game.Prefabs.BuildingInfomodePrefab), "Initialize", typeof(EntityManager), typeof(Entity))]
public static class BuildingInfomodePrefabPatch
{


    [HarmonyPrefix]
    public static bool InitializePrefix(
        Game.Prefabs.BuildingInfomodePrefab __instance,
        EntityManager entityManager,
        Entity entity)
    {
       // Mod.log.Info($"Yay it ran! Prefab: {__instance.name}, Type: {__instance.m_Type}");

        // Ensure componentData is properly initialized
        InfoviewBuildingData componentData = default;
        componentData.m_Type = __instance.m_Type;

        // Set the modified component data
        entityManager.SetComponentData(entity, componentData);
        Mod.log.Info($"Component data updated for entity: {entity}");

        return false;
    }


}*/

/*[HarmonyPatch(typeof(Game.Prefabs.BuildingInfomodePrefab), "Initialize")]
public static class BuildingInfomodePrefabTracker
{
    public static List<BuildingInfomodePrefab> trackedPrefabs = new List<BuildingInfomodePrefab>();

    [HarmonyPostfix]
    static void InitializePostfix(Game.Prefabs.BuildingInfomodePrefab __instance)
    {

        *//*InfoviewBuildingData componentData = default;
        componentData.m_Type = __instance.m_Type;

      
        // Set the modified component data
        entityManager.SetComponentData(entity, componentData);*/


/*if (__instance == null)
{
    Mod.log.Warn("Attempted to track a null BuildingInfomodePrefab.");
    return;
}*//*


if (__instance.m_Type == BuildingType.School)
{
    Mod.log.Info($"Building is a School: {__instance.name}");

    UpdateColors(Color.green);

}
else
{
    Mod.log.Info($"Building {__instance.m_Type}  is a: {__instance.name} COLOR NOT UPDATED");
}


if (!trackedPrefabs.Contains(__instance))
{
    trackedPrefabs.Add(__instance);
    Mod.log.Info($"Tracking new BuildingInfomodePrefab: {__instance.name}");
}
else
{
    Mod.log.Info($"BuildingInfomodePrefab already tracked: {__instance.name}");
}
}

public static void UpdateColors(Color color)
{
foreach (var prefab in trackedPrefabs)
{
    //Mod.log.Info($"Current prefab is {prefab}");

        prefab.m_Color = color;
        Mod.log.Info($"Updated {prefab.name} color to {color}");



}
}
}
*/

/*[HarmonyPatch(typeof(ToolSystem), "UpdateInfoviewColors")] 
public static class InfoviewUpdatePatch
{
    static WhitenessSystem _whitenesSystem;


    private static HashSet<string> observedNames = new HashSet<string>();




    static void Postfix(ToolSystem __instance)
    {
        // Get the WhitenessSystem instance


        if (_whitenesSystem == null)
        {
            _whitenesSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<WhitenessSystem>();
        }

       //Mod.log.Info($"Active Info view is: {__instance.activeInfoview.name}");


        *//*if (__instance.activeInfoview.active == true)
        {
            
            BuildingInfomodePrefabTracker.UpdateColors(Color.yellow);

        }*//*

        if (__instance.activeInfoview?.name == "Education" && __instance.activeInfoview?.active == true)
        {
            Mod.log.Info("Updating color of Education");
            //BuildingInfomodePrefabTracker.UpdateColors(Color.red);
            Mod.log.Info($"Active Info view is: {__instance.activeInfoview.name}");

        }
        else
        {
            Mod.log.Info("Its Not Education");
        }


        if (__instance.activeInfoview?.active == true && Mod.m_Setting.ToggleWhiteness == true)
        {
           
            // Update the shader when the Infoview is updated
            Shader.SetGlobalInt("colossal_InfoviewOn", _whitenesSystem.whitenessToggle ? 1 : 0);
      
           
            Mod.log.Info("UPDATED FROM HARMONEY PATCH");
        }

        if (__instance.activeInfoview?.active == true && Mod.m_Setting.ToggleOverlay == false)
        {
            // Update the shader when the Infoview is updated
            __instance.activeInfoview.m_DefaultColor = Color.grey;
            
           

            Mod.log.Info("UPDATED FROM HARMONEY PATCH");
        } 
        *//*else if (__instance.activeInfoview?.active == true && Mod.m_Setting.EnumDropdown == Whiteness_Toggle.Setting.SomeEnum.Off)
        {
            __instance.activeInfoview.m_DefaultColor = Color.grey;


            Mod.log.Info("UPDATED FROM HARMONEY PATCH");
        }*//*
        else if (__instance.activeInfoview?.active == true && Mod.m_Setting.ToggleOverlay == true)


        {
            Vector4 myColor = new Vector4(Mod.m_Setting.Red / 255f, Mod.m_Setting.Green / 255f, Mod.m_Setting.Blue / 255f, 0.1f);  // RGBA values normalized


            if (Mod.m_Setting.m_Protanopia)
            {
                Mod.m_Setting.Red = 255;
                Mod.m_Setting.Green = 128;
                Mod.m_Setting.Blue = 0;

                myColor = new Vector4(Mod.m_Setting.Red / 255f, Mod.m_Setting.Green / 255f, Mod.m_Setting.Blue / 255f, 0.1f);
            }
            else if (Mod.m_Setting.m_Deuteranopia)
            {
                Mod.m_Setting.Red = 255;
                Mod.m_Setting.Green = 129;
                Mod.m_Setting.Blue = 0;

                myColor = new Vector4(Mod.m_Setting.Red / 255f, Mod.m_Setting.Green / 255f, Mod.m_Setting.Blue / 255f, 0.1f);
            }
            else if (Mod.m_Setting.m_Tritanopia)
            {
                Mod.m_Setting.Red = 129;
                Mod.m_Setting.Green = 64;
                Mod.m_Setting.Blue = 64;

                myColor = new Vector4(Mod.m_Setting.Red / 255f, Mod.m_Setting.Green / 255f, Mod.m_Setting.Blue / 255f, 0.1f);
            }
            else if (Mod.m_Setting.m_lightBlue)
            {
                Mod.m_Setting.Red = 5;
                Mod.m_Setting.Green = 5;
                Mod.m_Setting.Blue = 15;

                myColor = new Vector4(Mod.m_Setting.Red / 255f, Mod.m_Setting.Green / 255f, Mod.m_Setting.Blue / 255f, 0.1f);
            }
            else if (Mod.m_Setting.m_lightGreen)
            {
                Mod.m_Setting.Red = 5;
                Mod.m_Setting.Green = 15;
                Mod.m_Setting.Blue = 5;

                myColor = new Vector4(Mod.m_Setting.Red / 255f, Mod.m_Setting.Green / 255f, Mod.m_Setting.Blue / 255f, 0.1f);
            }
            else if (Mod.m_Setting.m_lightRed)
            {
                Mod.m_Setting.Red = 15;
                Mod.m_Setting.Green = 5;
                Mod.m_Setting.Blue = 5;

                myColor = new Vector4(Mod.m_Setting.Red / 255f, Mod.m_Setting.Green / 255f, Mod.m_Setting.Blue / 255f, 0.1f);
            }
            else if (Mod.m_Setting.m_lightYellow)
            {
                Mod.m_Setting.Red = 19;
                Mod.m_Setting.Green = 16;
                Mod.m_Setting.Blue = 0;

                myColor = new Vector4(Mod.m_Setting.Red / 255f, Mod.m_Setting.Green / 255f, Mod.m_Setting.Blue / 255f, 0.1f);
            }
            else if (Mod.m_Setting.m_lightPink)
            {
                Mod.m_Setting.Red = 6;
                Mod.m_Setting.Green = 0;
                Mod.m_Setting.Blue = 8;

                myColor = new Vector4(Mod.m_Setting.Red / 255f, Mod.m_Setting.Green / 255f, Mod.m_Setting.Blue / 255f, 0.1f);
            }
            else
            {
                // Default color (no colorblindness adjustment)
                myColor = new Vector4(Mod.m_Setting.Red / 255f, Mod.m_Setting.Green / 255f, Mod.m_Setting.Blue / 255f, 0.1f);
               

            }
            

            Vector4[] colorArray = new Vector4[10];
            for (int i = 0; i < colorArray.Length; i++)
            {
                colorArray[i] = myColor;
            }

            Shader.SetGlobalVectorArray("colossal_InfomodeColors", colorArray);
           
            

            Mod.log.Info("UPDATED FROM HARMONEY PATCH - COLOURS");


        }


    }

  
}*/