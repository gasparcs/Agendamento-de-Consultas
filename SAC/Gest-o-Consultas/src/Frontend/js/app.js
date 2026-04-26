/**
 * Kigramed Frontend - Main Application Entry Point
 */

// Inicializar ícones Lucide
document.addEventListener('DOMContentLoaded', () => {
    if (window.lucide) {
        lucide.createIcons();
    }
});

// Restaurar credenciais ao carregar
function initializeAuth() {
    const creds = authManager.getCredentials();
    
    if (creds && authManager.isTokenValid()) {
        // Token ainda é válido
        appStore.set({
            token: creds.token,
            user: creds.user,
            isAuthenticated: true
        });
        console.log('✓ Credenciais restauradas da sessão anterior');
    } else if (creds) {
        // Token expirou
        console.warn('⚠ Token expirado');
        authManager.clearCredentials();
        appStore.set({
            token: null,
            user: null,
            isAuthenticated: false
        });
    }
}

// Proteção de rotas (require autenticação)
const protectedRoutes = [
    'dashboard', 'clientes', 'pacientes', 'consultas', 
    'especialidades', 'servicos', 'funcionarios', 'pagamentos', 
    'relatorios', 'configuracoes'
];

// Configurar rotas
router.addRoute('login', renderLoginPage);
router.addRoute('dashboard', renderDashboard);
router.addRoute('clientes', renderClientes);
router.addRoute('pacientes', renderPacientes);
router.addRoute('consultas', renderConsultas);
router.addRoute('especialidades', renderEspecialidades);
router.addRoute('servicos', renderServicos);
router.addRoute('funcionarios', renderFuncionarios);
router.addRoute('pagamentos', renderPagamentos);
router.addRoute('relatorios', renderRelatorios);
router.addRoute('configuracoes', renderConfiguracoes);

// Callback de mudança de rota
router.onChange((path) => {
    // Verificar se rota protegida
    if (protectedRoutes.includes(path) && !appStore.get('isAuthenticated')) {
        console.warn('❌ Rota protegida. Redirecionando para login...');
        router.navigate('login');
        return;
    }
    
    appStore.set({ currentPage: path });
});

// Inicializar autenticação e depois iniciar router
initializeAuth();
router.start();

// Funções globais para modais
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
        // Limpar formulário
        const form = modal.querySelector('form');
        if (form) form.reset();
    }
}

// Fechar modal ao clicar fora
document.addEventListener('click', (e) => {
    if (e.target.classList.contains('modal-overlay')) {
        e.target.classList.remove('active');
    }
});

// Exportar funções globais
window.openModal = openModal;
window.closeModal = closeModal;