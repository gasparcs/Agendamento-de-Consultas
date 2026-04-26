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
                <h2 style="font-size: 20px; font-weight: 600; margin-bottom: 20px;">Nova Especialidade</h2>
                <form id="form-especialidade">
                    <div class="form-group">
                        <label class="form-label">Nome *</label>
                        <input type="text" id="esp-nome" class="form-input" placeholder="Cardiologia" required>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Descrição</label>
                        <textarea id="esp-descricao" class="form-input" rows="3" placeholder="Descrição da especialidade..."></textarea>
                    </div>
                    <div style="display: flex; gap: 12px; justify-content: flex-end; margin-top: 24px;">
                        <button type="button" class="btn btn-secondary" onclick="closeModal('especialidade')">Cancelar</button>
                        <button type="submit" class="btn btn-primary"><i data-lucide="save"></i>Guardar</button>
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
        const demo = [
            { id: 1, nome: 'Cardiologia', descricao: 'Doenças do coração e sistema cardiovascular' },
            { id: 2, nome: 'Dermatologia', descricao: 'Tratamento de pele, cabelos e unhas' },
            { id: 3, nome: 'Pediatria', descricao: 'Cuidados de saúde infantil' },
            { id: 4, nome: 'Ortopedia', descricao: 'Sistema musculoesquelético' },
            { id: 5, nome: 'Neurologia', descricao: 'Doenças do sistema nervoso' },
            { id: 6, nome: 'Oftalmologia', descricao: 'Tratamento de doenças oculares' }
        ];
        appStore.set({ especialidades: demo });
        renderEspecialidadesGrid(demo);
    }
}

function renderEspecialidadesGrid(especialidades) {
    const grid = document.getElementById('especialidades-grid');
    if (!grid) return;
    
    if (especialidades.length === 0) {
        grid.innerHTML = '<div style="grid-column: 1/-1; text-align: center; padding: 40px; color: var(--gray-500);">Nenhuma especialidade encontrada</div>';
        return;
    }
    
    grid.innerHTML = especialidades.map(e => `
        <div class="card animate-fade-in">
            <div style="display: flex; align-items: center; gap: 16px; margin-bottom: 12px;">
                <div style="width: 48px; height: 48px; background: linear-gradient(135deg, #0ea5e9, #8b5cf6); border-radius: 12px; display: flex; align-items: center; justify-content: center;">
                    <i data-lucide="stethoscope" style="color: white;"></i>
                </div>
                <h3 style="font-size: 18px; font-weight: 600;">${e.nome}</h3>
            </div>
            <p style="color: var(--gray-500); font-size: 14px; margin-bottom: 16px;">${e.descricao || 'Sem descrição'}</p>
            <div style="display: flex; gap: 8px;">
                <button class="btn btn-secondary" style="flex: 1;" onclick="editEspecialidade(${e.id})"><i data-lucide="edit-2"></i> Editar</button>
                <button class="btn btn-danger" style="padding: 8px;" onclick="deleteEspecialidade(${e.id})"><i data-lucide="trash-2"></i></button>
            </div>
        </div>
    `).join('');
    
    if (window.lucide) lucide.createIcons({ node: grid });
}

async function handleSaveEspecialidade(e) {
    e.preventDefault();
    const data = {
        nome: document.getElementById('esp-nome').value,
        descricao: document.getElementById('esp-descricao').value
    };
    
    try {
        await endpoints.createEspecialidade(data);
        toast.success('Especialidade criada!');
    } catch (error) {
        toast.success('Especialidade criada (demo)!');
    }
    closeModal('especialidade');
    loadEspecialidades();
}

function editEspecialidade(id) { toast.info('Em desenvolvimento'); }
async function deleteEspecialidade(id) {
    if (!confirm('Excluir especialidade?')) return;
    try { await endpoints.deleteEspecialidade(id); toast.success('Especialidade excluída!'); } 
    catch { toast.success('Especialidade excluída (demo)!'); }
    loadEspecialidades();
}

window.renderEspecialidades = renderEspecialidades;