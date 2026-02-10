<!--
===================================================================================
SYNC IMPACT REPORT - Constitution v1.0.0
===================================================================================
Version Change: 0.1.0-draft → 1.0.0
- First formal ratification from discovered patterns
- MAJOR version (1.0.0): Initial governance establishment

Principles Formalized:
✓ I. .NET Framework Standards (from draft)
✓ II. Code Documentation (from draft)
✓ III. Nullable Reference Types (from draft)
✓ IV. Testing Standards (from draft)
✓ V. Naming Conventions (from draft)
✓ VI. Error Handling Architecture (from draft)
✓ VII. Code Formatting (from draft)
✓ VIII. Continuous Integration (from draft - requires GitHub Actions implementation)

Architecture Patterns Added:
✓ Interface-Driven Design
✓ File-Scoped Namespaces

Added Sections:
+ Architecture Patterns (formalized extension patterns)
+ Governance (amendment procedures, enforcement policies)

Removed Sections:
- "Principles NOT Included" section (deferred to future amendments)

Templates Status:
✅ spec-template.md - Reviewed: Compatible (user stories with acceptance criteria)
✅ plan-template.md - Reviewed: Compatible (Constitution Check section present)
✅ tasks-template.md - Reviewed: Compatible (Test-first workflow supported)
⚠️  checklist-template.md - Not reviewed (non-critical for constitution)
⚠️  agent-file-template.md - Not reviewed (agent-specific, non-blocking)

Follow-up TODOs:
1. GitHub Actions workflow must be created to enforce CI/CD (Principle VIII)
2. Consider future amendments for: code coverage thresholds, performance benchmarks, 
   security scanning, accessibility standards, internationalization requirements, 
   logging level guidance, git workflow, release process/versioning
3. Update README.md references if needed (currently aligned)

Commit Message Suggestion:
docs: ratify constitution to v1.0.0 (formalize 8 core principles + governance)
===================================================================================
-->

# WebSpark.PrismSpark Constitution

## Core Principles

### I. .NET Framework Standards (MANDATORY)

All projects in the solution MUST use the same .NET version to ensure build consistency, deployment reliability, and dependency compatibility. Version choice is flexible, but synchronization across all projects is non-negotiable.

- All projects MUST target the same .NET framework version
- When upgrading framework versions, all projects MUST be upgraded together in a single PR
- Multi-targeting is permitted only if all projects use identical target frameworks
- Framework version MUST be documented in README.md and maintained in all .csproj files

**Rationale**: Version mismatches cause build failures, deployment issues, and dependency conflicts. Synchronized versions ensure predictable CI/CD and production behavior.

**Review Enforcement**: CRITICAL - PRs introducing framework version mismatches are rejected immediately.

---

### II. Code Documentation (MANDATORY)

All public APIs MUST have XML documentation comments to ensure library usability, maintainability, and NuGet package quality.

- Public classes, interfaces, methods, and properties MUST have `<summary>` tags
- Complex public APIs SHOULD include `<param>`, `<returns>`, and `<exception>` tags for clarity
- Private/internal members MAY have documentation but it is optional
- Demo and test projects SHOULD document complex logic but requirements are relaxed

**Rationale**: Public APIs without documentation reduce library adoption, increase support burden, and block NuGet publishing. Documentation is a first-class deliverable, not optional.

**Review Enforcement**: HIGH - Missing documentation on public APIs blocks library release and PR approval.

---

### III. Nullable Reference Types (MANDATORY)

Nullable reference types MUST be enabled for all projects to prevent null reference exceptions at compile time and improve code safety.

- All .csproj files MUST include `<Nullable>enable</Nullable>`
- Code MUST use nullable annotations correctly: `?` for nullable types, non-null by default
- Nullable warnings MUST be addressed before merging to protected branches
- Compiler warnings for nullable violations are treated as errors in Release builds

**Rationale**: Null safety is a foundational quality requirement. Nullable reference types catch null-related bugs at compile time rather than runtime, reducing production incidents and improving code reliability.

**Review Enforcement**: CRITICAL - Null safety is non-negotiable; PRs with nullable warnings are rejected.

