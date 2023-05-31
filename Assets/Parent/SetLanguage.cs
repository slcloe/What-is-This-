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
        if (LoginDirector.language == 0)
            SetKorText();
        else
            SetEngText();
    }
    public void SetKor(bool isOn)
    {
        LoginDirector.language = 0;
        SetKorText();

	}
    public void SetEng(bool isOn)
    {
		LoginDirector.language = 1;
        SetEngText();
	}

    void SetEngText()
    {
        SetTextContent(SetText.texts[0], "ParentHome");
        SetTextContent(SetText.texts[3], UserInfo.GetcntWord().ToString() + " words");
        SetTextContent(SetText.texts[4], "learned words");
        SetTextContent(SetText.texts[5], "child level");
        SetTextContent(SetText.texts[6], "Learning Analysis");

		SetTextContent(SetText.texts[9], "level1");
		SetTextContent(SetText.texts[10], "level2");
		SetTextContent(SetText.texts[11], "level3");

		SetTextContent(SetText.texts[12], "Reward Settings");
        SetTextContent(SetText.texts[13], "Period");
        SetTextContent(SetText.texts[14], "Name");
        SetTextContent(SetText.texts[15], "Please enter prize");
		SetTextContent(SetText.texts[17], "Please enter period");
		SetTextContent(SetText.texts[19], "Save");
        SetTextContent(SetText.texts[20], "Language Setting");
        SetTextContent(SetText.texts[21], "Korean");
        SetTextContent(SetText.texts[22], "English");
    }
	void SetKorText()
	{
		SetTextContent(SetText.texts[0], "부모홈");
		SetTextContent(SetText.texts[3], UserInfo.GetcntWord().ToString() + " 개");
		SetTextContent(SetText.texts[4], "오늘까지 학습한 단어");
		SetTextContent(SetText.texts[5], "현재 아이 레벨");
		SetTextContent(SetText.texts[6], "학습 결과");

        SetTextContent(SetText.texts[9], "1단계");
		SetTextContent(SetText.texts[10], "2단계");
		SetTextContent(SetText.texts[11], "3단계");

		SetTextContent(SetText.texts[12], "보상 설정");
		SetTextContent(SetText.texts[13], "상품수여 주기");
		SetTextContent(SetText.texts[14], "상품 이름");
		SetTextContent(SetText.texts[15], "상품을 입력해주세요.");
		SetTextContent(SetText.texts[17], "상품수여주기를 입력해주세요.");
		SetTextContent(SetText.texts[19], "수정하기");
		SetTextContent(SetText.texts[20], "언어 설정");
		SetTextContent(SetText.texts[21], "한국어");
		SetTextContent(SetText.texts[22], "영어");
	}


	public static void SetTextContent(Text text, string str) { text.text = str; }
}
