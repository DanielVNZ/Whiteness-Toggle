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
        if (__instance.activeInfoview?.active == true && Mod.m_Setting.ToggleOverlay == false)
        {
            // Update the shader when the Infoview is updated
            __instance.activeInfoview.m_DefaultColor = Color.grey;
           

            Mod.log.Info("UPDATED FROM HARMONEY PATCH");
        } 
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

  
}