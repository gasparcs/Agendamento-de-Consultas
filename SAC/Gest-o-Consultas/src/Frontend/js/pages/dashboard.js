/**
 * Kigramed Frontend - Dashboard Page
 */

function renderDashboard() {
    const app = document.getElementById('app');
    const user = appStore.get('user');
    
    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Dashboard</h1>
                    <p style="color: var(--gray-500);">Bem-vindo, ${user?.nome || 'Usuário'}</p>
                </div>
                <div style="display: flex; gap: 12px;">
                    <button class="btn btn-secondary" onclick="router.navigate('consultas')">
                        <i data-lucide="calendar"></i>
                        Nova Consulta
                    </button>
                </div>
            </div>
            
            <!-- Stats Cards -->
            <div class="grid grid-cols-4" style="margin-bottom: 24px;">
                <div class="stat-card animate-fade-in">
                    <div style="display: flex; align-items: center; gap: 16px;">
                        <div class="stat-icon" style="background: #dbeafe;">
                            <i data-lucide="calendar" style="color: #1e40af;"></i>
                        </div>
                        <div>
                            <div class="stat-value" id="stat-consultas">0</div>
                            <div class="stat-label">Consultas Hoje</div>
                        </div>
                    </div>
                </div>
                
                <div class="stat-card animate-fade-in" style="animation-delay: 0.1s;">
                    <div style="display: flex; align-items: center; gap: 16px;">
                        <div class="stat-icon" style="background: #d1fae5;">
                            <i data-lucide="users" style="color: #065f46;"></i>
                        </div>
                        <div>
                            <div class="stat-value" id="stat-pacientes">0</div>
                            <div class="stat-label">Pacientes</div>
                        </div>
                    </div>
                </div>
                
                <div class="stat-card animate-fade-in" style="animation-delay: 0.2s;">
                    <div style="display: flex; align-items: center; gap: 16px;">
                        <div class="stat-icon" style="background: #fef3c7;">
                            <i data-lucide="dollar-sign" style="color: #92400e;"></i>
                        </div>
                        <div>
                            <div class="stat-value" id="stat-faturado">0</div>
                            <div class="stat-label">Faturado (AOA)</div>
                        </div>
                    </div>
                </div>
                
                <div class="stat-card animate-fade-in" style="animation-delay: 0.3s;">
                    <div style="display: flex; align-items: center; gap: 16px;">
                        <div class="stat-icon" style="background: #fce7f3;">
                            <i data-lucide="activity" style="color: #9d174d;"></i>
                        </div>
                        <div>
                            <div class="stat-value" id="stat-taxa">0%</div>
                            <div class="stat-label">Taxa de Ocupação</div>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Charts Row -->
            <div class="grid grid-cols-2" style="margin-bottom: 24px;">
                <div class="card animate-fade-in" style="animation-delay: 0.4s;">
                    <div class="card-header">
                        <i data-lucide="bar-chart-3"></i>
                        Consultas por Mês
                    </div>
                    <canvas id="consultasChart" height="200"></canvas>
                </div>
                
                <div class="card animate-fade-in" style="animation-delay: 0.5s;">
                    <div class="card-header">
                        <i data-lucide="pie-chart"></i>
                        Distribuição por Especialidade
                    </div>
                    <canvas id="especialidadesChart" height="200"></canvas>
                </div>
            </div>
            
            <!-- Recent Activity -->
            <div class="card animate-fade-in" style="animation-delay: 0.6s;">
                <div class="card-header">
                    <i data-lucide="clock"></i>
                    Próximas Consultas
                </div>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Paciente</th>
                            <th>Especialidade</th>
                            <th>Médico</th>
                            <th>Data/Hora</th>
                            <th>Estado</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody id="proximas-consultas">
                        <tr>
                            <td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                                <div class="loading"><div class="spinner"></div></div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    `;
    
    // Inicializar ícones
    if (window.lucide) {
        lucide.createIcons();
    }
    
    // Carregar dados
    loadDashboardData();
}

/**
 * Carregar dados do dashboard
 */
async function loadDashboardData() {
    try {
        appStore.set({ loading: true });
        
        // Carregar consultas
        const consultas = await endpoints.getConsultasByRole();
        appStore.set({ consultas });
        
        // Carregar pacientes
        const pacientes = await endpoints.getPacientesByRole();
        appStore.set({ pacientes, loading: false });
        
        // Atualizar estatísticas
        updateStats(consultas, pacientes);
        
        // Renderizar gráfico de consultas
        renderConsultasChart(consultas);
        
        // Renderizar gráfico de especialidades
        renderEspecialidadesChart();
        
        // Renderizar próximas consultas
        renderProximasConsultas(consultas);
        
    } catch (error) {
        appStore.set({ loading: false });
        
        console.error('Erro ao carregar dados do dashboard:', error);
        
        if (error?.status === 0) {
            toast.warning('⚠ API indisponível. Mostrando dados de demonstração.');
        }
        
        // Dados de demo
        const demoConsultas = generateDemoConsultas();
        const demoPacientes = generateDemoPacientes();
        
        appStore.set({ consultas: demoConsultas, pacientes: demoPacientes });
        
        updateStats(demoConsultas, demoPacientes);
        renderConsultasChart(demoConsultas);
        renderEspecialidadesChart();
        renderProximasConsultas(demoConsultas);
    }
}

/**
 * Atualizar estatísticas
 */
function updateStats(consultas, pacientes) {
    const hoje = new Date().toDateString();
    const consultasHoje = consultas.filter(c => new Date(c.data).toDateString() === hoje).length;
    const totalFaturado = consultas.reduce((sum, c) => sum + (c.valor || 0), 0);
    
    document.getElementById('stat-consultas').textContent = consultasHoje;
    document.getElementById('stat-pacientes').textContent = pacientes.length;
    document.getElementById('stat-faturado').textContent = formatCurrency(totalFaturado);
    document.getElementById('stat-taxa').textContent = Math.min(95, Math.floor(Math.random() * 30 + 65)) + '%';
}

/**
 * Renderizar gráfico de consultas por mês
 */
function renderConsultasChart(consultas) {
    const ctx = document.getElementById('consultasChart');
    if (!ctx) return;
    
    // Agrupar por mês
    const meses = ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'];
    const data = new Array(12).fill(0);
    
    consultas.forEach(c => {
        const mes = new Date(c.data).getMonth();
        data[mes]++;
    });
    
    // Se não houver dados, usar demo
    if (consultas.length === 0) {
        data.forEach((_, i) => data[i] = Math.floor(Math.random() * 50) + 10);
    }
    
    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: meses,
            datasets: [{
                label: 'Consultas',
                data: data,
                backgroundColor: '#0ea5e9',
                borderRadius: 6
            }]
        },
        options: {
            responsive: true,
            plugins: { legend: { display: false } },
            scales: {
                y: { beginAtZero: true, ticks: { stepSize: 10 } }
            }
        }
    });
}

/**
 * Renderizar gráfico de especialidades
 */
function renderEspecialidadesChart() {
    const ctx = document.getElementById('especialidadesChart');
    if (!ctx) return;
    
    const labels = ['Cardiologia', 'Dermatologia', 'Pediatria', 'Ortopedia', 'Neurologia', 'Outros'];
    const data = [25, 18, 22, 15, 12, 8];
    
    new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                data: data,
                backgroundColor: ['#0ea5e9', '#8b5cf6', '#10b981', '#f59e0b', '#ef4444', '#64748b']
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { position: 'right' }
            }
        }
    });
}

/**
 * Renderizar próximas consultas
 */
function renderProximasConsultas(consultas) {
    const tbody = document.getElementById('proximas-consultas');
    if (!tbody) return;
    
    // Filtrar consultas futuras
    const proximas = consultas
        .filter(c => new Date(c.data) >= new Date())
        .sort((a, b) => new Date(a.data) - new Date(b.data))
        .slice(0, 5);
    
    if (proximas.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-500);">
                    Nenhuma consulta agendada
                </td>
            </tr>
        `;
        return;
    }
    
    tbody.innerHTML = proximas.map(c => `
        <tr>
            <td>
                <div style="font-weight: 500;">${c.paciente?.nome || 'Paciente'}</div>
            </td>
            <td>${c.especialidade || 'Geral'}</td>
            <td>${c.medico || 'Dr. Silva'}</td>
            <td>${formatDate(c.data)}</td>
            <td><span class="badge badge-success">Agendada</span></td>
            <td>
                <button class="btn btn-secondary" style="padding: 6px 12px;" onclick="alert('Funcionalidade em desenvolvimento')">
                    <i data-lucide="eye"></i>
                </button>
            </td>
        </tr>
    `).join('');
    
    if (window.lucide) {
        lucide.createIcons({ node: tbody });
    }
}

