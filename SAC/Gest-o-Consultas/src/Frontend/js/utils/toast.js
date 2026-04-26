/**
 * Kigramed Frontend - Toast Notifications
 * success/error/warning/info com animação CSS (como React Toastify)
 */

const toast = {
    /**
     * Mostrar toast de sucesso
     * @param {string} message - Mensagem
     * @param {number} duration - Duração em ms
     */
    success(message, duration = 3000) {
        this.show(message, 'success', duration);
    },
    
    /**
     * Mostrar toast de erro
     * @param {string} message - Mensagem
     * @param {number} duration - Duração em ms
     */
    error(message, duration = 4000) {
        this.show(message, 'error', duration);
    },
    
    /**
     * Mostrar toast de aviso
     * @param {string} message - Mensagem
     * @param {number} duration - Duração em ms
     */
    warning(message, duration = 3500) {
        this.show(message, 'warning', duration);
    },
    
    /**
     * Mostrar toast informativo
     * @param {string} message - Mensagem
     * @param {number} duration - Duração em ms
     */
    info(message, duration = 3000) {
        this.show(message, 'info', duration);
    },
    
    /**
     * Mostrar toast genérico
     * @param {string} message - Mensagem
     * @param {string} type - Tipo (success/error/warning/info)
     * @param {number} duration - Duração em ms
     */
    show(message, type = 'info', duration = 3000) {
        const container = document.getElementById('toast-container');
        if (!container) return;
        
        const toastEl = document.createElement('div');
        toastEl.className = `toast toast-${type}`;
        toastEl.innerHTML = `
            <i data-lucide="${type === 'success' ? 'check-circle' : type === 'error' ? 'x-circle' : type === 'warning' ? 'alert-triangle' : 'info'}"></i>
            <span>${message}</span>
        `;
        
        container.appendChild(toastEl);
        
        // Inicializar ícones
        if (window.lucide) {
            lucide.createIcons({ node: toastEl });
        }
        
        // Remover após duração
        setTimeout(() => {
            toastEl.style.animation = 'fadeIn 0.3s ease-out reverse';
            setTimeout(() => toastEl.remove(), 300);
        }, duration);
    }
};

// Exportar
window.toast = toast;