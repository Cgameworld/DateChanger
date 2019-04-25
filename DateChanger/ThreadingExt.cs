using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;

namespace DateChanger
{
    public class ThreadingExt : LoadingExtensionBase
    {
        SimulationManager sim = Singleton<SimulationManager>.instance;
        long daylength = 864000000000;
        //value if nothing is set
        string grabbedGameTime = "1/01/2019 12:01:00PM";

        public override void OnLevelLoaded(LoadMode mode)
        {
            CityInfoPanel panel = UIView.library.Show<CityInfoPanel>("CityInfoPanel");
            UIButton bButton = panel.component.AddUIComponent<UIButton>();

            //textfield
            UITextField textField = panel.component.AddUIComponent<UITextField>();

            textField.size = new Vector2(90f, 17f);
            textField.padding = new RectOffset(6, 6, 3, 3);
            textField.builtinKeyNavigation = true;
            textField.isInteractive = true;
            textField.readOnly = false;
            textField.horizontalAlignment = UIHorizontalAlignment.Center;
            textField.selectionSprite = "EmptySprite";
            textField.selectionBackgroundColor = new Color32(0, 172, 234, 255);
            textField.normalBgSprite = "TextFieldPanelHovered";
            textField.disabledBgSprite = "TextFieldPanel";
            textField.textColor = new Color32(0, 0, 0, 255);
            textField.disabledTextColor = new Color32(0, 0, 0, 128);
            textField.color = new Color32(255, 255, 255, 255);

            textField.size = new Vector2(150f,27f);
            textField.padding.top = 7;

            textField.relativePosition = new Vector3(140f, 448f);

            //bButton
            bButton.size = new Vector2(90f, 27f);
            bButton.textScale = 1f;
            bButton.normalBgSprite = "ButtonMenu";
            bButton.hoveredBgSprite = "ButtonMenuHovered";
            bButton.pressedBgSprite = "ButtonMenuPressed";
            bButton.disabledBgSprite = "ButtonMenuDisabled";
            bButton.disabledTextColor = new Color32(128, 128, 128, 255);
            bButton.canFocus = false;

            bButton.width = 80;
            bButton.text = "Get Date";

            bButton.relativePosition = new Vector3(310f, 448f);

            panel.component.height = 321f + 5f + 16f;
            grabbedGameTime = sim.m_currentGameTime.ToString();
            grabbedGameTime = grabbedGameTime.Split(' ')[0];
            textField.text = grabbedGameTime;

            bButton.eventClick += (component, check) =>
            {
                grabbedGameTime = textField.text;
                Debug.Log(textField.text);
                bButton.text = "Change";
                ChangeDate();
            };

        }

        public void ChangeDate()
        {
            int day = Int32.Parse(grabbedGameTime.Split('/')[0]);
            int month = Int32.Parse(grabbedGameTime.Split('/')[1]);
            int year = Int32.Parse(grabbedGameTime.Split('/')[2].Split(' ')[0]);

            long dayTicks = day * daylength;
            long monthTicks = month * daylength * 30;
            long yearTicks = year * daylength * 365;

            Debug.Log("DT " + dayTicks);
            Debug.Log("MT " + monthTicks);
            Debug.Log("YT " + yearTicks);

            sim.m_timeOffsetTicks = dayTicks + monthTicks + yearTicks;
        }


    }


}