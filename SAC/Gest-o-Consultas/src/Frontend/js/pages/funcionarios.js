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
                            <th>Telefone</th>
                            <th>Estado</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody id="funcionarios-table">
                        <tr><td colspan="6" style="text-align: center; padding: 40px;">
                            <div class="loading"><div class="spinner"></div></div>
                        </td></tr>
                    </tbody>
                </table>
            </div>
        </div>
        
        <div class="modal-overlay" id="modal-funcionario">
            <div class="modal">
                <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
                    <h2 style="font-size: 20px; font-weight: 600;" id="modal-funcionario-title">Novo Funcionário</h2>
                    <button class="btn btn-secondary" style="padding: 8px;" onclick="closeModal('funcionario')">
                        <i data-lucide="x"></i>
                    </button>
                </div>
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
                            <option value="1">Administrador</option>
                            <option value="2">Secretária</option>
                            <option value="3">Médico</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Telefone *</label>
                        <input type="text" id="func-telefone" class="form-input" placeholder="9xxxxxxxx" required>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Estado</label>
                        <select id="func-estado" class="form-input">
                            <option value="true">Ativo</option>
                            <option value="false">Inativo</option>
                        </select>
                    </div>
                    <div style="display: flex; gap: 12px; justify-content: flex-end; margin-top: 24px;">
                        <button type="button" class="btn btn-secondary" onclick="closeModal('funcionario')">Cancelar</button>
                        <button type="submit" class="btn btn-primary">
                            <i data-lucide="save"></i>Guardar
                        </button>
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
        console.error('Erro ao carregar funcionários:', error);
        toast.error(error?.message || 'Erro ao carregar funcionários');
        document.getElementById('funcionarios-table').innerHTML = `
            <tr><td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                Erro ao carregar funcionários
            </td></tr>`;
    }
}

function renderFuncionariosTable(funcionarios) {
    const tbody = document.getElementById('funcionarios-table');
    if (!tbody) return;
    
    if (funcionarios.length === 0) {
        tbody.innerHTML = `
            <tr><td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                Nenhum funcionário encontrado
            </td></tr>`;
        return;
    }
    
    const perfilColors = { Admin: 'danger', Secretaria: 'info', Medico: 'success' };
    
    tbody.innerHTML = funcionarios.map(f => `
        <tr>
            <td><code style="background: var(--gray-100); padding: 4px 8px; border-radius: 4px;">${f.funcionarioNif || '-'}</code></td>
            <td><strong>${f.funcionarioNome || '-'}</strong></td>
            <td><span class="badge badge-${perfilColors[f.fUncionarioPerfil] || 'info'}">${f.fUncionarioPerfil || '-'}</span></td>
            <td>${f.contactos?.find(c => c.tipoContacto?.descricao?.toLowerCase() === 'telefone')?.contacto || '-'}</td>
            <td><span class="badge badge-${f.funcionaroEstado ? 'success' : 'danger'}">${f.funcionaroEstado ? 'Ativo' : 'Inativo'}</span></td>
            <td>
                <div style="display: flex; gap: 8px;">
                    <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="editFuncionario('${f.funcionarioNif}')">
                        <i data-lucide="edit-2"></i>
                    </button>
                    <button class="btn btn-danger" style="padding: 6px 12px;" onclick="deleteFuncionario('${f.funcionarioNif}')">
                        <i data-lucide="trash-2"></i>
                    </button>
                </div>
            </td>
        </tr>
    `).join('');
    
    if (window.lucide) lucide.createIcons({ node: tbody });
}

async function handleSaveFuncionario(e) {
    e.preventDefault();
    
    const data = {
        funcionaioNif: document.getElementById('func-nif').value,
        funcionarioNome: document.getElementById('func-nome').value,
        funcionarioPerfil: parseInt(document.getElementById('func-perfil').value),
        funcionarioEstado: document.getElementById('func-estado').value === 'true',
        contactos: [
            {
                tipoContacto: 1,
                contacto: document.getElementById('func-telefone').value
            }
        ],
        especialidades: []
    };
    
    try {
        await endpoints.createFuncionario(data);
        toast.success('Funcionário criado com sucesso!');
        closeModal('funcionario');
        loadFuncionarios();
    } catch (error) {
        toast.error(error?.message || 'Erro ao criar funcionário');
    }
}

function editFuncionario(nif) {
    const funcionarios = appStore.get('funcionarios') || [];
    const func = funcionarios.find(f => f.funcionarioNif === nif);
    if (!func) return;

    document.getElementById('func-nif').value = func.funcionarioNif;
    document.getElementById('func-nome').value = func.funcionarioNome;
    document.getElementById('func-estado').value = func.funcionaroEstado ? 'true' : 'false';
    document.getElementById('func-telefone').value =
        func.contactos?.find(c => c.tipoContacto?.descricao?.toLowerCase() === 'telefone')?.contacto || '';

    document.getElementById('modal-funcionario-title').textContent = 'Editar Funcionário';
    openModal('funcionario');
}

async function deleteFuncionario(nif) {
    if (!confirm('Tem certeza que deseja excluir este funcionário?')) return;
    try {
        await endpoints.deleteFuncionario(nif);
        toast.success('Funcionário excluído com sucesso!');
        loadFuncionarios();
    } catch (error) {
        toast.error(error?.message || 'Erro ao excluir funcionário');
    }
}

window.renderFuncionarios = renderFuncionarios;