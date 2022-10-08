using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class SceneCapturer : MonoBehaviour
{
    [SerializeField] bool capture = false;
    [SerializeField] string fileName;
    [SerializeField] string sceneName;
    [SerializeField] string skyboxPath;
    [SerializeField] AssignmentsContainer assignments;
    [SerializeField] ObjectToSaveWithParentName[] objectsToSaveWithParentName;
    [SerializeField] ObjectToSaveWithObjectName[] objectsToSaveWithObjectName;

    private void Update()
    {
        if (!capture) return;

        SceneData sceneData = new SceneData();

        sceneData.sceneName = sceneName;

        sceneData.assignments = new AssignmentsContainer();
        sceneData.assignments.x = new SecondaryAssignment();
        sceneData.assignments.y = new SecondaryAssignment();
        sceneData.assignments.z = new SecondaryAssignment();

        sceneData.assignments.x.completed = false;
        sceneData.assignments.y.completed = false;
        sceneData.assignments.z.completed = false;

        sceneData.skyboxPath = skyboxPath;
        sceneData.assignments = assignments;
        sceneData.objectContainers = new ObjectsContainer[objectsToSaveWithParentName.Length + objectsToSaveWithObjectName.Length];

        for (int i = 0; i < objectsToSaveWithParentName.Length; i++)
        {
            sceneData.objectContainers[i] = GetNewObjectsContainerWithParentName(objectsToSaveWithParentName[i].parentName, objectsToSaveWithParentName[i].prefabPath);
        }

        for (int i = 0; i < objectsToSaveWithObjectName.Length; i++)
        {
            sceneData.objectContainers[i + objectsToSaveWithParentName.Length] = GetNewObjectsContainerWithObjectName(objectsToSaveWithObjectName[i].objectName, objectsToSaveWithObjectName[i].prefabPath);
        }

        string json = JsonUtility.ToJson(sceneData);

        string path = "Assets/Resources/Scenes/" + fileName + ".json";

        StreamWriter t = new StreamWriter(path, false);
        t.Write(json);
        t.Close();

        capture = false;

        ObjectsContainer GetNewObjectsContainerWithParentName(string parentName, string prefabPath)
        {
            Transform parent = GameObject.Find(parentName).transform;
            ObjectsContainer objectsContainer = new ObjectsContainer();

            objectsContainer.prefabPath = prefabPath;
            objectsContainer.parentName = parentName;

            objectsContainer.transformContainers = new TransformContainer[parent.childCount];

            for (int i = 0; i < parent.childCount; i++)
            {
                TransformContainer newTransformContainer = new TransformContainer();
                Transform child = parent.GetChild(i);

                newTransformContainer.position = child.transform.position;
                newTransformContainer.rotation = child.transform.localRotation.eulerAngles;
                newTransformContainer.scale = child.transform.localScale;

                objectsContainer.transformContainers[i] = newTransformContainer;
            }

            return objectsContainer;
        }

        ObjectsContainer GetNewObjectsContainerWithObjectName(string objectName, string prefabPath)
        {
            ObjectsContainer objectsContainer = new ObjectsContainer();
            objectsContainer.prefabPath = prefabPath;

            objectsContainer.transformContainers = new TransformContainer[1];

            TransformContainer newTransformContainer = new TransformContainer();

            GameObject player = GameObject.FindGameObjectWithTag(objectName);

            newTransformContainer.position = transform.position;
            newTransformContainer.rotation = transform.localRotation.eulerAngles;
            newTransformContainer.scale = transform.localScale;

            objectsContainer.transformContainers[0] = newTransformContainer;

            return objectsContainer;
        }
    }
}
