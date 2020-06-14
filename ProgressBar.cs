/**
* 原文：https://www.cnblogs.com/zhanghuabin/p/5310680.html
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progress
{
    /// <summary>
    /// 进度条类型
    /// </summary>
    public enum ProgressBarType
    {
        /// <summary>
        /// 字符
        /// </summary>
        Character,
        /// <summary>
        /// 彩色
        /// </summary>
        Multicolor
    }

    public class ProgressBar
    {

        /// <summary>
        /// 光标的列位置。将从 0 开始从左到右对列进行编号。
        /// </summary>
        public int Left { get; set; }
        /// <summary>
        /// 光标的行位置。从上到下，从 0 开始为行编号。
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// 进度条宽度。
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 进度条当前值。
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue { get; set; }

        public char CharType = '#';
        /// <summary>
        /// 进度条类型
        /// </summary>
        public ProgressBarType ProgressBarType { get; set; }


        private ConsoleColor colorBack;
        private ConsoleColor colorFore;


        public ProgressBar(ProgressBarType type, float maxValue)
            : this(Console.CursorLeft, Console.CursorTop, type, maxValue)
        {
        }

        public ProgressBar(int left, int top, ProgressBarType type, float maxValue, int width = 50)
        {
            this.Left = left;
            this.Top = top;
            this.Width = width;
            this.ProgressBarType = type;
            MaxValue = maxValue;

            
            Console.SetCursorPosition(Left, Top);
            for (int i = left; ++i < Console.WindowWidth;)
            {
                Console.Write(" ");
            }

            if (this.ProgressBarType == ProgressBarType.Multicolor)
            {
                colorBack = Console.BackgroundColor;
                Console.SetCursorPosition(Left, Top);
                Console.BackgroundColor = ConsoleColor.Gray;
                for (int i = 0; ++i <= width;) { Console.Write(" "); }
                Console.BackgroundColor = colorBack;
            }
            else
            {
                Console.SetCursorPosition(left, top);
                Console.Write("[");
                Console.SetCursorPosition(left + width - 1, top);
                Console.Write("]");
            }
        }

        public float Dispaly(float value)
        {
            return Dispaly(value, null);
        }

        public float Dispaly(float value, string msg)
        {
            if (this.Value != value)
            {
                this.Value = value;

                if (this.ProgressBarType == ProgressBarType.Multicolor)
                {

                    colorBack = Console.BackgroundColor;
                    colorFore = Console.ForegroundColor;

                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(this.Left, this.Top);
                    Console.Write(new string(' ', (int)(Value * this.Width / MaxValue)));
                    Console.BackgroundColor = colorBack;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(this.Left + this.Width + 1, this.Top);
                    if (string.IsNullOrWhiteSpace(msg))
                    {
                        Console.Write($"{Value / MaxValue:P}");
                    }
                    else
                    {
                        Console.Write(msg);
                    }
                    Console.ForegroundColor = colorFore;
                }
                else
                {
                    Console.SetCursorPosition(this.Left + 1, this.Top);
                    Console.Write(new string(CharType, (int)(Value * (this.Width - 2) / MaxValue)));

                    Console.SetCursorPosition(this.Left + this.Width + 1, this.Top);
                    if (string.IsNullOrWhiteSpace(msg))
                    {
                        Console.Write($"{Value / MaxValue:P}");
                    }
                    else
                    {
                        Console.Write(msg);
                    }
                }
            }
            return value;
        }
    }
}
