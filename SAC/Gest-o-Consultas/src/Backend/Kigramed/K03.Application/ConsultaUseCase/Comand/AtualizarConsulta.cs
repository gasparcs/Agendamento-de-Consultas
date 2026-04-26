using System;
using Kigramed.K03.Application.ConsultaUseCase.DTO;
using Kigramed.K04.Domain.D15.Consulta;
using Kigramed.K04.Domain.Interfaces;

namespace Kigramed.K03.Application.ConsultaUseCase.Comand;

public class AtualizarConsulta(IActualizarRepository<ConsultaModel> repository)
{
    public async Task<string> ExecuteAsync(AtualizarConsultaDTO dto)
    {
        var model = new ConsultaModel
        {
            Id = dto.Id,
            Id_medico_especialiade = int.Parse(dto.Consulta_medico_especialiade),
            Id_servico = int.Parse(dto.Consultaservico),
            Id_paciente = int.Parse(dto.Consultapaciente),
            Id_estado_consulta = dto.ConsultaEstado ? 1 : 0,
            Data_consulta = dto.ConsultaData
        };

        return await repository.ActualizarAsync(model);
    }
}
