﻿using System;
using System.Collections.Generic;

namespace SuanFa1
{
    class Program
    {
        static void Main(string[] args)
        {

            //GetRepeatedCharacter();
            //testCharArray();
            Console.ReadKey();
        }

        public static void testRemoveDup()
        {
            string s1 = "kkkdjaadklsk";
            //kdjadklsk
        }

        public static void removeDup(Char[] charArray)
        {

        }


        public static void testCharArray()
        {
            string s1 = "abbcde";
           
            string s2 = "";
            string s3 = null;
            var ss1 = s1.ToCharArray();
            var ss2 = s2.ToCharArray();
            var ss3 = s3?.ToCharArray()??new char[0];

            var ssa = GetRepeatedCharacter(ss1);
            var ssb = GetRepeatedCharacter(ss2);
            var ssc = GetRepeatedCharacter(ss3);
            int e = 1;
        }

        public static bool GetRepeatedCharacter(Char[] charArray)
        {
            HashSet<Char> myset = new HashSet<char>(20);
            if(charArray.Length == 0)
            {
                return false;
            }

            for(int i=0; i<charArray.Length; i++)
            {
                myset.Add(charArray[i]);
            }

            if(myset.Count == charArray.Length)
            {
                return false;
            }else
            {
                return true;
            }

        }



        public static void printContinueNumber()
        {
            int sum = 15;
            print(sum);

          
        }

        public static void print(int n)
        {
            for(int i=1; i<=n/2; i++)
            {
                for(int j=i+1; j<n; j++)
                {
                    if(sumcon(i,j) == n)
                    {
                        Console.WriteLine(i+ "-" + j);
                    }
                }
            }
        }

        public static int sumcon(int begin, int end)
        {
            var s = (begin + end) * (end - begin + 1) / 2;
            return s;
        }
    }
}
