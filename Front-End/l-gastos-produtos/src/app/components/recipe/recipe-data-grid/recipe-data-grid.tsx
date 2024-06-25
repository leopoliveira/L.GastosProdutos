"use client";

import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  Flex,
  Heading,
  Input,
  Table,
  Tbody,
  Td,
  Th,
  Thead,
  Tr,
} from "@chakra-ui/react";
import { AddIcon } from "@chakra-ui/icons";
import IReadRecipe from "@/common/interfaces/recipe/IReadRecipe";
import { useRouter } from "next/navigation";

type RecipeGridProps = {
  recipes: IReadRecipe[];
};

const RecipeGrid: React.FC<RecipeGridProps> = ({ recipes }) => {
  const [filter, setFilter] = useState("");
  const [sortedData, setSortedData] =
    useState<IReadRecipe[]>(recipes);
  const [sortConfig, setSortConfig] = useState({
    key: "",
    direction: "",
  });

  const router = useRouter();

  useEffect(() => {
    setSortedData(recipes);
  }, [recipes]);

  const onFilterChange = (e: any) => {
    setFilter(e.target.value);
  };

  const filteredData = sortedData.filter((item) =>
    item.name.toLowerCase().includes(filter.toLowerCase())
  );

  const onSort = (key: any) => {
    let direction = "ascending";

    if (
      sortConfig.key === key &&
      sortConfig.direction === "ascending"
    ) {
      direction = "descending";
    }

    const sortedArray = [...sortedData].sort((a: any, b: any) => {
      if (a[key] < b[key]) {
        return direction === "ascending" ? -1 : 1;
      }
      if (a[key] > b[key]) {
        return direction === "ascending" ? 1 : -1;
      }
      return 0;
    });

    setSortedData(sortedArray);
    setSortConfig({ key, direction });
  };

  const formatCurrency = (value: number) => {
    return new Intl.NumberFormat("pt-BR", {
      style: "currency",
      currency: "BRL",
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    }).format(value);
  };

  const handleShow = (recipe: IReadRecipe) => {};

  const handleEdit = (id: string) => {
    router.push(`/recipes/${id}`);
  };

  const handleDelete = (id: string) => {};

  const handleAdd = () => {
    router.push("/recipes/new");
  };

  return (
    <Box m={2}>
      <Flex justifyContent="center">
        <Heading
          as="h1"
          size="2xl">
          Receitas
        </Heading>
      </Flex>
      <Flex
        justify="flex-end"
        mt={4}
        mb={4}>
        <Button
          colorScheme="teal"
          size="lg"
          variant="outline"
          leftIcon={<AddIcon />}
          onClick={handleAdd}>
          Adicionar
        </Button>
      </Flex>
      <Flex
        mt={2}
        mb={6}>
        <Input
          placeholder="Filtrar por Nome"
          value={filter}
          onChange={onFilterChange}
        />
      </Flex>
      <Table
        variant="striped"
        width="100%"
        size="lg">
        <Thead>
          <Tr>
            <Th
              cursor="pointer"
              textAlign="center"
              onClick={() => onSort("name")}>
              Nome
            </Th>
            <Th
              cursor="pointer"
              textAlign="center"
              onClick={() => onSort("price")}>
              Preço total
            </Th>
            <Th
              cursor="pointer"
              textAlign="center">
              Ações
            </Th>
          </Tr>
        </Thead>
        <Tbody>
          {filteredData.map((item) => (
            <Tr key={item.id}>
              <Td display="none">{item.id}</Td>
              <Td textAlign="center">{item.name}</Td>
              <Td textAlign="center">
                {formatCurrency(item.totalCost)}
              </Td>
              <Td textAlign="center">
                <Button
                  size="sm"
                  colorScheme="green"
                  mr={2}
                  onClick={() => handleShow(item)}>
                  Visualizar
                </Button>
                <Button
                  size="sm"
                  colorScheme="blue"
                  mr={2}
                  onClick={() => handleEdit(item.id)}>
                  Editar
                </Button>
                <Button
                  size="sm"
                  colorScheme="red"
                  onClick={() => handleDelete(item.id)}>
                  Excluir
                </Button>
              </Td>
            </Tr>
          ))}
        </Tbody>
      </Table>
    </Box>
  );
};

export default RecipeGrid;
