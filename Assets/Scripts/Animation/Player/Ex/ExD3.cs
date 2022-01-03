using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExD3 : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform transform = animator.transform;
        Vector3 pos = transform.forward * 1.0f + transform.up * 1.6f;
        EffectManager.Instance.PlayEffectOnce(5, transform, pos, 0.8f);
        EffectManager.Instance.PlayEffectOnce(4, transform, pos, 0.8f, 0.5f);
    }
}
