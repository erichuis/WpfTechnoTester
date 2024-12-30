using Cybervision.Dapr.DataModels;
using MongoDB.Driver;

namespace Cybervision.Dapr.Helpers
{
    public static class SearchKeyHelper
    {
        public static FilterDefinition<T> CreateSearchKeyFilter<T>(string searchKey) where T : ISearchable
        {
            var fieldName = GetSearchKeyFieldName(typeof(T));
            return Builders<T>.Filter.Eq(fieldName, searchKey);
        }

        private static string GetSearchKeyFieldName(Type type)
        {
            if (type == typeof(UserDocument)) return "Username";
            if (type == typeof(TodoItemDocument)) return "ProductCode";
            if (type == typeof(JournalEntryDocument)) return "Entry";

            throw new NotSupportedException($"No SearchKey mapping defined for type {type.Name}");
        }
    }
}
