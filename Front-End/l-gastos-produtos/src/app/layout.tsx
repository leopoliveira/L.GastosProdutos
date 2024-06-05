import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import Header from "./components/header";
import CssBaseline from "@mui/material/CssBaseline";
import Head from "next/head";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: "Gastos de Produtos",
  description: "Sistema pra controler de Produto x Receitas.",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="pt-br">
      <body
        suppressHydrationWarning={true}
        className={inter.className}>
        <Head>
          <meta
            name="viewport"
            content="initial-scale=1, width=device-width"
          />
        </Head>
        <CssBaseline />
        <Header />
        {children}
      </body>
    </html>
  );
}
