using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.UI
{
    public class EnemyPointer : MonoBehaviour, ILateInit /*Class that handles the markers pointing to the enemies in the Game scene.*/
    {
        [SerializeField] private float screenMargin;
        [SerializeField] private RectTransform canvas; /*RectTransform of the Canvas.*/
        [SerializeField] private Vector3 pointerOffset; /*On-screen offset for the pointers.*/
        [SerializeField] private RectTransform pointersParent; /*All pointers will be instantiated as children of this GameObject.*/
        [SerializeField] private RectTransform pointerPrefab; /*This prefab will be instantiated as pointers to the enemies.*/
        [SerializeField] private List<GameObject> enemies; /*List of the enemies still alive in the scene.*/
        private Dictionary<GameObject, GameObject> pointers; /*Dictionary that uses the enemies as keys and pointers as values.*/
        private GameObject player; /*The player GameObject.*/
        private Vector3 canvasCenter; /*Center point of the canvas.*/
        private bool hasStarted = false; /*Whether or not Update should be executed.*/

        public void LateAwake() /*Find the enemies and the player. Add the enemies to the pointers Dictionary as keys and instantiate instances of pointerPrefab as their values.*/
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
            player = GameObject.FindWithTag("Player");

            enemies = new List<GameObject>();
            pointers = new Dictionary<GameObject, GameObject>();

            foreach (GameObject enemy in gameObjects)
            {
                enemies.Add(enemy);
                pointers.Add(enemy, Instantiate(pointerPrefab, pointersParent).gameObject);
            }
        }

        public void LateStart() /*Set hasStarted to true and calculate the value of canvasCenter.*/
        {
            hasStarted = true;

            canvasCenter = new Vector3(canvas.rect.width / 2f, canvas.rect.height / 2f, 0f) * canvas.localScale.x;
        }

        private void Update() /*If hasStarted is true, calculate and set the new position of every enemy's corresponding pointer.*/
        {
            if (!hasStarted) return;

            foreach (GameObject enemy in enemies)
            {
                GameObject pointer = pointers[enemy];

                //https://www.youtube.com/watch?v=Ffi8kz6AheA&ab_channel=Omti9
                //https://github.com/Omti90/Off-Screen-Target-Indicator-Tutorial/blob/main/Scripts/TargetIndicator.cs

                Vector3 pointerPos = Camera.main.WorldToScreenPoint(enemy.transform.position) + pointerOffset;

                if (pointerPos.x >= 0f && pointerPos.x <= canvas.rect.width * canvas.localScale.x &&
                    pointerPos.y >= 0f && pointerPos.y <= canvas.rect.height * canvas.localScale.x &&
                    pointerPos.z >= 0f) //if the pointer is naturally within the bounds of the screen.
                {
                    pointerPos.z = 0f;

                    pointerPos.x = Mathf.Max(pointerPos.x, screenMargin);
                    pointerPos.x = Mathf.Min(pointerPos.x, canvas.rect.width * canvas.localScale.x - screenMargin);

                    pointerPos.y = Mathf.Max(pointerPos.y, screenMargin);
                    pointerPos.y = Mathf.Min(pointerPos.y, canvas.rect.height * canvas.localScale.x - screenMargin);
                }
                else if (pointerPos.z >= 0f) /*If the pointer is outside the screen.*/
                {
                    pointerPos = OutOfRangePos(pointerPos);
                }
                else /*If the player is looking away from the enemy.*/
                {
                    pointerPos *= -1f;
                    pointerPos = OutOfRangePos(pointerPos);
                }

                pointer.transform.position = pointerPos;
            }
        }

        private Vector3 OutOfRangePos(Vector3 pointerPos) /*Handles pointers outside the bounds of the screen.*/
        {
            pointerPos.z = 0f;

            pointerPos -= canvasCenter;

            float divX = (canvas.rect.width / 2f - screenMargin) / Mathf.Abs(pointerPos.x);
            float divY = (canvas.rect.height / 2f - screenMargin) / Mathf.Abs(pointerPos.y);

            if (divX < divY)
            {
                float angle = Vector3.SignedAngle(Vector3.right, pointerPos, Vector3.forward);
                pointerPos.x = Mathf.Sign(pointerPos.x) * (canvas.rect.width * 0.5f - screenMargin) * canvas.localScale.x;
                pointerPos.y = Mathf.Tan(Mathf.Deg2Rad * angle) * pointerPos.x;
            }

            else
            {
                float angle = Vector3.SignedAngle(Vector3.up, pointerPos, Vector3.forward);

                pointerPos.y = Mathf.Sign(pointerPos.y) * (canvas.rect.height / 2f - screenMargin) * canvas.localScale.y;
                pointerPos.x = -Mathf.Tan(Mathf.Deg2Rad * angle) * pointerPos.y;
            }

            pointerPos += canvasCenter;
            return pointerPos;
        }

        public void EnemyDestroyed(GameObject enemy) /*Removes the given enemy from enemies and pointers, and destroys that enemy's pointer.*/
        {
            enemies.Remove(enemy);
            Destroy(pointers[enemy]);
            pointers.Remove(enemy);
        }
    }
}