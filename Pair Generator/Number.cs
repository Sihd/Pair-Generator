﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Pair_Generator
{
    public class Number
    {
        private BigInteger n;//main variable storage
        private static BigInteger[] digits;//array to store 10^n+1 where n is the index in the array

        static Number()//initialize the digits array
        {
            int defaultsize = 50;//variable for default starting size of the array
            digits = new BigInteger[defaultsize];//initialize the array to size of defaultsize
            BigInteger temp = 10;//initialize temp BigInteger variable to store value to be assigned to each BigInteger in digits[]


            //fill array with 10^n for easy and fast calculations on n, such as removing the 7 in 7543 would be 7543 % digits[2] or 7543 % 1000 = 543
            //or adding a digit to the main variable n, such as: adding 7000 to 543 would be 543 + digits[2] = 543 + 7000 = 7543
            for(int i = 0; i < defaultsize; i++)//loop through digits[]
            {
                digits[i] = temp;//assign current value of temp to current index in digits[]
                temp *= 10;
            }
        }

        public Number()
        {
            n = 0;
        }

        //constructor for Number when given a value of type BigInt
        public Number(BigInteger num)
        {
            if(num.ToString().Length > (digits.Length + 1))
                increaseDigits(num.ToString().Length);
        }

        public Number(string num)
        {
            if (BigInteger.TryParse(num, out n) == false)//try to parse the string into a BigInt
                throw new ArgumentException("Tried storing non-numeric value in Number");//if unsuccessful throw error message
            if (num.Length > (digits.Length + 1))//check if digits array is long enough to handle working with this value
                increaseDigits(num.Length);//if not call increaseDigits to fix it
        }

        
        //set up accessors to n
        public BigInteger value
        {
            get { return n; }//return n
            set {
                if (value >= 0)//verify n is greater than or equal to zero
                    n = value;//if so set n to value
            }
        }


        //increase size of the static digits array 
        public static void increaseDigits(int length = -1)
        {
            if (length == -1)//check if default call was used
                length = digits.Length * 2;//if so set length to be twice what it is
            if (length <= digits.Length)//if array is already that size or greater just return
                return;//return without doing anything

            BigInteger[] temp = new BigInteger[length];//initialize new array of requested size
            BigInteger tempValue = digits[digits.Length - 1];//get value of last index in digits

            for (int i = 0; i < digits.Length; i++)//loop through digits array
                temp[i] = digits[i];//store value at current index in digits into current index in temp

            for(int i = digits.Length - 1; i < length; i++)//loop through remaining indexes in temp
            {
                temp[i] = tempValue;//assign current value of tempValue to current index in temp
                tempValue *= 10;//generate next value for the next index
            }

            digits = temp;//set pointer in digits to address of temp

        }


        //add (d * 10^(index + 1)) to value stored
        public void addDigit(int d, int index)
        {
            if(d > 10 || d < 0)
            {
                throw new ArgumentException("Digit being added to Number is too big or is negative: " + d);
            }

            if (d == 0)//check if digit being added is zero
                return;//if so then nothing needs to be done so just return            

            //set index to get the correct index for next digit to add to n. If n = 543 then n.size == 3 and I want to add a multiple of 1000.
            //digits[3] == 10^(3+1) == 10,000 when I want 1,000
            //so I want the value of digits[n.size - 1] == digits[2] == 10^(2+1) == 1,000 
            if(index < n.ToString().Length - 1)
                index = n.ToString().Length - 1;


            if(index < 0)//if adding to 1's digit just set n to d and return
            {
                n = d;
                return;
            }


            if (index > (digits.Length + 1))//make sure we have the index available in digits array
                increaseDigits();//if not call default increaseDigits() to double digits array size

            //add d * 10^(i + 1) if index == 2 and d == 7 then 7 * 10^(2+1) == 7 * 1,000 == 7,000
            n += digits[index] * d;
        }


        //remove largest digit
        public void removeDigit()
        {
            //get index of largest digit. if n == 7,543 then n.length == 4. we need the index with the value of 1,000 in digits
            //digit[2] == 1,000. 4 - 2 = 2. So (n.length - 2) will give us the correct index
            int index = n.ToString().Length - 2;

            if(index < 0)//verify that we are not trying to remove a digit smaller than the 10's digit
            {
                n = 0;//if we are then just set n to 0
                return; //and return
            }

            //use modulus opperator on n to quickly and easy remove the 
            n = n % digits[index];
        }
         
        
        //set digit at index to d   
        public void setDigit(int d, int index)
        {
            if (d > 10 || d < 0)//check if d is less than 10 and not negative
                throw new ArgumentException("Argument 'd' in setDigit too big or negative: " + d);
            if (index < 0)//make sure index is positive
                throw new ArgumentException("Argument 'index' in setDigit() is negative: " + index);

            if (index > (n.ToString().Length - 1))//check if this can be handled by addDigit()
                addDigit(d, index);//if so call addDigit()

            //get digit at index if index < n.length
            int digit = int.Parse(n.ToString()[d].ToString());

            if (d > 0)//check if value to be set is not 0
                d = d - digit; // if it is not 0 get multiplier to apply
            else if (d == 0)//if the value the caller wants to add is 0
                d = -digit;//set multiplier to negative value of digit

            //if d == 9 and digit == 6 then d gets assigned 9 - 6 = 3, so 3 gets added to 6 setting digit to 9
            //if d == 6 and digit == 9 then d gets assigned 6 - 9 = -3, so -3 gets added to 9 setting digit to 6
            //if d == 0 and digit == 6 then d gets assigned -6, so -6 gets added to 6 setting digit to 0

            if (index == 0)//if setting the ones digit
                n = d;//just set n to d
            else //otherwise set digit at index to d
                n += d * digits[index - 1];

        }
    }
}
