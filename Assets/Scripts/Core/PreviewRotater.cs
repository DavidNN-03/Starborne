using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Core
{
    public class PreviewRotater : MonoBehaviour /*Class that rotates the character preview in the character select.*/
    {
        [SerializeField] private float degreesPerFrame; /*How many frames the preview should rotate with per frame.*/

        private void Update() /*Rotate the preview.*/
        {
            transform.Rotate(0, degreesPerFrame, 0, Space.World);
        }
    }
}