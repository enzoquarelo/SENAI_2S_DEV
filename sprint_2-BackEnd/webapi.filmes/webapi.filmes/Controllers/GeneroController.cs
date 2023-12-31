﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.filmes.Domains;
using webapi.filmes.Interfaces;
using webapi.filmes.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webapi.filmes.Controllers
{
    // define que a rota de uma requisicao sera no seguinte formato
    //dominio/api/controller
    //ex: hhtp://localhost:5000/api/genero
    [Route("api/[controller]")]

    //define que eh um controlador de api
    [ApiController]

    //define que o tipo de resposta da api sera no formato json
    [Produces("application/json")]

    //metodo controlador que herda da controller base
    //onde sera criado os endpoints (rotas)
    public class GeneroController : ControllerBase
    {
        /// <summary>
        /// objeto _generoRepository que ira receber todos os metodos definidos na interface
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        //instancia o objeto _generoRepository para que haja referencia aos metodos no repositorio
        public GeneroController()
        {
            _generoRepository = new GeneroRepository();
        }


        /// <summary>
        ///endpoint que aciona o metodo listarTodos do repositorio e retorna a resposta para o usuario(front-end)
        /// </summary>

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //cria uma lista que recebe os dados da requisicao
                List<GeneroDomain> listaGeneros = _generoRepository.ListarTodos();

                //retorna a lista no formato JSON com o status code OK(200)
                return Ok(listaGeneros);
            }
            catch (Exception erro)
            {
                //retorna status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }

        }

        /// <summary>
        /// endpoint que aciona o metodo de cadastro do genero 
        /// </summary>
        /// <param name="novoGenero">objeto recebido na requisicao</param>
        /// <returns>status code 201(created)</returns>
        [HttpPost]
        public IActionResult Post(GeneroDomain novoGenero)
        {

            try
            {
                //fazendo a chamada para o metodo cadastrar passando o objeto como parametro 
                _generoRepository.Cadastrar(novoGenero);

                //retorna um status code 201(created)
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                //retorna status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// endpoint que aciona o metodo de deletar um genero 
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // chama o metodo de deletar
                _generoRepository.Deletar(id);

                //retorna um status code 204(delete)
                return StatusCode(204);
            }
            catch (Exception erro)
            {
                //retorna status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }


        /// <summary>
        ///endpoint que busca um determinado genero apartir do seu Id
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);

            try
            {
                GeneroDomain generoEncontrado = _generoRepository.BuscarPorId(id);

                if (generoEncontrado != null)
                {
                    return Ok(generoEncontrado);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }


        /// <summary>
        ///endpoint que te permite atualizar um genero a partir do seu corpo(Id)
        /// </summary>
        [HttpPut]
        public IActionResult Put(GeneroDomain genero)
        {
            try
            {
                _generoRepository.AtualizarIdCorpo(genero);

                return StatusCode(200);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }



        /// <summary>
        /// atualiza um gênero pelo id passado por sua url
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult UpdateByIdUrl(int id, GeneroDomain genero)
        {
            try
            {
                GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);

                if (generoBuscado != null)
                {
                    try
                    {
                        _generoRepository.AtualizarIdUrl(id, genero);

                        return StatusCode(204);
                    }
                    catch (Exception err)
                    {
                        return BadRequest(err.Message);
                    }
                }

                return NotFound("O gênero não existe.");
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
    }

