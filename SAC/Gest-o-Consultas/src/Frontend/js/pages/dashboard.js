/**
 * Kigramed Frontend - Dashboard Page
 */

function renderDashboard() {
    const app = document.getElementById('app');
    const user = appStore.get('user');
    const isSecretaria = String(user?.role || '').toLowerCase() === 'secretaria';

    if (isSecretaria) {
        app.innerHTML = `
            ${renderSidebar()}
            <div class="main-content">
                <div class="header">
                    <div>
                        <h1 class="header-title">Dashboard da Secretaria</h1>
                        <p style="color: var(--gray-500);">Bem-vindo, ${user?.nome || 'Usuario'}</p>
                    </div>
                    <div style="display: flex; gap: 12px;">
                        <button class="btn btn-secondary" onclick="router.navigate('consultas')">
                            <i data-lucide="calendar-plus"></i>
                            Nova Consulta
                        </button>
                        <button class="btn btn-primary" onclick="router.navigate('pacientes')">
                            <i data-lucide="user-plus"></i>
                            Novo Paciente
                        </button>
                    </div>
                </div>

                <div class="grid grid-cols-4" style="margin-bottom: 24px;">
                    <div class="stat-card animate-fade-in">
                        <div style="display: flex; align-items: center; gap: 16px;">
                            <div class="stat-icon" style="background: #dbeafe;">
                                <i data-lucide="calendar-days" style="color: #1e40af;"></i>
                            </div>
                            <div>
                                <div class="stat-value" id="sec-stat-consultas-hoje">0</div>
                                <div class="stat-label">Consultas Hoje</div>
                            </div>
                        </div>
                    </div>
                    <div class="stat-card animate-fade-in" style="animation-delay: 0.1s;">
                        <div style="display: flex; align-items: center; gap: 16px;">
                            <div class="stat-icon" style="background: #d1fae5;">
                                <i data-lucide="users" style="color: #065f46;"></i>
                            </div>
                            <div>
                                <div class="stat-value" id="sec-stat-pacientes">0</div>
                                <div class="stat-label">Pacientes</div>
                            </div>
                        </div>
                    </div>
                    <div class="stat-card animate-fade-in" style="animation-delay: 0.2s;">
                        <div style="display: flex; align-items: center; gap: 16px;">
                            <div class="stat-icon" style="background: #fef3c7;">
                                <i data-lucide="building-2" style="color: #92400e;"></i>
                            </div>
                            <div>
                                <div class="stat-value" id="sec-stat-clientes">0</div>
                                <div class="stat-label">Clientes</div>
                            </div>
                        </div>
                    </div>
                    <div class="stat-card animate-fade-in" style="animation-delay: 0.3s;">
                        <div style="display: flex; align-items: center; gap: 16px;">
                            <div class="stat-icon" style="background: #fce7f3;">
                                <i data-lucide="briefcase-medical" style="color: #9d174d;"></i>
                            </div>
                            <div>
                                <div class="stat-value" id="sec-stat-servicos">0</div>
                                <div class="stat-label">Servicos Ativos</div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="grid grid-cols-2">
                    <div class="card">
                        <div class="card-header">
                            <i data-lucide="clock-3"></i>
                            Proximas Consultas
                        </div>
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Data/Hora</th>
                                    <th>Paciente</th>
                                    <th>Cliente</th>
                                    <th>Servico</th>
                                    <th>Estado</th>
                                </tr>
                            </thead>
                            <tbody id="sec-proximas-consultas">
                                <tr>
                                    <td colspan="5" style="text-align: center; padding: 40px;">
                                        <div class="loading"><div class="spinner"></div></div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                    <div class="card">
                        <div class="card-header">
                            <i data-lucide="stethoscope"></i>
                            Especialidades Disponiveis
                        </div>
                        <div id="sec-especialidades-list">
                            <div class="loading"><div class="spinner"></div></div>
                        </div>
                    </div>
                </div>
            </div>
        `;

        if (window.lucide) {
            lucide.createIcons();
        }

        loadSecretariaDashboardData();
        return;
    }

    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Dashboard</h1>
                    <p style="color: var(--gray-500);">Bem-vindo, ${user?.nome || 'Usuario'}</p>
                </div>
                <div style="display: flex; gap: 12px;">
                    <button class="btn btn-secondary" onclick="router.navigate('consultas')">
                        <i data-lucide="calendar"></i>
                        Nova Consulta
                    </button>
                </div>
            </div>

            <div class="grid grid-cols-4" style="margin-bottom: 24px;">
                <div class="stat-card animate-fade-in">
                    <div style="display: flex; align-items: center; gap: 16px;">
                        <div class="stat-icon" style="background: #dbeafe;">
                            <i data-lucide="calendar" style="color: #1e40af;"></i>
                        </div>
                        <div>
                            <div class="stat-value" id="stat-consultas">0</div>
                            <div class="stat-label">Consultas Hoje</div>
                        </div>
                    </div>
                </div>

                <div class="stat-card animate-fade-in" style="animation-delay: 0.1s;">
                    <div style="display: flex; align-items: center; gap: 16px;">
                        <div class="stat-icon" style="background: #d1fae5;">
                            <i data-lucide="users" style="color: #065f46;"></i>
                        </div>
                        <div>
                            <div class="stat-value" id="stat-pacientes">0</div>
                            <div class="stat-label">Pacientes</div>
                        </div>
                    </div>
                </div>

                <div class="stat-card animate-fade-in" style="animation-delay: 0.2s;">
                    <div style="display: flex; align-items: center; gap: 16px;">
                        <div class="stat-icon" style="background: #fef3c7;">
                            <i data-lucide="dollar-sign" style="color: #92400e;"></i>
                        </div>
                        <div>
                            <div class="stat-value" id="stat-faturado">0</div>
                            <div class="stat-label">Faturado (AOA)</div>
                        </div>
                    </div>
                </div>

                <div class="stat-card animate-fade-in" style="animation-delay: 0.3s;">
                    <div style="display: flex; align-items: center; gap: 16px;">
                        <div class="stat-icon" style="background: #fce7f3;">
                            <i data-lucide="activity" style="color: #9d174d;"></i>
                        </div>
                        <div>
                            <div class="stat-value" id="stat-taxa">0%</div>
                            <div class="stat-label">Taxa de Ocupacao</div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card animate-fade-in" style="animation-delay: 0.4s;">
                <div class="card-header">
                    <i data-lucide="clock"></i>
                    Proximas Consultas
                </div>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Paciente</th>
                            <th>Especialidade</th>
                            <th>Medico</th>
                            <th>Data/Hora</th>
                            <th>Estado</th>
                            <th>Acoes</th>
                        </tr>
                    </thead>
                    <tbody id="proximas-consultas">
                        <tr>
                            <td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                                <div class="loading"><div class="spinner"></div></div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    `;

    if (window.lucide) {
        lucide.createIcons();
    }

    loadDashboardData();
}

async function loadDashboardData() {
    try {
        appStore.set({ loading: true });

        const consultas = await endpoints.getConsultasByRole();
        appStore.set({ consultas });

        const pacientes = await endpoints.getPacientesByRole();
        appStore.set({ pacientes, loading: false });

        updateStats(consultas, pacientes);
        renderProximasConsultas(consultas);
    } catch (error) {
        appStore.set({ loading: false });
        console.error('Erro ao carregar dados do dashboard:', error);

        const demoConsultas = generateDemoConsultas();
        const demoPacientes = generateDemoPacientes();
        appStore.set({ consultas: demoConsultas, pacientes: demoPacientes });

        updateStats(demoConsultas, demoPacientes);
        renderProximasConsultas(demoConsultas);
    }
}

async function loadSecretariaDashboardData() {
    try {
        appStore.set({ loading: true });

        const [consultas, pacientes, clientes, servicos, especialidades] = await Promise.all([
            endpoints.getConsultasSecretaria(),
            endpoints.getPacientesSecretaria(),
            endpoints.getClientesSecretaria(),
            endpoints.getServicosSecretaria(),
            endpoints.getEspecialidadesSecretaria()
        ]);

        const listaConsultas = Array.isArray(consultas) ? consultas : [];
        const listaPacientes = Array.isArray(pacientes) ? pacientes : [];
        const listaClientes = Array.isArray(clientes) ? clientes : [];
        const listaServicos = Array.isArray(servicos) ? servicos : [];
        const listaEspecialidades = Array.isArray(especialidades) ? especialidades : [];

        appStore.set({
            consultas: listaConsultas,
            pacientes: listaPacientes,
            clientes: listaClientes,
            servicos: listaServicos,
            especialidades: listaEspecialidades,
            loading: false
        });

        renderSecretariaStats(listaConsultas, listaPacientes, listaClientes, listaServicos);
        renderSecretariaProximasConsultas(listaConsultas);
        renderSecretariaEspecialidades(listaEspecialidades);
    } catch (error) {
        appStore.set({ loading: false });
        console.error('Erro ao carregar dashboard da secretaria:', error);
        toast.error(error?.message || 'Erro ao carregar dashboard da secretaria');
    }
}

function renderSecretariaStats(consultas, pacientes, clientes, servicos) {
    const hoje = new Date().toDateString();
    const consultasHoje = consultas.filter(c => {
        const data = getConsultaDate(c);
        return data && data.toDateString() === hoje;
    }).length;

    const servicosAtivos = servicos.filter(s => s.servicoEstado === true).length;

    document.getElementById('sec-stat-consultas-hoje').textContent = String(consultasHoje);
    document.getElementById('sec-stat-pacientes').textContent = String(pacientes.length);
    document.getElementById('sec-stat-clientes').textContent = String(clientes.length);
    document.getElementById('sec-stat-servicos').textContent = String(servicosAtivos);
}

function renderSecretariaProximasConsultas(consultas) {
    const tbody = document.getElementById('sec-proximas-consultas');
    if (!tbody) return;

    const agora = new Date();
    const proximas = consultas
        .map(c => ({ ...c, _data: getConsultaDate(c) }))
        .filter(c => c._data && c._data >= agora)
        .sort((a, b) => a._data - b._data)
        .slice(0, 8);

    if (proximas.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="5" style="text-align: center; padding: 40px; color: var(--gray-500);">
                    Nenhuma consulta futura encontrada
                </td>
            </tr>
        `;
        return;
    }

    tbody.innerHTML = proximas.map(c => `
        <tr>
            <td>${formatDate(c._data)}</td>
            <td>${c.idPaciente || c.paciente || '-'}</td>
            <td>${c.cliente || '-'}</td>
            <td>${c.servicos || c.servico || '-'}</td>
            <td><span class="badge badge-info">${c.idEstado || c.estado || '-'}</span></td>
        </tr>
    `).join('');
}

