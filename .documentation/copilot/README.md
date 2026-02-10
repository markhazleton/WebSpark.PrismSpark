# Copilot Session Outputs

This directory contains ephemeral documentation, analyses, and artifacts generated during GitHub Copilot chat sessions.

## Directory Structure

```
copilot/
├── README.md                      # This file
├── session-2026-01-05/            # .NET 10 upgrade planning session
├── session-2026-02-10/            # Repository cleanup and organization
└── session-YYYY-MM-DD/            # Future session outputs
```

## Naming Convention

- **Format**: `session-YYYY-MM-DD/` (ISO 8601 date format)
- **Date**: Use the session start date, not the last update date
- **Files**: Use kebab-case naming (e.g., `feature-analysis.md`, `code-review.md`)

## Purpose

Copilot sessions are **exploratory and ephemeral** by nature. This organization enables:

1. **Tracking**: Understand when and why architectural decisions were considered
2. **Archival**: Easy deletion of outdated AI-generated content after decisions are finalized
3. **Separation**: Keep temporary explorations separate from permanent project documentation
4. **Discovery**: Reference prior analyses when revisiting similar problems

## What Belongs Here

✅ **Include**:
- Feature analyses and explorations
- Code review summaries
- Architecture exploration documents
- Refactoring suggestions and trade-off analyses
- Planning artifacts (spec.md, plan.md, tasks.md drafts)
- Implementation session logs
- Upgrade/migration planning documents

❌ **Exclude**:
- Permanent project documentation (→ `.documentation/docs/`)
- Project constitution (→ `.documentation/memory/constitution.md`)
- Templates (→ `.documentation/templates/`)
- Agent definitions (→ `.github/agents/`)
- Copilot instructions (→ `.github/copilot-instructions.md`)

## Lifecycle Management

### During Development
- Create a new session folder for each significant Copilot interaction
- Document exploratory analyses, decision rationale, and implementation notes
- Reference session outputs in commit messages or PRs when helpful

### At Release
- Run `speckit.release` agent to archive/distill key decisions
- Move finalized decisions to permanent documentation in `.documentation/docs/`
- Optionally delete outdated sessions to reduce repository size

### Retention Policy
- **Recent sessions** (< 90 days): Keep for reference
- **Historical sessions** (> 90 days): Archive or delete after extracting key decisions
- **Upgrade/migration sessions**: Keep indefinitely for historical context

## File Examples

### session-2026-01-05/
.NET 10 upgrade planning session:
- `assessment.md` - Compatibility analysis
- `execution-log.md` - Upgrade execution tracking
- `plan.md` - Upgrade strategy
- `tasks.md` - Task breakdown
- `assessment.csv`, `assessment.json` - Machine-readable data

### session-2026-02-10/
Repository cleanup and organization:
- `repository-cleanup-report.md` - Cleanup summary and recommendations
- `completion-summary.md` - Feature completion summary (moved from root)
- `enhanced-features.md` - Feature documentation (moved from root)

## References

- [Project Constitution](../memory/constitution.md) - Governance and principles
- [Copilot Instructions](../../.github/copilot-instructions.md) - File organization rules
- [Permanent Documentation](../docs/) - Project documentation (when created)

---

**Last Updated**: 2026-02-10
