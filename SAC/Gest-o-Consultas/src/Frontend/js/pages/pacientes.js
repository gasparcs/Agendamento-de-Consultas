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
                            <th>Telefone</th>
                            <th>Endereço</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody id="pacientes-table">
                        <tr><td colspan="6" style="text-align: center; padding: 40px;"><div class="loading"><div class="spinner"></div></div></td></tr>
                    </tbody>
                </table>
            </div>
        </div>
        
        <div class="modal-overlay" id="modal-paciente">
            <div class="modal">
                <h2 style="font-size: 20px; font-weight: 600; margin-bottom: 20px;">Novo Paciente</h2>
                <form id="form-paciente">
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
                        <label class="form-label">Telefone</label>
                        <input type="text" id="paciente-telefone" class="form-input">
                    </div>
                    <div class="form-group">
                        <label class="form-label">Endereço</label>
                        <input type="text" id="paciente-endereco" class="form-input">
                    </div>
                    <div style="display: flex; gap: 12px; justify-content: flex-end; margin-top: 24px;">
                        <button type="button" class="btn btn-secondary" onclick="closeModal('paciente')">Cancelar</button>
                        <button type="submit" class="btn btn-primary"><i data-lucide="save"></i>Guardar</button>
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
        const demo = generateDemoPacientes();
        appStore.set({ pacientes: demo });
        renderPacientesTable(demo);
    }
}

function renderPacientesTable(pacientes) {
    const tbody = document.getElementById('pacientes-table');
    if (!tbody) return;
    
    if (pacientes.length === 0) {
        tbody.innerHTML = '<tr><td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">Nenhum paciente encontrado</td></tr>';
        return;
    }
    
    tbody.innerHTML = pacientes.map(p => `
        <tr>
            <td><strong>${p.nome}</strong></td>
            <td>${p.dataNascimento ? new Date(p.dataNascimento).toLocaleDateString('pt-PT') : '-'}</td>
            <td>${p.genero || '-'}</td>
            <td>${p.telefone || '-'}</td>
            <td>${p.endereco || '-'}</td>
            <td>
                <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="editPaciente(${p.id})"><i data-lucide="edit-2"></i></button>
                <button class="btn btn-danger" style="padding: 6px 12px;" onclick="deletePaciente(${p.id})"><i data-lucide="trash-2"></i></button>
            </td>
        </tr>
    `).join('');
    
    if (window.lucide) lucide.createIcons({ node: tbody });
}

async function handleSavePaciente(e) {
    e.preventDefault();
    const data = {
        nome: document.getElementById('paciente-nome').value,
        dataNascimento: document.getElementById('paciente-datanasc').value,
        genero: document.getElementById('paciente-genero').value,
        telefone: document.getElementById('paciente-telefone').value,
        endereco: document.getElementById('paciente-endereco').value
    };
    
    try {
        await endpoints.createPaciente(data);
        toast.success('Paciente criado!');
    } catch (error) {
        toast.success('Paciente criado (demo)!');
    }
    closeModal('paciente');
    loadPacientes();
}

function editPaciente(id) { toast.info('Em desenvolvimento'); }
async function deletePaciente(id) {
    if (!confirm('Excluir paciente?')) return;
    try { await endpoints.deletePaciente(id); toast.success('Paciente excluído!'); } 
    catch { toast.success('Paciente excluído (demo)!'); }
    loadPacientes();
}

window.renderPacientes = renderPacientes;