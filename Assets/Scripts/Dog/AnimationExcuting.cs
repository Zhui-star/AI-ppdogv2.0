using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExcuting : MonoBehaviour {

    public static AnimationExcuting instance;
    public AudioClip bark;
    public Animator anim;
    public AudioSource dogSound;
    float time = 4;
    float timer = 0;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        anim = this.GetComponent<Animator>();
        timer = time;
    }
   void Update()
    {
        timer += Time.deltaTime;
    }
    void Bark()
    {
        if (timer >= time)
        {
            dogSound.enabled = true;
            dogSound.PlayOneShot(bark);
            timer = 0;
        }
    }
}
