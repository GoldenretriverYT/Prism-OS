using static PrismProject.Functions.Graphics.Canvas2.Advanced;
using static PrismProject.Functions.Loader;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                InitCore();
            }
            catch
            {
                Reload();
            }
        }
    }
}