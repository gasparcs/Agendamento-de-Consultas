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

// Alias para compatibilidade
function getCookie(name) { return cookie.get(name); }
function setCookie(name, value, days) { return cookie.set(name, value, days); }
function removeCookie(name) { return cookie.remove(name); }

// Exportar
window.cookie = cookie;
window.getCookie = getCookie;
window.setCookie = setCookie;
window.removeCookie = removeCookie;