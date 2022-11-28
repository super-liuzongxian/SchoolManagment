namespace SchoolMangment.Dtos
{
    public class ResultDto<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int TotalCount { get; set; }
    }
}
