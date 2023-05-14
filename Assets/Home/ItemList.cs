using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public static class ButtonExtension
{
    public static void AddEventListener(this Button button, Action OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick();
        });
    }
}

public class ItemList : MonoBehaviour
{
    [Serializable]
    public struct MyPumpkin
    {
        public Sprite pumpkin;
    }

    [SerializeField] MyPumpkin[] allPumpkins;

    private List<Item> itemList;
    void getItemList()
    {

        //1,2,3,4(상품),5(학습시작)
        itemList = UserInfo.GetWords();
        itemList.Add(new Item(0, 5));

    }
    void Start()
    {
        getItemList();
        DrawUI();
    }
    void DrawUI()
    {
        GameObject itemTemplate = transform.GetChild(0).gameObject;
        GameObject g;


        foreach (Item item in itemList)
        {
            g = Instantiate(itemTemplate, transform);

            int pumpkinIdx = item.successLevel - 1;
            if (pumpkinIdx == -1) pumpkinIdx = 0;
            Sprite pumpkin = allPumpkins[pumpkinIdx].pumpkin;

            g.transform.GetChild(0).GetComponent<Image>().sprite = pumpkin;

            g.transform.GetChild(1).GetComponent<Text>().text = item.word;

            int fontSize = 200;
            if (item.successLevel != 5 && item.word.Length > 3) fontSize -= (item.word.Length - 3) * 40;
            g.transform.GetChild(1).GetComponent<Text>().fontSize = fontSize;

            if (item.idx == 0) g.GetComponent<Button>().AddEventListener(GotoLearning);
        }

        Destroy(itemTemplate);

    }

    void GotoLearning()
    {
        SceneManager.LoadScene("SSD");
    }

}
