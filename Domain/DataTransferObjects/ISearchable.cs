namespace Domain.DataTransferObjects
{
    public interface ISearchable
    {
        string SearchKey { get; }
        Guid SearchIdKey { get; set; }
    }
}
