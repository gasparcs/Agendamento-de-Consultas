/**
 * Kigramed Frontend - Servicos Page
 */

function renderServicos() {
    const app = document.getElementById('app');
    const isSecretaria = String(appStore.get('user')?.role || '').toLowerCase() === 'secretaria';

    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Servicos</h1>
                    <p style="color: var(--gray-500);">Gestao de servicos medicos</p>
                </div>
                ${isSecretaria ? '' : `
                <button class="btn btn-primary" onclick="openModal('servico')">
                    <i data-lucide="plus"></i>
                    Novo Servico
                </button>
                `}
            </div>

            <div class="card">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>Preco (AOA)</th>
                            <th>Duracao (min)</th>
                            <th>Estado</th>
                            <th>Especialidade</th>
                            <th>Acoes</th>
                        </tr>
                    </thead>
                    <tbody id="servicos-table">
                        <tr><td colspan="6" style="text-align: center; padding: 40px;">
                            <div class="loading"><div class="spinner"></div></div>
                        </td></tr>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="modal-overlay" id="modal-servico">
            <div class="modal">
                <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
                    <h2 style="font-size: 20px; font-weight: 600;" id="modal-servico-title">Novo Servico</h2>
                    <button class="btn btn-secondary" style="padding: 8px;" onclick="closeModal('servico')">
                        <i data-lucide="x"></i>
                    </button>
                </div>
                <form id="form-servico">
                    <input type="hidden" id="servico-id">
                    <div class="form-group">
                        <label class="form-label">Nome *</label>
                        <input type="text" id="servico-nome" class="form-input" required>
                    </div>
                    <div class="grid grid-cols-2">
                        <div class="form-group">
                            <label class="form-label">Preco (AOA)</label>
                            <input type="number" id="servico-preco" class="form-input" min="0" step="0.01">
                        </div>
                        <div class="form-group">
                            <label class="form-label">Duracao (min)</label>
                            <input type="number" id="servico-duracao" class="form-input" min="15" step="15">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="form-label">ID Especialidade *</label>
                        <input type="number" id="servico-especialidade" class="form-input" placeholder="ID da especialidade" required>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Estado</label>
                        <select id="servico-estado" class="form-input">
                            <option value="true">Ativo</option>
                            <option value="false">Inativo</option>
                        </select>
                    </div>
                    <div style="display: flex; gap: 12px; justify-content: flex-end; margin-top: 24px;">
                        <button type="button" class="btn btn-secondary" onclick="closeModal('servico')">Cancelar</button>
                        <button type="submit" class="btn btn-primary">
                            <i data-lucide="save"></i>Guardar
                        </button>
                    </div>
                </form>
            </div>
        </div>
    `;

    if (window.lucide) lucide.createIcons();
    loadServicos();
    if (!isSecretaria) {
        document.getElementById('form-servico').addEventListener('submit', handleSaveServico);
    }
}

async function loadServicos() {
    try {
        const servicos = await endpoints.getServicosByRole();
        appStore.set({ servicos });
        renderServicosTable(servicos);
    } catch (error) {
        console.error('Erro ao carregar servicos:', error);
        toast.error(error?.message || 'Erro ao carregar servicos');
        document.getElementById('servicos-table').innerHTML = `
            <tr><td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                Erro ao carregar servicos
            </td></tr>`;
    }
}

function renderServicosTable(servicos) {
    const tbody = document.getElementById('servicos-table');
    const isSecretaria = String(appStore.get('user')?.role || '').toLowerCase() === 'secretaria';
    if (!tbody) return;

    if (servicos.length === 0) {
        tbody.innerHTML = `
            <tr><td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                Nenhum servico encontrado
            </td></tr>`;
        return;
    }

    tbody.innerHTML = servicos.map(s => `
        <tr>
            <td><strong>${s.servicoNome || '-'}</strong></td>
            <td>${s.servicoPreco?.toLocaleString('pt-AO') || '0'} Kz</td>
            <td>${s.servicoDuracaoMinuto || '-'} min</td>
            <td>
                <span class="badge badge-${s.servicoEstado ? 'success' : 'danger'}">
                    ${s.servicoEstado ? 'Ativo' : 'Inativo'}
                </span>
            </td>
            <td>${s.idEspecialidade || '-'}</td>
            <td>
                <div style="display: flex; gap: 8px; flex-wrap: wrap;">
                    <button class="btn btn-primary" style="padding: 6px 12px;" onclick='marcarConsultaServico(${s.idEspecialidade}, ${JSON.stringify(s.servicoNome || '')})'>
                        <i data-lucide="calendar-plus"></i> Marcar Consulta
                    </button>
                    ${isSecretaria ? '' : `
                    <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="editServico(${s.idEspecialidade}, '${s.servicoNome}')">
                        <i data-lucide="edit-2"></i>
                    </button>
                    <button class="btn btn-danger" style="padding: 6px 12px;" onclick="deleteServico(${s.idEspecialidade})">
                        <i data-lucide="trash-2"></i>
                    </button>
                    `}
                </div>
            </td>
        </tr>
    `).join('');

    if (window.lucide) lucide.createIcons({ node: tbody });
}

async function handleSaveServico(e) {
    if (String(appStore.get('user')?.role || '').toLowerCase() === 'secretaria') {
        toast.warning('Perfil secretaria nao pode editar servicos');
        return;
    }
    e.preventDefault();

    const id = document.getElementById('servico-id').value;
    const data = {
        servicoNome: document.getElementById('servico-nome').value,
        servicoPreco: parseFloat(document.getElementById('servico-preco').value) || 0,
        servicoDuracaoMinuto: parseInt(document.getElementById('servico-duracao').value) || 30,
        idEspecialidade: parseInt(document.getElementById('servico-especialidade').value),
        servicoEstado: document.getElementById('servico-estado').value === 'true'
    };

    try {
        if (id) {
            await endpoints.updateServico(id, data);
            toast.success('Servico atualizado com sucesso!');
        } else {
            await endpoints.createServico(data);
            toast.success('Servico criado com sucesso!');
        }
        closeModal('servico');
        loadServicos();
    } catch (error) {
        toast.error(error?.message || 'Erro ao salvar servico');
    }
}

function editServico(id, nome) {
    if (String(appStore.get('user')?.role || '').toLowerCase() === 'secretaria') {
        toast.warning('Perfil secretaria nao pode editar servicos');
        return;
    }
    const servicos = appStore.get('servicos') || [];
    const servico = servicos.find(s => s.idEspecialidade === id && s.servicoNome === nome);
    if (!servico) return;

    document.getElementById('servico-id').value = id;
    document.getElementById('servico-nome').value = servico.servicoNome;
    document.getElementById('servico-preco').value = servico.servicoPreco;
    document.getElementById('servico-duracao').value = servico.servicoDuracaoMinuto;
    document.getElementById('servico-especialidade').value = servico.idEspecialidade;
    document.getElementById('servico-estado').value = servico.servicoEstado ? 'true' : 'false';

    document.getElementById('modal-servico-title').textContent = 'Editar Servico';
    openModal('servico');
}

async function deleteServico(id) {
    if (String(appStore.get('user')?.role || '').toLowerCase() === 'secretaria') {
        toast.warning('Perfil secretaria nao pode editar servicos');
        return;
    }
    if (!confirm('Tem certeza que deseja excluir este servico?')) return;
    try {
        await endpoints.deleteServico(id);
        toast.success('Servico excluido com sucesso!');
        loadServicos();
    } catch (error) {
        toast.error(error?.message || 'Erro ao excluir servico');
    }
}

function marcarConsultaServico(idEspecialidade, nomeServico) {
    appStore.set({
        consultaDraftSource: {
            tipo: 'servico',
            idEspecialidade,
            nome: nomeServico || ''
        }
    });
    router.navigate('consultas');
}

window.renderServicos = renderServicos;
window.marcarConsultaServico = marcarConsultaServico;
