using Kigramed.K03.Application.ClienteUseCase.Comand;
using Kigramed.K03.Application.ClienteUseCase.DTO;
using Kigramed.K03.Application.ClienteUseCase.Queries;
using Kigramed.K03.Application.ConsultaUseCase.Comand;
using Kigramed.K03.Application.ConsultaUseCase.DTO;
using Kigramed.K03.Application.ConsultaUseCase.Queries;
using Kigramed.K03.Application.EspecialidadeUseCase.Comand;
using Kigramed.K03.Application.EspecialidadeUseCase.DTO;
using Kigramed.K03.Application.EspecialidadeUseCase.Queries;
using Kigramed.K03.Application.FuncionarioUseCase.Comand;
using Kigramed.K03.Application.FuncionarioUseCase.DTO;
using Kigramed.K03.Application.FuncionarioUseCase.Queries;
using Kigramed.K03.Application.PacienteUseCase.Comand;
using Kigramed.K03.Application.PacienteUseCase.DTO;
using Kigramed.K03.Application.PacienteUseCase.Queries;
using Kigramed.K03.Application.PerfilUseCase.Queries;
using Kigramed.K03.Application.ServicoUseCase.Comand;
using Kigramed.K03.Application.ServicoUseCase.DTO;
using Kigramed.K03.Application.ServicoUseCase.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kigramed.K01.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController (AdicionarCliente adicionarServices,
     AtualizarCliente atualizarServices,
      ListarClientes listarServices, 
      RemoverCliente removerServices,
      PegarClientePeloNif pegarnifServices,
      PegarClientePeloTexto pegartextoServices,

      AdicionarConsulta adicionarconsultServices,
      ListarConsultas listarconsultaServices,

      AdicionarEspecialidade adicionarespecialidadeServices,
      AtualizarEspecialidade atualizarespecialidadeServices,
      RemoverEspecialidade removerespecialidadeServices,
      ListarEspecialidades listarespecialidadesServices,
      PegarEspecialidadePeloId pegaridespecialidadeServices,
      PegarEspecialidadePeloTexto pegartextoespecialidadeServices,

      AdicionarFuncionario adicionarfuncionarioServices,
      AtualizarFuncionario atualizarfuncionarioServices,
      RemoverFuncionario removerfuncionarioServices,
      ListarFuncionarios listarfuncionariosServices,
      PegarFuncionarioPeloNif pegarniffuncionarioServices,
      PegarFuncionarioPeloTexto pegartextofuncionarioServices,

      AdicionarPaciente adicionarpacientesServices,
      AtualizarPaciente atualizarpacienteServices,
      RemoverPaciente removerpacienteServices,
      ListarPacientes listarpacienteServices,
      PegarPacientePeloID pegaridpacienteServices,
      PegarPacientePeloTexto pegartextopacienteServices,


        ListarPerfis listarperfisServices,

        AdicionarServico adicionarservicoServices,
        AtualizarServico atualizarservicoServices,
        RemoverServico removerservicoServices,
        ListarServicos listarservicoServico,
        PegarServicoPeloId pegarservicoidServices,
        PegarServicoPeloTexto pegarservicotextoServices
    
      )
      : ControllerBase
    {

        // ------------ Cliente -----------//
        //método adicionar
        [HttpPost("cliente")]
        public async Task<IActionResult> AdicionarCliente(AdicionarClienteDTO dto)
        {
            if(!ModelState.IsValid)
            return StatusCode(400, ModelState);
            var resposta = await adicionarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(201, resposta): 
            StatusCode(500, resposta);
        }

        //método actualizar
        [HttpPut("cliente/{nif}")]
        public async Task<IActionResult> AtualizarCliente(string nif, AtualizarClienteDTO dto)
        {
            if (!ModelState.IsValid) 
            return StatusCode(400, ModelState);
            dto.ClienteNif = nif;

            var resposta = await atualizarServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(200, resposta)
            : StatusCode(404, resposta);
        }
        //método listar
        [HttpGet("cliente")]
        public async Task<IActionResult> ListarCliente()
        {
            var resposta = await listarServices.ExecuteAsync();
            return Ok(resposta);
        }

        //método remover
        [HttpDelete("cliente/{nif}")]
        public async Task<IActionResult> RemoverCliente(string nif)
        {
            var resposta = await removerServices.ExecuteAsync(nif);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(404, resposta);
        }

        //método pegar pelo nif
        [HttpGet("cliente/nif/{nif}")]
        public async Task<IActionResult> PegarClientePeloNif(string nif)
        {
            var resposta = await pegarnifServices.ExecuteAsync(nif);
            return resposta is null ? StatusCode(404, "Cliente não encontrado"):
            Ok(resposta);
        }

        //método pegar pelo texto
        [HttpGet("cliente/texto/{texto}")]
        public async Task<IActionResult> PegarClientePeloTexto(string texto)
        {
            var resposta = await pegartextoServices.ExecuteAsync(texto);
            return resposta is null ? StatusCode(404, "Nenhum cliente encontrado") :
            Ok(resposta);
        }

        //  ------------- Consulta ------------//

        [HttpPost("consulta")]
        public async Task<IActionResult> AdicionarConsulta(AdicionarConsultaDTO dto)
        {
            if(!ModelState.IsValid)
            return StatusCode(400, ModelState);
            var resposta = await adicionarconsultServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(201, resposta): 
            StatusCode(500, resposta);
        }

        [HttpGet("consulta")]
        public async Task<IActionResult> ListarConsultas()
        {
            var resposta = await listarconsultaServices.ExecuteAsync();
            return Ok(resposta);
        }

        [HttpPut("consulta/{id}")]
        public async Task<IActionResult> AtualizarConsulta(int id, AtualizarConsultaDTO dto)
        {
            if (!ModelState.IsValid)
                return StatusCode(400, ModelState);

            if (id != dto.Id)
                return StatusCode(400, "ID da consulta não corresponde");

            var resposta = await adicionarconsultServices.ExecuteAsync(new AdicionarConsultaDTO
            {
                IdMedicoEspecialidade = int.Parse(dto.Consulta_medico_especialiade),
                IdServico = int.Parse(dto.Consultaservico),
                IdPaciente = int.Parse(dto.Consultapaciente),
                IdEstado = dto.ConsultaEstado ? 1 : 0,
                DataConsulta = dto.ConsultaData
            });
            return resposta.Contains("sucesso") ? StatusCode(200, resposta) : StatusCode(400, resposta);
        }

        //--------------- Especialidade -----------//

        [HttpPost("especialidade")]
        public async Task<IActionResult> AdicionarEspecialidade(AdicionarEspecialidadeDTO dto)
        {
            if(!ModelState.IsValid)
            return StatusCode(400, ModelState);
            var resposta = await adicionarespecialidadeServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso") ? StatusCode(201, resposta) :
            StatusCode(500, resposta); 
        }

        [HttpPut("especialidade/{id}")]
        public async Task<IActionResult> AtualizarEspecialidade(int id, AtualizarEspecialidadeDTO dto)
        {
            if (!ModelState.IsValid)
            return StatusCode(400, ModelState);
            dto.EspecialidadeId = id;
            var resposta = await atualizarespecialidadeServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(500, resposta);
        }

        [HttpGet("especialidade")] 
        public async Task<IActionResult> ListarEspecialidades()
        {
            var resposta = await listarespecialidadesServices.ExecuteAsync();
            return Ok(resposta);
        }

        [HttpGet("especialidade/id/{id}")]
        public async Task<IActionResult> PegarEspecialidadePeloId(int id)
        {
            var resposta = await pegaridespecialidadeServices.ExecuteAsync(id);
            return resposta is null ? StatusCode(404, "Especialidade não encontrada"):
            Ok(resposta);
        }

        [HttpGet("especialidade/texto/{texto}")] 
        public async Task<IActionResult> PegarEspecialidadePeloTexto(string texto)
        {
            var resposta = await pegartextoespecialidadeServices.ExecuteAsync(texto);
            return resposta is null ? StatusCode(404, "Nenhuma especialidade encontrada com o texto fornecido"):
            Ok(resposta);
        }

         [HttpDelete("especialidade/{id}")]
        public async Task<IActionResult> RemoverEspecialidade(int id)
        {
            var resposta = await removerespecialidadeServices.ExecuteAsync(id);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(404, resposta);
        }

        //--------------- funcionario --------------//

        [HttpPost("funcionario")]
        public async Task<IActionResult> AdicionarFuncionario(AdicionarFuncionarioDTO dto)
        {
            if(!ModelState.IsValid)
            
            return StatusCode(400, ModelState);

            var resposta = await adicionarfuncionarioServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(201, resposta) :
            StatusCode(500, resposta);            
        }

        [HttpGet("funcionario")]
        public async Task<IActionResult> ListarFuncionarios()
        {
             var resposta = await listarfuncionariosServices.ExecuteAsync();
             return Ok(resposta);
        }

        [HttpGet("funcionario/nif/{nif}")] 
        public async Task<IActionResult> PegarFuncionarioPeloNif(string nif)
        {
            var resposta = await pegarniffuncionarioServices.ExecuteAsync(nif);
            return resposta is null? StatusCode(404, "Funcionário não encontrado"):
            Ok(resposta);
        } 

        [HttpGet("funcionario/texto/{texto}")]
        public async Task<IActionResult> PegarFuncionarioPeloTexto(string texto)
        {
            var resposta = await pegartextofuncionarioServices.ExecuteAsync(texto);
            return resposta is null? StatusCode(404, "Nenhum funcionário encontrado"):
            Ok(resposta); 
        } 

        [HttpPut("funcionario/{nif}")]
         public async Task<IActionResult> AtualizarFuncionario(string nif, AtualizarFuncionarioDTO dto)
        {
            if (!ModelState.IsValid)
            return StatusCode(400, ModelState);

            dto.FuncionaioNif = nif;

            var resposta = await atualizarfuncionarioServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(200, resposta)
            : StatusCode(404, resposta);
        }

        [HttpDelete("funcionario/{nif}")]
        public async Task<IActionResult> RemoverFuncionario(string nif)
        {
            var resposta = await removerfuncionarioServices.ExecuteAsync(nif);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(404, resposta);
        }

        //------------------ paciente ----------------//

         [HttpPost]
        public async Task<IActionResult> AdicionarPaciente(AdicionarPacienteDTO dto)
        {
            if(!ModelState.IsValid)
            return StatusCode(400, ModelState);
            var resposta = await adicionarpacientesServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(201, resposta) :
            StatusCode(500, resposta);            
        }

         [HttpPut("paciente/{id}")]
        public async Task<IActionResult> AtualizarPaciente(int id, AtualizarPacienteDTO dto)
        {
            if (!ModelState.IsValid)
            return StatusCode(400, ModelState);

            dto.IdPaciente = id;

            var resposta = await atualizarpacienteServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(200, resposta)
            : StatusCode(404, resposta);
        }

         [HttpDelete("paciente/{id}")]
        public async Task<IActionResult> RemoverPaciente(int id)
        {
            var resposta = await removerpacienteServices.ExecuteAsync(id);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(404, resposta);
        }

        [HttpGet("paciente")]
        public async Task<IActionResult> ListarPacientes()
        {
             var resposta = await listarpacienteServices.ExecuteAsync();
             return Ok(resposta);
        }
        
        [HttpGet("paciente/id/{id}")]
        public async Task<IActionResult> PegarPacientePeloId(int id)
        {
            var resposta = await pegaridpacienteServices.ExecuteAsync(id);
            return resposta is null ? StatusCode(404, "Paciente não encontrado"):
            Ok(resposta);
        }

         [HttpGet("paciente/texto/{texto}")]
        public async Task<IActionResult> PegarPacientePeloTexto(string texto)
        {
            var resposta = await pegartextopacienteServices.ExecuteAsync(texto);
            return resposta is null ? StatusCode(404, "Nenhum paciente encontrado com o texto fornecido"):
            Ok(resposta);
        }

        //-------------- perfil -----------------//

        [HttpGet("perfil")]
        public async Task<IActionResult> ListarPerfis()
        {
            var resposta = await listarperfisServices.ExecuteAsync();
            return Ok(resposta);
        }

        //-------------- serviço --------------//

        [HttpPost("servico")]
        public async Task<IActionResult> AdicionarServico(AdicionarServicoDTO dto)
        {
            if(!ModelState.IsValid)
            return StatusCode(400, ModelState);
            var resposta = await adicionarservicoServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso")? StatusCode(201, resposta): 
            StatusCode(500, resposta);
        }
        
        [HttpPut("servico/{id}")]
        public async Task<IActionResult> AtualizarServico(int id, AtualizarServicoDTO dto)
        {
            if (!ModelState.IsValid)
            return StatusCode(400, ModelState);

            dto.ServicoId = id;    
            var resposta = await atualizarservicoServices.ExecuteAsync(dto);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(500, resposta);
        }

        [HttpDelete("servico/{id}")]   
        public async Task<IActionResult> RemoverServico(int id)
        {
            var resposta = await removerservicoServices.ExecuteAsync(id);
            return resposta.Contains("sucesso") ? StatusCode(200, resposta):
            StatusCode(404, resposta);
        }

        [HttpGet("servico")] 
        public async Task<IActionResult> ListarServicos()
        {
            var resposta = await listarservicoServico.ExecuteAsync();
            return Ok(resposta);
        }

         [HttpGet("servico/id/{id}")]
        public async Task<IActionResult> PegarServicoPeloId(int id)
        {
            var resposta = await pegarservicoidServices.ExecuteAsync(id);
            return resposta is null ? StatusCode(404, "Servico não encontrado"):
            Ok(resposta);
        }

         [HttpGet("servico/texto/{texto}")]
        public async Task<IActionResult> PegarServicoPeloTexto(string texto)
        {
            var resposta = await pegarservicotextoServices.ExecuteAsync(texto);
            return resposta is null ? StatusCode(404, "Nenhum servico encontrado com o texto fornecido"):
            Ok(resposta);
        }
    }
}
