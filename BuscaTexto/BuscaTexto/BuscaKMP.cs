using System;
using System.Collections.Generic;

namespace BuscaTexto {
    class BuscaKMP {
        static int[] next = new int[1000];

        public static void InitNext(String p) {
            int i = 0, j = -1, m = p.Length;
            next[0] = -1;
            while (i < m) {
                while (j >= 0 && p[i] != p[j])
                    j = next[j];
                i++;
                j++;
                next[i] = j;
            }
        }

        public static List<int> KMPSearch(String p, String t) {
            List<int> posicoes = new List<int>();
            int i = 0, j = 0, m = p.Length, n = t.Length;
            if (m == 0 || n < m) return posicoes;

            InitNext(p);
            while (i < n) {
                while (j >= 0 && t[i] != p[j]) {
                    j = next[j];
                }
                i++;
                j++;
                if (j == m) {
                    posicoes.Add(i - m);
                    j = next[j];
                }
            }
            return posicoes;
        }
    }
}
