using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EShope.API.Helpers
{
    public static class ExceptionHelper
    {
        public static void TryCatch(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
            }
        }

        public static R TryCatch<R>(Func<R> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return default(R);
            }
        }

        public static async Task<R> TryCatch<R>(Func<Task<R>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                return await Task.FromResult<R>(default(R));
                //return default(R);
            }
        }
    }
}