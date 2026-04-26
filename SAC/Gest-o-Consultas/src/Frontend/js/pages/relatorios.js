/**
 * Kigramed Frontend - Relatórios Page
 */

function renderRelatorios() {
    const app = document.getElementById('app');
    
    app.innerHTML = `
        ${renderSidebar()}
        <div class="main-content">
            <div class="header">
                <div>
                    <h1 class="header-title">Relatórios</h1>
                    <p style="color: var(--gray-500);">Análise e estatísticas</p>
                </div>
                <button class="btn btn-secondary" onclick="exportRelatorio()">
                    <i data-lucide="download"></i>
                    Exportar PDF
                </button>
            </div>
            
            <!-- Filtros -->
            <div class="card">
                <div style="display: flex; gap: 16px; flex-wrap: wrap; align-items: flex-end;">
                    <div>
                        <label class="form-label">Data Início</label>
                        <input type="date" id="relatorio-data-inicio" class="form-input" value="${getDataInicio()}">
                    </div>
                    <div>
                        <label class="form-label">Data Fim</label>
                        <input type="date" id="relatorio-data-fim" class="form-input" value="${new Date().toISOString().split('T')[0]}">
                    </div>
                    <button class="btn btn-primary" onclick="gerarRelatorio()">
                        <i data-lucide="bar-chart-2"></i>
                        Gerar
                    </button>
                </div>
            </div>
            
            <!-- Charts -->
            <div class="grid grid-cols-2" style="margin-bottom: 24px;">
                <div class="card">
                    <div class="card-header">Consultas por Mês</div>
                    <canvas id="relatorio-consultas" height="200"></canvas>
                </div>
                <div class="card">
                    <div class="card-header">Receitas por Mês</div>
                    <canvas id="relatorio-receitas" height="200"></canvas>
                </div>
            </div>
            
            <div class="grid grid-cols-2" style="margin-bottom: 24px;">
                <div class="card">
                    <div class="card-header">Top Especialidades</div>
                    <canvas id="relatorio-especialidades" height="200"></canvas>
                </div>
                <div class="card">
                    <div class="card-header">Pacientes por Género</div>
                    <canvas id="relatorio-genero" height="200"></canvas>
                </div>
            </div>
            
            <!-- Tabela Resumo -->
            <div class="card">
                <div class="card-header">Resumo Mensal</div>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Mês</th>
                            <th>Consultas</th>
                            <th>Pacientes</th>
                            <th>Receita</th>
                            <th>Média/Consulta</th>
                        </tr>
                    </thead>
                    <tbody id="resumo-table">
                        <tr><td colspan="5" style="text-align: center; padding: 20px;">Carregando...</td></tr>
                    </tbody>
                </table>
            </div>
        </div>
    `;
    
    if (window.lucide) lucide.createIcons();
    gerarRelatorio();
}

function getDataInicio() {
    const date = new Date();
    date.setMonth(date.getMonth() - 6);
    return date.toISOString().split('T')[0];
}

function gerarRelatorio() {
    // Chart de Consultas por Mês
    const ctx1 = document.getElementById('relatorio-consultas');
    if (ctx1) {
        new Chart(ctx1, {
            type: 'line',
            data: {
                labels: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun'],
                datasets: [{
                    label: 'Consultas',
                    data: [45, 52, 38, 60, 55, 48],
                    borderColor: '#0ea5e9',
                    backgroundColor: 'rgba(14, 165, 233, 0.1)',
                    fill: true,
                    tension: 0.4
                }]
            },
            options: { responsive: true, plugins: { legend: { display: false } } }
        });
    }
    
    // Chart de Receitas
    const ctx2 = document.getElementById('relatorio-receitas');
    if (ctx2) {
        new Chart(ctx2, {
            type: 'bar',
            data: {
                labels: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun'],
                datasets: [{
                    label: 'Receita (AOA)',
                    data: [125000, 156000, 98000, 180000, 165000, 144000],
                    backgroundColor: '#10b981',
                    borderRadius: 6
                }]
            },
            options: { responsive: true, plugins: { legend: { display: false } } }
        });
    }
    
    // Chart de Especialidades
    const ctx3 = document.getElementById('relatorio-especialidades');
    if (ctx3) {
        new Chart(ctx3, {
            type: 'bar',
            data: {
                labels: ['Cardiologia', 'Dermatologia', 'Pediatria', 'Ortopedia', 'Neurologia'],
                datasets: [{
                    label: 'Consultas',
                    data: [120, 85, 95, 70, 45],
                    backgroundColor: ['#0ea5e9', '#8b5cf6', '#10b981', '#f59e0b', '#ef4444'],
                    borderRadius: 6
                }]
            },
            options: { 
                indexAxis: 'y',
                responsive: true, 
                plugins: { legend: { display: false } } 
            }
        });
    }
    
    // Chart de Género
    const ctx4 = document.getElementById('relatorio-genero');
    if (ctx4) {
        new Chart(ctx4, {
            type: 'doughnut',
            data: {
                labels: ['Masculino', 'Feminino'],
                datasets: [{
                    data: [45, 55],
                    backgroundColor: ['#0ea5e9', '#ec4899']
                }]
            },
            options: { responsive: true, plugins: { legend: { position: 'bottom' } } }
        });
    }
    
    // Tabela Resumo
    const tbody = document.getElementById('resumo-table');
    if (tbody) {
        const meses = [
            { nome: 'Janeiro', consultas: 45, pacientes: 38, receita: 125000 },
            { nome: 'Fevereiro', consultas: 52, pacientes: 42, receita: 156000 },
            { nome: 'Março', consultas: 38, pacientes: 35, receita: 98000 },
            { nome: 'Abril', consultas: 60, pacientes: 48, receita: 180000 },
            { nome: 'Maio', consultas: 55, pacientes: 45, receita: 165000 },
            { nome: 'Junho', consultas: 48, pacientes: 40, receita: 144000 }
        ];
        
        tbody.innerHTML = meses.map(m => `
            <tr>
                <td><strong>${m.nome}</strong></td>
                <td>${m.consultas}</td>
                <td>${m.pacientes}</td>
                <td>${formatCurrency(m.receita)}</td>
                <td>${formatCurrency(m.receita / m.consultas)}</td>
            </tr>
        `).join('');
    }
}

function exportRelatorio() {
    toast.info('Funcionalidade em desenvolvimento');
}

window.renderRelatorios = renderRelatorios;