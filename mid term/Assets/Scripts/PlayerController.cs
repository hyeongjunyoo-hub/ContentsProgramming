
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5.0f;
    [Header("점프 설정")]
    public float jumpForce = 8.0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isGrounded = false;  // 새로 추가!
    private int score = 0;
    private UnityEngine.Vector3 startPosition;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        Debug.Log("시작 위치 저장:" + startPosition);
        
        // 디버그: 제대로 찾았는지 확인
        if (animator != null)
        {
            Debug.Log("Animator 컴포넌트를 찾았습니다.");
        }
        else
        {
            Debug.LogError("Animator 컴포넌트가 없습니다.");
        }
        
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer 컴포넌트가 없습니다. 2D 캐릭터에 추가해야 합니다.");
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 없습니다! Player에 추가하세요.");
        }
    }

    void Update()
    {
        // 입력 감지
        float moveX = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
            spriteRenderer.flipX = true;  // 왼쪽
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
            spriteRenderer.flipX = false;   // 오른쪽
        }
        float currentMoveSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentMoveSpeed = moveSpeed * 2f;
            Debug.Log("달리기 모드 활성화!");
        }



        // 물리 기반 이동 (새로운 방식!)
        rb.linearVelocity = new UnityEngine.Vector2(moveX * currentMoveSpeed, rb.linearVelocity.y);

        if (animator != null)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {

                animator.SetTrigger("Jump");
                rb.linearVelocity = new UnityEngine.Vector2(rb.linearVelocity.x, jumpForce);
                Debug.Log("점프!");
            }
        }
        // 애니메이션 제어
        float currentSpeed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", currentSpeed);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
	{
        // 충돌한 오브젝트가 "Ground" Tag를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("바닥에 착지!");
            isGrounded = true;
        }
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("장애물 충돌! 시작 지점으로 돌아갑니다.");
            transform.position = startPosition;
            rb.linearVelocity = new UnityEngine.Vector2(0f,0f);
            

        }
	}


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("바닥에서 떨어짐");
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            score++;
            Debug.Log("코인 획득! 현재점수:" + score);
            Destroy(other.gameObject);  // 코인 제거
        }
    }
    
}