# Yangchel-Budae
YSU 2022 unity game project

----개발환경-----
Unity 2020.3.30f1
Visual Studio 2019

-----구현예정기능-----
[UI] - 윤정민
(인게임씬)
카드 덱 시스템 - 19:43 2022-3-28 작업중

덱 시스템하고 UI 연동

유닛/타워 체력바

자원바

다 기능 구현 완료하고 나면 유닛이랑 머지
그 후 UI 애니메이션


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
			1.유닛 생성 할때 타워나 다른 오브젝트에 마우스 올려서 생성되면 그 옆에 생성되게 하거나 아예 생성이 안되게 하기
			2.탱커, 힐러 제작
			2.5 밀리2티어, 렌쥐2티어 제작
			3.콜리더 크기 받아와서 거리재기에 포함시키기
				=> 이제 유닛, 타워, 넥서스 모두 콜리더, 태그, 이름정리하기 
				
---------------------------------------------------------------
[배경,스킬,적AI] - 방현준
격자 모양 맵 찍기 - 완료
스킬 구현 (플레이어꺼, 적꺼 / 유닛쪽 스크립트 설명 들으면서 가닥 잡아야함)
	1. SkillManager 통해 스킬1 사용 함수 제작 완료. (비행기 소환, 자동 폭탄 드랍까지 가능 / 충돌 판정 아직 x)
	2. 폭탄 콜라이더 콜라이더 넣고 충돌 기능 만들기 => (Collider / Trigger / RigidBody / {OnTrigger~~() / OnCollision~~() 함수 공부 필요})
	3. Skill2(타워 생성) 기능 만들기 => 2022-3-30 저녁에 회의해서 구체적인 기능에 대해서 토론한 뒤 생성 함수 만들기
	4. SkillManager를 통해 Skill2까지 사용 가능하도록.
	5. Ui/Card 파트에서 사용 하는거 보조 해주기

적 AI (거창한 AI느낌보다 적의 리소스 체크해서 리소스내의 유닛 랜덤으로 생성하는 것 정도)
	=> 여유가 된다면 FSM이나 행동트리(비헤이비어 트리) 공부하면 좋을텐데 한달만에 힘들 가능성 농후
	=> 적 유닛 생성은 타워 앞 두군대로 일단.

---------------------------------------------------------------
[여유 남는 사람 해야 하는거]
씬 연결, 구성 -> MainGame 클래스 만들어서 Dont Destory 하고 각 매니저,스크립트들 기능 확실히 인지한 상태에서 관리 해야함.

[후반 작업] - 팀원 모두
에셋 입히기
오디오 입히기
그래픽 모델링, UI스프라이트들 게임 톤 맞추기 


----계획----
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





