using System;
using System.Collections.Generic;

namespace BuscaTexto {
    class BuscaBoyerMoore {
        static int[] skip = new int[256];

        public static void InitSkip(String p) {
            int j, m = p.Length;
            for (j = 0; j < 256; j++)
                skip[j] = m;
            for (j = 0; j < m; j++)
                skip[p[j]] = m - j - 1;
        }

        public static List<int> BMSearch(String p, String t) {
            List<int> posicoes = new List<int>();
            int m = p.Length, n = t.Length;
            if (m == 0 || n < m) return posicoes;

            InitSkip(p);
            int i = m - 1, j = m - 1;
            while (i < n) {
                int k = i;
                j = m - 1;
                while (j >= 0 && k >= 0 && t[k] == p[j]) {
                    k--;
                    j--;
                }
                if (j < 0) {
                    posicoes.Add(k + 1);
                    i += m; // Continue searching
                } else {
                    int a = skip[t[i]];
                    i += (m - j > a) ? (m - j) : a;
                }
            }
            return posicoes;
        }
    }
}
