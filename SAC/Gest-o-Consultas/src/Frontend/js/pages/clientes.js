/**
 * Kigramed Frontend - Clientes Page
 */

function renderClientes() {
    const app = document.getElementById('app');
    
    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Clientes</h1>
                    <p style="color: var(--gray-500);">Gestão de clientes</p>
                </div>
                <button class="btn btn-primary" onclick="openModal('cliente')">
                    <i data-lucide="plus"></i>
                    Novo Cliente
                </button>
            </div>
            
            <!-- Search & Filters -->
            <div class="card">
                <div style="display: flex; gap: 16px; flex-wrap: wrap;">
                    <div class="search-box" style="flex: 1; min-width: 250px;">
                        <i data-lucide="search"></i>
                        <input type="text" id="search-cliente" class="form-input" placeholder="Pesquisar por nome ou NIF..." oninput="filterClientes(this.value)">
                    </div>
                </div>
            </div>
            
            <!-- Clientes Table -->
            <div class="card">
                <table class="table">
                    <thead>
                        <tr>
                            <th>NIF</th>
                            <th>Nome</th>
                            <th>Email</th>
                            <th>Telefone</th>
                            <th>Endereço</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody id="clientes-table">
                        <tr>
                            <td colspan="6" style="text-align: center; padding: 40px;">
                                <div class="loading"><div class="spinner"></div></div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        
        <!-- Modal Cliente -->
        <div class="modal-overlay" id="modal-cliente">
            <div class="modal">
                <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
                    <h2 style="font-size: 20px; font-weight: 600;" id="modal-cliente-title">Novo Cliente</h2>
                    <button class="btn btn-secondary" style="padding: 8px;" onclick="closeModal('cliente')">
                        <i data-lucide="x"></i>
                    </button>
                </div>
                
                <form id="form-cliente">
                    <input type="hidden" id="cliente-id">
                    
                    <div class="form-group">
                        <label class="form-label">NIF *</label>
                        <input type="text" id="cliente-nif" class="form-input" placeholder="123456789" maxlength="9" required>
                        <div class="form-error" id="cliente-nif-error"></div>
                    </div>
                    
                    <div class="form-group">
                        <label class="form-label">Nome *</label>
                        <input type="text" id="cliente-nome" class="form-input" placeholder="Nome completo" required>
                        <div class="form-error" id="cliente-nome-error"></div>
                    </div>
                    
                    <div class="form-group">
                        <label class="form-label">Email</label>
                        <input type="email" id="cliente-email" class="form-input" placeholder="email@exemplo.com">
                        <div class="form-error" id="cliente-email-error"></div>
                    </div>
                    
                    <div class="form-group">
                        <label class="form-label">Telefone *</label>
                        <input type="text" id="cliente-telefone" class="form-input" placeholder="9xxxxxxxx" required>
                        <div class="form-error" id="cliente-telefone-error"></div>
                    </div>
                    
                    <div class="form-group">
                        <label class="form-label">Endereço</label>
                        <input type="text" id="cliente-endereco" class="form-input" placeholder="Endereço completo">
                    </div>
                    
                    <div style="display: flex; gap: 12px; justify-content: flex-end; margin-top: 24px;">
                        <button type="button" class="btn btn-secondary" onclick="closeModal('cliente')">Cancelar</button>
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
    loadClientes();
    document.getElementById('form-cliente').addEventListener('submit', handleSaveCliente);
}

async function loadClientes() {
    try {
        appStore.set({ loading: true });
        const clientes = await endpoints.getClientesByRole();
        appStore.set({ clientes, loading: false });
        renderClientesTable(clientes);
    } catch (error) {
        appStore.set({ loading: false });
        console.error('Erro ao carregar clientes:', error);
        
        if (error?.status === 0) {
            toast.error('API indisponível.');
            document.getElementById('clientes-table').innerHTML = `
                <tr><td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                    Servidor não está respondendo.
                </td></tr>`;
        } else {
            toast.error(error?.message || 'Erro ao carregar clientes');
            document.getElementById('clientes-table').innerHTML = `
                <tr><td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                    Erro ao carregar clientes
                </td></tr>`;
        }
    }
}