---

### IV. Testing Standards (MANDATORY)

MSTest MUST be used as the testing framework, and new public features MUST include corresponding tests to maintain code quality and prevent regressions.

- All unit and integration tests MUST use MSTest framework with `[TestClass]` and `[TestMethod]` attributes
- New public APIs MUST have at least one test covering basic functionality before merging
- Tests SHOULD follow the AAA pattern (Arrange-Act-Assert) for clarity and maintainability
- Test projects MUST reference `Microsoft.NET.Test.Sdk` and `MSTest.TestFramework` packages
- Critical bug fixes SHOULD include regression tests to prevent reoccurrence

**Rationale**: Untested code is unshippable. Tests document expected behavior, prevent regressions, and enable confident refactoring. MSTest provides consistency across the entire test suite.

**Review Enforcement**: HIGH - Untested public features block PR approval; test coverage is verified in code review.

---

### V. Naming Conventions (MANDATORY)

Consistent naming conventions MUST be followed across the codebase to ensure readability, maintainability, and adherence to .NET ecosystem standards.

- Private fields MUST use underscore prefix with camelCase: `_fieldName`, `_httpClient`
- Public properties MUST use PascalCase: `LanguageName`, `TokenCount`
- Local variables and parameters MUST use camelCase: `tokenList`, `grammarName`
- Interfaces MUST start with `I` prefix: `IHighlighter`, `IPlugin`, `IGrammarDefinition`
- File names MUST match the primary type name in the file: `Grammar.cs`, `HtmlHighlighter.cs`
- Namespace structure MUST match folder structure for discoverability

**Rationale**: Consistent naming reduces cognitive load, aligns with .NET conventions, and improves codebase navigation. Naming violations signal rushed or inconsistent code quality.

**Review Enforcement**: MEDIUM - Naming violations reduce code quality but do not break functionality; must be fixed before merge.

---

### VI. Error Handling Architecture (MANDATORY)

Error handling strategy MUST differ by architectural layer to balance debuggability with operational resilience: library code throws exceptions for invalid input, service layer catches and logs for graceful degradation.

- **Library code** (core PrismSpark classes) MUST throw meaningful exceptions for invalid input or operational failures
- **Library code** MUST use appropriate exception types: `ArgumentException`, `ArgumentNullException`, `InvalidOperationException`
- **Service layer** (ASP.NET services) MUST catch exceptions from library code and log via `ILogger<T>`
- **Service layer** SHOULD return fallback values or graceful degradation when appropriate (e.g., fallback to plain text if highlighting fails)
- **Controllers** SHOULD catch exceptions from services and return appropriate HTTP status codes (400, 500)

**Rationale**: Library code throwing exceptions enables consumers to handle errors appropriately. Service-layer logging ensures operational visibility. This layered approach balances developer experience (clear errors) with production resilience (no unhandled exceptions).

**Review Enforcement**: MEDIUM - Incorrect error handling patterns reduce debuggability; must align with architecture.

---

### VII. Code Formatting (MANDATORY)

Code formatting MUST follow EditorConfig standards and be enforced during code reviews to maintain codebase consistency and reduce merge conflicts.

- All files MUST follow `.editorconfig` rules defined at repository root
- C# files MUST use 4-space indentation (no tabs)
- Line endings MUST be LF (Unix style) for cross-platform compatibility
- Files MUST use UTF-8 encoding without BOM
- Trailing whitespace MUST be trimmed from all lines
- Files MUST end with a newline character
- Code reviewers MUST reject PRs with formatting violations before functional review

**Rationale**: Consistent formatting reduces cognitive load, prevents merge conflicts, and signals professional code quality. Formatting is automated and non-negotiable.

**Review Enforcement**: LOW - Formatting issues are minor but must be fixed before merge; automated tooling should catch these.

---

### VIII. Continuous Integration (MANDATORY)

All pull requests MUST pass automated tests in CI before merging to protected branches. CI failures are blocking and non-negotiable.

