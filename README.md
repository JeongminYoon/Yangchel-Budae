# Yangchel-Budae
YSU 2022 unity game project

----개발환경-----
Unity 2020.3.30f1
Visual Studio 2019

-----구현예정기능-----
[UI] - 윤정민
UI 배치 - 작업중
카드 드래그 앤 드롭 애니메이션
카드 덱 시스템
자원바
유닛/타워 체력바

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

[배경] - 방현준
스킬 구현
// 마지막에 작업
에셋 입히기
오디오 입히기
(여유가 된다면)배경 네브 메쉬 틀 만들기
배경을 체스판처럼 큐브로 나눠놓기 - 야스



나 그냥 유티니랑 c# 써보면서 우리 프로젝트에 필요한거 만들어본거 참고할꺼면 하셈
https://github.com/JeonJohnson/YoungSanTeamProjectTest



