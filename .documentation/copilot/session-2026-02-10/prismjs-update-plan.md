# PrismJS Update Plan: v1.27.0 → v1.30.0

**Generated**: 2026-02-10  
**Session**: copilot-chat  
**Purpose**: Update PrismJS reference submodule from v1.27.0 (March 2022) to v1.30.0 (March 2025)

---

## Executive Summary

The `prismjs/` submodule in WebSpark.PrismSpark is currently **3 years out of date** (v1.27.0 from February 2022). This plan outlines the steps to update to v1.30.0 (March 2025) and evaluate the impact on the C# port.

**Risk Level**: Medium (breaking changes possible in grammars, new features to potentially port)

---

## Current State

- **Current Version**: v1.27.0 (February 17, 2022)
- **Current Commit**: `4c3f196976b9177817da1f7d681efc6f8dd49b76` (March 22, 2022)
- **Target Version**: v1.30.0 (March 10, 2025)
- **Versions Behind**: 3 major releases (v1.28.0, v1.29.0, v1.30.0)
- **Time Span**: ~3 years of updates

---

## Release History Review

### v1.28.0 (April 17, 2022)
- **Time Since Last**: 2 months
- **Impact**: Likely grammar updates, bug fixes, new languages

### v1.29.0 (August 23, 2022)
- **Time Since Last**: 4 months
- **Impact**: Major release with potential API changes

### v1.30.0 (March 10, 2025)
- **Time Since Last**: 2.5 years (significant gap)
- **Impact**: Likely accumulated grammar improvements, new languages, security fixes

---

## Update Strategy

### Phase 1: Pre-Update Analysis (Research & Documentation)

#### 1.1 Review Release Notes
**Goal**: Understand what changed in each version

- [ ] Review v1.28.0 release notes and changelog
- [ ] Review v1.29.0 release notes and changelog
- [ ] Review v1.30.0 release notes and changelog
- [ ] Identify breaking changes in grammar definitions
- [ ] Identify new languages added
- [ ] Identify deprecated/removed features
- [ ] Document grammar improvements for existing languages

**Output**: `prismjs-changes-analysis.md`

#### 1.2 Identify Impact on C# Port
**Goal**: Determine which changes affect WebSpark.PrismSpark

- [ ] Compare grammar files for currently supported languages:
  - C, C++, C#, JavaScript, CSS, HTML, SQL, JSON
  - Python, Java, Bash, PowerShell, YAML, Go, Rust
  - Lua, Pug, Markdown, Razor, ASP.NET
- [ ] Check for token type changes
- [ ] Check for pattern/regex changes
- [ ] Identify new language features in supported languages

**Output**: `impact-assessment.md`

#### 1.3 Test Current State
**Goal**: Establish baseline before update

- [ ] Run full test suite (`dotnet test`)
- [ ] Document current test results
- [ ] Create test snapshots for comparison
- [ ] Verify all 24 supported languages work correctly

**Output**: Test baseline report

---

### Phase 2: Submodule Update (Git Operations)

#### 2.1 Update Submodule to v1.28.0
**Goal**: Incremental update to minimize risk

```bash
cd prismjs
git fetch origin
git checkout v1.28.0
cd ..
git add prismjs
git commit -m "chore: update prismjs submodule to v1.28.0"
```

#### 2.2 Review Changes at v1.28.0
- [ ] Check grammar files for supported languages
- [ ] Note any structural changes
- [ ] Document changes that need porting

#### 2.3 Update to v1.29.0

```bash
cd prismjs
git checkout v1.29.0
cd ..
git add prismjs
git commit -m "chore: update prismjs submodule to v1.29.0"
```

#### 2.4 Review Changes at v1.29.0
- [ ] Check for major API changes
- [ ] Review grammar modifications
- [ ] Document porting requirements

#### 2.5 Update to v1.30.0 (Final)

