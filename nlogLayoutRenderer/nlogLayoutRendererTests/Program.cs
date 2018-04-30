using System;

namespace nlogLayoutRendererTests
{
    class Program
    {
        private static NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                FunctionWithException("Exception!!!");
            }
            catch (System.Exception ex)
            {
                _log.Error(ex, "Error Happend!");
            }
        }

        private static void FunctionWithException(string value)
        {
            try
            {
                value.Substring(100, 100);
            }
            catch (System.Exception ex)
            {
                throw new Exception("Function had exception!", ex);
            }
        }
    }
}
