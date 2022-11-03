using Starborne.Containers;

namespace Starborne.Saving
{
    [System.Serializable]
    public class ObjectsContainer /*Contains data for an array of objects of the same prefab.*/
    {
        public string parentName; /*The name of the parent GameObject these GameObjects should be instantiated as children of.*/
        public string prefabPath; /*Path to the prefab that should be instantiated.*/

        public TransformContainer[] transformContainers; /*Array of TransformContainers. These objects contain the positions, rotations, and scales the GameObjects should be instantiated with.*/
    }
}