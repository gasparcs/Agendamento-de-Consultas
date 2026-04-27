/**
 * Kigramed Frontend - Consultas Page
 */

function renderConsultas() {
    const app = document.getElementById('app');
    
    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Consultas</h1>
                    <p style="color: var(--gray-500);">Gestão de consultas</p>
                </div>
                <button class="btn btn-primary" onclick="openModal('consulta')">
                    <i data-lucide="plus"></i>
                    Nova Consulta
                </button>
            </div>
            
            <div class="card">
                <div style="display: flex; gap: 16px; flex-wrap: wrap;">
                    <div class="search-box" style="flex: 1; min-width: 200px;">
                        <i data-lucide="search"></i>
                        <input type="text" id="search-consulta" class="form-input" placeholder="Pesquisar..." oninput="filterConsultas(this.value)">
                    </div>
                    <select class="form-input" style="width: auto;" onchange="filterByStatus(this.value)">
                        <option value="">Todos os estados</option>
                        <option value="Agendada">Agendada</option>
                        <option value="Concluída">Concluída</option>
                        <option value="Cancelada">Cancelada</option>
                    </select>
                </div>
            </div>
            
            <div class="card">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Data/Hora</th>
                            <th>Paciente</th>
                            <th>Especialidade</th>
                            <th>Serviço</th>
                            <th>Cliente</th>
                            <th>Estado</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody id="consultas-table">
                        <tr>
                            <td colspan="7" style="text-align: center; padding: 40px;">
                                <div class="loading"><div class="spinner"></div></div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        
        <!-- Modal Consulta -->
        <div class="modal-overlay" id="modal-consulta">
            <div class="modal">
                <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
                    <h2 style="font-size: 20px; font-weight: 600;">Nova Consulta</h2>
                    <button class="btn btn-secondary" style="padding: 8px;" onclick="closeModal('consulta')">
                        <i data-lucide="x"></i>
                    </button>
                </div>
                
                <form id="form-consulta">
                    <div class="grid grid-cols-2">
                        <div class="form-group">
                            <label class="form-label">Data *</label>
                            <input type="date" id="consulta-data" class="form-input" required>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Hora *</label>
                            <input type="time" id="consulta-hora" class="form-input" required>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <label class="form-label">ID Médico Especialidade *</label>
                        <input type="number" id="consulta-medico-especialidade" class="form-input" placeholder="ID da especialidade do médico" required>
                    </div>

                    <div class="form-group">
                        <label class="form-label">ID Serviço *</label>
                        <input type="number" id="consulta-servico" class="form-input" placeholder="ID do serviço" required>
                    </div>

                    <div class="form-group">
                        <label class="form-label">ID Paciente *</label>
                        <input type="number" id="consulta-paciente" class="form-input" placeholder="ID do paciente" required>
                    </div>

                    <div class="form-group">
                        <label class="form-label">Estado *</label>
                        <select id="consulta-estado" class="form-input" required>
                            <option value="1">Agendada</option>
                            <option value="2">Concluída</option>
                            <option value="3">Cancelada</option>
                        </select>
                    </div>
                    
                    <div style="display: flex; gap: 12px; justify-content: flex-end; margin-top: 24px;">
                        <button type="button" class="btn btn-secondary" onclick="closeModal('consulta')">Cancelar</button>
                        <button type="submit" class="btn btn-primary">
                            <i data-lucide="save"></i>
                            Guardar
                        </button>
                    </div>
                </form>
            </div>
        </div>
    `;
    
    if (window.lucide) lucide.createIcons();
    loadConsultas();
    document.getElementById('form-consulta').addEventListener('submit', handleSaveConsulta);
    aplicarOrigemConsulta();
}

async function loadConsultas() {
    try {
        const consultas = await endpoints.getConsultasByRole();
        appStore.set({ consultas });
        renderConsultasTable(consultas);
    } catch (error) {
        console.error('Erro ao carregar consultas:', error);
        toast.error(error?.message || 'Erro ao carregar consultas');
        document.getElementById('consultas-table').innerHTML = `
            <tr><td colspan="7" style="text-align: center; padding: 40px; color: var(--gray-500);">
                Erro ao carregar consultas
            </td></tr>`;
    }
}

function renderConsultasTable(consultas) {
    const tbody = document.getElementById('consultas-table');
    if (!tbody) return;
    
    if (consultas.length === 0) {
        tbody.innerHTML = `
            <tr><td colspan="7" style="text-align: center; padding: 40px; color: var(--gray-500);">
                Nenhuma consulta encontrada
            </td></tr>`;
        return;
    }
    
    tbody.innerHTML = consultas.map(c => `
        <tr>
            <td>${c.dataConsulta ? new Date(c.dataConsulta).toLocaleString('pt-PT') : '-'}</td>
            <td>${c.idPaciente || '-'}</td>
            <td>${c.idMedicoEspecialidade || '-'}</td>
            <td>${c.servicos || '-'}</td>
            <td>${c.cliente || '-'}</td>
            <td>
                <span class="badge badge-${
                    c.idEstado === 'Agendada' ? 'success' : 
                    c.idEstado === 'Cancelada' ? 'danger' : 'info'
                }">${c.idEstado || 'Agendada'}</span>
            </td>
            <td>
                <div style="display: flex; gap: 8px;">
                    <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="editConsulta(${c.idConsulta})">
                        <i data-lucide="edit-2"></i>
                    </button>
                    <button class="btn btn-danger" style="padding: 6px 12px;" onclick="deleteConsulta(${c.idConsulta})">
                        <i data-lucide="trash-2"></i>
                    </button>
                </div>
            </td>
        </tr>
    `).join('');
    
    if (window.lucide) lucide.createIcons({ node: tbody });
}

function filterConsultas(query) {
    const consultas = appStore.get('consultas') || [];
    const filtered = consultas.filter(c => 
        c.idPaciente?.toLowerCase().includes(query.toLowerCase()) ||
        c.idMedicoEspecialidade?.toLowerCase().includes(query.toLowerCase()) ||
        c.cliente?.toLowerCase().includes(query.toLowerCase())
    );
    renderConsultasTable(filtered);
}

function filterByStatus(status) {
    const consultas = appStore.get('consultas') || [];
    const filtered = status ? consultas.filter(c => c.idEstado === status) : consultas;
    renderConsultasTable(filtered);
}

async function handleSaveConsulta(e) {
    e.preventDefault();
    
    const data = {
        idMedicoEspecialidade: parseInt(document.getElementById('consulta-medico-especialidade').value),
        idServico: parseInt(document.getElementById('consulta-servico').value),
        idPaciente: parseInt(document.getElementById('consulta-paciente').value),
        idEstado: parseInt(document.getElementById('consulta-estado').value),
        dataConsulta: document.getElementById('consulta-data').value + 'T' + document.getElementById('consulta-hora').value
    };
    
    try {
        await endpoints.createConsultaByRole(data);
        toast.success('Consulta agendada com sucesso!');
        closeModal('consulta');
        loadConsultas();
    } catch (error) {
        toast.error(error?.message || 'Erro ao agendar consulta');
    }
}

function editConsulta(id) {
    toast.info('Funcionalidade em desenvolvimento');
}

async function deleteConsulta(id) {
    if (!confirm('Cancelar esta consulta?')) return;
    try {
        await endpoints.deleteConsulta(id);
        toast.success('Consulta cancelada com sucesso!');
        loadConsultas();
    } catch (error) {
        toast.error(error?.message || 'Erro ao cancelar consulta');
    }
}

function aplicarOrigemConsulta() {
    const origem = appStore.get('consultaDraftSource');
    if (!origem) return;

    appStore.set({ consultaDraftSource: null });
    openModal('consulta');

    const nome = origem.nome ? `: ${origem.nome}` : '';
    toast.info(`Marcar consulta a partir de ${origem.tipo}${nome}`);
}

window.renderConsultas = renderConsultas;
