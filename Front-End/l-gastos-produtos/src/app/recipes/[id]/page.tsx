'use client';

import RecipeForm from '@/app/components/recipe/recipe-form';
import IReadRecipe from '@/common/interfaces/recipe/IReadRecipe';
import RecipeService from '@/common/services/recipe';
import { Alert, AlertIcon, Box, Button, Spinner, Text, useToast } from '@chakra-ui/react';
import { useParams } from 'next/navigation';
import { useEffect, useState } from 'react';

export default function FormRecipe() {
  const { id } = useParams();
  const [recipe, setRecipe] = useState<IReadRecipe>();
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const toast = useToast();

  const getData = async (id: string) => {
    setIsLoading(true);
    setError(null);
    try {
      const response = await RecipeService.GetRecipeById(id);
      setRecipe(response);
    } catch (err: any) {
      setError(err?.message ?? 'Erro ao carregar a receita.');
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    if (id) {
      getData(id as string);
    }
  }, []);

  const formSubmitCallback = (): void => {
    toast({
      title: `Receita salva com sucesso!`,
      status: 'success',
      duration: 5000,
      isClosable: true,
    });
  };

  return (
    <main>
      {isLoading ? (
        <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
          <Spinner size="xl" />
        </Box>
      ) : error ? (
        <Box p={4}>
          <Alert status="error" mb={4}>
            <AlertIcon />
            {error}
          </Alert>
          <Button onClick={() => id && getData(id as string)}>Tentar novamente</Button>
        </Box>
      ) : !recipe ? (
        <Box p={8} textAlign="center">
          <Text fontSize="lg">Receita não encontrada.</Text>
        </Box>
      ) : (
        <RecipeForm recipe={recipe} onFormSubmit={formSubmitCallback} />
      )}
    </main>
  );
}
