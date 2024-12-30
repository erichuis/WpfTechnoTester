//using Cybervision.Dapr.DataModels;
//using MongoDB.Bson.IO;
//using MongoDB.Bson.Serialization;

//namespace Cybervision.Dapr.Serializers
//{
//    public class SearchKeyAliasSerializer : IBsonSerializer<string>
//    {
//        private readonly string _fieldName;

//        public SearchKeyAliasSerializer(string fieldName)
//        {
//            _fieldName = fieldName;
//        }

//        public Type ValueType => typeof(string);

//        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string value)
//        {
           
//        }

//        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
//        {
//            Serialize(context, args, value as string);
//        }

//        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
//        {
//            return Deserialize(context, args);
//        }

//        public string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
//        {
//           //Deserialize
//        }
//    }

//}
