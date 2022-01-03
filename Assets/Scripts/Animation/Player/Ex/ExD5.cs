using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExD5 : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform transform = animator.transform;
        Vector3 pos = transform.position + transform.forward * -1+ transform.up * 1.6f;
        EffectManager.Instance.PlayEffectOnce(9, pos, transform.rotation.eulerAngles - new Vector3(0, 0, -90), 0.8f);

        transform.Translate(0, 0.1f, 1);
        EffectManager.Instance.PlayEffectOnce(0, transform.position, transform.rotation.eulerAngles, 0.8f);

        PlayerDataManager.Instance.PlayerData.Rigid.AddRelativeForce(new Vector3(0, 0, 1) * (6f + 1) * 50, ForceMode.Impulse);
    }
}
