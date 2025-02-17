﻿using Cosmos.System;
using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Panel : Control
    {
        public override void OnClick(int X, int Y, MouseState State)
        {
        }

        public override void OnDraw(FrameBuffer Buffer)
        {
            this.Buffer.DrawFilledRectangle(0, 0, (int)Width, (int)Height, (int)Theme.Radius, Theme.Background);

            if (HasBorder)
            {
                this.Buffer.DrawRectangle(0, 0, (int)Width - 1, (int)Height - 1, (int)Theme.Radius, Theme.Accent);
            }

            Buffer.DrawImage(X, Y, this.Buffer, Theme.Radius != 0);
        }

        public override void OnKeyPress(KeyEvent Key)
        {
        }
    }
}