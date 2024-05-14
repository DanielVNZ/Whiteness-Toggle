using Colossal.Logging;
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





namespace Whiteness_Toggle
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(Whiteness_Toggle)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        private static Harmony _harmony;

        
        public void OnLoad(UpdateSystem updateSystem)
        {

            _harmony = new Harmony("com.daniel.whitenessstoggle.whitenesstoggle");
            _harmony.PatchAll();
            log.Info(nameof(OnLoad));


            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            //updateSystem.UpdateAt<WhitenessSystem>(SystemUpdatePhase.MainLoop);
            updateSystem.UpdateAt<WhitenessSystem>(SystemUpdatePhase.Rendering);



        }

        public void OnDispose()
        {
            _harmony?.UnpatchAll("com.daniel.whitenessstoggle.whitenesstoggle");
            log.Info(nameof(OnDispose));
        }

       


    }

    public partial class WhitenessSystem : GameSystemBase
    {

        public Game.Tools.ToolSystem _toolSystem;


        protected override void OnCreate()
        {
            base.OnCreate();

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

        private void SetupKeybinds()
        {
            var inputAction = new InputAction("_WhitenessToggle");
            inputAction.AddCompositeBinding("ButtonWithOneModifier")
                .With("Modifier", "<Keyboard>/shift")
                .With("Button", "<Keyboard>/w");
            inputAction.Enable();
            inputAction.performed += OnShiftWPressed;
            Mod.log.Info("Keybinds set up correctly.");

        }

       











        public bool isPressed = false;
        public bool whitenessToggle;


        private void OnShiftWPressed(InputAction.CallbackContext context)
        {
            isPressed = !isPressed; // Toggle the isPressed boolean
            Mod.log.Info($"Whiteness set to {isPressed}"); // Log the updated value


            TriggerUpdate("UseStickyWhiteness");

            
            Mod.log.Info("Keybinds (Shift + W) pressed.");

        }
        public InfoviewPrefab lastActiveInfoView;

        public void TriggerUpdate(string property)
        {
            switch (property)
            {
                case "UseStickyWhiteness":
                    if (isPressed)
                        Shader.SetGlobalInt("colossal_InfoviewOn", _toolSystem.activeInfoview?.active == true &&
                            whitenessToggle ? 1 : 0);
                    Mod.log.Info("Toggled Whiteness");

                    break;

            }
        }



      













       //public bool hasTriggered = false;

        protected override void OnUpdate()
        {

           /* if (_toolSystem != null && _toolSystem.activeInfoview != lastActiveInfoView)
            {
                lastActiveInfoView = _toolSystem.activeInfoview; // Update the last active info view
                hasTriggered = false; // Reset the trigger flag when the info view changes
            }

            if (_toolSystem != null && _toolSystem.activeInfoview?.active == true && !hasTriggered)
            {
                TriggerUpdate("UseStickyWhiteness");
                hasTriggered = true; // Set the flag after executing once
            }*/
        }
        
        



    }

    
}




