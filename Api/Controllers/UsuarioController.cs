using System;
using System.Collections.Immutable;
using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Usuario> _infectadosCollection;

        public UsuarioController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Usuario>(typeof(Usuario).Name.ToLower());
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<Usuario>.Filter.Empty).ToList();
            
            return Ok(infectados);
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] UsuarioDto dto)
        {
            var infectado = new Usuario(dto.id, dto.nome, dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadosCollection.InsertOne(infectado);
            
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

                
        [HttpPut]
        public ActionResult AtualizarInfectados([FromBody] UsuarioDto dto)
        {
            var infectado = new Usuario(dto.id, dto.nome, dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadosCollection.UpdateOne(Builders<Usuario>.Filter.Where(_ => _.DataNascimento == dto.DataNascimento), Builders<Usuario>.Update.Set("sexo",dto.Sexo));
           
            return Ok("Atualizado com sucesso");
        }

        [HttpDelete("{dataNasc}")]
        public ActionResult DeletarInfectados(DateTime Datanasc)
        {
            
            _infectadosCollection.DeleteOne(Builders<Usuario>.Filter.Where(_ => _.DataNascimento == Datanasc));
           
            return Ok("Deletado com sucesso");
        }
    }
}
