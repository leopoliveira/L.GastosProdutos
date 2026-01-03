import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import './globals.css';
import Sidebar from './components/header';
import { Providers } from './providers';

const inter = Inter({ subsets: ['latin'] });

export const metadata: Metadata = {
  title: 'Gastos de Produtos',
  description: 'Sistema pra controle de Produto x Receitas.',
  icons: {
    icon: '/icon.svg',
  },
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
          <Sidebar />
          <div className="ml-64 p-4">{children}</div>
        </Providers>
      </body>
    </html>
  );
}
