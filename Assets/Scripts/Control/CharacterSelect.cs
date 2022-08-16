using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Starborne.SceneHandling;
using Starborne.Saving;

namespace Starborne.Control
{
    public class CharacterSelect : MonoBehaviour
    {
        int index = 0;

        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI healthText;
        [SerializeField] TextMeshProUGUI damageText;
        [SerializeField] TextMeshProUGUI shotsPerSecondText;
        [SerializeField] TextMeshProUGUI speedText;
        [SerializeField] TextMeshProUGUI agilityText;

        Character[] characters;

        void Start()
        {
            string pathsPath = "Assets/Resources/Paths.json";
            StreamReader streamReader = new StreamReader(pathsPath);
            string jPaths = streamReader.ReadToEnd();
            ArrayContainer arrayContainer = JsonUtility.FromJson<ArrayContainer>(jPaths);

            string[] charPaths = arrayContainer.array;
            characters = new Character[charPaths.Length];

            for (int i = 0; i < charPaths.Length; i++)
            {
                characters[i] = GetCharacter(charPaths[i]);
            }

            UpdateStats();
        }

        private Character GetCharacter(string path)
        {
            StreamReader streamReader = new StreamReader(path);
            string jCharacter = streamReader.ReadToEnd();
            Character character = JsonUtility.FromJson<Character>(jCharacter);
            return character;
        }

        void Update()
        {
            if (Input.GetKeyDown("left"))
            {
                previousChar();
            }
            else if (Input.GetKeyDown("right"))
            {
                nextChar();
            }
        }

        public void nextChar()
        {
            index++;
            if (index >= characters.Length)
            {
                index = 0;
            }
            UpdateStats();
        }

        public void previousChar()
        {
            index--;
            if (index < 0)
            {
                index = characters.Length - 1;
            }
            UpdateStats();
        }

        private void UpdateStats()
        {
            nameText.text = characters[index].name;
            healthText.text = "HP: " + characters[index].maxHP;
            damageText.text = "Damage: " + characters[index].damagePerShot;
            shotsPerSecondText.text = "Rate of fire: " + characters[index].shotsPerSecond;
            //speedText.text = "Speed: " + characters[index].speed;
            agilityText.text = "Agility: " + characters[index].turnSensitivity;
        }

        public void SelectCharacter()
        {
            FindObjectOfType<CharacterHandler>().SetCharacterStats(characters[index]);
            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();

            sceneHandler.LoadScene(sceneHandler.levelSelectSceneIndex);
        }
    }
}