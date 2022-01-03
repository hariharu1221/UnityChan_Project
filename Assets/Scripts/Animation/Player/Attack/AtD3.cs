using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtD3 : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player player = PlayerDataManager.Instance.PlayerData.Player;
        player.isAttack = true;
        Transform transform = animator.transform;
        Vector3 pos = transform.position + transform.forward * 1.0f + transform.up * 0.3f;
        EffectManager.Instance.PlayEffectOnce(5, pos, transform.rotation.eulerAngles, 0.8f, 0.2f);
        transform.Translate(0, 0.05f, 0.4f);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player player = animator.GetComponent<Player>();
        player.isAttack = false;
        player.GetComponent<Animator>().ResetTrigger("ExitAttack");
    }
}
