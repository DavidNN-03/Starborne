namespace Starborne.Saving
{
    [System.Serializable]
    public class ObjectToSaveWithObjectName /*Class that stores the name of a GameObject that should be saved with the SceneCapturer and the path to its prefab.*/
    {
        public string objectName; /*Name of the GameObject that should be saved.*/
        public string prefabPath; /*Path to the prefab of the GameObject.*/
    }
}