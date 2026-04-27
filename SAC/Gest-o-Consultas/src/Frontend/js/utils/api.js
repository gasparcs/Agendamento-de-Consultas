/**
 * Kigramed Frontend - API Client
 */

const API_BASE_URL = 'http://localhost:5284/api';

function getToken() {
    const creds = authManager.getCredentials();
    return creds?.token || getCookie('token');
}

function getCurrentUserRole() {
    const roleFromStore = appStore.get('user')?.role;
    const roleFromSession = authManager.getCredentials()?.user?.role;
    return String(roleFromStore || roleFromSession || '').toLowerCase();
}

function isSecretariaUser() {
    return getCurrentUserRole() === 'secretaria';
}

const api = {
    baseURL: API_BASE_URL,
    
    getHeaders() {
        const headers = { 'Content-Type': 'application/json' };
        const token = getToken();
        if (token) headers['Authorization'] = `Bearer ${token}`;
        return headers;
    },
    
    async request(endpoint, options = {}) {
        const url = `${this.baseURL}${endpoint}`;
        const config = { ...options, headers: { ...this.getHeaders(), ...options.headers } };
        
        try {
            const response = await fetch(url, config);
            const contentType = response.headers.get('content-type');
            const isJson = contentType?.includes('application/json');
            let data;
            try { data = isJson ? await response.json() : await response.text(); } catch (e) { data = null; }
            
            if (response.status === 401) {
                authManager.clearCredentials();
                appStore.set({ token: null, user: null, isAuthenticated: false });
                router.navigate('login');
                throw { status: 401, message: 'Sessão expirada. Faça login novamente.' };
            }
            
            if (!response.ok) throw { status: response.status, message: data?.message || data || `Erro ${response.status}` };
            
            return data;
        } catch (error) {
            if (error instanceof TypeError && error.message.includes('fetch')) {
                throw { status: 0, message: 'API indisponível. Verifique se o servidor está rodando.' };
            }
            throw error;
        }
    },
    
    get(endpoint) { return this.request(endpoint, { method: 'GET' }); },
    post(endpoint, data) { return this.request(endpoint, { method: 'POST', body: JSON.stringify(data) }); },
    put(endpoint, data) { return this.request(endpoint, { method: 'PUT', body: JSON.stringify(data) }); },
    delete(endpoint) { return this.request(endpoint, { method: 'DELETE' }); }
};

