using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtD2 : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.isAttack = true;
        Transform transform = Player.Instance.getTransform();
        Vector3 pos = transform.position + transform.forward * 1.0f + transform.up * 0.3f;
        EffectManager.Instance.PlayEffectOnce(4, pos, transform.rotation.eulerAngles, 0.8f, 0.2f);
        transform.Translate(0, 0.05f, 0.4f);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.isAttack = false;
        Player.Instance.GetComponent<Animator>().ResetTrigger("ExitAttack");
    }
}
