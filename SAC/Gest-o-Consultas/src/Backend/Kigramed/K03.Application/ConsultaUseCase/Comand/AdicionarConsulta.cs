using System;
using Kigramed.K03.Application.ConsultaUseCase.DTO;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ConsultaUseCase.Comand;

public class AdicionarConsulta(IAdicionarRepository<ConsultaModel> repository)
{
    public async Task<string> ExecuteAsync(AdicionarConsultaDTO dto)
    {
        var model = new ConsultaModel
        {
            Id_medico_especialiade = dto.IdMedicoEspecialidade,

            Id_servico = dto.IdServico,

            Id_paciente = dto.IdPaciente,

            Id_estado_consulta = dto.IdEstado,

            Data_consulta = dto.DataConsulta
        };

        return await repository.AddAsync(model);
    }
}
