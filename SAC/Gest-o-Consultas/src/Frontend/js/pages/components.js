/**
 * Kigramed Frontend - Sidebar Component
 */

function renderSidebar() {
    const currentRoute = router.getCurrentRoute();
    const user = appStore.get('user');
    const role = String(user?.role || '').toLowerCase();

    const menuByRole = {
        secretaria: [
            { route: 'dashboard', icon: 'layout-dashboard', label: 'Dashboard' },
            { route: 'consultas', icon: 'calendar', label: 'Consultas' },
            { route: 'clientes', icon: 'building-2', label: 'Clientes' },
            { route: 'pacientes', icon: 'users', label: 'Pacientes' },
            { route: 'especialidades', icon: 'stethoscope', label: 'Especialidades' },
            { route: 'servicos', icon: 'briefcase', label: 'Servicos' }
        ],
        medico: [
            { route: 'dashboard', icon: 'layout-dashboard', label: 'Dashboard' },
            { route: 'consultas', icon: 'calendar', label: 'Consultas' }
        ],
        admin: [
            { route: 'dashboard', icon: 'layout-dashboard', label: 'Dashboard' },
            { route: 'consultas', icon: 'calendar', label: 'Consultas' },
            { route: 'clientes', icon: 'building-2', label: 'Clientes' },
            { route: 'pacientes', icon: 'users', label: 'Pacientes' },
            { route: 'especialidades', icon: 'stethoscope', label: 'Especialidades' },
            { route: 'servicos', icon: 'briefcase', label: 'Servicos' },
            { route: 'funcionarios', icon: 'user-cog', label: 'Funcionarios' },
            { route: 'pagamentos', icon: 'credit-card', label: 'Pagamentos' },
            { route: 'configuracoes', icon: 'settings', label: 'Configuracoes' }
        ]
    };

    const menuItems = menuByRole[role] || menuByRole.admin;
    const navHtml = menuItems.map(item => `
        <a href="#${item.route}" class="sidebar-item ${currentRoute === item.route ? 'active' : ''}">
            <i data-lucide="${item.icon}"></i>
            <span>${item.label}</span>
        </a>
    `).join('');

    return `
        <div class="sidebar">
            <div class="sidebar-logo">
                <i data-lucide="calendar"></i>
                <span>Kigramed</span>
            </div>

            <nav>
                ${navHtml}
            </nav>

            <div style="position: absolute; bottom: 20px; left: 20px; right: 20px; padding-top: 20px; border-top: 1px solid rgba(255,255,255,0.1);">
                <div style="display: flex; align-items: center; gap: 12px; padding: 12px; background: rgba(255,255,255,0.05); border-radius: 8px;">
                    <div style="width: 40px; height: 40px; background: var(--primary); border-radius: 8px; display: flex; align-items: center; justify-content: center;">
                        <i data-lucide="user" style="color: white;"></i>
                    </div>
                    <div style="flex: 1; overflow: hidden;">
                        <div style="font-weight: 500; font-size: 14px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">${user?.nome || 'Usuario'}</div>
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
    toast.info('Sessao encerrada');
    router.navigate('login');
}

// Exportar
window.renderSidebar = renderSidebar;
