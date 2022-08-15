using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialObjects : MonoBehaviour
{
    static EssentialObjects instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        if(instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
