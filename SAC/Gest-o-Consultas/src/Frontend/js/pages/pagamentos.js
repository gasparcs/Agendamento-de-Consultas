/**
 * Kigramed Frontend - Pagamentos Page
 */

function renderPagamentos() {
    const app = document.getElementById('app');
    
    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Pagamentos</h1>
                    <p style="color: var(--gray-500);">Gestão de pagamentos</p>
                </div>
                <button class="btn btn-primary" onclick="openModal('pagamento')">
                    <i data-lucide="plus"></i>
                    Novo Pagamento
                </button>
            </div>
            
            <!-- Stats -->
            <div class="grid grid-cols-4" style="margin-bottom: 24px;">
                <div class="stat-card">
                    <div class="stat-value" style="color: var(--success);" id="total-recebido">0</div>
                    <div class="stat-label">Total Recebido</div>
                </div>
                <div class="stat-card">
                    <div class="stat-value" style="color: var(--warning);" id="total-pendente">0</div>
                    <div class="stat-label">Pendente</div>
                </div>
                <div class="stat-card">
                    <div class="stat-value" id="total-transacoes">0</div>
                    <div class="stat-label">Transações</div>
                </div>
                <div class="stat-card">
                    <div class="stat-value" id="media-transacao">0</div>
                    <div class="stat-label">Média por Transação</div>
                </div>
            </div>
            
            <div class="card">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Data</th>
                            <th>Paciente</th>
                            <th>Consulta</th>
                            <th>Valor</th>
                            <th>Método</th>
                            <th>Estado</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody id="pagamentos-table">
                        <tr><td colspan="7" style="text-align: center; padding: 40px;"><div class="loading"><div class="spinner"></div></div></td></tr>
                    </tbody>
                </table>
            </div>
        </div>
        
        <div class="modal-overlay" id="modal-pagamento">
            <div class="modal">
                <h2 style="font-size: 20px; font-weight: 600; margin-bottom: 20px;">Novo Pagamento</h2>
                <form id="form-pagamento">
                    <div class="form-group">
                        <label class="form-label">Paciente *</label>
                        <select id="pag-paciente" class="form-input" required>
                            <option value="">Selecione...</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Valor (AOA) *</label>
                        <input type="number" id="pag-valor" class="form-input" min="0" required>
                    </div>
                    <div class="form-group">
                        <label class="form-label">Método</label>
                        <select id="pag-metodo" class="form-input">
                            <option value="Dinheiro">Dinheiro</option>
                            <option value="Transferência">Transferência</option>
                            <option value="Multicaixa">Multicaixa</option>
                            <option value="POS">POS</option>
                        </select>
                    </div>
                    <div style="display: flex; gap: 12px; justify-content: flex-end; margin-top: 24px;">
                        <button type="button" class="btn btn-secondary" onclick="closeModal('pagamento')">Cancelar</button>
                        <button type="submit" class="btn btn-primary"><i data-lucide="save"></i>Guardar</button>
                    </div>
                </form>
            </div>
        </div>
    `;
    
    if (window.lucide) lucide.createIcons();
    loadPagamentos();
    document.getElementById('form-pagamento').addEventListener('submit', handleSavePagamento);
}

async function loadPagamentos() {
    try {
        const pagamentos = await endpoints.getPagamentos();
        appStore.set({ pagamentos });
        renderPagamentosTable(pagamentos);
    } catch (error) {
        // ✅ Remove os dados demo e mostra mensagem vazia
        appStore.set({ pagamentos: [] });
        renderPagamentosTable([]);
        toast.warning('Módulo de pagamentos não disponível.');
    }
}

function renderPagamentosTable(pagamentos) {
    const tbody = document.getElementById('pagamentos-table');
    if (!tbody) return;
    
    // Calcular stats
    const totalRecebido = pagamentos.filter(p => p.estado === 'Pago').reduce((sum, p) => sum + (p.valor || 0), 0);
    const totalPendente = pagamentos.filter(p => p.estado === 'Pendente').reduce((sum, p) => sum + (p.valor || 0), 0);
    
    document.getElementById('total-recebido').textContent = formatCurrency(totalRecebido);
    document.getElementById('total-pendente').textContent = formatCurrency(totalPendente);
    document.getElementById('total-transacoes').textContent = pagamentos.length;
    document.getElementById('media-transacao').textContent = formatCurrency(pagamentos.length ? totalRecebido / pagamentos.length : 0);
    
    if (pagamentos.length === 0) {
        tbody.innerHTML = '<tr><td colspan="7" style="text-align: center; padding: 40px; color: var(--gray-500);">Nenhum pagamento encontrado</td></tr>';
        return;
    }
    
    tbody.innerHTML = pagamentos.map(p => `
        <tr>
            <td>${new Date(p.data).toLocaleDateString('pt-PT')}</td>
            <td>${p.paciente || '-'}</td>
            <td>${p.consulta || '-'}</td>
            <td><strong>${formatCurrency(p.valor)}</strong></td>
            <td>${p.metodo || '-'}</td>
            <td><span class="badge badge-${p.estado === 'Pago' ? 'success' : 'warning'}">${p.estado}</span></td>
            <td>
                <button class="btn btn-danger" style="padding: 6px 12px;" onclick="deletePagamento(${p.id})"><i data-lucide="trash-2"></i></button>
            </td>
        </tr>
    `).join('');
    
    if (window.lucide) lucide.createIcons({ node: tbody });
}

async function handleSavePagamento(e) {
    e.preventDefault();
    const data = {
        valor: parseFloat(document.getElementById('pag-valor').value),
        metodo: document.getElementById('pag-metodo').value,
        estado: 'Pago'
    };
    
    try {
        await endpoints.createPagamento(data);
        toast.success('Pagamento registado!');
    } catch (error) {
        toast.success('Pagamento registado (demo)!');
    }
    closeModal('pagamento');
    loadPagamentos();
}

async function deletePagamento(id) {
    if (!confirm('Excluir pagamento?')) return;
    try { await endpoints.deletePagamento(id); toast.success('Pagamento excluído!'); } 
    catch { toast.success('Pagamento excluído (demo)!'); }
    loadPagamentos();
}

window.renderPagamentos = renderPagamentos;