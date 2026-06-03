using System;
using System.Collections.Generic;

namespace BuscaTexto {
    class BuscaBoyerMoore {
        // Tabela de deslocamentos de caracteres para Bad Character Heuristic
        static int[] skip = new int[256];

        // Pré-processamento: Cria a tabela Skip.
        // A tabela vai dizer quantas posições o algoritmo pode pular (deslocar a janela) 
        // caso encontre um caractere específico no texto que causa mismatch.
        public static void InitSkip(String p) {
            int j, m = p.Length;
            for (j = 0; j < 256; j++)
                skip[j] = m; // Se o caractere não estiver no padrão, pula todo o comprimento da palavra
            for (j = 0; j < m; j++)
                skip[p[j]] = m - j - 1; // Registra o salto para os caracteres presentes
        }

        public static List<int> BMSearch(String p, String t) {
            List<int> posicoes = new List<int>();
            int m = p.Length, n = t.Length;
            if (m == 0 || n < m) return posicoes;

            // Inicializa a tabela de pulos baseada no padrão buscado
            InitSkip(p);
            
            // Começa as buscas pelo final da palavra padrão em direção ao começo
            int i = m - 1, j = m - 1;
            while (i < n) {
                int k = i;
                j = m - 1;
                // Compara caracteres do texto e padrão de trás para frente
                while (j >= 0 && k >= 0 && t[k] == p[j]) {
                    k--;
                    j--;
                }
                
                // Se j for menor que zero, o padrão inteiro casou
                if (j < 0) {
                    posicoes.Add(k + 1);
                    i += m; // Avança a janela pelo tamanho da string para continuar procurando outras ocorrências
                } else {
                    // Mismatch: Consulta a tabela para pular vários índices de uma vez, otimizando a busca
                    int a = skip[t[i]];
                    i += (m - j > a) ? (m - j) : a;
                }
            }
            return posicoes;
        }
    }
}
