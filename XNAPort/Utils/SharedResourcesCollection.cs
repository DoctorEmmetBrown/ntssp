using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

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
        public Dictionary<int, TKey> mKeys = new Dictionary<int, TKey>();
        public Dictionary<int, TValue> mValues = new Dictionary<int, TValue>();

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
                output.WriteSharedResource<TKey>(item, Keyformat);

                if (default(TValue) is ValueType)
                {
                    output.WriteObject<TValue>(value[item], Valueformat);
                }
                else
                {
                    output.WriteSharedResource<TValue>(value[item], Valueformat);
                }
            }
        }


        protected override SharedResourceDictionary<TKey, TValue> Deserialize(IntermediateReader input,
                                                             ContentSerializerAttribute format,
                                                             SharedResourceDictionary<TKey, TValue> existingInstance)
        {
            if (existingInstance == null)
                existingInstance = new SharedResourceDictionary<TKey,TValue>();

            int i = 0;
            while (input.MoveToElement(Keyformat.ElementName))
            {
                input.ReadSharedResource<TKey>(Keyformat,delegate(TKey item)
                {
                    mKeys.Add(i,item);

                    if(mValues.ContainsKey(i))
                    {
                        existingInstance.Add(mKeys[i],mValues[i]);
                    }
                });

                if(input.MoveToElement(Valueformat.ElementName))
                {
                    if (default(TValue) is ValueType)
                    {
                        TValue value = input.ReadObject<TValue>(Valueformat);
                        mValues.Add(i, value);
                    }
                    else
                    {
                        input.ReadSharedResource<TValue>(Valueformat, delegate(TValue item)
                        {
                            mValues.Add(i, item);

                            if (mKeys.ContainsKey(i))
                            {
                                existingInstance.Add(mKeys[i], mValues[i]);
                            }
                        });
                    }
                }
                i++;
            }

            return existingInstance;
        }
    }
}
