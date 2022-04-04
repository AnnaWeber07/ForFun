using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAndTree
{
    class ExpCalculator
    {
        private ushort[] _InvNFactorialPrev;
        private ushort[] _InvNFactorial;
        private ushort[] _ENumber;
        private readonly int _SeriesLength;
        private readonly int _Precision;

        public ExpCalculator(int precision)
        {
            _Precision = precision;
            int ELength = precision;
            ELength /= 3;

            if (!(0 == precision % 3)) ELength++;
            ELength = (ELength < 9) ? 2 * ELength : ELength * 10 / 9;
            _InvNFactorialPrev = new ushort[ELength + 2];
            _InvNFactorial = new ushort[ELength + 2];
            _ENumber = new ushort[ELength + 2];
            _SeriesLength = SeriesLengthFromPrecision(_Precision + 3);

            Console.WriteLine();
        }

        public static int SeriesLengthFromPrecision(int precision)
        {
            int series = 1;
            int step = 100;

            do
            {
                int p = 0;
                series -= step;

                do
                {
                    series += step;
                    int s = series + step;
                    p = (int)(Math.Log10(2 * Math.PI * s) / 2 + s * Math.Log10((double)s / 3));
                }
                while (p < precision);

                step /= 10;
            }
            while (step > 0);

            return series++;
        }

        public void Calculate()
        {
            _ENumber[0] = 1;
            _InvNFactorial[0] = 1;

            for (uint n = 1; n <= _SeriesLength; n++)
            {
                int ENumberLen = _ENumber.Length - 1;
                _InvNFactorial.CopyTo(_InvNFactorialPrev, 0);
                uint number = _InvNFactorialPrev[0];

                for (int i = 0; i < ENumberLen; i++)
                {
                    _InvNFactorial[i] = (ushort)(number / n);
                    number = (number % n) * 1000 + _InvNFactorialPrev[i + 1];
                }

                number = 0;

                for (int i = ENumberLen; i >= 0; i--)
                {
                    uint OverOne = number / 1000;
                    number = (uint)_ENumber[i] + (uint)_InvNFactorial[i] + OverOne;
                    _ENumber[i] = (ushort)(number % 1000);
                }

                if (_Precision < 2001)
                {
                    int CursorTop = Console.CursorTop;

                    Console.WriteLine("Series length: {0}", n);
                    EPrint(_ENumber.Length, false);
                    Console.SetCursorPosition(0, CursorTop);
                }
            }
        }

        public void Print()
        {
            int piLength = _Precision + 3;
            piLength /= 3;
            if (!(_Precision % 3 == 0))
                piLength++;

            Console.WriteLine("Series length: {0}", _SeriesLength);
            EPrint(piLength, true);

            Console.WriteLine();
        }

        public void EPrint(int Len, bool Sepon)
        {
            int ENumberLen = Len;
            Console.Write("{0},", _ENumber[0]);

            for (int i = 1; i < ENumberLen; i++)
            {
                Console.WriteLine("{0:000}", _ENumber[i]);
                if (i % 2 == 0 && Sepon)
                {
                    Console.Write(" ");
                }
                Console.Write(" ");
            }
        }
    }
}
