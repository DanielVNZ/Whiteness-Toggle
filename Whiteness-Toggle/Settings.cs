using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;
using Game.UI.Widgets;
using System.Collections.Generic;
using Whiteness_Toggle;

namespace Whiteness_Toggle
{
    [FileLocation(nameof(Whiteness_Toggle))]
    [SettingsUIGroupOrder(kButtonGroup, kToggleGroup)]
    [SettingsUIShowGroupName(kButtonGroup, kToggleGroup)]
    public class Setting : ModSetting
    {

        private WhitenessSystem _system;
        private Mod _mod;
        public const string kSection = "Main";

        public const string kButtonGroup = "Button";
        public const string kToggleGroup = "Toggle";
        public const string kSliderGroup = "Slider";
        public const string kDropdownGroup = "Dropdown";

        public Setting(IMod mod, WhitenessSystem system) : base(mod)
        {
            _mod = (Mod)mod;
            _system = system;
        }
        public override void SetDefaults()
        {
            //throw new System.NotImplementedException();
            ToggleWhiteness = false;
        }

        public override void Apply()
        {
            base.Apply();
            _system.TriggerUpdate("UseStickyWhiteness");
            
        }



        [SettingsUISection(kSection, kToggleGroup)]
        public bool ToggleWhiteness { get; set; }

        
        
        

        

    
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
                { m_Setting.GetSettingsLocaleID(), "Mod settings sample" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection), "Main" },

                { m_Setting.GetOptionGroupLocaleID(Setting.kButtonGroup), "Buttons" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kToggleGroup), "Options" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kSliderGroup), "Sliders" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kDropdownGroup), "Dropdowns" },



                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ToggleWhiteness)), "Toggle Whiteness Off" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ToggleWhiteness)), $"(Unticked is whiteness on and ticked is whiteness off. Use this to enable/disable Whiteness, can also be done with Shift + W" },


            };
        }

        public void Unload()
        {

        }
    }
}
