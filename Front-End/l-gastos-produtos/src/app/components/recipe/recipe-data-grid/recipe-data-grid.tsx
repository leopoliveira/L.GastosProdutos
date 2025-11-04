'use client';

import React, { useCallback, useEffect, useMemo, useState } from 'react';
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
  useToast,
} from '@chakra-ui/react';
import { AddIcon } from '@chakra-ui/icons';
import IReadRecipe from '@/common/interfaces/recipe/IReadRecipe';
import { useRouter } from 'next/navigation';
import RecipeDeleteModal from '../recipe-delete-modal';
import { formatCurrency } from '@/common/services/utils/utils';

type RecipeGridProps = {
  recipes: IReadRecipe[];
  onSubmit: React.Dispatch<React.SetStateAction<boolean>>;
};

const RecipeGrid: React.FC<RecipeGridProps> = ({ recipes, onSubmit }) => {
  const [filter, setFilter] = useState('');
  const [sortedData, setSortedData] = useState<IReadRecipe[]>(recipes);
  const [sortConfig, setSortConfig] = useState<{
    key: keyof IReadRecipe | null;
    direction: 'asc' | 'desc';
  }>({
    key: null,
    direction: 'asc',
  });
  const [openDeleteModal, setOpenDeleteModal] = useState(false);
  const [selectedRecipeId, setSelectedRecipeId] = useState<string>('');
  const toast = useToast();

  const router = useRouter();

  useEffect(() => {
    setSortedData(recipes);
  }, [recipes]);

  const onFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFilter(e.target.value);
  };

  const filteredData = useMemo(() => {
    const lower = filter.toLowerCase();
    return sortedData.filter((item) => item.name.toLowerCase().includes(lower));
  }, [sortedData, filter]);

  const onSort = (key: keyof IReadRecipe) => {
    const nextDirection: 'asc' | 'desc' =
      sortConfig.key === key && sortConfig.direction === 'asc' ? 'desc' : 'asc';
    const sortedArray = [...sortedData].sort((a, b) => {
      const av = a[key] as unknown as number | string;
      const bv = b[key] as unknown as number | string;
      if (av < bv) return nextDirection === 'asc' ? -1 : 1;
      if (av > bv) return nextDirection === 'asc' ? 1 : -1;
      return 0;
    });
    setSortedData(sortedArray);
    setSortConfig({ key, direction: nextDirection });
  };

  const handleShow = useCallback(
    (id: string) => {
      router.push(`/recipes/visualize/${id}`);
    },
    [router]
  );

  const handleEdit = useCallback(
    (id: string) => {
      router.push(`/recipes/${id}`);
    },
    [router]
  );

  const handleDelete = useCallback((id: string) => {
    setOpenDeleteModal(true);
    setSelectedRecipeId(id);
  }, []);

  const handleAdd = useCallback(() => {
    router.push('/recipes/new');
  }, [router]);

  const handleSubmitDelete = () => {
    setOpenDeleteModal(false);
    setSelectedRecipeId('');

    toast({
      title: 'Receita excluída com sucesso!',
      status: 'success',
      duration: 5000,
      isClosable: true,
    });

    onSubmit(true);
  };

  const unitCost = (item: IReadRecipe): number => {
    return item.totalCost / item.quantity;
  };

  return (
    <>
      <Box m={2}>
        <Flex justifyContent="center">
          <Heading as="h1" size="2xl">
            Receitas
          </Heading>
        </Flex>
        <Flex justify="flex-end" mt={4} mb={4}>
          <Button
            colorScheme="teal"
            size="lg"
            variant="outline"
            leftIcon={<AddIcon />}
            onClick={handleAdd}
          >
            Adicionar
          </Button>
        </Flex>
        <Flex mt={2} mb={6}>
          <Input placeholder="Filtrar por Nome" value={filter} onChange={onFilterChange} />
        </Flex>
        <Box overflowX="auto">
          <Table variant="striped" width="100%" size="lg">
            <Thead>
              <Tr>
                <Th cursor="pointer" textAlign="center" onClick={() => onSort('name')}>
                  Nome
                </Th>
                <Th cursor="pointer" textAlign="center" onClick={() => onSort('totalCost')}>
                  Custo Total
                </Th>
                <Th cursor="pointer" textAlign="center" onClick={() => onSort('quantity')}>
                  Qtde. Produzida
                </Th>
                <Th textAlign="center">Custo por Unidade</Th>
                <Th cursor="pointer" textAlign="center" onClick={() => onSort('sellingValue')}>
                  Preço de Venda da Un.
                </Th>
                <Th textAlign="center">Lucro por Un.</Th>
                <Th textAlign="center">Ações</Th>
              </Tr>
            </Thead>
            <Tbody>
              {filteredData.length === 0 ? (
                <Tr>
                  <Td textAlign="center" colSpan={7}>Nenhum registro encontrado.</Td>
                </Tr>
              ) : (
                filteredData.map((item) => (
                <Tr key={item.id}>
                  <Td display="none">{item.id}</Td>
                  <Td textAlign="center">{item.name}</Td>
                  <Td textAlign="center">{formatCurrency(item.totalCost)}</Td>
                  <Td textAlign="center">{item.quantity}</Td>
                  <Td textAlign="center">{formatCurrency(unitCost(item))}</Td>
                  <Td textAlign="center">{formatCurrency(item.sellingValue)}</Td>
                  <Td textAlign="center">{formatCurrency(item.sellingValue - unitCost(item))}</Td>
                  <Td textAlign="center">
                    <Button
                      size="sm"
                      colorScheme="green"
                      mr={2}
                      onClick={() => handleShow(item.id)}
                    >
                      Visualizar
                    </Button>
                    <Button size="sm" colorScheme="blue" mr={2} onClick={() => handleEdit(item.id)}>
                      Editar
                    </Button>
                    <Button size="sm" colorScheme="red" onClick={() => handleDelete(item.id)}>
                      Excluir
                    </Button>
                  </Td>
                </Tr>
                ))
              )}
            </Tbody>
          </Table>
        </Box>
      </Box>

      <RecipeDeleteModal
        phrase="Deseja realmente excluir esta receita?"
        btnConfirmLabel="Excluir"
        btnCancelLabel="Cancelar"
        recipeId={selectedRecipeId}
        isOpen={openDeleteModal}
        onConfirm={handleSubmitDelete}
        onClose={() => setOpenDeleteModal(false)}
      />
    </>
  );
};

export default RecipeGrid;
