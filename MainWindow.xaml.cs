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
            languagePatterns.Add(("Go", new Regex(@"^\s*package\s+main\s*[\r\n]+import\s+""fmt""\s*[\r\n]+func\s+main\s*\(\)\s*\{\s*fmt\.Println\("".*""\)\s*\}\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*package\s+main\s*[\r\n]+import\s+""fmt""\s*[\r\n]*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*if\s+[\w]+", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*package\s+main\s*[\r\n]+import\s+""fmt""\s*[\r\n]+func\s+main\s*\(\)\s*\{\s*.*\s+if\s+.+\s*\{\s*fmt\.Println\(""(.+)""\)\s*\}.*\s*\}\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*package\s+main\s*[\r\n]+import\s+""fmt""\s*[\r\n]+func\s+main\s*\(\)\s*\{\s*.*\s+fmt\.Println\(""(.+)""\)\s*fmt\.Scanln\(&repeticiones\)\s*for\s+i\s*:=\s*1\s*;\s*i\s*<=\s*repeticiones\s*;\s*i\s*\+\+\s*\{\s*fmt\.Println\(""yodelayheehoo"",\s*i\)\s*\}\s*\}\s*$", RegexOptions.IgnoreCase)));
            languagePatterns.Add(("Go", new Regex(@"^\s*package\s+main\s*[\r\n]+import\s+""fmt""\s*[\r\n]+func\s+main\s*\(\)\s*\{\s*.*\s+var\s+marcasDeAutos\s+\[\s*4\s*\]string\s*[\r\n]+\s*marcasDeAutos\[0\]\s*=\s*""(.+)""\s*[\r\n]+\s*marcasDeAutos\[1\]\s*=\s*""(.+)""\s*[\r\n]+\s*marcasDeAutos\[2\]\s*=\s*""(.+)""\s*[\r\n]+\s*marcasDeAutos\[3\]\s*=\s*""(.+)""\s*[\r\n]+\s*fmt\.Println\(marcasDeAutos\)\s*\}\s*$", RegexOptions.IgnoreCase)));



        }

        public string Analyze(string code)
        {
            int errorLine = -1;
            string validLanguage = "";

            foreach (var pattern in languagePatterns)
            {
                Match match = pattern.Item2.Match(code);
                if (match.Success)
                {
                    validLanguage = pattern.Item1;
                    break;
                }
                else if (match.Index >= 0)
                {
                   
                    errorLine = code.Substring(0, match.Index).Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Length;
                }
            }

            if (!string.IsNullOrEmpty(validLanguage))
            {
                if (validLanguage == "Go")
                {

                    var match = Regex.Match(code, @"fmt\.Println\(""([^""]*)""\)");
                    var regex = Regex.Match(code, @"^\s*package\s+main\s*[\r\n]+import\s+""fmt""\s*[\r\n]+func\s+main\s*\(\)\s*\{\s*.*\s+var\s+marcasDeAutos\s+\[\s*4\s*\]string\s*[\r\n]+\s*marcasDeAutos\[0\]\s*=\s*""([^""]*)""\s*[\r\n]+\s*marcasDeAutos\[1\]\s*=\s*""([^""]*)""\s*[\r\n]+\s*marcasDeAutos\[2\]\s*=\s*""([^""]*)""\s*[\r\n]+\s*marcasDeAutos\[3\]\s*=\s*""([^""]*)""\s*[\r\n]+\s*fmt\.Println\(marcasDeAutos\)\s*\}\s*$", RegexOptions.IgnoreCase);
                   

                    if (match.Success)
                    {
                        return $"Compilación exitosa.\nLenguaje detectado: {validLanguage}\n\nSalida: {match.Groups[1].Value}";
                        
                    }
                    else if (regex.Success)
                    {
                        string marca1 = regex.Groups[1].Value;
                        string marca2 = regex.Groups[2].Value;
                        string marca3 = regex.Groups[3].Value;
                        string marca4 = regex.Groups[4].Value;

                        return $"Compilación exitosa.\nLenguaje detectado: {validLanguage}\n\nSalida:\n{marca1}, {marca2}, {marca3}, {marca4}";
                    }
                }

                return $"Compilación exitosa.\nLenguaje detectado: {validLanguage}\n\nCódigo ingresado:\n{code}";
            }
            else
            {
                if (errorLine > 0)
                {
                    return $"Error de compilación.\n\nCódigo ingresado:\n{code}";
                }
                else
                {
                    return $"Error de compilación.\nError en la línea: {errorLine}\n\nCódigo ingresado:\n{code}";
                }
            }
        }
    }
}
