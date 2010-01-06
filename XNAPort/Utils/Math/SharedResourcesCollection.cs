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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace Utils
{
    public class SharedResourceList<T> : List<T> { }

    public class SharedResourceDictionary<TKey, TValue> : Dictionary<TKey,TValue> {}

    [ContentTypeSerializer]
    public class SharedResourceListSerializer<T> : ContentTypeSerializer<SharedResourceList<T>>
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
    public class SharedResourceDictionnarySerializer<TKey, TValue> : ContentTypeSerializer<SharedResourceDictionary<TKey, TValue>>
    {
        static ContentSerializerAttribute Keyformat = new ContentSerializerAttribute()
        {
            ElementName = "Key"
        };

        static ContentSerializerAttribute Valueformat = new ContentSerializerAttribute()
        {
            ElementName = "Value"
        };

        List<TKey> mKeys = new List<TKey>();
        List<TValue> mValues = new List<TValue>();
        int mNumberOfItem = 0;

        protected override void Serialize(IntermediateWriter output,
                                          SharedResourceDictionary<TKey, TValue> value,
                                          ContentSerializerAttribute format)
        {
            foreach (TKey key in value.Keys)
            {
                if (default(TKey) is ValueType)
                {
                    output.WriteObject(key, Keyformat);
                }
                else
                {
                    output.WriteSharedResource(key, Keyformat);
                }

                if (default(TValue) is ValueType)
                {
                    output.WriteObject(value[key], Valueformat);
                }
                else
                {
                    output.WriteSharedResource(value[key], Valueformat);
                }
            }
        }

        protected override SharedResourceDictionary<TKey, TValue> Deserialize(IntermediateReader input,
                                                             ContentSerializerAttribute format,
                                                             SharedResourceDictionary<TKey, TValue> existingInstance)
        {
            if (existingInstance == null)
                existingInstance = new SharedResourceDictionary<TKey,TValue>();

            while(input.MoveToElement(Keyformat.ElementName))
            {
                if (default(TKey) is ValueType)
                {
                    TKey key = input.ReadObject<TKey>(Keyformat);
                    mKeys.Add(key);
                }
                else
                {
                    input.ReadSharedResource(Keyformat,delegate(TKey item)
                    {
                        mKeys.Add(item);

                        if(mKeys.Count == mNumberOfItem && mValues.Count == mNumberOfItem)
                        {
                            for(int i=0;i<mNumberOfItem;i++)
                            {
                                existingInstance.Add(mKeys[i],mValues[i]);
                            }
                        }
                    });
                }

                if (default(TValue) is ValueType)
                {
                    TValue value = input.ReadObject<TValue>(Valueformat);
                    mValues.Add(value);
                }
                else
                {
                     input.ReadSharedResource(Valueformat,delegate(TValue item)
                    {
                        mValues.Add(item);

                        if(mKeys.Count == mNumberOfItem && mValues.Count == mNumberOfItem)
                        {
                            for(int i=0;i<mNumberOfItem;i++)
                            {
                                existingInstance.Add(mKeys[i],mValues[i]);
                            }
                        }
                    });
                }
                mNumberOfItem++;
            }
            return existingInstance;
        }
    }

    [ContentTypeWriter]
    public class SharedResourceDictionaryWriter<TKey,TValue> : ContentTypeWriter<SharedResourceDictionary<TKey,TValue> >
    {
        /// <summary>
        /// Saves sprite sheet data into an XNB file.
        /// </summary>
        protected override void Write(ContentWriter aOutput, SharedResourceDictionary<TKey, TValue> aValue)
        {
            int nbElement = 0;
            nbElement = aValue.Keys.Count;

            aOutput.WriteObject(nbElement);

            foreach (TKey key in aValue.Keys)
            {
                if (default(TKey) is ValueType)
                {
                    aOutput.WriteObject(key);
                }
                else
                {
                    aOutput.WriteSharedResource(key);
                }

                if (default(TValue) is ValueType)
                {
                    aOutput.WriteObject(aValue[key]);
                }
                else
                {
                    aOutput.WriteSharedResource(aValue[key]);
                }
            }
        }


        /// <summary>
        /// Tells the content pipeline what worker type
        /// will be used to load the sprite sheet data.
        /// </summary>
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(SharedResourceDictionaryReader<TKey,TValue>).AssemblyQualifiedName; 
        }


        /// <summary>
        /// Tells the content pipeline what CLR type the sprite sheet
        /// data will be loaded into at runtime.
        /// </summary>
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(SharedResourceDictionary<TKey,TValue>).AssemblyQualifiedName;
        }
    }

    public class SharedResourceDictionaryReader<TKey, TValue> : ContentTypeReader<SharedResourceDictionary<TKey, TValue>>
    {
        List<TKey> mKeys = new List<TKey>();
        List<TValue> mValues = new List<TValue>();
        int mNumberOfItem = 0;

        protected override SharedResourceDictionary<TKey, TValue> Read(
                ContentReader input,
                SharedResourceDictionary<TKey, TValue> existingInstance)
        {
            if (existingInstance == null)
                existingInstance = new SharedResourceDictionary<TKey, TValue>();


            int nbElement = 0;

            nbElement = input.ReadObject<int>();

            int i = 0;
            while (i < nbElement)
            {
                if (default(TKey) is ValueType)
                {
                    TKey key = input.ReadObject<TKey>();
                    mKeys.Add(key);
                }
                else
                {
                    input.ReadSharedResource(delegate(TKey item)
                    {
                        mKeys.Add(item);

                        if (mKeys.Count == mNumberOfItem && mValues.Count == mNumberOfItem)
                        {
                            for (int j = 0; j < mNumberOfItem; j++)
                            {
                                existingInstance.Add(mKeys[j], mValues[j]);
                            }
                        }
                    });
                }

                if (default(TValue) is ValueType)
                {
                    TValue value = input.ReadObject<TValue>();
                    mValues.Add(value);
                }
                else
                {
                    input.ReadSharedResource(delegate(TValue item)
                    {
                        mValues.Add(item);

                        if (mKeys.Count == mNumberOfItem && mValues.Count == mNumberOfItem)
                        {
                            for (int j = 0; j < mNumberOfItem; j++)
                            {
                                existingInstance.Add(mKeys[j], mValues[j]);
                            }
                        }
                    });
                }
                mNumberOfItem++;
                i++;
            }
            return existingInstance;
        } 
    }

    [ContentTypeWriter]
    public class SharedResourceListWriter<T> : ContentTypeWriter<SharedResourceList<T>>
    {
        /// <summary>
        /// Saves sprite sheet data into an XNB file.
        /// </summary>
        protected override void Write(ContentWriter aOutput, SharedResourceList<T> aValue)
        {
            int nbElement = 0;
            nbElement = aValue.Count;

            aOutput.WriteObject(nbElement);

            foreach (T item in aValue)
            {
                aOutput.WriteSharedResource(item);
            }
        }


        /// <summary>
        /// Tells the content pipeline what worker type
        /// will be used to load the sprite sheet data.
        /// </summary>
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(SharedResourceListReader<T>).AssemblyQualifiedName; 
        }


        /// <summary>
        /// Tells the content pipeline what CLR type the sprite sheet
        /// data will be loaded into at runtime.
        /// </summary>
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(SharedResourceList<T>).AssemblyQualifiedName; 

        }
    }

    public class SharedResourceListReader<T> : ContentTypeReader<SharedResourceList<T>>
    {
        protected override SharedResourceList<T> Read(
                ContentReader input,
                SharedResourceList<T> existingInstance)
        {
            if (existingInstance == null)
                existingInstance = new SharedResourceList<T>();

            int nbElement = 0;

            nbElement = input.ReadObject<int>();

            int i = 0;
            while (i < nbElement)
            {
                input.ReadSharedResource((T item) => existingInstance.Add(item));
                i++;
            }

            return existingInstance;
        }
    }
}
