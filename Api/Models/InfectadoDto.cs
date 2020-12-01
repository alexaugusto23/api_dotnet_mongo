using System;

namespace Api.Models
{
    public class UsuarioDto
    {   
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}