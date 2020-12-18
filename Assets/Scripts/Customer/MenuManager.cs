using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public AudioSource forest;
    void OnDisable()
    {
        forest.enabled = true;
    }
}
