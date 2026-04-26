/**
 * Kigramed Frontend - API Client
 * Wrapper sobre fetch() com headers JWT (como Axios)
 */

const API_BASE_URL = 'http://localhost:5000/api';

// Obter token dos cookies
function getToken() {
    const creds = authManager.getCredentials();
    return creds?.token || getCookie('token');
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
            const contentType = response.headers.get('content-type');
            const isJson = contentType?.includes('application/json');
            
            let data;
            try {
                data = isJson ? await response.json() : await response.text();
            } catch (e) {
                data = null;
            }
            
            // Tratar erro 401 (Unauthorized)
            if (response.status === 401) {
                authManager.clearCredentials();
                appStore.set({
                    token: null,
                    user: null,
                    isAuthenticated: false
                });
                router.navigate('login');
                throw {
                    status: 401,
                    message: 'Sessão expirada. Faça login novamente.'
                };
            }
            
            if (!response.ok) {
                throw {
                    status: response.status,
                    message: data?.message || data || `Erro ${response.status}`
                };
            }
            
            return data;
        } catch (error) {
            // Diferenciar entre erro de rede e erro da API
            if (error instanceof TypeError && error.message.includes('fetch')) {
                console.warn('⚠ API indisponível (erro de rede)');
                throw {
                    status: 0,
                    message: 'API indisponível. Verifique se o servidor está rodando em http://localhost:5000'
                };
            }
            
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

// Endpoints da API (use role-based paths)
const endpoints = {
    // Auth
    login: (data) => api.post('/auth/login', data),
    status: () => api.get('/auth/status'),
    
    // Secretaria endpoints (CRUD)
    // Clientes
    getClientes: () => api.get('/secretaria/clientes'),
    getClienteById: (id) => api.get(`/secretaria/clientes/${id}`),
    createCliente: (data) => api.post('/secretaria/clientes', data),
    updateCliente: (id, data) => api.put(`/secretaria/clientes/${id}`, data),
    deleteCliente: (id) => api.delete(`/secretaria/clientes/${id}`),
    
    // Consultas
    getConsultas: () => api.get('/secretaria/consultas'),
    getConsultaById: (id) => api.get(`/secretaria/consultas/${id}`),
    createConsulta: (data) => api.post('/secretaria/consultas', data),
    updateConsulta: (id, data) => api.put(`/secretaria/consultas/${id}`, data),
    deleteConsulta: (id) => api.delete(`/secretaria/consultas/${id}`),
    
    // Pacientes
    getPacientes: () => api.get('/secretaria/pacientes'),
    getPacienteById: (id) => api.get(`/secretaria/pacientes/${id}`),
    createPaciente: (data) => api.post('/secretaria/pacientes', data),
    updatePaciente: (id, data) => api.put(`/secretaria/pacientes/${id}`, data),
    deletePaciente: (id) => api.delete(`/secretaria/pacientes/${id}`),
    
    // Admin endpoints (full access)
    // Especialidades
    getEspecialidades: () => api.get('/admin/especialidades'),
    getEspecialidadeById: (id) => api.get(`/admin/especialidades/${id}`),
    createEspecialidade: (data) => api.post('/admin/especialidades', data),
    updateEspecialidade: (id, data) => api.put(`/admin/especialidades/${id}`, data),
    deleteEspecialidade: (id) => api.delete(`/admin/especialidades/${id}`),
    
    // Serviços
    getServicos: () => api.get('/admin/servicos'),
    getServicoById: (id) => api.get(`/admin/servicos/${id}`),
    createServico: (data) => api.post('/admin/servicos', data),
    updateServico: (id, data) => api.put(`/admin/servicos/${id}`, data),
    deleteServico: (id) => api.delete(`/admin/servicos/${id}`),
    
    // Funcionários
    getFuncionarios: () => api.get('/admin/funcionarios'),
    getFuncionarioById: (id) => api.get(`/admin/funcionarios/${id}`),
    createFuncionario: (data) => api.post('/admin/funcionarios', data),
    updateFuncionario: (id, data) => api.put(`/admin/funcionarios/${id}`, data),
    deleteFuncionario: (id) => api.delete(`/admin/funcionarios/${id}`),
    
    // Pagamentos
    getPagamentos: () => api.get('/secretaria/pagamentos'),
    getPagamentoById: (id) => api.get(`/secretaria/pagamentos/${id}`),
    createPagamento: (data) => api.post('/secretaria/pagamentos', data),
    deletePagamento: (id) => api.delete(`/secretaria/pagamentos/${id}`),
    
    // Perfis
    getPerfis: () => api.get('/admin/perfis')
};

// Exportar
window.api = api;
window.endpoints = endpoints;