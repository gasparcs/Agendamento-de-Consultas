/**
 * Kigramed Frontend - Cookie Manager
 * Wrapper sobre document.cookie (como js-cookie)
 */

const cookie = {
    /**
     * Obter valor de um cookie
     * @param {string} name - Nome do cookie
     * @returns {string|null}
     */
    get(name) {
        const nameEQ = name + '=';
        const cookies = document.cookie.split(';');
        for (let i = 0; i < cookies.length; i++) {
            let c = cookies[i].trim();
            if (c.indexOf(nameEQ) === 0) {
                return c.substring(nameEQ.length);
            }
        }
        return null;
    },
    
    /**
     * Definir um cookie
     * @param {string} name - Nome do cookie
     * @param {string} value - Valor do cookie
     * @param {number} days - Dias até expirar
     */
    set(name, value, days = 7) {
        const expires = days ? '; expires=' + new Date(Date.now() + days * 24 * 60 * 60 * 1000).toUTCString() : '';
        document.cookie = name + '=' + (value || '') + expires + '; path=/; SameSite=Lax';
    },
    
    /**
     * Remover um cookie
     * @param {string} name - Nome do cookie
     */
    remove(name) {
        document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
    }
};

/**
 * Gerenciador de Autenticação com Persistência
 */
const authManager = {
    /**
     * Salvar credenciais de forma segura
     */
    saveCredentials(token, user, expiresIn = 7200) {
        const expiresAt = Date.now() + (expiresIn * 1000);
        cookie.set('token', token, 7);
        cookie.set('user', JSON.stringify(user), 7);
        cookie.set('token_expires_at', expiresAt.toString(), 7);
        
        // Também salvar em localStorage para restaurar na próxima sessão
        localStorage.setItem('kigramed_token', token);
        localStorage.setItem('kigramed_user', JSON.stringify(user));
        localStorage.setItem('kigramed_token_expires_at', expiresAt.toString());
    },
    
    /**
     * Recuperar credenciais salvas
     */
    getCredentials() {
        const token = cookie.get('token') || localStorage.getItem('kigramed_token');
        const userStr = cookie.get('user') || localStorage.getItem('kigramed_user');
        const expiresAtStr = cookie.get('token_expires_at') || localStorage.getItem('kigramed_token_expires_at');
        
        if (!token || !userStr) {
            return null;
        }
        
        try {
            return {
                token,
                user: JSON.parse(userStr),
                expiresAt: parseInt(expiresAtStr || '0')
            };
        } catch (e) {
            return null;
        }
    },
    
    /**
     * Limpar credenciais (logout)
     */
    clearCredentials() {
        cookie.remove('token');
        cookie.remove('user');
        cookie.remove('token_expires_at');
        localStorage.removeItem('kigramed_token');
        localStorage.removeItem('kigramed_user');
        localStorage.removeItem('kigramed_token_expires_at');
    },
    
    /**
     * Verificar se token é válido
     */
    isTokenValid() {
        const creds = this.getCredentials();
        if (!creds) return false;
        
        // Verificar se expirou (com margem de 1 minuto)
        const now = Date.now();
        const isExpired = creds.expiresAt <= (now + 60000);
        
        return !isExpired;
    },
    
    /**
     * Verificar se token expirou
     */
    isTokenExpired() {
        return !this.isTokenValid();
    }
};

// Alias para compatibilidade
function getCookie(name) { return cookie.get(name); }
function setCookie(name, value, days) { return cookie.set(name, value, days); }
function removeCookie(name) { return cookie.remove(name); }

// Exportar
window.cookie = cookie;
window.authManager = authManager;
window.getCookie = getCookie;
window.setCookie = setCookie;
window.removeCookie = removeCookie;