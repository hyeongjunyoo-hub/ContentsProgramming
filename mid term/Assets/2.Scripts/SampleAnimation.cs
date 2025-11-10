using UnityEngine;

public class PlayerController_v3 : MonoBehaviour
{
    // 이동 변수
    public float moveSpeed = 7f; 
    public float jumpForce = 12f; // 현재 적용되는 점프 힘
    
    // 점프 강화 시스템을 위한 기본 점프 힘
    private float baseJumpForce;

    // 내부 컴포넌트 참조
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // 점프 및 땅 착지 관련 상태 변수
    private bool isGrounded = false;

    // 게임 클리어 시스템 변수
    public Transform goalPosition; // Goal GameObject의 Transform을 Inspector에서 할당
    private int coinsCollected = 0;
    private const int REQUIRED_COINS = 3; // 클리어에 필요한 코인 개수

    void Awake()
    {
        // 컴포넌트 참조
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 점프력 기본값 설정 (JumpBoost 시스템을 위해 초기값을 저장)
        baseJumpForce = jumpForce; 
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D 컴포넌트가 Player에 없습니다. 물리 기반 이동을 위해 반드시 추가해야 합니다.");
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer 컴포넌트가 Player에 없습니다. 캐릭터 방향 전환을 위해 추가해야 합니다.");
        }
    }

    void Update()
    {
        // Space 키를 눌렀을 때 점프
        HandleJumpInput();
    }

    void FixedUpdate()
    {
        // 물리 기반 이동
        float horizontalInput = Input.GetAxis("Horizontal");
        HandleMovement(horizontalInput);
        FlipCharacter(horizontalInput);
    }

    // Rigidbody2D를 사용한 좌우 이동 (A: 오른쪽, D: 왼쪽으로 변경)
    void HandleMovement(float horizontalInput)
    {
        if (rb != null)
        {
            // 여기서 -1을 곱하여 이동 방향을 반전시킵니다.
            // A키(입력 -1) -> (-1 * 7) -> (X축 +7) -> 오른쪽 이동
            // D키(입력 +1) -> ( 1 * -7) -> (X축 -7) -> 왼쪽 이동
            rb.linearVelocity = new Vector2(-horizontalInput * moveSpeed, rb.linearVelocity.y);
        }
    }

    // 캐릭터 방향 전환
    void FlipCharacter(float horizontalInput)
    {
        // 참고: 이동 방향이 반전되더라도, 캐릭터 방향 전환은 Input.GetAxis 값 자체를 따르는 것이 일반적입니다.
        // (A를 누르면 왼쪽을 보고, D를 누르면 오른쪽을 보는 것이 자연스럽습니다.)
        if (spriteRenderer != null)
        {
            if (horizontalInput > 0.01f) // D를 누르면 (오른쪽 입력)
            {
                spriteRenderer.flipX = false;
            }
            else if (horizontalInput < -0.01f) // A를 누르면 (왼쪽 입력)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    // 기본 점프 구현
    void HandleJumpInput()
    {
        if (rb == null) return;

        // Space 키를 눌렀고, 땅에 닿아있을 때만 점프 가능
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false; // 공중 상태로 전환
            
            // 점프할 때 어떤 점프력이 적용되었는지 콘솔에 출력
            Debug.Log("점프! (현재 점프력: " + jumpForce + ")");
        }
    }

    // Ground 또는 JumpBoostPlatform과의 충돌 감지 (2. 점프 강화 시스템)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("JumpBoostPlatform"))
        {
            isGrounded = true;

            // JumpBoostPlatform에 착지할 때 점프력 2배 증가 (1.5점)
            if (collision.gameObject.CompareTag("JumpBoostPlatform"))
            {
                jumpForce = baseJumpForce * 2f;
                Debug.Log("점프 부스트 플랫폼에 착지! 점프력이 2배로 증가합니다.");
            }
            else // Ground 태그일 경우 (혹은 다른 일반 플랫폼)
            {
                // 기존 점프력으로 복구
                jumpForce = baseJumpForce;
            }
        }
    }
    
    // 코인 아이템 충돌 처리 및 게임 클리어 시스템 (3, 4. 코인 및 골)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 코인 수집 처리 (0.5점)
        if (other.CompareTag("Coin"))
        {
            coinsCollected++;
            Destroy(other.gameObject);
            
            Debug.Log($"코인 획득! ({coinsCollected}/{REQUIRED_COINS}개)");

            // 게임 클리어 조건 확인 (4. 게임 클리어 시스템)
            if (coinsCollected >= REQUIRED_COINS)
            {
                TeleportToGoal();
            }
        }
    }

    // 골 위치로 순간이동 (4. 게임 클리어 시스템)
    void TeleportToGoal()
    {
        if (goalPosition != null)
        {
            // Rigidbody의 속도 초기화 (순간이동 시 잔여 속도 방지)
            if(rb != null) rb.linearVelocity = Vector2.zero;
            
            // 골 위치로 순간이동
            transform.position = goalPosition.position;
            
            Debug.Log("게임 클리어! Goal 지점으로 순간이동했습니다.");
            // (TODO: 게임 클리어 UI나 다음 씬 로드 등의 최종 로직을 여기에 추가합니다.)
        }
        else
        {
            Debug.LogError("Goal 위치(Transform)가 PlayerController_v3 스크립트에 할당되지 않았습니다!");
        }
    }
}