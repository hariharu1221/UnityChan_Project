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
    public int isJump = 0;
    private bool isBorder;
    private bool ableJump = true;
    private bool ableRun = true;
    public bool isAttack = false;

    public static Player Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    Vector3 LastPos;
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        LastPos = transform.position;
    }

    void Update()
    {
        KeyInput();

        if (transform.position.y <= -5) transform.position = Vector3.zero;
    }

    private void LateUpdate()
    {
        Vector2 mPos = new Vector2(transform.position.x - LastPos.x, transform.position.z - LastPos.z);
        if (mPos.magnitude / Time.deltaTime < 0.3f)
            SetMoveFalse();
        LastPos = transform.position;
    }

    public float nowSpeed = 0;

    public void SetRotationY(float y, float rotationSpeed = -1)
    {
        Quaternion rotation = Quaternion.Euler(0, y, 0);
        if (rotationSpeed <= -1)
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * this.rotationSpeed);
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    void KeyInput()
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

    void StopToWall()
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

        if (isBorder) Debug.Log("true");
    }

    void SkillKey()
    {
        if (Input.GetMouseButtonDown(0) && attackCheck == 0)
            Attack();
        else if (Input.GetKeyDown(KeyCode.E))
            E();
        else if (Input.GetKeyDown(KeyCode.R))
            R();
    }


    public int attackCheck = 0;
    void Attack()
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

    public void E()
    {
        transform.Translate(0, 0.1f, 1);
        EffectManager.Instance.PlayEffectOnce(0, transform.position, transform.rotation.eulerAngles, 0.8f);
        this.rigid.AddRelativeForce(new Vector3(0, 0, 1) * (speed * 3.5f + 1) * 50, ForceMode.Impulse);
    }

    public void R()
    {
        anim.SetTrigger("doEx");
        StopAllCoroutines();
        StartCoroutine(Ex());
    }

    public bool isEx = false;
    IEnumerator Ex()
    {
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

    void MoveKey()
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

    public void Jump()
    {
        if (isJump != 0 || ableJump == false) return;
        isJump = 1;
    }

    public void RunSpeed()
    {
        if (ableRun == false) return;
        nowSpeed = speed * 3.5f;
        anim.SetBool("isRun", true);
    } //뛰는 스피드로

    public void WalkSpeed()
    {
        nowSpeed = speed;
        anim.SetBool("isRun", false);
    } //걷는 스피드로

    public void RotationMove(float value, float speed) //캐릭터 방향 및 앞으로 움직임
    {
        SetRotationY(value);
        if (isBorder || isAttack)
        {
            SetMoveFalse();
            return;
        }
        float translation = speed;
        translation *= Time.deltaTime;
        //rigid.velocity = new Vector3(0, 0, translation);
        rigid.MovePosition(rigid.position * (1 + translation));
        transform.Translate(0, 0, translation);
        anim.SetBool("isMove", true);
    }

    void Jumping()
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

    public void SetMoveFalse()
    {
        anim.SetBool("isMove", false);
        anim.SetBool("isRun", false);
        //nowSpeed = 0;
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

    public Transform getTransform()
    {
        return transform;
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