/**
 * Kigramed Frontend - Funcionários Page
 */

function renderFuncionarios() {
    const app = document.getElementById('app');
    
    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Funcionários</h1>
                    <p style="color: var(--gray-500);">Gestão de funcionários</p>
                </div>
                <button class="btn btn-primary" onclick="openModal('funcionario')">
                    <i data-lucide="plus"></i>
                    Novo Funcionário
                </button>
            </div>
            
            <div class="card">
                <table class="table">
                    <thead>
                        <tr>
                            <th>NIF</th>
                            <th>Nome</th>
                            <th>Perfil</th>
                            <th>Email</th>
                            <th>Telefone</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody id="funcionarios-table">
                        <tr><td colspan="6" style="text-align: center; padding: 40px;"><div class="loading"><div class="spinner"></div></div></td></tr>
                    </tbody>
                </table>
            </div>
        </div>
        
        <div class="modal-overlay" id="modal-funcionario">
            <div class="modal">
                <h2 style="font-size: 20px; font-weight: 600; margin-bottom: 20px;">Novo Funcionário</h2>
                <form id="form-funcionario">
                    <div class="form-group">
                        <label class="form-label">NIF *</label>
                        <input type="text" id="func-nif" class="form-input" maxlength="9" required>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Nome *</label>
                        <input type="text" id="func-nome" class="form-input" required>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Perfil *</label>
                        <select id="func-perfil" class="form-input" required>
                            <option value="">Selecione...</option>
                            <option value="Admin">Administrador</option>
                            <option value="Secretaria">Secretária</option>
                            <option value="Medico">Médico</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Email</label>
                        <input type="email" id="func-email" class="form-input">
                    </div>
                    <div class="form-group">
                        <label class="form-label">Telefone</label>
                        <input type="text" id="func-telefone" class="form-input">
                    </div>
                    <div style="display: flex; gap: 12px; justify-content: flex-end; margin-top: 24px;">
                        <button type="button" class="btn btn-secondary" onclick="closeModal('funcionario')">Cancelar</button>
                        <button type="submit" class="btn btn-primary"><i data-lucide="save"></i>Guardar</button>
                    </div>
                </form>
            </div>
        </div>
    `;
    
    if (window.lucide) lucide.createIcons();
    loadFuncionarios();
    document.getElementById('form-funcionario').addEventListener('submit', handleSaveFuncionario);
}

async function loadFuncionarios() {
    try {
        const funcionarios = await endpoints.getFuncionarios();
        appStore.set({ funcionarios });
        renderFuncionariosTable(funcionarios);
    } catch (error) {
        const demo = [
            { id: 1, nif: '123456789', nome: 'Administrador Principal', perfil: 'Admin', email: 'admin@kigramed.com', telefone: '921111111' },
            { id: 2, nif: '234567890', nome: 'Maria da Silva', perfil: 'Secretaria', email: 'maria@kigramed.com', telefone: '922222222' },
            { id: 3, nif: '345678901', nome: 'Dr. João Pereira', perfil: 'Medico', email: 'joao@kigramed.com', telefone: '923333333' }
        ];
        appStore.set({ funcionarios: demo });
        renderFuncionariosTable(demo);
    }
}

function renderFuncionariosTable(funcionarios) {
    const tbody = document.getElementById('funcionarios-table');
    if (!tbody) return;
    
    if (funcionarios.length === 0) {
        tbody.innerHTML = '<tr><td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">Nenhum funcionário encontrado</td></tr>';
        return;
    }
    
    const perfilColors = { Admin: 'danger', Secretaria: 'info', Medico: 'success' };
    
    tbody.innerHTML = funcionarios.map(f => `
        <tr>
            <td><code style="background: var(--gray-100); padding: 4px 8px; border-radius: 4px;">${f.nif}</code></td>
            <td><strong>${f.nome}</strong></td>
            <td><span class="badge badge-${perfilColors[f.perfil] || 'info'}">${f.perfil}</span></td>
            <td>${f.email || '-'}</td>
            <td>${f.telefone || '-'}</td>
            <td>
                <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="editFuncionario(${f.id})"><i data-lucide="edit-2"></i></button>
                <button class="btn btn-danger" style="padding: 6px 12px;" onclick="deleteFuncionario(${f.id})"><i data-lucide="trash-2"></i></button>
            </td>
        </tr>
    `).join('');
    
    if (window.lucide) lucide.createIcons({ node: tbody });
}

async function handleSaveFuncionario(e) {
    e.preventDefault();
    const data = {
        nif: document.getElementById('func-nif').value,
        nome: document.getElementById('func-nome').value,
        perfil: document.getElementById('func-perfil').value,
        email: document.getElementById('func-email').value,
        telefone: document.getElementById('func-telefone').value
    };
    
    try {
        await endpoints.createFuncionario(data);
        toast.success('Funcionário criado!');
    } catch (error) {
        toast.success('Funcionário criado (demo)!');
    }
    closeModal('funcionario');
    loadFuncionarios();
}

function editFuncionario(id) { toast.info('Em desenvolvimento'); }
async function deleteFuncionario(id) {
    if (!confirm('Excluir funcionário?')) return;
    try { await endpoints.deleteFuncionario(id); toast.success('Funcionário excluído!'); } 
    catch { toast.success('Funcionário excluído (demo)!'); }
    loadFuncionarios();
}

window.renderFuncionarios = renderFuncionarios;