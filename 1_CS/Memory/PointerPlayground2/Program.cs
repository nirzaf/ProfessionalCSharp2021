﻿using System;

namespace PointerPlayground2
{
    public class Program
    {
        unsafe public static void Main()
        {
            Console.WriteLine($"Size of CurrencyStruct struct is {sizeof(CurrencyStruct)}");
            CurrencyStruct amount1 = new(10, 10), amount2 = new(20, 20);

            CurrencyStruct* pAmount = &amount1;
            long* pDollars = &(pAmount->Dollars);
            byte* pCents = &(pAmount->Cents);

            Console.WriteLine($"Address of amount1 is 0x{(ulong)&amount1:X}");
            Console.WriteLine($"Address of amount2 is 0x{(ulong)&amount2:X}");
            Console.WriteLine($"Address of pAmount is 0x{(ulong)&pAmount:X}");
            Console.WriteLine($"Value of pAmount is 0x{(ulong)pAmount:X}");
            Console.WriteLine($"Address of pDollars is 0x{(ulong)&pDollars:X}");
            Console.WriteLine($"Value of pDollars is 0x{(ulong)pDollars:X}");
            Console.WriteLine($"Address of pCents is 0x{(ulong)&pCents:X}");
            Console.WriteLine($"Value of pCents is 0x{(ulong)pCents:X}");

            // because Dollars are declared readonly in CurrencyStruct, you cannot change it with a variable of type CurrencyStruct
            // pAmount->Dollars = 20;
            // but you can change it via a pointer referencing the memory address!
            *pDollars = 100;
            Console.WriteLine($"amount1 contains {amount1}");

            --pAmount;   // this should get it to point to amount2
            Console.WriteLine($"pAmount contains the new address {(ulong)pAmount:X} " +
                $"and references this value {*pAmount}");

            // do some clever casting to get pCents to point to cents
            // inside amount2
            CurrencyStruct* pTempCurrency = (CurrencyStruct*)pCents;
            pCents = (byte*)(--pTempCurrency);
            Console.WriteLine($"Value of pCents is now 0x{(ulong)pCents:X}");
            Console.WriteLine($"The value where pCents points to: {*pCents}");

            Console.WriteLine("\nNow with classes");
            // now try it out with classes
            CurrencyClass amount3 = new(30, 0);

            fixed (long* pDollars2 = &(amount3.Dollars))
            fixed (byte* pCents2 = &(amount3.Cents))
            {
                Console.WriteLine($"amount3.Dollars has address 0x{(ulong)pDollars2:X}");
                Console.WriteLine($"amount3.Cents has address 0x{(ulong)pCents2:X}");
                *pDollars2 = -100;
                Console.WriteLine($"amount3 contains {amount3}");
            }

            // use a function pointer
            FunctionPointerSample.Calc(&Add);
            FunctionPointerSample.Calc(&Subtract);
        }

        static int Add(int x, int y) => x + y;
        static int Subtract(int x, int y) => x - y;
    }
}
