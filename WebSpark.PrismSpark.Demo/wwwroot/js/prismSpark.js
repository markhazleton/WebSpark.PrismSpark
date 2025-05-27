/**
 * PrismSpark JavaScript Library
 * Enhanced client-side functionality for code examples and live editor
 */

// Global validation statistics
let validationStats = {
    totalValidations: 0,
    successfulValidations: 0,
    executionTimes: []
};

/**
 * Validate code using server-side validation
 * @param {string} language - The programming language
 */
async function validateCode(language) {
    const codeElement = document.getElementById(`${language}-code`);
    const resultElement = document.getElementById(`${language}-result`);

    if (!codeElement || !resultElement) return;

    const code = codeElement.textContent || codeElement.innerText;

    try {
        resultElement.className = 'validation-result loading';
        resultElement.innerHTML = 'Validating...';

        const startTime = performance.now();

        const response = await fetch('/Home/ValidateCode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ code, language })
        });

        const endTime = performance.now();
        const executionTime = endTime - startTime;

        const result = await response.json();

        updateValidationStats(result.isValid, executionTime);
        displayValidationResult(resultElement, result, executionTime);

    } catch (error) {
        console.error('Validation error:', error);
        resultElement.className = 'validation-result error';
        resultElement.innerHTML = `<strong>Error:</strong> ${error.message}`;
    }
}

/**
 * Format code using server-side formatting
 * @param {string} language - The programming language
 */
async function formatCode(language) {
    const codeElement = document.getElementById(`${language}-code`);
    const resultElement = document.getElementById(`${language}-result`);

    if (!codeElement || !resultElement) return;

    const code = codeElement.textContent || codeElement.innerText;

    try {
        resultElement.className = 'validation-result loading';
        resultElement.innerHTML = 'Formatting...';

        const response = await fetch('/Home/FormatCode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ code, language })
        });

        const result = await response.json();

        if (result.formattedCode) {
            codeElement.innerHTML = result.formattedCode;
            resultElement.className = 'validation-result success';
            resultElement.innerHTML = '<strong>✓</strong> Code formatted successfully!';
        } else {
            resultElement.className = 'validation-result error';
            resultElement.innerHTML = `<strong>Error:</strong> ${result.error || 'Formatting failed'}`;
        }

    } catch (error) {
        console.error('Formatting error:', error);
        resultElement.className = 'validation-result error';
        resultElement.innerHTML = `<strong>Error:</strong> ${error.message}`;
    }
}

/**
 * Highlight code using server-side highlighting
 * @param {string} language - The programming language
 */
async function highlightCode(language) {
    const codeElement = document.getElementById(`${language}-code`);
    const resultElement = document.getElementById(`${language}-result`);

    if (!codeElement || !resultElement) return;

    const code = codeElement.textContent || codeElement.innerText;

    try {
        resultElement.className = 'validation-result loading';
        resultElement.innerHTML = 'Highlighting...';

        const response = await fetch('/Home/HighlightCode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ code, language })
        });

        const result = await response.json();

        if (result.highlightedCode) {
            codeElement.innerHTML = result.highlightedCode;
            resultElement.className = 'validation-result success';
            resultElement.innerHTML = '<strong>✓</strong> Code highlighted successfully!';
        } else {
            resultElement.className = 'validation-result error';
            resultElement.innerHTML = `<strong>Error:</strong> ${result.error || 'Highlighting failed'}`;
        }

    } catch (error) {
        console.error('Highlighting error:', error);
        resultElement.className = 'validation-result error';
        resultElement.innerHTML = `<strong>Error:</strong> ${error.message}`;
    }
}

/**
 * Clear validation result for a specific language
 * @param {string} language - The programming language
 */
function clearResult(language) {
    const resultElement = document.getElementById(`${language}-result`);
    if (resultElement) {
        resultElement.className = 'validation-result';
        resultElement.innerHTML = '';
    }
}

// ========================================
// Live Editor Functions
// ========================================

/**
 * Validate code in the live editor
 */
