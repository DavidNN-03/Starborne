using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Building
{
    public class LateInitObject : MonoBehaviour
    {
        public void LateAwake()
        {
            foreach (ILateInit li in GetComponentsInChildren<ILateInit>())
            {
                li.LateAwake();
            }
        }

        public void LateStart()
        {
            foreach (ILateInit li in GetComponentsInChildren<ILateInit>())
            {
                li.LateStart();
            }
        }
    }
}