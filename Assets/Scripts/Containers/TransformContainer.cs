using UnityEngine;

namespace Starborne.Containers
{
    [System.Serializable]
    public class TransformContainer /*Class that contains the value for a Transform component.*/
    {
        public Vector3 position; /*Position of the GameObject.*/
        public Vector3 rotation; /*Rotation of the GameObject.*/
        public Vector3 scale; /*Scale of the GameObject.*/
    }
}