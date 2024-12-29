namespace Cybervision.Dapr.DataModels
{
    public interface ISearchable
    {
        string SearchKey { get; }
        Guid SearchIdKey { get; }
    }
}
