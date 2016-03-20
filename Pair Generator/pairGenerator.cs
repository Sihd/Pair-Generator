﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Pair_Generator
{
    class pairGenerator
    {
        private BigInteger compNum;//store the composite number we are working from
        private Number x, y; //store both roots
        private bool givenx;//are we working from the x(true) root or y(false) root
        private pDigit pd;//local instance of pDigit class

        //make default constructor private to prevent use
        private pairGenerator() { }

        public pairGenerator(BigInteger composite, Number root)
        {
            compNum = composite;//store given composite number to be factored in compNum
            rootGiven(root);//call rootGiven() to figure out if they gave us the x root or y root
            if (givenx)//if they gave us the x root then store root in x
                x = root;
            else//otherwise store root in y
                y = root;

            pd = new pDigit(compNum);
            Generate();
        }

        //figure out which root we were given
        private void rootGiven(Number root)
        {
            string tempnum = compNum.ToString();//temporarily store compNum as string so we don't create this string more than once
            string temproot = root.value.ToString();//temporarily store rootNum as string so we don't create this string more than once
            char compd = tempnum[tempnum.Length - 1];//get 1's digit of compNum and store in compd
            char rootd = temproot[temproot.Length - 1];//get 1's digit of root and store in rootd

            switch(compd)//enter switch case to find out which root was given
            {
                case '1':
                    givenx = (rootd == '7') ? false : true;//if 1's digit of compNum is 1 and 1's digit of root is 7 we were given the y root
                    //if we were given a root that has any other valid digit in the 1's digit it may be considered the x root
                    break;
                case '3':
                    givenx = (rootd == '3' || rootd == '9') ? false : true;//if 1's digit of compNum is 3 and 1's digit of root is 3 or 9 we were given the y root
                    //if we were given a root that has any other valid digit in the 1's digit it may be considered the x root
                    break;
                case '7':
                    givenx = (rootd == '7' || rootd == '9') ? false : true;//if 1's digit of compNum is 7 and 1's digit of root is 7 or 9 we were given the y root
                    //if we were given a root that has any other valid digit in the 1's digit it may be considered the x root
                    break;
                case '9':
                    givenx = (rootd == '9') ? false : true;//if 1's digit of compNum is 9 and 1's digit of root is 9 we were given the y root
                    //if we were given a root that has any other valid digit in the 1's digit it may be considered the x root
                    break;
            }

        }

        //generate the opposite pair to the one we were provided
        public void Generate()
        {
            //if we were given x
            if(givenx)
            {
                setOnes();//call setOnes to instantiate y root with correct 1's digit
                if (x.Length == 1)//if we were only given a 1 digit long root then return from Generate()
                    return;//return from Generate()


            }
        }


        //private function to set up the 1's digits of x and y
        private void setOnes()
        {
            string tempnum = compNum.ToString();//temporarily store compNum as string so we don't create this string more than once
            string temproot = (givenx) ? x.value.ToString() : y.value.ToString();//temporarily store rootNum as string so we don't create this string more than once
            char compd = tempnum[tempnum.Length - 1];//get 1's digit of compNum and store in compd
            char rootd = temproot[temproot.Length - 1];//get 1's digit of root and store in rootd

            //use nested switch cases to set up 1's digits of x and y
            switch (compd)
            {
                case '1':
                    switch (rootd)
                    {
                        case '1':
                            x = new Number(1);
                            break;
                        case '3':
                            y = new Number(7);
                            break;
                        case '7':
                            x = new Number(3);
                            break;
                        case '9':
                            y = new Number(9);
                            break;
                    }
                    break;
                case '3':
                    switch (rootd)
                    {
                        case '1':
                            y = new Number(3);
                            break;
                        case '3':
                            x = new Number(1);
                            break;
                        case '7':
                            y = new Number(9);
                            break;
                        case '9':
                            x = new Number(7);
                            break;
                    }
                    break;
                case '7':
                    switch (rootd)
                    {
                        case '1':
                            y = new Number(7);
                            break;
                        case '3':
                            y = new Number(9);
                            break;
                        case '7':
                            x = new Number(1);
                            break;
                        case '9':
                            x = new Number(3);
                            break;
                    }
                    break;
                case '9':
                    switch (rootd)
                    {
                        case '1':
                            y = new Number(9);
                            break;
                        case '3':
                            y = new Number(3);
                            break;
                        case '7':
                            y = new Number(7);
                            break;
                        case '9':
                            x = new Number(1);
                            break;
                    }
                    break;
            }
        }
    }
}