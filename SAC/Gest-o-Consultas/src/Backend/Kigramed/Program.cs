using Kigramed.K02.Infra.Data;
using Kigramed.K02.Infra.Reporitory.Auth;
using Kigramed.K02.Infra.Reporitory.Cliente;
using Kigramed.K02.Infra.Reporitory.Consulta;
using Kigramed.K02.Infra.Reporitory.Especialidade;
using Kigramed.K02.Infra.Reporitory.Ferfil;
using Kigramed.K02.Infra.Reporitory.Funcionario;
using Kigramed.K02.Infra.Reporitory.Paciente;
using Kigramed.K02.Infra.Reporitory.Pagamento;
using Kigramed.K02.Infra.Reporitory.Servico;
using Kigramed.K03.Application.ClienteUseCase.Comand;
using Kigramed.K03.Application.ClienteUseCase.Queries;
using Kigramed.K03.Application.PerfilUseCase.Queries;
using Kigramed.K04.Domain.D01.Perfil;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D05.Auth;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.D14.Pagamento;
using Kigramed.K04.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
string conexao= builder.Configuration.GetConnectionString("ConexaoLocal")!;
builder.Services.AddDbContext<KigramedDbContext>(options => options.UseNpgsql(conexao));

//Contratos da AuthModel
builder.Services.AddScoped<IAdicionarRepository<AuthModel>, AdicionarAuthRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepositoy>(); 
//Contratos do Cliente 
builder.Services.AddScoped<IAdicionarRepository<ClienteModel>, AdicionarClienteRepository>();
builder.Services.AddScoped<IActualizarRepository<ClienteModel>, AtualizarClienteRepository>();
builder.Services.AddScoped<IListagemRepository<ClienteModel>, ListarClientesRepository>();
builder.Services.AddScoped<IPegarpeloNifReporitory<ClienteModel>, PegarClientepeloNifRepository>();
builder.Services.AddScoped<IPegarpeloTextoRepository<ClienteModel>,PegarClientepeloTextoRepository >();
builder.Services.AddScoped<IRemoverRepository<ClienteModel>, RemoverClienteRepository>();
//Contratos da Consulta
builder.Services.AddScoped<IAdicionarRepository<ConsultaModel>, AdicionarConsultaRepository>();
builder.Services.AddScoped<IActualizarRepository<ConsultaModel>, AtualizarConsultaRepository>();
builder.Services.AddScoped<IListagemRepository<ConsultaModel>, ListarConsultaRepository>();
builder.Services.AddScoped<IPegarpeloId<ConsultaModel>, PegarIdConsultaRepository>();
builder.Services.AddScoped<IRemoverRepository<ConsultaModel>, RemoverConsultaRepository>();
//Contratos da Especialidade
builder.Services.AddScoped<IAdicionarRepository<EspecialidadeModel>, AddEspecialidadeRepository>();
builder.Services.AddScoped<IActualizarRepository<EspecialidadeModel>, AtualizarEspecialidadeRepository>();
builder.Services.AddScoped<IListagemRepository<EspecialidadeModel>, ListarEspecialidadeRepository>();
builder.Services.AddScoped<IPegarpeloId<EspecialidadeModel>, PegaridEspecialidadeRepository>();
builder.Services.AddScoped<IPegarpeloTextoRepository<EspecialidadeModel>, PegartextoEspecialidadeRepository>();
builder.Services.AddScoped<IRemoverRepository<EspecialidadeModel>, RemoverEspecialidadeRepository>();
//Contratos da Perfil
builder.Services.AddScoped<IListagemRepository<PerfilModel>, ListarPerfilRepository>();
//Contratos do Funcionário
builder.Services.AddScoped<IAdicionarRepository<FuncionarioModel>, AddFuncionarioRepository>();
builder.Services.AddScoped<IActualizarRepository<FuncionarioModel>, AtualizarFuncionarioRepository>();
builder.Services.AddScoped<IListagemRepository<FuncionarioModel>, ListarFuncionarioRepository>();
builder.Services.AddScoped<IPegarpeloNifReporitory<FuncionarioModel>, PegarnifFuncionarioRepository>();
builder.Services.AddScoped<IPegarpeloTextoRepository<FuncionarioModel>, PegartextoFuncionarioRepository>();
builder.Services.AddScoped<IRemoverRepository<FuncionarioModel>, RemoverFuncionarioRepository>();
//Contrato do Paciente
builder.Services.AddScoped<IAdicionarRepository<PacienteModel>, AdicionarPacienteRepository>();
builder.Services.AddScoped<IActualizarRepository<PacienteModel>, AtualizarPacienteRepository>();
builder.Services.AddScoped<IListagemRepository<PacienteModel>, ListarPacientesRepository>();
builder.Services.AddScoped<IPegarpeloId<PacienteModel>, PegarPacientepeloIDRepository>();
builder.Services.AddScoped<IPegarpeloTextoRepository<PacienteModel>, PegarPacientepeloTextoRepository>();
builder.Services.AddScoped<IRemoverRepository<PacienteModel>, RemoverPacienteRepository>();
//Contratos para o Pagamento
builder.Services.AddScoped<IAdicionarRepository<PagamentoModel>, AdicionarPagamentoRepository>();
builder.Services.AddScoped<IListagemRepository<PagamentoModel>, ListarPagamentoRepository>();
builder.Services.AddScoped<IPegarpeloId<PagamentoModel>, PegarIdPagamentoRepository>();
builder.Services.AddScoped<IRemoverRepository<PagamentoModel>, RemoverPagamentoRepository>();
//Contratos para o Serviço
builder.Services.AddScoped<IAdicionarRepository<ServicoModel>, AdicionarServicoRepository>();
builder.Services.AddScoped<IActualizarRepository<ServicoModel>, AtualizarServicoRepository>();
builder.Services.AddScoped<IListagemRepository<ServicoModel>,ListarServicoRepository>();
builder.Services.AddScoped<IPegarpeloId<ServicoModel>, PegarIdServicoRepository>();
builder.Services.AddScoped<IPegarpeloTextoRepository<ServicoModel>, PegarTextoServicoRepository>();
builder.Services.AddScoped<IRemoverRepository<ServicoModel>, RemoverServicoRepository>();

//casos de usos cliendemodel
builder.Services.AddTransient<AdicionarCliente>();
builder.Services.AddTransient<AtualizarCliente>();
builder.Services.AddTransient<ListarClientes>();
builder.Services.AddTransient<RemoverCliente>();
builder.Services.AddTransient<PegarClientePeloNif>();
builder.Services.AddTransient<PegarClientePeloTexto>();

//casos de uso perfilmodel
builder.Services.AddTransient<ListarPerfis>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
