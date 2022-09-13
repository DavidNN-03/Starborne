using System;
using System.Collections;
using System.Collections.Generic;
using Starborne.Saving;
using UnityEngine;

namespace Starborne.Building
{
    public class SceneBuilder : MonoBehaviour
    {
        SceneData sceneData;
        List<GameObject> gameObjectsInstantiated = new List<GameObject>();

        void Awake()
        {
            sceneData = FindObjectOfType<SceneDataHandler>().GetSceneData();

            Material skybox = Resources.Load<Material>(sceneData.skyboxPath);

            RenderSettings.skybox = skybox;

            foreach (ObjectsContainer objectsContainer in sceneData.objectContainers)
            {
                GameObject prefab = Resources.Load<GameObject>(objectsContainer.prefabPath);
                GameObject parent = GameObject.Find(objectsContainer.parentName);

                foreach (TransformContainer transformContainer in objectsContainer.transformContainers)
                {
                    Quaternion rotation = Quaternion.Euler(transformContainer.rotation);

                    if (parent != null)
                    {
                        gameObjectsInstantiated.Add(Instantiate(prefab, transformContainer.position, rotation, parent.transform));
                    }
                    else
                    {
                        gameObjectsInstantiated.Add(Instantiate(prefab, transformContainer.position, rotation));
                    }
                }
            }

            foreach (LateInitObject lateInit in FindObjectsOfType<LateInitObject>())
            {
                lateInit.LateAwake();
            }
            foreach (LateInitObject lateInit in FindObjectsOfType<LateInitObject>())
            {
                lateInit.LateStart();
            }
        }
    }
}
