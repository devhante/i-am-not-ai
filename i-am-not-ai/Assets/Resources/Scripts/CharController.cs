using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class CharController : NetworkBehaviour
{
    public static CharController Instance;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject AttackBox;

    //수동 회전 속도
    [SerializeField] private float RotateSpeed = 1.0f;

    //앞으로 달리기 및 걷기와 뒷걸음의 속도
    [SerializeField] private float forwardRunSpeed = 7.0f;
    [SerializeField] private float forwardSpeed = 3.0f;
    [SerializeField] private float sideSpeed = 2.0f;
    [SerializeField] private float backwardSpeed = 2.0f;

    //애니매이션 재생속도
    [SerializeField] private float animSpeed = 1.5f;

    private Animator anim;

    //캐릭터를 움직일 벡터
    private Vector3 velocity;

    //카메라 -> 캐릭터 선회 여부
    private bool C_rotate;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        C_rotate = true;
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            Instance = this;
        }
        if (!isLocalPlayer)
            mainCamera.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            //현재 정의된 입력으로 되어있으나, UI가 나오면 UI로 변경한다
            //Vector2 joystic_Input = PlayUI.Instance.GetMoveJoystickValue();

            //float input_h = joystic_Input.x;
            //float input_v = joystic_Input.y;

            float input_h = Input.GetAxis("Horizontal");
            float input_v = Input.GetAxis("Vertical");

            anim.SetFloat("Speed", input_v);

            //혹시 옆으로만 가고 있을 때에도 애니에 속도를 넣어줘야 해서.(수정예정)
            if (Mathf.Abs(input_v) <= 0.1f)
                anim.SetFloat("Speed", Mathf.Abs(input_h));

            anim.SetFloat("Direction", input_h);

            anim.speed = animSpeed;

            //애니매이션 상태 초기화
            anim.SetBool("Run", false);

            #region Move

            velocity = new Vector3(input_h, 0, input_v);
            //다음과 같이 카메라에 맞추어 수정하는 것이 적합하다고 본다

            #region Rotate_Look
            if (C_rotate)
            {
                Vector3 forward = mainCamera.transform.TransformDirection(Vector3.forward);
                forward.y = 0;
                var lookDirection = Quaternion.LookRotation(forward);

                Vector3 euler = new Vector3(0, lookDirection.y, 0);

                transform.rotation = lookDirection;

                //로컬 방향에 맞게 벨로시티를 수정
                velocity = mainCamera.transform.TransformDirection(velocity);
            }
            #endregion Rotate_Look

            //입력에 따라 속도의 변화를 준다
            if (Mathf.Abs(input_v) > 0.1f || Mathf.Abs(input_h) > 0.1f)
            {
                //뒤로 가고 있으면 어찌 되었건 뒤로가는 속도가 나오는 것으로 했다
                if (input_v < -0.1f)
                    velocity *= backwardSpeed;

                //만약 옆으로 움직인다면
                else if (Mathf.Abs(input_h) > 0.1f)
                {
                    //앞과 옆의 비율을 계산하여 옆속도 ~ 앞속도 만큼 움직인다
                    velocity *= Mathf.Lerp(sideSpeed, forwardSpeed, input_v / (1 + Mathf.Abs(input_h)));
                }

                //앞으로만 가고 있다면
                else
                {
                    //앞을 향하는 임계에 있을 경우만 달리기를 체크한다
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        anim.SetBool("Run", true);
                        velocity *= forwardRunSpeed;
                        anim.SetFloat("Speed", input_v * 2);
                    }
                    else
                        velocity *= forwardSpeed;
                }
            }

            //연산이 끝난 벡터를 로컬포지션에서 더한다
            velocity.y = 0;
            transform.localPosition += velocity * Time.fixedDeltaTime;

            #endregion Move

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (C_rotate == true)
                    SetLookRotate(false);
                else if (C_rotate == false)
                    SetLookRotate(true);
            }

            //수동 로테이트 조정
            if (Input.GetKey(KeyCode.T))
            {
                this.transform.Rotate(0, -RotateSpeed, 0);
            }
            if (Input.GetKey(KeyCode.Y))
            {
                this.transform.Rotate(0, RotateSpeed, 0);
            }
        }
    }


    private void SetLookRotate(bool value)
    {
        C_rotate = value;
    }

    private void Attack()
    {
        if (isLocalPlayer)
        {
            if (!anim.GetBool("Attack"))
                StartCoroutine("Attack_Couroutine");
        }
        else
        {

        }
    }

    private IEnumerator Attack_Couroutine()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.3f);
        AttackBox.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        AttackBox.SetActive(false);
        anim.SetBool("Attack", false);
    }
}
