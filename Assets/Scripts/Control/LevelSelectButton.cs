using UnityEngine;
using Starborne.SceneHandling;
using Starborne.Saving;
using UnityEngine.UI;

namespace Starborne.Control
{
    public class LevelSelectButton : MonoBehaviour /*Class that manages the level select buttons. These buttons can be pushed to load the given level.*/
    {
        private string scenePath; /*Path to the scene's JSON-file.*/
        private int stars; /*Amount of stars achieved in this level.*/
        [SerializeField] private GameObject[] starsImages; /*Array of the GameObjects with the star images.*/
        [SerializeField] private Sprite starSprite; /*The Sprite that will be displayed if the player has gotten a star.*/
        [SerializeField] private Color starColor; /*The color of the starSprite.*/
        [SerializeField] private Sprite noStarSprite; /*The Sprite that will be displayed if the player has not gotten a star.*/
        [SerializeField] private Color noStarColor; /*The color of noStarSprite.*/

        private bool isUnlocked = false; /*Whether or not the player has unlocked this level.*/

        public void SetScenePath(string newScenePath) /*Assigns new value to scenePath.*/
        {
            scenePath = newScenePath;
        }

        public void SetStarCount(int starCount) /*Assigns new value to stars.*/
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

        public void SetUnlocked(bool state) /*Assigns new value to isUnlocked.*/
        {
            isUnlocked = state;
        }

        public string GetScenePath() /*Returns scenePath*/
        {
            return scenePath;
        }

        public void AttemptSubmit() /*Attempt to enter the given level. If isUnlocked is true, call SendPathToSceneDataHandler.*/
        {
            if (isUnlocked)
            {
                SendPathToSceneDataHandler();
            }
        }

        private void SendPathToSceneDataHandler() /*Find SceneDataHandler and save the scenePath with SetSceneData. Also, load the loading-scene.*/
        {
            FindObjectOfType<SceneDataHandler>().SetSceneData(scenePath);
            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();
            sceneHandler.LoadScene(sceneHandler.loadingSceneIndex);
        }
    }
}