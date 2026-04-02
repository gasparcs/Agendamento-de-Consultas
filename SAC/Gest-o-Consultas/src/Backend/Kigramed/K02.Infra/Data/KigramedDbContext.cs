using System;
using Kigramed.K04.Domain.D01.Perfil;
using Kigramed.K04.Domain.D02.Funcionario;
using Kigramed.K04.Domain.D03.TipoContacto;
using Kigramed.K04.Domain.D04.Contacto;
using Kigramed.K04.Domain.D05.Auth;
using Kigramed.K04.Domain.D06.Especialidade;
using Kigramed.K04.Domain.D07.MedicoEspecialidade;
using Kigramed.K04.Domain.D08.Servico;
using Kigramed.K04.Domain.D09.Cliente;
using Kigramed.K04.Domain.D10.Genero;
using Kigramed.K04.Domain.D11.ClientePaciente;
using Kigramed.K04.Domain.D12.Paciente;
using Kigramed.K04.Domain.D13.EstadoConsulta;
using Kigramed.K04.Domain.D14.Consulta;
using Kigramed.K04.Domain.D14.Pagamento;
using Kigramed.K04.Domain.D15.Permissao;
using Kigramed.K04.Domain.D16.PerfilPermissao;
using Kigramed.K04.Domain.D18.PagamentoConsulta;
using Kigramed.K04.Domain.D19.MedicoConsulta;
using Microsoft.EntityFrameworkCore;

namespace Kigramed.K02.Infra.Data;

public class KigramedDbContext(DbContextOptions<KigramedDbContext> options) : DbContext(options)
{
    public DbSet<PerfilModel> Tabelatb01_perfil{get;set;}
    public DbSet<FuncionarioModel> Tabelatb02_funcionario{get;set;}
    public DbSet<TipoContactoModel> Tabelatb03_tipo_contato{get;set;}
    public DbSet<ContactoModel> Tabelatb04_contato{get;set;}
    public DbSet<AuthModel> Tabelatb05_auth{get;set;}
    public DbSet<EspecialidadeModel> Tabelatb06_especialidade{get;set;}
    public DbSet<MedicoEspecilidadeModel> Tabelatb07_medico_especialidade{get;set;}
    public DbSet<ServicoModel> Tabelatb08_servico{get;set;}
    public DbSet<ClienteModel> Tabelatb09_cliente{get;set;}
    public DbSet<GeneroModel> Tabelatb10_genero{get;set;}
    public DbSet<ClientePacienteModel> Tabelatb11_cliente_paciente{get;set;}
    public DbSet<PacienteModel> Tabelatb12_paciente{get;set;}
    public DbSet<EstadoConsultaModel> Tabelatb13_estado_consulta{get;set;}
    public DbSet<PagamentoModel> Tabelatb14_pagamento{get;set;}
    public DbSet<ConsultaModel> Tabelatb15_consulta{get;set;}
    public DbSet<PermissaoModel> Tabelatb16_permissoes{get;set;}
    public DbSet<PerfilPermissaoModel> Tabelatb17_peril_permissoes{get;set;}
    public DbSet<PagamentoConsultaModel> Tabelatb18_pagamento_consulta{get;set;}
    public DbSet<MedicoConsultaModel> Tabelatb19_medico_consulta{get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PerfilModel>(entity =>
        {
            entity.HasMany(pf =>pf.Funcionarios).WithOne(f =>f.Perfil).HasForeignKey(fk =>fk.Id_Perfil);
            entity.HasMany(pf =>pf.PerfisPermissoes).WithOne(pp =>pp.Perfil).HasForeignKey(fk =>fk.Id_perfil);
        });

        modelBuilder.Entity<FuncionarioModel>(entity =>
        {
            entity.HasMany(f => f.Contactos).WithOne(c => c.Funcionario ).HasForeignKey (fk => fk.Nif_funcionario);

            entity.HasOne(f => f.Auth).WithOne(a => a.Funcionario).HasForeignKey<AuthModel>(fk => fk.Nif_funcionario);

            entity.HasMany(f => f.MedicoEspecialidades).WithOne(me => me.Funcionario).HasForeignKey(fk => fk.Nif_funcionario);

            entity.HasMany(f => f.Pagamentos).WithOne(p => p.Funcionario).HasForeignKey(fk => fk.Nif_funcionario); 
        });


         modelBuilder.Entity<TipoContactoModel>(entity =>
        {
            entity.HasMany(tc =>tc.Contactos).WithOne(c => c.TipoContacto).HasForeignKey(fk => fk.Id_tipo_contacto);
        });

        modelBuilder.Entity<ContactoModel>( entity =>
        {
            entity.HasOne(c => c.Cliente).WithMany(cl => cl.Contactos).HasForeignKey(fk => fk.Nif_cliente);
        });

       modelBuilder.Entity<EspecialidadeModel>( entity =>
       {
            entity.HasMany(e => e.MedicoEspecialidades).WithOne(me => me.Especialidade).HasForeignKey(fk => fk.Id_especialidade);

            entity.HasMany(e => e.Servicos).WithOne(s => s.Especialidade).HasForeignKey(fk => fk.Id_especialidade);
       });

       modelBuilder.Entity<MedicoEspecilidadeModel>( entity =>
       {
           entity.HasMany(me => me.MedicoConsultas).WithOne(mc => mc.MedicoEspecialidade).HasForeignKey(fk => fk.Id_medico_especialidade);
       });

       modelBuilder.Entity<ServicoModel>( entity =>
       {
            entity.HasMany(s => s.Consultas).WithOne(c => c.Servico).HasForeignKey(fk => fk.Id_servico);
       });

       modelBuilder.Entity<ClienteModel>( entity =>
       {
           entity.HasMany(c => c.Pacientes).WithOne(p => p.Cliente).HasForeignKey(fk => fk.Id_cliente);

           entity.HasMany(c => c.Pagamentos).WithOne(p => p.Cliente).HasForeignKey( fk => fk.Id_cliente);
        });

        modelBuilder.Entity<GeneroModel>( entity =>
        {
            entity.HasMany(g => g.Pacientes).WithOne(p => p.Genero).HasForeignKey(fk => fk.Id_genero);
        });

        modelBuilder.Entity<ClientePacienteModel>( entity =>
        {
            entity.HasMany(cp => cp.Pacientes).WithOne(p => p.ClientePaciente).HasForeignKey(fk => fk.Id_cliente_paciente);
        });

        modelBuilder.Entity<PacienteModel>( entity =>
        {
           entity.HasMany(p => p.Consultas).WithOne(c => c.Paciente).HasForeignKey(fk => fk.Id_paciente);
        });

          modelBuilder.Entity<EstadoConsultaModel>( entity =>
        {
             entity.HasMany(ec => ec.Consultas).WithOne(c => c.EstadoConsulta).HasForeignKey(fk => fk.Id_estado_consulta);
        });

          modelBuilder.Entity<PagamentoModel>( entity =>
         {
              entity.HasMany(p => p.PagamentoConsultas).WithOne(pc => pc.Pagamento).HasForeignKey(fk => fk.Id_Pagamento);
         });
    }
}
