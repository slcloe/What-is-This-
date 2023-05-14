using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace TensorFlowLite
{
   public class Consonant
    {
        int text_len = 0;
        string word;
        string[] strs = new string[3];
		int[] cho_rand = { 4, 6, 7, 8, 10, 13, 16, 17, 18 };
		int[] jung_rand = { 0, 4, 8, 13, 20 };
		int[] jong_rand = { 0, 1, 4, 8, 16 };
		List<letter> lst = new List<letter>();
        List<letter> lst1 = new List<letter>();
        List<letter> lst2 = new List<letter>();

        public Consonant(string text) 
        {
            word = text;
            set_string_idx(text);
            set_letter_simil(lst1);
            set_letter_simil(lst2);
            set_words();
        }

        public string get_word(int idx)
        {
            if (idx < 0 || idx > strs.Length) { return null; }
            return strs[idx];
        }


        public struct letter
        {
            public int[] idx;
            public letter(int a, int b, int c)
            {
                idx = new int[3];
                idx[0] = a; idx[1] = b; idx[2] = c;
            }
        }

        void set_string_idx(string text)
        {
            foreach(char ch in text)
            {
                lst.Add(set_letter_idx(ch));
                lst1.Add(set_letter_idx(ch));
                lst2.Add(set_letter_idx(ch));
                text_len++;
            }
        }
        letter set_letter_idx(char ch)
        {
            int a = ((ch - 0xAC00) / 28) / 21;
            int b = ((ch - 0xAC00) / 28) % 21;
            int c = (ch - 0xAC00) % 28;
            letter ltr = new letter(a, b, c);
            return ltr;
        }
        void set_letter_simil(List<letter> letters)
        {
            int order = UnityEngine.Random.Range(0, text_len - 1);
            int jamo = UnityEngine.Random.Range(0, 2);
            if (jamo == 2 && letters[order].idx[2] == 0)
                jamo = 0;
            int idx = 0;
            do
            {
                if (jamo == 0)
                {
                    //idx = cho_rand[UnityEngine.Random.Range(0, cho_rand.Length)];
                    idx = UnityEngine.Random.Range(0, 19);

				}
                else if (jamo == 1)
                    idx = jung_rand[UnityEngine.Random.Range(0, jung_rand.Length)];
                else
                    idx = jong_rand[UnityEngine.Random.Range(0, jong_rand.Length)];
            } while (lst[order].idx[jamo] == idx);
            letters[order].idx[jamo] = idx;
        }
        char get_letter(letter ltr)
        {
            return (char)((ltr.idx[0] * 21 + ltr.idx[1]) * 28 + ltr.idx[2] + 0xAC00);
        }
        string get_text(List<letter> letters)
        {
            string str = "";
            foreach(letter ltr in letters)
            {
                str += get_letter(ltr);
            }
            return str;
        }
        void set_words()
        {
            int tmp = UnityEngine.Random.Range(1, 4);
            int tmp1 = UnityEngine.Random.Range(1, 3);
            if (tmp == 1)
            {
                strs[0] = get_text(lst);
                if (tmp1 == 1)
                {
                    strs[1] = get_text(lst1);
                    strs[2] = get_text(lst2);
                }
                else
                {
					strs[2] = get_text(lst2);
					strs[1] = get_text(lst1);
				}
            }
            else if (tmp == 2)
            {
                strs[0] = get_text(lst1);
                if (tmp1 == 1)
                {
					strs[1] = get_text(lst);
					strs[2] = get_text(lst2);
				}
                else
                {
					strs[2] = get_text(lst2);
					strs[1] = get_text(lst);
				}
            }
            else
            {
                strs[0] = get_text(lst2);
                if (tmp1 == 1)
                {
					strs[2] = get_text(lst);
					strs[1] = get_text(lst1);
				}
                else
                {
					strs[2] = get_text(lst1);
					strs[1] = get_text(lst);
				}
            }
        }
    }
}

