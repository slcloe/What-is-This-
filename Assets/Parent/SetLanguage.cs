using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLanguage : MonoBehaviour
{
    //public static int language = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void SetKor(bool isOn)
    {
        LoginDirector.language = 1;
        SetKorText();

	}
    public void SetEng(bool isOn)
    {
		LoginDirector.language = 0;
        SetEngText();
	}

    void SetEngText()
    {
        SetTextContent(SetText.parentText[0], "ParentHome");
        SetTextContent(SetText.parentText[2], "Back to HomeScene");
        SetTextContent(SetText.parentText[4], UserInfo.GetcntWord().ToString() + " words");
        SetTextContent(SetText.parentText[5], "learned words");
        SetTextContent(SetText.parentText[6], "child level");
        SetTextContent(SetText.parentText[7], "Learning Analysis");
        SetTextContent(SetText.parentText[10], "Reward Settings");
        SetTextContent(SetText.parentText[11], "Period");
        SetTextContent(SetText.parentText[12], "Name");
        SetTextContent(SetText.parentText[13], "Please enter prize");
		SetTextContent(SetText.parentText[15], "Please enter period");
		SetTextContent(SetText.parentText[17], "Save");
        SetTextContent(SetText.parentText[18], "Language Setting");
        SetTextContent(SetText.parentText[19], "Korean");
        SetTextContent(SetText.parentText[20], "English");
    }
	void SetKorText()
	{
		SetTextContent(SetText.parentText[0], "부모홈");
		SetTextContent(SetText.parentText[2], "학습화면으로 돌아가기");
		SetTextContent(SetText.parentText[4], UserInfo.GetcntWord().ToString() + " 개");
		SetTextContent(SetText.parentText[5], "오늘까지 학습한 단어");
		SetTextContent(SetText.parentText[6], "현재 아이 레벨");
		SetTextContent(SetText.parentText[7], "학습 결과");
		SetTextContent(SetText.parentText[10], "보상 설정");
		SetTextContent(SetText.parentText[11], "상품수여 주기");
		SetTextContent(SetText.parentText[12], "상품 이름");
		SetTextContent(SetText.parentText[13], "상품을 입력해주세요.");
		SetTextContent(SetText.parentText[15], "상품수여주기를 입력해주세요.");
		SetTextContent(SetText.parentText[17], "수정하기");
		SetTextContent(SetText.parentText[18], "언어 설정");
		SetTextContent(SetText.parentText[19], "한국어");
		SetTextContent(SetText.parentText[20], "영어");
	}


	void SetTextContent(Text text, string str) { text.text = str; }
}
