/**
 * Kigramed Frontend - API Client
 * Wrapper sobre fetch() com headers JWT (como Axios)
 */

const API_BASE_URL = 'http://localhost:5000/api';

// Obter token dos cookies
function getToken() {
    return getCookie('token');
}

// Configuração padrão
const api = {
    baseURL: API_BASE_URL,
    
    // Headers padrão
    getHeaders() {
        const headers = {
            'Content-Type': 'application/json'
        };
        const token = getToken();
        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        return headers;
    },
    
    // Request genérico
    async request(endpoint, options = {}) {
        const url = `${this.baseURL}${endpoint}`;
        const config = {
            ...options,
            headers: {
                ...this.getHeaders(),
                ...options.headers
            }
        };
        
        try {
            const response = await fetch(url, config);
            const data = await response.json().catch(() => null);
            
            if (!response.ok) {
                throw { status: response.status, message: data?.message || 'Erro na requisição' };
            }
            
            return data;
        } catch (error) {
            console.error('API Error:', error);
            throw error;
        }
    },
    
    // GET
    get(endpoint) {
        return this.request(endpoint, { method: 'GET' });
    },
    
    // POST
    post(endpoint, data) {
        return this.request(endpoint, {
            method: 'POST',
            body: JSON.stringify(data)
        });
    },
    
    // PUT
    put(endpoint, data) {
        return this.request(endpoint, {
            method: 'PUT',
            body: JSON.stringify(data)
        });
    },
    
    // DELETE
    delete(endpoint) {
        return this.request(endpoint, { method: 'DELETE' });
    }
};

// Endpoints da API
const endpoints = {
    // Auth
    login: (data) => api.post('/auth/login', data),
    
    // Clientes
    getClientes: () => api.get('/clientes'),
    getClienteById: (id) => api.get(`/clientes/${id}`),
    getClienteByNif: (nif) => api.get(`/clientes/nif/${nif}`),
    createCliente: (data) => api.post('/clientes', data),
    updateCliente: (id, data) => api.put(`/clientes/${id}`, data),
    deleteCliente: (id) => api.delete(`/clientes/${id}`),
    
    // Consultas
    getConsultas: () => api.get('/consultas'),
    getConsultaById: (id) => api.get(`/consultas/${id}`),
    createConsulta: (data) => api.post('/consultas', data),
    updateConsulta: (id, data) => api.put(`/consultas/${id}`, data),
    deleteConsulta: (id) => api.delete(`/consultas/${id}`),
    
    // Pacientes
    getPacientes: () => api.get('/pacientes'),
    getPacienteById: (id) => api.get(`/pacientes/${id}`),
    createPaciente: (data) => api.post('/pacientes', data),
    updatePaciente: (id, data) => api.put(`/pacientes/${id}`, data),
    deletePaciente: (id) => api.delete(`/pacientes/${id}`),
    
    // Especialidades
    getEspecialidades: () => api.get('/especialidades'),
    getEspecialidadeById: (id) => api.get(`/especialidades/${id}`),
    createEspecialidade: (data) => api.post('/especialidades', data),
    updateEspecialidade: (id, data) => api.put(`/especialidades/${id}`, data),
    deleteEspecialidade: (id) => api.delete(`/especialidades/${id}`),
    
    // Serviços
    getServicos: () => api.get('/servicos'),
    getServicoById: (id) => api.get(`/servicos/${id}`),
    createServico: (data) => api.post('/servicos', data),
    updateServico: (id, data) => api.put(`/servicos/${id}`, data),
    deleteServico: (id) => api.delete(`/servicos/${id}`),
    
    // Funcionários
    getFuncionarios: () => api.get('/funcionarios'),
    getFuncionarioById: (id) => api.get(`/funcionarios/${id}`),
    getFuncionarioByNif: (nif) => api.get(`/funcionarios/nif/${nif}`),
    createFuncionario: (data) => api.post('/funcionarios', data),
    updateFuncionario: (id, data) => api.put(`/funcionarios/${id}`, data),
    deleteFuncionario: (id) => api.delete(`/funcionarios/${id}`),
    
    // Pagamentos
    getPagamentos: () => api.get('/pagamentos'),
    getPagamentoById: (id) => api.get(`/pagamentos/${id}`),
    createPagamento: (data) => api.post('/pagamentos', data),
    deletePagamento: (id) => api.delete(`/pagamentos/${id}`),
    
    // Perfis
    getPerfis: () => api.get('/perfis')
};

// Exportar
window.api = api;
window.endpoints = endpoints;