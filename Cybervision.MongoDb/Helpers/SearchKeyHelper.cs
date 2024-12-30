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

        public static FilterDefinition<T> CreateSearchIdKeyFilter<T>(Guid searchIdKey) where T : ISearchable
        {
            var fieldName = GetSearchIdKeyFieldName(typeof(T));
            return Builders<T>.Filter.Eq(fieldName, searchIdKey);
        }

        private static string GetSearchKeyFieldName(Type type)
        {
            if (type == typeof(UserDocument)) return "Username";
            if (type == typeof(TodoItemDocument)) return "Title";
            if (type == typeof(JournalEntryDocument)) return "Entry";

            throw new NotSupportedException($"No SearchKey mapping defined for type {type.Name}");
        }

        private static string GetSearchIdKeyFieldName(Type type)
        {
            if (type == typeof(UserDocument)) return "UserId";
            if (type == typeof(TodoItemDocument)) return "TodoItemId";
            if (type == typeof(JournalEntryDocument)) return "JournalEntryId";

            throw new NotSupportedException($"No SearchKey mapping defined for type {type.Name}");
        }
    }
}
