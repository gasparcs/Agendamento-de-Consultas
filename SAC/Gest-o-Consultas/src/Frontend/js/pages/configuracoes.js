/**
 * Kigramed Frontend - Configurações Page
 */

function renderConfiguracoes() {
    const app = document.getElementById('app');
    
    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Configurações</h1>
                    <p style="color: var(--gray-500);">Definições do sistema</p>
                </div>
            </div>
            
            <div class="grid grid-cols-2">
                <!-- Perfil -->
                <div class="card">
                    <div class="card-header">
                        <i data-lucide="user"></i>
                        Perfil do Utilizador
                    </div>
                    <form id="form-perfil">
                        <div class="form-group">
                            <label class="form-label">Nome</label>
                            <input type="text" id="config-nome" class="form-input" value="${appStore.get('user')?.nome || ''}">
                        </div>
                        <div class="form-group">
                            <label class="form-label">Email</label>
                            <input type="email" id="config-email" class="form-input" value="${appStore.get('user')?.email || ''}">
                        </div>
                        <div class="form-group">
                            <label class="form-label">Telefone</label>
                            <input type="text" id="config-telefone" class="form-input" value="${appStore.get('user')?.telefone || ''}">
                        </div>
                        <button type="submit" class="btn btn-primary">
                            <i data-lucide="save"></i>
                            Guardar Alterações
                        </button>
                    </form>
                </div>
                
                <!-- Segurança -->
                <div class="card">
                    <div class="card-header">
                        <i data-lucide="shield"></i>
                        Segurança
                    </div>
                    <form id="form-seguranca">
                        <div class="form-group">
                            <label class="form-label">Senha Atual</label>
                            <input type="password" id="config-senha-atual" class="form-input" placeholder="••••••••">
                        </div>
                        <div class="form-group">
                            <label class="form-label">Nova Senha</label>
                            <input type="password" id="config-nova-senha" class="form-input" placeholder="••••••••">
                        </div>
                        <div class="form-group">
                            <label class="form-label">Confirmar Senha</label>
                            <input type="password" id="config-confirmar-senha" class="form-input" placeholder="••••••••">
                        </div>
                        <button type="submit" class="btn btn-primary">
                            <i data-lucide="lock"></i>
                            Alterar Senha
                        </button>
                    </form>
                </div>
                
                <!-- Clínica -->
                <div class="card">
                    <div class="card-header">
                        <i data-lucide="building"></i>
                        Dados da Clínica
                    </div>
                    <form id="form-clinica">
                        <div class="form-group">
                            <label class="form-label">Nome da Clínica</label>
                            <input type="text" id="config-nome-clinica" class="form-input" value="Kigramed - Centro Médico">
                        </div>
                        <div class="form-group">
                            <label class="form-label">Endereço</label>
                            <input type="text" id="config-endereco" class="form-input" value="Luanda, Angola">
                        </div>
                        <div class="form-group">
                            <label class="form-label">Telefone</label>
                            <input type="text" id="config-telefone-clinica" class="form-input" value="+244 900 000 000">
                        </div>
                        <div class="form-group">
                            <label class="form-label">Email</label>
                            <input type="email" id="config-email-clinica" class="form-input" value="contacto@kigramed.com">
                        </div>
                        <button type="submit" class="btn btn-primary">
                            <i data-lucide="save"></i>
                            Guardar
                        </button>
                    </form>
                </div>
                
                <!-- Sistema -->
                <div class="card">
                    <div class="card-header">
                        <i data-lucide="settings"></i>
                        Configurações do Sistema
                    </div>
                    <form id="form-sistema">
                        <div class="form-group">
                            <label class="form-label">Idioma</label>
                            <select id="config-idioma" class="form-input">
                                <option value="pt-PT" selected>Português (Angola)</option>
                                <option value="pt-BR">Português (Brasil)</option>
                                <option value="en">English</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Fuso Horário</label>
                            <select id="config-fuso" class="form-input">
                                <option value="Africa/Luanda" selected>Africa/Luanda (WAT)</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Moeda</label>
                            <select id="config-moeda" class="form-input">
                                <option value="AOA" selected>Kwanza Angolano (AOA)</option>
                                <option value="USD">Dólar Americano (USD)</option>
                                <option value="EUR">Euro (EUR)</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label style="display: flex; align-items: center; gap: 8px; cursor: pointer;">
                                <input type="checkbox" id="config-notificacoes" checked>
                                <span>Ativar notificações por email</span>
                            </label>
                        </div>
                        <button type="submit" class="btn btn-primary">
                            <i data-lucide="save"></i>
                            Guardar
                        </button>
                    </form>
                </div>
            </div>
            
            <!-- Informações do Sistema -->
            <div class="card" style="margin-top: 24px;">
                <div class="card-header">
                    <i data-lucide="info"></i>
                    Informações do Sistema
                </div>
                <div style="display: grid; grid-template-columns: repeat(4, 1fr); gap: 24px;">
                    <div>
                        <div style="color: var(--gray-500); font-size: 12px;">Versão</div>
                        <div style="font-weight: 600;">1.0.0</div>
                    </div>
                    <div>
                        <div style="color: var(--gray-500); font-size: 12px;">API</div>
                        <div style="font-weight: 600;">http://localhost:5000</div>
                    </div>
                    <div>
                        <div style="color: var(--gray-500); font-size: 12px;">Ambiente</div>
                        <div style="font-weight: 600;">Desenvolvimento</div>
                    </div>
                    <div>
                        <div style="color: var(--gray-500); font-size: 12px;">Última Atualização</div>
                        <div style="font-weight: 600;">${new Date().toLocaleDateString('pt-PT')}</div>
                    </div>
                </div>
            </div>
        </div>
    `;
    
    if (window.lucide) lucide.createIcons();
    
    // Event listeners
    document.getElementById('form-perfil')?.addEventListener('submit', (e) => {
        e.preventDefault();
        toast.success('Perfil atualizado!');
    });
    
    document.getElementById('form-seguranca')?.addEventListener('submit', (e) => {
        e.preventDefault();
        const novaSenha = document.getElementById('config-nova-senha').value;
        const confirmarSenha = document.getElementById('config-confirmar-senha').value;
        
        if (novaSenha !== confirmarSenha) {
            toast.error('As senhas não coincidem');
            return;
        }
        
        toast.success('Senha alterada com sucesso!');
    });
    
    document.getElementById('form-clinica')?.addEventListener('submit', (e) => {
        e.preventDefault();
        toast.success('Dados da clínica guardados!');
    });
    
    document.getElementById('form-sistema')?.addEventListener('submit', (e) => {
        e.preventDefault();
        toast.success('Configurações do sistema guardadas!');
    });
}

window.renderConfiguracoes = renderConfiguracoes;