using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Lab05
{
    internal class JSONDataStorage : IStudentDataStorage
    {
        private string jsonFilePath;

        public JSONDataStorage(string jsonFilePath)
        {
            this.jsonFilePath = jsonFilePath;
        }

        public string FilePath => jsonFilePath;

        public List<SinhVien> Load()
        {
            if (!File.Exists(FilePath))
            {
                System.IO.FileStream fs = File.Create(FilePath);
                fs.Close();
            }

            using (StreamReader r = new StreamReader(FilePath))
            {
                string json = r.ReadToEnd();
                if (string.IsNullOrWhiteSpace(json))
                    return new List<SinhVien>();

                List<SinhVien> items = JsonConvert.DeserializeObject<List<SinhVien>>(json);
                return items;
            }
        }

        public void Write(List<SinhVien> sinhViens)
        {
            using (StreamWriter file = File.CreateText(FilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, sinhViens);
            }
        }
    }
}