━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

📖 상황 설명

당신은 플랫포머 게임을 제작 중이며, 물리 기반 이동 시스템과 기본 점프만 가능한 캐릭터가 특수 아이템을 먹으면 더블 점프가 가능해지는 시스템을 만들어야 합니다. 프로그래머가 PlayerController_v2.cs 템플릿만 남겨두고 퇴근했습니다.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

📦 제공된 프로젝트 상태

✅ 이미 생성된 것
   • Player GameObject (Sprite Renderer 포함)
   • Ground GameObject (Platform)
   • DoubleJumpItem GameObject (Sprite Renderer 포함)

❌ 구현되지 않은 것
   • Player의 Rigidbody2D, Collider2D 컴포넌트
   • Ground의 Collider2D 컴포넌트
   • Tag 설정
   • PlayerController_v2.cs
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

✨ 세부 요구사항

1️⃣ PlayerController_v2 물리 기반 이동 시스템 구현 (2점)

   🔴 스크립트 이름 동일하게.

   ⚙️ 물리 및 충돌 설정 (0.5점)
      → Player가 중력의 영향을 받아 아래로 떨어지도록 설정
      → Player가 Ground와 물리적으로 충돌하도록 설정
      → Player가 회전하지 않도록 설정

   ⌨️ A/D 키 물리 기반 좌우 이동 구현 (0.5점)
      → Rigidbody2D를 활용한 물리 기반 이동 (Transform 사용 ❌)
      → 캐릭터 방향 전환 필수

   🎮 기본 점프 구현 (1점)
      → Space 키를 눌렀을 때 점프
      → 공중에 떠있을 때는 점프 불가능
      → Ground에 착지하면 다시 점프 가능

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

2️⃣ 더블점프 아이템 시스템 (1점)

   🎁 아이템 충돌 처리 및 더블 점프 활성화 (1점)
      → Player가 DoubleJumpItem과 닿으면 아이템 사라짐
      → 아이템 획득 후 공중에서 한 번 더 점프 가능

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