function renderClientesTable(clientes) {
    const tbody = document.getElementById('clientes-table');
    if (!tbody) return;
    
    if (clientes.length === 0) {
        tbody.innerHTML = `
            <tr><td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                Nenhum cliente encontrado
            </td></tr>`;
        return;
    }
    
    tbody.innerHTML = clientes.map(c => `
        <tr>
            <td><code style="background: var(--gray-100); padding: 4px 8px; border-radius: 4px;">${c.clienteNif || '-'}</code></td>
            <td><strong>${c.clienteNome || '-'}</strong></td>
            <td>${c.contactos?.find(ct => ct.tipoContacto?.descricao?.toLowerCase() === 'email')?.contacto || '-'}</td>
            <td>${c.contactos?.find(ct => ct.tipoContacto?.descricao?.toLowerCase() === 'telefone')?.contacto || '-'}</td>
            <td>-</td>
            <td>
                <div style="display: flex; gap: 8px;">
                    <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="editCliente('${c.clienteNif}')">
                        <i data-lucide="edit-2"></i>
                    </button>
                    <button class="btn btn-danger" style="padding: 6px 12px;" onclick="deleteCliente('${c.clienteNif}')">
                        <i data-lucide="trash-2"></i>
                    </button>
                </div>
            </td>
        </tr>
    `).join('');
    
    if (window.lucide) lucide.createIcons({ node: tbody });
}

function filterClientes(query) {
    const clientes = appStore.get('clientes') || [];
    const filtered = clientes.filter(c => 
        c.clienteNome?.toLowerCase().includes(query.toLowerCase()) ||
        c.clienteNif?.includes(query)
    );
    renderClientesTable(filtered);
}

async function handleSaveCliente(e) {
    e.preventDefault();
    
    const nif = document.getElementById('cliente-id').value;
    const data = {
        clienteNif: document.getElementById('cliente-nif').value,
        clienteNome: document.getElementById('cliente-nome').value,
        contactos: [
            {
                tipoContacto: 1,
                contacto: document.getElementById('cliente-telefone').value
            }
        ]
    };
    
    try {
        appStore.set({ loading: true });
        
        if (nif) {
            await endpoints.updateClienteByRole(nif, data);
            toast.success('Cliente atualizado com sucesso!');
        } else {
            await endpoints.createClienteByRole(data);
            toast.success('Cliente criado com sucesso!');
        }
        
        closeModal('cliente');
        loadClientes();
    } catch (error) {
        appStore.set({ loading: false });
        toast.error(error?.message || 'Erro ao salvar cliente');
    }
}

function editCliente(nif) {
    const clientes = appStore.get('clientes') || [];
    const cliente = clientes.find(c => c.clienteNif === nif);
    if (!cliente) return;

    document.getElementById('cliente-id').value = cliente.clienteNif;
    document.getElementById('cliente-nif').value = cliente.clienteNif;
    document.getElementById('cliente-nome').value = cliente.clienteNome;
    document.getElementById('cliente-email').value =
        cliente.contactos?.find(ct => ct.tipoContacto?.descricao?.toLowerCase() === 'email')?.contacto || '';
    document.getElementById('cliente-telefone').value =
        cliente.contactos?.find(ct => ct.tipoContacto?.descricao?.toLowerCase() === 'telefone')?.contacto || '';
    document.getElementById('cliente-endereco').value = '';

    document.getElementById('modal-cliente-title').textContent = 'Editar Cliente';
    openModal('cliente');
}

async function deleteCliente(nif) {
    if (!confirm('Tem certeza que deseja excluir este cliente?')) return;
    try {
        await endpoints.deleteClienteByRole(nif);
        toast.success('Cliente excluído com sucesso!');
        loadClientes();
    } catch (error) {
        toast.error(error?.message || 'Erro ao excluir cliente');
    }
}

function showFieldError(fieldId, message) {
    const errorEl = document.getElementById(`${fieldId}-error`);
    const inputEl = document.getElementById(fieldId);
    if (errorEl && inputEl) {
        errorEl.textContent = message;
        inputEl.classList.add('error');
        setTimeout(() => {
            errorEl.textContent = '';
            inputEl.classList.remove('error');
        }, 3000);
    }
}

window.renderClientes = renderClientes;
