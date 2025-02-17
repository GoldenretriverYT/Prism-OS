﻿using PrismOS.Libraries.UI;
using Cosmos.System;

namespace PrismOS.Apps
{
    public class Terminal : Window
    {
        public Button Button = new();
        public Label Label1 = new();
        public string Input = "";

        public Terminal()
        {
            // Window
            X = 75;
            Y = 75;
            Width = 300;
            Height = 200;
            Text = "Console";

            // Button
            Button.X = (int)(Width - 20);
            Button.Width = 20;
            Button.Height = 20;
            Button.Text = "X";
            Button.HasBorder = true;
            Button.OnClickEvents.Add(() => { Windows.Remove(this); });

            // Label1
            Label1.X = 1;
            Label1.Y = 21;
            Label1.Width = Width - 2;
            Label1.Height = Height - 22;
            Label1.Text = "";

            WriteLine("Prism OS CLI V1");
            Elements.Add(Button);
            Elements.Add(Label1);
            Windows.Add(this);
        }

        public override void OnKeyPress(KeyEvent Key)
        {
            base.OnKeyPress(Key);

            switch (Key.Key)
            {
                case ConsoleKeyEx.Backspace:
                    if (Input.Length != 0)
                    {
                        Input = Input[0..(Input.Length - 1)];
                        Label1.Text = Label1.Text[0..(Label1.Text.Length - 1)];
                    }
                    break;
                case ConsoleKeyEx.Enter:
                    WriteLine("");
                    RunCommand(Input);
                    Input = "";
                    break;
                case ConsoleKeyEx.Tab:
                    Input += '\t';
                    Label1.Text += '\t';
                    break;
                default:
                    Input += Key.KeyChar;
                    Label1.Text += Key.KeyChar;
                    break;
            }
        }

        public void RunCommand(string Command)
        {
            switch (Command)
            {
                case "test":
                    WriteLine("this is a testing command!");
                    break;
            }
        }

        public void WriteLine(string T)
        {
            Label1.Text += T + '\n';
        }
    }
}