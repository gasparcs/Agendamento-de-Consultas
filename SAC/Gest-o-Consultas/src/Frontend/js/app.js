/**
 * Kigramed Frontend - Main Application Entry Point
 */

// Inicializar ícones Lucide
document.addEventListener('DOMContentLoaded', () => {
    if (window.lucide) {
        lucide.createIcons();
    }
});

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
    appStore.set({ currentPage: path });
});

// Iniciar router
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