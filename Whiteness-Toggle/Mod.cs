﻿using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
using Colossal.Serialization.Entities;
using Game.Rendering;
using Game.Simulation;
using Game.UI;
using System.Collections.Generic;
using System.IO;
using Game.Tools;
using Game.Prefabs;
using HarmonyLib;
using static Game.UI.InGame.InfoviewsUISystem;
using Colossal.IO.AssetDatabase;
using Game.Input;
using Unity.Entities;
using Unity.Collections;
using Game.Common;
using Game.UI.InGame;
using System.Linq;







namespace Whiteness_Toggle
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(Whiteness_Toggle)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        public static Setting m_Setting;
        private static Harmony _harmony;
        public WhitenessSystem _System;
        private ProxyAction m_ButtonAction;


       



        public const string kButtonActionName = "ButtonBinding";



        public void OnLoad(UpdateSystem updateSystem)
        {

            _harmony = new Harmony("com.daniel.whitenessstoggle.whitenesstoggle");
            Harmony.DEBUG = true;
            _harmony.PatchAll();
            log.Info(nameof(OnLoad));


            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            if (_System == null)
            {
                _System = new WhitenessSystem(this);
            }

            m_Setting = new Setting(this, _System);
            m_Setting.RegisterInOptionsUI();
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));
            m_Setting.RegisterKeyBindings();

            m_ButtonAction = m_Setting.GetAction(kButtonActionName);
            m_ButtonAction.onInteraction += OnButtonActionInteraction;



            m_ButtonAction.shouldBeEnabled = true;


            m_ButtonAction.onInteraction += (_, phase) => log.Info($"[{m_ButtonAction.name}] On{phase} {m_ButtonAction.ReadValue<float>()}");





           








            AssetDatabase.global.LoadSettings(nameof(Whiteness_Toggle), m_Setting, new Setting(this, _System));

            updateSystem.UpdateAt<WhitenessSystem>(SystemUpdatePhase.Modification1);

            updateSystem.UpdateAt<WhitenessSystem>(SystemUpdatePhase.MainLoop);

            updateSystem.UpdateAt<WhitenessSystem>(SystemUpdatePhase.Rendering);

            updateSystem.UpdateAt<MySystem>(SystemUpdatePhase.PrefabUpdate);

            updateSystem.UpdateAt<MySystem>(SystemUpdatePhase.MainLoop);

            updateSystem.UpdateAt<MySystem>(SystemUpdatePhase.Rendering);



        }


        

        private void OnButtonActionInteraction(ProxyAction action, InputActionPhase phase)
        {
            if (phase == InputActionPhase.Performed)
            {
                Mod.m_Setting.ToggleWhiteness = !Mod.m_Setting.ToggleWhiteness;
                Mod.m_Setting.Apply();
            }
        }

        public void OnDispose()
        {
            _harmony?.UnpatchAll("com.daniel.whitenessstoggle.whitenesstoggle");
            log.Info(nameof(OnDispose));
            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
               
            }
        }

       


    }




        public partial class MySystem : UISystemBase
    {

        private EntityQuery m_Query;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_Query = GetEntityQuery(
               ComponentType.ReadWrite<Game.Prefabs.AreaColorData>(),
               ComponentType.Exclude<Temp>(),
               ComponentType.Exclude<Deleted>());


            RequireForUpdate(m_Query);

        }
        protected override void OnUpdate()
        {
            NativeArray<Game.Prefabs.AreaColorData> areacolordataArray
                 = m_Query.ToComponentDataArray<Game.Prefabs.AreaColorData>(Allocator.Temp);

            for (int i = 0; i < areacolordataArray.Length; i++)
            {
                var areacolorRecord = areacolordataArray[i];

                areacolorRecord.m_SelectionEdgeColor = Color.red;
                areacolorRecord.m_SelectionFillColor = Color.yellow;

                areacolordataArray[i] = areacolorRecord;

                //Mod.log.Info($"Found Record: {areacolordataArray[i]}");

            }

            m_Query.CopyFromComponentDataArray(areacolordataArray);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }











        /*private EntityQuery m_Query;
        protected override void OnCreate()
        {
            base.OnCreate();

            m_Query = GetEntityQuery(
                ComponentType.ReadWrite<Game.Prefabs.AreaColorData>(),
                ComponentType.Exclude<Temp>(),
                ComponentType.Exclude<Deleted>());

            RequireForUpdate(m_Query);
        }

        protected override void OnUpdate()
        {
           NativeArray<Game.Prefabs.AreaColorData> areacolordataArray
                = m_Query.ToComponentDataArray<Game.Prefabs.AreaColorData>(Allocator.Temp);

            for (int i = 0; i < areacolordataArray.Length; i++)
            {
                var areacolorRecord = areacolordataArray[i];

                areacolorRecord.m_SelectionEdgeColor = Color.red;
                areacolorRecord.m_SelectionFillColor = Color.yellow;

                areacolordataArray[i] = areacolorRecord;

                //Mod.log.Info($"Found Record: {areacolordataArray[i]}");

            }

            m_Query.CopyFromComponentDataArray(areacolordataArray);
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            if (!mode.IsGameOrEditor())
                return;
        }*/

    }


    public partial class WhitenessSystem : GameSystemBase
    {

        public Game.Tools.ToolSystem _toolSystem;


        public Mod _mod;
        public bool whitenessToggle;
        public bool isPressed;
        private ProxyAction action;
        //public bool whiteness = Mod.m_Setting.ToggleWhiteness;



        public WhitenessSystem()
        {
            

            
        }

        public WhitenessSystem(Mod mod)
        {
            _mod = mod;
        }


        protected override void OnCreate()
        {
            base.OnCreate();
            isPressed = Mod.m_Setting.ToggleWhiteness;
            action = Setting.instance.GetAction("kButtonActionName");




            SetupKeybinds();
        }

       

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            if (!mode.IsGameOrEditor())
                return;

            if (_toolSystem == null)
            {
                _toolSystem = World.GetExistingSystemManaged<Game.Tools.ToolSystem>();

            }
            else
            {
                Mod.log.Info("TOOL SYSTEM IS NULL");
            }

          



        }



        public void SetupKeybinds()
        {
            



        }


        protected override void OnUpdate()
        {


        }
        
        



    }

    
}




