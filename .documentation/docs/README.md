# Project Documentation

This directory contains **permanent project documentation** for WebSpark.PrismSpark.

## Current Status

⚠️ **Directory recently created** - Documentation migration in progress.

This folder was created on 2026-02-10 as part of repository cleanup to align with project constitution standards.

## Planned Documentation

The following documentation files should be created here as the project evolves:

### Essential Documentation
- **contributing.md** - Contribution guidelines, PR process, code review standards
- **architecture.md** - High-level architecture, design decisions, patterns
- **api-reference.md** - Comprehensive API documentation for library consumers
- **deployment.md** - NuGet package publishing, release process

### Supporting Documentation  
- **troubleshooting.md** - Common issues, FAQ, debugging guides
- **language-support.md** - Details on supported grammars, adding new languages
- **plugin-development.md** - Guide for creating custom plugins
- **theme-development.md** - Guide for creating custom themes
- **performance.md** - Performance characteristics, optimization guidelines
- **changelog.md** - Version history (if not using GitHub Releases exclusively)

## Documentation Standards

### File Naming
- Use **kebab-case**: `api-reference.md`, not `API_Reference.md` or `ApiReference.md`
- Use descriptive names that clearly indicate content
- Avoid generic names like `notes.md` or `info.md`

### Content Guidelines
- Include a title (H1) matching the filename concept
- Use relative links for internal cross-references
- Include "Last Updated" date at the bottom
- Reference constitution principles where applicable
- Follow markdown best practices (consistent heading hierarchy, code block language tags)

### Frontmatter (Optional but Recommended)
```markdown
---
title: Architecture Overview
category: design
last-updated: 2026-02-10
related: [api-reference.md, performance.md]
---
```

## What Doesn't Belong Here

❌ **Exclude**:
- Copilot session outputs (→ `.documentation/copilot/session-{date}/`)
- Project constitution (→ `.documentation/memory/constitution.md`)
- Templates (→ `.documentation/templates/`)
- Repository README (→ `README.md` at repository root)
- Project-specific READMEs (→ Keep in respective project folders)
- Temporary/draft documents (→ `.documentation/copilot/session-{date}/`)

## Migration from Existing Sources

Consider extracting content from:
- `WebSpark.PrismSpark/README.md` - Technical details that benefit all consumers
- GitHub Wiki (if exists) - Consolidate here for version control
- Code comments - Extract high-level architecture explanations
- Previous session outputs - Distill finalized decisions into permanent docs

## References

- [Project Constitution](../memory/constitution.md) - Governance rules
- [Copilot Instructions](../../.github/copilot-instructions.md) - Documentation organization
- [Copilot Session Outputs](../copilot/) - Temporary exploration artifacts

---

**Created**: 2026-02-10  
**Status**: Directory established, awaiting content migration
