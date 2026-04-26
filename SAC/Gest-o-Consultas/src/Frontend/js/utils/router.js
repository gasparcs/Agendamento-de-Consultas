/**
 * Kigramed Frontend - Router
 * Router hash-based nativo (#dashboard, #clientes, etc.)
 */

const router = {
    // Rotas registradas
    routes: {},
    
    // Callback de mudança de rota
    onChangeCallback: null,
    
    /**
     * Registrar uma rota
     * @param {string} path - Caminho (ex: #dashboard)
     * @param {Function} handler - Função handler
     */
    addRoute(path, handler) {
        this.routes[path] = handler;
    },
    
    /**
     * Navegar para uma rota
     * @param {string} path - Caminho
     */
    navigate(path) {
        window.location.hash = path;
    },
    
    /**
     * Obter rota atual
     * @returns {string}
     */
    getCurrentRoute() {
        return window.location.hash.slice(1) || 'login';
    },
    
    /**
     * Iniciar o router
     */
    start() {
        // Listener para mudanças de hash
        window.addEventListener('hashchange', () => this.handleRoute());
        
        // Primeira carga
        this.handleRoute();
    },
    
    /**
     * Tratar mudança de rota
     */
    handleRoute() {
        const path = this.getCurrentRoute();
        const handler = this.routes[path];
        
        if (handler) {
            handler();
        } else {
            // Rota não encontrada - redirecionar para login
            this.navigate('login');
        }
        
        // Callback de mudança
        if (this.onChangeCallback) {
            this.onChangeCallback(path);
        }
    },
    
    /**
     * Registrar callback de mudança de rota
     * @param {Function} callback
     */
    onChange(callback) {
        this.onChangeCallback = callback;
    }
};

// Exportar
window.router = router;