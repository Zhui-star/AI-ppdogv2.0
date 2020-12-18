using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour {
    private bool isShow = false;
    public void TransformState()
    {
        if(isShow)
        {
            isShow = false;
           StartCoroutine(Hide());
        }
        else
        {
            isShow = true;
            Show();
        }
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
        this.GetComponent<Animator>().SetBool("Hide", false);
    }
    public IEnumerator Hide()
    {
        this.GetComponent<Animator>().SetBool("Hide", true);
        yield return new WaitForSeconds(0.75f);
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        if (GameObject.Find("UICanvas").transform.Find("view_button").gameObject.activeInHierarchy==false)
        {
            TransformState();
        }
    }
}
