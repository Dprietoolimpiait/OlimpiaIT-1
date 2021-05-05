using System;

namespace WindowsLiderEntrega.Exceptions
{
    public class MessageException
    {
        public static string GetGeneralMessage(Exception ex)
        {
            return $"Ha ocurrido un error en la aplicación: {ex.Message}";
        }
    }
}
