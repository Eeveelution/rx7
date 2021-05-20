using System;
using System.IO;
using System.Linq;
using System.Reflection;
using _13B_REW.Bancho.Attributes;
using EeveeTools.Helpers;

namespace _13B_REW.Bancho.Packets.Objects {
    public abstract class Serializable {
        public Serializable() { }
        public virtual void ReadFromStream(Stream stream) {
            IOrderedEnumerable<PropertyInfo> properties = from property in this.GetType().GetProperties()
                                                          where Attribute.IsDefined(property, typeof(RetainDeclarationOrderAttribute))
                                                          orderby ((RetainDeclarationOrderAttribute) property.GetCustomAttributes(typeof(RetainDeclarationOrderAttribute), false).Single()).Order
                                                          select property;

            using BanchoReader reader = new(stream);

            foreach (PropertyInfo propertyInfo in properties) {

                string propType = propertyInfo.PropertyType.Name;

                if (propertyInfo.PropertyType.IsEnum) {
                    propType = propertyInfo.PropertyType.GetEnumUnderlyingType().Name;
                }

                switch (propType) {
                    case "Byte":
                        propertyInfo.SetValue(this, reader.ReadByte());
                        break;
                    case "Int32":
                        propertyInfo.SetValue(this, reader.ReadInt32());
                        break;
                    case "Int16":
                        propertyInfo.SetValue(this, reader.ReadInt16());
                        break;
                    case "Int64":
                        propertyInfo.SetValue(this, reader.ReadInt64());
                        break;
                    case "UInt32":
                        propertyInfo.SetValue(this, reader.ReadUInt32());
                        break;
                    case "UInt16":
                        propertyInfo.SetValue(this, reader.ReadUInt16());
                        break;
                    case "UInt64":
                        propertyInfo.SetValue(this, reader.ReadUInt64());
                        break;
                    case "String":
                        propertyInfo.SetValue(this, reader.ReadString());
                        break;
                    case "Single":
                        propertyInfo.SetValue(this, reader.ReadSingle());
                        break;
                    case "Double":
                        propertyInfo.SetValue(this, reader.ReadDouble());
                        break;
                    default:
                        Serializable serializable = (Serializable)Activator.CreateInstance(propertyInfo.PropertyType);
                        serializable?.ReadFromStream(stream);
                        propertyInfo.SetValue(this, serializable);
                        break;
                }
            }
        }
        public virtual void WriteToStream(Stream stream) {
            IOrderedEnumerable<PropertyInfo> properties = from property in this.GetType().GetProperties()
                                                          where Attribute.IsDefined(property, typeof(RetainDeclarationOrderAttribute))
                                                          orderby ((RetainDeclarationOrderAttribute) property.GetCustomAttributes(typeof(RetainDeclarationOrderAttribute), false).Single()).Order
                                                          select property;

            using BanchoWriter writer = new(stream);

            foreach (PropertyInfo propertyInfo in properties) {
                string propType = propertyInfo.PropertyType.Name;

                if (propertyInfo.PropertyType.IsEnum) {
                    propType = propertyInfo.PropertyType.GetEnumUnderlyingType().Name;
                }

                switch (propType) {

                    case "Byte":
                        byte byteValue = (byte)propertyInfo.GetValue(this);
                        writer.Write(byteValue);
                        break;
                    case "Int32":
                        int intValue = (int)propertyInfo.GetValue(this);
                        writer.Write(intValue);
                        break;
                    case "Int16":
                        short shortValue = (short)propertyInfo.GetValue(this);
                        writer.Write(shortValue);
                        break;
                    case "Int64":
                        long longValue = (long)propertyInfo.GetValue(this);
                        writer.Write(longValue);
                        break;
                    case "UInt32":
                        uint uintValue = (uint)propertyInfo.GetValue(this);
                        writer.Write(uintValue);
                        break;
                    case "UInt16":
                        ushort ushortValue = (ushort)propertyInfo.GetValue(this);
                        writer.Write(ushortValue);
                        break;
                    case "UInt64":
                        ulong ulongValue = (ulong)propertyInfo.GetValue(this);
                        writer.Write(ulongValue);
                        break;
                    case "String":
                        string stringValue = (string)propertyInfo.GetValue(this);
                        writer.Write(stringValue);
                        break;
                    case "Single":
                        float singleValue = (float)propertyInfo.GetValue(this);
                        writer.Write(singleValue);
                        break;
                    case "Double":
                        double doubleValue = (double)propertyInfo.GetValue(this);
                        writer.Write(doubleValue);
                        break;


                    default:
                        Serializable serializable = (Serializable) propertyInfo.GetValue(this);
                        serializable?.WriteToStream(stream);
                        break;
                }
            }

        }

        public byte[] ToBytes() {
            MemoryStream stream = new();

            this.WriteToStream(stream);

            return stream.ToArray();
        }
    }
}
