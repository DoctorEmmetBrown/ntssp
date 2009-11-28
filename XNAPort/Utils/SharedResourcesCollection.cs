using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

namespace Utils
{
    public class SharedResourceList<T> : List<T> { }

    public class SharedResourceDictionary<TKey, TValue> : Dictionary<TKey,TValue> {}

    [ContentTypeSerializer]
    class SharedResourceListSerializer<T> : ContentTypeSerializer<SharedResourceList<T>>
    {
        static ContentSerializerAttribute itemFormat = new ContentSerializerAttribute()
        {
            ElementName = "Item"
        };



        protected override void Serialize(IntermediateWriter output,
                                          SharedResourceList<T> value,
                                          ContentSerializerAttribute format)
        {
            foreach (T item in value)
            {
                output.WriteSharedResource(item, itemFormat);
            }
        }


        protected override SharedResourceList<T> Deserialize(IntermediateReader input,
                                                             ContentSerializerAttribute format,
                                                             SharedResourceList<T> existingInstance)
        {
            if (existingInstance == null)
                existingInstance = new SharedResourceList<T>();

            while (input.MoveToElement(itemFormat.ElementName))
            {
                input.ReadSharedResource(itemFormat, (T item) => existingInstance.Add(item));
            }

            return existingInstance;
        }
    }

    [ContentTypeSerializer]
    class SharedResourceDictionnarySerializer<TKey, TValue> : ContentTypeSerializer<SharedResourceDictionary<TKey, TValue>>
    {
        static ContentSerializerAttribute Keyformat = new ContentSerializerAttribute()
        {
            ElementName = "Key"
        };

        static ContentSerializerAttribute Valueformat = new ContentSerializerAttribute()
        {
            ElementName = "Value"
        };



        protected override void Serialize(IntermediateWriter output,
                                          SharedResourceDictionary<TKey, TValue> value,
                                          ContentSerializerAttribute format)
        {
            foreach (TKey item in value.Keys)
            {
                output.WriteSharedResource(item, Keyformat);
                output.WriteSharedResource(value[item], Valueformat);
            }
        }


        protected override SharedResourceDictionary<TKey, TValue> Deserialize(IntermediateReader input,
                                                             ContentSerializerAttribute format,
                                                             SharedResourceDictionary<TKey, TValue> existingInstance)
        {
            if (existingInstance == null)
                existingInstance = new SharedResourceDictionary<TKey,TValue>();

            while (input.MoveToElement(Keyformat.ElementName))
            {
                TKey key = default(TKey);
                input.ReadSharedResource(Keyformat, (TKey item) => key = item);

                TValue value = default(TValue);
                if(input.MoveToElement(Valueformat.ElementName))
                {
                    input.ReadSharedResource(Valueformat, (TValue item) => value = item);
                    existingInstance.Add(key,value);
                }

            }

            return existingInstance;
        }
    }
}
