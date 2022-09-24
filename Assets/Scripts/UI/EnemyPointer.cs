using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Starborne.UI
{
    public class EnemyPointer : MonoBehaviour, ILateInit
    {
        [SerializeField] float offset;
        [SerializeField] RectTransform canvas;
        [SerializeField] Vector3 pointerOffset;
        [SerializeField] RectTransform pointersParent;
        [SerializeField] RectTransform pointerPrefab;
        [SerializeField] List<GameObject> enemies;
        Dictionary<GameObject, GameObject> pointers;
        GameObject player;
        Vector3 canvasCenter;
        bool hasStarted = false;

        public void LateAwake()
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

        public void LateStart()
        {
            hasStarted = true;

            canvasCenter = new Vector3(canvas.rect.width / 2f, canvas.rect.height / 2f, 0f) * canvas.localScale.x;
        }

        private void Update()
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
                    pointerPos.z >= 0f)
                {
                    pointerPos.z = 0f;

                    pointerPos.x = Mathf.Max(pointerPos.x, offset);
                    pointerPos.x = Mathf.Min(pointerPos.x, canvas.rect.width * canvas.localScale.x - offset);

                    pointerPos.y = Mathf.Max(pointerPos.y, offset);
                    pointerPos.y = Mathf.Min(pointerPos.y, canvas.rect.height * canvas.localScale.x - offset);
                }
                else if (pointerPos.z >= 0f)
                {
                    pointerPos = OutOfRangePos(pointerPos);
                }
                else
                {
                    pointerPos *= -1f;
                    pointerPos = OutOfRangePos(pointerPos);
                }

                pointer.transform.position = pointerPos;
            }
        }

        private Vector3 OutOfRangePos(Vector3 pointerPos)
        {
            pointerPos.z = 0f;

            pointerPos -= canvasCenter;

            float divX = (canvas.rect.width / 2f - offset) / Mathf.Abs(pointerPos.x);
            float divY = (canvas.rect.height / 2f - offset) / Mathf.Abs(pointerPos.y);

            if (divX < divY)
            {
                float angle = Vector3.SignedAngle(Vector3.right, pointerPos, Vector3.forward);
                pointerPos.x = Mathf.Sign(pointerPos.x) * (canvas.rect.width * 0.5f - offset) * canvas.localScale.x;
                pointerPos.y = Mathf.Tan(Mathf.Deg2Rad * angle) * pointerPos.x;
            }

            else
            {
                float angle = Vector3.SignedAngle(Vector3.up, pointerPos, Vector3.forward);

                pointerPos.y = Mathf.Sign(pointerPos.y) * (canvas.rect.height / 2f - offset) * canvas.localScale.y;
                pointerPos.x = -Mathf.Tan(Mathf.Deg2Rad * angle) * pointerPos.y;
            }

            pointerPos += canvasCenter;
            return pointerPos;
        }

        public void EnemyDestroyed(GameObject enemy)
        {
            enemies.Remove(enemy);
            Destroy(pointers[enemy]);
            pointers.Remove(enemy);
        }
    }
}