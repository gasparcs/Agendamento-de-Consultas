/**
 * Kigramed Frontend - Schema Validator
 * Validação em runtime via Schema (como Zod)
 */

// Schema definitions
const schemas = {
    // NIF validation (9 dígitos para Angola)
    nif: () => ({
        validate: (value) => {
            if (!value) return { valid: false, error: 'NIF é obrigatório' };
            if (!/^\d{9}$/.test(value)) return { valid: false, error: 'NIF deve ter 9 dígitos' };
            return { valid: true, error: null };
        }
    }),
    
    // Email validation
    email: () => ({
        validate: (value) => {
            if (!value) return { valid: false, error: 'Email é obrigatório' };
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailRegex.test(value)) return { valid: false, error: 'Email inválido' };
            return { valid: true, error: null };
        }
    }),
    
    // Phone validation (Angola)
    phone: () => ({
        validate: (value) => {
            if (!value) return { valid: false, error: 'Telefone é obrigatório' };
            const phoneRegex = /^(9\d{8}|2\d{8})$/;
            if (!phoneRegex.test(value.replace(/\s/g, ''))) return { valid: false, error: 'Telefone inválido' };
            return { valid: true, error: null };
        }
    }),
    
    // String validation
    string: (options = {}) => ({
        validate: (value) => {
            const { required = true, min, max, message } = options;
            
            if (required && !value) return { valid: false, error: message || 'Campo obrigatório' };
            if (value) {
                if (min && value.length < min) return { valid: false, error: message || `Mínimo ${min} caracteres` };
                if (max && value.length > max) return { valid: false, error: message || `Máximo ${max} caracteres` };
            }
            return { valid: true, error: null };
        }
    }),
    
    // Number validation
    number: (options = {}) => ({
        validate: (value) => {
            const { min, max, required = true, message } = options;
            
            if (required && (value === null || value === undefined || value === '')) {
                return { valid: false, error: message || 'Campo obrigatório' };
            }
            const num = Number(value);
            if (isNaN(num)) return { valid: false, error: message || 'Número inválido' };
            if (min !== undefined && num < min) return { valid: false, error: message || `Mínimo ${min}` };
            if (max !== undefined && num > max) return { valid: false, error: message || `Máximo ${max}` };
            return { valid: true, error: null };
        }
    }),
    
    // Date validation
    date: (options = {}) => ({
        validate: (value) => {
            const { required = true, message } = options;
            
            if (required && !value) return { valid: false, error: message || 'Data é obrigatória' };
            if (value) {
                const date = new Date(value);
                if (isNaN(date.getTime())) return { valid: false, error: message || 'Data inválida' };
            }
            return { valid: true, error: null };
        }
    }),
    
    // Select/Enum validation
    enum: (options = {}) => ({
        validate: (value) => {
            const { values, required = true, message } = options;
            
            if (required && !value) return { valid: false, error: message || 'Seleção obrigatória' };
            if (value && values && !values.includes(value)) {
                return { valid: false, error: message || 'Valor inválido' };
            }
            return { valid: true, error: null };
        }
    })
};

// Form validation helper
const FormValidator = {
    /**
     * Validar um formulário completo
     * @param {Object} data - Dados do formulário
     * @param {Object} rules - Regras de validação
     * @returns {Object} - { valid, errors }
     */
    validate(data, rules) {
        const errors = {};
        let isValid = true;
        
        for (const field in rules) {
            const rule = rules[field];
            const value = data[field];
            const result = rule.validate(value);
            
            if (!result.valid) {
                errors[field] = result.error;
                isValid = false;
            }
        }
        
        return { valid: isValid, errors };
    },
    
    /**
     * Validar campo individual
     * @param {string} field - Nome do campo
     * @param {Object} rule - Regra de validação
     * @param {*} value - Valor a validar
     * @returns {Object} - { valid, error }
     */
    validateField(field, rule, value) {
        return rule.validate(value);
    }
};

// Exportar
window.schemas = schemas;
window.FormValidator = FormValidator;