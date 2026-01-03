'use client';

import React from 'react';
import NextLink from 'next/link';
import { usePathname } from 'next/navigation';
import clsx from 'clsx';

const Header = () => {
  const pathname = usePathname();

  const linkClass = (isActive: boolean) =>
    clsx(
      'hover:no-underline hover:text-gray-400 transition-colors',
      isActive && 'font-bold'
    );

  return (
    <header className="bg-gray-800 text-white px-4 py-6">
      <div className="flex justify-between items-center">
        <NextLink
          href="/"
          className="hover:no-underline hover:text-gray-400 transition-colors text-lg font-semibold"
        >
          Amo Doces
        </NextLink>
        <div className="flex gap-12">
          <NextLink
            href="/"
            className={linkClass(pathname === '/')}
          >
            Home
          </NextLink>
          <NextLink
            href="/products"
            className={linkClass(pathname?.startsWith('/products') || false)}
          >
            Materia Prima
          </NextLink>
          <NextLink
            href="/packings"
            className={linkClass(pathname?.startsWith('/packings') || false)}
          >
            Embalagens
          </NextLink>
          <NextLink
            href="/recipes"
            className={linkClass(pathname?.startsWith('/recipes') || false)}
          >
            Receitas
          </NextLink>
        </div>
      </div>
    </header>
  );
};

export default Header;
