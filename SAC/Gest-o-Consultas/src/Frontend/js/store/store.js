/**
 * Kigramed Frontend - Store (Padrão Observer como Zustand)
 * Implementa get/set/subscribe similar ao Zustand
 */

// Estado global da aplicação
const state = {};

// Callbacks de subscribers
const subscribers = new Set();

// Criador de store
function createStore(initialState) {
    // Inicializa o estado
    Object.assign(state, initialState);
    
    return {
        // Obter valor do estado
        get: (key) => state[key],
        
        // Obter todo o estado
        getState: () => ({ ...state }),
        
        // Atualizar estado
        set: (partial) => {
            const prevState = { ...state };
            const nextState = typeof partial === 'function' ? partial(prevState) : partial;
            
            Object.assign(state, nextState);
            
            // Notificar subscribers
            subscribers.forEach(callback => callback(state, prevState));
        },
        
        // Inscrever-se em mudanças
        subscribe: (callback) => {
            subscribers.add(callback);
            return () => subscribers.delete(callback);
        }
    };
}

// Store principal da aplicação
const appStore = createStore({
    // Autenticação
    user: null,
    token: null,
    isAuthenticated: false,
    
    // Dados
    clientes: [],
    consultas: [],
    pacientes: [],
    especialidades: [],
    servicos: [],
    funcionarios: [],
    pagamentos: [],
    
    // UI
    loading: false,
    currentPage: 'dashboard',
    sidebarOpen: true,
    
    // Métricas
    stats: {
        totalConsultas: 0,
        consultasHoje: 0,
        totalPacientes: 0,
        totalFaturado: 0
    }
});

// Exportar store
window.appStore = appStore;