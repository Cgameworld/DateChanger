using System.IO;
using ColossalFramework.IO;
using ICities;
using UnityEngine;

namespace DateChanger
{
    public class SerializableDataExtension : ISerializableDataExtension
    {
        public const string DataID = "DateChangerData";
        public const uint DataVersion = 0;
        private ISerializableData serializedData;

        public void OnCreated(ISerializableData serializedData)
        {
            this.serializedData = serializedData;
        }

        public void OnReleased()
        {
            serializedData = null;
        }

        public void OnLoadData()
        {
            byte[] data = serializedData.LoadData(DataID);

            if (data == null)
            {
                return;
            }

            using (var stream = new MemoryStream(data))
            {
                DataSerializer.Deserialize<SavedValues.Data>(stream, DataSerializer.Mode.Memory);
            }
        }

        public void OnSaveData()
        {
            byte[] data;

            using (var stream = new MemoryStream())
            {
                DataSerializer.Serialize(stream, DataSerializer.Mode.Memory, DataVersion, new SavedValues.Data());
                data = stream.ToArray();
            }

            serializedData.SaveData(DataID, data);
        }
    }
}
