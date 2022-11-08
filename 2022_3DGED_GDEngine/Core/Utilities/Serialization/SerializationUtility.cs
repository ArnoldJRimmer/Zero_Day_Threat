using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace GD.Engine.Utilities
{
    /// <summary>
    /// Provides XML serialization functionality
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/framework/wcf/samples/datacontractserializer-sample"/>
    public sealed class SerializationUtility
    {
        public static void Save(string name, object obj)
        {
            var dataContractSerializer = new DataContractSerializer(obj.GetType()); //TODO - add check on Type to ensure its serializable
            var xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "\t";
            var xmlWriter = XmlWriter.Create(name, xmlSettings);

            dataContractSerializer.WriteObject(xmlWriter, obj);
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        public static object Load(string name, System.Type type)
        {
            var fStream = new FileStream(name, FileMode.Open);
            var textReader = XmlDictionaryReader.CreateTextReader(fStream, new XmlDictionaryReaderQuotas());
            var objSerializer = new DataContractSerializer(type); //TODO - add check on Type to ensure its serializable

            object deserializedObject = objSerializer.ReadObject(textReader, true);
            textReader.Close();
            fStream.Close();
            return deserializedObject;
        }
    }
}