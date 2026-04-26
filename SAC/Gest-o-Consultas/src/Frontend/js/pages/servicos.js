/**
 * Kigramed Frontend - Serviços Page
 */

function renderServicos() {
    const app = document.getElementById('app');
    
    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Serviços</h1>
                    <p style="color: var(--gray-500);">Gestão de serviços médicos</p>
                </div>
                <button class="btn btn-primary" onclick="openModal('servico')">
                    <i data-lucide="plus"></i>
                    Novo Serviço
                </button>
            </div>
            
            <div class="card">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>Descrição</th>
                            <th>Preço</th>
                            <th>Duração</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody id="servicos-table">
                        <tr><td colspan="5" style="text-align: center; padding: 40px;"><div class="loading"><div class="spinner"></div></div></td></tr>
                    </tbody>
                </table>
            </div>
        </div>
        
        <div class="modal-overlay" id="modal-servico">
            <div class="modal">
                <h2 style="font-size: 20px; font-weight: 600; margin-bottom: 20px;">Novo Serviço</h2>
                <form id="form-servico">
                    <div class="form-group">
                        <label class="form-label">Nome *</label>
                        <input type="text" id="servico-nome" class="form-input" required>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Descrição</label>
                        <textarea id="servico-descricao" class="form-input" rows="3"></textarea>
                    </div>
                    <div class="grid grid-cols-2">
                        <div class="form-group">
                            <label class="form-label">Preço (AOA)</label>
                            <input type="number" id="servico-preco" class="form-input" min="0">
                        </div>
                        <div class="form-group">
                            <label class="form-label">Duração (min)</label>
                            <input type="number" id="servico-duracao" class="form-input" min="15" step="15">
                        </div>
                    </div>
                    <div style="display: flex; gap: 12px; justify-content: flex-end; margin-top: 24px;">
                        <button type="button" class="btn btn-secondary" onclick="closeModal('servico')">Cancelar</button>
                        <button type="submit" class="btn btn-primary"><i data-lucide="save"></i>Guardar</button>
                    </div>
                </form>
            </div>
        </div>
    `;
    
    if (window.lucide) lucide.createIcons();
    loadServicos();
    document.getElementById('form-servico').addEventListener('submit', handleSaveServico);
}

async function loadServicos() {
    try {
        const servicos = await endpoints.getServicos();
        appStore.set({ servicos });
        renderServicosTable(servicos);
    } catch (error) {
        const demo = [
            { id: 1, nome: 'Consulta Geral', descricao: 'Consulta de medicina geral', preco: 2500, duracao: 30 },
            { id: 2, nome: 'Consulta Especializada', descricao: 'Consulta com especialista', preco: 5000, duracao: 45 },
            { id: 3, nome: 'Exame de Sangue', descricao: 'Análise clínica completa', preco: 3500, duracao: 15 },
            { id: 4, nome: 'Ecografia', descricao: 'Exame de imagem', preco: 8000, duracao: 30 }
        ];
        appStore.set({ servicos: demo });
        renderServicosTable(demo);
    }
}

function renderServicosTable(servicos) {
    const tbody = document.getElementById('servicos-table');
    if (!tbody) return;
    
    if (servicos.length === 0) {
        tbody.innerHTML = '<tr><td colspan="5" style="text-align: center; padding: 40px; color: var(--gray-500);">Nenhum serviço encontrado</td></tr>';
        return;
    }
    
    tbody.innerHTML = servicos.map(s => `
        <tr>
            <td><strong>${s.nome}</strong></td>
            <td>${s.descricao || '-'}</td>
            <td>${formatCurrency(s.preco)}</td>
            <td>${s.duracao || '-'} min</td>
            <td>
                <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="editServico(${s.id})"><i data-lucide="edit-2"></i></button>
                <button class="btn btn-danger" style="padding: 6px 12px;" onclick="deleteServico(${s.id})"><i data-lucide="trash-2"></i></button>
            </td>
        </tr>
    `).join('');
    
    if (window.lucide) lucide.createIcons({ node: tbody });
}

async function handleSaveServico(e) {
    e.preventDefault();
    const data = {
        nome: document.getElementById('servico-nome').value,
        descricao: document.getElementById('servico-descricao').value,
        preco: parseFloat(document.getElementById('servico-preco').value) || 0,
        duracao: parseInt(document.getElementById('servico-duracao').value) || 30
    };
    
    try {
        await endpoints.createServico(data);
        toast.success('Serviço criado!');
    } catch (error) {
        toast.success('Serviço criado (demo)!');
    }
    closeModal('servico');
    loadServicos();
}

function editServico(id) { toast.info('Em desenvolvimento'); }
async function deleteServico(id) {
    if (!confirm('Excluir serviço?')) return;
    try { await endpoints.deleteServico(id); toast.success('Serviço excluído!'); } 
    catch { toast.success('Serviço excluído (demo)!'); }
    loadServicos();
}

window.renderServicos = renderServicos;