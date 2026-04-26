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
            
            <!-- Filtros -->
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
            
            <!-- Consultas Table -->
            <div class="card">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Data/Hora</th>
                            <th>Paciente</th>
                            <th>Especialidade</th>
                            <th>Médico</th>
                            <th>Valor</th>
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
                        <label class="form-label">Paciente *</label>
                        <select id="consulta-paciente" class="form-input" required>
                            <option value="">Selecione...</option>
                        </select>
                    </div>
                    
                    <div class="form-group">
                        <label class="form-label">Especialidade *</label>
                        <select id="consulta-especialidade" class="form-input" required>
                            <option value="">Selecione...</option>
                            <option value="Cardiologia">Cardiologia</option>
                            <option value="Dermatologia">Dermatologia</option>
                            <option value="Pediatria">Pediatria</option>
                            <option value="Ortopedia">Ortopedia</option>
                            <option value="Neurologia">Neurologia</option>
                        </select>
                    </div>
                    
                    <div class="form-group">
                        <label class="form-label">Médico</label>
                        <select id="consulta-medico" class="form-input">
                            <option value="">Selecione...</option>
                            <option value="Dr. Silva">Dr. Silva</option>
                            <option value="Dra. Santos">Dra. Santos</option>
                            <option value="Dr. Pereira">Dr. Pereira</option>
                        </select>
                    </div>
                    
                    <div class="form-group">
                        <label class="form-label">Valor (AOA)</label>
                        <input type="number" id="consulta-valor" class="form-input" placeholder="0" min="0">
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
}

async function loadConsultas() {
    try {
        const consultas = await endpoints.getConsultas();
        appStore.set({ consultas });
        renderConsultasTable(consultas);
    } catch (error) {
        const demo = generateDemoConsultas();
        appStore.set({ consultas: demo });
        renderConsultasTable(demo);
    }
}

function renderConsultasTable(consultas) {
    const tbody = document.getElementById('consultas-table');
    if (!tbody) return;
    
    if (consultas.length === 0) {
        tbody.innerHTML = '<tr><td colspan="7" style="text-align: center; padding: 40px; color: var(--gray-500);">Nenhuma consulta encontrada</td></tr>';
        return;
    }
    
    tbody.innerHTML = consultas.map(c => `
        <tr>
            <td>${formatDate(c.data)}</td>
            <td>${c.paciente?.nome || '-'}</td>
            <td>${c.especialidade || '-'}</td>
            <td>${c.medico || '-'}</td>
            <td>${formatCurrency(c.valor)}</td>
            <td><span class="badge badge-${c.estado === 'Agendada' ? 'success' : c.estado === 'Cancelada' ? 'danger' : 'info'}">${c.estado || 'Agendada'}</span></td>
            <td>
                <div style="display: flex; gap: 8px;">
                    <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="editConsulta(${c.id})">
                        <i data-lucide="edit-2"></i>
                    </button>
                    <button class="btn btn-danger" style="padding: 6px 12px;" onclick="deleteConsulta(${c.id})">
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
        c.paciente?.nome?.toLowerCase().includes(query.toLowerCase()) ||
        c.especialidade?.toLowerCase().includes(query.toLowerCase())
    );
    renderConsultasTable(filtered);
}

function filterByStatus(status) {
    const consultas = appStore.get('consultas') || [];
    const filtered = status ? consultas.filter(c => c.estado === status) : consultas;
    renderConsultasTable(filtered);
}

async function handleSaveConsulta(e) {
    e.preventDefault();
    const data = {
        data: document.getElementById('consulta-data').value + 'T' + document.getElementById('consulta-hora').value,
        especialidade: document.getElementById('consulta-especialidade').value,
        medico: document.getElementById('consulta-medico').value,
        valor: parseFloat(document.getElementById('consulta-valor').value) || 0
    };
    
    try {
        await endpoints.createConsulta(data);
        toast.success('Consulta agendada!');
    } catch (error) {
        toast.success('Consulta agendada (demo)!');
    }
    closeModal('consulta');
    loadConsultas();
}

function editConsulta(id) {
    toast.info('Funcionalidade em desenvolvimento');
}

async function deleteConsulta(id) {
    if (!confirm('Cancelar esta consulta?')) return;
    try {
        await endpoints.deleteConsulta(id);
        toast.success('Consulta cancelada!');
    } catch (error) {
        toast.success('Consulta cancelada (demo)!');
    }
    loadConsultas();
}

window.renderConsultas = renderConsultas;