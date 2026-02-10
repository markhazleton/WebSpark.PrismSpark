using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebSpark.PrismSpark.Tests;

// TODO: create the class by source-generator
[TestClass]
public class LanguageTokenizeTest
{
    [TestMethod]
    public void test_CLike_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/clike/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.CLike, testFile);
    }

    [TestMethod]
    public void test_C_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/c/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.C, testFile);
    }

    [TestMethod]
    public void test_Cpp_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/cpp/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Cpp, testFile);
    }

    [DataTestMethod]
    [DataRow("csharp")]
    // [DataRow("csharp!+xml-doc")]
    public void test_CSharp_features_ok(string testCase)
    {
        var testFiles = Directory.GetFiles($"./testcases/{testCase}/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.CSharp, testFile);
    }

    [DataTestMethod]
    [DataRow("comment_feature")]
    [DataRow("page-directive_feature")]
    public void test_AspNet_features_ok(string testCase)
    {
        var testFile = $"./testcases/aspnet/{testCase}.test";
        TestHelper.RunTestCaseFromFile(LanguageGrammars.AspNet, testFile);
    }

    [DataTestMethod]
    [DataRow("directive_feature")]
    public void test_CSharp_AspNet_features_ok(string testCase)
    {
        var testFile = $"./testcases/csharp+aspnet/{testCase}.test";
        TestHelper.RunTestCaseFromFile(LanguageGrammars.AspNet, testFile);
    }


    [TestMethod]
    public void test_CSHtml_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/cshtml/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.CSHtml, testFile);
    }

    [TestMethod]
    public void test_Cil_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/cil/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Cil, testFile);
    }

    [TestMethod]
    public void test_JavaScript_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/javascript/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.JavaScript, testFile);
    }

    [TestMethod]
    public void test_Markup_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/markup/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Markup, testFile);
    }

    [DataTestMethod]
    [DataRow("markup!+css")]
    [DataRow("markup!+css+javascript")]
    [DataRow("markup!+javascript")]
    [DataRow("markup+javascript+csharp+aspnet")]
    public void test_Markup_inline_Others_features_ok(string testCase)
    {
        var testFiles = Directory.GetFiles($"./testcases/{testCase}/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Markup, testFile);
    }

    [TestMethod]
    public void test_RegExp_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/regex/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.RegExp, testFile);
    }

    [TestMethod]
    public void test_Sql_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/sql/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Sql, testFile);
    }

    [TestMethod]
    public void test_Json_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/json/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Json, testFile);
    }

    [TestMethod]
    public void test_PowerShell_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/powershell/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.PowerShell, testFile);
    }

    [TestMethod]
    public void test_Yaml_features_ok()
    {
        var testFiles = Directory.GetFiles("./testcases/yaml/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Yaml, testFile);
    }

    [DataTestMethod]
    [DataRow("css")]
    public void test_Css_all_features_ok(string testCase)
    {
        var testFiles = Directory.GetFiles($"./testcases/{testCase}/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Css, testFile);
    }

    [DataTestMethod]
    [DataRow("lua")]
    public void test_Lua_all_features_ok(string testCase)
    {
        var testFiles = Directory.GetFiles($"./testcases/{testCase}/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Lua, testFile);
    }

    [DataTestMethod]
    [DataRow("bash")]
    public void test_Bash_all_features_ok(string testCase)
    {
        var testFiles = Directory.GetFiles($"./testcases/{testCase}/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Bash, testFile);
    }

    [DataTestMethod]
    [DataRow("batch")]
    public void test_Batch_all_features_ok(string testCase)
    {
        var testFiles = Directory.GetFiles($"./testcases/{testCase}/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Batch, testFile);
    }

    [DataTestMethod]
    [DataRow("go")]
    public void test_Go_all_features_ok(string testCase)
    {
        var testFiles = Directory.GetFiles($"./testcases/{testCase}/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Go, testFile);
    }

    [DataTestMethod]
    [DataRow("rust")]
    public void test_Rust_all_features_ok(string testCase)
    {
        var testFiles = Directory.GetFiles($"./testcases/{testCase}/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Rust, testFile);
    }

    [DataTestMethod]
    [DataRow("python")]
    public void test_Python_all_features_ok(string testCase)
    {
        var testFiles = Directory.GetFiles($"./testcases/{testCase}/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Python, testFile);
    }

    [DataTestMethod]
    [DataRow("java")]
    public void test_Java_all_features_ok(string testCase)
    {
        var testFiles = Directory.GetFiles($"./testcases/{testCase}/", "*.test")
            .Where(testFile => !testFile.EndsWith(".html.test")).ToArray();
        Assert.IsTrue(testFiles.Length > 0);
        foreach (var testFile in testFiles)
            TestHelper.RunTestCaseFromFile(LanguageGrammars.Java, testFile);
    }

    [TestMethod]
    public void test_Pug_features_ok()
    {
        var pugCode = "//- A comment\ndoctype html\nhtml\n  head\n    title= pageTitle\n  body\n    h1 Pug - node template engine\n    #container.col\n      if youAreUsingPug\n        p You are amazing\n      else\n        p Get on it!";
        var grammar = LanguageGrammars.GetGrammar("pug");
        var tokens = Prism.Tokenize(pugCode, grammar);
        Assert.IsNotNull(tokens);
        Assert.IsTrue(tokens.Length > 0);
    }
}
