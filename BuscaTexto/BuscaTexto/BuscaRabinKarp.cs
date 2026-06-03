using System;
using System.Collections.Generic;

namespace BuscaTexto {
    class BuscaRabinKarp {
        const long q = 10014521L;
        const int d = 128;

        public static List<int> RKSearch(String p, String t) {
            List<int> posicoes = new List<int>();
            int m = p.Length;
            int n = t.Length;
            if (n < m || m == 0) return posicoes;

            long dm = 1, h1 = 0, h2 = 0;
            for (int i = 1; i < m; i++)
                dm = (d * dm) % q;
            
            for (int i = 0; i < m; i++) {
                h1 = (h1 * d + p[i]) % q;
                h2 = (h2 * d + t[i]) % q;
            }

            for (int i = 0; i <= n - m; i++) {
                if (h1 == h2) {
                    int j;
                    for (j = 0; j < m; j++) {
                        if (t[i + j] != p[j]) break;
                    }
                    if (j == m) posicoes.Add(i);
                }
                if (i < n - m) {
                    h2 = (h2 + d * q - t[i] * dm) % q;
                    h2 = (h2 * d + t[i + m]) % q;
                }
            }
            return posicoes;
        }
    }
}
