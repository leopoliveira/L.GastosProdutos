'use client';

import React from 'react';
import NextLink from 'next/link';
import { Box, Flex, Link } from '@chakra-ui/react';
import { usePathname } from 'next/navigation';

const Header = () => {
  const pathname = usePathname();
  return (
    <Box as="header" bg="gray.800" color="white" px={4} py={6}>
      <Flex justify="space-between" align="center">
        <Link
          as={NextLink}
          href="/"
          aria-current={pathname === '/' ? 'page' : undefined}
          _hover={{ textDecoration: 'none', color: 'gray.600' }}
        >
          Amoh Doces
        </Link>
        <Flex gap={12}>
          <Link
            as={NextLink}
            href="/"
            aria-current={pathname === '/' ? 'page' : undefined}
            _hover={{ textDecoration: 'none', color: 'gray.600' }}
          >
            Home
          </Link>
          <Link
            as={NextLink}
            href="/products"
            aria-current={pathname?.startsWith('/products') ? 'page' : undefined}
            _hover={{ textDecoration: 'none', color: 'gray.600' }}
          >
            Materia Prima
          </Link>
          <Link
            as={NextLink}
            href="/packings"
            aria-current={pathname?.startsWith('/packings') ? 'page' : undefined}
            _hover={{ textDecoration: 'none', color: 'gray.600' }}
          >
            Embalagens
          </Link>
          <Link
            as={NextLink}
            href="/recipes"
            aria-current={pathname?.startsWith('/recipes') ? 'page' : undefined}
            _hover={{ textDecoration: 'none', color: 'gray.600' }}
          >
            Receitas
          </Link>
        </Flex>
      </Flex>
    </Box>
  );
};

export default Header;
