using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebSpark.PrismSpark.Demo.Models;
using WebSpark.PrismSpark.Demo.Services;
using System.Text.Json;

namespace WebSpark.PrismSpark.Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICodeHighlightingService _codeHighlightingService;

        public HomeController(ILogger<HomeController> logger, ICodeHighlightingService codeHighlightingService)
        {
            _logger = logger;
            _codeHighlightingService = codeHighlightingService;
        }

        public IActionResult Index()
        {
            var model = GetCodeExamples();
            return View(model);
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidateCode([FromBody] CodeValidationRequest request)
        {
            try
            {
                var highlightedCode = await _codeHighlightingService.HighlightCodeAsync(request.Code, request.Language);
                var formattedCode = _codeHighlightingService.ValidateAndFormatCode(request.Code, request.Language);

                var response = new CodeValidationResponse
                {
                    IsValid = ValidateCodeSyntax(request.Code, request.Language),
                    Message = GetValidationMessage(request.Code, request.Language),
                    HighlightedCode = highlightedCode,
                    FormattedCode = formattedCode
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating code");
                return Json(new CodeValidationResponse
                {
                    IsValid = false,
                    Message = "Error occurred during validation",
                    HighlightedCode = System.Net.WebUtility.HtmlEncode(request.Code),
                    FormattedCode = System.Net.WebUtility.HtmlEncode(request.Code)
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> HighlightCode([FromBody] CodeValidationRequest request)
        {
            try
            {
                var highlightedCode = await _codeHighlightingService.HighlightCodeAsync(request.Code, request.Language);
                return Json(new { success = true, highlightedCode });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error highlighting code");
                return Json(new { success = false, error = "Error occurred during highlighting" });
            }
        }

        [HttpPost]
        public IActionResult FormatCode([FromBody] CodeValidationRequest request)
        {
            try
            {
                var formattedCode = _codeHighlightingService.ValidateAndFormatCode(request.Code, request.Language);
                return Json(new { success = true, formattedCode });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error formatting code");
                return Json(new { success = false, error = "Error occurred during formatting" });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<CodeSnippet> GetCodeExamples()
        {
            var examples = new List<CodeSnippet>
            {
                new CodeSnippet
                {
                    Language = "json",
                    Title = "JSON - JavaScript Object Notation",
                    Code = GetSampleJson()
                },
                new CodeSnippet
                {
                    Language = "csharp",
                    Title = "C# - .NET Programming Language",
                    Code = GetSampleCSharp()
                },
                new CodeSnippet
                {
                    Language = "javascript",
                    Title = "JavaScript - Dynamic Programming Language",
                    Code = GetSampleJavaScript()
                }
            };

            // Highlight each code example
            foreach (var example in examples)
            {
                example.HighlightedCode = _codeHighlightingService.HighlightCodeWithTheme(example.Code, example.Language);
            }

            return examples;
        }

        private string GetSampleJson()
        {
            return @"{
  ""name"": ""PrismJS Validation Demo"",
  ""version"": ""1.0.0"",
  ""description"": ""A comprehensive validation example"",
  ""languages"": [""json"", ""csharp"", ""javascript""],
  ""features"": {
    ""syntaxHighlighting"": true,
    ""validation"": true,
    ""lineNumbers"": false
  },
  ""author"": {
    ""name"": ""Developer"",
    ""email"": ""dev@example.com""
  },
  ""dependencies"": {
    ""prismjs"": ""^1.29.0""
  },
  ""config"": {
    ""port"": 3000,
    ""ssl"": false,
    ""timeout"": 30000
  }
}";
        }

        private string GetSampleCSharp()
        {
            return @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PrismValidation.Controllers
{
    [ApiController]
    [Route(""api/[controller]"")]
    public class ValidationController : ControllerBase
    {
        private readonly ILogger<ValidationController> _logger;

        public ValidationController(ILogger<ValidationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ValidationResult>>> GetValidationResults()
        {
            try
            {
                var results = await ProcessValidationAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ""Error occurred during validation"");
                return StatusCode(500, ""Internal server error"");
            }
        }

        [HttpPost(""validate"")]
        public ActionResult<bool> ValidateCode([FromBody] CodeValidationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return BadRequest(""Code cannot be empty"");
            }

            var isValid = PerformSyntaxValidation(request.Code, request.Language);
            return Ok(new { IsValid = isValid, Message = isValid ? ""Valid syntax"" : ""Invalid syntax"" });
        }

        private async Task<List<ValidationResult>> ProcessValidationAsync()
        {
            var tasks = new List<Task<ValidationResult>>();
            var languages = new[] { ""json"", ""csharp"", ""javascript"" };

            foreach (var lang in languages)
            {
                tasks.Add(ValidateLanguageAsync(lang));
            }

            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }

        private async Task<ValidationResult> ValidateLanguageAsync(string language)
        {
            await Task.Delay(100); // Simulate async operation
            return new ValidationResult { Language = language, IsValid = true };
        }

        private bool PerformSyntaxValidation(string code, string language)
        {
            // Basic validation logic here
            return !string.IsNullOrEmpty(code) && code.Length > 10;
        }
    }

    public class CodeValidationRequest
    {
        public string Code { get; set; }
        public string Language { get; set; }
    }

    public class ValidationResult
    {
        public string Language { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}";
        }

        private string GetSampleJavaScript()
        {
            return @"// PrismJS Validation Utility Functions
class CodeValidator {
    constructor() {
        this.supportedLanguages = ['json', 'javascript', 'csharp'];
        this.validationHistory = [];
    }

    async validateCode(code, language) {
        if (!this.supportedLanguages.includes(language)) {
            throw new Error(`Unsupported language: ${language}`);
        }

        const startTime = performance.now();

        try {
            let result;

            switch (language) {
                case 'json':
                    result = this.validateJSON(code);
                    break;
                case 'javascript':
                    result = this.validateJavaScript(code);
                    break;
                case 'csharp':
                    result = this.validateCSharp(code);
                    break;
                default:
                    result = { isValid: false, errors: ['Unknown language'] };
            }

            const endTime = performance.now();
            const validationRecord = {
                language,
                isValid: result.isValid,
                executionTime: endTime - startTime,
                timestamp: new Date().toISOString(),
                errors: result.errors || []
            };

            this.validationHistory.push(validationRecord);
            return validationRecord;

        } catch (error) {
            console.error('Validation error:', error);
            return {
                language,
                isValid: false,
                errors: [error.message],
                executionTime: performance.now() - startTime,
                timestamp: new Date().toISOString()
            };
        }
    }

    validateJSON(jsonString) {
        try {
            const parsed = JSON.parse(jsonString);
            return {
                isValid: true,
                parsedObject: parsed,
                size: JSON.stringify(parsed).length
            };
        } catch (error) {
            return {
                isValid: false,
                errors: [error.message]
            };
        }
    }

    validateJavaScript(jsCode) {
        const errors = [];

        // Basic syntax checks
        if (jsCode.includes('eval(')) {
            errors.push('Warning: eval() usage detected');
        }

        // Check for balanced brackets
        const brackets = { '(': ')', '[': ']', '{': '}' };
        const stack = [];

        for (let char of jsCode) {
            if (brackets[char]) {
                stack.push(brackets[char]);
            } else if (Object.values(brackets).includes(char)) {
                if (stack.pop() !== char) {
                    errors.push('Unbalanced brackets detected');
                    break;
                }
            }
        }

        if (stack.length > 0) {
            errors.push('Unclosed brackets detected');
        }

        return {
            isValid: errors.length === 0,
            errors: errors,
            metrics: {
                lines: jsCode.split('\n').length,
                characters: jsCode.length,
                functions: (jsCode.match(/function\s+\w+/g) || []).length
            }
        };
    }

    validateCSharp(csharpCode) {
        const errors = [];
        const warnings = [];

        // Basic C# syntax validation
        if (!csharpCode.includes('using ') && csharpCode.includes('System')) {
            warnings.push('Consider adding using statements');
        }

        // Check for balanced braces
        const openBraces = (csharpCode.match(/{/g) || []).length;
        const closeBraces = (csharpCode.match(/}/g) || []).length;

        if (openBraces !== closeBraces) {
            errors.push(`Unbalanced braces: ${openBraces} open, ${closeBraces} close`);
        }

        return {
            isValid: errors.length === 0,
            errors: errors,
            warnings: warnings,
            metrics: {
                lines: csharpCode.split('\n').length,
                classes: (csharpCode.match(/class\s+\w+/g) || []).length,
                methods: (csharpCode.match(/public\s+\w+\s+\w+\s*\(/g) || []).length
            }
        };
    }

    getValidationStats() {
        const stats = {
            totalValidations: this.validationHistory.length,
            validCount: this.validationHistory.filter(v => v.isValid).length,
            averageExecutionTime: 0,
            languageBreakdown: {}
        };

        if (stats.totalValidations > 0) {
            stats.averageExecutionTime = this.validationHistory.reduce(
                (sum, v) => sum + v.executionTime, 0
            ) / stats.totalValidations;

            this.supportedLanguages.forEach(lang => {
                const langValidations = this.validationHistory.filter(v => v.language === lang);
                stats.languageBreakdown[lang] = {
                    total: langValidations.length,
                    valid: langValidations.filter(v => v.isValid).length
                };
            });
        }

        return stats;
    }

    exportValidationHistory() {
        return {
            exportDate: new Date().toISOString(),
            totalRecords: this.validationHistory.length,
            data: this.validationHistory
        };
    }
}

// Initialize validator
const validator = new CodeValidator();

// Export for global usage
window.codeValidator = validator;";
        }

        private bool ValidateCodeSyntax(string code, string language)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;

            try
            {
                switch (language?.ToLower())
                {
                    case "json":
                        JsonDocument.Parse(code);
                        return true;
                    case "javascript":
                    case "js":
                        // Basic JS validation
                        var openBraces = code.Count(c => c == '{');
                        var closeBraces = code.Count(c => c == '}');
                        return openBraces == closeBraces;
                    case "csharp":
                    case "cs":
                        // Basic C# validation
                        var openCsBraces = code.Count(c => c == '{');
                        var closeCsBraces = code.Count(c => c == '}');
                        return openCsBraces == closeCsBraces;
                    default:
                        return true; // Assume valid for unknown languages
                }
            }
            catch
            {
                return false;
            }
        }

        private string GetValidationMessage(string code, string language)
        {
            if (ValidateCodeSyntax(code, language))
            {
                return $"Valid {language} syntax";
            }
            else
            {
                return $"Invalid {language} syntax detected";
            }
        }
    }
}
