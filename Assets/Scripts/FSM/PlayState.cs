using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState :FSMState {
    IdleState idle;
    public Transform initalPosition;
    public PlayState(FSMSystem fsm): base(fsm)
    {
        stateID = StateID.Play;
        initalPosition = GameObject.Find("DogInitiatePosition").transform;      
        idle = new IdleState(fsm);
        idle.isCanPatrol = false;
    }
    public override void Act(GameObject npc)
    {
        AnimationExcuting.instance.anim.SetBool("Play", true);
    }
    public override void Reason(GameObject npc)
    {
       
        if (idle.isCanPatrol)
        {
            AnimationExcuting.instance.anim.SetBool("Play", false);
            EnviromentManager.instance.ChangeDepthField(false);
            fsm.PerformTransition(Transition.SeePlayer);
        }
        string keycode = Message.instance.GetKeyCodes();
        switch (keycode)
        {
            case "过来":
            case "坐下":
            case "好吧":
            case "就这样吧":
            case "休息":
            case "你真":
                ComebackInitalPosition();
                break;
            case "关闭":
            case "再见":
            case "拜拜":
            case "关机":
                SwitchClosing();
                break;
            case "超市":
                AnimationExcuting.instance.anim.SetBool("Play", false);
                break;
            case "巡逻":
            case "看家":
            case "出门":
            case "看下家":
                AnimationExcuting.instance.anim.SetBool("Play", false);
                EnviromentManager.instance.ChangeDepthField(false);
                fsm.PerformTransition(Transition.SeePlayer);
                break;
        }
        if (DogAI.instance.target == null) return;
        if (DogAI.instance.target.tag == Tags.shop)
        {
            EnviromentManager.instance.ChangeDepthField(false);
            fsm.PerformTransition(Transition.Shopping);
        }
    }
    void ComebackInitalPosition(float speed = 3)
    {
        AnimationExcuting.instance.anim.SetBool("Play", false);
        Debug.Log("speed::" + speed);
        while (Vector3.Distance(GameObject.FindGameObjectWithTag(Tags.player).transform.position, initalPosition.position) > 1.0f)
        {
            GameObject.FindGameObjectWithTag(Tags.player).transform.Translate(Vector3.forward * Time.deltaTime * speed);
            GameObject.FindGameObjectWithTag(Tags.player).transform.LookAt(initalPosition.position);
            AnimationExcuting.instance.anim.SetBool("Walk", true);
            AnimationExcuting.instance.anim.SetBool("Bark", false);
        }

        CameraController.Instance.SetCamera(new Vector3(5, 9.6f, 36.2f));
        GameObject.FindGameObjectWithTag(Tags.player).transform.forward = initalPosition.transform.forward;
        fsm.PerformTransition(Transition.Open);

    }
    void SwitchClosing()
    {
        AnimationExcuting.instance.anim.SetBool("Play", false);
        fsm.PerformTransition(Transition.Close);
    }

}

