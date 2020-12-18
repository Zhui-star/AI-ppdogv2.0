using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformState : MonoBehaviour {
    //信号量来标记商店界面是否打开
    private bool isShowShoppingMall = false;
    public static TransformState instance;
    public GameObject minimap;
    public AudioClip button;
    public AudioSource audiosource;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        minimap = GameObject.Find("MiniMapCircle");
        minimap.SetActive(false);
    }
	public void MessageButtonClick()
    {
        Message.instance.TransformState();
        PlayButtonClip();
    }
    public void ShoppingState()
    {
    
        if (isShowShoppingMall)
        {
            TransformState.instance.PlayButtonClip();
            isShowShoppingMall = false;
            GameObject.Find("UICanvas").transform.Find("ShoppingMall").gameObject.SetActive(false);
        }
        else
        {
            TransformState.instance.PlayButtonClip();
            isShowShoppingMall = true;
            GameObject.Find("UICanvas").transform.Find("ShoppingMall").gameObject.SetActive(true);
        }
    }
    public void ShowMinMap()
    {

        StartCoroutine("ShowMiniMap");
    }
    public IEnumerator ShowMiniMap()
    {
        yield return new WaitForSeconds(1.0f);
        minimap.SetActive(true);
    }
    public void ShowViewState()
    {
        GameObject.Find("UICanvas").transform.Find("ViewButton").gameObject.GetComponent<View>().TransformState();
        PlayButtonClip();
    }
    public void PlayButtonClip()
    {
        this.audiosource.PlayOneShot(button);
    }
}
