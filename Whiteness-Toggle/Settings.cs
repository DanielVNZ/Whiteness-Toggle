using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Input;
using Game.Modding;
using Game.Settings;
using Game.UI;
using Game.UI.Widgets;
using System.Collections.Generic;
using Whiteness_Toggle;


namespace Whiteness_Toggle
{
    [FileLocation(nameof(Whiteness_Toggle))]
    [SettingsUIGroupOrder(kToggleGroup, kCustomColours, kButtonGroup, kButtonGroup2)]
    [SettingsUIShowGroupName(kToggleGroup, kCustomColours, kButtonGroup, kButtonGroup2)]
    [SettingsUIKeyboardAction(Mod.kButtonActionName, ActionType.Button, usages: new string[] { Usages.kMenuUsage, "TestUsage" }, interactions: new string[] { "UIButton" })]
    [SettingsUIGamepadAction(Mod.kButtonActionName, ActionType.Button, usages: new string[] { Usages.kMenuUsage, "TestUsage" }, interactions: new string[] { "UIButton" })]
    [SettingsUIMouseAction(Mod.kButtonActionName, ActionType.Button, usages: new string[] { Usages.kMenuUsage, "TestUsage" }, interactions: new string[] { "UIButton" })]
    public class Setting : ModSetting
    {
        internal static ModSetting instance { get; private set; }
        private WhitenessSystem _system;
        private Mod _mod;
        private bool currentToggle;

        public const string kSection = "Main";
        public const string kSection2 = "Secondary";
        public const string kSection3 = "Presets";
        public const string kToggleGroup = "Toggle";
        public const string kCustomColours = "Custom Colours";
        public const string kButtonGroup = "Button";
        public const string kButtonGroup2 = "Button1";
        public const string kPresets = "Button2";
        public const string kKeybindingGroup = "KeyBinding";
        public string ColorBlindnessType;
        public bool m_Protanopia;
        public bool m_Deuteranopia;
        public bool m_Tritanopia;
        public bool m_TurnOff;
        public bool m_lightBlue;
        public bool m_lightGreen;
        public bool m_lightRed;
        public bool m_lightPink;
        public bool m_lightYellow;
        public bool togglewhitenesstesting;

        


        public Setting(IMod mod, WhitenessSystem system) : base(mod)
        {
            _mod = (Mod)mod;
            _system = system;
            instance = this;

            
        }
        public override void SetDefaults()
        {
            ToggleWhiteness = false;
            currentToggle = false;
            //TurnOff = true;
            m_Protanopia = false;
            m_Deuteranopia = false;
            m_Tritanopia = false;
            m_TurnOff = true;
            m_lightBlue = false;
            m_lightGreen = false;
            m_lightRed = false;
        }


        [SettingsUIKeyboardBinding(BindingKeyboard.E, Mod.kButtonActionName, shift: true)]
        [SettingsUISection(kSection, kKeybindingGroup)]
        public ProxyBinding KeyboardBinding { get; set; }

        [SettingsUISection(kSection, kKeybindingGroup)]
        public bool ResetBindings
        {
            set
            {
                Mod.log.Info("Reset key bindings");
                ResetKeyBindings();
            }
        }

        

        [SettingsUISection(kSection, kToggleGroup)]
        public bool ToggleWhiteness { get; set; } 


        [SettingsUISection(kSection, kToggleGroup)]
        public bool ToggleOverlay { get; set; }

        

        [SettingsUISection(kSection, kCustomColours)]
        public bool EnableCustomColours { set { m_TurnOff = true; m_Deuteranopia = false; m_Tritanopia = false; m_Protanopia = false; m_lightBlue = false; m_lightGreen = false; m_lightRed = false; ToggleOverlay = true; ToggleWhiteness = false; } }


        [SettingsUISlider(min = 0, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kCustomColours)]
        public int Red { get; set; }

        [SettingsUISlider(min = 0, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kCustomColours)]
        public int Green { get; set; }

        [SettingsUISlider(min = 0, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kCustomColours)]
        public int Blue { get; set; }

        [SettingsUISlider(min = 0, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kFloatTwoFractions)]
        [SettingsUISection(kSection, kCustomColours)]
        public int Alpha { get; set; }




        [SettingsUISection(kSection2, kButtonGroup)]
        public SomeEnum EnumDropdown { get; set; } = SomeEnum.Off;


        public enum SomeEnum
        {
            Off,
            Protanopia,
            Deuteranopia,
            Tritanopia,
        }



        [SettingsUISection(kSection3, kButtonGroup2)]
        public bool LightBlue { set { m_Protanopia = false; m_Deuteranopia = false; m_Tritanopia = false; m_TurnOff = false; m_lightBlue = true; m_lightGreen = false; m_lightRed = false; } }

        [SettingsUISection(kSection3, kButtonGroup2)]
        public bool LightGreen { set { m_Protanopia = false; m_Deuteranopia = false; m_Tritanopia = false; m_TurnOff = false; m_lightBlue = false; m_lightGreen = true; m_lightRed = false; } }

