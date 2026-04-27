/**
 * Kigramed Frontend - Main Application Entry Point
 */

document.addEventListener('DOMContentLoaded', () => {
    if (window.lucide) {
        lucide.createIcons();
    }
});

function initializeAuth() {
    const creds = authManager.getCredentials();

    if (creds && authManager.isTokenValid()) {
        appStore.set({
            token: creds.token,
            user: creds.user,
            isAuthenticated: true
        });
        console.log('Credenciais restauradas da sessao anterior');
    } else if (creds) {
        console.warn('Token expirado');
        authManager.clearCredentials();
        appStore.set({
            token: null,
            user: null,
            isAuthenticated: false
        });
    }
}

const protectedRoutes = [
    'dashboard', 'clientes', 'pacientes', 'consultas',
    'especialidades', 'servicos', 'funcionarios', 'pagamentos',
    'configuracoes'
];

const allowedRoutesByRole = {
    admin: new Set(protectedRoutes),
    secretaria: new Set(['dashboard', 'clientes', 'pacientes', 'consultas', 'especialidades', 'servicos']),
    medico: new Set(['dashboard', 'consultas'])
};

function getCurrentRole() {
    const roleFromStore = appStore.get('user')?.role;
    const roleFromCreds = authManager.getCredentials()?.user?.role;
    return String(roleFromStore || roleFromCreds || 'admin').toLowerCase();
}

function canAccessRoute(path) {
    const role = getCurrentRole();
    const allowed = allowedRoutesByRole[role] || allowedRoutesByRole.admin;
    return allowed.has(path);
}

router.addRoute('login', renderLoginPage);
router.addRoute('dashboard', renderDashboard);
router.addRoute('clientes', renderClientes);
router.addRoute('pacientes', renderPacientes);
router.addRoute('consultas', renderConsultas);
router.addRoute('especialidades', renderEspecialidades);
router.addRoute('servicos', renderServicos);
router.addRoute('funcionarios', renderFuncionarios);
router.addRoute('pagamentos', renderPagamentos);
router.addRoute('configuracoes', renderConfiguracoes);

router.onChange((path) => {
    if (protectedRoutes.includes(path) && !appStore.get('isAuthenticated')) {
        console.warn('Rota protegida. Redirecionando para login...');
        router.navigate('login');
        return;
    }

    if (protectedRoutes.includes(path) && !canAccessRoute(path)) {
        toast.warning('Voce nao tem permissao para acessar esta pagina');
        router.navigate('dashboard');
        return;
    }

    appStore.set({ currentPage: path });
});

initializeAuth();
router.start();

function openModal(name) {
    const modal = document.getElementById(`modal-${name}`);
    if (modal) {
        modal.classList.add('active');
    }
}

function closeModal(name) {
    const modal = document.getElementById(`modal-${name}`);
    if (modal) {
        modal.classList.remove('active');
        const form = modal.querySelector('form');
        if (form) form.reset();
    }
}

document.addEventListener('click', (e) => {
    if (e.target.classList.contains('modal-overlay')) {
        e.target.classList.remove('active');
    }
});

window.openModal = openModal;
window.closeModal = closeModal;
