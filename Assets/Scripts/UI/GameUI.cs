using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Starborne.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI missionText;
        [SerializeField] TextMeshProUGUI dampeningTypeText;
        [SerializeField] TextMeshProUGUI healthText;
        [SerializeField] TextMeshProUGUI speedText;

        public void UpdateMissionText(int enemiesKilled, int enemiesCount)
        {
            missionText.text = "Destroy enemies: " + enemiesKilled + "/" + enemiesCount;
        }

        public void UpdateDampeningText(MovementType movementType)
        {
            string newText = "";
            if (movementType == MovementType.noDampening) newText = "No dampening";
            else if (movementType == MovementType.dampenedBaseSpeed) newText = "Dampened to base speed";
            else if (movementType == MovementType.dampenedStatic) newText = "Dampened to static";
            dampeningTypeText.text = newText;
        }

        public void UpdateHealthText(float hp, float maxHP)
        {
            healthText.text = RoundToDecimal(hp, 2, true) + "/" + RoundToDecimal(maxHP, 2, true) + " HP";
        }

        public void UpdateSpeedText(float speed, float maxSpeed)
        {
            speedText.text = (RoundToDecimal(speed, 2, true) + "/" + RoundToDecimal(maxSpeed, 2, true));
        }

        private string RoundToDecimal(float value, int decimalPlaces, bool addZeros)
        {
            float fResult = Mathf.Round(value * Mathf.Pow(10f, decimalPlaces)) / Mathf.Pow(10f, decimalPlaces);
            string sResult = fResult.ToString();

            if (addZeros)
            {
                bool hasComma = GetHasComma(sResult);

                if (!hasComma)
                {
                    sResult += ',';
                }

                int decimals = GetDecimals(sResult);

                for (int i = 0; i < decimalPlaces - decimals; i++)
                {
                    sResult += "0";
                }
            }

            return sResult;
        }

        private bool GetHasComma(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ',')
                {
                    return true;
                }
            }
            return false;
        }

        private int GetDecimals(string s)
        {
            int d = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] != ',')
                {
                    d++;
                }
                else
                {
                    break;
                }
            }
            return d;
        }
    }
}