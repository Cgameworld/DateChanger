using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace DateChanger
{
    public class ThreadingExt : LoadingExtensionBase
    {
        SimulationManager sim = Singleton<SimulationManager>.instance;
        long daylength = 864000000000;

        public override void OnLevelLoaded(LoadMode mode)
        {
            CityInfoPanel panel = UIView.library.Show<CityInfoPanel>("CityInfoPanel");
            UIButton aButton = panel.component.AddUIComponent<UIButton>();
            UIButton bButton = panel.component.AddUIComponent<UIButton>();

            //aButton - a
            aButton.size = new Vector2(90f, 30f);
            aButton.textScale = 0.9f;
            aButton.normalBgSprite = "ButtonMenu";
            aButton.hoveredBgSprite = "ButtonMenuHovered";
            aButton.pressedBgSprite = "ButtonMenuPressed";
            aButton.disabledBgSprite = "ButtonMenuDisabled";
            aButton.disabledTextColor = new Color32(128, 128, 128, 255);
            aButton.canFocus = false;

            aButton.width = 110;
            aButton.text = "Add 1 Day";

            aButton.relativePosition = new Vector3(140f, 447f);

            //bButton
            bButton.size = new Vector2(90f, 30f);
            bButton.textScale = 0.9f;
            bButton.normalBgSprite = "ButtonMenu";
            bButton.hoveredBgSprite = "ButtonMenuHovered";
            bButton.pressedBgSprite = "ButtonMenuPressed";
            bButton.disabledBgSprite = "ButtonMenuDisabled";
            bButton.disabledTextColor = new Color32(128, 128, 128, 255);
            bButton.canFocus = false;

            bButton.width = 130;
            bButton.text = "Remove 1 Day";

            bButton.relativePosition = new Vector3(267f, 447f);

            panel.component.height = 321f + 5f + 16f;

            aButton.eventClick += (component, check) =>
            {
                sim.m_timeOffsetTicks = sim.m_timeOffsetTicks + daylength;
            };

            bButton.eventClick += (component, check) =>
            {
                sim.m_timeOffsetTicks = sim.m_timeOffsetTicks - daylength;
            };



        }

    }


}