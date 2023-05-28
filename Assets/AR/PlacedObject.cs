using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacedObject : MonoBehaviour
{

    [SerializeField]
    private GameObject Cube;
    private GameObject CubeSelected;
    Text Collected_text;
    public static string curText;


    public bool IsSelected
    {
        get =>  SelectedObject == this;
    }

    private static PlacedObject selectedObject;
    public static PlacedObject SelectedObject
    {
        get => selectedObject;

        set
        {
            if (selectedObject==value ) return;
            selectedObject = value;
            if (value != null)
            {
                string Corrent_Name = "";
                string Currnt_name = value.transform.parent.name;
                if ((Vars.Check_idx + 1) % 3 == 0)
                {
                    if (Vars.AR_correct[Vars.Check_idx] == 0)
                    {
                        Vars.Check_idx++;
                        return;
                    }

                    if (Vars.AR_correct[Vars.Check_idx] == 1) Corrent_Name = "PlacedH0(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 2) Corrent_Name = "PlacedH1(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 4) Corrent_Name = "PlacedH2(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 7) Corrent_Name = "PlacedH3(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 8) Corrent_Name = "PlacedH5(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 16) Corrent_Name = "PlacedH6(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 17) Corrent_Name = "PlacedH7(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 19) Corrent_Name = "PlacedH9(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 20) Corrent_Name = "PlacedH10(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 21) Corrent_Name = "PlacedH11(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 22) Corrent_Name = "PlacedH12(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 23) Corrent_Name = "PlacedH14(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 24) Corrent_Name = "PlacedH15(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 25) Corrent_Name = "PlacedH16(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 26) Corrent_Name = "PlacedH17(Clone)";
                }
                else if ((Vars.Check_idx + 1) % 3 == 2)
                {
                    if (Vars.AR_correct[Vars.Check_idx] == 0) Corrent_Name = "PlacedM0(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 1) Corrent_Name = "PlacedM1(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 2) Corrent_Name = "PlacedM2(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 4) Corrent_Name = "PlacedM4(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 5) Corrent_Name = "PlacedM5(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 6) Corrent_Name = "PlacedM6(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 7) Corrent_Name = "PlacedM7(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 8) Corrent_Name = "PlacedM8(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 9) Corrent_Name = "PlacedM9(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 12) Corrent_Name = "PlacedM12(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 13) Corrent_Name = "PlacedM13(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 16) Corrent_Name = "PlacedM16(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 17) Corrent_Name = "PlacedM17(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 18) Corrent_Name = "PlacedM18(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 19) Corrent_Name = "PlacedM19(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 20) Corrent_Name = "PlacedM20(Clone)";
                }
                else
                {
                    if (Vars.AR_correct[Vars.Check_idx] == 0) Corrent_Name = "PlacedH0(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 1) Corrent_Name = "PlacedH1(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 2) Corrent_Name = "PlacedH2(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 3) Corrent_Name = "PlacedH3(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 4) Corrent_Name = "PlacedH4(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 5) Corrent_Name = "PlacedH5(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 6) Corrent_Name = "PlacedH6(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 7) Corrent_Name = "PlacedH7(Clone)";

                    if (Vars.AR_correct[Vars.Check_idx] == 8) Corrent_Name = "PlacedH8(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 9) Corrent_Name = "PlacedH9(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 10) Corrent_Name = "PlacedH10(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 11) Corrent_Name = "PlacedH11(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 12) Corrent_Name = "PlacedH12(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 13) Corrent_Name = "PlacedH13(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 14) Corrent_Name = "PlacedH14(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 15) Corrent_Name = "PlacedH15(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 16) Corrent_Name = "PlacedH16(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 17) Corrent_Name = "PlacedH17(Clone)";
                    if (Vars.AR_correct[Vars.Check_idx] == 18) Corrent_Name = "PlacedH18(Clone)";
                }

                bool IsCorrect()
                {
                    if (Corrent_Name  != Currnt_name) return false;
                    else  return true;

                }
                char[] chosung = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };

                string GetCurrentState(int[] correct, int idx)
                {
                    string state = "";
                    int i = 0;
                    for (; i < idx / 3; i++)
                    {
                        state += get_letter(correct[i * 3], correct[i * 3 + 1], correct[i * 3 + 2]);
                    }
                    if (idx % 3 == 0)
                    {
                        state += chosung[correct[i * 3]];
                    }
                    else if (idx % 3 == 1)
                    {
                        state += get_letter(correct[i *3], correct[i * 3 + 1], 0);
                    }
                    else
                    {
                        state += get_letter(correct[i * 3], correct[i * 3 + 1], correct[i * 3 + 2]);
                    }
                    return state;
                }

                char get_letter(int a, int b, int c)
                {
                    return (char)((a * 21 + b) * 28 + c + 0xAC00);
                }

                Debug.Log("AR " + IsCorrect());

                if (IsCorrect())
                {
                    //얘를 textbox에 넣어야함
                    //Debug.Log("AR " + GetCurrentState(Vars.AR_correct,Vars.Check_idx));
                    curText = GetCurrentState(Vars.AR_correct, Vars.Check_idx);
                    Level3Director.curState.text = curText;


                    value.Cube.SetActive(false);
                    Destroy(value);

                    Vars.Check_idx++;

                    string word = TensorFlowLite.SsdSample.detection_text.Replace("\r", "");
                    //string word = "공";
                     if (word.Equals(curText))
                    {
                        Level3Director.success = true;
                    }

                    return;
                }
                else
                {
                    // life--;
                    Level3Director.calllife--;

                    return;
                }
            }
        }
    }

    private void Awake()
    {
        CubeSelected.SetActive(false);
    }


}
