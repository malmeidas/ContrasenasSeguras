using System.Collections.Generic;
using Newtonsoft.Json;

namespace ContraseñasSeguras.SkyDrive
{
    [JsonObject(MemberSerialization.OptIn)]
    public class clRESTHijos
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "upload_location")]
        public string LocalizacionUpload { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "is_embeddable")]
        public bool IsEmbeddable { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Tipo { get; set; }
    }
}
