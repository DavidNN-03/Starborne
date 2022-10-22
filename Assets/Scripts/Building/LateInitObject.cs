using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Building
{
    public class LateInitObject : MonoBehaviour /*All GameObjects with components that implement ILateInit must also have a component of this type or be descendants of one that does.*/
    {
        public void LateAwake() /*Iterate through all implementations of ILateInit in the GameObject or its descendants and call LateAwale*/
        {
            foreach (ILateInit li in GetComponentsInChildren<ILateInit>())
            {
                li.LateAwake();
            }
        }

        public void LateStart() /*Iterate through all implementations of ILateInit in the GameObject or its descendants and call LateAwale*/
        {
            foreach (ILateInit li in GetComponentsInChildren<ILateInit>())
            {
                li.LateStart();
            }
        }
    }
}
