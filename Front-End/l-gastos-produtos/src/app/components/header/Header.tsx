import React from "react";
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
          href="/"
          _hover={{ textDecoration: "none", color: "gray.400" }}>
          Amoh Doces
        </Link>
        <Flex gap={12}>
          <Link
            href="/"
            _hover={{ textDecoration: "none", color: "gray.400" }}>
            Home
          </Link>
          <Link
            href="/products"
            _hover={{ textDecoration: "none", color: "gray.400" }}>
            Products
          </Link>
          <Link
            href="/packings"
            _hover={{ textDecoration: "none", color: "gray.400" }}>
            Packings
          </Link>
          <Link
            href="/recipes"
            _hover={{ textDecoration: "none", color: "gray.400" }}>
            Recipes
          </Link>
        </Flex>
      </Flex>
    </Box>
  );
};

export default Header;
