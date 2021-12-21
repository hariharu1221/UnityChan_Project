using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpEnd : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("isJump", 2);
    }
}
