/**
 * Kigramed Frontend - Página de Login
 */

function renderLoginPage() {
    const app = document.getElementById('app');
    
    app.innerHTML = `
        <div class="login-page">
            <div class="login-card animate-fade-in">
                <div style="text-align: center; margin-bottom: 32px;">
                    <div style="width: 64px; height: 64px; background: var(--primary); border-radius: 16px; display: flex; align-items: center; justify-content: center; margin: 0 auto 16px;">
                        <i data-lucide="calendar" style="width: 32px; height: 32px; color: white;"></i>
                    </div>
                    <h1 style="font-size: 28px; font-weight: 700; color: var(--gray-800);">Kigramed</h1>
                    <p style="color: var(--gray-500); margin-top: 8px;">Sistema de Agendamento de Consultas</p>
                </div>
                
                <form id="login-form">
                    <div class="form-group">
                        <label class="form-label">NIF / Número de Funcionário</label>
                        <input type="text" id="login-nif" class="form-input" placeholder="Digite o NIF ou número de funcionário" required>
                    </div>
                    
                    <div class="form-group">
                        <label class="form-label">Senha</label>
                        <input type="password" id="login-password" class="form-input" placeholder="Digite a senha" required>
                    </div>
                    
                    <button type="submit" class="btn btn-primary" style="width: 100%; justify-content: center; padding: 12px;">
                        <i data-lucide="log-in"></i>
                        Entrar
                    </button>
                </form>
                
                <div style="margin-top: 24px; text-align: center;">
                    <p style="font-size: 13px; color: var(--gray-500);">
                        Demo: Use qualquer NIF e senha para testar
                    </p>
                </div>
            </div>
        </div>
    `;
    
    // Inicializar ícones
    if (window.lucide) {
        lucide.createIcons();
    }
    
    // Event listener do formulário
    document.getElementById('login-form').addEventListener('submit', handleLogin);
}

/**
 * Handler do login
 */
async function handleLogin(e) {
    e.preventDefault();
    
    const nif = document.getElementById('login-nif').value;
    const password = document.getElementById('login-password').value;
    
    if (!nif || !password) {
        toast.error('Por favor, preencha todos os campos');
        return;
    }
    
    try {
        appStore.set({ loading: true });
        
        // Fazer chamada real à API
        const response = await endpoints.login({ nif, password });
        
        // Salvar credenciais com authManager
        authManager.saveCredentials(response.token, response.user || { nif, nome: response.nome || 'Usuário' }, 120 * 60);
        
        // Atualizar store
        appStore.set({
            isAuthenticated: true,
            token: response.token,
            user: response.user || { nif, nome: response.nome || 'Usuário' },
            loading: false
        });
        
        toast.success('Login realizado com sucesso!');
        router.navigate('dashboard');
        
    } catch (error) {
        appStore.set({ loading: false });
        
        console.error('Erro ao fazer login:', error);
        
        const message = error?.message || 'Erro ao conectar com o servidor';
        
        // Para desenvolvimento, permitir demo
        if (error?.status === 0 || error?.message?.includes('Failed to fetch')) {
            toast.warning('API indisponível. Usando modo demo.');
            
            const demoUser = { 
                nif, 
                nome: 'Administrador Demo', 
                perfil: 'Admin',
                email: `${nif}@kigramed.com`
            };
            
            authManager.saveCredentials('demo-token-' + Date.now(), demoUser);
            
            appStore.set({
                isAuthenticated: true,
                token: 'demo-token',
                user: demoUser
            });
            
            router.navigate('dashboard');
        } else {
            toast.error(message);
        }
    }
}

// Exportar
window.renderLoginPage = renderLoginPage;