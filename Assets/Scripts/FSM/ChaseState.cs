﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : FSMState {

    private Transform playerTransform;
    public ChaseState(FSMSystem fsm) : base(fsm)
    {
        stateID = StateID.Chase;
        if (GameObject.FindGameObjectWithTag(Tags.enemy) == null) return;
        playerTransform = GameObject.FindGameObjectWithTag(Tags.enemy).transform;
    }

    public override void Act(GameObject npc)
    {
        if (GameObject.FindGameObjectWithTag(Tags.enemy)==null) return;
        if (GameObject.FindGameObjectWithTag(Tags.enemy).transform != null)
        {
            playerTransform = GameObject.FindGameObjectWithTag(Tags.enemy).transform;
        }
        Vector3 offset = playerTransform.transform.position;
        offset.y = 4.09f;
       playerTransform.transform.position = new Vector3(playerTransform.transform.position.x, offset.y, playerTransform.transform.position.z);
        npc.transform.LookAt(playerTransform.transform.position);

        npc.transform.LookAt(playerTransform.position);
        npc.transform.Translate(Vector3.forward * 10* Time.deltaTime);
        AnimationExcuting.instance.anim.SetBool("Walk", true);
    }

    public override void Reason(GameObject npc)
    {
        if (GameObject.FindGameObjectWithTag(Tags.enemy) == null)
        {
            fsm.PerformTransition(Transition.LostPlayer);
            return;
        }
       if (Vector3.Distance(playerTransform.position, npc.transform.position) > 60&& Vector3.Distance(playerTransform.position, npc.transform.position)<10)
        {
            fsm.PerformTransition(Transition.LostPlayer);
        }
         if(Vector3.Distance(playerTransform.position, npc.transform.position) <= 10)
        {
           
            fsm.PerformTransition(Transition.FindEnemy);
        }
       

    }
}
