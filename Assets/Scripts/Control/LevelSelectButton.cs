using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.SceneHandling;
using System.IO;
using Starborne.Saving;
using UnityEngine.UI;

namespace Starborne.Control
{
    public class LevelSelectButton : MonoBehaviour
    {
        string scenePath;
        int stars;
        [SerializeField] GameObject[] starsImages;
        [SerializeField] Sprite starSprite;
        [SerializeField] Color starColor;
        [SerializeField] Sprite noStarSprite;
        [SerializeField] Color noStarColor;

        bool isUnlocked = false;

        public void SetScenePath(string newScenePath)
        {
            scenePath = newScenePath;
        }

        public void SetStarCount(int starCount)
        {
            stars = starCount;

            for (int i = 0; i < starsImages.Length; i++)
            {
                Image image = starsImages[i].GetComponent<Image>();
                if (stars > i)
                {
                    image.sprite = starSprite;
                    image.color = starColor;
                }
                else
                {
                    image.sprite = noStarSprite;
                    image.color = noStarColor;
                }
            }
        }

        public void SetUnlocked(bool state)
        {
            isUnlocked = state;
        }

        public string GetScenePath()
        {
            return scenePath;
        }

        public void AttemptSubmit()
        {
            if (isUnlocked)
            {
                SendPathToSceneDataHandler();
            }
        }

        private void SendPathToSceneDataHandler()
        {
            FindObjectOfType<SceneDataHandler>().SetSceneData(scenePath);
            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();
            sceneHandler.LoadScene(sceneHandler.gameSceneIndex);
        }
    }
}