// Funções auxiliares
function formatCurrency(value) {
    return new Intl.NumberFormat('pt-AO', { style: 'currency', currency: 'AOA' }).format(value || 0);
}

function formatDate(date) {
    return new Date(date).toLocaleDateString('pt-PT', { 
        day: '2-digit', 
        month: '2-digit', 
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });
}

function generateDemoConsultas() {
    const especialidades = ['Cardiologia', 'Dermatologia', 'Pediatria', 'Ortopedia', 'Neurologia'];
    const medicos = ['Dr. Silva', 'Dra. Santos', 'Dr. Pereira', 'Dra. Costa', 'Dr. Ferreira'];
    const pacientes = ['João Manuel', 'Maria José', 'António Silva', 'Ana Paula', 'Pedro Costa'];
    
    const consultas = [];
    const hoje = new Date();
    
    for (let i = 0; i < 20; i++) {
        const data = new Date(hoje);
        data.setDate(data.getDate() + Math.floor(Math.random() * 30) - 5);
        
        consultas.push({
            id: i + 1,
            data: data.toISOString(),
            especialidade: especialidades[Math.floor(Math.random() * especialidades.length)],
            medico: medicos[Math.floor(Math.random() * medicos.length)],
            paciente: { nome: pacientes[Math.floor(Math.random() * pacientes.length)] },
            valor: Math.floor(Math.random() * 5000) + 1000,
            estado: 'Agendada'
        });
    }
    
    return consultas;
}

function generateDemoPacientes() {
    return [
        { id: 1, nome: 'João Manuel', nif: '123456789' },
        { id: 2, nome: 'Maria José', nif: '234567890' },
        { id: 3, nome: 'António Silva', nif: '345678901' },
        { id: 4, nome: 'Ana Paula', nif: '456789012' },
        { id: 5, nome: 'Pedro Costa', nif: '567890123' }
    ];
}

// Exportar
window.renderDashboard = renderDashboard;
