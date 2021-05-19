using System;
using System.IO;
using System.Linq;
using System.Reflection;
using EeveeTools.Helpers;
using RX7.Bancho.Attributes;

namespace RX7.Bancho.Objects {
    public abstract class Serializable {
        public Serializable() { }
        public virtual void ReadFromStream(Stream stream) {
            IOrderedEnumerable<PropertyInfo> properties = from property in this.GetType().GetProperties()
                                                          where Attribute.IsDefined(property, typeof(RetainDeclarationOrderAttribute))
                                                          orderby ((RetainDeclarationOrderAttribute) property.GetCustomAttributes(typeof(RetainDeclarationOrderAttribute), false).Single()).Order
                                                          select property;

            using BanchoReader reader = new(stream);

            foreach (PropertyInfo propertyInfo in properties) {
                switch (propertyInfo.PropertyType.Name) {
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
                    default:
                        Serializable serializable = (Serializable)Activator.CreateInstance(propertyInfo.PropertyType);
                        serializable?.ReadFromStream(stream);
                        propertyInfo.SetValue(this, serializable);
                        break;
                }
            }
        }
        public virtual void WriteToStream(Stream stream) {
            
        }
    }
}
