using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TensorFlowLite
{
   public class Consonant
    {
        int text_len = 0;
        string word;
        //List<letter> lst = new List<letter>();
        //List<letter> lst2 = new List<letter>();
        //List<letter> lst3 = new List<letter>();
        List<letter>[] lst = new List<letter>[3];

        public Consonant(string text) 
        {
            word = text;
            set_string_idx(text);
            set_letter_simil(lst[1]);
            set_letter_simil(lst[2]);
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
                lst[0].Add(set_letter_idx(ch));
                lst[1].Add(set_letter_idx(ch));
                lst[2].Add(set_letter_idx(ch));
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
            System.Random r = new System.Random();
            int order = r.Next(0, text_len - 1);
            int jamo = r.Next(2);
            if (jamo == 2 && letters[order].idx[2] == 0)
                jamo = 0;
            int idx = 0;
            do
            {
                if (jamo == 0)
                    idx = r.Next(0, 18);
                else if (jamo == 1)
                    idx = r.Next(0, 20);
                else
                    idx = r.Next(1, 27);
            } while (lst[0][order].idx[jamo] == idx);
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
        public string[] get_words()
        {
            string[] strs = new string[3];
            System.Random r = new System.Random();
            int tmp = r.Next(1, 4);
            int tmp1 = r.Next(1, 3);
            if (tmp == 1)
            {
                strs[1] = get_text(lst[0]);
                if (tmp1 == 1)
                {
                    strs[2] = get_text(lst[1]);
                    strs[3] = get_text(lst[2]);
                }
                else
                {
					strs[2] = get_text(lst[2]);
					strs[3] = get_text(lst[1]);
				}
            }
            else if (tmp == 2)
            {
                strs[1] = get_text(lst[1]);
                if (tmp1 == 1)
                {
					strs[2] = get_text(lst[0]);
					strs[3] = get_text(lst[2]);
				}
                else
                {
					strs[2] = get_text(lst[2]);
					strs[3] = get_text(lst[0]);
				}
            }
            else
            {
                strs[1] = get_text(lst[2]);
                if (tmp1 == 1)
                {
					strs[2] = get_text(lst[0]);
					strs[3] = get_text(lst[1]);
				}
                else
                {
					strs[2] = get_text(lst[1]);
					strs[3] = get_text(lst[0]);
				}
            }

            return strs;
        }
    }
}

