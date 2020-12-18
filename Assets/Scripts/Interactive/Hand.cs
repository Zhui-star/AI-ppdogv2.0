using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float maxYRotation = 120;
    public float minYRotation = 0;
    public float maxXRotation = 60;
    public float minXRotation = 0;
    //射击的间隔时长
    private float shootTime = 1;
    //射击间隔时间的计时器
    private float shootTimer = 0;
    //子弹的游戏物体，和子弹的生成位置
    public GameObject bulletGO;
    public Transform firePosition;
    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootTime)
        {
            //点击鼠标左键，进行射击
            if (Input.GetMouseButtonDown(0))
            {
                //实例化子弹
                GameObject bulletCurrent = GameObject.Instantiate(bulletGO, firePosition.position, transform.localRotation);
                // 通过刚体组件给子弹添加一个正前方向上的力，以达到让子弹向前运动的效果
               

                shootTimer = 0;



            }
            float xPosPrecent = Input.mousePosition.x / Screen.width;
            float yPosPrecent = Input.mousePosition.y / Screen.height;

            float xAngle = -Mathf.Clamp(yPosPrecent * maxXRotation, minXRotation, maxXRotation) + 15;
            float yAngle = Mathf.Clamp(xPosPrecent * maxYRotation, minYRotation, maxYRotation) - 180;

            transform.eulerAngles = new Vector3(xAngle, yAngle, 0);
        }
    }
}