```bash
cd prismjs
git checkout v1.30.0
cd ..
git add prismjs
git commit -m "chore: update prismjs submodule to v1.30.0"
```

---

### Phase 3: Port Analysis (C# Implementation Review)

#### 3.1 Compare Grammar Definitions
**Goal**: Identify which C# grammars need updates

For each supported language:
- [ ] Compare old vs new JavaScript grammar
- [ ] Identify regex pattern changes
- [ ] Identify token type additions/removals
- [ ] Identify lookbehind/lookahead changes
- [ ] Document required C# updates

**Languages to check**:
```
Languages/CSharp.cs        → prismjs/components/prism-csharp.js
Languages/JavaScript.cs    → prismjs/components/prism-javascript.js
Languages/Markup.cs        → prismjs/components/prism-markup.js
Languages/Css.cs           → prismjs/components/prism-css.js
Languages/Python.cs        → prismjs/components/prism-python.js
Languages/Java.cs          → prismjs/components/prism-java.js
Languages/Sql.cs           → prismjs/components/prism-sql.js
Languages/Json.cs          → prismjs/components/prism-json.js
... (all 24 languages)
```

#### 3.2 Test Updated Grammars
- [ ] Run test suite after identifying changes
- [ ] Add new test cases for changed grammar rules
- [ ] Validate existing tests still pass
- [ ] Add regression tests for known issues fixed in PrismJS

---

### Phase 4: Implementation Updates (C# Code Changes)

#### 4.1 Update Language Grammar Files
**Goal**: Port JavaScript grammar changes to C#

- [ ] Update regex patterns to match new PrismJS definitions
- [ ] Add new token types
- [ ] Update token patterns
- [ ] Preserve C# idioms (nullable types, XML docs)
- [ ] Maintain constitution compliance

