'use client';

import RecipeVisualizer from '@/app/components/recipe/recipe-visualizer';
import IReadRecipe from '@/common/interfaces/recipe/IReadRecipe';
import RecipeService from '@/common/services/recipe';
import { useParams } from 'next/navigation';
import { useEffect, useState } from 'react';
import { AlertCircle } from 'lucide-react';

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
        <div className="flex justify-center items-center h-screen">
          <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-blue-500"></div>
        </div>
      ) : error ? (
        <div className="p-4">
          <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4 flex items-center" role="alert">
            <AlertCircle className="w-5 h-5 mr-2" />
            <span className="block sm:inline">{error}</span>
          </div>
          <button 
            onClick={() => id && getData(id as string)}
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
          >
            Tentar novamente
          </button>
        </div>
      ) : !recipe.id ? (
        <div className="p-8 text-center">
          <p className="text-lg">Receita não encontrada.</p>
        </div>
      ) : (
        <RecipeVisualizer recipe={recipe} />
      )}
    </main>
  );
}
