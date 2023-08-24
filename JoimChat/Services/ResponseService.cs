namespace JoimChat.Services
{
    public class ResponseService
    {
        private bool Status;
        private string Message = "";

        public static ResponseService Success(string message = "")
        {
            return new ResponseService { Status = true, Message = message };
        }

        public static ResponseService Error(string message)
        {
            return new ResponseService { Status = false, Message = message };
        }
    }
}
