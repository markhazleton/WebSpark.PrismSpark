# Archive

This directory contains historical development artifacts that are no longer actively used but preserved for reference.

## Contents

### transform.js
**Original Location**: Repository root  
**Moved**: 2026-02-10  
**Purpose**: WIP JavaScript script to automatically transform PrismJS language definitions to C# code  

**Historical Context**:
- Early development tool for porting PrismJS grammars to C#
- Requires Node.js and the prismjs submodule
- Generated C# code template: `PrismSharp.Core/Languages/{Language}GrammarDefinition.cs`
- Status: Incomplete/experimental - not actively used in current workflow

**Current Approach**:
- Language grammars are now manually authored in C#
- Located in `WebSpark.PrismSpark/Languages/`
- Provides better type safety and maintainability than auto-generated code

**If You Need To Use It**:
```bash
# Ensure prismjs submodule is initialized
git submodule update --init --recursive

# Install Node.js dependencies
cd prismjs
npm install

# Run transform script (requires modification for target language)
node ../transform.js
```

**Recommendation**: Do not use. Manual grammar authoring is the preferred approach.

---

**Retention Policy**: Keep indefinitely as historical reference. May be useful if future automation is considered.
