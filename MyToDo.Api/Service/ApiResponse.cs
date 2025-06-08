namespace MyToDo.Api.Service
{
    public class ApiResponse
    {
        public ApiResponse(bool status, object data)
        {

            this.Status = status;
            this.Data = data;
        }
        public ApiResponse(string massage, bool status = false)
        {
            this.Message = massage;
            this.Status = status;
        }
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
