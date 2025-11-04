import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import './globals.css';
import Header from './components/header';
import { Providers } from './providers';

const inter = Inter({ subsets: ['latin'] });

export const metadata: Metadata = {
  title: 'Gastos de Produtos',
  description: 'Sistema pra controle de Produto x Receitas.',
};

export const viewport = {
  width: 'device-width',
  initialScale: 1,
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="pt-br">
      <body suppressHydrationWarning={true} className={inter.className}>
        <Providers>
          <Header />
          <div style={{ maxWidth: '1120px', margin: '0 auto', padding: '16px' }}>{children}</div>
        </Providers>
      </body>
    </html>
  );
}
