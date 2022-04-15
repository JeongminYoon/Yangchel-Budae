# Yangchel-Budae
YSU 2022 unity game project

----개발환경-----
Unity 2020.3.30f1
Visual Studio 2019

-----구현예정기능-----
[UI] - 윤정민
(인게임씬)
카드 덱 시스템 - 21:31 2022-3-30 완성

덱 시스템하고 UI 연동 - 2022-4-01 완성

유닛/타워 체력바 - 21:02 2022-4-15 100% 완성 - HpBarManager.HpBarInstrate() 함수,  HpBar 클래스의 HpBarSetting(CurHp, FulHp, UnitPos, bool isEnemy) 함수 를 이용하시면 됩니당.

자원바 - 2022-4-01 완성 ,21:02 2022-4-15 시연용 에셋 연동작업完

다 기능 구현 완료하고 나면 유닛이랑 머지 - 2022-4-04 90% 완성

씬 트리 제작 - 21:02 2022-4-15 기능부 구현完, 시연용 에셋 연동작업完(프로젝트 종료시 에셋이 개선될수 있음)

씬에 띄울 현재 카드 선택창 제작과 덱 시스템 연동 - 2022-4-10 완성. 스크롤 기능등 세부적인 기능은 마지막주에 구현예정. ,21:02 2022-4-15 시연용 에셋 연동작업完

그 후 UI 애니메이션 - 2022-4-04 작업중  ,21:02 2022-4-15 시연용 에셋 연동작업중(30%)

메인메뉴 > 카드셀렉트씬 > 인게임씬 > 결과창 씬트리제작 - 2022=4-13 50% 작업중
ㄴ 씬화면 가변해상도 적용, 에셋 적용 시키고 결과창 씬 제작하기.

--------------------------------------------------------
[유닛] - 전근희
체력시스템
공격시스템
스크립트로 이동 구현하기
ㄴ 스크립터블 오브젝트 O
ㄴㄴ status, 기능부() 빼서 따로 제작하기 O
08:05 2022-3-21 적군생성, 아군생성 테스트 하면서 각 리스트 만들어서 서치기능 테스트 하기 위해 밑 작업중
			=> 서치해서 가까운 타겟 정해지면 공격, 체력 닳는거 까지 테스트 해보기
06:50 2022-3-24 	1. 유닛 타겟Obj에서 유닛 죽을 때 상호참조하던 문제 유닛매니저하고 델리게이트 사용해서 해결완료
		2. 스크립터블 옵젝 빼놓은거 그대로 쓰면 각각 새로 생성하는게 아니라 공유하고 진짜 값까지 바뀌는 문제 발생 => 깊은 복사 이용해서 해결
			=> 유닛끼리의 타겟 설정, 공격, 사망은 잘됨.
		차후 할일 : 타워 클래스, 타워 매니저 만들어서 타워까지 포함된 전투 완성하면 됨.
				=> 지금 타워든 유닛이던 중심부(피봇)위치 기준 거리재기를 하고 있어서 타워에 씹히는 문제 collider 크기 받아와서 거리 계산에포함시키면 될듯.
				=> 그 후 에셋 파일들로 매쉬 교체하는거 + 애니메이션 블랜딩이나 타이밍등 잡기
15:38 2022-3-27	타워 매니저로 타워 생성 코드 완료
16:25 2022-3-27	타워도 유닛 서치 후 공격 까지 함. 타워 사망 제작 시작
16:57 2022-3-27	타워 죽으면 유닛이 넥서스로 타겟 바꾸는거 까지 완료 
			=> 이후 할것
				ㄴ1. 거리재기할때 Pos.y 0으로 두는거
				ㄴ2. Collider 크기 받아와서 거리잴때 Magnitude(Vec(B) - Vec(A)) - (A.col.len + B.col.len) 포함 시키기
				ㄴ3. 에셋 매쉬 넣기전에 코드 정리 한번 하기
						=> 총알이라던가 태그라던가 

14:03 2022-03-28 목표 타겟 찾을때 pos.y 세팅해줘서 유닛이 포탑으로 갈때 하늘 안날아감.
			c# 에서는 비교연산자가 항등연산자보다 우선순위가 높음, 관계연산자는 산술연산자 보다 우선순위가 낮다.

