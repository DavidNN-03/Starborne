using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Starborne.SceneHandling;
using Starborne.Saving;
using Starborne.Containers;

namespace Starborne.Control
{
    public class CharacterSelect : MonoBehaviour /*Class that allows the player to see the stats of the different characters and select one.*/
    {
        private int index = 0; /*The index of the currently viewed character.*/

        [SerializeField] private TextMeshProUGUI nameText; /*The text that displays the currently viewed chacater's name.*/
        [SerializeField] private TextMeshProUGUI healthText; /*The text that displays the currently viewed chacater's max health.*/
        [SerializeField] private TextMeshProUGUI damageText; /*The text that displays the currently viewed chacater's damage per projectile.*/
        [SerializeField] private TextMeshProUGUI shotsPerSecondText; /*The text that displays the currently viewed chacater's shots per second per gun.*/
        [SerializeField] private TextMeshProUGUI speedText; /*The text that displays the currently viewed chacater's max speed.*/
        [SerializeField] private MeshFilter meshFilter; /*The MeshFilter of the character preview.*/
        [SerializeField] private MeshRenderer meshRenderer; /*The MeshRenderer of the character preview.*/

        Character[] characters; /*Array of all the found characters.*/
        Mesh[] meshes; /*Array of all the characters' meshes.*/
        Material[] materials; /*Array of all the characters' materials.*/

        void Start() /*The file CharPaths.json which includes the paths to all the character files is found and all the characters, their meshes, and their materials are loaded. Lastly, the UI is updated to show the first character.*/
        {
            string pathsPath = "Assets/Resources/CharPaths.json";
            StreamReader streamReader = new StreamReader(pathsPath);
            string jPaths = streamReader.ReadToEnd();
            ArrayContainer arrayContainer = JsonUtility.FromJson<ArrayContainer>(jPaths);

            string[] charPaths = arrayContainer.array;
            characters = new Character[charPaths.Length];
            meshes = new Mesh[charPaths.Length];
            materials = new Material[charPaths.Length];

            for (int i = 0; i < charPaths.Length; i++)
            {
                characters[i] = GetCharacter(charPaths[i]);
                string meshFileName = characters[i].meshFileName;
                meshes[i] = Resources.Load<Mesh>("Meshes/" + meshFileName);
                string materialFileName = characters[i].materialFileName;
                materials[i] = Resources.Load<Material>("Materials/" + materialFileName);
            }

            UpdateUI();
        }

        private Character GetCharacter(string path) /*Returns a character at a given path.*/
        {
            StreamReader streamReader = new StreamReader(path);
            string jCharacter = streamReader.ReadToEnd();
            Character character = JsonUtility.FromJson<Character>(jCharacter);
            return character;
        }

        void Update() /*Go to next or previous character if the player pressed the left or right arrow key.*/
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

        public void nextChar() /*Increments to the next character*/
        {
            index++;
            if (index >= characters.Length)
            {
                index = 0;
            }
            UpdateUI();
        }

        public void previousChar() /*Decrements to the previous character.*/
        {
            index--;
            if (index < 0)
            {
                index = characters.Length - 1;
            }
            UpdateUI();
        }

        private void UpdateUI() /*Update the text and character preview to the currently selected character.*/
        {
            nameText.text = characters[index].name;
            healthText.text = "HP: " + characters[index].maxHP;
            damageText.text = "Damage: " + characters[index].damagePerShot;
            shotsPerSecondText.text = "Rate of fire: " + characters[index].shotsPerSecond;
            speedText.text = "Speed: " + characters[index].maxSpeed;
            meshFilter.mesh = meshes[index];
            meshRenderer.material = materials[index];
        }

        public void SelectCharacter() /*This function is called when the in-game select-button is pressed. The character is saved in the CharacterHandler and SceneHandler loads the level-select-scene.*/
        {
            FindObjectOfType<CharacterHandler>().SetCharacterStats(characters[index]);
            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();

            sceneHandler.LoadScene(sceneHandler.levelSelectSceneIndex);
        }
    }
}