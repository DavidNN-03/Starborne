using System;
using System.Collections;
using System.Collections.Generic;
using Starborne.Core;
using Starborne.Saving;
using UnityEngine;

namespace Starborne.Building
{
    public class SceneBuilder : MonoBehaviour /*This class sets the skybox of the game scene and instantiates all the GameObjects defined in the level's JSON file*/
    {
        private SceneData sceneData; /*Contains the scene data retrieved from the SceneDataHandler.*/

        private void Awake() /*This functions retrieves the scene data from the SceneDataHandler on the EssentialObjects GameObject. Thereafter, it loads and assigns the given skybox. It then instantiates the GameObjects of the scene. Lastly, it finds all instances of ILateInitObject and call LateAwake on each of them followed by LateStart.*/
        {
            sceneData = EssentialObjects.instance.GetComponentInChildren<SceneDataHandler>().GetSceneData();

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
                        Instantiate(prefab, transformContainer.position, rotation, parent.transform);
                    }
                    else
                    {
                        Instantiate(prefab, transformContainer.position, rotation);
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