async function validateLiveCode() {
    const editor = document.getElementById('live-editor');
    const languageSelector = document.getElementById('language-selector');
    const resultElement = document.getElementById('live-result');

    if (!editor || !languageSelector || !resultElement) return;

    const code = editor.value;
    const language = languageSelector.value;

    if (!code.trim()) {
        showResult(resultElement, 'Please enter some code to validate.', 'info');
        return;
    }

    try {
        resultElement.className = 'validation-result loading';
        resultElement.innerHTML = 'Validating...';

        const startTime = performance.now();

        const response = await fetch('/Home/ValidateCode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                code: code,
                language: language
            })
        });

        const endTime = performance.now();
        const executionTime = Math.round(endTime - startTime);

        const result = await response.json();

        updateValidationStats(result.isValid, executionTime);

        const message = `${result.message} (${executionTime}ms)`;
        showResult(resultElement, message, result.isValid ? 'success' : 'error');

    } catch (error) {
        console.error('Validation error:', error);
        showResult(resultElement, 'Error occurred during validation', 'error');
    }
}

/**
 * Highlight code in the live editor
 */
async function highlightLiveCode() {
    const editor = document.getElementById('live-editor');
    const languageSelector = document.getElementById('language-selector');
    const previewElement = document.getElementById('live-preview');

    if (!editor || !languageSelector || !previewElement) return;

    const code = editor.value;
    const language = languageSelector.value;

    if (!code.trim()) {
        previewElement.innerHTML = '<p class="text-muted">Enter code to see syntax highlighting...</p>';
        return;
    }

    try {
        previewElement.className = 'code-preview loading';
        previewElement.innerHTML = 'Highlighting...';

        const response = await fetch('/Home/HighlightCode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                code: code,
                language: language
            })
        });

        const result = await response.json();

        previewElement.className = 'code-preview';
        previewElement.innerHTML = `<pre><code class="language-${language}">${result.highlightedCode}</code></pre>`;

    } catch (error) {
        console.error('Highlighting error:', error);
        previewElement.className = 'code-preview';
        previewElement.innerHTML = '<p class="text-danger">Error occurred during highlighting</p>';
    }
}

/**
 * Format code in the live editor
 */
async function formatLiveCode() {
    const editor = document.getElementById('live-editor');
    const languageSelector = document.getElementById('language-selector');
    const resultElement = document.getElementById('live-result');

    if (!editor || !languageSelector || !resultElement) return;

    const code = editor.value;
    const language = languageSelector.value;

    if (!code.trim()) {
        showResult(resultElement, 'Please enter some code to format.', 'info');
        return;
    }

    try {
        resultElement.className = 'validation-result loading';
        resultElement.innerHTML = 'Formatting...';

        const response = await fetch('/Home/FormatCode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                code: code,
                language: language
            })
        });

        const result = await response.json();

        if (result.formattedCode && result.formattedCode !== code) {
            // Show formatted code in modal with syntax highlighting
            await showFormattedCodeModal(result.formattedCode, language);
            showResult(resultElement, 'Code formatted successfully! Click "Apply to Editor" to use the formatted version.', 'success');

            // Clear loading state
            resultElement.className = 'validation-result success';
        } else {
            showResult(resultElement, 'Code is already properly formatted.', 'info');
        }

    } catch (error) {
        console.error('Formatting error:', error);
        showResult(resultElement, 'Error occurred during formatting', 'error');
    }
}

/**
 * Clear the live editor
 */
function clearEditor() {
    const editor = document.getElementById('live-editor');
    const previewElement = document.getElementById('live-preview');
    const resultElement = document.getElementById('live-result');

    if (editor) editor.value = '';
    if (previewElement) previewElement.innerHTML = '<p class="text-muted">Enter code to see syntax highlighting...</p>';
    if (resultElement) {
        resultElement.className = 'validation-result';
        resultElement.innerHTML = '';
    }
}

// ========================================
// Utility Functions
// ========================================

/**
 * Display result message with appropriate styling
 * @param {HTMLElement} element - The result element
 * @param {string} message - The message to display
 * @param {string} type - The type of message (success, error, info)
 */
function showResult(element, message, type) {
    element.className = `validation-result ${type}`;
    element.innerHTML = message;
}

