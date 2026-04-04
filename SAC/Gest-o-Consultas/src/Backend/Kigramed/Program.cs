using Kigramed.K02.Infra.Data;
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
builder.Services.AddScoped<IAdicionarRepository<AuthModel>>();
builder.Services.AddScoped<ILoginRepository>(); 
//Contratos do Cliente 
builder.Services.AddScoped<IAdicionarRepository<ClienteModel>>();
builder.Services.AddScoped<IActualizarRepository<ClienteModel>>();
builder.Services.AddScoped<IListagemRepository<ClienteModel>>();
builder.Services.AddScoped<IPegarpeloNifReporitory<ClienteModel>>();
builder.Services.AddScoped<IPegarpeloTextoRepository<ClienteModel>>();
builder.Services.AddScoped<IRemoverRepository<ClienteModel>>();
//Contratos da Consulta
builder.Services.AddScoped<IAdicionarRepository<ConsultaModel>>();
builder.Services.AddScoped<IActualizarRepository<ConsultaModel>>();
builder.Services.AddScoped<IListagemRepository<ConsultaModel>>();
builder.Services.AddScoped<IPegarpeloId<ConsultaModel>>();
builder.Services.AddScoped<IRemoverRepository<ConsultaModel>>();
//Contratos da Especialidade
builder.Services.AddScoped<IAdicionarRepository<EspecialidadeModel>>();
builder.Services.AddScoped<IActualizarRepository<EspecialidadeModel>>();
builder.Services.AddScoped<IListagemRepository<EspecialidadeModel>>();
builder.Services.AddScoped<IPegarpeloId<EspecialidadeModel>>();
builder.Services.AddScoped<IPegarpeloTextoRepository<EspecialidadeModel>>();
builder.Services.AddScoped<IRemoverRepository<EspecialidadeModel>>();
//Contratos da Perfil
builder.Services.AddScoped<IListagemRepository<EspecialidadeModel>>();
//Contratos do Funcionário
builder.Services.AddScoped<IAdicionarRepository<FuncionarioModel>>();
builder.Services.AddScoped<IActualizarRepository<FuncionarioModel>>();
builder.Services.AddScoped<IListagemRepository<FuncionarioModel>>();
builder.Services.AddScoped<IPegarpeloNifReporitory<FuncionarioModel>>();
builder.Services.AddScoped<IPegarpeloNifReporitory<FuncionarioModel>>();
builder.Services.AddScoped<IRemoverRepository<FuncionarioModel>>();
//Contrato do Paciente
builder.Services.AddScoped<IAdicionarRepository<PacienteModel>>();
builder.Services.AddScoped<IActualizarRepository<PacienteModel>>();
builder.Services.AddScoped<IListagemRepository<PacienteModel>>();
builder.Services.AddScoped<IPegarpeloId<PacienteModel>>();
builder.Services.AddScoped<IPegarpeloNifReporitory<PacienteModel>>();
builder.Services.AddScoped<IPegarpeloTextoRepository<PacienteModel>>();
builder.Services.AddScoped<IRemoverRepository<PacienteModel>>();
//Contratos para o Pagamento
builder.Services.AddScoped<IAdicionarRepository<PagamentoModel>>();
builder.Services.AddScoped<IListagemRepository<PagamentoModel>>();
builder.Services.AddScoped<IPegarpeloId<PagamentoModel>>();
builder.Services.AddScoped<IRemoverRepository<PagamentoModel>>();
//Contratos para o Serviço
builder.Services.AddScoped<IAdicionarRepository<ServicoModel>>();
builder.Services.AddScoped<IActualizarRepository<ServicoModel>>();
builder.Services.AddScoped<IListagemRepository<ServicoModel>>();
builder.Services.AddScoped<IPegarpeloId<ServicoModel>>();
builder.Services.AddScoped<IPegarpeloTextoRepository<ServicoModel>>();
builder.Services.AddScoped<IRemoverRepository<ServicoModel>>();

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
