using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models
{
    public partial class AgentesVenta
    {
        public int IdAgentesVenta { get; set; }

        public int IdAgente { get; set; }

        public int IdCliente { get; set; }

        [JsonIgnore]
        public Usuario? Agente { get; set; }

        [JsonIgnore]
        public Usuario? Cliente { get; set; }
    }
}
