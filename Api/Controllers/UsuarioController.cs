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
        IMongoCollection<Usuario> _usuarioCollection;

        public UsuarioController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _usuarioCollection = _mongoDB.DB.GetCollection<Usuario>(typeof(Usuario).Name.ToLower());
        }

        [HttpGet]
        public ActionResult Obter()
        {
            var usuarios = _usuarioCollection.Find(Builders<Usuario>.Filter.Empty).ToList();
            
            return Ok(usuarios);
        }

        [HttpPost]
        public ActionResult Salvar([FromBody] UsuarioDto dto)
        {
            var usuarios = new Usuario(dto.Id, dto.Nome, dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _usuarioCollection.InsertOne(usuarios);
            
            return StatusCode(201, "Adicionado com sucesso");
        }

                
        [HttpPut]
        public ActionResult Atualizar([FromBody] UsuarioDto dto)
        {
            var usuarios = new Usuario(dto.Id, dto.Nome, dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _usuarioCollection.UpdateOne(Builders<Usuario>.Filter.Where(_ => _.Id == dto.Id), Builders<Usuario>.Update.Set("sexo",dto.Sexo));
           
            return Ok("Atualizado com sucesso");
        }

        [HttpDelete("{dataId}")]
        public ActionResult Deletar(int DataId)
        {
            
            _usuarioCollection.DeleteOne(Builders<Usuario>.Filter.Where(_ => _.Id == DataId));
           
            return Ok("Deletado com sucesso");
        }
    }
}
