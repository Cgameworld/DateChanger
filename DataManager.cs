using ColossalFramework.IO;
using ICities;
using System.IO;
using UnityEngine;

namespace MakeHistorical
{
    public class MakeHistoricalDataManager : SerializableDataExtensionBase
    {
        // The data object of our mod
        private MakeHistoricalData _data;

        public override void OnLoadData()
        {
            // Get bytes from savegame
            byte[] bytes = serializableDataManager.LoadData(MakeHistoricalData.DataId);
            if (bytes != null)
            {
                // Convert the bytes to MakeHistoricalData object
                using (var stream = new MemoryStream(bytes))
                {
                    _data = DataSerializer.Deserialize<MakeHistoricalData>(stream, DataSerializer.Mode.Memory);
                }

                Debug.LogFormat("Data loaded (Size in bytes: {0})", bytes.Length);
            }
            else
            {
                _data = new MakeHistoricalData();

                Debug.Log("Data created");
            }
        }

        public override void OnSaveData()
        {
            byte[] bytes;

            // Convert the MakeHistoricalData object to bytes
            using (var stream = new MemoryStream())
            {
                DataSerializer.Serialize(stream, DataSerializer.Mode.Memory, MakeHistoricalData.DataVersion, _data);
                bytes = stream.ToArray();
            }

            // Save bytes in savegame
            serializableDataManager.SaveData(MakeHistoricalData.DataId, bytes);

            Debug.LogFormat("Data saved (Size in bytes: {0})", bytes.Length);
        }
    }
}