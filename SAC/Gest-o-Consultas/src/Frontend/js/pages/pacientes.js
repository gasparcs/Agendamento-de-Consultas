/**
 * Kigramed Frontend - Pacientes Page
 */

function renderPacientes() {
    const app = document.getElementById('app');
    
    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Pacientes</h1>
                    <p style="color: var(--gray-500);">Gestão de pacientes</p>
                </div>
                <button class="btn btn-primary" onclick="openModal('paciente')">
                    <i data-lucide="plus"></i>
                    Novo Paciente
                </button>
            </div>
            
            <div class="card">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>Data Nasc.</th>
                            <th>Género</th>
                            <th>Cliente</th>
                            <th>Nº Consultas</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody id="pacientes-table">
                        <tr><td colspan="6" style="text-align: center; padding: 40px;">
                            <div class="loading"><div class="spinner"></div></div>
                        </td></tr>
                    </tbody>
                </table>
            </div>
        </div>
        
        <div class="modal-overlay" id="modal-paciente">
            <div class="modal">
                <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
                    <h2 style="font-size: 20px; font-weight: 600;" id="modal-paciente-title">Novo Paciente</h2>
                    <button class="btn btn-secondary" style="padding: 8px;" onclick="closeModal('paciente')">
                        <i data-lucide="x"></i>
                    </button>
                </div>
                <form id="form-paciente">
                    <input type="hidden" id="paciente-id">
                    <div class="form-group">
                        <label class="form-label">Nome *</label>
                        <input type="text" id="paciente-nome" class="form-input" required>
                    </div>
                    <div class="grid grid-cols-2">
                        <div class="form-group">
                            <label class="form-label">Data Nasc. *</label>
                            <input type="date" id="paciente-datanasc" class="form-input" required>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Género</label>
                            <select id="paciente-genero" class="form-input">
                                <option value="Masculino">Masculino</option>
                                <option value="Feminino">Feminino</option>
                            </select>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="form-label">NIF do Cliente *</label>
                        <input type="text" id="paciente-cliente" class="form-input" placeholder="NIF do cliente responsável" required>
                    </div>
                    <div style="display: flex; gap: 12px; justify-content: flex-end; margin-top: 24px;">
                        <button type="button" class="btn btn-secondary" onclick="closeModal('paciente')">Cancelar</button>
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
    loadPacientes();
    document.getElementById('form-paciente').addEventListener('submit', handleSavePaciente);
}

async function loadPacientes() {
    try {
        const pacientes = await endpoints.getPacientes();
        appStore.set({ pacientes });
        renderPacientesTable(pacientes);
    } catch (error) {
        console.error('Erro ao carregar pacientes:', error);
        toast.error(error?.message || 'Erro ao carregar pacientes');
        document.getElementById('pacientes-table').innerHTML = `
            <tr><td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                Erro ao carregar pacientes
            </td></tr>`;
    }
}

function renderPacientesTable(pacientes) {
    const tbody = document.getElementById('pacientes-table');
    if (!tbody) return;
    
    if (pacientes.length === 0) {
        tbody.innerHTML = `
            <tr><td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                Nenhum paciente encontrado
            </td></tr>`;
        return;
    }
    
    tbody.innerHTML = pacientes.map(p => `
        <tr>
            <td><strong>${p.pacienteNome || '-'}</strong></td>
            <td>${p.pacienteData_nascimento ? new Date(p.pacienteData_nascimento).toLocaleDateString('pt-PT') : '-'}</td>
            <td>${p.genero || '-'}</td>
            <td>${p.cliente || '-'}</td>
            <td>${p.consultas?.length || 0}</td>
            <td>
                <div style="display: flex; gap: 8px;">
                    <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="editPaciente(${p.pacienteId})">
                        <i data-lucide="edit-2"></i>
                    </button>
                    <button class="btn btn-danger" style="padding: 6px 12px;" onclick="deletePaciente(${p.pacienteId})">
                        <i data-lucide="trash-2"></i>
                    </button>
                </div>
            </td>
        </tr>
    `).join('');
    
    if (window.lucide) lucide.createIcons({ node: tbody });
}

async function handleSavePaciente(e) {
    e.preventDefault();
    
    const id = document.getElementById('paciente-id').value;
    const data = {
        pacienteNome: document.getElementById('paciente-nome').value,
        pacienteData_nascimento: document.getElementById('paciente-datanasc').value,
        genero: document.getElementById('paciente-genero').value,
        clienteNif: document.getElementById('paciente-cliente').value
    };
    
    try {
        if (id) {
            await endpoints.updatePaciente(id, data);
            toast.success('Paciente atualizado com sucesso!');
        } else {
            await endpoints.createPaciente(data);
            toast.success('Paciente criado com sucesso!');
        }
        closeModal('paciente');
        loadPacientes();
    } catch (error) {
        toast.error(error?.message || 'Erro ao salvar paciente');
    }
}

function editPaciente(id) {
    const pacientes = appStore.get('pacientes') || [];
    const paciente = pacientes.find(p => p.pacienteId === id);
    if (!paciente) return;

    document.getElementById('paciente-id').value = paciente.pacienteId;
    document.getElementById('paciente-nome').value = paciente.pacienteNome;
    document.getElementById('paciente-datanasc').value = 
        paciente.pacienteData_nascimento ? paciente.pacienteData_nascimento.split('T')[0] : '';
    document.getElementById('paciente-genero').value = paciente.genero || 'Masculino';
    document.getElementById('paciente-cliente').value = paciente.cliente || '';

    document.getElementById('modal-paciente-title').textContent = 'Editar Paciente';
    openModal('paciente');
}

async function deletePaciente(id) {
    if (!confirm('Tem certeza que deseja excluir este paciente?')) return;
    try {
        await endpoints.deletePaciente(id);
        toast.success('Paciente excluído com sucesso!');
        loadPacientes();
    } catch (error) {
        toast.error(error?.message || 'Erro ao excluir paciente');
    }
}

window.renderPacientes = renderPacientes;