const endpoints = {
    // Auth
    login: (data) => api.post('/auth/login', data),

    // ===== ADMIN =====
    getClientes: () => api.get('/admin/cliente'),
    createCliente: (data) => api.post('/admin/cliente', data),
    updateCliente: (nif, data) => api.put(`/admin/cliente/${nif}`, data),
    deleteCliente: (nif) => api.delete(`/admin/cliente/${nif}`),
    getClienteByNif: (nif) => api.get(`/admin/cliente/nif/${nif}`),
    getClienteByTexto: (texto) => api.get(`/admin/cliente/texto/${texto}`),

    getConsultas: () => api.get('/admin/consulta'),
    createConsulta: (data) => api.post('/admin/consulta', data),
    updateConsulta: (id, data) => api.put(`/admin/consulta/${id}`, data),

    getEspecialidades: () => api.get('/admin/especialidade'),
    createEspecialidade: (data) => api.post('/admin/especialidade', data),
    updateEspecialidade: (id, data) => api.put(`/admin/especialidade/${id}`, data),
    deleteEspecialidade: (id) => api.delete(`/admin/especialidade/${id}`),
    getEspecialidadeById: (id) => api.get(`/admin/especialidade/id/${id}`),
    getEspecialidadeByTexto: (texto) => api.get(`/admin/especialidade/texto/${texto}`),

    getFuncionarios: () => api.get('/admin/funcionario'),
    createFuncionario: (data) => api.post('/admin/funcionario', data),
    updateFuncionario: (nif, data) => api.put(`/admin/funcionario/${nif}`, data),
    deleteFuncionario: (nif) => api.delete(`/admin/funcionario/${nif}`),
    getFuncionarioByNif: (nif) => api.get(`/admin/funcionario/nif/${nif}`),
    getFuncionarioByTexto: (texto) => api.get(`/admin/funcionario/texto/${texto}`),

    getPacientes: () => api.get('/admin/paciente'),
    createPaciente: (data) => api.post('/admin/paciente', data),
    updatePaciente: (id, data) => api.put(`/admin/paciente/${id}`, data),
    deletePaciente: (id) => api.delete(`/admin/paciente/${id}`),
    getPacienteById: (id) => api.get(`/admin/paciente/id/${id}`),
    getPacienteByTexto: (texto) => api.get(`/admin/paciente/texto/${texto}`),

    getPerfis: () => api.get('/admin/perfil'),

    getServicos: () => api.get('/admin/servico'),
    createServico: (data) => api.post('/admin/servico', data),
    updateServico: (id, data) => api.put(`/admin/servico/${id}`, data),
    deleteServico: (id) => api.delete(`/admin/servico/${id}`),
    getServicoById: (id) => api.get(`/admin/servico/id/${id}`),
    getServicoByTexto: (texto) => api.get(`/admin/servico/texto/${texto}`),

    // ===== SECRETARIA =====
    getClientesSecretaria: () => api.get('/secretaria/cliente'),
    createClienteSecretaria: (data) => api.post('/secretaria/cliente', data),
    updateClienteSecretaria: (nif, data) => api.put(`/secretaria/cliente/${nif}`, data),
    deleteClienteSecretaria: (nif) => api.delete(`/secretaria/cliente/${nif}`),

    getConsultasSecretaria: () => api.get('/secretaria/consulta'),
    createConsultaSecretaria: (data) => api.post('/secretaria/consulta', data),

    getPacientesSecretaria: () => api.get('/secretaria/paciente'),
    createPacienteSecretaria: (data) => api.post('/secretaria/paciente', data),
    updatePacienteSecretaria: (id, data) => api.put(`/secretaria/paciente/${id}`, data),
    deletePacienteSecretaria: (id) => api.delete(`/secretaria/paciente/${id}`),

    getServicosSecretaria: () => api.get('/secretaria/servico'),
    getEspecialidadesSecretaria: () => api.get('/secretaria/especialidade'),

    // ===== ROLE-AWARE HELPERS =====
    getClientesByRole: () => isSecretariaUser() ? endpoints.getClientesSecretaria() : endpoints.getClientes(),
    createClienteByRole: (data) => isSecretariaUser() ? endpoints.createClienteSecretaria(data) : endpoints.createCliente(data),
    updateClienteByRole: (nif, data) => isSecretariaUser() ? endpoints.updateClienteSecretaria(nif, data) : endpoints.updateCliente(nif, data),
    deleteClienteByRole: (nif) => isSecretariaUser() ? endpoints.deleteClienteSecretaria(nif) : endpoints.deleteCliente(nif),

    getConsultasByRole: () => isSecretariaUser() ? endpoints.getConsultasSecretaria() : endpoints.getConsultas(),
    createConsultaByRole: (data) => isSecretariaUser() ? endpoints.createConsultaSecretaria(data) : endpoints.createConsulta(data),

    getPacientesByRole: () => isSecretariaUser() ? endpoints.getPacientesSecretaria() : endpoints.getPacientes(),
    createPacienteByRole: (data) => isSecretariaUser() ? endpoints.createPacienteSecretaria(data) : endpoints.createPaciente(data),
    updatePacienteByRole: (id, data) => isSecretariaUser() ? endpoints.updatePacienteSecretaria(id, data) : endpoints.updatePaciente(id, data),
    deletePacienteByRole: (id) => isSecretariaUser() ? endpoints.deletePacienteSecretaria(id) : endpoints.deletePaciente(id),

    getServicosByRole: () => isSecretariaUser() ? endpoints.getServicosSecretaria() : endpoints.getServicos(),
    getEspecialidadesByRole: () => isSecretariaUser() ? endpoints.getEspecialidadesSecretaria() : endpoints.getEspecialidades(),

    // ===== MEDICO =====
    getConsultasMedico: () => api.get('/medico/consultas'),
    updateConsultaMedico: (id, data) => api.put(`/medico/consulta/${id}`, data),

    // ===== DROPDOWNS (Para formulários) =====
    getEstadosConsulta: () => api.get('/admin/dropdown/estados-consulta'),
    getMedicosEspecialidades: () => api.get('/admin/dropdown/medicos-especialidades'),
};

window.api = api;
window.endpoints = endpoints;
