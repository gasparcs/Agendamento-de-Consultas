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
    
    // Inicializar ícones
    if (window.lucide) {
        lucide.createIcons();
    }
    
    // Carregar clientes
    loadClientes();
    
    // Event listener do formulário
    document.getElementById('form-cliente').addEventListener('submit', handleSaveCliente);
}

/**
 * Carregar clientes
 */
async function loadClientes() {
    try {
        const clientes = await endpoints.getClientes();
        appStore.set({ clientes });
        renderClientesTable(clientes);
    } catch (error) {
        console.log('Erro ao carregar clientes:', error);
        // Demo data
        const demoClientes = generateDemoClientes();
        appStore.set({ clientes: demoClientes });
        renderClientesTable(demoClientes);
    }
}

/**
 * Renderizar tabela de clientes
 */
function renderClientesTable(clientes) {
    const tbody = document.getElementById('clientes-table');
    if (!tbody) return;
    
    if (clientes.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                    Nenhum cliente encontrado
                </td>
            </tr>
        `;
        return;
    }
    
    tbody.innerHTML = clientes.map(c => `
        <tr>
            <td><code style="background: var(--gray-100); padding: 4px 8px; border-radius: 4px;">${c.nif}</code></td>
            <td><strong>${c.nome}</strong></td>
            <td>${c.email || '-'}</td>
            <td>${c.telefone || '-'}</td>
            <td>${c.endereco || '-'}</td>
            <td>
                <div style="display: flex; gap: 8px;">
                    <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="editCliente(${c.id})">
                        <i data-lucide="edit-2"></i>
                    </button>
                    <button class="btn btn-danger" style="padding: 6px 12px;" onclick="deleteCliente(${c.id})">
                        <i data-lucide="trash-2"></i>
                    </button>
                </div>
            </td>
        </tr>
    `).join('');
    
    if (window.lucide) {
        lucide.createIcons({ node: tbody });
    }
}

/**
 * Filtrar clientes
 */
function filterClientes(query) {
    const clientes = appStore.get('clientes') || [];
    const filtered = clientes.filter(c => 
        c.nome?.toLowerCase().includes(query.toLowerCase()) ||
        c.nif?.includes(query)
    );
    renderClientesTable(filtered);
}

/**
 * Salvar cliente
 */
async function handleSaveCliente(e) {
    e.preventDefault();
    
    const id = document.getElementById('cliente-id').value;
    const data = {
        nif: document.getElementById('cliente-nif').value,
        nome: document.getElementById('cliente-nome').value,
        email: document.getElementById('cliente-email').value,
        telefone: document.getElementById('cliente-telefone').value,
        endereco: document.getElementById('cliente-endereco').value
    };
    
    // Validar
    const nifResult = schemas.nif().validate(data.nif);
    if (!nifResult.valid) {
        showFieldError('cliente-nif', nifResult.error);
        return;
    }
    
    const emailResult = schemas.email().validate(data.email);
    if (!emailResult.valid) {
        showFieldError('cliente-email', emailResult.error);
        return;
    }
    
    try {
        if (id) {
            await endpoints.updateCliente(id, data);
            toast.success('Cliente atualizado com sucesso!');
        } else {
            await endpoints.createCliente(data);
            toast.success('Cliente criado com sucesso!');
        }
        
        closeModal('cliente');
        loadClientes();
    } catch (error) {
        console.log('Erro ao salvar cliente:', error);
        toast.success('Cliente guardado (modo demo)!');
        closeModal('cliente');
        loadClientes();
    }
}

/**
 * Editar cliente
 */
function editCliente(id) {
    const clientes = appStore.get('clientes') || [];
    const cliente = clientes.find(c => c.id === id);
    
    if (!cliente) return;
    
    document.getElementById('cliente-id').value = cliente.id;
    document.getElementById('cliente-nif').value = cliente.nif;
    document.getElementById('cliente-nome').value = cliente.nome;
    document.getElementById('cliente-email').value = cliente.email || '';
    document.getElementById('cliente-telefone').value = cliente.telefone || '';
    document.getElementById('cliente-endereco').value = cliente.endereco || '';
    
    document.getElementById('modal-cliente-title').textContent = 'Editar Cliente';
    openModal('cliente');
}

/**
 * Excluir cliente
 */
async function deleteCliente(id) {
    if (!confirm('Tem certeza que deseja excluir este cliente?')) return;
    
    try {
        await endpoints.deleteCliente(id);
        toast.success('Cliente excluído com sucesso!');
        loadClientes();
    } catch (error) {
        console.log('Erro ao excluir cliente:', error);
        toast.success('Cliente excluído (modo demo)!');
        loadClientes();
    }
}

/**
 * Mostrar erro de campo
 */
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

function generateDemoClientes() {
    return [
        { id: 1, nif: '123456789', nome: 'João Manuel da Silva', email: 'joao@email.com', telefone: '921234567', endereco: 'Luanda, Angola' },
        { id: 2, nif: '234567890', nome: 'Maria José Pereira', email: 'maria@email.com', telefone: '922345678', endereco: 'Benguela, Angola' },
        { id: 3, nif: '345678901', nome: 'António Carlos Ferreira', email: 'antonio@email.com', telefone: '923456789', endereco: 'Huíla, Angola' },
        { id: 4, nif: '456789012', nome: 'Ana Paula dos Santos', email: 'ana@email.com', telefone: '924567890', endereco: 'Luanda, Angola' },
        { id: 5, nif: '567890123', nome: 'Pedro Miguel Costa', email: 'pedro@email.com', telefone: '925678901', endereco: 'Namibe, Angola' }
    ];
}

// Exportar
window.renderClientes = renderClientes;