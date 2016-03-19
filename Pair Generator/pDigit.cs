using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Pair_Generator
{
    internal class pDigit
    {
        private int[][][] digits; // 2D array to hold digit pairs
        private int branches; //holds greatest index of first array boundary in digits array

        //make default constructor private to prevent use
        private pDigit() { }

        //set up pDigit based on d being any of the following: '1','3','7','9'
        private pDigit(char d)
        {
            //set up digits[][][] with correct 3D array depending on ones digit of number being factored
            switch (d)
            {
                case '1':
                    setup1();
                    break;
                case '3':
                    setup3();
                    break;
                case '7':
                    setup7();
                    break;
                case '9':
                    setup9();
                    break;
                default://if an invalid value is passed into the constructor then throw an exception
                    throw new ArgumentException("Tried to instantiate pDigit class with invalid char: " + d);
            }
        }

        //construct using a string
        private pDigit(string number):this(number[number.Length - 1])
        {
        }

        //construct pDigit based on 1's digit of BigInt number
        public pDigit(BigInteger number):this(number.ToString())
        {
        }

        private void setup1()
        {
            digits = new int[][][] { //1x1 table
                new int[][] { new int[] { 0,9,8,7,6,5,4,3,2,1 },
                              new int[] { 1,0,9,8,7,6,5,4,3,2 },
                              new int[] { 2,1,0,9,8,7,6,5,4,3 },
                              new int[] { 3,2,1,0,9,8,7,6,5,4 },
                              new int[] { 4,3,2,1,0,9,8,7,6,5 },
                              new int[] { 5,4,3,2,1,0,9,8,7,6 },
                              new int[] { 6,5,4,3,2,1,0,9,8,7 },
                              new int[] { 7,6,5,4,3,2,1,0,9,8 },
                              new int[] { 8,7,6,5,4,3,2,1,0,9 },
                              new int[] { 9,8,7,6,5,4,3,2,1,0 }},
            //3x7 table
                new int[][] { new int[] { 0,1,2,3,4,5,6,7,8,9 },
                              new int[] { 7,8,9,0,1,2,3,4,5,6 },
                              new int[] { 4,5,6,7,8,9,0,1,2,3 },
                              new int[] { 1,2,3,4,5,6,7,8,9,0 },
                              new int[] { 8,9,0,1,2,3,4,5,6,7 },
                              new int[] { 5,6,7,8,9,0,1,2,3,4 },
                              new int[] { 2,3,4,5,6,7,8,9,0,1 },
                              new int[] { 9,0,1,2,3,4,5,6,7,8 },
                              new int[] { 6,7,8,9,0,1,2,3,4,5 },
                              new int[] { 3,4,5,6,7,8,9,0,1,2 }},
            //9x9 table
                new int[][] { new int[] { 0,9,8,7,6,5,4,3,2,1 },
                              new int[] { 9,8,7,6,5,4,3,2,1,0 },
                              new int[] { 8,7,6,5,4,3,2,1,0,9 },
                              new int[] { 7,6,5,4,3,2,1,0,9,8 },
                              new int[] { 6,5,4,3,2,1,0,9,8,7 },
                              new int[] { 5,4,3,2,1,0,9,8,7,6 },
                              new int[] { 4,3,2,1,0,9,8,7,6,5 },
                              new int[] { 3,2,1,0,9,8,7,6,5,4 },
                              new int[] { 2,1,0,9,8,7,6,5,4,3 },
                              new int[] { 1,0,9,8,7,6,5,4,3,2 }} };

            branches = 2;
        }

        private void setup3()
        {
            digits = new int[][][] { //1x3 table
                new int[][] {new int[] { 0,7,4,1,8,5,2,9,6,3},
                             new int[] { 1,8,5,2,9,6,3,0,7,4},
                             new int[] { 2,9,6,3,0,7,4,1,8,5},
                             new int[] { 3,0,7,4,1,8,5,2,9,6},
                             new int[] { 4,1,8,5,2,9,6,3,0,7},
                             new int[] { 5,2,9,6,3,0,7,4,1,8},
                             new int[] { 6,3,0,7,4,1,8,5,2,9},
                             new int[] { 7,4,1,8,5,2,9,6,3,0},
                             new int[] { 8,5,2,9,6,3,0,7,4,1},
                             new int[] { 9,6,3,0,7,4,1,8,5,2} },
                    //7x9 table
                new int[][] {new int[] { 0,3,6,9,2,5,8,1,4,7},
                             new int[] { 3,6,9,2,5,8,1,4,7,0},
                             new int[] { 6,9,2,5,8,1,4,7,0,3},
                             new int[] { 9,2,5,8,1,4,7,0,3,6},
                             new int[] { 2,5,8,1,4,7,0,3,6,9},
                             new int[] { 5,8,1,4,7,0,3,6,9,2},
                             new int[] { 8,1,4,7,0,3,6,9,2,5},
                             new int[] { 1,4,7,0,3,6,9,2,5,8},
                             new int[] { 4,7,0,3,6,9,2,5,8,1},
                             new int[] { 7,0,3,6,9,2,5,8,1,4} }};

            branches = 1;
        }

        private void setup7()
        {
            digits = new int[][][] {//1x7 table
                new int[][] {new int[] { 0,3,6,9,2,5,8,1,4,7},
                             new int[] { 1,4,7,0,3,6,9,2,5,8},
                             new int[] { 2,5,8,1,4,7,0,3,6,9},
                             new int[] { 3,6,9,2,5,8,1,4,7,0},
                             new int[] { 4,7,0,3,6,9,2,5,8,1},
                             new int[] { 5,8,1,4,7,0,3,6,9,2},
                             new int[] { 6,9,2,5,8,1,4,7,0,3},
                             new int[] { 7,0,3,6,9,2,5,8,1,4},
                             new int[] { 8,1,4,7,0,3,6,9,2,5},
                             new int[] { 9,2,5,8,1,4,7,0,3,6}},
                //3x9 table
                new int[][] {new int[] { 0,7,4,1,8,5,2,9,6,3},
                             new int[] { 7,4,1,8,5,2,9,6,3,0},
                             new int[] { 4,1,8,5,2,9,6,3,0,7},
                             new int[] { 1,8,5,2,9,6,3,0,7,4},
                             new int[] { 8,5,2,9,6,3,0,7,4,1},
                             new int[] { 5,2,9,6,3,0,7,4,1,8},
                             new int[] { 2,9,6,3,0,7,4,1,8,5},
                             new int[] { 9,6,3,0,7,4,1,8,5,2},
                             new int[] { 6,3,0,7,4,1,8,5,2,9},
                             new int[] { 3,0,7,4,1,8,5,2,9,6}}};

            branches = 1;
        }

        private void setup9()
        {
            digits = new int[][][] {
                //1x9 table
                new int[][] {new int[] { 0,1,2,3,4,5,6,7,8,9},
                             new int[] { 1,2,3,4,5,6,7,8,9,0},
                             new int[] { 2,3,4,5,6,7,8,9,0,1},
                             new int[] { 3,4,5,6,7,8,9,0,1,2},
                             new int[] { 4,5,6,7,8,9,0,1,2,3},
                             new int[] { 5,6,7,8,9,0,1,2,3,4},
                             new int[] { 6,7,8,9,0,1,2,3,4,5},
                             new int[] { 7,8,9,0,1,2,3,4,5,6},
                             new int[] { 8,9,0,1,2,3,4,5,6,7},
                             new int[] { 9,0,1,2,3,4,5,6,7,8}},
                     //3x3 table
                new int[][] {new int[] { 0,9,8,7,6,5,4,3,2,1},
                             new int[] { 7,6,5,4,3,2,1,0,9,8},
                             new int[] { 4,3,2,1,0,9,8,7,6,5},
                             new int[] { 1,0,9,8,7,6,5,4,3,2},
                             new int[] { 8,7,6,5,4,3,2,1,0,9},
                             new int[] { 5,4,3,2,1,0,9,8,7,6},
                             new int[] { 2,1,0,9,8,7,6,5,4,3},
                             new int[] { 9,8,7,6,5,4,3,2,1,0},
                             new int[] { 6,5,4,3,2,1,0,9,8,7},
                             new int[] { 3,2,1,0,9,8,7,6,5,4}},
                     //7x7 table
                new int[][] {new int[] { 0,9,8,7,6,5,4,3,2,1},
                             new int[] { 3,2,1,0,9,8,7,6,5,4},
                             new int[] { 6,5,4,3,2,1,0,9,8,7},
                             new int[] { 9,8,7,6,5,4,3,2,1,0},
                             new int[] { 2,1,0,9,8,7,6,5,4,3},
                             new int[] { 5,4,3,2,1,0,9,8,7,6},
                             new int[] { 8,7,6,5,4,3,2,1,0,9},
                             new int[] { 1,0,9,8,7,6,5,4,3,2},
                             new int[] { 4,3,2,1,0,9,8,7,6,5},
                             new int[] { 7,6,5,4,3,2,1,0,9,8}}};

            branches = 2;
        }

        public int[] getArray(int branch, int valueAdding)
        {

            //verify parameters are within acceptable range
            if (branch < 0 || branch > branches)
                throw new ArgumentException("branch value passed to pDigit.getArray() is invalid: " + branch);
            if (valueAdding < 0 || valueAdding > 9)
                throw new ArgumentException("branch value passed to pDigit.getArray() is invalid: " + valueAdding);

            //return array at given indices
            return digits[branch][valueAdding];
        }
    }
}