16:51 2022-3-30	해야할거 : 현준이한테 타워 생성 함수 만들어 주고 관련해서 정리해야함. => 회의 끝나고 타워 스킬 사용에 대해 좀더 정해지면 ㄱㄱ
			1.유닛 생성 할때 타워나 다른 오브젝트에 마우스 올려서 생성되면 그 옆에 생성되게 하거나 아예 생성이 안되게 하기 -> 생성 안되게(카드 돌아가게)
				ㄴ22:58 2022-3-30 완료
			2.탱커, 힐러 제작
			2.5 밀리2티어, 렌쥐2티어 제작
			3.콜리더 크기 받아와서 거리재기에 포함시키기
				=> 이제 유닛, 타워, 넥서스 모두 콜리더, 태그, 이름정리하기 
01:14 2022-3-31	폴더정리, enum 정리, 
		UnitFactory 정리완료
		탱커 제작 완료
		미리2티어, 렌쥐2티어 제작 완료
		내씬에 TestScript Update안에 보면 적유닛, 우리팀 유닛 소환 편하게 하는 거 있음 필요한 사람덜 쓰삼

23:08 2022-4-3	힐러 제작 해야함. 
		타워 생성 -> 하이머딩거 포탑처럼 (유닛 판정으로 들어가야함) 
			=> 타워 매니저 말고 유닛매니저에서 관리 될 수 있도록. 그치만 사용은 유닛처럼....? 
				=> 근데 어그로 빠지는건 타워처럼 들어가야함.
			=> 유닛 상속 중이라서 그냥 태그만 다르게 해줘도 될듯...?
		다 시마이 치고
			1. Unit클래스 상속받는 Tower 지금 현재 그대로 사용할지
			2. Unit클래스 상속받는 Skill용 Tower 컴포넌트를 만들지
		SkillManager에서 생성/관리 함 => Unit쪽에서 SearchTower 수정 필요(타워 매니저에서 검색하기 전에 스킬매니저의 타워도 확인해보는 방식으로.)
		현준이한테 Unit 클래스 그대로 설명 해줘야함. {상속, virtual 키워드, override 키워드(가상함수, 함수 재정의)}

03:49 2022-4-4	힐러 투사체 던지는건 됐음.
		    해야할거 
			힐러 서치 유닛 예외 조건 처리
			유닛 콜리더 크기 받아오는 함수(공격, 걷기에서 적용)
			애들한테 유닛 클래스 설명해주고 나서 스킬 매니저에서 스킬 관리가 완성되면 Units SearchUnits/ SearchTower 대공사.
			다 하고 나면 유니티 FSM 방식 공부해서 바꿀 수 있겠다 싶으면 바꾸고
				=> 안되면 걍 바로 매쉬 넣으면서 정리하기.

03:06 2022-4-4	힐러 대충 완성.
		현준이한테 units 클래스 설명 완료.
			해야할거 (0404 저녁, 0405 아침까지 교양이랑 과제점...)
				콜라이더 크기 받아와서 공격, 걷기에 적용 시키기.(테스트 필요)
				skill쪽 완성되면 타워, 유닛 검색 대공사
							=> FSM 방식은 방학 개인 폴때
					=>걍 매쉬 넣고 정리하기.

03:27 2022-4-6	유닛 콜라이더 크기 받아와서 거리 조절하기. 03:46 2022-4-6 완료
		유닛 SearchTower 우선순위 skillmanger에서도 받아오게 하기 
			ㄴ03:59 2022-4-6 코드 작업은 해놨는디 정민이 씬에서 지금 오류남. 오류 잡기엔 너무 피건해서 일단 내일 ㄱㄱㄱ
			앞으로 할일
				매쉬 골라서 넣은 뒤 애들 무기 콜라이더 작업 + 리지드바디 작업 + 투사체 작업 + 태그 정리 작업 (일단 애니메이션 제외)

13:41 2022-4-6	스태틱 매쉬 오브젝트들 매쉬, 머테리얼(텍스쳐) 세팅 방법 알아보기
			-> 그담에 다이내믹 매쉬들 어뜨케 익스퍼트해가 어떻게 임폴트해가 어떻게 지금 캡슐덩어리들한테 적용을 할것인가 알아보기

