'use client';

import React from 'react';
import NextLink from 'next/link';
import { usePathname } from 'next/navigation';
import clsx from 'clsx';

const Sidebar = () => {
  const pathname = usePathname();

  const linkClass = (isActive: boolean) =>
    clsx(
      'block px-4 py-3 rounded-md hover:bg-gray-700 transition-colors',
      isActive ? 'bg-gray-700 font-bold' : 'hover:bg-gray-700'
    );

  return (
    <aside className="bg-gray-800 text-white w-64 min-h-screen p-4 fixed left-0 top-0">
      <div className="mb-8">
        <NextLink
          href="/"
          className="text-xl font-bold hover:text-gray-400 transition-colors block"
        >
          Amo Doces
        </NextLink>
      </div>
      <nav className="flex flex-col gap-2">
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
        <NextLink
          href="/configuration"
          className={linkClass(pathname?.startsWith('/configuration') || false)}
        >
          Configurações
        </NextLink>
      </nav>
    </aside>
  );
};

export default Sidebar;
