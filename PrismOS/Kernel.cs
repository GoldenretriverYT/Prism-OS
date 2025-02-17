﻿using Cosmos.HAL.Drivers.PCI.Audio;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Resource;
using PrismOS.Libraries.UI;
using Cosmos.System.Audio;
using Cosmos.System;
using PrismOS.Apps;
using Cosmos.Core;

namespace PrismOS
{
    public unsafe class Kernel : Cosmos.System.Kernel
    {
        public static FrameBuffer Canvas = new(VBE.getModeInfo().width, VBE.getModeInfo().height);
        public static AudioMixer Mixer = new();

        protected override void BeforeRun() 
        {
            #region Splash Screen

            Canvas.DrawImage((int)((Canvas.Width / 2) - 128), (int)((Canvas.Height / 2) - 128), Assets.Splash256);
            Canvas.CopyTo((uint*)VBE.getLfbOffset());

            #endregion

            #region Misc

            Assets.Wallpaper = Assets.Wallpaper.Resize(Canvas.Width, Canvas.Height);
            MouseManager.ScreenWidth = Canvas.Width;
            MouseManager.ScreenHeight = Canvas.Height;

            #endregion

            #region Apps

            Desktop D = new();
            D.Add(() => { _ = new AppTemplate1(); });
            D.Add(() => { _ = new Terminal(); });
            D.Add(() => { Shutdown(); });

            #endregion

            #region Startup Sound

            Play(Assets.Vista);

            #endregion
        }

        protected override void Run()
        {
            Canvas.DrawImage(0, 0, Assets.Wallpaper, false);
            Canvas.DrawFilledRectangle(0, 0, (int)Font.Default.MeasureString($"FPS: {Canvas.FPS}") + 30, (int)Font.Default.Size + 30, 0, Color.LightBlack);
            Canvas.DrawString(15, 15, $"FPS: {Canvas.FPS}", Font.Default, Color.White);

            bool KeyPress = KeyboardManager.TryReadKey(out var Key);
            foreach (Window Window in Window.Windows)
            {
                if (Window.Windows[^1] == Window && KeyPress)
                {
                    Window.OnKeyPress(Key);
                }
                Window.OnDraw(Canvas);
            }

            // Draw Cursor And Update The Screen
            Canvas.DrawImage((int)MouseManager.X, (int)MouseManager.Y, Assets.Cursor);
            Canvas.CopyTo((uint*)VBE.getLfbOffset());
        }

        public static void Play(AudioStream Stream)
        {
            try
            {
                Mixer.Streams.Add(Stream);
                new AudioManager()
                {
                    Stream = Mixer,
                    Output = AC97.Initialize(4096),
                }.Enable();
            }
            catch (System.Exception E)
            {
                Cosmos.HAL.Debug.Serial.SendString($"[WARN] Unable To Play Audio! ({E.Message})");
            }
        }

        public static void Shutdown()
        {
            // Try VBOX Method
            IOPort P = new(0x4004);
            P.DWord = 0x3400;

            // Try QEMU Method
            Power.QemuShutdown();

            // Try Normal Method
            Power.Shutdown();
        }
    }
}