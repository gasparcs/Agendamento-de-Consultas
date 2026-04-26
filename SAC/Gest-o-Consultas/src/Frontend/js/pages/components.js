/**
 * Kigramed Frontend - Sidebar Component
 */

function renderSidebar() {
    const currentRoute = router.getCurrentRoute();
    const user = appStore.get('user');
    
    return `
        <div class="sidebar">
            <div class="sidebar-logo">
                <i data-lucide="calendar"></i>
                <span>Kigramed</span>
            </div>
            
            <nav>
                <a href="#dashboard" class="sidebar-item ${currentRoute === 'dashboard' ? 'active' : ''}">
                    <i data-lucide="layout-dashboard"></i>
                    <span>Dashboard</span>
                </a>
                
                <a href="#consultas" class="sidebar-item ${currentRoute === 'consultas' ? 'active' : ''}">
                    <i data-lucide="calendar"></i>
                    <span>Consultas</span>
                </a>
                
                <a href="#clientes" class="sidebar-item ${currentRoute === 'clientes' ? 'active' : ''}">
                    <i data-lucide="building-2"></i>
                    <span>Clientes</span>
                </a>
                
                <a href="#pacientes" class="sidebar-item ${currentRoute === 'pacientes' ? 'active' : ''}">
                    <i data-lucide="users"></i>
                    <span>Pacientes</span>
                </a>
                
                <a href="#especialidades" class="sidebar-item ${currentRoute === 'especialidades' ? 'active' : ''}">
                    <i data-lucide="stethoscope"></i>
                    <span>Especialidades</span>
                </a>
                
                <a href="#servicos" class="sidebar-item ${currentRoute === 'servicos' ? 'active' : ''}">
                    <i data-lucide="briefcase"></i>
                    <span>Serviços</span>
                </a>
                
                <a href="#funcionarios" class="sidebar-item ${currentRoute === 'funcionarios' ? 'active' : ''}">
                    <i data-lucide="user-cog"></i>
                    <span>Funcionários</span>
                </a>
                
                <a href="#pagamentos" class="sidebar-item ${currentRoute === 'pagamentos' ? 'active' : ''}">
                    <i data-lucide="credit-card"></i>
                    <span>Pagamentos</span>
                </a>
                
                <a href="#relatorios" class="sidebar-item ${currentRoute === 'relatorios' ? 'active' : ''}">
                    <i data-lucide="bar-chart-2"></i>
                    <span>Relatórios</span>
                </a>
                
                <a href="#configuracoes" class="sidebar-item ${currentRoute === 'configuracoes' ? 'active' : ''}">
                    <i data-lucide="settings"></i>
                    <span>Configurações</span>
                </a>
            </nav>
            
            <div style="position: absolute; bottom: 20px; left: 20px; right: 20px; padding-top: 20px; border-top: 1px solid rgba(255,255,255,0.1);">
                <div style="display: flex; align-items: center; gap: 12px; padding: 12px; background: rgba(255,255,255,0.05); border-radius: 8px;">
                    <div style="width: 40px; height: 40px; background: var(--primary); border-radius: 8px; display: flex; align-items: center; justify-content: center;">
                        <i data-lucide="user" style="color: white;"></i>
                    </div>
                    <div style="flex: 1; overflow: hidden;">
                        <div style="font-weight: 500; font-size: 14px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">${user?.nome || 'Usuário'}</div>
                        <div style="font-size: 12px; color: var(--gray-400);">${user?.perfil || 'Admin'}</div>
                    </div>
                    <button class="btn" style="padding: 6px; color: var(--gray-400);" onclick="handleLogout()">
                        <i data-lucide="log-out"></i>
                    </button>
                </div>
            </div>
        </div>
    `;
}

/**
 * Logout
 */
function handleLogout() {
    authManager.clearCredentials();
    appStore.set({
        isAuthenticated: false,
        token: null,
        user: null
    });
    toast.info('Sessão encerrada');
    router.navigate('login');
}

// Exportar
window.renderSidebar = renderSidebar;