01:18 2022-4-9	개같이 부활 -> 눈깔 좀 많이 나아짐.
02:12 2022-4-9	스태틱 매쉬 머테리얼 세팅법 확인 완료
		타워 프리팹, 넥서스 프리팹 교체
		!!타워 매니저에 프리팹들 바꿔줘!!!!!! -> 내 폴더 -> 프리팹 ->스트럭쳐즈에 있는 프리팹들로!!		
		**현준이 타워 소환 할 때 Funcs 에있는 ChangeMesh 함수 써서 하면 편함!! 
			=> 스킬1 프리팹을 그냥 우리가 추출한 매쉬로 그냥 새 프리팹(적군 아군)맨들어 쓰셈, 그 계층구조 자체는 어쩔수가 없음. 코드로 다 달아줄거 아니면
			해야할거
				1. 유닛들 다이내믹 매쉬 고르기
				2. 우리프로젝트에서 세팅
				3. 콜라이더, 리지드바디, 투사체, 태그 정리 작업

03:02 2022-4-9	유닛 매쉬, 무기 골랐음 -> 일단 생각보다 시인성 안좋고, 힐러같은경우 녹색 십자가 그려져 있는 캐릭은 하나밖에없음. 탱커로 쓸만한것도 안보이고
					=> 같은 회사 로우폴리곤 현대전 에셋있는데 거진 35만원임 ㅋㅋ;
			=> 그래서 일단 이걸로 스킨드 매쉬, 애니메이션 공부해보고 나중에 다시 하던지 하자.
			해야할거
				1. 추출한 유닛 매쉬에 총 달고 우리 프로젝트에 임폴트 한 뒤 프리팹들 새로 만들어 주기 => 유닛 생성코드 아마 많이 바뀔듯.
				2. 그 후 콜라이더, 리지드바디, 투사체, 태그 정리 => 하면서 코드 정리

04:24 2022-4-12	모든 유닛 프리팹 무기 달기 완성
			그거에 맞춰 Units 클래스 가진 애들 Weapon 찾고 Range유닛들 총알 나갈 총구 부분 찾아야함
				=> Funcs에 자식까지 포함해서 게임오브젝트 태그나 이름으로 찾는 함수 만들어놓음 : 해결
			=> UnitFactory에 유닛 프리팹들 넣어놓는거 좀 바뀜. 자기 씬에서 확인하도록.
		캐릭터 컨트롤러로 충돌 테스트 완성.
			=> 모든 유닛 프리팹들에 적용 필요함.
		모든 유닛 기본 행동 애니메이션 찾음(Idle, Walk, Attack,Hit 등)
			=> 아직 컨트롤러 설정은 못함
		Bullet 프리팹도 바뀜에 따라서 RangeFunc 코드 교체 완료.
			=> Tower 전용 Bullet 프리팹 만들고 Tower프리팹도 좀 바껴야해서 해서 한동안 타워 작동 안될꺼임
		이제 Unit이건 Tower건 Nexus건 아군, 적군인건 Units스크립트에 IsEnemy로 확인해야함.
			=> Tag로 아군, 적군 확인 금지 금지 금지 금지 !!!!
	=> 기존 캡슐 유닛 프리팹들, 안쓰는 프리팹, 파일들 지울꺼임. => 차후 MainScene에 머지하면 폴더도 정리 할꺼임.

04:50 2022-4-12 해야할거 
		1. 모든 유닛에 캐릭터 컨트롤러 달아주고 콜라이더 크기 설정해주기
		2. 애니메이션 세세한 디테일 안잡고 그냥 상태에 맞는 애니메이션 나오게 애니메이터 수정
		3. Range 유닛 같은경우 Attack시 총구방향 적팀쪽으로 돌리게 하기
		4. 근접 유닛 같은 경우 Attack시 Weapon의 Collider 껏다 켰다 하기
		5. Tower 프리팹에 총구 모델링 하나 박아놓고 거기서 총알 나가게 + 타겟 유닛 바라보도록
		6. Tower 전용 Bullet 프리팹 만들기
		7. Medic 유닛 Attack 과 Heal 따로 만들기. => 좀 대공사 (무적권 우선순위 힐링, 아군없으면 공격(좃구대기로))
		8. Medic 유닛 약품 모델링 찾기
		9. 타워, 넥서스 마무리 짓기 
			=> 다 끝나면 파티클 시스템 공부해서 힐 이펙트나 그런거
	