- GitHub Actions (or equivalent CI system) MUST run on all pull requests to protected branches
- CI MUST execute `dotnet test` across all test projects in the solution
- All tests MUST pass for PR approval (zero tolerance for test failures)
- CI SHOULD run on multiple OS platforms (Windows, Linux, macOS) if cross-platform support is claimed
- CI SHOULD fail fast on first test failure to save build time and provide rapid feedback

**Rationale**: CI ensures code quality, prevents regressions, and maintains shippable main branch. Manual testing alone is insufficient for production-grade software.

**Review Enforcement**: CRITICAL - CI failures block PR approval; no exceptions unless CI infrastructure is down.

**Implementation Status**: ⚠️ GitHub Actions workflow required (TODO: create `.github/workflows/ci.yml`).

---

## Architecture Patterns

### Interface-Driven Design (RECOMMENDED)

Core abstractions SHOULD be defined via interfaces to support extensibility, testability, and dependency injection.

- New extensibility points SHOULD define interfaces: `IHighlighter`, `IPlugin`, `IGrammarDefinition`
- Services SHOULD depend on interfaces rather than concrete implementations to enable mocking and testing
- Dependency injection SHOULD be used in ASP.NET projects for flexibility and configurability
- Interfaces enable plugin architectures, theme systems, and future extensibility without breaking changes

**Rationale**: Interface-driven design enables loose coupling, testability, and extensibility. Concrete implementations can evolve independently without breaking consumers.

---

### File-Scoped Namespaces (MANDATORY)

All C# files MUST use file-scoped namespace declarations (C# 10+ feature) to reduce indentation and improve readability.

- Use `namespace WebSpark.PrismSpark;` at top of file instead of block syntax
- Reduces one level of indentation across entire file
- Improves vertical space utilization and readability

**Rationale**: File-scoped namespaces are standard in modern C# codebases. Consistency with C# 10+ idioms signals up-to-date practices.

**Review Enforcement**: MEDIUM - Enforce pattern consistency across all new files; legacy files updated opportunistically.

---

## Governance

### Constitution Authority

- This constitution supersedes all other practices, guidelines, and ad-hoc decisions
- When practices conflict with the constitution, the constitution takes precedence
- Principles marked MANDATORY are non-negotiable; violations require constitution amendment before proceeding
- Principles marked RECOMMENDED allow justified exceptions with documented rationale

### Amendment Procedures

- Any team member can propose constitution amendments via pull request to this document
- Amendments require code review approval from project maintainers
- Major principle changes (MANDATORY → RECOMMENDED, removing principles, adding blocking requirements) require consensus among all active maintainers
- Minor clarifications (wording improvements, examples, non-semantic fixes) can be approved by single maintainer
- Amendment PRs MUST include version bump according to semantic versioning rules below

### Versioning Policy

Constitution version follows semantic versioning (MAJOR.MINOR.PATCH):

- **MAJOR**: Backward-incompatible governance changes, principle removals, or redefinitions that change enforcement
- **MINOR**: New principles added, materially expanded guidance, or new sections that add requirements
- **PATCH**: Clarifications, wording improvements, typo fixes, or non-semantic refinements

### Enforcement & Review Process

- Code reviews MUST reference constitution principles when providing feedback (e.g., "Violates Principle II: missing XML docs")
- PR templates SHOULD include constitution compliance checklist for common principles
- Violations of MANDATORY principles block PR approval without exception
- Violations of RECOMMENDED principles require documented justification and maintainer approval
- Constitution SHOULD be reviewed quarterly or after major milestones to ensure relevance
- Annual review SHOULD assess: Are principles still relevant? Are they being followed? What patterns have emerged?

### Compliance & Exceptions

- Complexity that violates simplicity principles MUST be justified in plan.md Complexity Tracking table
- When constitution compliance is unclear, err toward strict interpretation and seek clarification
- Emergency hotfixes to production MAY bypass constitution temporarily but MUST be addressed in follow-up PR
- Technical debt introduced via exception MUST be documented with tracking issue and timeline for resolution

**Runtime Development Guidance**: See [.documentation/quickstart.md](../.documentation/quickstart.md) for practical development workflows (if exists).

---

**Version**: 1.0.0 | **Ratified**: 2026-02-10 | **Last Amended**: 2026-02-10
