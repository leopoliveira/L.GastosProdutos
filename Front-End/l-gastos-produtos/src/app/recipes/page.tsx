'use client';

import { useEffect, useState } from 'react';
import { Spinner, Box, Alert, AlertIcon, Text, Button } from '@chakra-ui/react';
import IReadRecipe from '@/common/interfaces/recipe/IReadRecipe';
import RecipeService from '@/common/services/recipe';
import RecipeGrid from '../components/recipe/recipe-data-grid/recipe-data-grid';

export default function Recipes() {
  const [recipes, setRecipes] = useState<IReadRecipe[]>([]);
  const [loading, setLoading] = useState(true);
  const [reRender, setReRender] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const getData = async () => {
    setLoading(true);

    try {
      const response = await RecipeService.GetAllRecipes();
      setRecipes(response);
    } catch (err: any) {
      setError(err?.message ?? 'Erro ao carregar receitas.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    getData();
    setReRender(false);
  }, []);

  useEffect(() => {
    if (!reRender) {
      return;
    }
    getData();
    setReRender(false);
  }, [reRender]);

  return (
    <main>
      {loading ? (
        <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
          <Spinner size="xl" />
        </Box>
      ) : error ? (
        <Box p={4}>
          <Alert status="error" mb={4}>
            <AlertIcon />
            {error}
          </Alert>
          <Button onClick={getData}>Tentar novamente</Button>
        </Box>
      ) : recipes.length === 0 ? (
        <Box p={8} textAlign="center">
          <Text fontSize="lg">Nenhuma receita encontrada.</Text>
        </Box>
      ) : (
        <RecipeGrid recipes={recipes} onSubmit={setReRender} />
      )}
    </main>
  );
}
