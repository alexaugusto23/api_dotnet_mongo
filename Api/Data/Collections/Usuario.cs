using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Api.Data.Collections
{
    public class Usuario
    {
        public Usuario(int dataId, string dataNome , DateTime dataNascimento, string sexo, double latitude, double longitude)
        {
            this.id = dataId;
            this.nome = dataNome;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }
        
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }
    }
}
