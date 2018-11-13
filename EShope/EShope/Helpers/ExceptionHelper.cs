using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShope.Helpers
{
    public class ExceptionHelper
    {
        public static void TryCatch(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                // log
            }
        }

        public static async Task TryCatchAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                // log
            }
        }
    }
}