/**
 * Update validation statistics
 * @param {boolean} isValid - Whether the validation was successful
 * @param {number} executionTime - The execution time in milliseconds
 */
function updateValidationStats(isValid, executionTime) {
    validationStats.totalValidations++;
    if (isValid) validationStats.successfulValidations++;
    validationStats.executionTimes.push(executionTime);

    // Update display elements if they exist
    const totalElement = document.getElementById('total-validations');
    const successRateElement = document.getElementById('success-rate');
    const avgTimeElement = document.getElementById('avg-time');

    if (totalElement) {
        totalElement.textContent = validationStats.totalValidations;
    }

    if (successRateElement) {
        const successRate = validationStats.totalValidations > 0
            ? Math.round((validationStats.successfulValidations / validationStats.totalValidations) * 100)
            : 0;
        successRateElement.textContent = successRate + '%';
    }

    if (avgTimeElement) {
        const avgTime = validationStats.executionTimes.length > 0
            ? Math.round(validationStats.executionTimes.reduce((a, b) => a + b, 0) / validationStats.executionTimes.length)
            : 0;
        avgTimeElement.textContent = avgTime + 'ms';
    }
}

/**
 * Display validation result with execution time
 * @param {HTMLElement} element - The result element
 * @param {Object} result - The validation result
 * @param {number} executionTime - The execution time in milliseconds
 */
function displayValidationResult(element, result, executionTime) {
    const className = result.isValid ? 'validation-result success' : 'validation-result error';
    const icon = result.isValid ? '✓' : '✗';

    element.className = className;
    element.innerHTML = `
        <div style="display: flex; justify-content: space-between; align-items: center; flex-wrap: wrap;">
            <div>
                <strong>${icon} ${result.message}</strong>
            </div>
            <div style="font-size: 0.9rem; color: #666;">
                Execution time: ${Math.round(executionTime)}ms
            </div>
        </div>
    `;
}

// ========================================
// Modal Functions (for Format Result)
// ========================================

/**
 * Copy formatted code to clipboard
 */
function copyFormattedCode() {
    const formattedCodeContainer = document.getElementById('formatted-code-container');
    const codeElement = formattedCodeContainer.querySelector('code, pre');
    const code = codeElement ? codeElement.textContent || codeElement.innerText : formattedCodeContainer.textContent;

    navigator.clipboard.writeText(code).then(() => {
        // Show temporary success message
        const copyBtn = event.target;
        const originalText = copyBtn.innerHTML;
        copyBtn.innerHTML = '<i class="bi bi-check"></i> Copied!';
        copyBtn.classList.remove('btn-outline-secondary');
        copyBtn.classList.add('btn-success');

        setTimeout(() => {
            copyBtn.innerHTML = originalText;
            copyBtn.classList.remove('btn-success');
            copyBtn.classList.add('btn-outline-secondary');
        }, 2000);
    }, (err) => {
        console.error('Could not copy text: ', err);
        alert('Failed to copy to clipboard. Please try selecting and copying manually.');
    });
}

/**
 * Apply formatted code to the editor
 */
function applyFormattedCode() {
    const formattedCodeContainer = document.getElementById('formatted-code-container');
    const codeElement = formattedCodeContainer.querySelector('code, pre');
    const code = codeElement ? codeElement.textContent || codeElement.innerText : formattedCodeContainer.textContent;
    const editor = document.getElementById('live-editor');

    if (editor && code) {
        // Confirm before applying
        if (confirm('Apply the formatted code to the editor? This will replace your current code.')) {
            editor.value = code;

            // Close modal
            const formatModal = bootstrap.Modal.getInstance(document.getElementById('formatModal'));
            if (formatModal) {
                formatModal.hide();
            }

            // Update preview
            highlightLiveCode();

            // Show success message
            const resultElement = document.getElementById('live-result');
            showResult(resultElement, 'Formatted code applied to editor successfully!', 'success');
        }
    }
}

/**
 * Show formatted code in modal
 * @param {string} formattedCode - The formatted code
 * @param {string} language - The programming language
 */
