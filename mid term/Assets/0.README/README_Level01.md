━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

📖 상황 설명

당신은 2D 플랫포머 게임 프로젝트를 인수받았습니다. 하지만 씬을 열어보니 여러 렌더링 문제가 발생하고 있고, 일부 GameObject는 아직 생성되지 않았습니다. 이 씬을 올바르게 수정하고 완성해야 합니다.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

📦 제공된 프로젝트 상태

✅ 이미 배치된 GameObject
   • Player
   • Enemy
   • Sky
   • Ground

❌ 문제점
   • Player,Enemy,Ground가 Sky(하늘 배경) 뒤에 가려져 보이지 않음
   • Player,Enemy 스프라이트가 흐릿하게 보임 (픽셀아트인데 blur 처리된 것처럼)
   • Ground의 스프라이트가 좌우 반전되어야 하는데 정상으로 보임

⚠️ 미완성
   • Tree GameObject 생성 필요

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

✨ 세부 요구사항

1️⃣ 렌더링 우선순위 문제 해결 (1.5점)

   🔴 Layer 이름은 Background, Character, Foreground로 설정할 것 (이름이 다를 경우 반영안됨)

   🔍 Player,Enemy,Ground 가시성 문제 해결 (0.5점)
      → Player,Enemy,Ground가 Sky 앞에 보이도록 설정 수정
      → Player,Enemy가 Ground 앞에 보이도록 설정 수정

   🔍 Player,Enemy 스프라이트 선명도 문제 해결 (1점)
      → Player,Enemy가 선명한 픽셀아트로 보이도록 스프라이트 설정 수정

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

2️⃣ Sprite 반전 및 추가 GameObject 생성 (2점)

   🔄 Ground 좌우 반전 (0.5점)
      → Ground 스프라이트를 좌우 반전

   🔄 Tree, Bush 크기 조절 (0.5점)
      → Transform Scale 조절이 아닌, 픽셀아트에 알맞은 방법으로 조절

   🌳 Tree GameObject 생성 (0.5점)
      • SunnyLand 에셋의 tree 스프라이트 사용 (SunnyLand/Environment/props/tree)
      • Position: (7, -0.9, 0)
      • Player,Enemy와 같은 공간에 있지만 Player,Enemy보다 뒤에 보여야 함

   🌳 Bush GameObject 생성 (0.5점)
      • SunnyLand 에셋의 bush 스프라이트 사용 (SunnyLand/Environment/props/bush)
      • Position: (-1.7, -0.12, 0)
      • Player와 같은 공간에 있지만 Player보다 뒤에 보여야 함

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

3️⃣ 최종 렌더링 순서 확인 (0.5점)

씬의 GameObject들이 다음 순서로 렌더링되어야 합니다 (뒤에서 앞으로):

   1. 🌌 Sky (가장 뒤)
   2. 🟫 Ground
   3. 🌳 Tree
   4. 🧑 Player
   5. 👾 Enemy
   6. 🌳 Bush

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
