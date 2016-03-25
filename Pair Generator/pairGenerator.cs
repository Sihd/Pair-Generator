using System;
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
        private int branch;//store the branch we are working on

        //make default constructor private to prevent use
        private pairGenerator() { }

        public pairGenerator(BigInteger composite, Number root)
        {
            compNum = composite;//store given composite number to be factored in compNum
            rootGiven(root);//call rootGiven() to figure out if they gave us the x root or y root
            if (givenx)//if they gave us the x root then store root in x
                x = new Number(root[0]);
            else//otherwise store root in y
                y = new Number(root[0]);

            pd = new pDigit(compNum);
            Generate(root);//fill out the opposite root based on the root given
        }


        //allow read only access to root given
        public Number mainRoot
        {
            get
            {
                if (givenx)//if we were given x root
                    return x;//return x root
                return y;//if given y root return y root
            }
        }


        //give read only access to generated root
        public Number otherRoot
        {
            get
            {
                if (givenx)//if we were given x root
                    return y;//return y root
                return x;//if given y root return x root
            }
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
        private void Generate(Number root)
        {
            //if we were given x
            if(givenx)
            {
                setOnes();//call setOnes to instantiate y root with correct 1's digit
                if (x.Length == 1)//if we were only given a 1 digit long root then return from Generate()
                    return;//return from Generate()

                //proccess each digit starting with the 10's digit
                for (int i = 1; i < root.Length; i++)
                {
                    addDigit(root[i]);
                    proccessDigit(i);
                }
            }
            else
            {
                setOnes();//call setOnes to instantiate x root with correct 1's digit
                if (y.Length == 1)//if we were only given a 1 digit long root then return from Generate()
                    return;//return from Generate()

                //proccess each digit starting with the 10's digit
                for (int i = 1; i < root.Length; i++)
                {
                    addDigit(root[i]);
                    proccessDigit(i);
                }
            }
        }

        //process digit at indx
        private void proccessDigit(int indx)
        {
            
            if(givenx)//if we are proccessing a digit when we were given the x digit
            {
                if (indx > (x.Length - 1) || indx < 0)//check if indx is within valid ranges
                    throw new ArgumentException("Index given to pairGenerator.proccessDigit() is invalid: " + indx);//if not give helpful error message

                //calculate the difference between compnum and the product of x and y and store in difNum
                Number difNum = new Number(compNum - (x.value * y.value));
                int[] ary = pd.getArray(branch, difNum[indx]);//get array for the amount we need to add

                y.addDigit(x[indx]);//add digit to y at the index in ary associated with the value of the digit in x at position indx

            }
            else
            {
                if (indx > (y.Length - 1) || indx < 0)//check if indx is within valid ranges
                    throw new ArgumentException("Index given to pairGenerator.proccessDigit() is invalid: " + indx);//if not give helpful error message

                //calculate the difference between compnum and the product of x and y and store in difNum
                Number difNum = new Number(compNum - (x.value * y.value));
                int[] ary = pd.getArray(branch, difNum[indx]);//get array for the amount we need to add

                //since the array given from pd.getArray is ordered according to the x digit
                //we must search for the index of the correct y digit

                int j = y[indx];//get y digit value we are looking for
                for(int i = 0; i < 10; i++)
                {
                    if (ary[i] == j)//check if ary[i] is the y digit we are looking for
                    {
                        x.addDigit(i);//if so add index of the y digit in the array ary to x
                        break;//and break
                    }
                }
            }
        }

        //Extend the addDigit functionality from the Number struct to the pairGenerator
        //to enable modifacation of the root being tried while maintaining a read only
        //state of the opposite root to ensure it remains a valid pair
        public void addDigit(int d)
        {
            if(givenx)//if we are working from the x root
            {
                x.addDigit(d);//add new digit to x root
                proccessDigit(x.Length - 1);//generate digit in y root
            }
            else //if we are working from the y root
            {
                y.addDigit(d);//add new digit to y root
                proccessDigit(y.Length - 1);//generate digit in x root
            }
        }

        //extend removeDigit functionality from Number struct
        //to maintain read only status of opposite root generated
        public void removeDigit()
        {
            x.removeDigit();
            y.removeDigit();
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
                            branch = 0;
                            break;
                        case '3':
                            y = new Number(7);
                            branch = 1;
                            break;
                        case '7':
                            x = new Number(3);
                            branch = 1;
                            break;
                        case '9':
                            y = new Number(9);
                            branch = 2;
                            break;
                    }
                    break;
                case '3':
                    switch (rootd)
                    {
                        case '1':
                            y = new Number(3);
                            branch = 0;
                            break;
                        case '3':
                            x = new Number(1);
                            branch = 0;
                            break;
                        case '7':
                            y = new Number(9);
                            branch = 1;
                            break;
                        case '9':
                            x = new Number(7);
                            branch = 1;
                            break;
                    }
                    break;
                case '7':
                    switch (rootd)
                    {
                        case '1':
                            y = new Number(7);
                            branch = 0;
                            break;
                        case '3':
                            y = new Number(9);
                            branch = 1;
                            break;
                        case '7':
                            x = new Number(1);
                            branch = 0;
                            break;
                        case '9':
                            x = new Number(3);
                            branch = 1;
                            break;
                    }
                    break;
                case '9':
                    switch (rootd)
                    {
                        case '1':
                            y = new Number(9);
                            branch = 0;
                            break;
                        case '3':
                            y = new Number(3);
                            branch = 1;
                            break;
                        case '7':
                            y = new Number(7);
                            branch = 2;
                            break;
                        case '9':
                            x = new Number(1);
                            branch = 0;
                            break;
                    }
                    break;
            }
        }
    }
}
