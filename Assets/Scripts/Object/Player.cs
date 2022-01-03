using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float speed = 0.7f;
    [SerializeField] private float rotationSpeed = 5f;

    List<DoubleKeyCode> doubleKeys = new List<DoubleKeyCode>()
    {
        new DoubleKeyCode(KeyCode.W, KeyCode.A, 315),
        new DoubleKeyCode(KeyCode.W, KeyCode.D, 45),
        new DoubleKeyCode(KeyCode.S, KeyCode.A, 225),
        new DoubleKeyCode(KeyCode.S, KeyCode.D, 135),
        new DoubleKeyCode(KeyCode.W, KeyCode.S, 0),
        new DoubleKeyCode(KeyCode.A, KeyCode.D, 0),
        new DoubleKeyCode(KeyCode.W, KeyCode.W, 0),
        new DoubleKeyCode(KeyCode.A, KeyCode.A, 270),
        new DoubleKeyCode(KeyCode.S, KeyCode.S, 180),
        new DoubleKeyCode(KeyCode.D, KeyCode.D, 90)
    };

    private Animator anim;
    private Rigidbody rigid;
    private bool isBorder;
    private bool ableJump = true;
    private bool ableRun = true;
    private int isJump = 0;
    public bool isAttack = false;

    Vector3 LastPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        LastPos = transform.position;
    }

    private void Update()
    {
        KeyInput();
        if (transform.position.y <= -5) transform.position = Vector3.zero;
    }

    private void LateUpdate()
    {
        Vector2 mPos = new Vector2(transform.position.x - LastPos.x, transform.position.z - LastPos.z);
        if (mPos.magnitude / Time.deltaTime < 0.3f)
            ResetAnimMove();
        LastPos = transform.position;
    }

    private float nowSpeed = 0;

    private void SetRotationY(float y, float rotationSpeed = -1)
    {
        Quaternion rotation = Quaternion.Euler(0, y, 0);
        if (rotationSpeed <= -1)
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * this.rotationSpeed);
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    private void KeyInput()
    {
        MoveKey();
        Jumping();
        SkillKey();
    }

    private void FixedUpdate()
    {
        rigid.angularVelocity = Vector3.zero;
        StopToWall();
    }

    private void StopToWall()
    {
        Vector3 foward = transform.forward + new Vector3(0, 1.2f, 0);
        Vector3 pos = transform.position + new Vector3(0, 0.2f, 0);

        Debug.DrawRay(pos + transform.right * 0.15f, foward * 0.5f, Color.green);
        Debug.DrawRay(pos, foward * 0.5f, Color.green);
        Debug.DrawRay(pos + transform.right * -0.15f, foward * 0.5f, Color.green);

        for (int i = 0; i < 3; i++)
        {
            if (i == 0) isBorder = Physics.Raycast(pos + transform.right * 0.1f, foward, 0.5f, LayerMask.GetMask("Block"));
            if (i == 1) isBorder = Physics.Raycast(pos, foward, 0.5f, LayerMask.GetMask("Block"));
            if (i == 2) isBorder = Physics.Raycast(pos + transform.right * -0.1f, foward, 0.5f, LayerMask.GetMask("Block"));

            if (isBorder) break;
        }
    }

    private void SkillKey()
    {
        if (Input.GetMouseButtonDown(0) && attackCheck == 0)
            Attack();
        else if (Input.GetKeyDown(KeyCode.E))
            E();
        else if (Input.GetKeyDown(KeyCode.R))
            R();
    }

    private int attackCheck = 0;
    private void Attack()
    {
        if (isEx) return;
        StartCoroutine(attackWait(0.4f));
    }

    IEnumerator attackWait(float time)
    {
        attackCheck++;
        if (attackCheck == 1)
        {
            anim.Play("Attack_1");
        }
        if (attackCheck == 3)
        {
            yield return new WaitForSeconds(0.7f);
            attackCheck = 0;
            yield break;
        }
        
        bool check = false;
        float cool = 0;
        yield return new WaitForSeconds(0.2f);
        while (cool < time)
        {
            cool += Time.deltaTime;
            if (Input.GetMouseButton(0) && check == false)
            {
                anim.SetTrigger("doAttack");
                check = true;
            }
            yield return new WaitForFixedUpdate();
        }
        if (check)
        {
            yield return StartCoroutine(attackWait(time));
        }
        else
        {
            anim.SetTrigger("ExitAttack");
            attackCheck = 0;
        }
    }

    private void E()
    {
        transform.Translate(0, 0.1f, 1);
        EffectManager.Instance.PlayEffectOnce(0, transform.position, transform.rotation.eulerAngles, 0.8f);
        this.rigid.AddRelativeForce(new Vector3(0, 0, 1) * (speed * 3.5f + 1) * 50, ForceMode.Impulse);
    }

    private void R()
    {
        StopAllCoroutines();
        StartCoroutine(Ex());
    }

    private bool isEx = false;
    IEnumerator Ex()
    {
        anim.SetTrigger("doEx");
        isEx = true;
        ableJump = false;
        ableRun = false;
        WalkSpeed();
        yield return new WaitForSeconds(2.7f);
        attackCheck = 0;
        ableJump = true;
        ableRun = true;
        isEx = false;
    }

    private void MoveKey()
    {
        if (Input.GetKey(KeyCode.LeftShift)) RunSpeed();

        if (isJump != 0) return;
        
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        foreach (var list in doubleKeys)                        
            if (Input.GetKey(list.keyOne) && Input.GetKey(list.keyTwo))
            {
                RotationMove(list.value, nowSpeed);
                return;
            }

        nowSpeed = speed;
    }

    private void Jump()
    {
        if (isJump != 0 || ableJump == false) return;
        isJump = 1;
    }

    private void RunSpeed()
    {
        if (ableRun == false) return;
        nowSpeed = speed * 3.5f;
        anim.SetBool("isRun", true);
    } //뛰는 스피드로

    private void WalkSpeed()
    {
        nowSpeed = speed;
        anim.SetBool("isRun", false);
    } //걷는 스피드로

    private void RotationMove(float value, float speed) //캐릭터 방향 및 앞으로 움직임
    {
        SetRotationY(value);
        if (isBorder || isAttack)
        {
            ResetAnimMove();
            return;
        }
        float translation = speed;
        translation *= Time.deltaTime;
        //rigid.velocity = new Vector3(0, 0, translation);
        rigid.MovePosition(rigid.position * (1 + translation));
        transform.Translate(0, 0, translation);
        anim.SetBool("isMove", true);
    }

    private void Jumping()
    {
        foreach (var list in doubleKeys)
            if (Input.GetKey(list.keyOne) && Input.GetKey(list.keyTwo))
                SetRotationY(list.value, 0.3f);
        
        if (isJump == 1)
        {
            anim.SetInteger("isJump", 1);
            anim.SetTrigger("doJump");

            this.rigid.velocity = Vector3.zero;
            this.rigid.AddForce(transform.up * jumpForce, ForceMode.Force);
            isJump = 2;
        }
        else if (isJump == 2)
        {
            anim.SetInteger("isJump", 2);
            float translation = nowSpeed;
            translation *= Time.smoothDeltaTime;
            transform.Translate(0, 0, translation);
        }
    }

    private void ResetAnimMove()
    {
        anim.SetBool("isMove", false);
        anim.SetBool("isRun", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJump = 0;
        anim.SetInteger("isJump", 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        isJump = 0;
    }
}

public class DoubleKeyCode
{
    public KeyCode keyOne;
    public KeyCode keyTwo;
    public float value;

    public DoubleKeyCode(KeyCode keyOne, KeyCode keyTwo, float value)
    {
        this.keyOne = keyOne;
        this.keyTwo = keyTwo;
        this.value = value;
    }
}

public class OneKeyCode
{
    public KeyCode key;
    public float value;

    public OneKeyCode(KeyCode key, float value)
    {
        this.key = key;
        this.value = value;
    }
}


//StopToWall() Exist로 수정 할것
//