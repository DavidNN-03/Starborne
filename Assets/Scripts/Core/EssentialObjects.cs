using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Core
{
    public class EssentialObjects : MonoBehaviour
    {
        public static EssentialObjects instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            if (instance == this)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}