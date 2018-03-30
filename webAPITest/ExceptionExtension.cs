using Elmah;
using System;


namespace webAPITest
{
    public static class ExceptionExtension
    {
        public static void Process(this Exception excepcion)
        {
            try
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(excepcion);
            }
            catch (Exception)
            {
                Elmah.ErrorLog.GetDefault(null).Log(new Error(excepcion));
            }
        }
    }
}