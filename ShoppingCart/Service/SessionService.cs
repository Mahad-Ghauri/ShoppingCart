using Newtonsoft.Json;

namespace ShoppingCart.Service
{
    public static class SessionService
    {
        // We are receiving an object of List of type item and then converting the object in strong through Json
        
       
            public static void SetSessionObjectAsJson(ISession session, string key, object value)
            {
                session.SetString(key, JsonConvert.SerializeObject(value));
            }

            public static T GetSessionObjectFromJson<T>(ISession session, string key)
            {
                var value = session.GetString(key);
                return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
            }
        

    }
}
