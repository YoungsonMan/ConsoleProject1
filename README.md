# Console Project 1

### Table of Contents / 목차

1. About the Project / 프로젝트에관하여
    1. Goal / 학습목표
    2. Built With / 사용된 툴
    3. explanation / 설명
2. Roadmap

---

# About the Project

---

지금까지 배운것들 : (변수, 조건문, 반복문, 배열 구조체, 함수 등…)
을 이용 하여 모드게임, 텍스트 게임이나  2차원 배열,  Console Readkey, 혹은 ReadLine을 통해 캐릭터 이동 구현하기.

## Goal 학습목표

---

- 변수 ~ 구조체 학습 실전 활용
- 간이 기획서 제작
- 정말로 게임에 쓸 수 있는 메커니즘을 만들기.

## Built with

---

Visual Studio 2022 v17.10.3

Laguage used - C#

## Explanation

---

로스트아크 군단장 레이드 쿠크세이튼 2관 미로 모방  v1.0.0.0
Lost Ark Legion Raid  Kakul Saydon Gate 2 Maze Mimic v1.0.0.0

플레이어는 미로에서 방해꾼을 피해 같은 문양 3개를 먹고 최종 탈출을 해야하는 게임.

![ss01](https://github.com/user-attachments/assets/7672f881-5883-42a4-9610-315fa936394e) ![ss02](https://github.com/user-attachments/assets/acbcf6b6-ef1a-4c8f-ba26-8845c74e4d79)

[https://youtu.be/slN88ptQChg?si=W2IP00Zfhm6ZHgyl&t=9](url)

위와 같은 미니 게임을 구현.

하려했으나 아직 구축하지 못한 기능들이 태반.

# Roadmap

- [ ]  게임설명
- [x]  맵 늘리기
- [x]  문양 만들기
    - [x]  ♠ ♥ ♣ ◆
    - [x]  문양 상호작용
        - [x]  먹으면 반응
        - [ ]  먹어야하는 문양 정해지기
            - [ ]  빈문양 지나치는 문양 ♤ ♡ ♧  ◇
            - [ ]  먹어야 하는 문양 꽉찬문양
- [x]  세토 (방해물) 만들기
    - [x]  길만큼 갯수의 세토 만들기
    - [x]  종으로 한줄
    - [x]  횡으로 한줄
    - [x]  종적 움직임
    - [x]  횡적 움직임
    - [x]  닿으면 상호작용
        - [x]  닿으면 반응
        - [ ]  다시시작
    - [ ]  한번 끝까지가면 다시반복
- [ ]  단계분리
    - [ ]  문양 정해지기
    - [ ]  먹으면 다음 단계
    - [ ]  총 3회 반복
    - [ ]  세토 맞으면 다시시작
    - [ ]  클리어 문구

---

많이 부족하다보니 비효율적인 코드도 정말 많고 구축못한 부분, 손봐야할 부분이 태산입니다.
조금씩 업데이트하면서 수정해갈 예정입니다.