async function showFormattedCodeModal(formattedCode, language) {
    // Update modal content
    const languageDisplay = document.getElementById('format-language-display');
    if (languageDisplay) {
        languageDisplay.textContent = language.toUpperCase();
    }

    // Get syntax highlighted version of formatted code
    try {
        const response = await fetch('/Home/HighlightCode', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                code: formattedCode,
                language: language
            })
        });

        const result = await response.json();

        // Display highlighted formatted code
        const container = document.getElementById('formatted-code-container');
        if (container) {
            container.innerHTML = `<pre class="formatted-code-display"><code class="language-${language}">${result.highlightedCode}</code></pre>`;
        }

    } catch (error) {
        console.error('Error highlighting formatted code:', error);
        // Fallback to plain text
        const container = document.getElementById('formatted-code-container');
        if (container) {
            container.innerHTML = `<pre class="formatted-code-display"><code>${formattedCode}</code></pre>`;
        }
    }

    // Show modal
    const formatModal = new bootstrap.Modal(document.getElementById('formatModal'));
    formatModal.show();
}

// ========================================
// Live Editor Sample Code
// ========================================

/**
 * Load default code based on selected language
 */
function loadDefaultCode() {
    const languageSelector = document.getElementById('language-selector');
    const editor = document.getElementById('live-editor');

    if (!languageSelector || !editor) return;

    const language = languageSelector.value;
    let defaultCode = '';

    switch (language) {
        case 'json':
            defaultCode = `{
  "name": "WebSpark.PrismSpark",
  "version": "1.0.0",
  "description": "Syntax highlighting and code formatting library",
  "features": [ "highlighting", "validation", "formatting" ],
  "author": { "name": "Mark Hazleton", "url": "https://github.com/MarkHazleton" }
}`;
            break;
        case 'csharp':
            defaultCode = `using System;
using System.Collections.Generic;

namespace WebSpark.PrismSpark.Demo
{
    public class CodeEditor
    {
        private readonly List<string> _supportedLanguages;
        
        public CodeEditor()
        {
            _supportedLanguages = new List<string> { "C#", "JavaScript", "Python", "JSON", "SQL", "HTML", "CSS" };
        }
        
        public bool ValidateCode(string code, string language)
        {
            if (string.IsNullOrWhiteSpace(code)) return false;
            return _supportedLanguages.Contains(language);
        }
    }
}`;
            break;
        case 'javascript':
            defaultCode = `// Interactive Code Editor with PrismSpark
class CodeEditor {
    constructor() {
        this.supportedLanguages = ['json', 'csharp', 'javascript', 'python', 'sql', 'html', 'css'];
        this.validationStats = { total: 0, successful: 0, executionTimes: [] };
    }
    
    async validateCode(code, language) {
        if (!code.trim()) return { isValid: false, message: 'Code cannot be empty' };
        
        const startTime = performance.now();
        // Validation logic here
        const endTime = performance.now();
        
        return { 
            isValid: true, 
            message: 'Code validation successful',
            executionTime: Math.round(endTime - startTime)
        };
    }
}

const editor = new CodeEditor();
console.log('Code editor initialized with', editor.supportedLanguages.length, 'supported languages');`;
            break;
        case 'python':
            defaultCode = `# PrismSpark Code Editor - Python Example
import json
import time
from typing import List, Dict, Any

class CodeEditor:
    def __init__(self):
        self.supported_languages = ['json', 'csharp', 'javascript', 'python', 'sql', 'html', 'css']
        self.validation_stats = {'total': 0, 'successful': 0, 'execution_times': []}
    
    def validate_code(self, code: str, language: str) -> Dict[str, Any]:
        """Validate code and return validation results"""
        if not code.strip():
            return {'is_valid': False, 'message': 'Code cannot be empty'}
        
        start_time = time.time()
        # Validation logic would go here
        end_time = time.time()
        
        execution_time = round((end_time - start_time) * 1000)  # Convert to ms
        
        return {
            'is_valid': language in self.supported_languages,
            'message': f'Validation completed for {language}',
            'execution_time': execution_time
        }

# Initialize the editor
editor = CodeEditor()
print(f"Code editor supports {len(editor.supported_languages)} languages")`;
            break;
        case 'sql':
            defaultCode = `-- PrismSpark Demo - SQL Example
-- Sample database queries for code formatting demo

SELECT 
    u.UserId,
    u.Username,
    u.Email,
    COUNT(p.PostId) as TotalPosts,
    MAX(p.CreatedDate) as LastPostDate
FROM Users u
LEFT JOIN Posts p ON u.UserId = p.UserId
WHERE u.IsActive = 1
    AND u.CreatedDate >= '2023-01-01'
GROUP BY u.UserId, u.Username, u.Email
HAVING COUNT(p.PostId) > 0
ORDER BY TotalPosts DESC, LastPostDate DESC;`;
            break;
        case 'html':
            defaultCode = `<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>PrismSpark Demo - Code Highlighting</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <div class="container mt-5">
        <header class="text-center mb-4">
            <h1 class="display-4">WebSpark.PrismSpark Demo</h1>
            <p class="lead">Interactive syntax highlighting and code formatting</p>
        </header>
        
        <main>
            <div class="row">
                <div class="col-md-6">
                    <h3>Code Input</h3>
                    <textarea id="code-editor" class="form-control" rows="10" placeholder="Enter your code here..."></textarea>
                </div>
                <div class="col-md-6">
                    <h3>Highlighted Output</h3>
                    <div id="code-preview" class="border p-3 bg-light" style="min-height: 200px;"></div>
                </div>
            </div>
        </main>
    </div>
</body>
</html>`;
            break;
        case 'css':
            defaultCode = `/* PrismSpark Demo - CSS Styling */
:root {
    --primary-color: #667eea;
    --secondary-color: #764ba2;
    --success-color: #28a745;
    --warning-color: #ffc107;
    --error-color: #dc3545;
    --font-mono: 'Courier New', Consolas, 'Ubuntu Mono', monospace;
}

.code-editor-container {
    background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
    border-radius: 12px;
    padding: 2rem;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
    margin: 2rem 0;
}

.code-textarea {
    font-family: var(--font-mono);
    font-size: 0.9rem;
    line-height: 1.5;
    border: 2px solid #e0e0e0;
    border-radius: 8px;
    padding: 1rem;
    resize: vertical;
    transition: border-color 0.3s ease;
}

.code-textarea:focus {
    border-color: var(--primary-color);
    box-shadow: 0 0 0 0.2rem rgba(102, 126, 234, 0.25);
    outline: none;
}`;
            break;
        default:
            defaultCode = '// Select a language to see sample code';
    }

    // Only set if editor is empty
    if (!editor.value.trim()) {
        editor.value = defaultCode;
        // Auto-highlight if there's default code
        if (defaultCode.trim()) {
            setTimeout(highlightLiveCode, 100);
        }
    }
}

