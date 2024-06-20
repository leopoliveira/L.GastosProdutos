import React from "react";
import NextLink from "next/link";
import { Box, Flex, Link } from "@chakra-ui/react";

const Header = () => {
  return (
    <Box
      as="header"
      bg="gray.800"
      color="white"
      px={4}
      py={6}>
      <Flex
        justify="space-between"
        align="center">
        <Link
          as={NextLink}
          href="/"
          _hover={{ textDecoration: "none", color: "gray.600" }}>
          Amoh Doces
        </Link>
        <Flex gap={12}>
          <Link
            as={NextLink}
            href="/"
            _hover={{ textDecoration: "none", color: "gray.600" }}>
            Home
          </Link>
          <Link
            as={NextLink}
            href="/products"
            _hover={{ textDecoration: "none", color: "gray.600" }}>
            Materia Prima
          </Link>
          <Link
            as={NextLink}
            href="/packings"
            _hover={{ textDecoration: "none", color: "gray.600" }}>
            Embalagens
          </Link>
          <Link
            as={NextLink}
            href="/recipes"
            _hover={{ textDecoration: "none", color: "gray.600" }}>
            Receitas
          </Link>
        </Flex>
      </Flex>
    </Box>
  );
};

export default Header;
