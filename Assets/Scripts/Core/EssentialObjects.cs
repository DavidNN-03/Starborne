using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Core
{
    public class EssentialObjects : MonoBehaviour /*Class that implements the Singleton pattern. The GameObject that this class is added to will be carried between scenes and any duplicates will be destroyed. You can add other components or child GameObjects to this gameObject to carry them between scenes along with this.*/
    {
        public static EssentialObjects instance; /*This will save the first instance of this class. Any other instances will be destroyed.*/

        private void Awake() /*If instance is null, then this instance is the first. If this instance is the first, call DontDestroyOnLoad so it doesnt get destroyed when other scenes are loaded. If this is not the first instance, destroy the GameObject.*/
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