~~0416 부터 아마 교양 과목 공부 하느라 작업량 확 줄어들듯함.~~

02:08 2022-4-13	모든 유닛 캐릭터 컨트롤러 달아줌 
		밀리 유닛 걷기, 공격 애니메이션은 잡음 + 무기 콜라이더로 공격 판정 나게 함
			=> 이제 각 유닛들 무기 자체에 weapon이라는 스크립트 가지고 있음. 그리고 그 안에서 근접:콜라이더on/off / 원거리:총알생성 해줌.
					=> 데미지 들어가는 부분도 근접무기 or 총알에서 처리 해주는 중.

			=> 근데 애니메이션 관련 부분은 어렵다기 보다 내가 상태머신을 코드에 맞게 짜는걸 계속 만져보면서 시행착오 겪어야 할거같음
				=> 맞는 애니메이션, 죽는 애니메이션 차후에 넣을 예정
		해야할거 
			1. Range 유닛 간단한 공격 애니메이션 상태머신 세팅
			2. Range 유닛 공격 할때 총구 상대방향으로 돌리기
			3. Range 유닛 공격판정 들어가면 weapon에서 총알 생성하기.
			4. 총알 스크립트에서 데미지 판정

03:54 2022-4-13	Range 유닛 간단한 애니메이션 상태머신 세팅 완료
		Range 유닛 공격시 총구방향 45도 돌려주기
		Range 유닛 공격시 총알 스크립트 생성
		
		해야할거
			타워 총알 정상화

04:33 2022-4-13	타워 총알 발사, 죽는거 까지 다함.
		
		해야할거
		->타워, 넥서스 처럼 부피 있는 애들 타겟으로 잡은 Unit들 공격 처리 따로 빼주기
		->넥서스 클래스 작동
		->탱커 애니메이션 상태머신 세팅
			탱커 공격
		->힐러 재단장
			=> 아군 유닛이 있으면 힐주고
				없으면 권총 빵야 빵야
		
			
	

---------------------------------------------------------------
[배경,스킬,적AI] - 방현준
격자 모양 맵 찍기 - 완료
스킬 구현 (플레이어꺼, 적꺼 / 유닛쪽 스크립트 설명 들으면서 가닥 잡아야함)
    /////완료
	1. SkillManager 통해 스킬1 사용 함수 제작 완료. (비행기 소환, 자동 폭탄 드랍까지 가능 / 충돌 판정 아직 x)
	2. 폭탄 콜라이더 콜라이더 넣고 충돌 기능 만들기 => (Collider / Trigger / RigidBody / {OnTrigger~~() / OnCollision~~() 함수 공부 필요})
	3. Skill2(타워 생성) 기능 만들기 => 2022-3-30 유닛 프리팹에 이동기능을 빼고 재활용하여 소형 포탑 생성스킬기능을 만들기 ex)토르비욘 포탑
	4. SkillManager를 통해 Skill2까지 사용 가능하도록.
	/////완료

	5. Ui/Card 파트에서 사용 하는거 보조 해주기

	

적 AI (거창한 AI느낌보다 적의 리소스 체크해서 리소스내의 유닛 랜덤으로 생성하는 것 정도)
	=> 여유가 된다면 FSM이나 행동트리(비헤이비어 트리) 공부하면 좋을텐데 한달만에 힘들 가능성 농후
	=> 적 유닛 생성은 타워 앞 두군대로 일단.

	04/12 적 AI 랜덤 스폰 설정 완료!
             04/13 적 AI 랜덤 스폰 설정 추가 수정( 좌표값 적 타워에서 받아오기) 완료 및 Skill2 타워 소환시 아군 넥서스 좌표를 받아옴






