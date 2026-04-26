/**
 * Kigramed Frontend - Especialidades Page
 */

function renderEspecialidades() {
    const app = document.getElementById('app');
    
    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Especialidades</h1>
                    <p style="color: var(--gray-500);">Gestão de especialidades médicas</p>
                </div>
                <button class="btn btn-primary" onclick="openModal('especialidade')">
                    <i data-lucide="plus"></i>
                    Nova Especialidade
                </button>
            </div>
            
            <div class="grid grid-cols-3" id="especialidades-grid">
                <div class="loading"><div class="spinner"></div></div>
            </div>
        </div>
        
        <div class="modal-overlay" id="modal-especialidade">
            <div class="modal">
                <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
                    <h2 style="font-size: 20px; font-weight: 600;" id="modal-especialidade-title">Nova Especialidade</h2>
                    <button class="btn btn-secondary" style="padding: 8px;" onclick="closeModal('especialidade')">
                        <i data-lucide="x"></i>
                    </button>
                </div>
                <form id="form-especialidade">
                    <input type="hidden" id="especialidade-id">
                    <div class="form-group">
                        <label class="form-label">Nome *</label>
                        <input type="text" id="esp-nome" class="form-input" placeholder="Cardiologia" required>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Descrição</label>
                        <textarea id="esp-descricao" class="form-input" rows="3" placeholder="Descrição da especialidade..."></textarea>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Estado</label>
                        <select id="esp-estado" class="form-input">
                            <option value="true">Ativo</option>
                            <option value="false">Inativo</option>
                        </select>
                    </div>
                    <div style="display: flex; gap: 12px; justify-content: flex-end; margin-top: 24px;">
                        <button type="button" class="btn btn-secondary" onclick="closeModal('especialidade')">Cancelar</button>
                        <button type="submit" class="btn btn-primary">
                            <i data-lucide="save"></i>Guardar
                        </button>
                    </div>
                </form>
            </div>
        </div>
    `;
    
    if (window.lucide) lucide.createIcons();
    loadEspecialidades();
    document.getElementById('form-especialidade').addEventListener('submit', handleSaveEspecialidade);
}

async function loadEspecialidades() {
    try {
        const especialidades = await endpoints.getEspecialidades();
        appStore.set({ especialidades });
        renderEspecialidadesGrid(especialidades);
    } catch (error) {
        console.error('Erro ao carregar especialidades:', error);
        toast.error(error?.message || 'Erro ao carregar especialidades');
        document.getElementById('especialidades-grid').innerHTML = `
            <div style="grid-column: 1/-1; text-align: center; padding: 40px; color: var(--gray-500);">
                Erro ao carregar especialidades
            </div>`;
    }
}

function renderEspecialidadesGrid(especialidades) {
    const grid = document.getElementById('especialidades-grid');
    if (!grid) return;
    
    if (especialidades.length === 0) {
        grid.innerHTML = `
            <div style="grid-column: 1/-1; text-align: center; padding: 40px; color: var(--gray-500);">
                Nenhuma especialidade encontrada
            </div>`;
        return;
    }
    
    grid.innerHTML = especialidades.map(e => `
        <div class="card animate-fade-in">
            <div style="display: flex; align-items: center; gap: 16px; margin-bottom: 12px;">
                <div style="width: 48px; height: 48px; background: linear-gradient(135deg, #0ea5e9, #8b5cf6); border-radius: 12px; display: flex; align-items: center; justify-content: center;">
                    <i data-lucide="stethoscope" style="color: white;"></i>
                </div>
                <div>
                    <h3 style="font-size: 18px; font-weight: 600;">${e.especialidadeNome || '-'}</h3>
                    <span class="badge badge-${e.especialidadeEstado ? 'success' : 'danger'}">
                        ${e.especialidadeEstado ? 'Ativo' : 'Inativo'}
                    </span>
                </div>
            </div>
            <p style="color: var(--gray-500); font-size: 14px; margin-bottom: 8px;">
                ${e.especialidadeDescricao || 'Sem descrição'}
            </p>
            <p style="font-size: 13px; color: var(--gray-400); margin-bottom: 8px;">
                Médicos: ${e.medicoEspecialidade?.map(m => m.funcionarioNome).join(', ') || 'Nenhum'}
            </p>
            <p style="font-size: 13px; color: var(--gray-400); margin-bottom: 16px;">
                Serviços: ${e.servicos?.map(s => s.servicoDescricao).join(', ') || 'Nenhum'}
            </p>
            <div style="display: flex; gap: 8px;">
                <button class="btn btn-secondary" style="flex: 1;" onclick="editEspecialidade(${e.especialidadeId})">
                    <i data-lucide="edit-2"></i> Editar
                </button>
                <button class="btn btn-danger" style="padding: 8px;" onclick="deleteEspecialidade(${e.especialidadeId})">
                    <i data-lucide="trash-2"></i>
                </button>
            </div>
        </div>
    `).join('');
    
    if (window.lucide) lucide.createIcons({ node: grid });
}

async function handleSaveEspecialidade(e) {
    e.preventDefault();
    
    const id = document.getElementById('especialidade-id').value;
    const data = {
        especialidadeNome: document.getElementById('esp-nome').value,
        especialidadeDescricao: document.getElementById('esp-descricao').value,
        especialidadeEstado: document.getElementById('esp-estado').value === 'true'
    };
    
    try {
        if (id) {
            await endpoints.updateEspecialidade(id, data);
            toast.success('Especialidade atualizada com sucesso!');
        } else {
            await endpoints.createEspecialidade(data);
            toast.success('Especialidade criada com sucesso!');
        }
        closeModal('especialidade');
        loadEspecialidades();
    } catch (error) {
        toast.error(error?.message || 'Erro ao salvar especialidade');
    }
}

function editEspecialidade(id) {
    const especialidades = appStore.get('especialidades') || [];
    const esp = especialidades.find(e => e.especialidadeId === id);
    if (!esp) return;

    document.getElementById('especialidade-id').value = esp.especialidadeId;
    document.getElementById('esp-nome').value = esp.especialidadeNome;
    document.getElementById('esp-descricao').value = esp.especialidadeDescricao || '';
    document.getElementById('esp-estado').value = esp.especialidadeEstado ? 'true' : 'false';

    document.getElementById('modal-especialidade-title').textContent = 'Editar Especialidade';
    openModal('especialidade');
}

async function deleteEspecialidade(id) {
    if (!confirm('Tem certeza que deseja excluir esta especialidade?')) return;
    try {
        await endpoints.deleteEspecialidade(id);
        toast.success('Especialidade excluída com sucesso!');
        loadEspecialidades();
    } catch (error) {
        toast.error(error?.message || 'Erro ao excluir especialidade');
    }
}

window.renderEspecialidades = renderEspecialidades;