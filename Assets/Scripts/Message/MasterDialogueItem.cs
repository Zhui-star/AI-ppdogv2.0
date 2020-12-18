using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MasterDialogueItem : MonoBehaviour {
    private Text message;
    public List<string> keywords = new List<string>();
    public GameObject petMessage;
    //private Transform VerticalLayout;
    void Start()
    {
        message= transform.Find("message_text").gameObject.GetComponent<Text>();
        if (this.transform.Find("dog_bg").gameObject.activeInHierarchy ==false)
        {
            SearchMessage();
        }
        }
    void SearchMessage()
    {
        int count = 0;
       if(message.text!=null&&message.text.Length!=0)
        {
            foreach(var item in keywords)
            {
                string currentMessage = item;             
                if (message.text.Contains(currentMessage))
                {
                    Message.instance.SetKeyCodes(currentMessage);
                    FilterMessage(currentMessage);
                }
                else
                {
                    count++;
                }
            }
        }
       if(count>=keywords.Count)
        {
            StartCoroutine(DefaultAnswer());
        }
    }
    void FilterMessage(string currentMessage)
    {
        switch(currentMessage)
        {
            case "聪明":
            case "机智":
            case "喜欢":
            case "爱":
                StartCoroutine(Gradte());
                break;
            case "跑腿":
                StartCoroutine(ShopOrSend());
                break;
            case "启动":
            case "你好":
            case "小狗同学":   
               StartCoroutine(StartUp());
                break;
            case "巡逻":
            case "看家":
            case "出门":
            case "看下家":
            case "离开一小会":
                StartCoroutine(Trail());
                break;
            case "超市":
            case "买":
            case "商城":
            case "购":
            case "shop":
                StartCoroutine(Shopping());
                Debug.Log("超市");
                break;
            case "快递":
            case "邮":
            case "包裹":
            case "信件":
                StartCoroutine(Send());
                break;
            case "再见":
            case "关闭":
            case "拜拜":
            case "关机":
                StartCoroutine(SwitchClosing());
                break;
            case "过来":
            case "坐下":
            case "回来":
               StartCoroutine(Comeback());
                break;
            case "玩":
            case "表演":
                StartCoroutine(StartPlay());
                break;
            case "好吧":
            case "就这样吧":
            case "休息":
            case "你真":
                StartCoroutine(StopPlay());
                break;
            default:StartCoroutine(DefaultAnswer());
                break;             
        }
    }
    IEnumerator Gradte()
    {
        yield return new WaitForSeconds(1.0f);
        InstantiatePetMessage("谢谢夸奖！嘿嘿");
    }
    IEnumerator ShopOrSend()
    {
        yield return new WaitForSeconds(1.0f);
        InstantiatePetMessage("是想购物还是拿快递？");
    }
    IEnumerator StopPlay()
    {
        yield return new WaitForSeconds(1.0f);
        InstantiatePetMessage("我可不可爱");
    }
    IEnumerator StartPlay()
    {
        yield return new WaitForSeconds(1.0f);
        if (AnimationExcuting.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Sleep"))
        {
            InstantiatePetMessage("ZZZZZZZ");
        }
        else {
            InstantiatePetMessage("好勒");
        }
    }
	IEnumerator StartUp()
    {
        yield return new WaitForSeconds(1.0f);
        InstantiatePetMessage("你好,我的朋友");
    }
    IEnumerator DefaultAnswer()
    {
        yield return new WaitForSeconds(1.0f);
        if (AnimationExcuting.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Sleep"))
        {
            InstantiatePetMessage("ZZZZZZZ");
        }
        else {
            InstantiatePetMessage("我不知道你在说什么");
        }
    }
    IEnumerator Trail()
    {
        if (GameObject.Find("UICanvas").transform.Find("ShoppingMall").gameObject.activeInHierarchy == true)
        {
            GameObject.Find("UICanvas").transform.Find("ShoppingMall").gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1.0f);
        //TODO 巡逻
      
        if (AnimationExcuting.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Sleep"))
        {
            InstantiatePetMessage("ZZZZZZZ");
        }
        else {
            InstantiatePetMessage("好的，我将保证没有一只苍蝇");
        }
    }
    IEnumerator Shopping()
    {
        yield return new WaitForSeconds(1.0f);
        if (AnimationExcuting.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Sleep"))
        {
            InstantiatePetMessage("ZZZZZZZ");
        }
        else {
            InstantiatePetMessage("好的你想购买点什么呢?");
            yield return new WaitForSeconds(1.0f);
            TransformState.instance.ShoppingState();
            //TODO 购物
        }
       
    }
    IEnumerator Send()
    {
        yield return new WaitForSeconds(1.0f);
        if (AnimationExcuting.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Sleep"))
        {
            InstantiatePetMessage("ZZZZZZZ");
        }
        else {
            InstantiatePetMessage("确认您的订单号");
            yield return new WaitForSeconds(1.0f);
            GameObject.Find("UICanvas").transform.Find("Send").gameObject.SetActive(true);
            //TODO 购物
        }
    }
    IEnumerator SwitchClosing()
    {
        yield return new WaitForSeconds(1.0f);
       
            InstantiatePetMessage("拜拜");
       
    }
    IEnumerator Comeback()
    {
        yield return new WaitForSeconds(1.0f);
        if (AnimationExcuting.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Sleep"))
        {
            InstantiatePetMessage("ZZZZZZZ");
        }
        else {
            InstantiatePetMessage("马上");
        }
    }
    void InstantiatePetMessage(string message)
    {
        this.transform.Find("dog_message").gameObject.GetComponent<Text>().text = message;
        this.transform.Find("dog_bg").gameObject.SetActive(true);
        this.transform.Find("dog_name").gameObject.SetActive(true);
        this.transform.Find("dog_message").gameObject.SetActive(true);
        
    }
	
}
