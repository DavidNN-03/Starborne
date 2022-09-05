using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.UI
{
    public class EnemyPointer : MonoBehaviour, ILateInit
    {
        [SerializeField] Vector3 pointerOffset;
        [SerializeField] RectTransform pointersParent;
        [SerializeField] RectTransform pointerPrefab;
        [SerializeField] List<GameObject> enemies;
        Dictionary<GameObject, GameObject> pointers;
        GameObject player;
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
        }

        private void Update()
        {
            if (!hasStarted) return;

            foreach (GameObject enemy in enemies)
            {
                GameObject pointer = pointers[enemy];

                Vector3 a = Camera.main.transform.forward;
                Vector3 b = enemy.transform.position - player.transform.position;

                float angle = Mathf.Acos((a.x * b.x + a.y * b.y + a.z * b.z) / (a.magnitude * b.magnitude)) * 180 / Mathf.PI;

                if (angle > 90)
                {
                    pointer.gameObject.SetActive(false);
                }
                else
                {
                    pointer.gameObject.SetActive(true);
                }
                pointer.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position) + pointerOffset;
            }
        }

        public void EnemyDestroyed(GameObject enemy)
        {
            enemies.Remove(enemy);
            Destroy(pointers[enemy]);
            pointers.Remove(enemy);
        }
    }
}