#### 4.2 Add New Keywords/Features
- [ ] Port new language keywords (e.g., C# 10+, Python 3.10+)
- [ ] Update built-in types
- [ ] Add new language features

#### 4.3 Update Tests
- [ ] Add test cases for new grammar features
- [ ] Update expected outputs for changed tokenization
- [ ] Ensure all 52 tests pass
- [ ] Add integration tests for changed behavior

#### 4.4 Update Documentation
- [ ] Update supported languages list if any added
- [ ] Document grammar version parity
- [ ] Add migration notes
- [ ] Update README if needed

---

### Phase 5: Validation & Testing

#### 5.1 Comprehensive Testing
- [ ] Run full MSTest suite: `dotnet test`
- [ ] Verify all 52 tests pass
- [ ] Manual testing with demo application
- [ ] Test all 24 languages with sample code
- [ ] Visual comparison of highlighting output

#### 5.2 Performance Testing
- [ ] Benchmark tokenization performance
- [ ] Compare with baseline (pre-update)
- [ ] Identify any performance regressions
- [ ] Optimize if needed

#### 5.3 Integration Testing
- [ ] Test in WebSpark.PrismSpark.Demo application
- [ ] Verify all demo pages work correctly
- [ ] Test Live Editor functionality
- [ ] Test Markdown Demo
- [ ] Test PUG Demo

---

### Phase 6: Documentation & Finalization

#### 6.1 Update Project Documentation
- [ ] Update `.documentation/docs/architecture.md` (if grammar changes significant)
- [ ] Document any breaking changes
- [ ] Update version compatibility notes
- [ ] Add changelog entry

#### 6.2 Commit Strategy
```bash
# Grammar updates
git commit -m "feat: update C# language grammar to PrismJS v1.30.0"
git commit -m "feat: update JavaScript grammar to PrismJS v1.30.0"
# ... one commit per language with significant changes

# Test updates
git commit -m "test: add test cases for updated grammars"

# Documentation
git commit -m "docs: document PrismJS v1.30.0 grammar updates"
```

#### 6.3 Create Archive of Session
- [ ] Archive analysis documents
- [ ] Archive comparison reports
- [ ] Document lessons learned
- [ ] Update constitution if patterns discovered

---

## Risk Mitigation

### Technical Risks

| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|------------|
| Breaking grammar changes | High | High | Incremental updates, thorough testing |
| Regex incompatibility (JS → C#) | Medium | Medium | Test each regex, use .NET equivalents |
| New language features not portable | Low | Medium | Document limitations, fallback handling |
| Performance degradation | Low | Medium | Benchmark before/after |
| Test failures | Medium | High | Fix incrementally, maintain baseline |

### Process Risks

| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|------------|
| Time estimation too low | Medium | Low | Break into phases, regular checkpoints |
| Incomplete change identification | Medium | High | Systematic grammar comparison |
| Documentation gaps | Low | Low | Document as you go |

---

## Success Criteria

- [ ] PrismJS submodule updated to v1.30.0
- [ ] All 52 MSTest tests pass
- [ ] All 24 supported languages tokenize correctly
- [ ] Demo application works without regressions
- [ ] Performance within 10% of baseline
- [ ] Documentation updated
- [ ] No breaking changes for existing users
- [ ] Grammar definitions match PrismJS v1.30.0 behavior

---

## Timeline Estimate

**Total Estimated Time**: 12-20 hours

- **Phase 1** (Analysis): 3-4 hours
- **Phase 2** (Submodule): 1 hour
- **Phase 3** (Port Analysis): 4-6 hours
- **Phase 4** (Implementation): 4-8 hours
- **Phase 5** (Testing): 2-3 hours
- **Phase 6** (Documentation): 1-2 hours

---

## Rollback Plan

If critical issues are discovered:

1. **Immediate**: Revert submodule to v1.27.0
   ```bash
   cd prismjs
   git checkout 4c3f196976b9177817da1f7d681efc6f8dd49b76
   cd ..
   git add prismjs
   git commit -m "revert: rollback prismjs to v1.27.0"
   ```

2. **Gradual**: Stop at intermediate version (v1.28.0 or v1.29.0)
   - Update documentation to reflect partial update
   - Document blockers for future updates

3. **Code Isolation**: Use feature flags for new grammar features
   - Allow gradual enablement
   - Enable per-language testing

---

## Next Steps

1. **Approve Plan**: Review and approve this plan
2. **Start Phase 1**: Begin with release notes review
3. **Create Tracking Issue**: GitHub issue for progress tracking
4. **Schedule Work**: Allocate time blocks for phases

---

## References

- [PrismJS Repository](https://github.com/PrismJS/prism)
- [PrismJS v1.28.0 Release](https://github.com/PrismJS/prism/releases/tag/v1.28.0)
- [PrismJS v1.29.0 Release](https://github.com/PrismJS/prism/releases/tag/v1.29.0)
- [PrismJS v1.30.0 Release](https://github.com/PrismJS/prism/releases/tag/v1.30.0)
- [Project Constitution](../../memory/constitution.md)

---

**Plan Version**: 1.1.0
**Status**: Completed (2026-02-10)

## Implementation Notes

### Changes Made
- **prismjs submodule**: Updated from v1.27.0 (commit `4c3f1969`) to v1.30.0 (commit `76dde18a`)
- **Bash.cs**: Updated `assign-left` for dotted variable names, added `parameter` token, added `cargo`/`java`/`sysctl` commands
- **Css.cs**: Fixed `atrule` pattern to handle quoted strings (e.g., `@import "file.css"`)
- **Java.cs**: Added `constant` token for `ALL_CAPS` identifiers
- **Markup.cs**: Fixed attribute value quote highlighting (leading/trailing only)
- **LanguageGrammars.cs**: Added `"sh"` alias for Bash grammar

### Impact Assessment (Actual)
- Only 4 of 24 language grammar files had upstream changes (low impact)
- 20 language grammars were unchanged between v1.27.0 and v1.30.0
- All 52 MSTest tests pass with no regressions
- Build succeeds with 0 errors
