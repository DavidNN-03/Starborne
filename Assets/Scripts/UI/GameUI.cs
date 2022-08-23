using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Starborne.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI missionText;
        [SerializeField] TextMeshProUGUI dampeningTypeText;
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

        public void UpdateSpeedText(float speed, float maxSpeed)
        {
            speedText.text = (Mathf.Round(speed * 100f) / 100f) + "/" + (Mathf.Round(maxSpeed * 100f) / 100f);
        }
    }
}