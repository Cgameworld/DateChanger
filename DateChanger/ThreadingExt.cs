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
        long offsetfactor = 0;
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

            //textField.tooltip = "Date Format: mm/dd/yyyy";
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
            bButton.text = "Change";

            bButton.relativePosition = new Vector3(310f, 448f);

            panel.component.height = 321f + 5f + 16f;

            //get it instead from system.string.text
            //then static mode - system.string.text
            grabbedGameTime = sim.m_currentGameTime.ToString();
            grabbedGameTime = grabbedGameTime.Split(' ')[0];
            textField.text = "dd/mm/yyyy";

            bButton.eventClick += (component, check) =>
            {
                grabbedGameTime = textField.text;
                //finds extra tick offset that m_timeOffsetTicks doesn't account for
                offsetfactor = 0 - (sim.m_currentGameTime.ToBinary() - sim.m_timeOffsetTicks);
                ChangeDate();
            };

        }

        public void ChangeDate()
        {
            int day = Int32.Parse(grabbedGameTime.Split('/')[1]);
            int month = Int32.Parse(grabbedGameTime.Split('/')[0]);
            int year = Int32.Parse(grabbedGameTime.Split('/')[2].Split(' ')[0]);
            Debug.Log("Values from Field \nDay:" + day + "\nMonth: " + month + "\nYear: " + year);
            sim.m_timeOffsetTicks = DateToTicks(year,month,day) + offsetfactor;

        }

        //modifed tick calcuation methods from game
        private static long DateToTicks(int year, int month, int day)
        {
            if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
            {
                int[] array = System.DateTime.IsLeapYear(year) ? DaysToMonth366 : DaysToMonth365;
                if (day >= 1 && day <= array[month] - array[month - 1])
                {
                    int num = year - 1;
                    int num2 = num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1;
                    return num2 * 864000000000L;
                }
            }
            throw new ArgumentOutOfRangeException("error");
        }
        private static readonly int[] DaysToMonth366 = new int[13]
{0,31,60,91,121,152,182,213,244,274,305,335,366};
        private static readonly int[] DaysToMonth365 = new int[13]
 {0,31,59,90,120,151,181,212,243,273,304,334,365};

    }


}