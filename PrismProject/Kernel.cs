using System;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                while (true)
                {
                    Display.Visual2D.DisplayConfig.Controler.Clear();
                    Services.Mouse_Service.TickForward();

                }
            }
            catch (Exception exc)
            {
                // ToDo: create new error dialog with window manager
                Console.WriteLine(exc.Message);
            }
        }
    }
}