function renderSecretariaEspecialidades(especialidades) {
    const list = document.getElementById('sec-especialidades-list');
    if (!list) return;

    if (especialidades.length === 0) {
        list.innerHTML = `
            <div style="text-align: center; color: var(--gray-500); padding: 24px;">
                Nenhuma especialidade encontrada
            </div>
        `;
        return;
    }

    list.innerHTML = especialidades.slice(0, 8).map(e => `
        <div style="display:flex; justify-content:space-between; gap:12px; padding:12px 0; border-bottom:1px solid var(--gray-200);">
            <div>
                <div style="font-weight:600;">${e.especialidadeNome || '-'}</div>
                <div style="font-size:12px; color:var(--gray-500);">
                    ${e.especialidadeDescricao || 'Sem descricao'}
                </div>
            </div>
            <div style="display:flex; align-items:center;">
                <span class="badge badge-${e.especialidadeEstado ? 'success' : 'danger'}">
                    ${e.especialidadeEstado ? 'Ativa' : 'Inativa'}
                </span>
            </div>
        </div>
    `).join('');
}

function getConsultaDate(consulta) {
    const valor = consulta?.dataConsulta || consulta?.data_consulta || consulta?.data;
    if (!valor) return null;
    const data = new Date(valor);
    return Number.isNaN(data.getTime()) ? null : data;
}

