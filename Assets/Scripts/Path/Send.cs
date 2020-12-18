using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Send : MonoBehaviour {
    string number;
  public  InputField inputFiled_number;
    public GameObject send00;
    public GameObject send001;
    [Header("限制")]
    public GameObject shop00;
    public GameObject shop001;
    public GameObject send;
    void OnEnable()
    {
        GetNumber();
    }
    public void GetNumber()
    {
        number = UserDB.instance.GetUserNumber();
        inputFiled_number.text = number;
    }
    public void SelectPlaceClick()
    {
        send00.SetActive(true);
        send001.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