---------------------------------------------------------------
[여유 남는 사람 해야 하는거]
씬 연결, 구성 -> MainGame 클래스 만들어서 매니저들 Dont Destory 하고 각 매니저,스크립트들 기능 확실히 인지한 상태에서 관리 해야함.

[후반 작업] - 팀원 모두
에셋 입히기
오디오 입히기
그래픽 모델링, UI스프라이트들 게임 톤 맞추기 


----계획----
매주 수요일 캡스톤수업, 수요일 저녁때마다 일주일 계획확인 및 결산

3월5주~4월1주 개발일정
윤정민: 덱시스템,UI 연동 및 자원바 시스템 완성 完
방현준: 스킬 시스템 구현(폭탄이 땅에 닿으면 터지고 새로운 폭발체를 생성해서 폭발체에 유닛이 데미지 입음) 完
전근희: 1. 유닛 생성시 건물과 충돌시 생성취소 2. 탱커/힐러 제작 3. 유닛 공격범위 조정 完

0406 ~ 0413 
윤정민:체력 데미지 폰트 구현(일단은 숫자와 월드포지션만 받아오면 화면에 띄워주는 기능 구현):完 , 씬트리 구현:10%, 카드 선택 화면과 선택한 덱의 인게임과 연동 구현):完, 적 플레이어AI의 덱 시스템 구현 
방현준:제작한 스킬에서 에셋연동(스킬1(비행기,폭탄), 스킬2(포탑,총알)), 적 플레이어AI
전근희:제작한 유닛에서 에셋연동, 끝나면 태그,콜라이더,리지드바디 설정 끝내기(유닛 다듬기)

0413 ~ 0420
윤정민: 씬트리 에셋작업까지 완성하고 남은시간 UI애니메이션(효과) 제작하기 :   HP바 프리팹 새로 완성 - HpBarManager.HpBarInstrate() 함수,  HpBar 클래스의 HpBarSetting(CurHp, FulHp, UnitPos, bool isEnemy) 함수를 이용하시면 됩니당.
전근희: 유닛 끝.내.기(기본적인 기능만) + 승리/패배 조건 완성(넥서스파괴)

0420 ~ 0427


3월 마지막주 팀플 수업때 기능 기획 구체화 해서 확실히 정하기.
		=> 유닛 몇개, 스킬 몇개, 씬 구성 어떻게 + 각 기능들 어디까지 구현 할것인지
		=> 공동 작업(디코하면서) 요일 or 시간 정하기 + 중간고사시험기간 감안 해서 

4월 안으로 기본적인 기능 모두 완성.

5월 남은 2주 정도에 사운드, 그래픽 톤앤매너 맞추고 남은시간에 순위가 밀리는 기능 +@ 구현하고 프로젝트 끝.





나 그냥 유티니랑 c# 써보면서 우리 프로젝트에 필요한거 만들어본거 참고할꺼면 하셈
https://github.com/JeonJohnson/YoungSanTeamProjectTest



시간남으면 구현할 부가기능
1.(로비 씬)아마 여유 되는 사람 붙어서 같이 작업 해야함.
2.타워상태에 따라 가변적으로 변하는 유닛 소환위치





정할것
유닛 개수
근접:2(티어1, 티어2)
탱커:1
원거리:2(티어1, 티어2)
힐러:1

총 6개



유닛 컨셉


스킬 컨셉
1.융단폭격(맵의 랜덤한 좌표에 데미지), ()
2.타워소환(유닛에 이동기능 빼고 특정좌표에 소환후 @초뒤 사라짐)

총 스킬개수 2개



적도 똑같은 매커니즘(but 모델링만 다름)의 유닛 및 스킬 사용







수요일 캡스톤 수업, 수요일 오후 8시30분 ~ 12시 까지 디코에 모여서 작업

근희형님 안되는 시간 : 1달에 1,2번 금,토


참고

~~UI~~
itween


~~캐릭터 애니메이션~~ 
믹사모 사이트
형식 fbx foe unity
리깅타입 휴머노이드
아바타 들어가서 리깅 t포즈로 맞춰주기(모델이던 애니매이션이던)
애니메이션 컨트롤러 제작

아바타 마스크 - 상하체분리

애니메이션 타입
레거시
제너릭
휴머노이드 - 리타갯팅

컨트롤러