// ========================================
// Event Listeners and Initialization
// ========================================

// Auto-highlight on language change and keyboard shortcuts
document.addEventListener('DOMContentLoaded', function () {
    const languageSelector = document.getElementById('language-selector');
    const editor = document.getElementById('live-editor');

    if (languageSelector) {
        languageSelector.addEventListener('change', function () {
            loadDefaultCode();
            if (editor && editor.value.trim()) {
                highlightLiveCode();
            }
        });
    }

    // Keyboard shortcuts
    if (editor) {
        editor.addEventListener('keydown', function (e) {
            // Ctrl+Shift+F for format
            if (e.ctrlKey && e.shiftKey && e.key === 'F') {
                e.preventDefault();
                formatLiveCode();
            }
            // Ctrl+Enter for highlight
            else if (e.ctrlKey && e.key === 'Enter') {
                e.preventDefault();
                highlightLiveCode();
            }
        });

        // Auto-highlight live code as user types (debounced)
        let highlightTimeout;
        editor.addEventListener('input', function () {
            clearTimeout(highlightTimeout);
            highlightTimeout = setTimeout(() => {
                if (this.value.trim()) {
                    highlightLiveCode();
                }
            }, 1000);
        });
    }

    // Load default code and initialize preview
    loadDefaultCode();
    const previewElement = document.getElementById('live-preview');
    if (previewElement) {
        previewElement.innerHTML = '<p class="text-muted">Enter code to see syntax highlighting...</p>';
    }
});
