# .NET 10.0 Upgrade Plan - WebSpark.PrismSpark

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
  - [WebSpark.PrismSpark](#websparkprismspark)
  - [WebSpark.PrismSpark.Tests](#websparkprismsparktests)
  - [WebSpark.PrismSpark.Demo](#websparkprismsparkdemo)
- [Risk Management](#risk-management)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario Description
Upgrade WebSpark.PrismSpark solution from .NET 9.0 to .NET 10.0 (Long Term Support).

### Scope
**Projects Affected**: 3 projects
- WebSpark.PrismSpark (Core library)
- WebSpark.PrismSpark.Tests (Test project)
- WebSpark.PrismSpark.Demo (ASP.NET Core demo application)

**Current State**: All projects targeting net9.0

**Target State**: All projects targeting net10.0

### Selected Strategy
**All-At-Once Strategy** - All projects upgraded simultaneously in single operation.

**Rationale**: 
- Small solution (3 projects)
- All currently on .NET 9.0
- Clear, simple dependency structure (1 leaf project → 2 dependants)
- All NuGet packages compatible with .NET 10.0
- Low complexity across all projects (🟢 Low difficulty rating)
- No security vulnerabilities
- Minimal API compatibility issues (1 source incompatibility, 6 behavioral changes in Demo only)

### Complexity Assessment

**Discovered Metrics**:
- Total Projects: 3
- Total LOC: 14,287
- Dependency Depth: 2 levels
- Circular Dependencies: None
- Security Vulnerabilities: 0
- Package Compatibility: 100% (8/8 packages compatible)
- API Compatibility: 99.9% (8,620 compatible, 1 source incompatible, 6 behavioral changes)

**Classification**: **Simple Solution**
- Small project count (≤5 projects)
- Shallow dependency tree (depth ≤2)
- No high-risk projects
- Excellent package compatibility
- Clear migration path

### Critical Issues
**None** - This is a clean upgrade with:
- ✅ All packages compatible
- ✅ No security vulnerabilities
- ✅ Minimal breaking changes
- ⚠️ 1 source incompatibility in Demo project (`System.TimeSpan.FromMinutes`)
- ⚠️ 6 behavioral changes in Demo project (require runtime testing)

### Recommended Approach
**All-At-Once Migration** with atomic upgrade operation:
1. Update all project files simultaneously
2. Verify build and dependencies
3. Address single source incompatibility
4. Test behavioral changes
5. Validate with full test suite

### Iteration Strategy
**Fast batch approach** (3 detail iterations):
- Simple solution characteristics enable efficient batching
- All projects can be specified together
- Focus on the Demo project's API compatibility issues
- Single comprehensive testing phase

---

## Migration Strategy

### Approach Selection: All-At-Once Strategy

**Selected Approach**: Upgrade all 3 projects simultaneously in a single coordinated operation.

**Justification**:

1. **Solution Size**: 3 projects (well within small solution threshold)
2. **Current State**: All projects on .NET 9.0 (homogeneous starting point)
3. **Dependency Simplicity**: Clean 2-level tree, no circular dependencies
4. **Package Compatibility**: 100% of packages (8/8) compatible with .NET 10.0
5. **Risk Profile**: All projects rated 🟢 Low difficulty
6. **Test Coverage**: Dedicated test project with comprehensive coverage (51 tests mentioned in project metadata)
7. **API Compatibility**: Minimal issues (99.9% compatible APIs)

**All-At-Once Strategy Rationale**:
- **Speed**: Fastest path to completion - single upgrade operation
- **Simplicity**: No multi-targeting complexity, no intermediate states
- **Clean Transition**: All projects benefit from .NET 10 LTS immediately
- **Coordination**: Small team can synchronize easily for testing
- **Low Risk**: Simple solution characteristics minimize all-at-once risks

### Dependency-Based Ordering Rationale

While All-At-Once upgrades all projects simultaneously, the logical dependency order informs our attention and validation priorities:

**Priority 1 - Core Library** (WebSpark.PrismSpark):
- Foundation for all other projects
- No dependencies means no external blockers
- Validate this builds cleanly first (conceptually, though all update together)

**Priority 2 - Dependent Projects** (Tests + Demo):
- Both depend only on core library
- Can validate in parallel after core library builds
- Focus attention on Demo project's API compatibility issues

**All-At-Once Ordering Principles**:
- All `TargetFramework` properties updated atomically
- All package references updated atomically
- Single `dotnet restore` and `dotnet build` for entire solution
- Breaking changes addressed in single coordinated fix session
- Tests run once after all changes applied

### Parallel vs Sequential Execution

**Parallel Execution Approach**:
- **Framework Updates**: All 3 project files updated in parallel (atomic batch edit)
- **Dependency Restore**: Single `dotnet restore` at solution level
- **Build**: Single `dotnet build` at solution level (projects build in dependency order automatically)
- **Testing**: Tests can run in parallel once solution builds

**Sequential Validation** (post-upgrade):
1. Verify core library builds (MSBuild handles dependency order)
2. Verify dependent projects build
3. Address Demo project's API compatibility issues
4. Run test suite
5. Manual validation of Demo application

**No Sequential Project Upgrades**: All-At-Once means no project-by-project iteration. The entire solution transitions together.

### Phase Definitions

**Phase 0: Preparation**
- Verify .NET 10 SDK installed
- Verify upgrade branch ready (upgrade-to-NET10)
- Review assessment findings

**Phase 1: Atomic Upgrade**
- Update all 3 project files to net10.0
- Restore solution dependencies
- Build entire solution
- Fix compilation errors (expected: 1 source incompatibility in Demo)
- Rebuild to verify

**Phase 2: Test Validation**
- Execute all tests in WebSpark.PrismSpark.Tests
- Validate test pass rate
- Address any test failures

**Phase 3: Behavioral Validation**
- Test Demo application for behavioral changes:
  - `System.Text.Json.JsonDocument` usage (4 occurrences)
  - `UseExceptionHandler` middleware behavior
  - Console logging behavior
- Document any runtime behavioral differences
- Ensure application functions correctly

**Phase 4: Completion**
- Final smoke test of Demo application
- Update release notes to reflect .NET 10 support
- Commit all changes to upgrade-to-NET10 branch

---

## Detailed Dependency Analysis

### Dependency Graph Summary

The solution has a clean, simple dependency structure with no circular dependencies:

```
WebSpark.PrismSpark (leaf - no dependencies)
├── WebSpark.PrismSpark.Tests (depends on core library)
└── WebSpark.PrismSpark.Demo (depends on core library)
```

**Characteristics**:
- **Leaf Node**: WebSpark.PrismSpark (core library with 0 dependencies, 2 dependants)
- **Consumer Projects**: Tests and Demo both depend only on the core library
- **Dependency Depth**: 2 levels (shallow)
- **Circular Dependencies**: None

### Project Groupings by Migration Phase

Given the All-At-Once strategy, all projects will be upgraded simultaneously in a single atomic operation. However, for logical organization:

**Phase 1: Foundation Library**
- WebSpark.PrismSpark.csproj (core library)
  - 0 project dependencies
  - 10,731 LOC
  - No API compatibility issues
  - 2 NuGet packages (both compatible)

**Phase 2: Dependent Projects** (upgraded simultaneously with Phase 1)
- WebSpark.PrismSpark.Tests.csproj
  - Depends on: WebSpark.PrismSpark
  - 735 LOC
  - No API compatibility issues
  - 4 NuGet packages (all compatible)

- WebSpark.PrismSpark.Demo.csproj
  - Depends on: WebSpark.PrismSpark
  - 2,821 LOC
  - 7 API compatibility issues (1 source incompatible, 6 behavioral changes)
  - 2 NuGet packages (both compatible)

### Critical Path Identification

**Critical Path**: WebSpark.PrismSpark → (Tests + Demo)

Since all projects upgrade simultaneously:
1. All project files updated to net10.0 in single operation
2. Solution restored and built together
3. Focus attention on Demo project's API compatibility issues
4. Test suite validates core library and all dependants

**No Blocking Dependencies**: The simple structure allows parallel validation after the atomic upgrade completes.

### Circular Dependency Details

**None** - The solution has a clean acyclic dependency graph.

---

## Project-by-Project Plans

### WebSpark.PrismSpark

**Current State**: 
- Target Framework: net9.0
- Project Type: ClassLibrary (SDK-style)
- Dependencies: 0 project dependencies, 2 NuGet packages
- LOC: 10,731
- Risk Level: 🟢 Low

**Target State**: 
- Target Framework: net10.0
- All packages compatible (no version changes required)

#### Migration Steps

1. **Prerequisites**
   - ✅ .NET 10 SDK installed
   - ✅ Upgrade branch created (upgrade-to-NET10)
   - ✅ All dependencies compatible with .NET 10

2. **Project File Update**
   - File: `WebSpark.PrismSpark\WebSpark.PrismSpark.csproj`
   - Change: Update `<TargetFrameworks>` from `net9.0` to `net10.0`
   - Note: Project uses `<TargetFrameworks>` (plural) despite single target

3. **Package Updates**

| Package | Current Version | Target Version | Reason |
|---------|-----------------|----------------|--------|
| Microsoft.SourceLink.GitHub | 8.0.0 | 8.0.0 | ✅ Compatible - no change needed |
| DotNet.ReproducibleBuilds | 1.2.25 | 1.2.25 | ✅ Compatible - no change needed |

4. **Expected Breaking Changes**
   - **None** - No API compatibility issues detected in this project

5. **Code Modifications**
   - **None required** - Core library has no API compatibility issues

6. **Testing Strategy**
   - Build project independently: `dotnet build WebSpark.PrismSpark\WebSpark.PrismSpark.csproj`
   - Verify no compilation errors
   - Verify no warnings
   - Tests executed via WebSpark.PrismSpark.Tests project

7. **Validation Checklist**
   - [ ] Project builds without errors
   - [ ] Project builds without warnings
   - [ ] No NuGet dependency conflicts
   - [ ] Dependent projects (Tests, Demo) can reference successfully
   - [ ] NuGet package metadata updated (release notes, target framework in description)

---

### WebSpark.PrismSpark.Tests

**Current State**: 
- Target Framework: net9.0
- Project Type: DotNetCoreApp (SDK-style)
- Dependencies: 1 (WebSpark.PrismSpark), 4 NuGet packages
- LOC: 735
- Risk Level: 🟢 Low

**Target State**: 
- Target Framework: net10.0
- All packages compatible (no version changes required)

#### Migration Steps

1. **Prerequisites**
   - ✅ WebSpark.PrismSpark upgraded to net10.0 (done in same atomic operation)
   - ✅ All test framework packages compatible

2. **Project File Update**
   - File: `WebSpark.PrismSpark.Tests\WebSpark.PrismSpark.Tests.csproj`
   - Change: Update `<TargetFramework>` from `net9.0` to `net10.0`

3. **Package Updates**

| Package | Current Version | Target Version | Reason |
|---------|-----------------|----------------|--------|
| Microsoft.NET.Test.Sdk | 17.14.1 | 17.14.1 | ✅ Compatible - no change needed |
| xunit | 2.9.3 | 2.9.3 | ✅ Compatible - no change needed |
| xunit.runner.visualstudio | 3.1.1 | 3.1.1 | ✅ Compatible - no change needed |
| coverlet.collector | 6.0.4 | 6.0.4 | ✅ Compatible - no change needed |

4. **Expected Breaking Changes**
   - **None** - No API compatibility issues detected

5. **Code Modifications**
   - **None required** - Test project has no API compatibility issues

6. **Testing Strategy**
   - Build project: `dotnet build WebSpark.PrismSpark.Tests\WebSpark.PrismSpark.Tests.csproj`
   - Execute all tests: `dotnet test WebSpark.PrismSpark.Tests\WebSpark.PrismSpark.Tests.csproj`
   - Verify all 51 tests pass (per project metadata)
   - Review test output for any warnings or behavioral changes

7. **Validation Checklist**
   - [ ] Project builds without errors
   - [ ] Project builds without warnings
   - [ ] All 51 tests execute successfully
   - [ ] No test failures or regressions
   - [ ] Test coverage maintained

---

### WebSpark.PrismSpark.Demo

**Current State**: 
- Target Framework: net9.0
- Project Type: AspNetCore (SDK-style)
- Dependencies: 1 (WebSpark.PrismSpark), 2 NuGet packages
- LOC: 2,821
- Risk Level: 🟡 Low-Medium (has API compatibility issues)

**Target State**: 
- Target Framework: net10.0
- All packages compatible (no version changes required)

#### Migration Steps

1. **Prerequisites**
   - ✅ WebSpark.PrismSpark upgraded to net10.0 (done in same atomic operation)
   - ✅ All packages compatible

2. **Project File Update**
   - File: `WebSpark.PrismSpark.Demo\WebSpark.PrismSpark.Demo.csproj`
   - Change: Update `<TargetFramework>` from `net9.0` to `net10.0`

3. **Package Updates**

| Package | Current Version | Target Version | Reason |
|---------|-----------------|----------------|--------|
| WebSpark.Bootswatch | 1.20.1 | 1.20.1 | ✅ Compatible - no change needed |
| WebSpark.HttpClientUtility | 1.1.0 | 1.1.0 | ✅ Compatible - no change needed |

4. **Expected Breaking Changes**

This project has **7 API compatibility issues** that require attention:

##### Source Incompatibility (Must Fix - Compilation Error)

**Issue 1: `System.TimeSpan.FromMinutes(Int64)` - Source Incompatible**
- **Location**: `Program.cs`, line 16
- **Current Code**: `options.IdleTimeout = TimeSpan.FromMinutes(30);`
- **Problem**: `TimeSpan.FromMinutes` in .NET 10 expects `double`, but integer literal `30` may cause ambiguity
- **Fix**: Cast to double explicitly: `TimeSpan.FromMinutes(30.0)` or `TimeSpan.FromMinutes((double)30)`
- **Reference**: [Breaking changes in .NET](https://go.microsoft.com/fwlink/?linkid=2262679)

##### Behavioral Changes (Require Testing - No Compilation Error)

**Issue 2-5: `System.Text.Json.JsonDocument` - Behavioral Change (4 occurrences)**
- **Locations**: 
  - `Services\CodeHighlightingService.cs`, line 96 (2 occurrences)
  - `Controllers\HomeController.cs`, line 459 (2 occurrences)
- **Current Code**: `JsonDocument.Parse(jsonCode)` / `System.Text.Json.JsonDocument.Parse(jsonCode)`
- **Problem**: Behavioral changes in JSON parsing in .NET 10
- **Action Required**: 
  - No code changes needed
  - Test JSON parsing scenarios thoroughly
  - Verify edge cases (malformed JSON, large documents, special characters)
  - Compare behavior with .NET 9 if discrepancies observed

**Issue 6: `UseExceptionHandler` Middleware - Behavioral Change**
- **Location**: `Program.cs`, line 57
- **Current Code**: `app.UseExceptionHandler("/Home/Error");`
- **Problem**: Exception handling behavior changed in .NET 10
- **Action Required**:
  - No code changes needed
  - Test exception scenarios (404, 500, unhandled exceptions)
  - Verify error page displays correctly
  - Check exception logging behavior

**Issue 7: Console Logging - Behavioral Change**
- **Location**: `Program.cs`, line 44
- **Current Code**: `builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);`
- **Problem**: Console logger behavior changed in .NET 10
- **Action Required**:
  - No code changes needed
  - Verify log output format and content
  - Check that Debug-level logs still appear
  - Validate log filtering behavior

5. **Code Modifications**

**Required Changes**:
1. **Program.cs, line 16**: Fix `TimeSpan.FromMinutes` source incompatibility
   ```csharp
   // Before
   options.IdleTimeout = TimeSpan.FromMinutes(30);
   
   // After
   options.IdleTimeout = TimeSpan.FromMinutes(30.0);
   ```

**Testing Changes** (no code modification, runtime verification):
- JSON parsing in `CodeHighlightingService.cs` and `HomeController.cs`
- Exception handling middleware in `Program.cs`
- Console logging configuration in `Program.cs`

---

## Risk Management

### High-Level Risk Assessment

**Overall Risk Level**: 🟢 **Low**

This upgrade presents minimal risk due to:
- Small solution size (3 projects)
- Excellent package compatibility (100%)
- No security vulnerabilities
- Comprehensive test coverage
- Modern .NET baseline (9.0 → 10.0, not legacy framework)

### Risk Breakdown by Category

| Risk Category | Level | Description | Mitigation |
|---------------|-------|-------------|------------|
| **Dependency Conflicts** | 🟢 Low | All 8 packages compatible with .NET 10 | None required - compatibility verified |
| **Breaking API Changes** | 🟡 Low-Medium | 1 source incompatibility, 6 behavioral changes | Address during compilation; test behavioral changes |
| **Build Failures** | 🟢 Low | Simple project structure, SDK-style projects | Standard build troubleshooting |
| **Test Failures** | 🟢 Low | 51 tests, no API issues in library code | Run full test suite, address failures |
| **Runtime Behavior** | 🟡 Low-Medium | 6 behavioral changes in Demo | Manual testing of Demo scenarios |
| **Rollback Complexity** | 🟢 Low | Clean branch, simple revert path | Git branch isolation |

### Project-Specific Risks

#### WebSpark.PrismSpark (Core Library)
- **Risk Level**: 🟢 Very Low
- **Issues**: None detected
- **Mitigation**: Standard build verification

#### WebSpark.PrismSpark.Tests
- **Risk Level**: 🟢 Very Low  
- **Issues**: None detected
- **Mitigation**: Execute full test suite

#### WebSpark.PrismSpark.Demo
- **Risk Level**: 🟡 Low-Medium
- **Issues**: 7 API compatibility concerns
  - 1 source incompatibility: `System.TimeSpan.FromMinutes(Int64)` 
  - 6 behavioral changes affecting JSON handling, exception middleware, logging
- **Mitigation**: 
  - Fix compilation error during build phase
  - Thorough manual testing of Demo application
  - Verify JSON serialization scenarios
  - Test exception handling behavior
  - Validate logging output

### Detailed Risk Mitigation

#### Risk 1: Source Incompatibility in Demo Project

**Risk**: `TimeSpan.FromMinutes(Int64)` causes compilation error
- **Impact**: High (blocks build)
- **Likelihood**: High (confirmed by assessment)
- **Mitigation**: 
  - Fix identified in project plan: Change `TimeSpan.FromMinutes(30)` to `TimeSpan.FromMinutes(30.0)`
  - Simple, well-understood fix
  - Verify no other similar patterns exist
- **Contingency**: If additional overload ambiguities exist, explicitly cast all integer literals to double

#### Risk 2: JSON Parsing Behavioral Changes

**Risk**: `JsonDocument.Parse` behavior changes (4 occurrences)
- **Impact**: Medium (could affect data processing)
- **Likelihood**: Low (breaking changes are rare, likely minor)
- **Mitigation**:
  - Comprehensive JSON parsing tests (valid, malformed, large, special characters)
  - Compare output with .NET 9 version (if available)
  - Review .NET 10 release notes for specific JSON changes
- **Contingency**: 
  - If behavior is problematic, consider using `JsonSerializerOptions` to control parsing
  - Update code to handle new behavior explicitly

#### Risk 3: Exception Handling Middleware Changes

**Risk**: `UseExceptionHandler` behavior changed
- **Impact**: Medium (affects error handling)
- **Likelihood**: Low (exception middleware is stable)
- **Mitigation**:
  - Test 404, 500, and unhandled exception scenarios
  - Verify error page renders correctly
  - Check exception logging
- **Contingency**: 
  - If middleware behavior is problematic, review new middleware options
  - Update middleware configuration to match desired behavior

#### Risk 4: Console Logging Changes

**Risk**: Console logger behavior changed
- **Impact**: Low (affects logging output only)
- **Likelihood**: Low (console logging is stable)
- **Mitigation**:
  - Verify log output during manual testing
  - Check Debug-level logs appear as expected
- **Contingency**: 
  - Adjust logging configuration if needed
  - Use `appsettings.json` to fine-tune logging levels

#### Risk 5: Undocumented Breaking Changes

**Risk**: Unexpected breaking changes not detected by assessment
- **Impact**: Variable (depends on change)
- **Likelihood**: Very Low (assessment is comprehensive)
- **Mitigation**:
  - Thorough testing after upgrade
  - Review .NET 10 breaking changes documentation
  - Monitor application logs for warnings/errors
- **Contingency**: 
  - Research specific breaking change
  - Apply fix based on Microsoft documentation
  - Consult .NET upgrade guides if needed

#### Risk 6: Test Failures

**Risk**: Tests fail after upgrade
- **Impact**: Medium (requires investigation and fixes)
- **Likelihood**: Low (no API issues detected in library code)
- **Mitigation**:
  - Run full test suite immediately after build succeeds
  - Investigate any failures promptly
  - Verify test framework compatibility
- **Contingency**: 
  - If test failures occur, determine root cause:
    - API behavior change → update code
    - Test expectation change → update test
    - Test framework issue → update test framework
  - Ensure all tests pass before proceeding

#### Risk 7: Package Compatibility Issues

**Risk**: Runtime package incompatibilities despite build success
- **Impact**: Medium (runtime errors)
- **Likelihood**: Very Low (all packages marked compatible)
- **Mitigation**:
  - Package compatibility pre-verified by assessment
  - Test application runtime behavior thoroughly
- **Contingency**: 
  - If runtime errors occur, check for package updates
  - Review package release notes for .NET 10 compatibility
  - Update specific packages if needed

### Rollback Strategy

**If Upgrade Fails**:
1. **Assess Impact**: Determine if issues are resolvable
2. **Decision Point**: Fix forward vs. rollback
3. **Rollback Procedure**:
   - Discard changes on upgrade-to-NET10 branch: `git reset --hard origin/main`
   - OR switch back to main branch: `git checkout main`
   - Repository remains in pre-upgrade state
   - No impact on main branch

**Rollback Risk**: 🟢 Very Low
- Clean branch isolation
- No changes to main branch
- Simple git operations
- No dependency on external systems

### Risk Monitoring

**During Upgrade**:
- Monitor build output for unexpected warnings
- Watch for deprecation notices
- Review detailed test results
- Check application logs for errors

**Post-Upgrade**:
- Monitor Demo application runtime behavior
- Collect user feedback (if applicable)
- Watch for performance changes
- Review error logs regularly

### Risk Acceptance

**Accepted Risks**:
- **Minor behavioral changes**: Some behavioral changes are expected and acceptable if functionality remains correct
- **Learning curve**: Team may need time to adapt to .NET 10 features/changes
- **Documentation lag**: Some .NET 10 features may have limited documentation initially

**Risk Threshold**: This upgrade has been deemed acceptable because:
- All identified risks are Low or Low-Medium
- Mitigation strategies are clear and achievable
- Rollback path is simple and reliable
- Benefits of .NET 10 LTS outweigh risks

---

## Testing & Validation Strategy

### Multi-Level Testing Approach

#### Phase 1: Build Validation (Atomic Upgrade)

**Objective**: Verify all projects compile successfully after framework and package updates.

**Steps**:
1. Clean solution: `dotnet clean WebSpark.PrismSpark.sln`
2. Restore dependencies: `dotnet restore WebSpark.PrismSpark.sln`
3. Build entire solution: `dotnet build WebSpark.PrismSpark.sln --configuration Release`
4. Verify 0 errors, 0 warnings

**Expected Outcome**:
- ✅ All 3 projects build successfully
- ✅ Single source incompatibility fixed (`TimeSpan.FromMinutes`)
- ✅ No dependency conflicts
- ✅ No unexpected compilation errors

**If Build Fails**:
- Review error messages
- Check for additional source incompatibilities
- Verify package restore completed successfully
- Consult breaking changes documentation

---

#### Phase 2: Automated Test Validation

**Objective**: Verify core library functionality through comprehensive test suite.

**Test Project**: WebSpark.PrismSpark.Tests

**Steps**:
1. Execute test suite: `dotnet test WebSpark.PrismSpark.Tests\WebSpark.PrismSpark.Tests.csproj --configuration Release`
2. Review test results (expect: 51 tests)
3. Investigate any failures
4. Verify test framework compatibility
5. Ensure no warnings or deprecation notices

**Success Criteria**:
- ✅ All 51 tests pass
- ✅ No new test failures introduced
- ✅ Test execution time comparable to .NET 9 baseline
- ✅ No warnings or deprecation notices

**If Tests Fail**:
- Identify failed test(s)
- Determine if failure is due to:
  - Core library API changes
  - Test framework behavior changes
  - Actual regression
- Fix root cause or update test expectations if behavior change is expected
- Re-run tests to verify fix

---

#### Phase 3: Behavioral Validation (Demo Application)

**Objective**: Verify runtime behavior changes don't negatively impact application.

**Test Application**: WebSpark.PrismSpark.Demo

##### 3.1 JSON Parsing Tests

**Areas Affected**:
- `Services\CodeHighlightingService.cs` (line 96)
- `Controllers\HomeController.cs` (line 459)

**Test Scenarios**:
1. **Valid JSON**: Submit well-formed JSON code for highlighting
2. **Malformed JSON**: Test with syntax errors (missing braces, trailing commas)
3. **Large JSON**: Test with large JSON documents (>10KB)
4. **Special Characters**: JSON with unicode, escaped characters
5. **Edge Cases**: Empty JSON, nested objects, arrays

**Validation**:
- Compare output with .NET 9 version (if available)
- Verify no exceptions thrown
- Verify highlighting renders correctly
- Check for any performance differences

##### 3.2 Exception Handling Tests

**Area Affected**:
- `Program.cs` (line 57) - `UseExceptionHandler` middleware

**Test Scenarios**:
1. **404 Not Found**: Navigate to invalid route (e.g., `/nonexistent`)
2. **500 Internal Server Error**: Trigger unhandled exception (if test endpoint exists)
3. **Error Page Display**: Verify `/Home/Error` page renders correctly
4. **Exception Logging**: Check that exceptions are logged appropriately

**Validation**:
- Error pages display correctly
- Status codes are correct
- Exception details logged (in Development mode)
- User-friendly error messages (in Production mode)

##### 3.3 Console Logging Tests

**Area Affected**:
- `Program.cs` (line 44) - `AddConsole().SetMinimumLevel(LogLevel.Debug)`

**Test Scenarios**:
1. **Application Startup**: Verify startup logs appear
2. **Debug-Level Logs**: Confirm Debug messages are output
3. **Log Format**: Check log structure and readability
4. **Log Filtering**: Verify minimum level filtering works

**Validation**:
- Console output contains expected log entries
- Log format is readable
- Debug-level logs visible
- No excessive or missing log entries

##### 3.4 General Functional Tests

**Test Scenarios**:
1. **Home Page**: Verify home page loads
2. **Language Selection**: Test all 20+ supported languages
3. **Code Highlighting**: Submit code samples for each language
4. **UI Rendering**: Verify CSS styles apply correctly
5. **Navigation**: Test all routes and pages

**Validation**:
- All pages load without errors
- Syntax highlighting works for all languages
- UI looks correct (no layout issues)
- Navigation works smoothly

---

#### Phase 4: Smoke Testing

**Objective**: Final end-to-end validation before completion.

**Steps**:
1. Start Demo application: `dotnet run --project WebSpark.PrismSpark.Demo\WebSpark.PrismSpark.Demo.csproj`
2. Navigate to application in browser (typically `https://localhost:5001`)
3. Perform quick smoke test:
   - Load home page
   - Test 3-5 different language highlighters
   - Submit sample code
   - Verify output renders
4. Review console logs for errors/warnings
5. Stop application gracefully

**Success Criteria**:
- ✅ Application starts without errors
- ✅ UI loads and is functional
- ✅ Core functionality works (syntax highlighting)
- ✅ No console errors or warnings
- ✅ Application stops cleanly

---

### Testing Matrix Summary

| Test Phase | Scope | Automated | Manual | Pass Criteria |
|------------|-------|-----------|--------|---------------|
| Build Validation | All projects | ✅ | ❌ | 0 errors, 0 warnings |
| Automated Tests | Core library | ✅ | ❌ | 51/51 tests pass |
| JSON Parsing | Demo app | ❌ | ✅ | Correct behavior, no exceptions |
| Exception Handling | Demo app | ❌ | ✅ | Error pages work, logging correct |
| Console Logging | Demo app | ❌ | ✅ | Logs appear, correct format |
| General Functional | Demo app | ❌ | ✅ | All features work |
| Smoke Test | End-to-end | ❌ | ✅ | Application fully functional |

---

### Testing Timeline

**Estimated Testing Time** (relative complexity):
- Build Validation: Low (minutes)
- Automated Tests: Low (minutes)
- Behavioral Validation: Medium (requires thorough manual testing)
- Smoke Testing: Low (quick verification)

**Total Testing Effort**: Low-Medium complexity

---

## Source Control Strategy

### Branching Strategy

**Branch Structure**:
- **Main Branch**: `main` (stable, .NET 9.0 codebase)
- **Upgrade Branch**: `upgrade-to-NET10` (isolated upgrade work)
- **Target Branch**: `main` (after successful merge)

**Branch Isolation Benefits**:
- Main branch remains stable during upgrade
- Easy rollback if issues arise
- Clear separation of .NET 9 and .NET 10 code
- Safe experimentation and testing

### Commit Strategy

**All-At-Once Commit Approach** (Recommended):

Given the small solution size and atomic nature of the upgrade, prefer a single comprehensive commit:

**Single Commit Structure**:
```
git add .
git commit -m "Upgrade solution to .NET 10.0

- Update all 3 projects from net9.0 to net10.0
- Fix TimeSpan.FromMinutes source incompatibility in Demo project
- Verify all 51 tests pass
- Validate behavioral changes (JSON parsing, exception handling, logging)
- All packages compatible with .NET 10.0 (no version changes required)

Projects upgraded:
- WebSpark.PrismSpark (core library)
- WebSpark.PrismSpark.Tests (test project)  
- WebSpark.PrismSpark.Demo (demo application)

Breaking changes addressed:
- Program.cs: TimeSpan.FromMinutes(30) → TimeSpan.FromMinutes(30.0)

Tested:
- All 51 automated tests pass
- JSON parsing validated in Demo
- Exception handling validated
- Console logging validated
- Full smoke test completed successfully"
```
**Rationale for Single Commit**:
- Atomic upgrade (all projects change together)
- Simple solution (3 projects)
- Single source incompatibility fix
- All changes directly related
- Easier to revert if needed (single atomic unit)
- Cleaner git history

**Alternative: Multi-Commit Approach** (If Preferred):

If you prefer granular commits for better traceability:

1. **Commit 1: Framework Update**
   ```
   git add *.csproj
   git commit -m "Update all projects to .NET 10.0 target framework"
   ```

2. **Commit 2: Breaking Changes Fix**
   ```
   git add WebSpark.PrismSpark.Demo/Program.cs
   git commit -m "Fix TimeSpan.FromMinutes source incompatibility for .NET 10"
   ```

3. **Commit 3: Documentation Updates** (if applicable)
   ```
   git add README.md WebSpark.PrismSpark/WebSpark.PrismSpark.csproj
   git commit -m "Update documentation and release notes for .NET 10 support"
   ```

### Commit Message Best Practices

**Format**:
```
<type>: <subject>

<body>

<footer>
```

**Example**:
```
chore: Upgrade to .NET 10.0 LTS

- All 3 projects upgraded from net9.0 to net10.0
- Fixed TimeSpan.FromMinutes source incompatibility
- All tests passing (51/51)
- Behavioral changes validated

Refs: #<issue-number> (if applicable)
```

### Review and Merge Process

**Pull Request Requirements**:

1. **PR Title**: "Upgrade to .NET 10.0 LTS"

2. **PR Description Template**:
   ```markdown
   ## Upgrade Summary
   - **From**: .NET 9.0
   - **To**: .NET 10.0 (LTS)
   - **Projects**: 3 (all upgraded)
   - **Breaking Changes**: 1 fixed
   
   ## Changes
   - [x] Updated target framework in all project files
   - [x] Fixed TimeSpan.FromMinutes compilation error
   - [x] All tests passing (51/51)
   - [x] Behavioral changes validated
   - [x] Documentation updated
   
   ## Testing Completed
   - [x] Build validation (all projects)
   - [x] Automated test suite (51 tests)
   - [x] JSON parsing tests
   - [x] Exception handling tests
   - [x] Console logging tests
   - [x] Smoke test (Demo application)
   
   ## Breaking Changes Addressed
   - **Program.cs**: `TimeSpan.FromMinutes(30)` → `TimeSpan.FromMinutes(30.0)`
   
   ## Behavioral Changes Tested
   - JsonDocument.Parse (4 occurrences) - ✅ Validated
   - UseExceptionHandler middleware - ✅ Validated
   - Console logging - ✅ Validated
   
   ## Checklist
   - [x] All projects build without errors
   - [x] All tests pass
   - [x] Breaking changes documented
   - [x] Manual testing completed
   - [x] Code review completed
   - [x] Documentation updated
   - [x] Release notes updated
   ```

3. **PR Checklist**:
   - [ ] All projects build successfully
   - [ ] All tests pass
   - [ ] Breaking changes documented
   - [ ] Manual testing completed
   - [ ] Code review completed
   - [ ] Documentation updated
   - [ ] Release notes updated

### Merge Strategy

**Recommended Approach**: Squash and merge (for clean main branch history)

**Merge Command** (if merging locally):
```bash
git checkout main
git merge --squash upgrade-to-NET10
git commit -m "Upgrade to .NET 10.0 LTS

<comprehensive commit message>"
git push origin main
```

**Alternative**: Create Pull Request on GitHub for team review

### Post-Merge Actions

After successful merge to main:

1. **Tag Release** (if applicable):
   ```bash
   git tag -a v1.1.0 -m "Version 1.1.0 - .NET 10.0 support"
   git push origin v1.1.0
   ```

2. **Update Release Notes**: Document .NET 10 support in changelog

3. **Publish NuGet Package** (if applicable):
   - Update package version
   - Build release package
   - Publish to NuGet.org with .NET 10 support noted

4. **Clean Up Branch**:
   ```bash
   git branch -d upgrade-to-NET10  # local
   git push origin --delete upgrade-to-NET10  # remote (if pushed)
   ```

### Branch Protection

**If Repository Uses Branch Protection**:
- Ensure all CI/CD checks pass
- Require code review approval
- Ensure all tests pass in CI pipeline
- Update status checks if needed for .NET 10 SDK

### Commit Hygiene

**DO**:
- ✅ Write clear, descriptive commit messages
- ✅ Reference issue numbers if applicable
- ✅ Group related changes together
- ✅ Include "why" in commit body, not just "what"
- ✅ Keep commits atomic and logical

**DON'T**:
- ❌ Commit unrelated changes
- ❌ Use vague messages like "fix stuff" or "updates"
- ❌ Commit commented-out code or debug statements
- ❌ Mix whitespace changes with functional changes
- ❌ Commit broken code (always ensure builds succeed)

---

## Success Criteria

### Technical Criteria

The migration is complete when all of the following conditions are met:

#### 1. Target Framework Compliance
- [x] All 3 projects target net10.0
  - [ ] `WebSpark.PrismSpark.csproj`: `<TargetFrameworks>net10.0</TargetFrameworks>`
  - [ ] `WebSpark.PrismSpark.Tests.csproj`: `<TargetFramework>net10.0</TargetFramework>`
  - [ ] `WebSpark.PrismSpark.Demo.csproj`: `<TargetFramework>net10.0</TargetFramework>`

#### 2. Build Success
- [x] Solution builds without errors
  - [ ] `dotnet build WebSpark.PrismSpark.sln --configuration Release` returns exit code 0
  - [ ] 0 compilation errors across all projects
  - [ ] 0 warnings (or documented/accepted warnings only)

#### 3. Package Compatibility
- [x] All NuGet packages restored successfully
  - [ ] `dotnet restore WebSpark.PrismSpark.sln` completes without errors
  - [ ] No package dependency conflicts
  - [ ] All 8 packages compatible with .NET 10.0

#### 4. Breaking Changes Addressed
- [x] Source incompatibility fixed
  - [ ] `TimeSpan.FromMinutes` compilation error resolved in Demo/Program.cs

#### 5. Test Success
- [x] All automated tests pass
  - [ ] WebSpark.PrismSpark.Tests: 51/51 tests pass
  - [ ] `dotnet test` returns exit code 0
  - [ ] No test failures or regressions
  - [ ] Test execution completes successfully

#### 6. Behavioral Validation
- [x] Behavioral changes tested and validated
  - [ ] JSON parsing tested (JsonDocument.Parse - 4 occurrences)
  - [ ] Exception handling tested (UseExceptionHandler)
  - [ ] Console logging tested (AddConsole)
  - [ ] No functional regressions observed

#### 7. Application Functionality
- [x] Demo application runs successfully
  - [ ] Application starts without errors
  - [ ] All pages load correctly
  - [ ] Syntax highlighting works for all languages
  - [ ] No runtime exceptions
  - [ ] Application stops cleanly

#### 8. Security Compliance
- [x] No security vulnerabilities remain
  - [ ] All packages free of known vulnerabilities
  - [ ] No new security warnings introduced

---

### Quality Criteria

#### 1. Code Quality Maintained
- [ ] No code quality degradation
- [ ] No new code smells introduced
- [ ] Coding standards maintained
- [ ] No commented-out code left behind

#### 2. Test Coverage Maintained
- [ ] Test coverage remains at or above baseline
- [ ] All existing tests updated if needed
- [ ] No tests disabled or skipped without justification

#### 3. Documentation Updated
- [ ] README.md updated with .NET 10.0 support (if applicable)
- [ ] Package release notes updated in project file
- [ ] API documentation regenerated (if public library)
- [ ] Breaking changes documented

#### 4. Performance Acceptable
- [ ] Application startup time comparable to .NET 9.0
- [ ] Test execution time comparable to .NET 9.0
- [ ] No significant performance regressions
- [ ] Memory usage acceptable

---

### Process Criteria

#### 1. All-At-Once Strategy Followed
- [ ] All projects upgraded simultaneously (atomic operation)
- [ ] No intermediate states created
- [ ] All framework updates in single coordinated batch
- [ ] All breaking changes fixed together

#### 2. Source Control Strategy Followed
- [ ] All changes committed to upgrade-to-NET10 branch
- [ ] Commit messages clear and descriptive
- [ ] Pull request created with comprehensive description
- [ ] Code review completed (if applicable)

#### 3. Testing Strategy Executed
- [ ] Build validation completed
- [ ] Automated test suite executed
- [ ] Behavioral validation completed
- [ ] Smoke testing completed
- [ ] All test phases passed

#### 4. Risk Management Applied
- [ ] All identified risks addressed
- [ ] Mitigation strategies applied
- [ ] No unresolved blocking issues
- [ ] Rollback plan understood and ready if needed

---

### Definition of Done

**The .NET 10.0 upgrade is DONE when**:

1. ✅ All 3 projects target .NET 10.0
2. ✅ Solution builds with 0 errors and 0 warnings
3. ✅ All 51 tests pass
4. ✅ Demo application runs successfully with no functional issues
5. ✅ All behavioral changes tested and validated
6. ✅ Source incompatibility fixed (TimeSpan.FromMinutes)
7. ✅ All packages compatible and restored
8. ✅ No security vulnerabilities
9. ✅ Documentation updated (README, release notes)
10. ✅ Changes committed and reviewed
11. ✅ All acceptance gates passed
12. ✅ Ready to merge to main branch

**At this point**, the upgrade-to-NET10 branch can be merged to main, and the WebSpark.PrismSpark solution is successfully running on .NET 10.0 LTS.
