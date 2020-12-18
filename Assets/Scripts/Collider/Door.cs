using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool isShow = false;
    public AudioSource streetSource;
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.player)
        {
            if (isShow)
            {
                isShow = false;
                streetSource.enabled = false;
            }
            else
            {
                isShow = true;
                streetSource.enabled = true;
            }
        }
    }
}
