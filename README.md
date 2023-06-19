# 이건 뭐지? (What Is This)
<img width="100" alt="pumpkin_gold" src="https://github.com/Sao-jung-listens-well/WIT/assets/81304917/cfae966d-fdda-46f2-9d14-208c45f7a554">
 
 "이건 뭐지?"는 사물인식을 통해 인식된 단어를 학습합니다. 주변에 있는 사물을 학습하고 이후 AR을 통해 주변 사물에 숨겨진 자모음 오브젝트를 순서대로 모으며 자모음 학습을 진행합니다. 각 단계별 학습 성공률에 따라 난이도가 고려됩니다.


### 개발 환경
Unity / C#


### 사용 기술
-	사물인식 : TensorflowLite, Tensorflow.NET, Unity ML-agents
-	STT / TTS : Google Cloud API
-	AR : ARCore
-	백엔드 : SpringBoot, HQL, MariaDB, AWS EC2, AWS RDS, Jenkins, NGINX
-	보상이미지 : Naver Search API
-	Github, Git-LFS, Unity


### 구조도
- 프론트엔드 프로세스
![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/a53e5031-a3b0-493e-bc82-dbf5dcf3e6e8)

- 백엔드 프로세스
![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/07566798-a10a-4fd4-8e24-9ae1d7c6dd17)

- 유스케이스  
![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/30b352e7-b2cb-477e-9488-668e3a798952)


### 기능

- `학습단어 화면`  
  지금까지 학습한 단어들을 확인할 수 있습니다.  
  초록색 호박은 1단계, 노란색 호박은 2단계, 주황색 호박은 3단계 성공 단어입니다.   
  회색 호박을 누르면 학습 화면으로 넘어갑니다.  
  황금 호박을 누르면 보상을 얻을 수 있습니다.  
- `0단계 기본학습`  
  “소리듣기”버튼을 누르면 해당 단어의 발음을 들을 수 있습니다.  
- `1단계 통문자학습`  
  학습단어를 기준으로 유사한 단어를 추출하여 선택지로 제시합니다.  
  정답을 고르면 성공, 틀린 답을 고르면 실패입니다.  
- `2단계 말하기학습`  
  “소리듣기” 버튼을 누르면 학습 단어의 발음을 들을 수 있습니다.  
  “말하기” 버튼을 누르면 5초동안 학습 단어의 발음을 녹음하여 성공 여부를 결정합니다.  
- `3단계 자모음학습`  
  단어를 이루는 자모음이 주변 공간에 흩어집니다.  
  자모음을 순서대로 터치하여 단어를 완성하면 성공입니다.  
  틀린 자모음을 누르면 화면 왼쪽 하단의 생명이 깎이게 되며
  3개의 생명이 다 사라질 경우 실패입니다.
- `부모페이지`  
  부모가 아이의 학습 현황을 살펴볼 수 있는 부모페이지입니다.  
  지금까지 학습한 단어와, 현재 아이 레벨을 확인할 수 있습니다.  
  각 단계별 학습 성공률을 그래프로 볼 수 있습니다.  
  보상을 설정하거나 언어를 설정할 수 있습니다. (한국어/영어 지원)


|**홈**  |**회원가입** |**로그인**  |
|---|---|---|
|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/17b9de04-0e9e-4004-8ad2-1b3eb18ae88c)|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/c7d19218-ecab-4d17-8974-c921596c8ae3)|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/cb98a148-caed-45d8-a43b-5d6b82c012b7)|  

|**학습단어**  |**사물인식** |**0단계 기본학습**  |
|---|---|---|
|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/892e11a6-0ba8-4900-87cb-fc80bdda941d)|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/2b3db342-83b2-4d3a-b5a7-3ef13dc039bb)|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/6cf24205-eac0-4ad1-a7fb-2c9233f41f97)|  

|**1단계 통문자학습**  |**2단계 말하기학습** |**3단계 자모음학습**  |
|---|---|---|
|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/805c3d1e-c98a-4d3d-8c56-5cdbba2806c8)|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/e867425b-441e-472e-b840-492449fc08b3)|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/41550b8e-637f-420e-a2ad-cab113c6e0b1)|  

|**성공 화면**  |**실패 화면** |**학습 보상**  |
|---|---|---|
|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/77263eb8-2b86-48b8-ada4-7f78e257fb93)|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/957636c5-66b4-4a46-9129-d9eb3e9c6d1c)|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/c22a9fc9-7e98-44e9-a9b0-65b01127c3eb)|  

|**부모페이지: 학습결과**  |**부모페이지: 성공률그래프** |**부모페이지: 언어•보상설정**  |
|---|---|---|
|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/3358fd98-7cf2-4d7e-8e70-3ddf81c5aef9)|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/3af348f6-d043-4d2c-b137-c6b92f9e3cf1)|![image](https://github.com/Sao-jung-listens-well/WIT/assets/81304917/96bf1356-53dc-4b6a-b468-1a66fbf1abea)|  


