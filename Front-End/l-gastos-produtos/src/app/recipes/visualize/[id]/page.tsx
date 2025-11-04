'use client';

import RecipeVisualizer from '@/app/components/recipe/recipe-visualizer';
import IReadRecipe from '@/common/interfaces/recipe/IReadRecipe';
import RecipeService from '@/common/services/recipe';
import { Alert, AlertIcon, Box, Button, Spinner, Text } from '@chakra-ui/react';
import { useParams } from 'next/navigation';
import { useEffect, useState } from 'react';

export default function VisualizeRecipe() {
  const [recipe, setRecipe] = useState<IReadRecipe>({
    id: '',
    name: '',
    description: '',
    totalCost: 0,
    ingredients: [],
    packings: [],
    quantity: 0,
    sellingValue: 0,
  });
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const { id } = useParams();

  const getData = async (recipeId: string) => {
    setError(null);
    try {
      const response = await RecipeService.GetRecipeById(recipeId);
      setRecipe(response);
    } catch (err: any) {
      setError(err?.message ?? 'Erro ao carregar a receita.');
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);
      if (id) await getData(id as string);
      setIsLoading(false);
    };
    fetchData();
  }, []);
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
      ) : !recipe.id ? (
        <Box p={8} textAlign="center">
          <Text fontSize="lg">Receita não encontrada.</Text>
        </Box>
      ) : (
        <RecipeVisualizer recipe={recipe} />
      )}
    </main>
  );
}
