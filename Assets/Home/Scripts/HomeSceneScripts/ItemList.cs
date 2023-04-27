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

    private struct Item
    {
        int id;
        string word;
        int level;
        public Item(int id, string word, int level)
        {
            this.id = id;
            this.word = word;
            this.level = level;
        }
        public string getWord() { return word; }
        public int getId() { return id; }
        public int getLevel() { return level; }
    }
    private List<Item> itemList;
    void getItemList()
    {

        //1,2,3,4(상품),5(학습시작)
        itemList = new List<Item>();
        itemList.Add(new Item(0, "hello", 1));
        itemList.Add(new Item(1, "사과", 1));
        itemList.Add(new Item(2, "사과", 3));
        itemList.Add(new Item(3, "", 4));
        itemList.Add(new Item(4, "가방", 1));
        itemList.Add(new Item(-1, "", 5));

        /*
                itemList[0] = new Item(0, "hello", 1);
                itemList[1] = new Item(1, "사과", 1);
                itemList[2] = new Item(2, "사과", 3);
                itemList[3] = new Item(3, "당근", 1);
                itemList[4] = new Item(4, "가방", 1);
                itemList[5] = new Item(5, "", 4);
                itemList[6] = new Item(6, "인형", 1);
                itemList[7] = new Item(7, "빵", 1);
                itemList[8] = new Item(8, "hello", 2);
                itemList[9] = new Item(9, "사과", 2);
                itemList[10] = new Item(10, "사과", 3);
                itemList[11] = new Item(11, "당근",2);
                itemList[12] = new Item(12, "가방",3);
                itemList[13] = new Item(13, "마우스", 1);
                itemList[14] = new Item(14, "인형", 3);
                itemList[15] = new Item(15, "빵" ,2);
                itemList[15] = new Item(15, "빵", 3);
        */
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


        int N = itemList.Count;

        for (int i = 0; i < N; i++)
        {
            g = Instantiate(itemTemplate, transform);
            Sprite pumpkin = allPumpkins[itemList[i].getLevel() - 1].pumpkin;

            g.transform.GetChild(0).GetComponent<Image>().sprite = pumpkin;

            g.transform.GetChild(1).GetComponent<Text>().text = itemList[i].getWord();

            if(itemList[i].getId() == -1) g.GetComponent<Button>().AddEventListener(GotoLevel1);
        }

        Destroy(itemTemplate);

    }

    void GotoLevel1()
    {
        SceneManager.LoadScene("Level2Scene");
    }

}