function updateStats(consultas, pacientes) {
    const hoje = new Date().toDateString();
    const consultasHoje = consultas.filter(c => new Date(c.data).toDateString() === hoje).length;
    const totalFaturado = consultas.reduce((sum, c) => sum + (c.valor || 0), 0);

    document.getElementById('stat-consultas').textContent = consultasHoje;
    document.getElementById('stat-pacientes').textContent = pacientes.length;
    document.getElementById('stat-faturado').textContent = formatCurrency(totalFaturado);
    document.getElementById('stat-taxa').textContent = Math.min(95, Math.floor(Math.random() * 30 + 65)) + '%';
}

function renderProximasConsultas(consultas) {
    const tbody = document.getElementById('proximas-consultas');
    if (!tbody) return;

    const proximas = consultas
        .filter(c => new Date(c.data) >= new Date())
        .sort((a, b) => new Date(a.data) - new Date(b.data))
        .slice(0, 5);

    if (proximas.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                    Nenhuma consulta agendada
                </td>
            </tr>
        `;
        return;
    }

    tbody.innerHTML = proximas.map(c => `
        <tr>
            <td><div style="font-weight: 500;">${c.paciente?.nome || 'Paciente'}</div></td>
            <td>${c.especialidade || 'Geral'}</td>
            <td>${c.medico || 'Dr. Silva'}</td>
            <td>${formatDate(c.data)}</td>
            <td><span class="badge badge-success">Agendada</span></td>
            <td>
                <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="alert('Funcionalidade em desenvolvimento')">
                    <i data-lucide="eye"></i>
                </button>
            </td>
        </tr>
    `).join('');

    if (window.lucide) {
        lucide.createIcons({ node: tbody });
    }
}

function formatCurrency(value) {
    return new Intl.NumberFormat('pt-AO', { style: 'currency', currency: 'AOA' }).format(value || 0);
}

function formatDate(date) {
    return new Date(date).toLocaleDateString('pt-PT', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });
}

function generateDemoConsultas() {
    const especialidades = ['Cardiologia', 'Dermatologia', 'Pediatria', 'Ortopedia', 'Neurologia'];
    const medicos = ['Dr. Silva', 'Dra. Santos', 'Dr. Pereira', 'Dra. Costa', 'Dr. Ferreira'];
    const pacientes = ['Joao Manuel', 'Maria Jose', 'Antonio Silva', 'Ana Paula', 'Pedro Costa'];

    const consultas = [];
    const hoje = new Date();

    for (let i = 0; i < 20; i++) {
        const data = new Date(hoje);
        data.setDate(data.getDate() + Math.floor(Math.random() * 30) - 5);

        consultas.push({
            id: i + 1,
            data: data.toISOString(),
            especialidade: especialidades[Math.floor(Math.random() * especialidades.length)],
            medico: medicos[Math.floor(Math.random() * medicos.length)],
            paciente: { nome: pacientes[Math.floor(Math.random() * pacientes.length)] },
            valor: Math.floor(Math.random() * 5000) + 1000,
            estado: 'Agendada'
        });
    }

    return consultas;
}

function generateDemoPacientes() {
    return [
        { id: 1, nome: 'Joao Manuel', nif: '123456789' },
        { id: 2, nome: 'Maria Jose', nif: '234567890' },
        { id: 3, nome: 'Antonio Silva', nif: '345678901' },
        { id: 4, nome: 'Ana Paula', nif: '456789012' },
        { id: 5, nome: 'Pedro Costa', nif: '567890123' }
    ];
}

window.renderDashboard = renderDashboard;
