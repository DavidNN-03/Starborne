namespace Starborne.Saving
{
    [System.Serializable]
    public class ObjectToSaveWithParentName /*Class that stores the name of a GameObject that should have its children saved by the ScreenCapturer and the path to the prefab.*/
    {
        public string parentName; /*Name of the parent. This gameObject's children will be saved by the SceneCapturer.*/
        public string prefabPath; /*The path to the prefab of the children.*/
    }
}