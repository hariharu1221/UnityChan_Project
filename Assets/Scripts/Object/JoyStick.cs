//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
//{
//    [SerializeField] RectTransform m_rectBack;
//    [SerializeField] RectTransform m_rectJoystick;

//    float m_fRadius;
//    bool m_bTouch = false;

//    void Awake()
//    {
//        m_rectBack = GameObject.Find("JoyStickBack").GetComponent<RectTransform>();
//        m_rectJoystick = GameObject.Find("JoyStickBack/JoyStick").GetComponent<RectTransform>();

//        // JoystickBackground의 반지름입니다.
//        m_fRadius = m_rectBack.rect.width * 0.5f;
//    }

//    void Update()
//    {
//        if (m_bTouch)
//        {
//            if (fSqr > 0.55f)
//                Player.Instance.RunSpeed();
//            else
//                Player.Instance.WalkSpeed();

//            Player.Instance.SetRotationY(RotateY, 0.3f);
//            if (Player.Instance.isJump == 0) Player.Instance.RotationMove(RotateY, Player.Instance.nowSpeed);
//        }
//    }

//    float fSqr;
//    float RotateY;
//    void OnTouch(Vector2 vecTouch)
//    {
//        Vector2 vec = new Vector2(vecTouch.x - m_rectBack.position.x, vecTouch.y - m_rectBack.position.y);
//        // vec값을 m_fRadius 이상이 되지 않도록 합니다.
//        vec = Vector2.ClampMagnitude(vec, m_fRadius);
//        m_rectJoystick.localPosition = vec;
//        // 조이스틱 배경과 조이스틱과의 거리 비율로 이동합니다.
//        fSqr = (m_rectBack.position - m_rectJoystick.position).sqrMagnitude / (m_fRadius * m_fRadius);
//        // 터치위치 정규화
//        Vector2 vecNormal = vec.normalized;

//        RotateY = Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg;
//    }

//    public void OnDrag(PointerEventData eventData)
//    {
//        OnTouch(eventData.position);
//        m_bTouch = true;
//    }

//    public void OnPointerDown(PointerEventData eventData)
//    {
//        OnTouch(eventData.position);
//        m_bTouch = true;
//    }

//    public void OnPointerUp(PointerEventData eventData)
//    {
//        // 원래의 값으로 되돌림
//        Player.Instance.ResetAnimMove();
//        fSqr = 0;
//        m_rectJoystick.localPosition = Vector2.zero;
//        m_bTouch = false;
//    }
//}