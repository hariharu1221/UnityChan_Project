using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    Button jumpButton;
    Button E;
    Button R;

    private void Awake()
    {
        jumpButton = canvas.transform.Find("JumpButton").GetComponent<Button>();
        jumpButton.onClick.AddListener(() => Player.Instance.Jump());
        E = canvas.transform.Find("E").GetComponent<Button>();
        E.onClick.AddListener(() => Player.Instance.E());
        R = canvas.transform.Find("R").GetComponent<Button>();
        R.onClick.AddListener(() => Player.Instance.R());
    }
}
