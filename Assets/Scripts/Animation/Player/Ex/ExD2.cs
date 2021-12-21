using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExD2 : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform transform = Player.Instance.getTransform();
        Vector3 pos = transform.position + transform.forward * 1.0f + transform.up * 1.6f;
        EffectManager.Instance.PlayEffectOnce(4, pos, transform.rotation.eulerAngles, 0.8f);
    }
}
