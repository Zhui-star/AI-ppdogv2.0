using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Text.RegularExpressions;

public class UserDB : MonoBehaviour {
    public static UserDB instance;
    public InputField userName;
    public InputField userPassword;
    public InputField ConfirmPassword;
    public InputField userNumber;
    public InputField UserName;
    public InputField Password;
    public GameObject message;
    //用户现在登录的账号和密码
    private string currentName;
    private string currentPassword;
    public Text hidePassword;
    public List<UserData> users = new List<UserData>();
    public GameObject message_button;
    void Awake()
    {
        instance = this;
        if(!Directory.Exists(Application.dataPath + "/Text"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Text");
        }
    }

    void Start()
    {
        GameObject.Find("UICanvas").transform.Find("LoginPanel/login_button").GetComponent<Button>().onClick.AddListener(LoginUser);
        GameObject.Find("UICanvas").transform.Find("RegistPanel/regist_button").GetComponent<Button>().onClick.AddListener(RegisterUser);
        message_button.SetActive(false);
    }

	public void RegisterUser()
    {
        SetUsers();
        if (ConfirmPassword.text.Trim()!=userPassword.text.Trim())
        {
            
            StartCoroutine(ShowMessage("请输入相同密码"));
         
            return;
        }
        foreach(UserData temp in users)
        {
            string name = temp.userName;
            if(name==userName.text.Trim())
            {
                StartCoroutine(ShowMessage("账号已存在"));
                return;
            }
        }

        string number = userNumber.text;
       if(number.Length!=11||IsDigit(number)==false)
        {

            StartCoroutine(ShowMessage("请输入正确的手机号码"));
            return;
        }

       if(userNumber.text.Trim().Length<6||userPassword.text.Length<6)
        {
  
            StartCoroutine(ShowMessage("账号密码最少不能低于6位字符"));
            return;
        }
       if(IsCN(userName.text.Trim())==false||IsCN(userPassword.text.Trim())==false||IsCN(userNumber.text.Trim())==false)
        {

            StartCoroutine(ShowMessage("请正确在账号密码，手机号处写入字符或者数字"));
            return;
        }
       
       List<UserData> user_s = CreatUserData();

        string filePath = Application.dataPath  +"/Text"+"/user.json";

      

        string saveJsonStr = JsonMapper.ToJson(user_s);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        sw.Close();
      
        StartCoroutine(ShowMessage("注册成功"));
        ReturnClcik();
    }
    public void LoginUser()
    {
        SetUsers();
        int sentinel = 0;
        foreach (UserData temp in users)
        { string name = temp.userName;
            string password = temp.userPassword;
            if(name==UserName.text&&Password.text==password)
            {
                currentName = name;
                currentPassword = password;
                sentinel++;
                GameObject.Find("UICanvas").transform.Find("LoginPanel").gameObject.SetActive(false);
                GameObject.Find("UICanvas").transform.Find("message_button").gameObject.SetActive(true);
                message_button.SetActive(true);
            }
        }
        if(sentinel==0)
        {
          
            StartCoroutine(ShowMessage("账号密码错误"));
        }
    }
    public List<UserData> CreatUserData()
    {
        UserData user = new UserData();
        user.userName = userName.text;
        user.userPassword = userPassword.text;
        user.userNumber = userNumber.text;
        users.Add(user);
        return users;
    }
 /// <summary>
 /// 刷新Json文件
 /// </summary>
    public void SetUsers()
    {
        string filePath = Application.dataPath + "/Text" + "/user.json";
        if (File.Exists(filePath))
        {
            //创建一个StreamReader，用来读取流
            StreamReader sr = new StreamReader(filePath);
            //将读取到的流赋值给jsonStr
            string jsonStr = sr.ReadToEnd();
            //关闭
            sr.Close();

            //将字符串jsonStr转换为Save对象
            if (jsonStr.Length <= 0) return;
            List<UserData> tempUser = JsonMapper.ToObject<List<UserData>>(jsonStr);
            if (tempUser.Count <= 0) return;
            foreach(UserData temp in tempUser)
            {
                users.Add(temp);
            }
        }
        else
        {
            File.Create(Application.dataPath + "/Text" + "/user.json");
        }
    }
    public static bool IsCN(string strIn)
    {
        
        //return Regex.IsMatch(strIn, @"^[A-Za-z]+$");
        return Regex.IsMatch(strIn, @"^[a-zA-Z0-9]*$");
    }
    public static bool IsDigit(string strIn)
    {
        return Regex.IsMatch(strIn, @"^\d+$");
    }
    /// <summary>
    /// 返回手机号码userNumber
    /// </summary>
    /// <returns></returns>
    public string GetUserNumber()
    {
        SetUsers();
        foreach(UserData temp in users)
        {
            string name = temp.userName;
            string password = temp.userPassword;
            if(name==currentName&&password==currentPassword)
            {
                Debug.Log("userNumber::" + temp.userNumber);
                Debug.Log("尾号::" + TailNumber(temp.userNumber));
                return temp.userNumber;
            }

        }
        return "请登录";
    }
    public void ReturnClcik()
    {
        GameObject.Find("UICanvas").transform.Find("LoginPanel").gameObject.SetActive(true);
        GameObject.Find("UICanvas").transform.Find("RegistPanel").gameObject.SetActive(false);
    }
    public string TailNumber(string str)
    {
        return str.Substring(7, 4);
    }
    IEnumerator ShowMessage(string str)
    {
        message = GameObject.Find("UICanvas").transform.Find("Mesaage_Bg").gameObject;
        message.SetActive(true);
        message.transform.GetChild(0).gameObject.GetComponent<Text>().text = str;
        yield return new WaitForSeconds(2.0f);
        message.SetActive(false);
    }
    //隐藏密码
    public void HidePassword(string a)
    { 
        if (Input.GetKeyDown(KeyCode.Backspace)) { hidePassword.text="";Password.text = ""; return; }
        hidePassword.text += "*";
        
    }
}