        [SettingsUISection(kSection3, kButtonGroup2)]
        public bool LightRed { set { m_Protanopia = false; m_Deuteranopia = false; m_Tritanopia = false; m_TurnOff = false; m_lightBlue = false; m_lightGreen = false; m_lightRed = true; } }

        [SettingsUISection(kSection3, kButtonGroup2)]
        public bool LightYellow { set { m_Protanopia = false; m_Deuteranopia = false; m_Tritanopia = false; m_TurnOff = false; m_lightBlue = false; m_lightGreen = false; m_lightRed = false; m_lightPink = false; m_lightYellow = true; } }

        [SettingsUISection(kSection3, kButtonGroup2)]
        public bool LightPink { set { m_Protanopia = false; m_Deuteranopia = false; m_Tritanopia = false; m_TurnOff = false; m_lightBlue = false; m_lightGreen = false; m_lightRed = false; m_lightYellow = false; m_lightPink = true; } }

        public override void Apply()
        {
            if (ToggleWhiteness == true)
            { 
                ToggleOverlay = false; 
            }

            if (EnumDropdown == SomeEnum.Protanopia)
            {
                m_Protanopia = true; m_Deuteranopia = false; m_Tritanopia = false; m_TurnOff = false; m_lightBlue = false; m_lightGreen = false; m_lightRed = false; ToggleWhiteness = false; ToggleOverlay = true;
            }
            else if (EnumDropdown == SomeEnum.Deuteranopia)
            {
                m_Deuteranopia = true; m_Protanopia = false; m_Tritanopia = false; m_TurnOff = false; m_lightBlue = false; m_lightGreen = false; m_lightRed = false; ToggleWhiteness = false; ToggleOverlay = true;
            }
            else if (EnumDropdown == SomeEnum.Tritanopia)
            {
                m_Tritanopia = true; m_Deuteranopia = false; m_Protanopia = false; m_TurnOff = false; m_lightBlue = false; m_lightGreen = false; m_lightRed = false; ToggleWhiteness = false; ToggleOverlay = true;
            }
            else if (EnumDropdown == SomeEnum.Off)
            {
                m_TurnOff = true; m_Deuteranopia = false; m_Tritanopia = false; m_Protanopia = false; m_lightBlue = false; m_lightGreen = false; m_lightRed = false;
            }

            base.Apply();
        }



    }

    public class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;
        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Whiteness Toggle" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection), "Main Options" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection2), "Colour Blindness Presets" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection3), "Normal Presets" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kToggleGroup), "Options" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kCustomColours), "Custom Colours" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kButtonGroup), "Colour Blind Presets" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kButtonGroup2), "Normal Presets" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kKeybindingGroup), "Key bindings" },


                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableCustomColours)), "Enable Custom Colours" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableCustomColours)), $"Sets mode to enable custom colours" },


                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Red)), "Red" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Red)), $"When the overlay is turned on, this changes the red value" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Green)), "Green" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Green)), $"When the overlay is turned on, this changes the green value" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Blue)), "Blue" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Blue)), $"When the overlay is turned on, this changes the blue value" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Alpha)), "Alpha (Unavailable)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Alpha)), $"Currently Unavailable - When the overlay is turned on, this changes the opacity" },
                
                
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ToggleWhiteness)), "Tick to turn of Whiteness" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ToggleWhiteness)), $"Use this to enable/disable Whiteness, also works as a keybind, can be changed below. NOTE: if you have an Info View open, must close/open to take effect." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ToggleOverlay)), "Use Custom Overlay" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ToggleOverlay)), $"Use this to enable a custom Overlay using the sliders below or the colour blind settings." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.KeyboardBinding)), "Toggle Whiteness" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.KeyboardBinding)), $"Keybind to toggle whiteness. NOTE: if you have an Info View open, must close/open to take effect." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetBindings)), "Reset key bindings" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetBindings)), $"Reset all key bindings" },


                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LightBlue)), "Light Blue" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LightBlue)), $"Sets mode to Light Blue." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LightGreen)), "Light Green" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LightGreen)), $"Sets mode to Light Green." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LightRed)), "Light Red" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LightRed)), $"Sets mode to Light Red." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LightYellow)), "Light Yellow" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LightYellow)), $"Sets mode to Light Yellow." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LightPink)), "Light Pink" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LightPink)), $"Sets mode to Light Pink." },

                { m_Setting.GetBindingKeyLocaleID(Mod.kButtonActionName), "Button key" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnumDropdown)), "Select a Colour Blind Option" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnumDropdown)), $"Select a Colour Blind Option" },

                { m_Setting.GetEnumValueLocaleID(Setting.SomeEnum.Off), "Turn Off" },
                { m_Setting.GetEnumValueLocaleID(Setting.SomeEnum.Protanopia), "Protanopia" },
                { m_Setting.GetEnumValueLocaleID(Setting.SomeEnum.Tritanopia), "Tritanopia" },
                { m_Setting.GetEnumValueLocaleID(Setting.SomeEnum.Deuteranopia), "Deuteranopia" },



            };
        }

        public void Unload()
        {

        }
    }
}
