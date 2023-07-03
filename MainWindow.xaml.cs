using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace GoCompilerC3
{
    public partial class MainWindow : Window
    {
        private SyntaxAnalyzer analyzer;

        public MainWindow()
        {
            InitializeComponent();
            analyzer = new SyntaxAnalyzer();
        }

        private void OnAnalyzeClick(object sender, RoutedEventArgs e)
        {
            string code = CodeTextBox.Text;

            if (!string.IsNullOrWhiteSpace(code))
            {
                string result = analyzer.Analyze(code);
                ResultTextBlock.Text = result;
            }
            else
            {
                MessageBox.Show("Por favor, ingrese el código a analizar.");
            }
        }

        private void OnClearScreenClick(object sender, RoutedEventArgs e)
        {
            CodeTextBox.Text = string.Empty;
            ResultTextBlock.Text = string.Empty;
        }
    }

    public class SyntaxAnalyzer
    {
        private List<(string language, Regex pattern)> languagePatterns;

        public SyntaxAnalyzer()
        {
            languagePatterns = new List<(string, Regex)>();

            // Agrega aquí tus patrones de sintaxis para cada lenguaje
            // ...

            languagePatterns.Add(("Go", new Regex(@"^\s*package\s+main\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*import\s+""[\w\/]+""\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*func\s+[\w]+\s*\(.*?\)\s*[\w]+\s*\{\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^.*fmt\.Println\(.+\);\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*type\s+[\w]+\s+struct\s+\{\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*var\s+[\w]+\s+[\w]+\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*const\s+[\w]+\s+[\w]+\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*for\s+[\w]+\s*:=\s*range\s+[\w]+\s*\{\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*if\s+[\w]+\s*:=\s*[\w]+\s*;\s*[\w]+\s*\{\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*else\s*\{\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*switch\s+[\w]+\s*\{\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*case\s+[\w]+\s*:\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*default\s*:\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*func\s+[\w]+\s*\(.*?\)\s*(?:\(.*?\))?\s*\{\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*defer\s+[\w]+\s*\(\)\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*go\s+[\w]+\s*\(\)\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*return\s+.*\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*break\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*continue\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*select\s*\{\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*chan\s+(?:<-)?[\w]+\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*panic\s+.*\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*recover\s*\(\)\s*\{\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*package\s+main\s*[\r\n]+import\s+""fmt""\s*[\r\n]+func\s+main\s*\(\)\s*\{\s*fmt\.Println\(""Hola Mundo""\)\s*\}\s*$", RegexOptions.IgnoreCase)));
            // ...
        }

        public string Analyze(string code)
        {
            bool validSyntax = false;
            string validLanguage = "";

            foreach (var pattern in languagePatterns)
            {
                if (pattern.Item2.IsMatch(code))
                {
                    validSyntax = true;
                    validLanguage = pattern.language;
                    break;
                }
            }

            if (validSyntax)
            {
                return $"El código tiene sintaxis válida.\nLenguaje detectado: {validLanguage}\n\nCódigo ingresado:\n{code}";
            }
            else
            {
                return $"El código no tiene sintaxis válida.\n\nCódigo ingresado:\n{code}";
            }
        }
    }
}
